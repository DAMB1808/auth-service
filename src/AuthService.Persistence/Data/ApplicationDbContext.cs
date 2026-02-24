using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    // CONSTRUCTOR
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // REPRESENTACIÓN DE TABLAS
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    // CONFIGURACIÓN DEL MODELO
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Convertir nombres a snake_case
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }
            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName();
                if (!string.IsNullOrEmpty(columnName))
                {
                    property.SetColumnName(ToSnakeCase(columnName));
                }
            }
        }

        // CONFIGURACIÓN ENTIDAD USER
        modelBuilder.Entity<User>(entity =>
        {
            
            entity.HasKey(e => e.Id);


            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();

            entity.HasOne(e => e.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.UserEmail)
                .WithOne(ue => ue.User)
                .HasForeignKey<UserEmail>(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.UserPasswordReset)
                .WithOne(upr => upr.User)
                .HasForeignKey<UserPasswordReset>(upr => upr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CONFIGURACIÓN ENTIDAD USERROLE
        modelBuilder.Entity<UserRole>(entity =>
        {
            // Configuraciones de UserRole si hay
        });
    }

    // MÉTODO AUXILIAR
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return string.Concat(
            input.Select((x, i) => i > 0 && char.IsUpper(x) 
                ? "_" + x.ToString().ToLower() 
                : x.ToString().ToLower())
        );
    }
}




/*
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    // MÉTODO CONSTRUCTOR
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
         
    }

 // REPRESENTACIÓN DE TABLAS EN EL MODELO
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    // Funcion PARA CONFIGURAR EL NOMBRE DE DE CLASE A NOMBRE DE DB
        
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    foreach (var entity in modelBuilder.Model.GetEntityTypes())
    {
        var tableName = entity.GetTableName();
        if (!string.IsNullOrEmpty(tableName))
        {
            entity.SetTableName(ToSnakeCase(tableName));
        }
        foreach (var property in entity.GetProperties())
        {
            var columnName = property.GetColumnName();
            if (!string.IsNullOrEmpty(columnName))
            {
                property.SetColumnName(ToSnakeCase(columnName));
            }
     }
}

    // ------------------------------------------------------
    // CONFIGURAR ESPECIFICA DE LA ENTIDAD USER    
    // -------------------------------------------------------


// CONFIGURACIÓN ESPECÍFICA DE LA ENTIDAD USER
modelBuilder.Entity<User>(entity =>
{
    // Llave primaria
    entity.HasKey(e => e.Id);

    // Índices únicos
    entity.HasIndex(e => e.Email).IsUnique();
    entity.HasIndex(e => e.UserName).IsUnique();

    // Relación 1:1 con UserProfile
    entity.HasOne(e => e.UserProfile)
        .WithOne(p => p.User)
        .HasForeignKey<UserProfile>(p => p.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación 1:N con UserRoles (un usuario puede tener varios roles)
    entity.HasMany(e => e.UserRoles)
        .WithOne(ur => ur.User)
        .HasForeignKey(ur => ur.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación 1:1 con UserEmail
    entity.HasOne(e => e.UserEmail)
        .WithOne(ue => ue.User)
        .HasForeignKey<UserEmail>(ue => ue.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación 1:1 con UserPasswordReset
    entity.HasOne(e => e.UserPasswordReset)
        .WithOne(upr => upr.User)
        .HasForeignKey<UserPasswordReset>(upr => upr.UserId)
        .OnDelete(DeleteBehavior.Cascade);
})

    modelBuilder.Entity<UserRole>(entity =>
    {

    })
    
    



    // -------------------------------------------------------



      private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
 
        return string.Concat(
            input.Select((x, i) => i > 0 && char.IsUpper(x) 
                ? "_" + x.ToString().ToLower() 
                : x.ToString().ToLower())
        );
    }

}

}
*/