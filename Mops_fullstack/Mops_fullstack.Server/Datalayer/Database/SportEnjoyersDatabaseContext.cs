using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Datalayer.Database;

public partial class SportEnjoyersDatabaseContext : DbContext
{
    public SportEnjoyersDatabaseContext()
    {
    }

    public SportEnjoyersDatabaseContext(DbContextOptions<SportEnjoyersDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    // public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Field>(entity =>
        {
            entity.ToTable("Field");

            entity.HasIndex(e => e.AreaOwnerId, "IX_Field_AreaOwnerId");

            entity.HasOne(d => d.AreaOwner).WithMany(p => p.Fields).HasForeignKey(d => d.AreaOwnerId);
        });*/

        modelBuilder.Entity<Field>(entity =>
        {
            entity.ToTable("Field");

            entity.HasIndex(e => e.OwnerId, "IX_Field_OwnerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.Fields).HasForeignKey(d => d.OwnerId);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasMany(d => d.Players).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupPlayer",
                    r => r.HasOne<Player>().WithMany().HasForeignKey("PlayersId").OnDelete(DeleteBehavior.NoAction),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupsId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("GroupsId", "PlayersId");
                        j.ToTable("GroupPlayer");
                        j.HasIndex(new[] { "PlayersId" }, "IX_GroupPlayer_PlayersId");
                    });

            entity.HasMany(d => d.PlayerRequests).WithMany(p => p.GroupRequests)
                .UsingEntity<Dictionary<string, object>>(
                    "JoinRequests",
                    r => r.HasOne<Player>().WithMany().HasForeignKey("PlayerId").OnDelete(DeleteBehavior.NoAction),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("GroupId", "PlayerId");
                        j.ToTable("JoinRequests");
                        j.HasIndex(new[] { "PlayerId" }, "IX_JoinRequests_PlayerId");
                    });
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasIndex(e => e.OwnerId, "IX_Groups_OrganizerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.GroupsOwned).HasForeignKey(d => d.OwnerId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(e => e.GroupId, "IX_Matches_GroupId");

            entity.HasOne(d => d.Group).WithMany(p => p.Matches).HasForeignKey(d => d.GroupId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(e => e.FieldId, "IX_Matches_FieldId");

            entity.HasOne(d => d.Field).WithMany(p => p.Matches).HasForeignKey(d => d.FieldId);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.HasIndex(e => e.ThreadId, "IX_Message_ThreadId");

            entity.HasOne(d => d.Thread).WithMany(p => p.Messages).HasForeignKey(d => d.ThreadId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.HasIndex(e => e.PlayerId, "IX_Message_PlayerId");

            entity.HasOne(d => d.Player).WithMany(p => p.Messages).HasForeignKey(d => d.PlayerId);
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.HasIndex(e => e.GroupId, "IX_Threads_GroupId");

            entity.HasOne(d => d.Group).WithMany(p => p.Threads).HasForeignKey(d => d.GroupId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified
            ));

        foreach (var entry in entries)
        {
            ((BaseEntity)entry.Entity).DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified
            ));

        foreach (var entry in entries)
        {
            ((BaseEntity)entry.Entity).DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
public static class DbSetExtensions
{
    public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
    {
        var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
        var serviceProvider = infrastructure.Instance;
        var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
        return currentDbContext.Context;
    }
}
