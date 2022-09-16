using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class CustommerRepository : GenericRepository<Custommer>, ICustommerReponsitory
    {
        public CustommerRepository(BaseServicesContext context, ILogger logger) : base(context, logger)
        {

        }
    }

}
