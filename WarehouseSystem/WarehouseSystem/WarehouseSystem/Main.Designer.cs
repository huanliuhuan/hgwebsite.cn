namespace WarehouseSystem
{
    partial class Main
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.skinUI1 = new DotNetSkin.SkinUI();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pal_DisplayContent = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.商品管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.供应商管理GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大客户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采购单管理RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售单管理FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存管理VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.权限管理PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(157, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "众泰进销管理系统";
            // 
            // skinUI1
            // 
            this.skinUI1.Active = true;
            this.skinUI1.Button = true;
            this.skinUI1.Caption = true;
            this.skinUI1.CheckBox = true;
            this.skinUI1.ComboBox = true;
            this.skinUI1.ContextMenu = true;
            this.skinUI1.DisableTag = 999;
            this.skinUI1.Edit = true;
            this.skinUI1.GroupBox = true;
            this.skinUI1.ImageList = null;
            this.skinUI1.MaiMenu = true;
            this.skinUI1.Panel = true;
            this.skinUI1.Progress = true;
            this.skinUI1.RadioButton = true;
            this.skinUI1.ScrollBar = true;
            this.skinUI1.SkinFile = null;
            this.skinUI1.SkinSteam = null;
            this.skinUI1.Spin = true;
            this.skinUI1.StatusBar = true;
            this.skinUI1.SystemMenu = true;
            this.skinUI1.TabControl = true;
            this.skinUI1.Text = "Mycontrol1=edit\r\nMycontrol2=edit\r\n";
            this.skinUI1.ToolBar = true;
            this.skinUI1.TrackBar = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 84);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pal_DisplayContent);
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(754, 357);
            this.panel2.TabIndex = 2;
            // 
            // pal_DisplayContent
            // 
            this.pal_DisplayContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_DisplayContent.Location = new System.Drawing.Point(0, 24);
            this.pal_DisplayContent.Name = "pal_DisplayContent";
            this.pal_DisplayContent.Size = new System.Drawing.Size(754, 333);
            this.pal_DisplayContent.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品管理ToolStripMenuItem,
            this.供应商管理GToolStripMenuItem,
            this.大客户ToolStripMenuItem,
            this.采购单管理RToolStripMenuItem,
            this.销售单管理FToolStripMenuItem,
            this.库存管理VToolStripMenuItem,
            this.系统设置IToolStripMenuItem,
            this.注销ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(754, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 商品管理ToolStripMenuItem
            // 
            this.商品管理ToolStripMenuItem.Name = "商品管理ToolStripMenuItem";
            this.商品管理ToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.商品管理ToolStripMenuItem.Text = "商品管理(&Q)";
            this.商品管理ToolStripMenuItem.Click += new System.EventHandler(this.商品管理ToolStripMenuItem_Click);
            // 
            // 供应商管理GToolStripMenuItem
            // 
            this.供应商管理GToolStripMenuItem.Name = "供应商管理GToolStripMenuItem";
            this.供应商管理GToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.供应商管理GToolStripMenuItem.Text = "供应商管理(&A)";
            this.供应商管理GToolStripMenuItem.Click += new System.EventHandler(this.供应商管理GToolStripMenuItem_Click);
            // 
            // 大客户ToolStripMenuItem
            // 
            this.大客户ToolStripMenuItem.Name = "大客户ToolStripMenuItem";
            this.大客户ToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.大客户ToolStripMenuItem.Text = "大客户管理(&Z)";
            this.大客户ToolStripMenuItem.Click += new System.EventHandler(this.大客户ToolStripMenuItem_Click);
            // 
            // 采购单管理RToolStripMenuItem
            // 
            this.采购单管理RToolStripMenuItem.Name = "采购单管理RToolStripMenuItem";
            this.采购单管理RToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.采购单管理RToolStripMenuItem.Text = "采购单管理(&R)";
            this.采购单管理RToolStripMenuItem.Click += new System.EventHandler(this.采购单管理RToolStripMenuItem_Click);
            // 
            // 销售单管理FToolStripMenuItem
            // 
            this.销售单管理FToolStripMenuItem.Name = "销售单管理FToolStripMenuItem";
            this.销售单管理FToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.销售单管理FToolStripMenuItem.Text = "销售单管理(&F)";
            this.销售单管理FToolStripMenuItem.Click += new System.EventHandler(this.销售单管理FToolStripMenuItem_Click);
            // 
            // 库存管理VToolStripMenuItem
            // 
            this.库存管理VToolStripMenuItem.Name = "库存管理VToolStripMenuItem";
            this.库存管理VToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.库存管理VToolStripMenuItem.Text = "库存管理(V)";
            // 
            // 系统设置IToolStripMenuItem
            // 
            this.系统设置IToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户管理OToolStripMenuItem,
            this.权限管理PToolStripMenuItem});
            this.系统设置IToolStripMenuItem.Name = "系统设置IToolStripMenuItem";
            this.系统设置IToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.系统设置IToolStripMenuItem.Text = "系统设置(&I)";
            // 
            // 用户管理OToolStripMenuItem
            // 
            this.用户管理OToolStripMenuItem.Name = "用户管理OToolStripMenuItem";
            this.用户管理OToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.用户管理OToolStripMenuItem.Text = "用户管理(&O)";
            // 
            // 权限管理PToolStripMenuItem
            // 
            this.权限管理PToolStripMenuItem.Name = "权限管理PToolStripMenuItem";
            this.权限管理PToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.权限管理PToolStripMenuItem.Text = "权限管理(&P)";
            // 
            // 注销ToolStripMenuItem
            // 
            this.注销ToolStripMenuItem.Name = "注销ToolStripMenuItem";
            this.注销ToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.注销ToolStripMenuItem.Text = "注销(&M)";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 441);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "众";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DotNetSkin.SkinUI skinUI1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 商品管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 供应商管理GToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大客户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采购单管理RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售单管理FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存管理VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 权限管理PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注销ToolStripMenuItem;
        private System.Windows.Forms.Panel pal_DisplayContent;
    }
}