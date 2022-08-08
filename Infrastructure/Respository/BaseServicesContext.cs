﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Respository
{
 
    public class BaseServicesContext : DbContext
    {
        public BaseServicesContext(DbContextOptions<BaseServicesContext> options) : base(options)
        {

        }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Custommer> Custommers { get; set; }

    }
}