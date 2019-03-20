﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Npgsql.Bulk.DAL
{
    public class BulkContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Address2> Addresses2 { get; set; }

        public BulkContext(DbContextOptions<BulkContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().Property(x => x.CreatedAt)
                .HasValueGenerator<ValueGen>().ValueGeneratedOnAdd();

        }
    }

    public class ValueGen : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry)
        {
            return DateTime.Now;
        }
    }
}
