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
using System.Net;
using System.Diagnostics;

namespace IOProtocolExt
{
	public sealed class WinScpWebRequestCreator : IWebRequestCreate
	{
		private static readonly string[] m_vSupportedPrefixes = new string[] {
			// The following protocols must be registered without a
			// terminating "//"; when e.g. trying to register "scp://",
			// the RegisterPrefix method of .NET 4.0 actually registers
			// the prefix "scp:///" (obviously a bug; this doesn't happen
			// in .NET 2.0)
			"sftp:", "scp:", "ftps:",

			// The FTP protocol can't be registered by "ftp:", because
			// .NET already has assigned this to an FtpWebRequestCreator
			"ftp:/"
		};

		public void Register()
		{
			foreach(string strPrefix in m_vSupportedPrefixes)
				WebRequest.RegisterPrefix(strPrefix, this);
		}

		public WebRequest Create(Uri uri)
		{
			return new WinScpWebRequest(uri);
		}
	}
}
