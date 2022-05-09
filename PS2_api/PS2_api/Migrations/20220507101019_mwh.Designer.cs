﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PS2_api.DataBase;

#nullable disable

namespace PS2_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220507101019_mwh")]
    partial class mwh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PS2_api.Models.Power", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("mWh")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Powers");
                });

            modelBuilder.Entity("PS2_api.Models.PowerLive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("livePower")
                        .HasColumnType("real");

                    b.Property<float>("totalPower")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("PowerLives");
                });

            modelBuilder.Entity("PS2_api.Models.Waypoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("LR")
                        .HasColumnType("integer");

                    b.Property<int>("TD")
                        .HasColumnType("integer");

                    b.Property<DateTime>("positionTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Waypoints");
                });
#pragma warning restore 612, 618
        }
    }
}
