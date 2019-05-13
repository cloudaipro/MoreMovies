using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MoreMovies.Models
{
    //Contains the IMDb rating and votes information for titles
    public class Title_Ratings : INotifyPropertyChanged
    {
        public virtual string tconst { get; set; }           // (string) - alphanumeric unique identifier of the title
        public virtual float? averageRating { get; set; }    // – weighted average of all the individual user ratings
        public virtual int? numVotes { get; set; }         // - number of votes the title has received

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
