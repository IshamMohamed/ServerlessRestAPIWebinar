#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System;


private static HttpClient httpClient = new HttpClient();

public static async Task<HttpResponseMessage> Run(HttpRequest req, int id, ILogger log)
{
    var logicAppUri = Environment.GetEnvironmentVariable("GetTaskLogicAppUri");
    var reqbody = await new StreamReader(req.Body).ReadToEndAsync();

    var response = await httpClient.GetAsync(string.Format(logicAppUri, id)); 
    var resultstring = await response.Content.ReadAsStringAsync();

    return response.IsSuccessStatusCode
    ? new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(resultstring, Encoding.UTF8, "application/json")
        }
    : new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent(resultstring, Encoding.UTF8, "application/json")
        };
}
