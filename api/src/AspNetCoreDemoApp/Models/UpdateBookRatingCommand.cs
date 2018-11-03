using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemoApp.Models
{
    public class UpdateBookRatingCommand
    {
        public string BookId { get; set; }

        public double Rating { get; set; }
    }
}
