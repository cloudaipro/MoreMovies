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
    public class Title_Basics : INotifyPropertyChanged
    {
        public virtual string tconst { get; set; }          //(string) - alphanumeric unique identifier of the title
        public virtual string titleType { get; set; }       //(string) – the type/format of the title(e.g.movie, short, tvseries, tvepisode, video, etc)
        public virtual string primaryTitle { get; set; }    //(string) – the more popular title / the title used by the filmmakers on promotional materials at the point of release
        public virtual string originalTitle { get; set; }           //(string) - original title, in the original language
        public virtual bool isAdult { get; set; }           //(boolean) - 0: non-adult title; 1: adult title
        public virtual int? startYear { get; set; }          //(YYYY) – represents the release year of a title.In the case of TV Series, it is the series start year
        public virtual int? endYear { get; set; }            //(YYYY) – TV Series end year. ‘\N’ for all other title types
        public virtual int? runtimeMinutes { get; set; }    // – primary runtime of the title, in minutes
        public virtual string genres { get; set; }          //(string array) – includes up to three genres associated with the title

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
