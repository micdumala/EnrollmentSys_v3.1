using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EnrollmentSys_v3._1.Models;

namespace EnrollmentSys_v3._1.Context
{
    public class PreregisterDbContext: DbContext
    {
        public DbSet<Preregister> Preregister  { get; set; }
        public PreregisterDbContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}