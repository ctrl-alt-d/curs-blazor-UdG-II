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

    public static class BlogResultFactory
    {

        public static MethodResult<BlogDto> AsMethodResult(this Blog blog, BlogContext ctx)
        {
            return new MethodResult<BlogDto>
            {
                Data = blog.AsDto(ctx)
            };
        }

        public static MethodResult<PostDto> AsMethodResult(this Post post, BlogContext ctx)
        {
            return new MethodResult<PostDto>
            {
                Data = post.AsDto(ctx)
            };
        }

        public static BlogDto AsDto(this Blog blog, BlogContext ctx)
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

        public static PostDto AsDto(this Post post, BlogContext ctx)
        {
            ctx.Entry(post).Reference(post => post.Blog).Load();
            return new PostDto(
                                postId: post.PostId,
                                title: post.Title,
                                content: post.Content,
                                blog: post.Blog.AsDto(ctx)
                            );
        }

    }
}
