using Microsoft.EntityFrameworkCore;
using Tss.Process.Data.Entities;

namespace Tss.Process.Data.Context {

    public class ProcessContext : DbContext {

        #region Fields

        private readonly string _connectionString;

        #endregion Fields

        public ProcessContext(DbContextOptions<ProcessContext> options) : base(options) {
            Database.EnsureCreated();
        }
        
        public ProcessContext(string connectionString) {
            _connectionString = connectionString;
            Database.EnsureCreated();
        } 
        
        public DbSet<ProcessMetadata>  ProcessMetadata  { get; set; }  
        public DbSet<ProcessExecution> ProcessExecution { get; set; }
        public DbSet<StepMetadata>     StepMetadata     { get; set; }
        public DbSet<StepExecution>    StepExecution    { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // **** StepMetadata ****

            modelBuilder.Entity<StepMetadata>()
                .HasKey(p => p.StepMetadataId);

            modelBuilder.Entity<StepMetadata>() 
                .Property(p => p.StepMetadataId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StepMetadata>()
                .HasIndex(p => new { p.ProcessMetadataId, p.Ordinal })
                .IsUnique(true);

            modelBuilder.Entity<StepMetadata>()
                .HasOne(p   => p.ProcessMetadata)
                .WithMany(p => p.Steps)
                .OnDelete(DeleteBehavior.NoAction);

            // **** ProcessMetaData ****

            modelBuilder.Entity<ProcessMetadata>()
                .HasKey(p => p.ProcessMetadataId);
                
            modelBuilder.Entity<ProcessMetadata>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            modelBuilder.Entity<ProcessMetadata>()
                .HasMany(p => p.Steps)
                .WithOne(p => p.ProcessMetadata)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProcessMetadata>()
                .HasOne(p => p.StepServiceInfo);

            // **** StepExecution ****

            modelBuilder.Entity<StepExecution>()
                .HasKey(p => p.StepExecutionId);
                
            modelBuilder.Entity<StepExecution>()
                .HasOne(p => p.StepMetadata);

            modelBuilder.Entity<StepExecution>()
                .HasOne(b   => b.ProcessExecution)
                .WithMany(b => b.Steps)
                .OnDelete(DeleteBehavior.NoAction);
                
            // **** ProcessExecution ****

            modelBuilder.Entity<ProcessExecution>()
                .HasKey(p => p.ProcessExecutionId);

            modelBuilder.Entity<ProcessExecution>()
                .HasOne(p => p.CurrentStepExecution);

            modelBuilder.Entity<ProcessExecution>()
                .HasMany(p => p.Steps);

            modelBuilder.Entity<ProcessExecution>()
                .HasOne(p => p.CurrentStepExecution); 

            // **** StepServiceInfo ****
            modelBuilder.Entity<StepServiceInfo>()
                .HasKey(p => p.StepServiceInfoId);
        }
    }
}