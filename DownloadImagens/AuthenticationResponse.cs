using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadImagens
{
    public class AuthenticationResponse
    {
        public string chaveDeSeguranca { get; set; }
        public string token { get; set; }
        public List<string> errors { get; set; }
        public string message { get; set; }
    }
}
