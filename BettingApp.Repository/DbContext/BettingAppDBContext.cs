using BettingApp.DAL.DbEntities;
using BettingApp.DAL.IdentityAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace BettingApp.DAL.DbContexts
{
    public partial class BettingAppDBContext : IdentityDbContext<ApplicationUser>
    {
     

        public BettingAppDBContext(DbContextOptions<BettingAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<Fixture> Fixtures { get; set; }
        public virtual DbSet<FixtureDetail> FixtureDetails { get; set; }
        public virtual DbSet<LookupPlayer> LookupPlayers { get; set; }
        public virtual DbSet<LookupSport> LookupSports { get; set; }
        public virtual DbSet<LookupStadium> LookupStadia { get; set; }
        public virtual DbSet<LookupTeam> LookupTeams { get; set; }
        public virtual DbSet<Market> Markets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Competitor>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FixtureDetails)
                    .WithMany(p => p.Competitors)
                    .HasForeignKey(d => d.FixtureDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitors_FixtureDetails");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Competitors)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitors_LookupPlayer");
            });

            modelBuilder.Entity<Fixture>(entity =>
            {
                entity.ToTable("Fixture");

                entity.HasIndex(e => e.WinnerId, "IX_Fixture_WinnerId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Winner)
                    .WithMany(p => p.Fixtures)
                    .HasForeignKey(d => d.WinnerId)
                    .HasConstraintName("FK_Fixture_Market");
            });

            modelBuilder.Entity<FixtureDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduledBegin).HasColumnType("datetime");

                entity.HasOne(d => d.Fixture)
                    .WithMany(p => p.FixtureDetails)
                    .HasForeignKey(d => d.FixtureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixtureDetails_Fixture");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.FixtureDetails)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixtureDetails_LookupStadium");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.FixtureDetails)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixtureDetails_LookupSport");
            });

            modelBuilder.Entity<LookupPlayer>(entity =>
            {
                entity.ToTable("LookupPlayer");

                entity.HasIndex(e => e.TeamId, "IX_LookupPlayer_TeamId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.LookupPlayers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupPlayer_LookupTeam");
            });

            modelBuilder.Entity<LookupSport>(entity =>
            {
                entity.ToTable("LookupSport");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Sports)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LookupStadium>(entity =>
            {
                entity.ToTable("LookupStadium");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LookupTeam>(entity =>
            {
                entity.ToTable("LookupTeam");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.ToTable("Market");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            //OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
