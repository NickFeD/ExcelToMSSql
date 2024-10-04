using DB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcelToMSSql;

public class Worker(DatabaseContext context, ClientService clientService, FileInfo fileInfo) : BackgroundService
{
    private readonly DatabaseContext _context = context;
    private readonly ClientService _clientService = clientService;
    private readonly FileInfo _fileInfo = fileInfo;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var clients = _clientService.GetClientsFromExcel(_fileInfo, stoppingToken);
        _context.AddRange(clients);

        await _context.SaveChangesAsync(stoppingToken);

        Console.WriteLine("Database Create and Update");
        this.Dispose();
    }
}
