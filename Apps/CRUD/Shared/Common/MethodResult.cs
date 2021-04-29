using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Shared
{
    public record MethodResult<T> where T: class, new()
    {
        public List<string> Errors { get; set; } = new();
        public T? Data { get; set; }
    }
}
