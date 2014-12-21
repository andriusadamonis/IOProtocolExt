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
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using KeePass.UI;

using KeePassLib;

namespace IOProtocolExt
{
	public sealed class WinScpJitStream : Stream
	{
		private string m_strScript;
		private string m_strTempFile;
		private FileStream m_s = null;
		private FileMode m_fm;
		private FileAccess m_fa;
		private FileShare m_fs;

		private object m_objDownloadSync = new object();
		private volatile bool m_bFileDownloaded = false;
		private volatile Exception m_exDownload = null;

		public override bool CanRead
		{
			get { return ((m_s != null) ? m_s.CanRead : true); }
		}

		public override bool CanSeek
		{
			get { return ((m_s != null) ? m_s.CanSeek : true); }
		}

		public override bool CanWrite
		{
			get { return ((m_s != null) ? m_s.CanWrite : false); }
		}

		public override long Length
		{
			get
			{
				EnsureStream();
				return m_s.Length;
			}
		}

		public override long Position
		{
			get { return ((m_s != null) ? m_s.Position : 0); }
			set
			{
				if(m_s != null) m_s.Position = value;
				else if(value > 0)
				{
					EnsureStream();
					m_s.Position = value;
				}
			}
		}

		public WinScpJitStream(string strScript, string strTempFile, FileMode fm,
			FileAccess fa, FileShare fs)
		{
			m_strScript = strScript;
			m_strTempFile = strTempFile;
			m_fm = fm;
			m_fa = fa;
			m_fs = fs;
		}

		private void EnsureStream()
		{
			if(m_s != null) return;

			StatusStateInfo st = StatusUtil.Begin("Downloading file...");

			try
			{
				m_bFileDownloaded = false;
				m_exDownload = null;

				WaitCallback wc = new WaitCallback(DownloadFileProc);
				ThreadPool.QueueUserWorkItem(wc);

				bool bFinished = false;
				while(!bFinished)
				{
					lock(m_objDownloadSync) { bFinished = m_bFileDownloaded; }

					Thread.Sleep(100);
					Application.DoEvents();
				}

				if(m_exDownload != null) throw m_exDownload;

				m_s = new FileStream(m_strTempFile, m_fm, m_fa, m_fs);
			}
			finally { StatusUtil.End(st); }
		}

		private void DownloadFileProc(object objState)
		{
			try { WinScpExecutor.RunScript(m_strScript); }
			catch(Exception exDl) { m_exDownload = exDl; }

			lock(m_objDownloadSync) { m_bFileDownloaded = true; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			EnsureStream();
			return m_s.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			EnsureStream();
			return m_s.Seek(offset, origin);
		}

		public override void Flush()
		{
			EnsureStream();
			m_s.Flush();
		}

		public override void SetLength(long value)
		{
			EnsureStream();
			m_s.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			EnsureStream();
			m_s.Write(buffer, offset, count);
		}

		public override void Close()
		{
			if(m_s != null) { m_s.Close(); m_s = null; }

			base.Close();

			if(File.Exists(m_strTempFile)) File.Delete(m_strTempFile);
		}
	}
}
