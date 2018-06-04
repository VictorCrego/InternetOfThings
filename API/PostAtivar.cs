using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class PostAtivar
    {
        public string Cliente { get; set; }
        public string Ambiente { get; set; }
        public string Dispositivo { get; set; }
        public int Versao { get; set; }
    }
}
