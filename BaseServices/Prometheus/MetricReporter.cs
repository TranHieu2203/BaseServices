using Microsoft.Extensions.Logging;
using Prometheus;
using System;

namespace BaseServices.Prometheus
{
    /// <summary>
    /// Class MetricReporter 
    /// </summary>
    public class MetricReporter
    {
        private readonly ILogger<MetricReporter> _logger;
        private readonly Counter _requestCounter;
        private readonly Histogram _responseTimeHistogram;
        private readonly Summary _summary;

        /// <summary>
        /// Contructor using DI
        /// </summary>
        /// <param name="logger"></param>
        public MetricReporter(ILogger<MetricReporter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Init MetricReporter");

            _requestCounter = Metrics.CreateCounter("Tong_so_request", "Tong_so_request_duoc_goi.");

            _responseTimeHistogram = Metrics.CreateHistogram("Thoi_gian_phan_hoi",
                "Khoang thoi gian phan hoi(tinh_theo_giay).", new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(0.01, 2, 10),
                    LabelNames = new[] { "status_code", "method" }
                });
            _summary = Metrics.CreateSummary("Dung_luong", "Dung_luong_cua_request");
        }

        /// <summary>
        /// Method Register for request
        /// </summary>
        public void RegisterRequest()
        {
            _requestCounter.Inc();
        }

        /// <summary>
        /// Method register response time
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="method"></param>
        /// <param name="elapsed"></param>
        public void RegisterResponseTime(int statusCode, string method, TimeSpan elapsed)
        {
            _responseTimeHistogram.Labels(statusCode.ToString(), method).Observe(elapsed.TotalSeconds);
        }

        /// <summary>
        /// Method summary
        /// </summary>
        public void RegisterSummary()
        {
            _summary.WithLabels();
        }
    }
}
