using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConceptArchitect.BookManagement
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string AuthorId { get; set; }

        public string CoverPhoto { get; set; }

        public override string ToString()
        {
            return $"Book[Id={Id} , Title={Title} ]";
        }
    }
}
