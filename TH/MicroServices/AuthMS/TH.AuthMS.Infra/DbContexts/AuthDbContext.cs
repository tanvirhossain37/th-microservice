using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.App;

namespace TH.AuthMS.Infra
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        //Tanvir - If abstract class is used, then you have to add the derived class here. But there will be
        //a Discriminator column
        //public DbSet<UserImpl> Users { get; set; }
    }
}