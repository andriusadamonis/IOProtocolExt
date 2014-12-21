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

namespace IOProtocolExt
{
	public sealed class CopyMemoryStream : MemoryStream
	{
		private List<byte> m_lCopyBuffer;

		public CopyMemoryStream(List<byte> lCopyBuffer) : base()
		{
			m_lCopyBuffer = lCopyBuffer;
		}

		public override void Close()
		{
			if(m_lCopyBuffer != null)
			{
				m_lCopyBuffer.AddRange(this.ToArray());
				m_lCopyBuffer = null; // Copy once only
			}

			base.Close();
		}
	}
}
