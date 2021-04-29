using Blogging.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractionns
{
    public interface IBlogBusinessLayer
    {
        // Create - Blogs
        Task<MethodResult<BlogDto>> CreateBlog(BlogCreateParms parms);

        // Retrieve - Blogs
        Task<MethodResult<List<BlogDto>>> RetrieveBlogs();
        Task<MethodResult<BlogDto>> RetrieveBlogById(int id);

        // Update - Blogs
        Task<MethodResult<BlogDto>> PublicaBlog(int id);
        Task<MethodResult<BlogDto>> CanviaNom(BlogCanviaNomParms parms);

        // Delete - Blogs

        /* ----------------------- */

        // Create - Posts
        Task<MethodResult<PostDto>> CreatePost(PostCreateParms parms);

        // Retrieve - Posts
        Task<MethodResult<List<PostDto>>> RetrievePosts();
        Task<MethodResult<PostDto>> RetrievePostById(int id);

        // Update - Posts
        Task<MethodResult<PostDto>> UpdatePost(PostUpdateParms parms);
        Task<MethodResult<PostDto>> MovePost(PostMoveParms parms);


        // Delete - Posts
        Task<MethodResult<PostDto>> DeletePost(int id);

    }
}