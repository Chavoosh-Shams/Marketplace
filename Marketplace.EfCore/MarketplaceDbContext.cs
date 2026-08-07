using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Marketplace.EfCore.Frameworks;
using Microsoft.AspNetCore.Identity;
using Marketplace.Domain.Frameworks.Abstracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Marketplace.EfCore
{
    public class MarketplaceDbContext : IdentityDbContext<IdentityUser>
    {
        #region [- ctor -]
        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options)
        {
        }
        #endregion

        #region [- OnModelCreating(ModelBuilder modelBuilder) -]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            #region [- RegisterAllEntities() -]
            modelBuilder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);
            #endregion

            #region [- ApplyConfigurationsFromAssembly() -]
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }
}
