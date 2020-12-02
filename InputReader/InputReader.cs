using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Input
{
    public static class InputReader
    {
        public static string SessionKey { get; set; } 


        public static async Task<string> GetInput(int day)
        {
            if (string.IsNullOrEmpty(SessionKey))
                throw new InvalidOperationException($"Session key is not set.");
            var baseAddress = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                cookieContainer.Add(baseAddress, new Cookie("session", SessionKey));
                var result = await client.GetAsync($"/2020/day/{day}/input");
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }
    }

}
