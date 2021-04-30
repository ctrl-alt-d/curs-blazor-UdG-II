using System.ComponentModel.DataAnnotations;

namespace Datalayer
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        [Required]
        public Blog Blog { get; set; } = default!;
    }

}
