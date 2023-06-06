using Microsoft.EntityFrameworkCore;
using RestaurantAppi.Core.Domain.Common;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<TableStatus> TableStatuses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Dish_Ingredient> Dish_Ingredients { get; set; }
        public DbSet<Order_Dish> Order_Dishes { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
						entry.Entity.Id = int.Parse(DateTime.UtcNow.Ticks.ToString().Substring(8, 9));
						entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            modelBuilder.UseSerialColumns();

            #region Tables

            modelBuilder.Entity<Ingredient>()
                .ToTable("Ingredients");

            modelBuilder.Entity<Dish>()
                .ToTable("Dishes");

            modelBuilder.Entity<Table>()
                .ToTable("Tables");

            modelBuilder.Entity<Order>()
                .ToTable("Orders");

            modelBuilder.Entity<DishCategory>()
                .ToTable("Dish_Categories");

            modelBuilder.Entity<TableStatus>()
                .ToTable("Table_Statuses");

            modelBuilder.Entity<OrderStatus>()
                .ToTable("Order_Statuses");

            modelBuilder.Entity<Dish_Ingredient>()
                .ToTable("Dish_Ingredients");

            modelBuilder.Entity<Order_Dish>()
                .ToTable("Order_Dishes");

			modelBuilder.Entity<RefreshToken>()
				.ToTable("Refresh_Tokens");
			#endregion

			#region Primary keys
			modelBuilder.Entity<Ingredient>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Dish>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Table>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<DishCategory>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TableStatus>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<OrderStatus>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Dish_Ingredient>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Order_Dish>()
                .HasKey(x => x.Id);

			modelBuilder.Entity<RefreshToken>()
				.HasKey(x => x.Id);
			#endregion

			#region Relationships

			modelBuilder.Entity<Dish>()
                .HasOne<DishCategory>(x => x.Category)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dish_Ingredient>()
                .HasOne<Dish>(x => x.Dish)
                .WithMany(x => x.Dish_Ingredients)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dish_Ingredient>()
                .HasOne<Ingredient>(x => x.Ingredient)
                .WithMany(x => x.Dish_Ingredients)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order_Dish>()
                .HasOne<Order>(x => x.Order)
                .WithMany(x => x.Order_Dishes)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order_Dish>()
                .HasOne<Dish>(x => x.Dish)
                .WithMany(x => x.Order_Dishes)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne<Table>(x => x.Table)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne<OrderStatus>(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Table>()
                .HasOne<TableStatus>(x => x.Status)
                .WithMany(x => x.Tables)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Property configurations
			#endregion
		}

    }
}
