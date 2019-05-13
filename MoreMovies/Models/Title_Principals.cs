using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the principal cast/crew for titles
    public class Title_Principals : INotifyPropertyChanged
    {
        public virtual string tconst { get; set; }         //(string) - alphanumeric unique identifier of the title
        public virtual int? ordering { get; set; }          //(integer) – a number to uniquely identify rows for a given titleId
        public virtual string nconst { get; set; }         //(string) - alphanumeric unique identifier of the name/person
        public virtual string category { get; set; }       //(string) - the category of job that person was in
        public virtual string job { get; set; }            //(string) - the specific job title if applicable, else '\N'
        public virtual string characters { get; set; }     //(string) - the name of the character played if applicable, else '\N'    }

        public virtual event PropertyChangedEventHandler PropertyChanged;
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return ((obj as Title_Principals).tconst == tconst &&
                (obj as Title_Principals).ordering == ordering);
        }
        public override int GetHashCode()
        {
            return (tconst + "|" + ordering).GetHashCode();
        }
    }
}
