using Blogging.Shared;
using BusinessLayer.Abstractionns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogging.Server.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class BlogController : ControllerBase, IBlogBusinessLayer
    {
        private readonly IBlogBusinessLayer _BlogBusinessLayer;

        public BlogController(IBlogBusinessLayer blogBusinessLayer)
        {
            _BlogBusinessLayer = blogBusinessLayer;
        }

        [HttpPost("[action]")]
        public Task<MethodResult<BlogDto>> CanviaNom([FromBody] BlogCanviaNomParms parms)
        {
            return _BlogBusinessLayer.CanviaNom(parms);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<BlogDto>> CreateBlog([FromBody]  BlogCreateParms parms)
        {
            return _BlogBusinessLayer.CreateBlog(parms);
        }

        [HttpGet("[action]")]
        public Task<MethodResult<List<BlogDto>>> RetrieveBlogs()
        {
            return _BlogBusinessLayer.RetrieveBlogs();
        }

        [HttpGet("[action]/{id:int}")]
        public Task<MethodResult<BlogDto>> RetrieveBlogById(int id)
        {
            return _BlogBusinessLayer.RetrieveBlogById(id);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<BlogDto>> PublicaBlog([FromBody] int id)
        {
            return _BlogBusinessLayer.PublicaBlog(id);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<PostDto>> CreatePost(PostCreateParms parms)
        {
            return _BlogBusinessLayer.CreatePost(parms);
        }

        [HttpGet("[action]")]
        public Task<MethodResult<List<PostDto>>> RetrievePosts()
        {
            return _BlogBusinessLayer.RetrievePosts();
        }

        [HttpGet("[action]/{id:int}")]
        public Task<MethodResult<PostDto>> RetrievePostById(int id)
        {
            return _BlogBusinessLayer.RetrievePostById(id);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<PostDto>> UpdatePost([FromBody] PostUpdateParms parms)
        {
            return _BlogBusinessLayer.UpdatePost(parms);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<PostDto>> MovePost([FromBody] PostMoveParms parms)
        {
            return _BlogBusinessLayer.MovePost(parms);
        }

        [HttpPost("[action]")]
        public Task<MethodResult<PostDto>> DeletePost([FromBody] int id)
        {
            return _BlogBusinessLayer.DeletePost(id);
        }
    }
}
