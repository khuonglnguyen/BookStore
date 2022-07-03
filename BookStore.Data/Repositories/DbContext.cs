using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Repositories
{
    public class DbCon : DbContext
    {
        public DbCon(DbContextOptions<DbCon> context) : base(context)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
