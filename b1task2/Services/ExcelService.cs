using System.Data;
using Aspose.Cells;
using b1task2.Context;
using b1task2.Models.Balance;

namespace b1task2.Services;

public static class ExcelService
{
    public static BalanceFile FormFileToBalanceFile(IFormFile file)
    {
        // создаем экземпляр класса Workbook (некое представление нашего эксель файла в виде объекта)
        var excel = new Workbook(file.OpenReadStream());
        // выбираем первый лист
        var sheet = excel.Worksheets[0];
        var balanceFile = new BalanceFile
        {
            FileName = file.FileName,
            BankName = sheet.Cells["A1"].StringValue,
            Period = sheet.Cells["A3"].StringValue,
            CreatingDate = DateTime.Parse(sheet.Cells["A6"].StringValue)
        };
        /* создаем DataTable на основе указанных ячеек
         в данных ячейках хранится таблица бухгалтерского баланса*/
        var dt = sheet.Cells.ExportDataTable(8, 0, 617, 7);
        foreach (DataRow dataRow in dt.Rows)
        {
            if (int.TryParse(dataRow.ItemArray[0]?.ToString(), out var lineNumber))
            {
                // если код строки баланса меньше 1000 создается группа для хранения четырехзначных записей
                if (lineNumber < 1000)
                {
                    balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks.Last().BalanceLineBlockNumber =
                        lineNumber;
                    balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks
                        .Add(new());
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
            // создаем класс и инициализируем его некоторыми значениями
            else if (!dataRow.ItemArray[0]!.ToString()!.Equals("ПО КЛАССУ"))
            {
                balanceFile.BalanceSheetClasses.Add(new BalanceSheetClass
                    {ClassName = dataRow.ItemArray[0]!.ToString()!});
                // создаем блок со строками баланса
                balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks.Add(new());
            }
            // в конце парсинга каждого класса накапливается одна лишняя пустая группа строк, ее удаляем
            else
            {
                balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks
                    .Remove(balanceFile.BalanceSheetClasses.Last().BalanceLineBlocks.Last());
            }
        }
        return balanceFile;
    }
}