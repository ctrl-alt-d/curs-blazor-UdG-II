using System;
using System.Threading.Tasks;
using BusinessLayer.Abstractionns;
using Blogging.Shared;
using System.Collections.Generic;

namespace Server.Test
{
    public class BlogBusinessFakeLayer : IBlogBusinessLayer
    {
        public Task<MethodResult<BlogDto>> CanviaNom(BlogCanviaNomParms parms)
        {
            var result = new MethodResult<BlogDto>
            {
                Data = new BlogDto
                {
                    BlogId = parms.Id,
                    Nom = parms.NouNom,
                    TitolsDelsPosts = new()
                    {
                        "fake titol"
                    }
                }
            };
            return Task.FromResult(result);
        }

        public Task<MethodResult<BlogDto>> CreateBlog(BlogCreateParms parms)
        {
            var result = new MethodResult<BlogDto>
            {
                Data = new BlogDto
                {
                    BlogId = 100,
                    Nom = parms.NomBlog,
                    TitolsDelsPosts = new()
                    {
                        parms.TitolPrimerPost
                    }
                }
            };
            return Task.FromResult(result);
        }

        public Task<MethodResult<List<BlogDto>>> RetrieveBlogs()
        {
            var result = new MethodResult<List<BlogDto>>
            {
                Data = new List<BlogDto>
                {
                    new BlogDto
                    {
                        BlogId = 1,
                        Nom= "Fake blog",
                        TitolsDelsPosts = new()
                        {
                            "fake titol"
                        }
                    }
                }
            };
            return Task.FromResult(result);
        }

        public Task<MethodResult<BlogDto>> RetrieveBlogById(int id)
        {
            var result = new MethodResult<BlogDto>
            {
                Data = new BlogDto
                {
                    BlogId = id,
                    Nom = "fake nom",
                    TitolsDelsPosts = new()
                    {
                        "fake titol"
                    }
                }
            };
            return Task.FromResult(result);
        }

        public Task<MethodResult<BlogDto>> PublicaBlog(int id)
        {
            var result = new MethodResult<BlogDto>
            {
                Data = new BlogDto
                {
                    BlogId = id,
                    Nom = "fake nom",
                    DataDePublicacio = DateTime.Now,
                    TitolsDelsPosts = new()
                    {
                        "fake titol"
                    }

                }
            };
            return Task.FromResult(result);
        }
    }
}
