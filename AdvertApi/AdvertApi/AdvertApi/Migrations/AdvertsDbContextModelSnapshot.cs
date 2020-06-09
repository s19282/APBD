﻿// <auto-generated />
using System;
using AdvertApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvertApi.Migrations
{
    [DbContext(typeof(AdvertsDbContext))]
    partial class AdvertsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvertApi.Model.Building", b =>
                {
                    b.Property<int>("IdBuilding")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.HasKey("IdBuilding");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("AdvertApi.Models.Banner", b =>
                {
                    b.Property<int>("IdAdvertisment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("CampaignIdCampaign")
                        .HasColumnType("int");

                    b.Property<int?>("IdCampaign")
                        .HasColumnType("int")
                        .HasAnnotation("ForeignKey", "IdCampaign");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("IdAdvertisment");

                    b.HasIndex("CampaignIdCampaign");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("AdvertApi.Models.Campaign", b =>
                {
                    b.Property<int>("IdCampaign")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientIdClient")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<int?>("FBulidlingIdBuilding")
                        .HasColumnType("int");

                    b.Property<int?>("FromIdBuilding")
                        .HasColumnType("int");

                    b.Property<int?>("IdClient")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePreSquareMeter")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.Property<int?>("TBulidlingIdBuilding")
                        .HasColumnType("int");

                    b.Property<int?>("ToIdBuilding")
                        .HasColumnType("int");

                    b.HasKey("IdCampaign");

                    b.HasIndex("ClientIdClient");

                    b.HasIndex("FBulidlingIdBuilding");

                    b.HasIndex("TBulidlingIdBuilding");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("AdvertApi.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100)
                        .HasAnnotation("EmailAddress", "");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AdvertApi.Models.Banner", b =>
                {
                    b.HasOne("AdvertApi.Models.Campaign", "Campaign")
                        .WithMany("Banners")
                        .HasForeignKey("CampaignIdCampaign");
                });

            modelBuilder.Entity("AdvertApi.Models.Campaign", b =>
                {
                    b.HasOne("AdvertApi.Models.Client", "Client")
                        .WithMany("Campaigns")
                        .HasForeignKey("ClientIdClient");

                    b.HasOne("AdvertApi.Model.Building", "FBulidling")
                        .WithMany("CampaignsFrom")
                        .HasForeignKey("FBulidlingIdBuilding");

                    b.HasOne("AdvertApi.Model.Building", "TBulidling")
                        .WithMany("CampaignsTo")
                        .HasForeignKey("TBulidlingIdBuilding");
                });
#pragma warning restore 612, 618
        }
    }
}