using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitNebula.Developkit.Network;

public partial class HttpRequest
{
    public async Task<HttpResponseMessage> PostAsync(string name, string url, string json)
    {
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        return await PostAsync(name, url, content);
    }

    public async Task<HttpResponseMessage> PostAsync(string name, string url, HttpContent content)
    {
        var client = httpClientFactory.CreateClient(name);
        return await client.PostAsync(url, content);
    }

    public async Task<T?> PostAsync<T>(string name, string url, string json) where T : class
    {
        logger.LogDebug("Request Url: {url}", url);
        logger.LogDebug("Request Content: {json}", json);
        var response = await PostAsync(name, url, json);
        logger.LogInformation("Response Status Code: {StatusCode}", response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            logger.LogDebug("Response Content: {responseString}", responseString);
            try
            {
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Json解析响应内容失败: {responseString}", responseString);
            }
        }
        return default;
    }

    public async Task<T?> PostAsync<T>(string name, string url, HttpContent content) where T : class
    {
        logger.LogDebug("Request Url: {url}", url);
        var response = await PostAsync(name, url, content);
        logger.LogInformation("Response Status Code: {StatusCode}", response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            logger.LogDebug("Response Content: {responseString}", responseString);
            try
            {
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Json解析响应内容失败: {responseString}", responseString);
            }
        }
        return default;
    }
}