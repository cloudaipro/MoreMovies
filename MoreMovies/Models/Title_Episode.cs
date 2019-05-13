using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the tv episode information. Fields include:
    public class Title_Episode : INotifyPropertyChanged
    {
        public virtual string tconst { get; set; }          //(string) - alphanumeric identifier of episode
        public virtual string parentTconst { get; set; }    //(string) - alphanumeric identifier of the parent TV Series
        public virtual int? seasonNumber { get; set; }       //(integer) – season number the episode belongs to
        public virtual int? episodeNumber { get; set; }      //(integer) – episode number of the tconst in the TV series

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
