using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using Aspose.Cells;
using b1task2.Context;
using Microsoft.AspNetCore.Mvc;
using b1task2.Models;
using b1task2.Models.Balance;
using Microsoft.AspNetCore.Http.Features;

namespace b1task2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() => View(_context.BalanceFiles);

    [HttpGet]
    public IActionResult BalanceFile(int id) => View(_context.BalanceFiles.Find(id));

    [HttpPost("upload")]
    public void UploadFile()
    {
        var balanceFiles = new List<BalanceFile>();
        foreach (var file in Request.Form.Files)
        {
            try
            {
                var excel = new Workbook(file.OpenReadStream());
                var sheet = excel.Worksheets[0];

                var dt = sheet.Cells.ExportDataTable(8, 0, 617, 7);
                var balanceFile = new BalanceFile {FileName = file.FileName};
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (int.TryParse(dataRow.ItemArray[0]?.ToString(), out var lineNumber))
                    {
                        if (lineNumber < 1000)
                        {
                            balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks
                                .Add(new BalanceLineBlock(lineNumber));
                        }
                        else
                        {
                            balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks.Last().BalanceLines.Add(
                                new BalanceLine
                                {
                                    BalanceLineNumber = lineNumber,
                                    OpeningBalanceAsset = double.Parse(dataRow.ItemArray[1]!.ToString()!),
                                    OpeningBalanceLiability = double.Parse(dataRow.ItemArray[2]!.ToString()!),
                                    TurnoverDebit = double.Parse(dataRow.ItemArray[3]!.ToString()!),
                                    TurnoverCredit = double.Parse(dataRow.ItemArray[4]!.ToString()!),
                                });
                        }
                    }
                    else if (!dataRow.ItemArray[0]!.ToString()!.Equals("ПО КЛАССУ"))
                    {
                        balanceFile.BalanceSheetClasses.Add(new BalanceSheetClass(dataRow.ItemArray[0]!.ToString()!));
                    }
                    else
                    {
                        balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks
                            .Remove(balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks.Last());
                    }
                }
                balanceFiles.Add(balanceFile);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        _context.BalanceFiles.AddRange(balanceFiles);
        _context.SaveChanges();
        var aa = _context.BalanceFiles.Count();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}