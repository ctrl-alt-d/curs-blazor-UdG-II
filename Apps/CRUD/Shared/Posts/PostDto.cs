using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record PostDto
    {
        public PostDto()
        {
        }

        public PostDto(int postId, string title, string content, BlogDto blog)
        {
            PostId = postId;
            Title = title;
            Content = content;
            Blog = blog;
        }

        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public BlogDto Blog { get; set; } = default!;
    }
}
