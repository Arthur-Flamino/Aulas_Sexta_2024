using Arthur_GustavoToledo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;





        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AppDataContext>();
        var app = builder.Build();



        //criando um funcionario
        //http://localhost:5012/api/funcionario/cadastrar
        app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
        {
            ctx.Funcionarios.Add(funcionario);
            ctx.SaveChanges();
            return Results.Created("", funcionario);
        });

        //http://localhost:5012/api/funcionario/listar
        app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
        {
            if (ctx.Funcionarios.Any())
            {
                return Results.Ok(ctx.Funcionarios.ToList());
            }
            return Results.NotFound("Tabela vazia!");
        });

        //http://localhost:5012/api/folha/cadastrar
        app.MapPost("/api/folha/cadastrar", ([FromBody] Folha folha, [FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
        {
            Funcionario? funcionarioExiste = ctx.Funcionarios.FirstOrDefault(x => x.Id == funcionario.Id);
            if (funcionario is null)
            {
                return Results.NotFound("Funcionario não encontrado!");
            }

            folha.SalarioBruto = folha.Quantidade * folha.Valor;
            folha.SalarioLiquido = folha.SalarioBruto;

            //Imposto de Renda
            if (folha.SalarioBruto <= 1903.98)
            {
                folha.SalarioIrrf = folha.SalarioBruto;
                folha.SalarioLiquido -= folha.SalarioIrrf;
            }
            else if (folha.SalarioBruto >= 1903.99 && folha.SalarioBruto <= 2826.65)
            {
                folha.SalarioIrrf = folha.SalarioBruto * 0.075;
                folha.SalarioLiquido -= folha.SalarioIrrf;
            }
            else if (folha.SalarioBruto >= 2826.66 && folha.SalarioBruto <= 3751.05)
            {
                folha.SalarioIrrf = folha.SalarioBruto * 0.15;
                folha.SalarioLiquido -= folha.SalarioIrrf;
            }
            else if (folha.SalarioBruto >= 3751.06 && folha.SalarioBruto <= 4664.68)
            {
                folha.SalarioIrrf = folha.SalarioBruto * 0.225;
                folha.SalarioLiquido -= folha.SalarioIrrf;
            }
            else if (folha.SalarioBruto > 4664.68)
            {
                folha.SalarioIrrf = folha.SalarioBruto * 0.275;
                folha.SalarioLiquido -= folha.SalarioIrrf;
            }

            // INSS
            if (folha.SalarioBruto <= 1693.72)
            {
                folha.SalarioInss = folha.SalarioBruto * 0.08;
                folha.SalarioLiquido -= folha.SalarioInss;
            }
            else if (folha.SalarioBruto >= 1693.73 && folha.SalarioBruto <= 2822.90)
            {
                folha.SalarioInss = folha.SalarioBruto * 0.09;
                folha.SalarioLiquido -= folha.SalarioInss;
            }
            else if (folha.SalarioBruto >= 2822.91 && folha.SalarioBruto <= 5645.80)
            {
                folha.SalarioInss = folha.SalarioBruto * 0.11;
                folha.SalarioLiquido -= folha.SalarioInss;
            }
            else if (folha.SalarioBruto >= 5645.81)
            {
                folha.SalarioInss = folha.SalarioBruto - 621.03;
                folha.SalarioLiquido -= folha.SalarioInss;
            }

            folha.SalarioFgts = folha.SalarioBruto * 0.08;

            ctx.Folhas.Add(folha);
            ctx.SaveChanges();
            return Results.Created("", folha);
        });

        //http://localhost:5012/api/folha/listar
        app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
        {
            if (ctx.Folhas.Any())
            {
                return Results.Ok(ctx.Folhas.ToList());
            }
            return Results.NotFound("Tabela vazia!");
        });



        app.Run();
    
