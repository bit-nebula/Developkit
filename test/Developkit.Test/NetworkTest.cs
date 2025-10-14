using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitNebula.Developkit.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BitNebula.Developkit.Test;


[TestClass]
public class NetworkTest
{

    [TestMethod]
    public void AddServiceTest()
    {
        IServiceCollection services = new ServiceCollection();

        //services.AddMulitpleHttpRequest("TestApp", options =>
        //{
        //    options.AppId = "testAppId";
        //    options.Domain = "https://www.example.com";
        //    options.Endpoints = [];
        //    options.Endpoints.Add("testApi", new RequestEndpoint()
        //    {
        //        Method = MethodType.Get,
        //        Url = "/api/Test/GetData",
        //    });
        //});

        services.AddHttpRequest("AAA", options =>
        {
            //options.AppId = "testAppId";
            options.Domain = "https://blog.csdn.net";
            options.Endpoints.Add("testApi", new RequestEndpoint()
            {
                Method = MethodType.Get,
                Url = "/zheliku/article/details/135583607",
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetService<IHttpRequestFactory>();
        Assert.IsNotNull(factory);

        var request = factory.Create("AAA");
        Assert.IsNotNull(request);

        var ret = request.SendAsync<object, object>("testApi", new { id = 1 }).ConfigureAwait(false).GetAwaiter().GetResult();
        Assert.IsNotNull(ret);

        //var httpRequestFactory = serviceProvider.GetService<IHttpRequestFactory>();
        //Assert.IsNotNull(httpRequestFactory);
        //var httpRequest = httpRequestFactory.Create("TestApp");

    }
}
