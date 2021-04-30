using Blogging.Shared;
using BusinessLayer.Abstractionns;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Transport
{
    public class BlogTransport : IBlogBusinessLayer
    {

        private readonly HttpClient HttpClient;

        public BlogTransport(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<MethodResult<BlogDto>> CanviaNom(BlogCanviaNomParms parms)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/CanviaNom", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<BlogDto>> CreateBlog(BlogCreateParms parms)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/CreateBlog", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<List<BlogDto>>> RetrieveBlogs()
        {
            var result = await HttpClient.GetAsync("api/Blog/RetrieveBlogs");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<List<BlogDto>>>();
            return r!;
        }

        public async Task<MethodResult<BlogDto>> RetrieveBlogById(int id)
        {
            var result = await HttpClient.GetAsync($"api/Blog/RetrieveBlogById/{id}");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<BlogDto>> PublicaBlog(int id)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/PublicaBlog", id);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<PostDto>> CreatePost(PostCreateParms parms)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/CreatePost", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<PostDto>>();
            return r!;
        }

        public async Task<MethodResult<List<PostDto>>> RetrievePosts()
        {
            var result = await HttpClient.GetAsync("api/Blog/RetrievePosts");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<List<PostDto>>>();
            return r!;
        }

        public async Task<MethodResult<PostDto>> RetrievePostById(int id)
        {
            var result = await HttpClient.GetAsync($"api/Blog/RetrievePostById/{id}");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<PostDto>>();
            return r!;
        }

        public async Task<MethodResult<PostDto>> UpdatePost(PostUpdateParms parms)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/UpdatePost", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<PostDto>>();
            return r!;
        }

        public async Task<MethodResult<PostDto>> MovePost(PostMoveParms parms)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/MovePost", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<PostDto>>();
            return r!;
        }

        public async Task<MethodResult<PostDto>> DeletePost(int id)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/DeletePost", id);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<PostDto>>();
            return r!;
        }
    }
}
