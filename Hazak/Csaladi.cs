using System;
using System.Collections.Generic;
using System.Text;

namespace Hazak
{
   

    class Csaladi : Haz
    {
        int ottelok;
        bool garazs;
        string teto;

        public Csaladi(string cim, int id, int alapterulet, string epitesianyag, DateTime mkezdete, DateTime mvege,int ottelok, bool garazs, string teto):base(cim,id, alapterulet, epitesianyag, mkezdete, mvege)
        {
            this.Ottelok = ottelok;
            this.Garazs = garazs;
            this.Teto = teto;
        }

        public int Ottelok { get => ottelok; set => ottelok = value; }
        public bool Garazs { get => garazs; set => garazs = value; }
        public string Teto { get => teto; set => teto = value; }
    }
}
