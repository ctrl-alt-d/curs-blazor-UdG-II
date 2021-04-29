using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record PostMoveParms
    {
        public PostMoveParms()
        {
        }

        public PostMoveParms(int postId, int toBlogId)
        {
            PostId = postId;
            ToBlogId = toBlogId;
        }

        public int PostId { get; set; }
        public int ToBlogId { get; set; }

    }
}
