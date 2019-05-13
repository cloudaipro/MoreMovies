using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the following information for titles:
    public class Title_Akas : INotifyPropertyChanged
    {
        public virtual string titleId { get; set; }     // (string) - a tconst, an alphanumeric unique identifier of the title
        public virtual int? ordering { get; set; }       // (integer) – a number to uniquely identify rows for a given titleId
        public virtual string title { get; set; }       //(string) – the localized title
        public virtual string region { get; set; }      //(string) - the region for this version of the title
        public virtual string language { get; set; }    //(string) - the language of the title
        public virtual string types { get; set; }       //(array) - Enumerated set of attributes for this alternative title.One or more of the following: "alternative", "dvd", "festival", "tv", "video", "working", "original", "imdbDisplay". New values may be added in the future without warning
        public virtual string attributes { get; set; }  //(array) - Additional terms to describe this alternative title, not enumerated
        public virtual bool isOriginalTitle { get; set; }//(boolean) – 0: not original title; 1: original title

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return ((obj as Title_Akas).titleId == titleId &&
                (obj as Title_Akas).ordering == ordering);
        }
        public override int GetHashCode()
        {
            return (titleId + "|" + ordering).GetHashCode();
        }
    }
}
