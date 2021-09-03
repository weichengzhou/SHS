using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SHS.Models.Entities;


namespace SHS.Models
{
    /// <summary>
    /// Customize DbContext define the structure of database.
    /// </summary>
    public class ShsDbContext : DbContext
    {
        /// <summary>
        /// Customize DbContext, inheritance of DbContext. 
        /// </summary>
        /// <param name="options">The DbContext options can be configured by user.</param>
        public ShsDbContext(DbContextOptions<ShsDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// When model created in DbContext, call this.
        /// </summary>
        /// <param name="modelBuilder">Used to build model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity => this.CreateAgentModel(entity));
        }

        /// <summary>
        /// Create agent model by schema.
        /// </summary>
        /// <param name="agentBuilder">Used to build agent model.</param>
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

        /// <summary>
        /// The set of agents in table.
        /// </summary>
        public virtual DbSet<Agent> Agents { get; set; }

        /// <summary>
        /// The set of branch offices in table.
        /// </summary>
        public virtual DbSet<BranchOffice> BranchOffices { get; set; }

        /// <summary>
        /// The set of insurance certificates in table.
        /// </summary>
        public virtual DbSet<InsCertificate> InsCertificates { get; set; }

        /// <summary>
        /// The set of insurance courses in table.
        /// </summary>
        public virtual DbSet<InsCourse> InsCourses { get; set; }
    }
}