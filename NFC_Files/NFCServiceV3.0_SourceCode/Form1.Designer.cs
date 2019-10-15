using System.Windows.Forms;

namespace NFC_ID
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.getNFC_btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ClearNFC_btn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.myMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadData_btn = new System.Windows.Forms.Button();
            this.myMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // getNFC_btn
            // 
            this.getNFC_btn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.getNFC_btn.Location = new System.Drawing.Point(180, 26);
            this.getNFC_btn.Name = "getNFC_btn";
            this.getNFC_btn.Size = new System.Drawing.Size(130, 50);
            this.getNFC_btn.TabIndex = 0;
            this.getNFC_btn.Text = "GetNFC_uid";
            this.getNFC_btn.UseVisualStyleBackColor = true;
            this.getNFC_btn.Click += new System.EventHandler(this.GetNFC_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(115, 99);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(462, 287);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // ClearNFC_btn
            // 
            this.ClearNFC_btn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearNFC_btn.Location = new System.Drawing.Point(431, 26);
            this.ClearNFC_btn.Name = "ClearNFC_btn";
            this.ClearNFC_btn.Size = new System.Drawing.Size(146, 50);
            this.ClearNFC_btn.TabIndex = 2;
            this.ClearNFC_btn.Text = "ClearText_uid";
            this.ClearNFC_btn.UseVisualStyleBackColor = true;
            this.ClearNFC_btn.Click += new System.EventHandler(this.ClearNFC_btn_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.myMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "NFC";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick_1);
            // 
            // myMenu
            // 
            this.myMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.myMenu.Name = "contextMenuStrip1";
            this.myMenu.Size = new System.Drawing.Size(211, 104);
            this.myMenu.Text = "显示窗口";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem1.Text = "显示窗口";
            this.toolStripMenuItem1.Visible = false;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem2.Text = "隐藏窗口";
            this.toolStripMenuItem2.Visible = false;
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem3.Text = "退出程序";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // ReadData_btn
            // 
            this.ReadData_btn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadData_btn.Location = new System.Drawing.Point(624, 115);
            this.ReadData_btn.Name = "ReadData_btn";
            this.ReadData_btn.Size = new System.Drawing.Size(136, 53);
            this.ReadData_btn.TabIndex = 3;
            this.ReadData_btn.Text = "ReadNFC_data";
            this.ReadData_btn.UseVisualStyleBackColor = true;
            this.ReadData_btn.Click += new System.EventHandler(this.ReadData_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ReadData_btn);
            this.Controls.Add(this.ClearNFC_btn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.getNFC_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "NFC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.myMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button getNFC_btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ClearNFC_btn;
        private System.Windows.Forms.Button ReadData_btn;
        private System.Windows.Forms.ContextMenuStrip myMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}

