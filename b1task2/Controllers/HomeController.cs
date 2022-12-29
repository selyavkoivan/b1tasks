using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using Aspose.Cells;
using b1task2.Context;
using Microsoft.AspNetCore.Mvc;
using b1task2.Models;
using b1task2.Models.Balance;
using b1task2.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;

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
    public async Task<IActionResult> BalanceFile(int id) => View(await _context.BalanceFiles.FindAsync(id));


    //формируем файл и возвращаем его клиенту 
    [HttpPost]
    public async Task<FileStreamResult> ToFile(int id)
    {
        var balanceFile = await _context.BalanceFiles.FindAsync(id);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(balanceFile!.ToString()));
        return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
        {
            FileDownloadName = $"{balanceFile.FileName}.txt"
        };
    }

    [HttpPost("upload")]
    public async Task UploadFile()
    {
        var balanceFiles = new List<BalanceFile>();
        foreach (var file in Request.Form.Files)
        {
            try
            {
                balanceFiles.Add(ExcelService.FormFileToBalanceFile(file));
            }
            catch (Exception)
            {
                /*если словлена какая то ошибка (скорее всего загружен файл не того формата либо структура экселя
                 отличается, переходим в следующему из загруженных файлов*/
            }
        }

        await _context.BalanceFiles.AddRangeAsync(balanceFiles);
        await _context.SaveChangesAsync();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}