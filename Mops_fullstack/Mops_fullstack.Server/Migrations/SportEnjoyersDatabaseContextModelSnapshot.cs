﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mops_fullstack.Server.Datalayer.Database;

#nullable disable

namespace Mops_fullstack.Server.Migrations
{
    [DbContext(typeof(SportEnjoyersDatabaseContext))]
    partial class SportEnjoyersDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupPlayer", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "PlayersId");

                    b.HasIndex(new[] { "PlayersId" }, "IX_GroupPlayer_PlayersId");

                    b.ToTable("GroupPlayer", (string)null);
                });

            modelBuilder.Entity("JoinRequests", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "PlayerId");

                    b.HasIndex(new[] { "PlayerId" }, "IX_JoinRequests_PlayerId");

                    b.ToTable("JoinRequests", (string)null);
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "OwnerId" }, "IX_Field_OwnerId");

                    b.ToTable("Field", (string)null);
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "OwnerId" }, "IX_Groups_OrganizerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "FieldId" }, "IX_Matches_FieldId");

                    b.HasIndex(new[] { "GroupId" }, "IX_Matches_GroupId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PlayerId" }, "IX_Message_PlayerId");

                    b.HasIndex(new[] { "ThreadId" }, "IX_Message_ThreadId");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Thread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "GroupId" }, "IX_Threads_GroupId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("GroupPlayer", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("JoinRequests", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Field", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Player", "Owner")
                        .WithMany("Fields")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Group", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Player", "Owner")
                        .WithMany("GroupsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Match", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Field", "Field")
                        .WithMany("Matches")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Group", "Group")
                        .WithMany("Matches")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Message", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Player", "Player")
                        .WithMany("Messages")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Thread", "Thread")
                        .WithMany("Messages")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Thread", b =>
                {
                    b.HasOne("Mops_fullstack.Server.Datalayer.Models.Group", "Group")
                        .WithMany("Threads")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Field", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Group", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Threads");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Player", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("GroupsOwned");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Mops_fullstack.Server.Datalayer.Models.Thread", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
