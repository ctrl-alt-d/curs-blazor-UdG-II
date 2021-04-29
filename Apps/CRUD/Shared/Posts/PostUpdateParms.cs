using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record PostUpdateParms
    {
        public PostUpdateParms()
        {
        }

        public PostUpdateParms(int postId, string title, string content)
        {
            PostId = postId;
            Title = title;
            Content = content;
        }

        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
