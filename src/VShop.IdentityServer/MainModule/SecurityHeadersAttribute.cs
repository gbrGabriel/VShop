// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServerHost.Quickstart.UI
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ViewResult)
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                    context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
                {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                    context.HttpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
                var csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";
                // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
                //csp += "upgrade-insecure-requests;";
                // also an example if you need client images to be displayed from twitter
                // csp += "img-src 'self' https://pbs.twimg.com;";

                // once for standards compliant browsers
                if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                    context.HttpContext.Response.Headers.Add("Content-Security-Policy", csp);
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                }
                // and once again for IE
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
                {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                    context.HttpContext.Response.Headers.Add("X-Content-Security-Policy", csp);
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
                var referrer_policy = "no-referrer";
                if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy"))
                {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                    context.HttpContext.Response.Headers.Add("Referrer-Policy", referrer_policy);
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
                }
            }
        }
    }
}
