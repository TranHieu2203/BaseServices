using Core.Interfaces;
using Infrastructure.Respository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseServicesContext _context;
        private readonly ILogger _logger;
        public IProjectRepository Projects { get; private set; }

    public UnitOfWork(
        BaseServicesContext context,
        ILoggerFactory logger
        )
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

        Projects = new ProjectRepository(_context, _logger);
        }

    public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
