using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TvShowsApp.Models
{
    public class Users
    {
        public Users()
        {
            RentedMovies = new HashSet<RentedMovies>();
        }
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public ICollection<RentedMovies> RentedMovies { get; set; }
    }
}
