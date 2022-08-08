using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(BaseServicesContext context, ILogger logger) : base(context, logger)
        {
           
        }
        public async Task<Project> FindByName(string name)
        {
            var obj = (from p in _context.Projects
                       where p.Name == name
                       select p);
            return  obj.FirstOrDefault();
            
        }
    }

}
