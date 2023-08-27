using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class review
    {
        public string reviewer_email { get; set; }
        public string book_id { get; set; }
        public int rating { get; set; }
        public string title { get; set; }
        public string details { get; set; }
    }
}
