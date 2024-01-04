﻿// <auto-generated />
using System;
using CallCenterApi.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CallCenterApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CallCenterApi.Infrastructure.DB.Entities.AgentInfo", b =>
                {
                    b.Property<Guid>("AgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AgentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AgentState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid[]>("QueueIds")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<DateTime>("TimestampUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AgentId");

                    b.ToTable("AgentInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
