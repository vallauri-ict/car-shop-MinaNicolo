namespace winFormProject
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lbVeicoli = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnAddVeicolo = new System.Windows.Forms.ToolStripButton();
            this.salvaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonWord = new System.Windows.Forms.ToolStripButton();
            this.tlsBtnCaricaOnline = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbVeicoli
            // 
            this.lbVeicoli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVeicoli.FormattingEnabled = true;
            this.lbVeicoli.Location = new System.Drawing.Point(0, 25);
            this.lbVeicoli.Margin = new System.Windows.Forms.Padding(2);
            this.lbVeicoli.Name = "lbVeicoli";
            this.lbVeicoli.Size = new System.Drawing.Size(545, 318);
            this.lbVeicoli.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnAddVeicolo,
            this.salvaToolStripButton,
            this.ToolStripButtonExcel,
            this.toolStripButtonWord,
            this.tlsBtnCaricaOnline});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(545, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnAddVeicolo
            // 
            this.toolStripBtnAddVeicolo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnAddVeicolo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnAddVeicolo.Image")));
            this.toolStripBtnAddVeicolo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAddVeicolo.Name = "toolStripBtnAddVeicolo";
            this.toolStripBtnAddVeicolo.Size = new System.Drawing.Size(47, 22);
            this.toolStripBtnAddVeicolo.Text = "&Nuovo";
            this.toolStripBtnAddVeicolo.Click += new System.EventHandler(this.toolStripBtnAddVeicolo_Click_1);
            // 
            // salvaToolStripButton
            // 
            this.salvaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.salvaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("salvaToolStripButton.Image")));
            this.salvaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.salvaToolStripButton.Name = "salvaToolStripButton";
            this.salvaToolStripButton.Size = new System.Drawing.Size(38, 22);
            this.salvaToolStripButton.Text = "&Salva";
            this.salvaToolStripButton.Click += new System.EventHandler(this.salvaToolStripButton_Click);
            // 
            // ToolStripButtonExcel
            // 
            this.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripButtonExcel.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonExcel.Image")));
            this.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonExcel.Name = "ToolStripButtonExcel";
            this.ToolStripButtonExcel.Size = new System.Drawing.Size(38, 22);
            this.ToolStripButtonExcel.Text = "&Excel";
            this.ToolStripButtonExcel.Click += new System.EventHandler(this.ToolStripButtonExcel_Click);
            // 
            // toolStripButtonWord
            // 
            this.toolStripButtonWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButtonWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonWord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWord.Image")));
            this.toolStripButtonWord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWord.Name = "toolStripButtonWord";
            this.toolStripButtonWord.Size = new System.Drawing.Size(40, 22);
            this.toolStripButtonWord.Text = "&Word";
            this.toolStripButtonWord.Click += new System.EventHandler(this.toolStripButtonWord_Click);
            // 
            // tlsBtnCaricaOnline
            // 
            this.tlsBtnCaricaOnline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlsBtnCaricaOnline.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnCaricaOnline.Image")));
            this.tlsBtnCaricaOnline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnCaricaOnline.Name = "tlsBtnCaricaOnline";
            this.tlsBtnCaricaOnline.Size = new System.Drawing.Size(69, 22);
            this.tlsBtnCaricaOnline.Text = "Sito Online";
            this.tlsBtnCaricaOnline.Click += new System.EventHandler(this.tlsBtnCaricaOnline_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 343);
            this.Controls.Add(this.lbVeicoli);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "Salone Vendita";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbVeicoli;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnAddVeicolo;
        private System.Windows.Forms.ToolStripButton ToolStripButtonExcel;
        private System.Windows.Forms.ToolStripButton salvaToolStripButton;
        private System.Windows.Forms.ToolStripButton tlsBtnCaricaOnline;
        private System.Windows.Forms.ToolStripButton toolStripButtonWord;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

