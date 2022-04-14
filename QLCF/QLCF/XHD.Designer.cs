namespace QLCF
{
    partial class XHD
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
            this.lbinhoadon = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbinhoadon
            // 
            this.lbinhoadon.AutoSize = true;
            this.lbinhoadon.Location = new System.Drawing.Point(34, 49);
            this.lbinhoadon.Name = "lbinhoadon";
            this.lbinhoadon.Size = new System.Drawing.Size(0, 13);
            this.lbinhoadon.TabIndex = 2;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(176, 297);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(81, 59);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "Xác nhận";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // XHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 436);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.lbinhoadon);
            this.Name = "XHD";
            this.Text = "XHD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbinhoadon;
        private System.Windows.Forms.Button btOk;
    }
}