﻿using CommandLine;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace ConsoleExcelToMSSql;

internal class Program
{
    static async Task Main(string[] args)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed(RunOptions);

        void RunOptions(Options opts)
        {
            builder.Services.AddSingleton(opts.InputFiles);
        }

        var connection = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddHostedService<Worker>();
        builder.Services.AddTransient<ClientService>();
        builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
        builder.Services.AddLogging();

        using IHost host = builder.Build();

        await host.RunAsync();
    }
}
