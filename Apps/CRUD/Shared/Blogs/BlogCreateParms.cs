using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record BlogCreateParms
    {
        public BlogCreateParms()
        {
        }

        public BlogCreateParms(string nomBlog, string titolPrimerPost, bool contingutPerAdults)
        {
            NomBlog = nomBlog;
            TitolPrimerPost = titolPrimerPost;
            ContingutPerAdults = contingutPerAdults;
        }

        public string NomBlog { get; set; } = string.Empty;
        public string TitolPrimerPost { get; set; } = string.Empty;
        public bool ContingutPerAdults { get; set; }
    }
}
