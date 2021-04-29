using System.ComponentModel.DataAnnotations;

namespace Blogging.Client.Components.Pages
{
    public partial class CrearBlog
    {
        public class CrearBlogWebView
        {
            [Required(ErrorMessage = "El camp nom és obligatori")]
            public string Nom { get; set; }

            [Required(ErrorMessage = "El camp titol primer post és obligatori")]
            public string NomPost { get; internal set; }
            public bool ContingutPerAdults { get; internal set; }
        }

    }
}
