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
       
        public async Task<MethodResult<BlogDto>> CreateBlog(BlogCreateParms parms)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions


            // body
            var blog = new Blog
            {
                Nom = parms.NomBlog,
                ContingutPerAdults = parms.ContingutPerAdults,
            };
            var post = new Post
            {
                Blog = blog,
                Content = "Contingut del meu primer blog",
                Title = parms.TitolPrimerPost
            };

            // persistim
            ctx.Add(post);
            ctx.Add(blog);
            await ctx.SaveChangesAsync();

            // retornem
            return blog.AsMethodResult(ctx);
        }

        public async Task<MethodResult<BlogDto>> RetrieveBlogById(int id)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions

            // body
            var blog = await ctx.Blogs.FindAsync(id);

            //
            if (blog == null)
            {
                return ResultFactory.MethodErrorResult<BlogDto>("No trobat");
            }

            // retornem
            var result = blog.AsMethodResult(ctx);
            return result;
        }

        public async Task<MethodResult<List<BlogDto>>> RetrieveBlogs()
        {
            var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions

            // body
            var blogs_dto =
                await ctx
                .Blogs
                .Include(blog => blog.Posts)
                .Select(blog => blog.AsDto(ctx))
                .ToListAsync();

            // retornem
            return ResultFactory.AsMethodResult(ctx,blogs_dto);
        }

        public async Task<MethodResult<BlogDto>> PublicaBlog(int id)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions

            // body
            var blog = await ctx.Blogs.FindAsync(id);

            //
            if (blog == null)
            {
                return ResultFactory.MethodErrorResult<BlogDto>("No trobat");
            }

            blog.DiaDePublicacio = DateTime.Now;

            // persistim
            await ctx.SaveChangesAsync();

            // retornem
            var result = blog.AsMethodResult(ctx);
            return result;
        }

        public async Task<MethodResult<BlogDto>> CanviaNom(BlogCanviaNomParms parms)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions

            // body
            var blog = await ctx.Blogs.FindAsync(parms.Id);

            //
            if (blog == null)
            {
                return ResultFactory.MethodErrorResult<BlogDto>("No trobat");
            }

            //
            if (blog.DiaDePublicacio.HasValue)
            {
                return ResultFactory.MethodErrorResult<BlogDto>("No li podem canviar el nom a un blog publicat");
            }


            //
            blog.Nom = parms.NouNom;

            // persistim
            await ctx.SaveChangesAsync();

            // retornem
            var result = blog.AsMethodResult(ctx);
            return result;
        }
    }
}
