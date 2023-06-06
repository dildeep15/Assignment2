using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Models
{
    internal class CustomerGenre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int totalTracks { get; set; }
    }
}
