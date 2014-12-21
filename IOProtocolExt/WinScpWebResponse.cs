/*
  IOProtocolExt Plugin
  Copyright (C) 2011-2014 Dominik Reichl <dominik.reichl@t-online.de>

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace IOProtocolExt
{
	public sealed class WinScpWebResponse : WebResponse
	{
		private Uri m_uriResponse;
		private string m_strScript;
		private string m_strDataFile;
		private bool m_bJit;

		private Stream m_sResponse = null;

		private long m_lSize = 0;
		public override long ContentLength
		{
			get { return m_lSize; }
			set { throw new InvalidOperationException(); }
		}

		public override string ContentType
		{
			get { return "application/octet-stream"; }
			set { throw new InvalidOperationException(); }
		}

		public override Uri ResponseUri
		{
			get { return m_uriResponse; }
		}

		private WebHeaderCollection m_whc;
		public override WebHeaderCollection Headers
		{
			get { return m_whc; }
		}

		public WinScpWebResponse(Uri uriResponse, string strScript, string strDataFile,
			bool bJit, WebHeaderCollection whc)
		{
			if(uriResponse == null) throw new ArgumentNullException("uriResponse");
			m_uriResponse = uriResponse;

			m_strScript = strScript;
			m_strDataFile = strDataFile;
			m_bJit = bJit;
			m_whc = (whc ?? new WebHeaderCollection());

			if(!string.IsNullOrEmpty(m_strDataFile) && File.Exists(m_strDataFile))
			{
				FileInfo fi = new FileInfo(m_strDataFile);
				m_lSize = fi.Length;
			}

			if(!m_bJit) GetResponseStream();
		}

		public override Stream GetResponseStream()
		{
			if(m_sResponse != null) return m_sResponse;

			if(!m_bJit) WinScpExecutor.RunScript(m_strScript);

			if(string.IsNullOrEmpty(m_strDataFile) || !m_bJit)
			{
				byte[] pb = new byte[0];
				// Cache the empty response to avoid double script execution
				m_sResponse = new MemoryStream(pb, false);
				return m_sResponse;
			}

			m_sResponse = new WinScpJitStream(m_strScript, m_strDataFile,
				FileMode.Open, FileAccess.Read, FileShare.Read);
			return m_sResponse;
		}

		public override void Close()
		{
			if(m_sResponse != null) { m_sResponse.Close(); m_sResponse = null; }
		}
	}
}
