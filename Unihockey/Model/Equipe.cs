using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unihockey.Model
{
    internal class Equipe
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Categorie Categorie { get; set; }
    }
}
