using System;
using System.Collections.Generic;
using System.Text;

namespace Hazak
{
    class Tomb : Haz

    {
        int lakasok;
        string tipus;
        bool lift;

        public Tomb(string cim, int id, int alapterulet, string epitesianyag, DateTime mkezdete, DateTime mvege,int lakasok, string tipus, bool lift): base(cim,id, alapterulet, epitesianyag, mkezdete, mvege)
        {
            this.Lakasok = lakasok;
            this.Tipus = tipus;
            this.Lift = lift;
        }

        public int Lakasok { get => lakasok; set => lakasok = value; }
        public string Tipus { get => tipus; set => tipus = value; }
        public bool Lift { get => lift; set => lift = value; }
    }
}
