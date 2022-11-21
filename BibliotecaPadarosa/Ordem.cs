using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaPadarosa
{
    public class Ordem
    {
        public int Id { get; set; } 
        public int IDFicha { get; set; }
        public int IDProduto { get; set; }
        public int Quantidade { get; set; }
        public int IDResponsavel { get; set; }
        public bool Situacao { get; set; }
    }
}
