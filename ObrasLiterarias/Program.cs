using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ObrasLiterarias.Context;
using ObrasLiterarias.Model;

namespace ObrasLiterarias
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SeedAutores();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedAutores(AppDbContext context)
        {
            if (!context.Autores.Any())
            {
                var countries = new List<Autor>
            {
                new Autor { Nome = "Afghanistan" }
            };
                context.AddRange(countries);
                context.SaveChanges();
            }

        }


    }
}
