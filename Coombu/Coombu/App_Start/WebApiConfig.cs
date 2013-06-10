using Coombu.Formatters;
using Coombu.Models.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Routing;

namespace Coombu
{
    public static class WebApiConfig
    {       
        public static void Register(HttpConfiguration config)
        {                        
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize; 
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects; 
            
            config.Routes.MapHttpRoute(
                name: "LikeApi",
                routeTemplate: "api/{id}/like",
                defaults: new { controller = "Like" },
                constraints: new { method = new HttpMethodConstraint("POST", "DELETE"), id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "FollowApi",
                routeTemplate: "api/{id}/follow",
                defaults: new { controller = "Follow" },
                constraints: new { method = new HttpMethodConstraint("POST", "DELETE"), id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );                    

            config.Routes.MapHttpRoute(
                name: "PostCommentsApi",
                routeTemplate: "api/posts/{postId}/comments/{commentId}",
                defaults: new { controller = "Comments", commentId = RouteParameter.Optional },
                constraints: new { postId = @"\d+" }
            );
        }
    }
}
