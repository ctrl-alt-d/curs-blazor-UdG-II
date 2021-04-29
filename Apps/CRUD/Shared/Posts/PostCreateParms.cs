using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record PostCreateParms
    {
        public PostCreateParms()
        {
        }

        public PostCreateParms(int blogId, string title, string content)
        {
            BlogId = blogId;
            Title = title;
            Content = content;
        }

        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
