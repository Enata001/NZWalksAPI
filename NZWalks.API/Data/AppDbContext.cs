﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //seeding data for difficulties, easy, medium  and hard
        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("ad34cc98-cb5e-4e1b-b268-406f9e0438b5"),
                Name = "Easy",
            },
            new Difficulty()
            {
                Id = Guid.Parse("e5cf8492-58b7-402a-b732-56aad9736732"),
                Name = "Medium",
            },
            new Difficulty()
            {
                Id = Guid.Parse("6769ad84-ac1c-4124-9f17-f20d7bf8345e"),
                Name = "Hard",
            },
        };

        //seeding difficulties to the database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);


        var regions = new List<Region>
        {
            new Region
            {
                Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                Name = "Auckland",
                Code = "AKL",
                ImageUrl =
                    "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "Northland",
                Code = "NTL",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Bay Of Plenty",
                Code = "BOP",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "Wellington",
                Code = "WGN",
                ImageUrl =
                    "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Nelson",
                Code = "NSN",
                ImageUrl =
                    "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Southland",
                Code = "STL",
                ImageUrl = null
            },
        };

        modelBuilder.Entity<Region>().HasData(regions);

        var viewerRoleId = "a3fd847f-012c-4465-aac0-4116695ff1b8";
        var adminRoleId = "379d42af-628c-4d71-92f8-ff63f3c7999b";

        var roles = new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Id = viewerRoleId, ConcurrencyStamp = viewerRoleId, Name = "Viewer", NormalizedName = "Viewer".ToUpper()
            },
            new IdentityRole()
                { Id = adminRoleId, ConcurrencyStamp = adminRoleId, Name = "Admin", NormalizedName = "Admin".ToUpper() }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}