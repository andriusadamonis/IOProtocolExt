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
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using KeePass.Forms;
using KeePass.UI;

using KeePassLib;

namespace IOProtocolExt
{
	public sealed class StatusStateInfo
	{
		private ProgressBarStyle m_pbs = ProgressBarStyle.Continuous;
		public ProgressBarStyle Style
		{
			get { return m_pbs; }
			set { m_pbs = value; }
		}

		private bool m_bVisible = true;
		public bool Visible
		{
			get { return m_bVisible; }
			set { m_bVisible = value; }
		}

		private StatusProgressForm m_form = null;
		public StatusProgressForm Form
		{
			get { return m_form; }
			set { m_form = value; }
		}

		public StatusStateInfo()
		{
		}

		public StatusStateInfo(ProgressBarStyle pbs)
		{
			m_pbs = pbs;
		}
	}

	public static class StatusUtil
	{
		private static StatusProgressForm BeginStatusDialog(string strText)
		{
			StatusProgressForm dlg = new StatusProgressForm();
			dlg.InitEx(PwDefs.ShortProductName, false, true, null);
			dlg.Show();
			dlg.StartLogging(strText ?? string.Empty, false);

			return dlg;
		}

		private static void EndStatusDialog(StatusProgressForm dlg)
		{
			if(dlg == null) { Debug.Assert(false); return; }

			dlg.EndLogging();
			dlg.Close();
			UIUtil.DestroyForm(dlg);
		}

		public static StatusStateInfo Begin(string strText)
		{
			StatusStateInfo s = new StatusStateInfo();
			
			try
			{
				MainForm mf = IOProtocolExtExt.Host.MainWindow;

				if(!IOProtocolExtExt.MainFormLoading)
				{
					mf.SetStatusEx(strText);

					s.Visible = mf.MainProgressBar.Visible;
					if(!s.Visible) mf.MainProgressBar.Visible = true;

					s.Style = mf.MainProgressBar.Style;
					mf.MainProgressBar.Style = ProgressBarStyle.Marquee;
				}
				else s.Form = BeginStatusDialog(strText);
			}
			catch(Exception) { Debug.Assert(false); }

			return s;
		}

		public static void End(StatusStateInfo s)
		{
			if(s == null) { Debug.Assert(false); return; }

			try
			{
				if(s.Form != null)
				{
					EndStatusDialog(s.Form);
					s.Form = null;
				}
				else
				{
					MainForm mf = IOProtocolExtExt.Host.MainWindow;
					mf.MainProgressBar.Style = s.Style;
					mf.MainProgressBar.Visible = s.Visible;
				}
			}
			catch(Exception) { Debug.Assert(false); }
		}
	}
}
