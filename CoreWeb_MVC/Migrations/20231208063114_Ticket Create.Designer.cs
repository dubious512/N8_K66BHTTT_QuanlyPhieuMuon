﻿// <auto-generated />
using System;
using CoreWeb_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreWeb_MVC.Migrations
{
    [DbContext(typeof(TicketDbContext))]
    [Migration("20231208063114_Ticket Create")]
    partial class TicketCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreWeb_MVC.Models.Ticket", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("book_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ticket_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("tickets");
                });
#pragma warning restore 612, 618
        }
    }
}