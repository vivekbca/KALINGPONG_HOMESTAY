using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KLMPNHomeStay.Entities
{
    public partial class klmpnhomestay_dbContext : DbContext
    {
        public klmpnhomestay_dbContext()
        {
        }

        public klmpnhomestay_dbContext(DbContextOptions<klmpnhomestay_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TmBlock> TmBlock { get; set; }
        public virtual DbSet<TmBlockVillage> TmBlockVillage { get; set; }
        public virtual DbSet<TmCountry> TmCountry { get; set; }
        public virtual DbSet<TmDistrict> TmDistrict { get; set; }
        public virtual DbSet<TmFinancialYear> TmFinancialYear { get; set; }
        public virtual DbSet<TmGuestUser> TmGuestUser { get; set; }
        public virtual DbSet<TmHomestay> TmHomestay { get; set; }
        public virtual DbSet<TmHsFacilities> TmHsFacilities { get; set; }
        public virtual DbSet<TmHsGallery> TmHsGallery { get; set; }
        public virtual DbSet<TmHsRoomCategory> TmHsRoomCategory { get; set; }
        public virtual DbSet<TmHsRooms> TmHsRooms { get; set; }
        public virtual DbSet<TmMarquee> TmMarquee { get; set; }
        public virtual DbSet<TmNotice> TmNotice { get; set; }
        public virtual DbSet<TmPermission> TmPermission { get; set; }
        public virtual DbSet<TmPopulardestination> TmPopulardestination { get; set; }
        public virtual DbSet<TmState> TmState { get; set; }
        public virtual DbSet<TmTender> TmTender { get; set; }
        public virtual DbSet<TmTour> TmTour { get; set; }
        public virtual DbSet<TmUser> TmUser { get; set; }
        public virtual DbSet<TmUserRole> TmUserRole { get; set; }
        public virtual DbSet<TmVillageCategory> TmVillageCategory { get; set; }
        public virtual DbSet<TtBankTransaction> TtBankTransaction { get; set; }
        public virtual DbSet<TtBooking> TtBooking { get; set; }
        public virtual DbSet<TtBookingRoomDetail> TtBookingRoomDetail { get; set; }
        public virtual DbSet<TtHsFeedback> TtHsFeedback { get; set; }
        public virtual DbSet<TtHsPopularity> TtHsPopularity { get; set; }
        public virtual DbSet<TtPackageFeedback> TtPackageFeedback { get; set; }
        public virtual DbSet<TtRolePermissionMap> TtRolePermissionMap { get; set; }
        public virtual DbSet<TtTourBooking> TtTourBooking { get; set; }
        public virtual DbSet<TtTourBookingDetail> TtTourBookingDetail { get; set; }
        public virtual DbSet<TtTourDate> TtTourDate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TmBlock>(entity =>
            {
                entity.HasKey(e => e.BlockId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_block");

                entity.HasIndex(e => e.BlockCode)
                    .HasName("BlockCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.BlockName)
                    .HasName("BlockName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_tmBlock_Country_Id_Idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmBlock_Created_By_Idx");

                entity.HasIndex(e => e.DistrictId)
                    .HasName("FK_tmBlock_District_Id_Idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmBlock_Modified_By_Idx");

                entity.HasIndex(e => e.StateId)
                    .HasName("FK_tmBlock_State_Id_Idx");

                entity.Property(e => e.BlockId)
                    .HasColumnName("Block_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.BlockCode)
                    .HasColumnName("Block_Code")
                    .HasColumnType("int(10)");

                entity.Property(e => e.BlockName)
                    .IsRequired()
                    .HasColumnName("Block_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.DistrictId)
                    .IsRequired()
                    .HasColumnName("District_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .HasColumnName("State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TmBlock)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlock_tmCountry");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmBlockCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmBlock_tmUser");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TmBlock)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlock_tmDistrict");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmBlockModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmBlock_tmUser");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TmBlock)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlock_tmState");
            });

            modelBuilder.Entity<TmBlockVillage>(entity =>
            {
                entity.HasKey(e => e.VillId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_block_village");

                entity.HasIndex(e => e.BlockId)
                    .HasName("FK_tmBlockVillage_Block_Id_Idx");

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_tmBlockVillage_Country_Id_Idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmBlockVillage_Created_By_Idx");

                entity.HasIndex(e => e.DistrictId)
                    .HasName("FK_tmBlockVillage_District_Id_Idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmBlockVillage_Modified_By_Idx");

                entity.HasIndex(e => e.StateId)
                    .HasName("FK_tmBlockVillage_State_Id_Idx");

                entity.HasIndex(e => e.VillCode)
                    .HasName("VillCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.VillName)
                    .HasName("VillName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.VillId)
                    .HasColumnName("Vill_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.BlockId)
                    .IsRequired()
                    .HasColumnName("Block_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.DistrictId)
                    .IsRequired()
                    .HasColumnName("District_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .HasColumnName("State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.VillCode)
                    .HasColumnName("Vill_Code")
                    .HasColumnType("int(10)");

                entity.Property(e => e.VillImage)
                    .HasColumnName("Vill_Image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VillName)
                    .IsRequired()
                    .HasColumnName("Vill_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.TmBlockVillage)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlockVillage_tmBlock");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TmBlockVillage)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlockVillage_tmCountry");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmBlockVillageCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmBlockVillage_tmUser");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TmBlockVillage)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlockVillage_tmDistrict");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmBlockVillageModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmBlockVillage_tmUser");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TmBlockVillage)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmBlockVillage_tmState");
            });

            modelBuilder.Entity<TmCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_country");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("CountryCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CountryName)
                    .HasName("CountryName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmCountry_Created_By_Idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmCountry_Modified_By_Idx");

                entity.Property(e => e.CountryId)
                    .HasColumnName("Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CountryName)
                    .HasColumnName("Country_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmCountryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmCountry_tmUser");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmCountryModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmCountry_tmUser");
            });

            modelBuilder.Entity<TmDistrict>(entity =>
            {
                entity.HasKey(e => e.DistrictId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_district");

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_tmDistrict_Country_Id_Idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmDistrict_Created_By_Idx");

                entity.HasIndex(e => e.DistrictCode)
                    .HasName("DistrictCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.DistrictName)
                    .HasName("DistrictName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmDistrict_Modified_By_Idx");

                entity.HasIndex(e => e.StateId)
                    .HasName("FK_tmDistrict_State_Id_Idx");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("District_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("District_Code")
                    .HasColumnType("int(10)");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasColumnName("District_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .HasColumnName("State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TmDistrict)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmDistrict_tmCountry");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmDistrictCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmDistrict_tmUser");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmDistrictModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmDistrict_tmUser");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TmDistrict)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmDistrict_tmState");
            });

            modelBuilder.Entity<TmFinancialYear>(entity =>
            {
                entity.HasKey(e => e.FinancialYearId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_financial_year");

                entity.Property(e => e.FinancialYearId)
                    .HasColumnName("Financial_Year_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FinancialYear)
                    .IsRequired()
                    .HasColumnName("Financial_Year")
                    .HasMaxLength(9)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TmGuestUser>(entity =>
            {
                entity.HasKey(e => e.GuId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_guest_user");

                entity.HasIndex(e => e.GuCountryId)
                    .HasName("FK_tm_GuestUser_CountryId_idx");

                entity.HasIndex(e => e.GuEmailId)
                    .HasName("GU_Email_Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.GuStateId)
                    .HasName("FK_tm_GuestUser_StateId_idx");

                entity.HasIndex(e => e.ResetToken)
                    .HasName("Reset_Token_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.GuId)
                    .HasColumnName("GU_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.GuAddress1)
                    .IsRequired()
                    .HasColumnName("GU_Address1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuAddress2)
                    .IsRequired()
                    .HasColumnName("GU_Address2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuAddress3)
                    .HasColumnName("GU_Address3")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuCity)
                    .IsRequired()
                    .HasColumnName("GU_City")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.GuCountryId)
                    .IsRequired()
                    .HasColumnName("GU_Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.GuCreatedOn).HasColumnName("GU_Created_On");

                entity.Property(e => e.GuDob)
                    .HasColumnName("GU_DOB")
                    .HasColumnType("date");

                entity.Property(e => e.GuEmailId)
                    .IsRequired()
                    .HasColumnName("GU_Email_Id")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.GuIdentityNo)
                    .IsRequired()
                    .HasColumnName("GU_IdentityNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GuIdentityProof)
                    .IsRequired()
                    .HasColumnName("GU_Identity_Proof")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuIsActive)
                    .HasColumnName("GU_IsActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.GuLastActivity).HasColumnName("GU_Last_Activity");

                entity.Property(e => e.GuMobileNo)
                    .IsRequired()
                    .HasColumnName("GU_Mobile_No")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GuName)
                    .IsRequired()
                    .HasColumnName("GU_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuPassword)
                    .IsRequired()
                    .HasColumnName("GU_Password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuPincode)
                    .IsRequired()
                    .HasColumnName("GU_Pincode")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.GuSex)
                    .IsRequired()
                    .HasColumnName("GU_Sex")
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.GuStateId)
                    .IsRequired()
                    .HasColumnName("GU_State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ResetToken)
                    .HasColumnName("Reset_Token")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResetTokenExpires).HasColumnName("Reset_Token_Expires");

                entity.HasOne(d => d.GuCountry)
                    .WithMany(p => p.TmGuestUser)
                    .HasForeignKey(d => d.GuCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tm_GuestUser_CountryId");

                entity.HasOne(d => d.GuState)
                    .WithMany(p => p.TmGuestUser)
                    .HasForeignKey(d => d.GuStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tm_GuestUser_StateId");
            });

            modelBuilder.Entity<TmHomestay>(entity =>
            {
                entity.HasKey(e => e.HsId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_homestay");

                entity.HasIndex(e => e.ApprovedBy)
                    .HasName("FK_tmHomestay_Approved_By_Idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmHomestay_Created_By_Idx");

                entity.HasIndex(e => e.DeactivatedBy)
                    .HasName("FK_tmHomestay_Deactivated_By_Idx");

                entity.HasIndex(e => e.DestinationId)
                    .HasName("FK_tmHomestay_tmPopularDestination_idx");

                entity.HasIndex(e => e.HsAccountNo)
                    .HasName("HSAccountNo_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.HsBlockId)
                    .HasName("FK_tmHomestay_HS_Block_Id_Idx");

                entity.HasIndex(e => e.HsCountryId)
                    .HasName("FK_tmHomestay_HS_Country_Id_Idx");

                entity.HasIndex(e => e.HsDistrictId)
                    .HasName("FK_tmHomestay_HS_District_Id_Idx");

                entity.HasIndex(e => e.HsStateId)
                    .HasName("FK_tmHomestay_HS_State_Id_Idx");

                entity.HasIndex(e => e.HsVillId)
                    .HasName("FK_tmHomestay_tmmBlockVilage_idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmHomestay_Modified_By_Idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_tmHomestay_tmuser_idx");

                entity.HasIndex(e => e.VillageCategoryId)
                    .HasName("FK_tmHomestay_tmVillageCategory_idx");

                entity.Property(e => e.HsId)
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ActiveSince)
                    .HasColumnName("Active_Since")
                    .HasColumnType("date");

                entity.Property(e => e.AddonServices)
                    .HasColumnName("Addon_Services")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedBy)
                    .HasColumnName("Approved_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ApprovedOn).HasColumnName("Approved_On");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.DeactivatedBy)
                    .HasColumnName("Deactivated_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.DeactivatedOn)
                    .HasColumnName("Deactivated_On")
                    .HasColumnType("date");

                entity.Property(e => e.DestinationId)
                    .IsRequired()
                    .HasColumnName("Destination_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HomestayDescription)
                    .IsRequired()
                    .HasColumnName("Homestay_Description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.HsAccountNo)
                    .HasColumnName("HS_Account_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HsAccountType)
                    .IsRequired()
                    .HasColumnName("HS_Account_Type")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HsAddress1)
                    .IsRequired()
                    .HasColumnName("HS_Address1")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.HsAddress2)
                    .IsRequired()
                    .HasColumnName("HS_Address2")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.HsAddress3)
                    .HasColumnName("HS_Address3")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.HsBankBranch)
                    .IsRequired()
                    .HasColumnName("HS_Bank_Branch")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HsBankName)
                    .IsRequired()
                    .HasColumnName("HS_Bank_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HsBlockId)
                    .IsRequired()
                    .HasColumnName("HS_Block_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsContactEmail)
                    .IsRequired()
                    .HasColumnName("HS_Contact_Email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HsContactMob1)
                    .IsRequired()
                    .HasColumnName("HS_Contact_Mob1")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HsContactMob2)
                    .HasColumnName("HS_Contact_Mob2")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HsContactName)
                    .IsRequired()
                    .HasColumnName("HS_Contact_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HsCountryId)
                    .IsRequired()
                    .HasColumnName("HS_Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsDistrictId)
                    .IsRequired()
                    .HasColumnName("HS_District_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsIfsc)
                    .IsRequired()
                    .HasColumnName("HS_IFSC")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.HsMicr)
                    .IsRequired()
                    .HasColumnName("HS_MICR")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HsName)
                    .IsRequired()
                    .HasColumnName("HS_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HsNoOfRooms)
                    .HasColumnName("HS_No_of_Rooms")
                    .HasColumnType("int(3)");

                entity.Property(e => e.HsStateId)
                    .IsRequired()
                    .HasColumnName("HS_State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsVillId)
                    .IsRequired()
                    .HasColumnName("HS_Vill_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HwtReach)
                    .IsRequired()
                    .HasColumnName("HWT_Reach")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.LocalAttraction)
                    .IsRequired()
                    .HasColumnName("Local_Attraction")
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("User_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.VillageCategoryId)
                    .IsRequired()
                    .HasColumnName("Village_Category_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.TmHomestayApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK3_tmHomestay_tmUser");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmHomestayCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmHomestay_tmUser");

                entity.HasOne(d => d.DeactivatedByNavigation)
                    .WithMany(p => p.TmHomestayDeactivatedByNavigation)
                    .HasForeignKey(d => d.DeactivatedBy)
                    .HasConstraintName("FK4_tmHomestay_tmUser");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmPopularDestination");

                entity.HasOne(d => d.HsBlock)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.HsBlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmBlock");

                entity.HasOne(d => d.HsCountry)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.HsCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmCountry");

                entity.HasOne(d => d.HsDistrict)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.HsDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmDistrict");

                entity.HasOne(d => d.HsState)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.HsStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmState");

                entity.HasOne(d => d.HsVill)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.HsVillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmmBlockVilage");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmHomestayModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmHomestay_tmUser");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TmHomestayUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmuser");

                entity.HasOne(d => d.VillageCategory)
                    .WithMany(p => p.TmHomestay)
                    .HasForeignKey(d => d.VillageCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHomestay_tmVillageCategory");
            });

            modelBuilder.Entity<TmHsFacilities>(entity =>
            {
                entity.HasKey(e => e.HsFacilityId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_hs_facilities");

                entity.HasIndex(e => e.HsFacilityName)
                    .HasName("HSFacilityName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.HsFacilityId)
                    .HasColumnName("HS_Facility_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FileName)
                    .HasColumnName("File_Name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.HsFacilityName)
                    .IsRequired()
                    .HasColumnName("HS_facility_Name")
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TmHsGallery>(entity =>
            {
                entity.HasKey(e => e.HsId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_hs_gallery");

                entity.HasIndex(e => e.HsId)
                    .HasName("FK_tmHsGallery_HS_Id_Idx");

                entity.Property(e => e.HsId)
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsLi1)
                    .IsRequired()
                    .HasColumnName("HS_LI_1");

                entity.Property(e => e.HsLi10).HasColumnName("HS_LI_10");

                entity.Property(e => e.HsLi2).HasColumnName("HS_LI_2");

                entity.Property(e => e.HsLi3).HasColumnName("HS_LI_3");

                entity.Property(e => e.HsLi4).HasColumnName("HS_LI_4");

                entity.Property(e => e.HsLi5).HasColumnName("HS_LI_5");

                entity.Property(e => e.HsLi6).HasColumnName("HS_LI_6");

                entity.Property(e => e.HsLi7).HasColumnName("HS_LI_7");

                entity.Property(e => e.HsLi8).HasColumnName("HS_LI_8");

                entity.Property(e => e.HsLi9).HasColumnName("HS_LI_9");

                entity.Property(e => e.HsMapLat)
                    .HasColumnName("HS_Map_lat")
                    .HasColumnType("decimal(8,6)");

                entity.Property(e => e.HsMapLong)
                    .HasColumnName("HS_Map_long")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.HsRi1)
                    .IsRequired()
                    .HasColumnName("HS_RI_1");

                entity.Property(e => e.HsRi10).HasColumnName("HS_RI_10");

                entity.Property(e => e.HsRi2).HasColumnName("HS_RI_2");

                entity.Property(e => e.HsRi3).HasColumnName("HS_RI_3");

                entity.Property(e => e.HsRi4).HasColumnName("HS_RI_4");

                entity.Property(e => e.HsRi5).HasColumnName("HS_RI_5");

                entity.Property(e => e.HsRi6).HasColumnName("HS_RI_6");

                entity.Property(e => e.HsRi7).HasColumnName("HS_RI_7");

                entity.Property(e => e.HsRi8).HasColumnName("HS_RI_8");

                entity.Property(e => e.HsRi9).HasColumnName("HS_RI_9");

                entity.HasOne(d => d.Hs)
                    .WithOne(p => p.TmHsGallery)
                    .HasForeignKey<TmHsGallery>(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHsGallery_tmHomestay");
            });

            modelBuilder.Entity<TmHsRoomCategory>(entity =>
            {
                entity.HasKey(e => e.HsCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_hs_room_category");

                entity.HasIndex(e => e.HsCategoryName)
                    .HasName("HSCategoryName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.HsCategoryId)
                    .HasColumnName("HS_Category_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsCategoryName)
                    .IsRequired()
                    .HasColumnName("HS_Category_Name")
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TmHsRooms>(entity =>
            {
                entity.HasKey(e => new { e.HsId, e.HsRoomNo })
                    .HasName("PRIMARY");

                entity.ToTable("tm_hs_rooms");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmHsRooms_Created_By_Idx");

                entity.HasIndex(e => e.HsRoomCategoryId)
                    .HasName("FK_tmHsRooms_HS_Room_Category_Id_Idx");

                entity.HasIndex(e => e.HsRoomFacility1)
                    .HasName("FK_tmHsRooms_tmHsFacilities_1_idx");

                entity.HasIndex(e => e.HsRoomFacility10)
                    .HasName("FK_tmHsRooms_tmHsFacilities_10_idx");

                entity.HasIndex(e => e.HsRoomFacility11)
                    .HasName("FK_tmHsRooms_tmHsFacilities_11_idx");

                entity.HasIndex(e => e.HsRoomFacility12)
                    .HasName("FK_tmHsRooms_tmHsFacilities_12_idx");

                entity.HasIndex(e => e.HsRoomFacility13)
                    .HasName("FK_tmHsRooms_tmHsFacilities_13_idx");

                entity.HasIndex(e => e.HsRoomFacility14)
                    .HasName("FK_tmHsRooms_tmHsFacilities_14_idx");

                entity.HasIndex(e => e.HsRoomFacility15)
                    .HasName("FK_tmHsRooms_tmHsFacilities_15_idx");

                entity.HasIndex(e => e.HsRoomFacility2)
                    .HasName("FK_tmHsRooms_tmHsFacilities_2_idx");

                entity.HasIndex(e => e.HsRoomFacility3)
                    .HasName("FK_tmHsRooms_tmHsFacilities_3_idx");

                entity.HasIndex(e => e.HsRoomFacility4)
                    .HasName("FK_tmHsRooms_tmHsFacilities_4_idx");

                entity.HasIndex(e => e.HsRoomFacility5)
                    .HasName("FK_tmHsRooms_tmHsFacilities_5_idx");

                entity.HasIndex(e => e.HsRoomFacility6)
                    .HasName("FK_tmHsRooms_tmHsFacilities_6_idx");

                entity.HasIndex(e => e.HsRoomFacility7)
                    .HasName("FK_tmHsRooms_tmHsFacilities_7_idx");

                entity.HasIndex(e => e.HsRoomFacility8)
                    .HasName("FK_tmHsRooms_tmHsFacilities_8_idx");

                entity.HasIndex(e => e.HsRoomFacility9)
                    .HasName("FK_tmHsRooms_tmHsFacilities_9_idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK_tmHsRooms_Modified_By_Idx");

                entity.Property(e => e.HsId)
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomNo)
                    .HasColumnName("HS_Room_No")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.HsRoomAvailable)
                    .HasColumnName("HS_Room_Available")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.HsRoomCategoryId)
                    .IsRequired()
                    .HasColumnName("HS_Room_Category_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility1)
                    .IsRequired()
                    .HasColumnName("HS_Room_Facility1")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility10)
                    .HasColumnName("HS_Room_Facility10")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility11)
                    .HasColumnName("HS_Room_Facility11")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility12)
                    .HasColumnName("HS_Room_Facility12")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility13)
                    .HasColumnName("HS_Room_Facility13")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility14)
                    .HasColumnName("HS_Room_Facility14")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility15)
                    .HasColumnName("HS_Room_Facility15")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility2)
                    .HasColumnName("HS_Room_Facility2")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility3)
                    .HasColumnName("HS_Room_Facility3")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility4)
                    .HasColumnName("HS_Room_Facility4")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility5)
                    .HasColumnName("HS_Room_Facility5")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility6)
                    .HasColumnName("HS_Room_Facility6")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility7)
                    .HasColumnName("HS_Room_Facility7")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility8)
                    .HasColumnName("HS_Room_Facility8")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFacility9)
                    .HasColumnName("HS_Room_Facility9")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomFloor)
                    .HasColumnName("HS_Room_Floor")
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.HsRoomImage).HasColumnName("HS_Room_Image");

                entity.Property(e => e.HsRoomNoBeds)
                    .HasColumnName("HS_Room_No_Beds")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.HsRoomRate)
                    .HasColumnName("HS_Room_Rate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HsRoomSize)
                    .HasColumnName("HS_Room_Size")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IsAvailable)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmHsRoomsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmHsRooms_tmUser");

                entity.HasOne(d => d.Hs)
                    .WithMany(p => p.TmHsRooms)
                    .HasForeignKey(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHsRooms_tmHomestay");

                entity.HasOne(d => d.HsRoomCategory)
                    .WithMany(p => p.TmHsRooms)
                    .HasForeignKey(d => d.HsRoomCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHsRooms_tmHsRoomCategory");

                entity.HasOne(d => d.HsRoomFacility1Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility1Navigation)
                    .HasForeignKey(d => d.HsRoomFacility1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_1");

                entity.HasOne(d => d.HsRoomFacility10Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility10Navigation)
                    .HasForeignKey(d => d.HsRoomFacility10)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_10");

                entity.HasOne(d => d.HsRoomFacility11Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility11Navigation)
                    .HasForeignKey(d => d.HsRoomFacility11)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_11");

                entity.HasOne(d => d.HsRoomFacility12Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility12Navigation)
                    .HasForeignKey(d => d.HsRoomFacility12)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_12");

                entity.HasOne(d => d.HsRoomFacility13Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility13Navigation)
                    .HasForeignKey(d => d.HsRoomFacility13)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_13");

                entity.HasOne(d => d.HsRoomFacility14Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility14Navigation)
                    .HasForeignKey(d => d.HsRoomFacility14)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_14");

                entity.HasOne(d => d.HsRoomFacility15Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility15Navigation)
                    .HasForeignKey(d => d.HsRoomFacility15)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_15");

                entity.HasOne(d => d.HsRoomFacility2Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility2Navigation)
                    .HasForeignKey(d => d.HsRoomFacility2)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_2");

                entity.HasOne(d => d.HsRoomFacility3Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility3Navigation)
                    .HasForeignKey(d => d.HsRoomFacility3)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_3");

                entity.HasOne(d => d.HsRoomFacility4Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility4Navigation)
                    .HasForeignKey(d => d.HsRoomFacility4)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_4");

                entity.HasOne(d => d.HsRoomFacility5Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility5Navigation)
                    .HasForeignKey(d => d.HsRoomFacility5)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_5");

                entity.HasOne(d => d.HsRoomFacility6Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility6Navigation)
                    .HasForeignKey(d => d.HsRoomFacility6)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_6");

                entity.HasOne(d => d.HsRoomFacility7Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility7Navigation)
                    .HasForeignKey(d => d.HsRoomFacility7)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_7");

                entity.HasOne(d => d.HsRoomFacility8Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility8Navigation)
                    .HasForeignKey(d => d.HsRoomFacility8)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_8");

                entity.HasOne(d => d.HsRoomFacility9Navigation)
                    .WithMany(p => p.TmHsRoomsHsRoomFacility9Navigation)
                    .HasForeignKey(d => d.HsRoomFacility9)
                    .HasConstraintName("FK_tmHsRooms_tmHsFacilities_9");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmHsRoomsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmHsRooms_tmUser");
            });

            modelBuilder.Entity<TmMarquee>(entity =>
            {
                entity.HasKey(e => e.MarqueeId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_marquee");

                entity.Property(e => e.MarqueeId)
                    .HasColumnName("Marquee_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Heading)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");
            });

            modelBuilder.Entity<TmNotice>(entity =>
            {
                entity.HasKey(e => e.NoticeId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_notice");

                entity.Property(e => e.NoticeId)
                    .HasColumnName("Notice_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FileName)
                    .HasColumnName("File_Name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Heading)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("Is_Deleted")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.PublishingDate)
                    .HasColumnName("Publishing_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_permission");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("Permission_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasColumnName("Permission_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmPopulardestination>(entity =>
            {
                entity.HasKey(e => e.DestinationId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_populardestination");

                entity.HasIndex(e => e.DestinationName)
                    .HasName("BlockCode_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DestinationId)
                    .HasColumnName("Destination_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.DestinationName)
                    .IsRequired()
                    .HasColumnName("Destination_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.PopulardestinationImage)
                    .HasColumnName("Populardestination_Image")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_state");

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_tmState_tmCountry_idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK1_tmState_tmUser_idx");

                entity.HasIndex(e => e.ModifiedBy)
                    .HasName("FK2_tmState_tmUser_idx");

                entity.HasIndex(e => e.StateCode)
                    .HasName("StateCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StateName)
                    .HasName("StateName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StateId)
                    .HasColumnName("State_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("Country_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnName("Modified_On");

                entity.Property(e => e.StateCode)
                    .HasColumnName("State_Code")
                    .HasColumnType("int(10)");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnName("State_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TmState)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmState_tmCountry");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmStateCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_tmState_tmUser");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TmStateModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK2_tmState_tmUser");
            });

            modelBuilder.Entity<TmTender>(entity =>
            {
                entity.HasKey(e => e.TenderId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_tender");

                entity.HasIndex(e => e.FinancialYearId)
                    .HasName("FK_tmTender_tmFinancialYear");

                entity.Property(e => e.TenderId)
                    .HasColumnName("Tender_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ClosingDate).HasColumnType("date");

                entity.Property(e => e.FileName)
                    .HasColumnName("File_Name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FinancialYearId)
                    .IsRequired()
                    .HasColumnName("Financial_Year_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.IsPublished)
                    .HasColumnName("Is_Published")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.MemoNo)
                    .HasColumnName("Memo_No")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.PublishingDate)
                    .HasColumnName("Publishing_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.FinancialYear)
                    .WithMany(p => p.TmTender)
                    .HasForeignKey(d => d.FinancialYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmTender_tmFinancialYear");
            });

            modelBuilder.Entity<TmTour>(entity =>
            {
                entity.ToTable("tm_tour");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_tmTour_tmUser");

                entity.HasIndex(e => e.FacilityId1)
                    .HasName("FK_tmTour_Facility_Id1_Idx");

                entity.HasIndex(e => e.FacilityId2)
                    .HasName("FK_tmTour_Facility_Id2_Idx");

                entity.HasIndex(e => e.FacilityId3)
                    .HasName("FK_tmTour_Facility_Id3_Idx");

                entity.HasIndex(e => e.FacilityId4)
                    .HasName("FK_tmTour_Facility_Id4_Idx");

                entity.HasIndex(e => e.FacilityId5)
                    .HasName("FK_tmTour_Facility_Id5_Idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ContactPersonMobile)
                    .HasColumnName("Contact_Person_Mobile")
                    .HasColumnType("int(10)");

                entity.Property(e => e.ContactPersonName)
                    .HasColumnName("Contact_Person_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonNameEmail)
                    .HasColumnName("Contact_Person_Name_Email")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnName("Created_On");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationDay)
                    .HasColumnName("Destination_Day")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DestinationNight)
                    .HasColumnName("Destination_Night")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FacilityId1)
                    .HasColumnName("Facility_Id1")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FacilityId2)
                    .HasColumnName("Facility_Id2")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FacilityId3)
                    .HasColumnName("Facility_Id3")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FacilityId4)
                    .HasColumnName("Facility_Id4")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FacilityId5)
                    .HasColumnName("Facility_Id5")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.Image1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image5)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TourPdfFile)
                    .HasColumnName("TourPDF_File")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TmTour)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmTour_tmUser");

                entity.HasOne(d => d.FacilityId1Navigation)
                    .WithMany(p => p.TmTourFacilityId1Navigation)
                    .HasForeignKey(d => d.FacilityId1)
                    .HasConstraintName("FK_tmTour_tmHsFacilities1");

                entity.HasOne(d => d.FacilityId2Navigation)
                    .WithMany(p => p.TmTourFacilityId2Navigation)
                    .HasForeignKey(d => d.FacilityId2)
                    .HasConstraintName("FK_tmTour_tmHsFacilities2");

                entity.HasOne(d => d.FacilityId3Navigation)
                    .WithMany(p => p.TmTourFacilityId3Navigation)
                    .HasForeignKey(d => d.FacilityId3)
                    .HasConstraintName("FK_tmTour_tmHsFacilities3");

                entity.HasOne(d => d.FacilityId4Navigation)
                    .WithMany(p => p.TmTourFacilityId4Navigation)
                    .HasForeignKey(d => d.FacilityId4)
                    .HasConstraintName("FK_tmTour_tmHsFacilities4");

                entity.HasOne(d => d.FacilityId5Navigation)
                    .WithMany(p => p.TmTourFacilityId5Navigation)
                    .HasForeignKey(d => d.FacilityId5)
                    .HasConstraintName("FK_tmTour_tmHsFacilities5");
            });

            modelBuilder.Entity<TmUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_user");

                entity.HasIndex(e => e.ResetToken)
                    .HasName("Reset_Token_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserCreatedBy)
                    .HasName("FK_tmUser_User_Id");

                entity.HasIndex(e => e.UserEmailId)
                    .HasName("User_Email_Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UserName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserRoleId)
                    .HasName("FK_tmUser_tmUserRole");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasMaxLength(36)
                    .IsFixedLength()
                    .HasComment("36 character GUID/UUID");

                entity.Property(e => e.InvalidLoginAttempts)
                    .HasColumnName("Invalid_Login_Attempts")
                    .HasColumnType("int(7)");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("Is_System_Defined")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("Last_Login")
                    .HasColumnType("date");

                entity.Property(e => e.LockedOutUntil)
                    .HasColumnName("Locked_Out_Until")
                    .HasColumnType("date");

                entity.Property(e => e.LockoutEnabled)
                    .HasColumnName("Lockout_Enabled")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PasswordLastChanged)
                    .HasColumnName("Password_Last_Changed")
                    .HasColumnType("date");

                entity.Property(e => e.PreviousPasswords)
                    .HasColumnName("Previous_Passwords")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ResetToken)
                    .HasColumnName("Reset_Token")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResetTokenExpires).HasColumnName("Reset_Token_Expires");

                entity.Property(e => e.UserCreatedBy)
                    .IsRequired()
                    .HasColumnName("User_Created_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.UserCreatedOn).HasColumnName("User_Created_On");

                entity.Property(e => e.UserDob)
                    .HasColumnName("User_DOB")
                    .HasColumnType("date");

                entity.Property(e => e.UserEmailId)
                    .IsRequired()
                    .HasColumnName("User_Email_Id")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserIsActive)
                    .HasColumnName("User_IsActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.UserLastActivity).HasColumnName("User_Last_Activity");

                entity.Property(e => e.UserMobileNo)
                    .HasColumnName("User_Mobile_No")
                    .HasColumnType("int(10)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("User_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("User_Password")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleId)
                    .IsRequired()
                    .HasColumnName("User_Role_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.UserSex)
                    .IsRequired()
                    .HasColumnName("User_Sex")
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.UserCreatedByNavigation)
                    .WithMany(p => p.InverseUserCreatedByNavigation)
                    .HasForeignKey(d => d.UserCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmUser_User_Created_By");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.TmUser)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmUser_tmUserRole");
            });

            modelBuilder.Entity<TmUserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_user_role");

                entity.HasIndex(e => e.RoleName)
                    .HasName("Role_Name")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("Role_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("Is_Deleted")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("Is_System_Defined")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.RoleIsActive)
                    .HasColumnName("Role_IsActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("Role_Name")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmVillageCategory>(entity =>
            {
                entity.HasKey(e => e.VillageCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_village_category");

                entity.Property(e => e.VillageCategoryId)
                    .HasColumnName("Village_Category_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.VillageCategoryName)
                    .IsRequired()
                    .HasColumnName("Village_Category_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TtBankTransaction>(entity =>
            {
                entity.HasKey(e => new { e.BookingId, e.TransactionDate, e.TransactionType })
                    .HasName("PRIMARY");

                entity.ToTable("tt_bank_transaction");

                entity.HasIndex(e => e.HsId)
                    .HasName("FK_ttBankTransaction_HS_Id_Idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_ttBankTransaction_User_Id_Idx");

                entity.Property(e => e.BookingId)
                    .HasColumnName("Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.TransactionDate).HasColumnName("Transaction_Date");

                entity.Property(e => e.TransactionType)
                    .HasColumnName("Transaction_Type")
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasDefaultValueSql("''");

                entity.Property(e => e.HsId)
                    .IsRequired()
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.IsMailed).HasColumnType("tinyint(1)");

                entity.Property(e => e.TransAmount)
                    .HasColumnName("Trans_Amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransMode)
                    .IsRequired()
                    .HasColumnName("Trans_Mode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransStatus)
                    .IsRequired()
                    .HasColumnName("Trans_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("User_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.Hs)
                    .WithMany(p => p.TtBankTransaction)
                    .HasForeignKey(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBankTransaction_tmHomestay");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TtBankTransaction)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBankTransaction_tmUser");
            });

            modelBuilder.Entity<TtBooking>(entity =>
            {
                entity.HasKey(e => e.HsBookingId)
                    .HasName("PRIMARY");

                entity.ToTable("tt_booking");

                entity.HasIndex(e => e.BkRefundBy)
                    .HasName("FK_ttBooking_tmUser_idx");

                entity.HasIndex(e => e.CancelledBy)
                    .HasName("FK_ttBooking_cancelBy_idx");

                entity.HasIndex(e => e.GuId)
                    .HasName("FK_ttBooking_GU_Id_Idx");

                entity.HasIndex(e => e.HsId)
                    .HasName("FK_ttBooking_HS_Id_Idx");

                entity.Property(e => e.HsBookingId)
                    .HasColumnName("HS_Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.AccountNo)
                    .HasColumnName("Account_No")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasColumnName("Account_Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranch)
                    .HasColumnName("Bank_Branch")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasColumnName("Bank_Name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BkBookingDate).HasColumnName("BK_Booking_Date");

                entity.Property(e => e.BkCancellationReason)
                    .HasColumnName("BK_Cancellation_Reason")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BkCancelledDate)
                    .HasColumnName("BK_Cancelled_Date")
                    .HasColumnType("date");

                entity.Property(e => e.BkDateFrom).HasColumnName("BK_Date_from");

                entity.Property(e => e.BkDateTo).HasColumnName("BK_Date_To");

                entity.Property(e => e.BkIsAvailed)
                    .HasColumnName("BK_Is_Availed")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.BkIsCancelled)
                    .HasColumnName("BK_Is_Cancelled")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.BkIsRefundStatus)
                    .HasColumnName("BK_Is_Refund_Status")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.BkNoPers)
                    .HasColumnName("BK_No_Pers")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.BkPaymentAmount)
                    .HasColumnName("BK_Payment_Amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BkPaymentMode)
                    .HasColumnName("BK_Payment_Mode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BkPaymentStatus)
                    .HasColumnName("BK_Payment_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BkPmtVoucharDate)
                    .HasColumnName("BK_Pmt_Vouchar_Date")
                    .HasColumnType("date");

                entity.Property(e => e.BkPmtVoucharNo)
                    .HasColumnName("BK_Pmt_Vouchar_No")
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.BkRefundAmount)
                    .HasColumnName("BK_Refund_Amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BkRefundBy)
                    .HasColumnName("BK_Refund_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.BkRefundMode)
                    .HasColumnName("BK_Refund_Mode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BkRefundOn)
                    .HasColumnName("BK_Refund_On")
                    .HasColumnType("date");

                entity.Property(e => e.BkRefundStatus)
                    .HasColumnName("BK_Refund_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BkRfdVoucharDate)
                    .HasColumnName("BK_Rfd_Vouchar_Date")
                    .HasColumnType("date");

                entity.Property(e => e.BkRfdVoucharNo)
                    .HasColumnName("BK_Rfd_Vouchar_No")
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.BkRoomNumber)
                    .IsRequired()
                    .HasColumnName("BK_Room_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CancelledBy)
                    .HasColumnName("Cancelled_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.GuId)
                    .IsRequired()
                    .HasColumnName("GU_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsId)
                    .IsRequired()
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.Ifsc)
                    .HasColumnName("IFSC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsCancelCheckedByAdmin)
                    .HasColumnName("Is_CancelChecked_ByAdmin")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCancelCheckedByBankUser)
                    .HasColumnName("Is_CancelChecked_ByBankUser")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCheckedByAdmin)
                    .HasColumnName("Is_CheckedBy_Admin")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCheckedByBankUser)
                    .HasColumnName("Is_CheckedBy_BankUser")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsReportChecked)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.TotalCost).HasColumnType("int(11)");

                entity.HasOne(d => d.BkRefundByNavigation)
                    .WithMany(p => p.TtBooking)
                    .HasForeignKey(d => d.BkRefundBy)
                    .HasConstraintName("FK_ttBooking_tmUser");

                entity.HasOne(d => d.CancelledByNavigation)
                    .WithMany(p => p.TtBookingCancelledByNavigation)
                    .HasForeignKey(d => d.CancelledBy)
                    .HasConstraintName("FK_ttBooking_cancelBy");

                entity.HasOne(d => d.Gu)
                    .WithMany(p => p.TtBookingGu)
                    .HasForeignKey(d => d.GuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBooking_tmGuestUser");

                entity.HasOne(d => d.Hs)
                    .WithMany(p => p.TtBooking)
                    .HasForeignKey(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBooking_tmHomestay");
            });

            modelBuilder.Entity<TtBookingRoomDetail>(entity =>
            {
                entity.ToTable("tt_booking_room_detail");

                entity.HasIndex(e => e.BookingId)
                    .HasName("FK_ttBookingRoomDetail_Booking_Id_Idx");

                entity.HasIndex(e => e.HsId)
                    .HasName("FK_ttBookingRoomDetail_HS_Id_Idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.BookingId)
                    .IsRequired()
                    .HasColumnName("Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FromDt).HasColumnName("From_Dt");

                entity.Property(e => e.HsId)
                    .IsRequired()
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.RoomNo)
                    .HasColumnName("Room_No")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ToDt).HasColumnName("To_Dt");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.TtBookingRoomDetail)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBookingRoomDetail_HSBookingId");

                entity.HasOne(d => d.Hs)
                    .WithMany(p => p.TtBookingRoomDetail)
                    .HasForeignKey(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttBookingRoomDetail_HSId");
            });

            modelBuilder.Entity<TtHsFeedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackId)
                    .HasName("PRIMARY");

                entity.ToTable("tt_hs_feedback");

                entity.HasIndex(e => e.ActionTakenBy)
                    .HasName("FK_ttHsFeedback_tmUser");

                entity.HasIndex(e => e.HsBookingId)
                    .HasName("FK_ttHsFeedback_ttBooking_idx");

                entity.Property(e => e.FeedbackId)
                    .HasColumnName("Feedback_Id")
                    .HasMaxLength(36)
                    .IsFixedLength()
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ActionDate).HasColumnName("Action_Date");

                entity.Property(e => e.ActionDescription).HasColumnName("Action_Description");

                entity.Property(e => e.ActionTakenBy)
                    .HasColumnName("Action_Taken_by")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsBookingId)
                    .IsRequired()
                    .HasColumnName("HS_Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength()
                    .HasDefaultValueSql("''");

                entity.Property(e => e.HsFeedback).HasColumnName("HS_Feedback");

                entity.Property(e => e.HsRatings)
                    .HasColumnName("HS_Ratings")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActionTaken)
                    .HasColumnName("Is_Action_Taken")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsViewed)
                    .HasColumnName("Is_Viewed")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.ActionTakenByNavigation)
                    .WithMany(p => p.TtHsFeedback)
                    .HasForeignKey(d => d.ActionTakenBy)
                    .HasConstraintName("FK_ttHsFeedback_tmUser");

                entity.HasOne(d => d.HsBooking)
                    .WithMany(p => p.TtHsFeedback)
                    .HasForeignKey(d => d.HsBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttHsFeedback_ttBooking");
            });

            modelBuilder.Entity<TtHsPopularity>(entity =>
            {
                entity.HasKey(e => e.HsId)
                    .HasName("PRIMARY");

                entity.ToTable("tt_hs_popularity");

                entity.Property(e => e.HsId)
                    .HasColumnName("HS_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsSearchCount)
                    .HasColumnName("HS_Search_count")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Hs)
                    .WithOne(p => p.TtHsPopularity)
                    .HasForeignKey<TtHsPopularity>(d => d.HsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttHsPopularity_tmHomestay");
            });

            modelBuilder.Entity<TtPackageFeedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackId)
                    .HasName("PRIMARY");

                entity.ToTable("tt_package_feedback");

                entity.HasIndex(e => e.ActionTakenBy)
                    .HasName("FK_ttPackageFeedback_tmUser");

                entity.HasIndex(e => e.TourBookingId)
                    .HasName("FK_ttPackageFeedback_Tour_Booking_Id_idx");

                entity.Property(e => e.FeedbackId)
                    .HasColumnName("Feedback_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.ActionDate).HasColumnName("Action_Date");

                entity.Property(e => e.ActionDescription).HasColumnName("Action_Description");

                entity.Property(e => e.ActionTakenBy)
                    .HasColumnName("Action_Taken_by")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.HsFeedback).HasColumnName("HS_Feedback");

                entity.Property(e => e.HsRatings)
                    .HasColumnName("HS_Ratings")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActionTaken)
                    .HasColumnName("Is_Action_Taken")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsViewed)
                    .HasColumnName("Is_Viewed")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.TourBookingId)
                    .IsRequired()
                    .HasColumnName("Tour_Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.ActionTakenByNavigation)
                    .WithMany(p => p.TtPackageFeedback)
                    .HasForeignKey(d => d.ActionTakenBy)
                    .HasConstraintName("FK_ttPackageFeedback_tmUser");

                entity.HasOne(d => d.TourBooking)
                    .WithMany(p => p.TtPackageFeedback)
                    .HasForeignKey(d => d.TourBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttPackageFeedback_ttTourBooking");
            });

            modelBuilder.Entity<TtRolePermissionMap>(entity =>
            {
                entity.ToTable("tt_role_permission_map");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("FK_ttRolePermissionMap_Permission_Id_By_Idx");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FK_ttRolePermissionMap_Role_Id_By_Idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.PermissionId)
                    .HasColumnName("Permission_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.RoleId)
                    .HasColumnName("Role_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.TtRolePermissionMap)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_ttRolePermissionMap_tmPermission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TtRolePermissionMap)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_ttRolePermissionMap_RoleId");
            });

            modelBuilder.Entity<TtTourBooking>(entity =>
            {
                entity.ToTable("tt_tour_booking");

                entity.HasIndex(e => e.CancelledBy)
                    .HasName("FK_ttTourBooking_cancelledBy_idx");

                entity.HasIndex(e => e.GuId)
                    .HasName("FK_ttTourBooking_GU_Id_Idx");

                entity.HasIndex(e => e.RefundBy)
                    .HasName("FK_ttTourBooking_tmUser_idx");

                entity.HasIndex(e => e.TourDateId)
                    .HasName("FK_ttTourBooking_Tour_Date_Id_Idx");

                entity.HasIndex(e => e.TourId)
                    .HasName("FK_ttTourBooking_Tour_Id_Idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.AccountNo)
                    .HasColumnName("Account_No")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasColumnName("Account_Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranch)
                    .HasColumnName("Bank_Branch")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasColumnName("Bank_Name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BookingDate).HasColumnName("Booking_Date");

                entity.Property(e => e.CancellationReason)
                    .HasColumnName("Cancellation_Reason")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CancelledBy)
                    .HasColumnName("Cancelled_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.CancelledDate)
                    .HasColumnName("Cancelled_Date")
                    .HasColumnType("date");

                entity.Property(e => e.GuId)
                    .IsRequired()
                    .HasColumnName("GU_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.Ifsc)
                    .HasColumnName("IFSC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsCancel)
                    .HasColumnName("Is_Cancel")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCancelCheckedByAdmin)
                    .HasColumnName("Is_CancelChecked_ByAdmin")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCancelCheckedByBankUser)
                    .HasColumnName("Is_CancelChecked_ByBankUser")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCheckedByAdmin)
                    .HasColumnName("Is_CheckedBy_Admin")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCheckedByBankUser)
                    .HasColumnName("Is_CheckedBy_BankUser")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("Is_Completed")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsReportChecked)
                    .HasColumnName("Is_ReportChecked")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.NoOfPerson)
                    .HasColumnName("No_Of_Person")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("Payment_Amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentMode)
                    .HasColumnName("Payment_Mode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .HasColumnName("Payment_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PaymentVoucherDate)
                    .HasColumnName("Payment_Voucher_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PaymentVoucherNo)
                    .HasColumnName("Payment_Voucher_No")
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.RefundBy)
                    .HasColumnName("Refund_By")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.RefundOn)
                    .HasColumnName("Refund_On")
                    .HasColumnType("date");

                entity.Property(e => e.RefundStatus)
                    .HasColumnName("Refund_Status")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.RfdVoucherAmount)
                    .HasColumnName("Rfd_Voucher_Amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RfdVoucherDate)
                    .HasColumnName("Rfd_Voucher_Date")
                    .HasColumnType("date");

                entity.Property(e => e.RfdVoucherMode)
                    .HasColumnName("Rfd_Voucher_Mode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RfdVoucherNo)
                    .HasColumnName("Rfd_Voucher_No")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RfdVoucherStatus)
                    .HasColumnName("Rfd_Voucher_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TotalRate)
                    .HasColumnName("Total_Rate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TourDateId)
                    .IsRequired()
                    .HasColumnName("Tour_Date_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.TourId)
                    .IsRequired()
                    .HasColumnName("Tour_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.CancelledByNavigation)
                    .WithMany(p => p.TtTourBookingCancelledByNavigation)
                    .HasForeignKey(d => d.CancelledBy)
                    .HasConstraintName("FK_ttTourBooking_cancelledBy");

                entity.HasOne(d => d.Gu)
                    .WithMany(p => p.TtTourBookingGu)
                    .HasForeignKey(d => d.GuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttTourBooking_tmGuestUser");

                entity.HasOne(d => d.RefundByNavigation)
                    .WithMany(p => p.TtTourBooking)
                    .HasForeignKey(d => d.RefundBy)
                    .HasConstraintName("FK_ttTourBooking_refundBy");

                entity.HasOne(d => d.TourDate)
                    .WithMany(p => p.TtTourBooking)
                    .HasForeignKey(d => d.TourDateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttTourBooking_ttTourDate");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TtTourBooking)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttTourBooking_tmTour");
            });

            modelBuilder.Entity<TtTourBookingDetail>(entity =>
            {
                entity.ToTable("tt_tour_booking_detail");

                entity.HasIndex(e => e.TourBookingId)
                    .HasName("FK_ttTourBookingDetail_Tour_Booking_Id_Idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.TourBookingId)
                    .IsRequired()
                    .HasColumnName("Tour_Booking_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.TourBooking)
                    .WithMany(p => p.TtTourBookingDetail)
                    .HasForeignKey(d => d.TourBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttTourBookingDetail_tmTour");
            });

            modelBuilder.Entity<TtTourDate>(entity =>
            {
                entity.ToTable("tt_tour_date");

                entity.HasIndex(e => e.Id)
                    .HasName("FK_ttTourDate_Id_Idx");

                entity.HasIndex(e => e.TourId)
                    .HasName("FK_ttTourDate_tmTour_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.FromDate).HasColumnName("From_Date");

                entity.Property(e => e.IsActive)
                    .HasColumnName("Is_Active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ToDate).HasColumnName("To_Date");

                entity.Property(e => e.TourId)
                    .IsRequired()
                    .HasColumnName("Tour_Id")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TtTourDate)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ttTourDate_tmTour");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
