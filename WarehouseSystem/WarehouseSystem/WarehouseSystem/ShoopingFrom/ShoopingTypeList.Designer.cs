namespace WarehouseSystem.ShoopingFrom
{
    partial class ShoopingTypeList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShoopingTypeList));
            this.skinUI1 = new DotNetSkin.SkinUI();
            this.treeGridView1 = new AdvancedDataGridView.TreeGridView();
            this.imageStrip = new System.Windows.Forms.ImageList(this.components);
            this.GTID = new AdvancedDataGridView.TreeGridColumn();
            this.GTName = new AdvancedDataGridView.TreeGridColumn();
            this.GTPID = new AdvancedDataGridView.TreeGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).BeginInit();
            this.SuspendLayout();
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
            // treeGridView1
            // 
            this.treeGridView1.AllowUserToAddRows = false;
            this.treeGridView1.AllowUserToDeleteRows = false;
            this.treeGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.treeGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.treeGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.treeGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTID,
            this.GTName,
            this.GTPID});
            this.treeGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeGridView1.ImageList = null;
            this.treeGridView1.Location = new System.Drawing.Point(79, 51);
            this.treeGridView1.Name = "treeGridView1";
            this.treeGridView1.Size = new System.Drawing.Size(484, 296);
            this.treeGridView1.TabIndex = 7;
            // 
            // imageStrip
            // 
            this.imageStrip.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageStrip.ImageSize = new System.Drawing.Size(16, 16);
            this.imageStrip.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GTID
            // 
            this.GTID.DefaultNodeImage = null;
            this.GTID.HeaderText = "GTID";
            this.GTID.Name = "GTID";
            this.GTID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GTID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GTID.Visible = false;
            this.GTID.Width = 35;
            // 
            // GTName
            // 
            this.GTName.DefaultNodeImage = null;
            this.GTName.HeaderText = "类型名称";
            this.GTName.Name = "GTName";
            this.GTName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GTName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GTName.Width = 59;
            // 
            // GTPID
            // 
            this.GTPID.DefaultNodeImage = null;
            this.GTPID.HeaderText = "GTPID";
            this.GTPID.Name = "GTPID";
            this.GTPID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GTPID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GTPID.Visible = false;
            this.GTPID.Width = 41;
            // 
            // ShoopingTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 437);
            this.Controls.Add(this.treeGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShoopingTypeList";
            this.Text = "商品类型管理";
            this.Load += new System.EventHandler(this.ShoopingTypeList_Load);
            this.Shown += new System.EventHandler(this.ShoopingTypeList_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DotNetSkin.SkinUI skinUI1;
        private AdvancedDataGridView.TreeGridView treeGridView1;
        private System.Windows.Forms.ImageList imageStrip;
        private AdvancedDataGridView.TreeGridColumn GTID;
        private AdvancedDataGridView.TreeGridColumn GTName;
        private AdvancedDataGridView.TreeGridColumn GTPID;
    }
}