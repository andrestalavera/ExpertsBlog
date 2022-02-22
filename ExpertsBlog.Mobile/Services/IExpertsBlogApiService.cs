using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ExpertsBlog.Entities;
using Newtonsoft.Json;

namespace ExpertsBlog.Mobile.Services
{
    public interface IExpertsBlogApiService
    {
        Task<IEnumerable<BlogPost>> GetBlogPosts();
    }

    public class ExpertsBlogApiService : IExpertsBlogApiService
    {
        public async Task<IEnumerable<BlogPost>> GetBlogPosts()
        {
            using (HttpClient httpclient = new HttpClient()
            {
                BaseAddress = new Uri("https://expertsblogapi.azurewebsites.net/")
            })
            {
                var json = await httpclient.GetStringAsync("BlogPosts");
                Debug.WriteLine("************************************************" + json);
                return JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(json);
            }
        }
    }
}