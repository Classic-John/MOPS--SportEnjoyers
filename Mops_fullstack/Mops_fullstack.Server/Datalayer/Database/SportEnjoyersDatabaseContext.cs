using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Field>(entity =>
        {
            entity.ToTable("Field");

            entity.HasIndex(e => e.AreaOwnerId, "IX_Field_AreaOwnerId");

            entity.HasOne(d => d.AreaOwner).WithMany(p => p.Fields).HasForeignKey(d => d.AreaOwnerId);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasMany(d => d.Players).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupPlayer",
                    r => r.HasOne<Player>().WithMany().HasForeignKey("PlayersId"),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                    j =>
                    {
                        j.HasKey("GroupsId", "PlayersId");
                        j.ToTable("GroupPlayer");
                        j.HasIndex(new[] { "PlayersId" }, "IX_GroupPlayer_PlayersId");
                    });
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(e => e.AssociatedGroupId, "IX_Matches_AssociatedGroupId");

            entity.HasOne(d => d.AssociatedGroup).WithMany(p => p.Matches).HasForeignKey(d => d.AssociatedGroupId);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.HasIndex(e => e.AssociatedThreadId, "IX_Message_AssociatedThreadId");

            entity.HasOne(d => d.AssociatedThread).WithMany(p => p.Messages).HasForeignKey(d => d.AssociatedThreadId);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasIndex(e => e.AreaOwnerId, "IX_Players_AreaOwnerId");

            entity.HasOne(d => d.AreaOwner).WithMany(p => p.Players).HasForeignKey(d => d.AreaOwnerId);
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.HasIndex(e => e.AssociatedGroupId, "IX_Threads_AssociatedGroupId");

            entity.HasOne(d => d.AssociatedGroup).WithMany(p => p.Threads).HasForeignKey(d => d.AssociatedGroupId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
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
