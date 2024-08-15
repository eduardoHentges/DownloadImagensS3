using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DownloadImagens
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client;

        public Form1()
        {
            InitializeComponent();
            buttonGerarToken.Click += buttonGerarToken_Click;
            _client = new HttpClient(); // Inicialização do HttpClient
        }

        private async void buttonGerarToken_Click(object sender, EventArgs e)
        {
            string chaveSeguranca = textBoxChaveSeguranca.Text.Trim();
            string token = await GetTokenAsync(chaveSeguranca);

            if (!string.IsNullOrEmpty(token))
            {
                var (sucesso, produtos) = await GetProductsAsync(token);

                if (sucesso)
                {
                    // Limpa colunas existentes
                    dataGridViewProdutos.Columns.Clear();

                    // Define as colunas na ordem desejada
                    dataGridViewProdutos.Columns.Add("id", "ID");
                    dataGridViewProdutos.Columns.Add("codigo", "Código");
                    dataGridViewProdutos.Columns.Add("nome", "Nome");
                    dataGridViewProdutos.Columns.Add("imagem", "Imagem Principal");

                    // Cria colunas adicionais dinamicamente
                    var numeroDeColunasAdicionais = produtos.Max(p => p.imagensAdicionais?.Count ?? 0);
                    for (int i = 0; i < numeroDeColunasAdicionais; i++)
                    {
                        dataGridViewProdutos.Columns.Add($"imagemAdicional{i + 1}", $"Imagem Adicional {i + 1}");
                    }

                    // Preenche o DataGridView com dados
                    foreach (var produto in produtos)
                    {
                        var row = new DataGridViewRow();
                        row.CreateCells(dataGridViewProdutos);

                        row.Cells[0].Value = produto.id; // ID
                        row.Cells[1].Value = produto.codigo; // Código do produto
                        row.Cells[2].Value = produto.nome; // Nome do produto
                        row.Cells[3].Value = produto.imagem; // Imagem Principal

                        // Preenchendo as imagens adicionais
                        for (int i = 0; i < numeroDeColunasAdicionais; i++)
                        {
                            if (i < produto.imagensAdicionais.Count)
                            {
                                // Acesse a URL da imagem adicional
                                var imagemAdicionalUrl = produto.imagensAdicionais[i].imagem;
                                row.Cells[i + 4].Value = imagemAdicionalUrl; // Adiciona a URL na coluna correspondente
                            }
                            else
                            {
                                row.Cells[i + 4].Value = null; // Se não houver, define como nulo
                            }
                        }

                        dataGridViewProdutos.Rows.Add(row); // Adiciona a linha ao DataGridView
                    }

                    // Define propriedades de visualização se necessário
                    dataGridViewProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("Falha ao obter produtos.");
                }
            }
        }

        private async void btnDownloadAllImages_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    int totalImages = 0; // Contador para total de imagens
                    int downloadedImages = 0; // Contador para imagens baixadas

                    // Percorre as linhas do DataGridView
                    foreach (DataGridViewRow row in dataGridViewProdutos.Rows)
                    {
                        // Baixando a imagem principal
                        var mainImageUrl = row.Cells[3].Value?.ToString(); // A URL da imagem principal
                        var productCode = row.Cells[1].Value?.ToString(); // O código do produto (presumindo estar na segunda coluna)

                        if (!string.IsNullOrEmpty(mainImageUrl) && !string.IsNullOrEmpty(productCode))
                        {
                            totalImages++; // Incrementa total de imagens

                            string fileName = GetGuidFromUrl(mainImageUrl); // Usa o GUID como nome
                            string filePath = Path.Combine(folderBrowserDialog.SelectedPath, $"{fileName}.jpeg"); // Define o caminho completo com o nome

                            await DownloadImageAsync(mainImageUrl, filePath); // Tenta baixar a imagem
                            downloadedImages++; // Incrementa imagens baixadas
                        }

                        // Baixando as imagens adicionais
                        for (int i = 4; i < row.Cells.Count; i++) // Começa a partir da quarta coluna
                        {
                            var imageUrl = row.Cells[i].Value?.ToString(); // Acesse a URL da imagem adicional

                            if (!string.IsNullOrEmpty(imageUrl))
                            {
                                totalImages++; // Incrementa total de imagens

                                string additionalFileName = GetGuidFromUrl(imageUrl); // Usa o GUID como nome
                                string additionalFilePath = Path.Combine(folderBrowserDialog.SelectedPath, $"{additionalFileName}.jpeg"); // Define o caminho completo com o nome

                                await DownloadImageAsync(imageUrl, additionalFilePath); // Tenta baixar a imagem adicional
                                downloadedImages++; // Incrementa imagens baixadas
                            }
                        }
                    }

                    // Exibir mensagem de conclusão
                    MessageBox.Show($"Download concluído!\nTotal de Imagens: {totalImages}\nImagens Baixadas: {downloadedImages}");
                }
            }
        }

        private async void btnDownloadMainImage_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Percorre as linhas do DataGridView
                    foreach (DataGridViewRow row in dataGridViewProdutos.Rows)
                    {
                        // A segunda coluna é a imagem principal
                        var mainImageUrl = row.Cells[3].Value?.ToString(); // URL da imagem principal
                        var productCode = row.Cells[1].Value?.ToString(); // O código do produto

                        if (!string.IsNullOrEmpty(mainImageUrl) && !string.IsNullOrEmpty(productCode))
                        {
                            string fileName = $"{productCode}.jpeg"; // O nome da imagem será baseado no código do produto
                            string filePath = Path.Combine(folderBrowserDialog.SelectedPath, fileName); // Define o caminho completo com o nome

                            await DownloadImageAsync(mainImageUrl, filePath); // Tenta baixar a imagem principal
                                                        
                        }
                    }
                }
                MessageBox.Show($"Imagem principal baixada com sucesso!");
            }
        }

        private string GetGuidFromUrl(string url)
        {
            var segments = url.Split('/');
            // Obtenha a última parte da URL, sem usar o operador de índice
            string lastSegment = segments[segments.Length - 2]; // Último segmento
            return lastSegment.Split('.')[0]; // Retorna o GUID, que é a parte antes da extensão
        }


        private async Task DownloadImageAsync(string url, string savePath)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var imageBytes = await client.GetByteArrayAsync(url);
                    System.IO.File.WriteAllBytes(savePath, imageBytes); // Salva a imagem
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao baixar a imagem: {ex.Message}");
                }
            }
        }

        private async Task<string> GetTokenAsync(string chaveSeguranca)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://ms-ecommerce.hiper.com.br/api/v1/auth/gerar-token/{chaveSeguranca}");
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonResponse);
                return result?.token; // Retorna o token se obtido
            }
            else
            {
                await ShowErrorMessage(response, "Erro ao obter o token.");
                return null; // Retorna null caso ocorrer erro
            }
        }

        private async Task<(bool sucesso, List<Product> produtos)> GetProductsAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, "https://ms-ecommerce.hiper.com.br/api/v1/produtos/pontoDeSincronizacao?pontoDeSincronizacao=0");

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductResponse>(jsonResponse);
                return (true, result.produtos); // Retorna sucesso e a lista de produtos
            }
            else
            {
                await ShowErrorMessage(response, "Erro ao obter produtos.");
                return (false, null); // Retorna falso e null em caso de erro
            }
        }

        private async Task ShowErrorMessage(HttpResponseMessage response, string customMessage)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"{customMessage}\nStatus Code: {response.StatusCode}\nDetalhes: {errorResponse}");
        }
    }
   
}
