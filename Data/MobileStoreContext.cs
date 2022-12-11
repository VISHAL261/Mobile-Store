using Microsoft.EntityFrameworkCore;

namespace MobileStore.Data
{
    public class MobileStoreContext : DbContext
    {
        public MobileStoreContext (DbContextOptions<MobileStoreContext> options)
            : base(options)
        {
        }

        public DbSet<MobileStore.Models.MobileInformation> MobileInformation { get; set; }

        public DbSet<MobileStore.Models.User> User { get; set; }
    }
}
