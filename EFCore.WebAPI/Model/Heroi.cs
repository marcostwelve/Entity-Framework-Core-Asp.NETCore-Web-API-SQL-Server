using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Model
{
    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Arma> Armas { get; set; }
        public List<HeroiBatalha> HeroisBatalhas { get; set; }
    }
}
