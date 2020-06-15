using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace EmployreeService
{

    public class CutomJsonFormatter : JsonMediaTypeFormatter
    {
        //public CutomJsonFormatter()
        //{
        //    this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //}

        //public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        //{
        //    base.SetDefaultContentHeaders(type, headers, mediaType);
        //    headers.ContentType = new MediaTypeHeaderValue("appclication/json");       
        //}
    }

    public static class WebApiConfig
    {


        public static void Register(HttpConfiguration config)
        {
            // Web API 구성 및 서비스

            // Web API 경로
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //config.Formatters.Add(new CutomJsonFormatter());


            /// xml 포맷터 지우기 결과적으로 json 만 
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            /// 카멜케이스 속성으로 변경 exam) ID > id , FirstName > firstName
    
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }

}


