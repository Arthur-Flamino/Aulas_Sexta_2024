using Microsoft.EntityFrameworkCore;

namespace Arthur_GustavoToledo.Models;

public class Folha
{
    public Folha(string folhaId)
    {
        folhaId = Guid.NewGuid().ToString();
    }

    public Folha(double valor, double quantidade, int mes, int ano, double salarioBruto, double salarioIrrf, double salarioInss, double salarioFgts, double salarioLiquido, Funcionario funcionario){
        folhaId = Guid.NewGuid().ToString();
        Valor = valor;
        Quantidade = quantidade;
        Mes = mes;
        Ano = ano;
        SalarioBruto = salarioBruto;
        SalarioIrrf = salarioIrrf;
        SalarioInss = salarioInss;
        SalarioFgts = salarioFgts;
        SalarioLiquido = salarioLiquido;    
        Funcionario = funcionario;
    }

    private string folhaId{get; set;}

    public double Valor{get; set;}

    public double Quantidade{get; set;}

    public int Mes{get; set;}

    public int Ano{get; set;}

    public double SalarioBruto{get; set;}

    public double SalarioIrrf{get; set;}

    public double SalarioInss{get; set;}

    public double SalarioFgts{get; set;}

    public double SalarioLiquido{get; set;}
    public Funcionario Funcionario { get; }
}