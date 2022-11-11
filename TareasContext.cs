using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext {
    
    public DbSet<Categoria> Categorias {get; set; }
    public DbSet<Tarea> Tareas {get; set; }
    public TareasContext (DbContextOptions<TareasContext> options):base (options){
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("e01e4fdb-f1b4-464c-9f88-730813f3efea"), Nombre = "Actividades Pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("e01e4fdb-f1b4-464c-9f88-730813f3ef02"), Nombre = "Actividades Personales", Peso = 50});

        modelBuilder.Entity<Categoria>(categoria => {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).IsRequired(false);
            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("e01e4fdb-f1b4-464c-9f88-730813f3ef10"),CategoriaId = Guid.Parse("e01e4fdb-f1b4-464c-9f88-730813f3efea"), Titulo = "Pago de servicios publicos", FechaCreaccion = DateTime.Now, PrioridadTarea = Prioridad.Media});
        
        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion).IsRequired(false);;
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreaccion);

            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);

            
        });
    }
}