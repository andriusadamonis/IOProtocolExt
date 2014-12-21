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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using KeePass.App.Configuration;
using KeePass.UI;

namespace IOProtocolExt.Forms
{
	public partial class IopOptionsForm : Form
	{
		public IopOptionsForm()
		{
			InitializeComponent();
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			GlobalWindowManager.AddWindow(this);

			this.Text = IopDefs.ProductName + " Options";

			AceCustomConfig cfg = IOProtocolExtExt.Host.CustomConfig;

			ulong uTimeout = cfg.GetULong(IopDefs.OptTimeout, 0);
			m_cbTimeout.Checked = (uTimeout > 0);
			if(uTimeout > 0)
			{
				try { m_numTimeout.Value = uTimeout; }
				catch(Exception) { Debug.Assert(false); }
			}

			m_cbFtpsImplicit.Checked = cfg.GetBool(IopDefs.OptFtpsImplicit, false);
			m_cbFtpsExplicitSsl.Checked = cfg.GetBool(IopDefs.OptFtpsExplicitSsl, false);
			m_cbFtpsExplicitTls.Checked = cfg.GetBool(IopDefs.OptFtpsExplicitTls, false);

			EnableControlsEx();
		}

		private void EnableControlsEx()
		{
			m_numTimeout.Enabled = m_cbTimeout.Checked;
		}

		private void OnTimeoutCheckedChanged(object sender, EventArgs e)
		{
			EnableControlsEx();
		}

		private void OnBtnOK(object sender, EventArgs e)
		{
			AceCustomConfig cfg = IOProtocolExtExt.Host.CustomConfig;

			cfg.SetULong(IopDefs.OptTimeout, (m_cbTimeout.Checked ?
				(ulong)m_numTimeout.Value : 0UL));

			CfgSetBool(cfg, IopDefs.OptFtpsImplicit, m_cbFtpsImplicit.Checked, false);
			CfgSetBool(cfg, IopDefs.OptFtpsExplicitSsl, m_cbFtpsExplicitSsl.Checked, false);
			CfgSetBool(cfg, IopDefs.OptFtpsExplicitTls, m_cbFtpsExplicitTls.Checked, false);
		}

		private static void CfgSetBool(AceCustomConfig cfg, string strKey,
			bool bValue, bool bDefault)
		{
			if(bValue != bDefault) cfg.SetBool(strKey, bValue);
			else
			{
				try { cfg.SetString(strKey, null); }
				catch(Exception) { Debug.Assert(false); cfg.SetBool(strKey, bValue); }
			}
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			GlobalWindowManager.RemoveWindow(this);
		}
	}
}
