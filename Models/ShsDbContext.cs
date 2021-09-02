using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SHS.Models.Entities;


namespace SHS.Models
{
    /// <summary>
    /// 
    public class ShsDbContext : DbContext
    {
        public ShsDbContext(DbContextOptions<ShsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity => this.CreateAgentModel(entity));
        }

        private void CreateAgentModel(EntityTypeBuilder<Agent> agentBuilder)
        {
            agentBuilder.ToTable("agent");

            agentBuilder.HasIndex(agent => agent.IdNo, "id_no")
                .IsUnique();

            agentBuilder.Property(agent => agent.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            agentBuilder.Property(e => e.AgentNo)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("agent_no")
                .HasComment("業務員編號");

            agentBuilder.Property(agent => agent.CellPhone)
                .HasMaxLength(10)
                .HasColumnName("cell_phone")
                .HasComment("業務員電話");

            agentBuilder.Property(agent => agent.Dob)
                .HasColumnType("date")
                .HasColumnName("dob")
                .HasComment("業務員生日");

            agentBuilder.Property(agent => agent.Email)
                .HasMaxLength(320)
                .HasColumnName("email")
                .HasComment("業務員信箱");

            agentBuilder.Property(agent => agent.IdNo)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("id_no")
                .IsFixedLength(true)
                .HasComment("業務員身份證字號");

            agentBuilder.Property(agent => agent.Name)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("name")
                .HasComment("業務員姓名");
        }

        public virtual DbSet<Agent> Agents { get; set; }

        public virtual DbSet<BranchOffice> BranchOffices { get; set; }

        public virtual DbSet<InsCertificate> InsCertificates { get; set; }

        public virtual DbSet<InsCourse> InsCourses { get; set; }
    }
}