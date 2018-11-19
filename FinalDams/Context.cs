using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDams
{
    public class Context : DbContext
    {
        public DbSet<Data> Data { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Meta> Meta { get; set; }
        public DbSet<AssetType> Types { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Access> Access { get; set; }
    }
}
