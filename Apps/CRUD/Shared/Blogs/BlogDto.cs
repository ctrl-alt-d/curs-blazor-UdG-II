using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record BlogDto
    {
        public BlogDto()
        {
        }

        public BlogDto(int blogId, string nom, DateTime? dataDePublicacio, List<string> titolsDelsPosts, bool contingutPerAdults)
        {
            BlogId = blogId;
            Nom = nom;
            DataDePublicacio = dataDePublicacio;
            TitolsDelsPosts = titolsDelsPosts;
            ContingutPerAdults = contingutPerAdults;
        }

        public int BlogId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public DateTime? DataDePublicacio { get; set; }
        public List<string> TitolsDelsPosts { get; set; } = new();
        public Boolean ContingutPerAdults { get; set; }

    }
}
