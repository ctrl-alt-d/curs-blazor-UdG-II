using System.ComponentModel.DataAnnotations;

namespace Blogging.Client.Components.Pages
{
    public partial class EditarBlog
    {
        public class EditarBlogWebView
        {
            [Required(ErrorMessage = "El camp nom és obligatori")]
            public string Nom { get; set; }
        }

    }
}
