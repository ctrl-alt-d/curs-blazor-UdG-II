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

        private static MethodResult<BlogDto> AsMethodResult(BlogContext ctx, Blog blog)
        {
            return new MethodResult<BlogDto>
            {
                Data = AsDto(ctx, blog)
            };
        }

        private static MethodResult<PostDto> AsMethodResult(BlogContext ctx, Post post)
        {
            return new MethodResult<PostDto>
            {
                Data = AsDto(ctx, post)
            };
        }

        private static BlogDto AsDto(BlogContext ctx, Blog blog)
        {
            ctx.Entry(blog).Collection(blog => blog.Posts).Load();
            return new BlogDto(
                                blogId: blog.BlogId,
                                dataDePublicacio: blog.DiaDePublicacio,
                                nom: blog.Nom,
                                titolsDelsPosts: blog.Posts.Select(p => p.Title).ToList(),
                                contingutPerAdults: blog.ContingutPerAdults
                            );
        }

        private static PostDto AsDto(BlogContext ctx, Post post)
        {
            ctx.Entry(post).Reference(post => post.Blog).Load();
            return new PostDto(
                                postId: post.PostId,
                                title: post.Title,
                                content: post.Content,
                                blog: AsDto(ctx, post.Blog!)
                            );
        }


        private static MethodResult<T> AsMethodResult<T>(BlogContext ctx, T data) where T: class, new()
        {
            return new MethodResult<T>
            {
                Data = data
            };
        }

        private static MethodResult<T> MethodErrorResult<T>(string error) where T: class, new()
        {
            return new MethodResult<T>
            {
                Errors = new List<string> { error }
            };
        }


    }
}
