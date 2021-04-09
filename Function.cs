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
        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string search ="";
            Dictionary<string, string> dict = (Dictionary<string, string>)input.QueryStringParameters;
            dict.TryGetValue("search", out search);
            HttpResponseMessage res = await client.GetAsync($"https://api.nytimes.com/svc/books/v3/lists/current/{search}.json?api-key=z8vWtDc1q9BpwACFGvHQXKpffCnyOG8R"); ;
            string resBody=await res.Content.ReadAsStringAsync();
            ExpandoObject books = JsonConvert.DeserializeObject<ExpandoObject>(resBody);
            return books;
        }
    }
}
