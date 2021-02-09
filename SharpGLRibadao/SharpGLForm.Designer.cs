namespace SharpGL_CG_TDM
{
    partial class SharpGLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpGLForm));
            this.openGLControl = new SharpGL.OpenGLControl();
            this.txtPontuacao = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtBestScore = new System.Windows.Forms.TextBox();
            this.txtPontFinal = new System.Windows.Forms.TextBox();
            this.TeclasText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.AutoSize = true;
            this.openGLControl.BackColor = System.Drawing.Color.Transparent;
            this.openGLControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("openGLControl.BackgroundImage")));
            this.openGLControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, -1);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(752, 471);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tecladown);
            // 
            // txtPontuacao
            // 
            this.txtPontuacao.BackColor = System.Drawing.Color.Yellow;
            this.txtPontuacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPontuacao.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPontuacao.Location = new System.Drawing.Point(12, 12);
            this.txtPontuacao.Name = "txtPontuacao";
            this.txtPontuacao.Size = new System.Drawing.Size(185, 22);
            this.txtPontuacao.TabIndex = 2;
            // 
            // txtStart
            // 
            this.txtStart.BackColor = System.Drawing.Color.Yellow;
            this.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStart.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStart.Location = new System.Drawing.Point(123, 211);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(500, 29);
            this.txtStart.TabIndex = 3;
            this.txtStart.Text = "Pressione a tecla ESPAÇO para começar";
            this.txtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBestScore
            // 
            this.txtBestScore.BackColor = System.Drawing.Color.Yellow;
            this.txtBestScore.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBestScore.Location = new System.Drawing.Point(523, 12);
            this.txtBestScore.Margin = new System.Windows.Forms.Padding(8);
            this.txtBestScore.Name = "txtBestScore";
            this.txtBestScore.Size = new System.Drawing.Size(213, 29);
            this.txtBestScore.TabIndex = 2;
            this.txtBestScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPontFinal
            // 
            this.txtPontFinal.BackColor = System.Drawing.Color.Yellow;
            this.txtPontFinal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPontFinal.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPontFinal.Location = new System.Drawing.Point(123, 161);
            this.txtPontFinal.Name = "txtPontFinal";
            this.txtPontFinal.Size = new System.Drawing.Size(500, 29);
            this.txtPontFinal.TabIndex = 4;
            this.txtPontFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TeclasText
            // 
            this.TeclasText.BackColor = System.Drawing.Color.Yellow;
            this.TeclasText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TeclasText.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeclasText.Location = new System.Drawing.Point(123, 262);
            this.TeclasText.Name = "TeclasText";
            this.TeclasText.Size = new System.Drawing.Size(500, 29);
            this.TeclasText.TabIndex = 5;
            this.TeclasText.Text = "Use as Teclas I e O para Movimentar o Objecto";
            this.TeclasText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 470);
            this.Controls.Add(this.TeclasText);
            this.Controls.Add(this.txtPontFinal);
            this.Controls.Add(this.txtBestScore);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.txtPontuacao);
            this.Controls.Add(this.openGLControl);
            this.Name = "SharpGLForm";
            this.Text = "SharpGL Form";
            this.Load += new System.EventHandler(this.SharpGLForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tecladown);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.TextBox txtPontuacao;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtBestScore;
        private System.Windows.Forms.TextBox txtPontFinal;
        private System.Windows.Forms.TextBox TeclasText;
    }
}

