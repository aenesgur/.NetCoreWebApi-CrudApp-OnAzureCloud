using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApp.Entities
{
    public class Book : IEntity
    {
        public int Id { get ; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
