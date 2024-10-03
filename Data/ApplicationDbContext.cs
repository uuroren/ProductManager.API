using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManager.API.Entities;
using ProductManager.API.Models;

namespace ProductManager.API.Data {
    public class ApplicationDbContext:IdentityDbContext<User,IdentityRole<Guid>,Guid> {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }

        public DbSet<Product> Products { get; set; }
    }
}
