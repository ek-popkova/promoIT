using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Data
{
    public partial class promo_itContext : DbContext
    {
        public promo_itContext()
        {
        }

        public promo_itContext(DbContextOptions<promo_itContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SpResults> SpResults { get; set; }
        public virtual DbSet<Bcr> Bcrs { get; set; } = null!;
        public virtual DbSet<BcrShip> BcrShips { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<NonProfitRepresentative> NonProfitRepresentatives { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; } = null!;
        public virtual DbSet<ProductToCampaign> ProductToCampaigns { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sa> Sas { get; set; } = null!;
        public virtual DbSet<SaTransaction> SaTransactions { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<SaToCampaign> SaToCampaigns { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=promo_it_Kate;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bcr>(entity =>
            {
                entity.ToTable("BCR");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("company_name");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Bcrs)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BCR_status");

            });

            modelBuilder.Entity<BcrShip>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("BCR_ship");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BcrId).HasColumnName("BCR_id");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.ProductBought).HasColumnName("product_bought");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ProductReady).HasColumnName("product_ready");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Bcr)
                    .WithMany()
                    .HasForeignKey(d => d.BcrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BCR_ship_BCR");

                entity.HasOne(d => d.Campaign)
                    .WithMany()
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BCR_ship_campaign");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BCR_ship_product");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(50)
                    .HasColumnName("hashtag");

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.NprId).HasColumnName("NPR_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Npr)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.NprId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_non_profit_representative");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_status");
            });

            modelBuilder.Entity<NonProfitRepresentative>(entity =>
            {
                entity.ToTable("non_profit_representative");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.OrganizationLink)
                    .HasMaxLength(250)
                    .HasColumnName("organization_link");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.NonProfitRepresentatives)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_non_profit_representative_status");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BcrId).HasColumnName("BCR_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Bcr)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BcrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_BCR");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_status");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("product_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<ProductToCampaign>(entity =>
            {
                entity.ToTable("product_to_campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BoughtNumber).HasColumnName("bought_number");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.InititalNumber).HasColumnName("initital_number");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Campaign)
                    .WithMany()
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_to_campaign_campaign");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_to_campaign_product");

                entity.HasOne(d => d.Status)
                    .WithMany()
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_to_campaign_status");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_status");
            });

            modelBuilder.Entity<Sa>(entity =>
            {
                entity.ToTable("SA");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(50)
                    .HasColumnName("twitter");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Sas)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_status");

            });

            modelBuilder.Entity<SaTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SA_transaction");

                entity.Property(e => e.BcrId).HasColumnName("BCR_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductsNumber).HasColumnName("products_number");

                entity.Property(e => e.SaId).HasColumnName("SA_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TransactionStatusId).HasColumnName("transaction_status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Bcr)
                    .WithMany()
                    .HasForeignKey(d => d.BcrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_transaction_BCR");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_transaction_product");

                entity.HasOne(d => d.Sa)
                    .WithMany()
                    .HasForeignKey(d => d.SaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_transaction_SA");

                entity.HasOne(d => d.Status)
                    .WithMany()
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_transaction_status");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany()
                    .HasForeignKey(d => d.TransactionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SA_transaction_transaction_status");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status1).HasColumnName("status");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.ToTable("transaction_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TransactionStatus1)
                    .HasMaxLength(50)
                    .HasColumnName("transaction_status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .HasMaxLength(25)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_status");
            });

            modelBuilder.Entity<SaToCampaign>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sa_to_campaign");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.SocialActivistId).HasColumnName("social_activist_id");

                entity.HasOne(d => d.Campaign)
                    .WithMany()
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_to_campaign_campaign");

                entity.HasOne(d => d.Status)
                    .WithMany()
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_to_campaign_status");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.SocialActivistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_to_campaign_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
