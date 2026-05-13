using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        public DbSet<Useres> useres=> Set<Useres>();
        public DbSet<Gifts>  gifts => Set<Gifts>();
        public DbSet<Donors>  donors => Set<Donors>();
        public DbSet<Orders>  orders => Set<Orders>();
        public DbSet<Categories> categories => Set<Categories>();
        public DbSet<Lottery> lotteries => Set<Lottery>();  
    }
}
