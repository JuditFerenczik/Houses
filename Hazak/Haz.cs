using System;
using System.Collections.Generic;
using System.Text;

namespace Hazak
{
    class Haz
    {
        string cim;
        int id;
        int alapterulet;
        string epitesianyag;
        DateTime mkezdete;
        DateTime mvege;

        public Haz(string cim, int id, int alapterulet, string epitesianyag, DateTime mkezdete, DateTime mvege)
        {
            this.cim = cim;
            this.id = id;
            this.alapterulet = alapterulet;
            this.epitesianyag = epitesianyag;
            this.mkezdete = mkezdete;
            this.mvege = mvege;
        }

        public string Cim { get => cim; set => cim = value; }
        public int Alapterulet { get => alapterulet; set => alapterulet = value; }
        public string Epitesianyag { get => epitesianyag; set => epitesianyag = value; }
        public DateTime Mkezdete { get => mkezdete; set => mkezdete = value; }
        public DateTime Mvege { get => mvege; set => mvege = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return cim;
        }
    }
    
}
