using Blogging.Shared;
using BusinessLayer.Abstractionns;
using Datalayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public partial class BlogBusinessLayer : IBlogBusinessLayer
    {
        public async Task<MethodResult<PostDto>> CreatePost(PostCreateParms parms)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions
            var blog = await ctx.Blogs.FindAsync(parms.BlogId);
            if (blog==null)
            {
                return MethodErrorResult<PostDto>("Blog no trobat");
            }

            // body            
            var post = new Post
            {
                Blog = blog!,
                Content = parms.Content,
                Title = parms.Title
            };
            ctx.Add(post);

            var es_contingut_adults = post.Content.Contains("caca") || post.Title.Contains("caca");
            blog.ContingutPerAdults = blog.ContingutPerAdults || es_contingut_adults;

            // post condicions

            // persistim            
            await ctx.SaveChangesAsync();

            // retornem
            return AsMethodResult(ctx,post);
        }

        public async Task<MethodResult<PostDto>> DeletePost(int id)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions
            var post = await ctx.Posts.FindAsync(id);
            if (post == null)
            {
                return MethodErrorResult<PostDto>("Post no trobat");
            }

            // body            
            ctx.Posts.Remove(post);

            // post condicions

            // persistim
            await ctx.SaveChangesAsync();

            // retornem
            return AsMethodResult(ctx,post);
        }

        public async Task<MethodResult<PostDto>> MovePost(PostMoveParms parms)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions
            var post = await ctx.Posts.FindAsync(parms.PostId);
            if (post == null)
            {
                return MethodErrorResult<PostDto>("Post no trobat");
            }

            var toBlog = await ctx.Blogs.FindAsync(parms.ToBlogId);
            if (toBlog == null)
            {
                return MethodErrorResult<PostDto>("Blog no trobat");
            }

            // body            
            post.Blog = toBlog;

            // post condicions

            // persistim
            await ctx.SaveChangesAsync();

            // retornem
            return AsMethodResult(ctx,post);
        }

        public async Task<MethodResult<PostDto>> RetrievePostById(int id)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions
            var post = await ctx.Posts.FindAsync(id);
            if (post == null)
            {
                return MethodErrorResult<PostDto>("Post no trobat");
            }

            // body            
            ctx.Posts.Remove(post);

            // post condicions

            // persistim
            await ctx.SaveChangesAsync();

            // retornem
            return AsMethodResult(ctx,post);
        }

        public async Task<MethodResult<List<PostDto>>> RetrievePosts()
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions

            // body            
            var posts_dto =
                await
                ctx
                .Posts
                .Include(post => post.Blog)
                .Select(post => AsDto(ctx, post))
                .ToListAsync();

            // post condicions

            // retornem
            return AsMethodResult(ctx,posts_dto);
        }

        public async Task<MethodResult<PostDto>> UpdatePost(PostUpdateParms parms)
        {
            using var ctx = BlogContextFactory.CreateDbContext();

            // pre-condicions
            var post = await ctx.Posts.FindAsync(parms.PostId);
            if (post == null)
            {
                return MethodErrorResult<PostDto>("Post no trobat");
            }

            // body - updating post
            post.Title = parms.Title;
            post.Content = parms.Content;

            // body - updating related data
            var es_contingut_adults = post.Content.Contains("caca") || post.Title.Contains("caca");
            await ctx.Entry(post).Reference(p => p.Blog).LoadAsync();
            post.Blog.ContingutPerAdults = post.Blog.ContingutPerAdults || es_contingut_adults;

            // post condicions

            // persistim            
            await ctx.SaveChangesAsync();

            // retornem
            return AsMethodResult(ctx,post);
        }
    }
}
