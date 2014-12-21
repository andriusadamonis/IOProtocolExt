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
using System.Diagnostics;

using KeePass.App;
using KeePass.Util;

using KeePassLib.Utility;

namespace IOProtocolExt
{
	public static class WinScpExecutor
	{
		private static string m_strPath = null;
		private static string GetPath()
		{
			if(m_strPath != null) return m_strPath;

			string strBaseDir = UrlUtil.GetFileDirectory(WinUtil.GetExecutable(),
				false, true);
			string[] vExes = Directory.GetFiles(strBaseDir, "WinSCP.com",
				SearchOption.AllDirectories);
			if((vExes != null) && (vExes.Length >= 1))
				m_strPath = UrlUtil.MakeAbsolutePath(WinUtil.GetExecutable(),
					vExes[0]);

			return m_strPath;
		}

		public static string RunScript(string strScript)
		{
			if(IOProtocolExtExt.Host.CommandLineArgs[
				AppDefs.CommandLineOptions.Debug] != null)
				MessageService.ShowInfo(strScript, Environment.StackTrace);

			string strPath = GetPath();
			if(string.IsNullOrEmpty(strPath))
				throw new FileNotFoundException("WinSCP not found!");

			Process p = new Process();

			ProcessStartInfo psi = p.StartInfo;
			if(psi == null) { psi = new ProcessStartInfo(); p.StartInfo = psi; }

			psi.FileName = strPath;
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardInput = true;
			psi.RedirectStandardOutput = true;

			p.Start();

			p.StandardInput.Write(strScript);
			p.StandardInput.Close();

			string strOutput = p.StandardOutput.ReadToEnd();
			if(strOutput == null) strOutput = string.Empty;
			strOutput = FilterOutput(strOutput);

			p.WaitForExit();

			if(p.ExitCode != 0) throw new Exception(strOutput);
			return strOutput;
		}

		private static string FilterOutput(string strOutput)
		{
			strOutput = strOutput.Replace("\r\n", "\n");
			strOutput = strOutput.Replace("\r", "\n");

			string[] vLines = strOutput.Split(new char[] { '\n' },
				StringSplitOptions.RemoveEmptyEntries);

			StringBuilder sb = new StringBuilder();
			foreach(string strLineIt in vLines)
			{
				string strLine = strLineIt.Trim();
				if(strLine.Length == 0) continue;

				if(strLine.StartsWith("winscp>", StrUtil.CaseIgnoreCmp)) continue;
				if(strLine.StartsWith("echo")) continue;
				if(strLine.StartsWith("batch")) continue;
				if(strLine.StartsWith("confirm")) continue;
				if(strLine.StartsWith("transfer")) continue;

				if(sb.Length > 0) sb.Append(MessageService.NewLine);
				sb.Append(strLine);
			}

			return sb.ToString();
		}
	}
}
