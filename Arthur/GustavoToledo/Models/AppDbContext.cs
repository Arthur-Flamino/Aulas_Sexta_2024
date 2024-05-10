using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Arthur_GustavoToledo.Models;

public class AppDataContext : DbContext
{
    public DbSet<Funcionario> Funcionarios {get; set;}
    public required ISet<Folha> Folhas{get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Arthur_GustavoToledo.db");
    }
}

