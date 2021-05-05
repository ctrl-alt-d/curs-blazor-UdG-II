using BS.Pages;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS.Shared
{
    public partial class BSTable<T>
    {
        [Parameter] public IEnumerable<T> datasource { get; set; } = Array.Empty<T>();

        [Parameter] public RenderFragment Head { get; set; }

        [Parameter] public RenderFragment<T> Body { get; set; }
    }
}
