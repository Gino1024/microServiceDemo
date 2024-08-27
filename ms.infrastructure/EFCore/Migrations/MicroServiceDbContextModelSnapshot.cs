﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ms.infrastructure.EFCore.Migrations
{
    [DbContext(typeof(MicroServiceDbContext))]
    partial class MicroServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TFuncGroup", b =>
                {
                    b.Property<int>("func_group_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("func_group_id"));

                    b.Property<DateTime>("create_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("func_group_id");

                    b.ToTable("TFunctionGroup");
                });

            modelBuilder.Entity("TFuncGroupRel", b =>
                {
                    b.Property<int>("func_id")
                        .HasColumnType("integer");

                    b.Property<int>("func_group_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("create_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("func_id", "func_group_id");

                    b.HasIndex("func_group_id");

                    b.ToTable("TFuncGroupRel");
                });

            modelBuilder.Entity("TFunction", b =>
                {
                    b.Property<int>("func_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("func_id"));

                    b.Property<DateTime>("create_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("func_id");

                    b.ToTable("TFunction");
                });

            modelBuilder.Entity("TUser", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("user_id"));

                    b.Property<DateTime>("create_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("is_enable")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("last_login_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("mima")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("mima_change_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("user_id");

                    b.ToTable("TUser");
                });

            modelBuilder.Entity("TUserFuncGroupRel", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("integer");

                    b.Property<int>("func_group_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("create_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("user_id", "func_group_id");

                    b.ToTable("TUserFuncGroupRel");
                });

            modelBuilder.Entity("TFuncGroupRel", b =>
                {
                    b.HasOne("TFuncGroup", "func_group")
                        .WithMany("func_group_rels")
                        .HasForeignKey("func_group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFunction", "func")
                        .WithMany("func_group_rels")
                        .HasForeignKey("func_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("func");

                    b.Navigation("func_group");
                });

            modelBuilder.Entity("TUserFuncGroupRel", b =>
                {
                    b.HasOne("TFuncGroup", "func_group")
                        .WithMany("user_func_group_rels")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TUser", "user")
                        .WithMany("user_func_group_rels")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("func_group");

                    b.Navigation("user");
                });

            modelBuilder.Entity("TFuncGroup", b =>
                {
                    b.Navigation("func_group_rels");

                    b.Navigation("user_func_group_rels");
                });

            modelBuilder.Entity("TFunction", b =>
                {
                    b.Navigation("func_group_rels");
                });

            modelBuilder.Entity("TUser", b =>
                {
                    b.Navigation("user_func_group_rels");
                });
#pragma warning restore 612, 618
        }
    }
}
