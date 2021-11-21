using BlinkCash.Core.Models;
using BlinkCash.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUserExtension>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<ConfirmationToken> ConfirmationToken { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestion { get; set; } 
        public virtual DbSet<UserSecurityQuestionAndAnswer> UserSecurityQuestionAndAnswer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);             
        }
    }
}
