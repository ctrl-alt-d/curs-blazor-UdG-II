using Blogging.Shared;
using BusinessLayer.Abstractionns;
using Datalayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public partial class BlogBusinessLayer : IBlogBusinessLayer
    {
        private readonly IDbContextFactory<BlogContext> BlogContextFactory;

        public BlogBusinessLayer(IDbContextFactory<BlogContext> myFactory)
        {
            BlogContextFactory = myFactory;
        }

    }
}
