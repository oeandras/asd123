﻿// <auto-generated />
using Asd123.Repository.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Asd13.Repository.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171113215521_Tags")]
    partial class Tags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Asd123.Domain.ImageInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUri");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<Guid?>("UploadedById");

                    b.HasKey("Id");

                    b.HasIndex("UploadedById");

                    b.ToTable("ImageInfos");
                });

            modelBuilder.Entity("Asd123.Domain.PictureTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<Guid?>("ImageId");

                    b.Property<Guid?>("TagId");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("TagId");

                    b.ToTable("PictureTag");
                });

            modelBuilder.Entity("Asd123.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Text");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Asd123.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<string>("Hometown");

                    b.Property<string>("Locale");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<string>("UserIdentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Asd123.Domain.ImageInfo", b =>
                {
                    b.HasOne("Asd123.Domain.User", "UploadedBy")
                        .WithMany("UploadedImages")
                        .HasForeignKey("UploadedById");
                });

            modelBuilder.Entity("Asd123.Domain.PictureTag", b =>
                {
                    b.HasOne("Asd123.Domain.ImageInfo", "Image")
                        .WithMany("PictureTags")
                        .HasForeignKey("ImageId");

                    b.HasOne("Asd123.Domain.Tag", "Tag")
                        .WithMany("PictureTags")
                        .HasForeignKey("TagId");
                });
#pragma warning restore 612, 618
        }
    }
}