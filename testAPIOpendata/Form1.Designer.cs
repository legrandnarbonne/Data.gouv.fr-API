namespace testAPIOpendata
{
    partial class MainClientTestForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainClientTestForm));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.obtenirLesDatasetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obtenirLesRessourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remplacerLeFichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effacerLaRessourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterUneRessourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterUnDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLeDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkKeepPreviousRequest = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.mainSplitH = new System.Windows.Forms.SplitContainer();
            this.hautVertical = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.txtPage = new System.Windows.Forms.ToolStripTextBox();
            this.txtMaxPage = new System.Windows.Forms.ToolStripLabel();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.botomH = new System.Windows.Forms.SplitContainer();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contextTreeview.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitH)).BeginInit();
            this.mainSplitH.Panel1.SuspendLayout();
            this.mainSplitH.Panel2.SuspendLayout();
            this.mainSplitH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hautVertical)).BeginInit();
            this.hautVertical.Panel1.SuspendLayout();
            this.hautVertical.Panel2.SuspendLayout();
            this.hautVertical.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.botomH)).BeginInit();
            this.botomH.Panel1.SuspendLayout();
            this.botomH.Panel2.SuspendLayout();
            this.botomH.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(134, 221);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Enrg. les modifications";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.update_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextTreeview;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(556, 235);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // contextTreeview
            // 
            this.contextTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.obtenirLesDatasetsToolStripMenuItem,
            this.obtenirLesRessourcesToolStripMenuItem,
            this.remplacerLeFichierToolStripMenuItem,
            this.effacerLaRessourceToolStripMenuItem,
            this.ajouterUneRessourceToolStripMenuItem,
            this.ajouterUnDatasetToolStripMenuItem,
            this.supprimerLeDatasetToolStripMenuItem});
            this.contextTreeview.Name = "contextTreeview";
            this.contextTreeview.Size = new System.Drawing.Size(204, 158);
            this.contextTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.contextTreeview_Opening);
            // 
            // obtenirLesDatasetsToolStripMenuItem
            // 
            this.obtenirLesDatasetsToolStripMenuItem.Name = "obtenirLesDatasetsToolStripMenuItem";
            this.obtenirLesDatasetsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.obtenirLesDatasetsToolStripMenuItem.Text = "Obtenir les datasets";
            this.obtenirLesDatasetsToolStripMenuItem.Click += new System.EventHandler(this.obtenirLesDatasetsToolStripMenuItem_Click);
            // 
            // obtenirLesRessourcesToolStripMenuItem
            // 
            this.obtenirLesRessourcesToolStripMenuItem.Name = "obtenirLesRessourcesToolStripMenuItem";
            this.obtenirLesRessourcesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.obtenirLesRessourcesToolStripMenuItem.Text = "Obtenir le fichier associé";
            this.obtenirLesRessourcesToolStripMenuItem.Click += new System.EventHandler(this.obtenirLesRessourcesToolStripMenuItem_Click);
            // 
            // remplacerLeFichierToolStripMenuItem
            // 
            this.remplacerLeFichierToolStripMenuItem.Name = "remplacerLeFichierToolStripMenuItem";
            this.remplacerLeFichierToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.remplacerLeFichierToolStripMenuItem.Text = "Remplacer le fichier";
            this.remplacerLeFichierToolStripMenuItem.Click += new System.EventHandler(this.remplacerLeFichierToolStripMenuItem_Click);
            // 
            // effacerLaRessourceToolStripMenuItem
            // 
            this.effacerLaRessourceToolStripMenuItem.Name = "effacerLaRessourceToolStripMenuItem";
            this.effacerLaRessourceToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.effacerLaRessourceToolStripMenuItem.Text = "Effacer la ressource";
            this.effacerLaRessourceToolStripMenuItem.Click += new System.EventHandler(this.effacerLaRessourceToolStripMenuItem_Click);
            // 
            // ajouterUneRessourceToolStripMenuItem
            // 
            this.ajouterUneRessourceToolStripMenuItem.Name = "ajouterUneRessourceToolStripMenuItem";
            this.ajouterUneRessourceToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.ajouterUneRessourceToolStripMenuItem.Text = "Ajouter une ressource";
            this.ajouterUneRessourceToolStripMenuItem.Click += new System.EventHandler(this.ajouterUneRessourceToolStripMenuItem_Click);
            // 
            // ajouterUnDatasetToolStripMenuItem
            // 
            this.ajouterUnDatasetToolStripMenuItem.Name = "ajouterUnDatasetToolStripMenuItem";
            this.ajouterUnDatasetToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.ajouterUnDatasetToolStripMenuItem.Text = "Ajouter un Dataset";
            this.ajouterUnDatasetToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnDatasetToolStripMenuItem_Click);
            // 
            // supprimerLeDatasetToolStripMenuItem
            // 
            this.supprimerLeDatasetToolStripMenuItem.Name = "supprimerLeDatasetToolStripMenuItem";
            this.supprimerLeDatasetToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.supprimerLeDatasetToolStripMenuItem.Text = "Supprimer le Dataset";
            this.supprimerLeDatasetToolStripMenuItem.Click += new System.EventHandler(this.supprimerLeDatasetToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkKeepPreviousRequest);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 260);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rechercher";
            // 
            // chkKeepPreviousRequest
            // 
            this.chkKeepPreviousRequest.AutoSize = true;
            this.chkKeepPreviousRequest.Location = new System.Drawing.Point(12, 196);
            this.chkKeepPreviousRequest.Name = "chkKeepPreviousRequest";
            this.chkKeepPreviousRequest.Size = new System.Drawing.Size(181, 17);
            this.chkKeepPreviousRequest.TabIndex = 15;
            this.chkKeepPreviousRequest.Text = "Conserver la requete précédente";
            this.chkKeepPreviousRequest.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(199, 231);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "Rechercher";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.search_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(52, 95);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(125, 20);
            this.txtId.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "ID :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(52, 69);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(125, 20);
            this.txtName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nom  :";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(556, 256);
            this.propertyGrid1.TabIndex = 10;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.SelectedObjectsChanged += new System.EventHandler(this.propertyGrid1_SelectedObjectsChanged);
            // 
            // mainSplitH
            // 
            this.mainSplitH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitH.Location = new System.Drawing.Point(0, 0);
            this.mainSplitH.Name = "mainSplitH";
            this.mainSplitH.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitH.Panel1
            // 
            this.mainSplitH.Panel1.Controls.Add(this.hautVertical);
            // 
            // mainSplitH.Panel2
            // 
            this.mainSplitH.Panel2.Controls.Add(this.botomH);
            this.mainSplitH.Size = new System.Drawing.Size(840, 520);
            this.mainSplitH.SplitterDistance = 260;
            this.mainSplitH.TabIndex = 11;
            // 
            // hautVertical
            // 
            this.hautVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hautVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.hautVertical.Location = new System.Drawing.Point(0, 0);
            this.hautVertical.Name = "hautVertical";
            // 
            // hautVertical.Panel1
            // 
            this.hautVertical.Panel1.Controls.Add(this.groupBox2);
            // 
            // hautVertical.Panel2
            // 
            this.hautVertical.Panel2.Controls.Add(this.treeView1);
            this.hautVertical.Panel2.Controls.Add(this.toolStrip1);
            this.hautVertical.Size = new System.Drawing.Size(840, 260);
            this.hautVertical.SplitterDistance = 280;
            this.hautVertical.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevious,
            this.txtPage,
            this.txtMaxPage,
            this.btnNext,
            this.toolStripLabel1,
            this.txtPageSize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(556, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(23, 22);
            this.btnPrevious.Text = "Page précédente";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // txtPage
            // 
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(30, 25);
            this.txtPage.Text = "1";
            this.txtPage.TextChanged += new System.EventHandler(this.txtPage_TextChanged);
            // 
            // txtMaxPage
            // 
            this.txtMaxPage.Name = "txtMaxPage";
            this.txtMaxPage.Size = new System.Drawing.Size(18, 22);
            this.txtMaxPage.Text = "/..";
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Enabled = false;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Page suivante";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "Limiter à";
            // 
            // txtPageSize
            // 
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(30, 25);
            this.txtPageSize.Text = "10";
            this.txtPageSize.TextChanged += new System.EventHandler(this.txtPageSize_TextChanged);
            // 
            // botomH
            // 
            this.botomH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.botomH.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.botomH.Location = new System.Drawing.Point(0, 0);
            this.botomH.Name = "botomH";
            // 
            // botomH.Panel1
            // 
            this.botomH.Panel1.Controls.Add(this.txtKey);
            this.botomH.Panel1.Controls.Add(this.label1);
            this.botomH.Panel1.Controls.Add(this.txtURL);
            this.botomH.Panel1.Controls.Add(this.label2);
            this.botomH.Panel1.Controls.Add(this.btnUpdate);
            // 
            // botomH.Panel2
            // 
            this.botomH.Panel2.Controls.Add(this.propertyGrid1);
            this.botomH.Size = new System.Drawing.Size(840, 256);
            this.botomH.SplitterDistance = 280;
            this.botomH.TabIndex = 11;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(68, 45);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(184, 69);
            this.txtKey.TabIndex = 13;
            this.txtKey.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Clef API :";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(68, 19);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(184, 20);
            this.txtURL.TabIndex = 11;
            this.txtURL.Text = "https://www.data.gouv.fr/api/1/";
            this.txtURL.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "URL API  :";
            // 
            // MainClientTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 520);
            this.Controls.Add(this.mainSplitH);
            this.Name = "MainClientTestForm";
            this.Text = "API Data.gouv.fr client test";
            this.contextTreeview.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mainSplitH.Panel1.ResumeLayout(false);
            this.mainSplitH.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitH)).EndInit();
            this.mainSplitH.ResumeLayout(false);
            this.hautVertical.Panel1.ResumeLayout(false);
            this.hautVertical.Panel2.ResumeLayout(false);
            this.hautVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hautVertical)).EndInit();
            this.hautVertical.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.botomH.Panel1.ResumeLayout(false);
            this.botomH.Panel1.PerformLayout();
            this.botomH.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.botomH)).EndInit();
            this.botomH.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer mainSplitH;
        private System.Windows.Forms.SplitContainer hautVertical;
        private System.Windows.Forms.SplitContainer botomH;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtPageSize;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.CheckBox chkKeepPreviousRequest;
        private System.Windows.Forms.ContextMenuStrip contextTreeview;
        private System.Windows.Forms.ToolStripMenuItem obtenirLesDatasetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obtenirLesRessourcesToolStripMenuItem;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem remplacerLeFichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effacerLaRessourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneRessourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnDatasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerLeDatasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtPage;
        private System.Windows.Forms.ToolStripLabel txtMaxPage;
    }
}

