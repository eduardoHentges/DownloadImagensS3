namespace DownloadImagens
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGerarToken = new System.Windows.Forms.Button();
            this.textBoxChaveSeguranca = new System.Windows.Forms.TextBox();
            this.dataGridViewProdutos = new System.Windows.Forms.DataGridView();
            this.btnDownloadAllImages = new System.Windows.Forms.Button();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.btnDownloadMainImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProdutos)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGerarToken
            // 
            this.buttonGerarToken.Location = new System.Drawing.Point(13, 53);
            this.buttonGerarToken.Name = "buttonGerarToken";
            this.buttonGerarToken.Size = new System.Drawing.Size(112, 23);
            this.buttonGerarToken.TabIndex = 0;
            this.buttonGerarToken.Text = "Buscar Produtos";
            this.buttonGerarToken.UseVisualStyleBackColor = true;
            this.buttonGerarToken.Click += new System.EventHandler(this.buttonGerarToken_Click);
            // 
            // textBoxChaveSeguranca
            // 
            this.textBoxChaveSeguranca.Location = new System.Drawing.Point(13, 27);
            this.textBoxChaveSeguranca.Name = "textBoxChaveSeguranca";
            this.textBoxChaveSeguranca.Size = new System.Drawing.Size(400, 20);
            this.textBoxChaveSeguranca.TabIndex = 1;
            this.textBoxChaveSeguranca.Text = "Insira a chave de segurança";
            // 
            // dataGridViewProdutos
            // 
            this.dataGridViewProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProdutos.Location = new System.Drawing.Point(13, 80);
            this.dataGridViewProdutos.Name = "dataGridViewProdutos";
            this.dataGridViewProdutos.Size = new System.Drawing.Size(517, 300);
            this.dataGridViewProdutos.TabIndex = 2;
            // 
            // btnDownloadAllImages
            // 
            this.btnDownloadAllImages.Location = new System.Drawing.Point(12, 386);
            this.btnDownloadAllImages.Name = "btnDownloadAllImages";
            this.btnDownloadAllImages.Size = new System.Drawing.Size(161, 23);
            this.btnDownloadAllImages.TabIndex = 3;
            this.btnDownloadAllImages.Text = "Baixar Todas as Imagens";
            this.btnDownloadAllImages.UseVisualStyleBackColor = true;
            this.btnDownloadAllImages.Click += new System.EventHandler(this.btnDownloadAllImages_Click);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(13, 425);
            this.progressBarDownload.Maximum = 10000;
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(517, 23);
            this.progressBarDownload.TabIndex = 4;
            // 
            // btnDownloadMainImage
            // 
            this.btnDownloadMainImage.Location = new System.Drawing.Point(193, 386);
            this.btnDownloadMainImage.Name = "btnDownloadMainImage";
            this.btnDownloadMainImage.Size = new System.Drawing.Size(204, 23);
            this.btnDownloadMainImage.TabIndex = 5;
            this.btnDownloadMainImage.Text = "Baixar Imagem Principal";
            this.btnDownloadMainImage.UseVisualStyleBackColor = true;
            this.btnDownloadMainImage.Click += new System.EventHandler(this.btnDownloadMainImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDownloadMainImage);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.btnDownloadAllImages);
            this.Controls.Add(this.dataGridViewProdutos);
            this.Controls.Add(this.textBoxChaveSeguranca);
            this.Controls.Add(this.buttonGerarToken);
            this.Name = "Form1";
            this.Text = "Visualizador de Produtos";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProdutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGerarToken;
        private System.Windows.Forms.TextBox textBoxChaveSeguranca;
        private System.Windows.Forms.DataGridView dataGridViewProdutos;
        private System.Windows.Forms.Button btnDownloadAllImages;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Button btnDownloadMainImage;
    }
}
