using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Invoice.Infrastructure.Entities
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ViewUnifiedInvoice> ViewUnifiedInvoices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=dbview.sca.local/viewdb;User Id=hosp;Password=h4j3xzn7;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HOSP")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<ViewUnifiedInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VIEW_UNIFIED_INVOICES");

                entity.Property(e => e.AgencyNumber)
                    .HasPrecision(5)
                    .HasColumnName("AGENCY_NUMBER");

                entity.Property(e => e.Currency)
                    .HasPrecision(3)
                    .HasColumnName("CURRENCY");

                entity.Property(e => e.Dl)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DL");

                entity.Property(e => e.GrossTonnage)
                    .HasColumnType("NUMBER(13,3)")
                    .HasColumnName("GROSS_TONNAGE");

                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .HasColumnName("ID");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INVOICE_DATE");

                entity.Property(e => e.InvoiceNumber)
                    .HasPrecision(15)
                    .HasColumnName("INVOICE_NUMBER");

                entity.Property(e => e.Le)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LE");

                entity.Property(e => e.NetTonnage)
                    .HasColumnType("NUMBER(13,3)")
                    .HasColumnName("NET_TONNAGE");

                entity.Property(e => e.TravelDate)
                    .HasColumnType("DATE")
                    .HasColumnName("TRAVEL_DATE");

                entity.Property(e => e.VesselImo)
                    .HasPrecision(10)
                    .HasColumnName("VESSEL_IMO");

                entity.Property(e => e.VesselName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VESSEL_NAME");
            });

            modelBuilder.HasSequence("MESSAGE_NEXT");

            modelBuilder.HasSequence("USER_PERSONAL_CARDS_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
