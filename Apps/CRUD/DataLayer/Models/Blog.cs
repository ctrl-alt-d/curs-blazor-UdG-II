using System;
using System.Collections.Generic;

namespace Datalayer
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public DateTime? DiaDePublicacio { get; set; }
        public List<Post> Posts { get; set; } = new();
        public Boolean ContingutPerAdults { get; set; }
    }

}
