using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

using Amazon.Lambda.Core;
using System.Reflection.Metadata;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NYTimes
{
    public class Function
    {
        public static readonly HttpClient client = new HttpClient();

        public async Task<ExpandoObject> FunctionHandler(string input, ILambdaContext context)
        {
            string search = input.ToString();
            HttpResponseMessage res = await client.GetAsync("https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=z8vWtDc1q9BpwACFGvHQXKpffCnyOG8R");
            res.EnsureSuccessStatusCode();
            string resBody=await res.Content.ReadAsStringAsync();
            ExpandoObject books = JsonConvert.DeserializeObject<ExpandoObject>(resBody);
           // Console.WriteLine(resBody);
           // Console.WriteLine(books);
            return books;
        }
    }
}
