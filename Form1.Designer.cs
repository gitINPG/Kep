namespace KEP
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textBoxTrig = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectServerCtrl1 = new Opc.Ua.Client.Controls.ConnectServerCtrl();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 279);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(523, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textBoxTrig
            // 
            this.textBoxTrig.Location = new System.Drawing.Point(389, 70);
            this.textBoxTrig.Name = "textBoxTrig";
            this.textBoxTrig.Size = new System.Drawing.Size(122, 20);
            this.textBoxTrig.TabIndex = 7;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(261, 70);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(122, 20);
            this.textBoxName.TabIndex = 10;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(133, 70);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(122, 20);
            this.textBoxValue.TabIndex = 11;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(5, 70);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(122, 20);
            this.textBoxID.TabIndex = 12;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // connectServerCtrl1
            // 
            this.connectServerCtrl1.Configuration = null;
            this.connectServerCtrl1.DisableDomainCheck = false;
            this.connectServerCtrl1.Location = new System.Drawing.Point(11, 12);
            this.connectServerCtrl1.MaximumSize = new System.Drawing.Size(2048, 23);
            this.connectServerCtrl1.MinimumSize = new System.Drawing.Size(500, 23);
            this.connectServerCtrl1.Name = "connectServerCtrl1";
            this.connectServerCtrl1.PreferredLocales = null;
            this.connectServerCtrl1.ServerUrl = "";
            this.connectServerCtrl1.SessionName = "Kep";
            this.connectServerCtrl1.Size = new System.Drawing.Size(500, 23);
            this.connectServerCtrl1.StatusStrip = this.statusStrip1;
            this.connectServerCtrl1.TabIndex = 13;
            this.connectServerCtrl1.UserIdentity = null;
            this.connectServerCtrl1.UseSecurity = true;
            this.connectServerCtrl1.ConnectComplete += new System.EventHandler(this.connectServerCtrl1_ConnectComplete);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 301);
            this.Controls.Add(this.connectServerCtrl1);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxTrig);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBoxTrig;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Opc.Ua.Client.Controls.ConnectServerCtrl connectServerCtrl1;
    }
}

