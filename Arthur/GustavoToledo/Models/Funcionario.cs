using Microsoft.EntityFrameworkCore;


namespace Arthur_GustavoToledo.Models;

public class Funcionario
{
    public Funcionario(){
        Id = Guid.NewGuid().ToString();
    }

    public Funcionario(String nome, String cpf){
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Cpf = cpf;
    }

    public string? Id {get; set;}

    public string? Nome {get; set;}

    public string? Cpf {get; set;}
}