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
            var result = await HttpClient.PostAsJsonAsync("api/Blog/Create", parms);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<List<BlogDto>>> RetrieveBlogs()
        {
            var result = await HttpClient.GetAsync("api/Blog/GetAll");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<List<BlogDto>>>();
            return r!;
        }

        public async Task<MethodResult<BlogDto>> RetrieveBlogById(int id)
        {
            var result = await HttpClient.GetAsync($"api/Blog/GetById/{id}");
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }

        public async Task<MethodResult<BlogDto>> PublicaBlog(int id)
        {
            var result = await HttpClient.PostAsJsonAsync("api/Blog/PublicaBlog", id);
            var r = await result.Content.ReadFromJsonAsync<MethodResult<BlogDto>>();
            return r!;
        }
    }
}
