using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class AccountsDatabase : DbContext
    {
        public AccountsDatabase()
           : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Modules> Modules { get; set; }

        public DbSet<Razrabotchiki> Razrabotchiki { get; set; }

        public DbSet<Statuses> Statuses { get; set; }

        public DbSet<Oshibki> Oshibki { get; set; }
    }
}