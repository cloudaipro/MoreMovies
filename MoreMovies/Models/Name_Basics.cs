using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the following information for names:
    public class Name_Basics : INotifyPropertyChanged
    {
        public virtual string nconst { get; set; }              // (string) - alphanumeric unique identifier of the name/person
        public virtual string primaryName { get; set; }         // (string)– name by which the person is most often credited
        public virtual int? birthYear { get; set; }              //  – in YYYY format
        public virtual int? deathYear { get; set; }              //  – in YYYY format if applicable, else '\N'
        public virtual string primaryProfession { get; set; }   // (array of strings)– the top-3 professions of the person
        public virtual string knownForTitles { get; set; }      // (array of tconsts) – titles the person is known for

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
