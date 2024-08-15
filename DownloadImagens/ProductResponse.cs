using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadImagens
{
    // Crie uma nova classe para mapear a resposta inteira
    public class ProductResponse
    {
        public int pontoDeSincronizacao { get; set; }
        public List<Product> produtos { get; set; }
        public List<string> errors { get; set; }


    }
}
