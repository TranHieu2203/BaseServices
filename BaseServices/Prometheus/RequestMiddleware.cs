using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BaseServices.Prometheus
{
    /// <summary>
    /// Class metruc middleware
    /// </summary>
    public class ResponseMetricMiddleware
    {
        private readonly RequestDelegate _request;

        /// <summary>
        /// Contructor for ResponseMetricMiddleware
        /// </summary>
        /// <param name="request"></param>
        public ResponseMetricMiddleware(RequestDelegate request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        /// <summary>
        /// Method invoke HttpContext with metricReporter
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, MetricReporter reporter)
        {
            var path = httpContext.Request.Path.Value;
            if (path == "/metrics")
            {
                await _request.Invoke(httpContext);
                return;
            }
            var sw = Stopwatch.StartNew();

            try
            {
                await _request.Invoke(httpContext);
            }
            finally
            {
                sw.Stop();
                if (!httpContext.Request.Path.ToString().Contains("api/docs/index"))
                {
                    reporter.RegisterRequest();
                    reporter.RegisterResponseTime(httpContext.Response.StatusCode, httpContext.Request.Path + httpContext.Request.Method, sw.Elapsed);
                    reporter.RegisterSummary();
                }

            }
        }
    }

}
