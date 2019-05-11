using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreMovies.Models
{
    public class Title_Akas
    {
        public virtual string titleId { get; set; }     // (string) - a tconst, an alphanumeric unique identifier of the title
        public virtual int ordering { get; set; }       // (integer) – a number to uniquely identify rows for a given titleId
        public virtual string title { get; set; }       //(string) – the localized title
        public virtual string region { get; set; }      //(string) - the region for this version of the title
        public virtual string language { get; set; }    //(string) - the language of the title
        public virtual string types { get; set; }       //(array) - Enumerated set of attributes for this alternative title.One or more of the following: "alternative", "dvd", "festival", "tv", "video", "working", "original", "imdbDisplay". New values may be added in the future without warning
        public virtual string attributes { get; set; }  //(array) - Additional terms to describe this alternative title, not enumerated
        public virtual bool isOriginalTitle { get; set; }//(boolean) – 0: not original title; 1: original title
    }
}
