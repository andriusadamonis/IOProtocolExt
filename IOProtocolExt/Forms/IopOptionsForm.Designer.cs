namespace IOProtocolExt.Forms
{
	partial class IopOptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_btnOK = new System.Windows.Forms.Button();
			this.m_btnCancel = new System.Windows.Forms.Button();
			this.m_grpConn = new System.Windows.Forms.GroupBox();
			this.m_numTimeout = new System.Windows.Forms.NumericUpDown();
			this.m_cbTimeout = new System.Windows.Forms.CheckBox();
			this.m_grpFtps = new System.Windows.Forms.GroupBox();
			this.m_cbFtpsExplicitTls = new System.Windows.Forms.CheckBox();
			this.m_cbFtpsExplicitSsl = new System.Windows.Forms.CheckBox();
			this.m_cbFtpsImplicit = new System.Windows.Forms.CheckBox();
			this.m_tbPrivateKey = new System.Windows.Forms.TextBox();
			this.m_lbPrivateKey = new System.Windows.Forms.Label();
			this.m_grpConn.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_numTimeout)).BeginInit();
			this.m_grpFtps.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_btnOK
			// 
			this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_btnOK.Location = new System.Drawing.Point(175, 205);
			this.m_btnOK.Name = "m_btnOK";
			this.m_btnOK.Size = new System.Drawing.Size(75, 23);
			this.m_btnOK.TabIndex = 0;
			this.m_btnOK.Text = "&OK";
			this.m_btnOK.UseVisualStyleBackColor = true;
			this.m_btnOK.Click += new System.EventHandler(this.OnBtnOK);
			// 
			// m_btnCancel
			// 
			this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnCancel.Location = new System.Drawing.Point(256, 205);
			this.m_btnCancel.Name = "m_btnCancel";
			this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
			this.m_btnCancel.TabIndex = 1;
			this.m_btnCancel.Text = "&Cancel";
			this.m_btnCancel.UseVisualStyleBackColor = true;
			// 
			// m_grpConn
			// 
			this.m_grpConn.Controls.Add(this.m_numTimeout);
			this.m_grpConn.Controls.Add(this.m_cbTimeout);
			this.m_grpConn.Location = new System.Drawing.Point(12, 12);
			this.m_grpConn.Name = "m_grpConn";
			this.m_grpConn.Size = new System.Drawing.Size(319, 52);
			this.m_grpConn.TabIndex = 2;
			this.m_grpConn.TabStop = false;
			this.m_grpConn.Text = "Connection";
			// 
			// m_numTimeout
			// 
			this.m_numTimeout.Location = new System.Drawing.Point(221, 19);
			this.m_numTimeout.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.m_numTimeout.Name = "m_numTimeout";
			this.m_numTimeout.Size = new System.Drawing.Size(61, 20);
			this.m_numTimeout.TabIndex = 1;
			this.m_numTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_cbTimeout
			// 
			this.m_cbTimeout.AutoSize = true;
			this.m_cbTimeout.Location = new System.Drawing.Point(10, 20);
			this.m_cbTimeout.Name = "m_cbTimeout";
			this.m_cbTimeout.Size = new System.Drawing.Size(205, 17);
			this.m_cbTimeout.TabIndex = 0;
			this.m_cbTimeout.Text = "Custom server response timeout [sec]:";
			this.m_cbTimeout.UseVisualStyleBackColor = true;
			this.m_cbTimeout.CheckedChanged += new System.EventHandler(this.OnTimeoutCheckedChanged);
			// 
			// m_grpFtps
			// 
			this.m_grpFtps.Controls.Add(this.m_cbFtpsExplicitTls);
			this.m_grpFtps.Controls.Add(this.m_cbFtpsExplicitSsl);
			this.m_grpFtps.Controls.Add(this.m_cbFtpsImplicit);
			this.m_grpFtps.Location = new System.Drawing.Point(12, 70);
			this.m_grpFtps.Name = "m_grpFtps";
			this.m_grpFtps.Size = new System.Drawing.Size(319, 91);
			this.m_grpFtps.TabIndex = 3;
			this.m_grpFtps.TabStop = false;
			this.m_grpFtps.Text = "FTPS";
			// 
			// m_cbFtpsExplicitTls
			// 
			this.m_cbFtpsExplicitTls.AutoSize = true;
			this.m_cbFtpsExplicitTls.Location = new System.Drawing.Point(10, 65);
			this.m_cbFtpsExplicitTls.Name = "m_cbFtpsExplicitTls";
			this.m_cbFtpsExplicitTls.Size = new System.Drawing.Size(82, 17);
			this.m_cbFtpsExplicitTls.TabIndex = 2;
			this.m_cbFtpsExplicitTls.Text = "Explicit TLS";
			this.m_cbFtpsExplicitTls.UseVisualStyleBackColor = true;
			// 
			// m_cbFtpsExplicitSsl
			// 
			this.m_cbFtpsExplicitSsl.AutoSize = true;
			this.m_cbFtpsExplicitSsl.Location = new System.Drawing.Point(10, 42);
			this.m_cbFtpsExplicitSsl.Name = "m_cbFtpsExplicitSsl";
			this.m_cbFtpsExplicitSsl.Size = new System.Drawing.Size(82, 17);
			this.m_cbFtpsExplicitSsl.TabIndex = 1;
			this.m_cbFtpsExplicitSsl.Text = "Explicit SSL";
			this.m_cbFtpsExplicitSsl.UseVisualStyleBackColor = true;
			// 
			// m_cbFtpsImplicit
			// 
			this.m_cbFtpsImplicit.AutoSize = true;
			this.m_cbFtpsImplicit.Location = new System.Drawing.Point(10, 19);
			this.m_cbFtpsImplicit.Name = "m_cbFtpsImplicit";
			this.m_cbFtpsImplicit.Size = new System.Drawing.Size(106, 17);
			this.m_cbFtpsImplicit.TabIndex = 0;
			this.m_cbFtpsImplicit.Text = "Implicit SSL/TLS";
			this.m_cbFtpsImplicit.UseVisualStyleBackColor = true;
			// 
			// m_tbPrivateKey
			// 
			this.m_tbPrivateKey.Location = new System.Drawing.Point(93, 168);
			this.m_tbPrivateKey.Name = "m_tbPrivateKey";
			this.m_tbPrivateKey.Size = new System.Drawing.Size(227, 20);
			this.m_tbPrivateKey.TabIndex = 4;
			// 
			// m_lbPrivateKey
			// 
			this.m_lbPrivateKey.Location = new System.Drawing.Point(25, 171);
			this.m_lbPrivateKey.Name = "m_lbPrivateKey";
			this.m_lbPrivateKey.Size = new System.Drawing.Size(62, 23);
			this.m_lbPrivateKey.TabIndex = 5;
			this.m_lbPrivateKey.Text = "Private key";
			// 
			// IopOptionsForm
			// 
			this.AcceptButton = this.m_btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_btnCancel;
			this.ClientSize = new System.Drawing.Size(343, 239);
			this.Controls.Add(this.m_lbPrivateKey);
			this.Controls.Add(this.m_tbPrivateKey);
			this.Controls.Add(this.m_grpFtps);
			this.Controls.Add(this.m_grpConn);
			this.Controls.Add(this.m_btnCancel);
			this.Controls.Add(this.m_btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IopOptionsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "<>";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.m_grpConn.ResumeLayout(false);
			this.m_grpConn.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_numTimeout)).EndInit();
			this.m_grpFtps.ResumeLayout(false);
			this.m_grpFtps.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.GroupBox m_grpConn;
		private System.Windows.Forms.NumericUpDown m_numTimeout;
		private System.Windows.Forms.CheckBox m_cbTimeout;
		private System.Windows.Forms.GroupBox m_grpFtps;
		private System.Windows.Forms.CheckBox m_cbFtpsExplicitSsl;
		private System.Windows.Forms.CheckBox m_cbFtpsImplicit;
		private System.Windows.Forms.CheckBox m_cbFtpsExplicitTls;
		private System.Windows.Forms.TextBox m_tbPrivateKey;
		private System.Windows.Forms.Label m_lbPrivateKey;
	}
}