using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TvShowsApp.Models
{
    public class RentedMovies
    {
        [Key]
        public int RentedMovieId { get; set; }
        
        public int TvID { get; set; }
        
        public int UsersID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RentDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
        //public Users Users { get; set; }
        [ForeignKey("TvID")]
        
        public virtual TvShow TvShow { get; set; }
        [ForeignKey("UsersID")]
       public virtual Users Users { get; set; }

        //public ICollection<TvShow> TvShows { get; set; } 


    }
}
