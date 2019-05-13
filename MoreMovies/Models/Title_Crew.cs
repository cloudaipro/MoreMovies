using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the director and writer information for all the titles in IMDb.
    public class Title_Crew : INotifyPropertyChanged
    {
        public virtual string tconst { get; set; }          //(string) - alphanumeric unique identifier of the title
        public virtual string directors { get; set; }       //(array of nconsts) - director(s) of the given title
        public virtual string writers { get; set; }         //(array of nconsts) – writer(s) of the given title

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
