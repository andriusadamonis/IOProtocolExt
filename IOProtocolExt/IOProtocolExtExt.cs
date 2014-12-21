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
using System.Windows.Forms;
using System.Diagnostics;

using IOProtocolExt.Forms;

using KeePass.Ecas;
using KeePass.Plugins;
using KeePass.UI;

using KeePassLib;
using KeePassLib.Native;
using KeePassLib.Utility;

namespace IOProtocolExt
{
	// Plugin name 'IOProtocolExt' + class suffix 'Ext' = 'IOProtocolExtExt'
	public sealed class IOProtocolExtExt : Plugin
	{
		private static IPluginHost m_host = null;
		private WinScpWebRequestCreator m_wrcWinScp = null;

		private ToolStripSeparator m_tsSep = null;
		private ToolStripMenuItem m_tsOptions = null;

		internal static IPluginHost Host { get { return m_host; } }

		private static bool m_bMainFormLoading = true;
		internal static bool MainFormLoading { get { return m_bMainFormLoading; } }

		private static readonly PwUuid EcasAppLoadPost = new PwUuid(new byte[] {
			0xD8, 0xF3, 0x1E, 0xE9, 0xCC, 0x69, 0x48, 0x1B,
			0x89, 0xC5, 0xFC, 0xE2, 0xEA, 0x4B, 0x6A, 0x97
		});

		public override bool Initialize(IPluginHost host)
		{
			if(m_host != null) Terminate();
			if(host == null) return false;
			if(NativeLib.IsUnix()) return false;

			// #pragma warning disable 162
			// if(PwDefs.Version32 <= 0x02010500)
			// {
			//	MessageService.ShowWarning("IOProtocolExt",
			//		"This plugin requires KeePass 2.16 or higher.");
			//	return false;
			// }
			// #pragma warning restore 162

			m_host = host;
			m_host.TriggerSystem.RaisingEvent += this.OnEcasEvent;

			m_tsSep = new ToolStripSeparator();
			m_host.MainWindow.ToolsMenu.DropDownItems.Add(m_tsSep);

			m_tsOptions = new ToolStripMenuItem(IopDefs.ProductName + " Options...");
			m_tsOptions.Click += this.OnOptions;
			m_host.MainWindow.ToolsMenu.DropDownItems.Add(m_tsOptions);

			m_wrcWinScp = new WinScpWebRequestCreator();
			m_wrcWinScp.Register();

			return true;
		}

		public override void Terminate()
		{
			if(m_host != null)
			{
				m_tsOptions.Click -= this.OnOptions;
				m_host.MainWindow.ToolsMenu.DropDownItems.Remove(m_tsOptions);
				m_tsOptions = null;

				m_host.MainWindow.ToolsMenu.DropDownItems.Remove(m_tsSep);
				m_tsSep = null;

				m_host.TriggerSystem.RaisingEvent -= this.OnEcasEvent;
				m_host = null;
			}
		}

		private void OnEcasEvent(object sender, EcasRaisingEventArgs e)
		{
			if(e.Event.Type.Equals(EcasAppLoadPost))
				m_bMainFormLoading = false;
		}

		private void OnOptions(object sender, EventArgs e)
		{
			IopOptionsForm dlg = new IopOptionsForm();
			UIUtil.ShowDialogAndDestroy(dlg);
		}
	}
}
