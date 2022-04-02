using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class ATranquilinoProgramacionNCapasContext : DbContext
    {
        public ATranquilinoProgramacionNCapasContext()
        {
        }

        public ATranquilinoProgramacionNCapasContext(DbContextOptions<ATranquilinoProgramacionNCapasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aseguradora> Aseguradoras { get; set; } = null!;
        public virtual DbSet<Colonium> Colonia { get; set; } = null!;
        public virtual DbSet<Dependiente> Dependientes { get; set; } = null!;
        public virtual DbSet<DependienteTipo> DependienteTipos { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<EmpleadoDependiente> EmpleadoDependientes { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Poliza> Polizas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SubPoliza> SubPolizas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-2PSCNMSB; Database= ATranquilinoProgramacionNCapas; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aseguradora>(entity =>
            {
                entity.HasKey(e => e.IdAseguradora)
                    .HasName("PK__Asegurad__8FA1C5975F4D6784");

                entity.ToTable("Aseguradora");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Aseguradoras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Asegurado__IdUsu__412EB0B6");
            });

            modelBuilder.Entity<Colonium>(entity =>
            {
                entity.HasKey(e => e.IdColonia)
                    .HasName("PK__Colonia__A1580F66DB13C358");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Colonia)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Colonia__IdMunic__5FB337D6");
            });

            modelBuilder.Entity<Dependiente>(entity =>
            {
                entity.HasKey(e => e.IdDependiente)
                    .HasName("PK__Dependie__366D077101F2DC99");

                entity.ToTable("Dependiente");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Genero)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDependienteTipoNavigation)
                    .WithMany(p => p.Dependientes)
                    .HasForeignKey(d => d.IdDependienteTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dependien__IdDep__6BE40491");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Dependientes)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dependien__IdEmp__6AEFE058");
            });

            modelBuilder.Entity<DependienteTipo>(entity =>
            {
                entity.HasKey(e => e.IdDependienteTipo)
                    .HasName("PK__Dependie__2C220C629F6CFD2A");

                entity.ToTable("DependienteTipo");

                entity.Property(e => e.IdDependienteTipo).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__1F8E0C762111D4F5");

                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroInterior)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdColonia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Direccion__IdCol__628FA481");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Direccion__IdUsu__6383C8BA");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9EB01D3F7E");

                entity.ToTable("Empleado");

                entity.HasIndex(e => e.Email, "UQ__Empleado__A9D10534D13B45B9")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NSS");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__IdEmpr__489AC854");

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__IdPoli__498EEC8D");
            });

            modelBuilder.Entity<EmpleadoDependiente>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoDependiente)
                    .HasName("PK__Empleado__1AE6F22BB1310F80");

                entity.ToTable("EmpleadoDependiente");

                entity.HasOne(d => d.IdDependienteNavigation)
                    .WithMany(p => p.EmpleadoDependientes)
                    .HasForeignKey(d => d.IdDependiente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmpleadoD__IdDep__70A8B9AE");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.EmpleadoDependientes)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmpleadoD__IdEmp__6FB49575");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__Empresa__5EF4033E363DD362");

                entity.ToTable("Empresa");

                entity.Property(e => e.DireccionWeb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC1AAB40B26");

                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estados)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estado__IdPais__59FA5E80");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__6100597816FEDF40");

                entity.ToTable("Municipio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Municipio__IdEst__5CD6CB2B");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__Pais__FC850A7B8DFA4154");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.IdPoliza)
                    .HasName("PK__Poliza__8E3943B34427C1BB");

                entity.ToTable("Poliza");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubPolizaNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdSubPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Poliza__IdSubPol__208CD6FA");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Poliza__IdUsuari__236943A5");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C42A68E78");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubPoliza>(entity =>
            {
                entity.HasKey(e => e.IdSubPoliza)
                    .HasName("PK__SubPoliz__91F56A98D95FED95");

                entity.ToTable("SubPoliza");

                entity.Property(e => e.IdSubPoliza).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF97BE0433D2");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534C6C72AD1")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F284566DF82B43")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Curp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CURP");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdRol__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
