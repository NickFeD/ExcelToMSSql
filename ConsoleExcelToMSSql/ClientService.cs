using DB.Entites;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcelToMSSql
{
    public class ClientService(ILogger<ClientService> logger)
    {
        private ILogger _logger = logger;

        public IEnumerable<Client> GetClientsFromExcel(FileInfo pathExcel, CancellationToken cancellationToken)
        {
            using var excel = new ExcelPackage(pathExcel);

            var worksheet = excel.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                if (!cancellationToken.IsCancellationRequested)
                    yield return GetClientFromWorksheet(worksheet,row);
            }
        }
        private Client GetClientFromWorksheet(ExcelWorksheet worksheet, int row)
        {
            var client = new Client()
            {
                CardCode = int.Parse(worksheet.Cells[row, 1].Text),
                FirstName = worksheet.Cells[row, 2].Text,
                LastName = worksheet.Cells[row, 3].Text,
                SurName = worksheet.Cells[row, 4].Text,
                PhoneMobile = worksheet.Cells[row, 5].Text,
                Email = worksheet.Cells[row, 6].Text,
                GenderId = worksheet.Cells[row, 7].Text,
                City = worksheet.Cells[row, 9].Text,
                Pincode = worksheet.Cells[row, 10].Text,
            };

            var birthdayText = worksheet.Cells[row, 8].Text;
            if (!DateOnly.TryParseExact(birthdayText, "d.M.yyyy", out var birthday))
                _logger.LogWarning("No validated date {birthdayText} -> {birthday}", birthdayText, birthday);

            client.Birthday = birthday;

            var bonusText = worksheet.Cells[row, 11].Text;
            if (!string.IsNullOrWhiteSpace(bonusText))
            {
                if (!int.TryParse(bonusText, out var bonus))
                    _logger.LogWarning("No validation {birthdayText}", bonusText);
                client.Bonus = bonus;
            }
            else
                client.Bonus = null;
            

            var turnoverText = worksheet.Cells[row, 12].Text;
            if (!string.IsNullOrWhiteSpace(turnoverText))
            {
                if (!int.TryParse(turnoverText, out var turnover))
                    _logger.LogWarning("No validation {turnoverText}", turnoverText);
                client.Turnover = turnover;
            }
            else
                client.Turnover = null;



            return client;
        }

    }
}
