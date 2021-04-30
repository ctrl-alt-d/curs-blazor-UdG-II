using BusinessLayer.Abstractionns;
using Datalayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLayer.Test
{
    public class BlogTest
    {
        [Fact]
        public async Task ProvaElCreate()
        {

            // arrange
            var services = new ServiceCollection();
            services.AddDbContextFactory<BlogContext>(opt => opt.UseInMemoryDatabase(nameof(ProvaElCreate)));
            services.AddScoped<IBlogBusinessLayer, BlogBusinessLayer>();
            var serviceProvider = services.BuildServiceProvider();
            var blogbl = serviceProvider.GetRequiredService<IBlogBusinessLayer>();

            // act
            var parms = new Blogging.Shared.BlogCreateParms
            {
                NomBlog = "El meu blog",
                TitolPrimerPost = "El meu Titol",
            };
            var result = await blogbl.CreateBlog(parms);

            // assert
            var expected = parms.NomBlog;
            Assert.Equal(expected, result.Data.Nom);

        }

        [Fact]
        public async Task ProvaLaPublicacio()
        {

            // arrange DI
            var services = new ServiceCollection();
            services.AddDbContextFactory<BlogContext>(opt => opt.UseInMemoryDatabase(nameof(ProvaLaPublicacio)));
            services.AddScoped<IBlogBusinessLayer, BlogBusinessLayer>();
            var serviceProvider = services.BuildServiceProvider();
            var dbfactory = serviceProvider.GetRequiredService<IDbContextFactory<BlogContext>>();

            // arrange DB: desem un blog (sense data de publicació)
            var blog = await AfegirBlogAlDbContext(dbfactory);

            // act: publiquem el blog
            var blogbl = serviceProvider.GetRequiredService<IBlogBusinessLayer>();
            var result = await blogbl.PublicaBlog(1);

            // assert: s'ha d'haver desat a la base de dades
            using (var ctx = dbfactory.CreateDbContext())
            {
                var blog_from_ctx = await ctx.Blogs.FindAsync(blog.BlogId);
                var data_model_te_valor = blog_from_ctx.DiaDePublicacio.HasValue;
                Assert.True(data_model_te_valor);
            };

            // assert: el dto ha de contenir el valor de la data
            var data_dto_te_valor = result.Data.DataDePublicacio.HasValue;
            Assert.True(data_dto_te_valor);
        }

        [Fact]
        public async Task ProvaActualitzarUnPost()
        {

            // arrange DI
            var services = new ServiceCollection();
            services.AddDbContextFactory<BlogContext>(opt => opt.UseInMemoryDatabase(nameof(ProvaActualitzarUnPost)));
            services.AddScoped<IBlogBusinessLayer, BlogBusinessLayer>();
            var serviceProvider = services.BuildServiceProvider();
            var dbfactory = serviceProvider.GetRequiredService<IDbContextFactory<BlogContext>>();

            // arrange DB: desem un blog (sense data de publicació)
            var post = await AfegirPostAlDbContext(dbfactory);

            // act: publiquem el blog
            var blogbl = serviceProvider.GetRequiredService<IBlogBusinessLayer>();
            var parms = new Blogging.Shared.PostUpdateParms(
                postId: post.PostId,
                title: "New title",
                content: "New content. caca");
            var result = await blogbl.UpdatePost(parms);

            // assert: s'ha d'haver desat a la base de dades
            using (var ctx = dbfactory.CreateDbContext())
            {
                var blog_from_ctx = await ctx.Posts.FindAsync(post.PostId);
                var te_el_nou_titol = blog_from_ctx.Title == "New title";
                Assert.True(te_el_nou_titol);
            };

            // assert: el blog ha passat a ser d'adults
            var blog_adults = result.Data.Blog.ContingutPerAdults;
            Assert.True(blog_adults);
        }




        /* -------- helpers ------------- */

        private static async Task<Blog> AfegirBlogAlDbContext(IDbContextFactory<BlogContext> dbfactory)
        {
            var blog = new Blog
            {
                BlogId = 1,
                Nom = "Proves",
                DiaDePublicacio = null // <----- data publicació a null
            };
            using (var ctx = dbfactory.CreateDbContext())
            {
                ctx.Add(blog);
                await ctx.SaveChangesAsync();
            };
            return blog;
        }

        private static async Task<Post> AfegirPostAlDbContext(IDbContextFactory<BlogContext> dbfactory)
        {
            var blog = new Blog
            {
                BlogId = 1,
                Nom = "Proves",
                DiaDePublicacio = null // <----- data publicació a null
            };
            var post = new Post
            {
                PostId = 1,
                Blog = blog,
                Title = "Proves",                
            };

            using (var ctx = dbfactory.CreateDbContext())
            {
                ctx.Add(blog);
                ctx.Add(post);
                await ctx.SaveChangesAsync();
            };
            return post;
        }

    }
}
