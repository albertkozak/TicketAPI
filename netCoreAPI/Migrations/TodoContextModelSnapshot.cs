﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using netCoreAPI.Services;

namespace netCoreAPI.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("netCoreAPI.Services.ToDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ToDos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2020, 1, 27, 14, 30, 48, 109, DateTimeKind.Local).AddTicks(6554),
                            Description = "Clean house",
                            IsComplete = false,
                            Priority = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2020, 1, 27, 14, 30, 48, 113, DateTimeKind.Local).AddTicks(568),
                            Description = "Bake cake",
                            IsComplete = false,
                            Priority = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
