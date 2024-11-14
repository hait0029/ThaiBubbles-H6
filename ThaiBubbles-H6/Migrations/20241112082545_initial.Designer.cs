﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThaiBubbles_H6.Database;

#nullable disable

namespace ThaiBubbles_H6.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241112082545_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThaiBubbles_H6.Model.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityID"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZIPCode")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Favorite", b =>
                {
                    b.Property<int>("FavoriteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteID"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteID");

                    b.HasIndex("UserId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Login", b =>
                {
                    b.Property<int>("LoginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("LoginID");

                    b.HasIndex("RoleId");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.ProductList", b =>
                {
                    b.Property<int>("ProductOrderListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductOrderListID"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("ProductOrderListID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductList");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FavoriteId")
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNr")
                        .HasColumnType("int");

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("CityId");

                    b.HasIndex("RoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Favorite", b =>
                {
                    b.HasOne("ThaiBubbles_H6.Model.User", "UserFK")
                        .WithMany("FavoriteFk")
                        .HasForeignKey("UserId");

                    b.Navigation("UserFK");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Login", b =>
                {
                    b.HasOne("ThaiBubbles_H6.Model.Role", "RoleType")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("RoleType");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.ProductList", b =>
                {
                    b.HasOne("ThaiBubbles_h6.Model.Order", "Orders")
                        .WithMany("orderlists")
                        .HasForeignKey("OrderId");

                    b.HasOne("ThaiBubbles_h6.Model.Product", "Products")
                        .WithMany("orderlists")
                        .HasForeignKey("ProductId");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.User", b =>
                {
                    b.HasOne("ThaiBubbles_H6.Model.City", "Cities")
                        .WithMany("Users")
                        .HasForeignKey("CityId");

                    b.HasOne("ThaiBubbles_H6.Model.Role", "Role")
                        .WithMany("UserFk")
                        .HasForeignKey("RoleID");

                    b.Navigation("Cities");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Order", b =>
                {
                    b.HasOne("ThaiBubbles_H6.Model.User", "user")
                        .WithMany("Order")
                        .HasForeignKey("UserId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Product", b =>
                {
                    b.HasOne("ThaiBubbles_h6.Model.Category", "category")
                        .WithMany("product")
                        .HasForeignKey("CategoryId");

                    b.Navigation("category");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.Role", b =>
                {
                    b.Navigation("UserFk");
                });

            modelBuilder.Entity("ThaiBubbles_H6.Model.User", b =>
                {
                    b.Navigation("FavoriteFk");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Category", b =>
                {
                    b.Navigation("product");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Order", b =>
                {
                    b.Navigation("orderlists");
                });

            modelBuilder.Entity("ThaiBubbles_h6.Model.Product", b =>
                {
                    b.Navigation("orderlists");
                });
#pragma warning restore 612, 618
        }
    }
}
