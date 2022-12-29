namespace b1task2.Models.Balance;

public class BalanceSheetClass
{
    public int BalanceSheetClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;

    //virtual потому что LazyLoading требует этого
    public virtual List<BalanceLineBlock> BalanceLineBlocks { get; set; } = new();

    //вычисляемые свойства
    public double TotalOpeningBalanceAsset => BalanceLineBlocks.Select(l => l.TotalOpeningBalanceAsset).Sum();
    public double TotalOpeningBalanceLiability => BalanceLineBlocks.Select(l => l.TotalOpeningBalanceLiability).Sum();
    public double TotalTurnoverDebit => BalanceLineBlocks.Select(l => l.TotalTurnoverDebit).Sum();
    public double TotalTurnoverCredit => BalanceLineBlocks.Select(l => l.TotalTurnoverCredit).Sum();
    public double TotalClosingBalanceAsset => BalanceLineBlocks.Select(l => l.TotalClosingBalanceAsset).Sum();
    public double TotalClosingBalanceLiability => BalanceLineBlocks.Select(l => l.TotalClosingBalanceLiability).Sum();

    //формируем многострочную строку с использованием интерполяции для записи в файл
    public override string ToString() =>
        $"""
        {ClassName}
        {string.Join('\n', BalanceLineBlocks.Select(b => b.ToString()))}
        {"ПО КЛАССУ",-10} | {TotalOpeningBalanceAsset,-20:F2} | {TotalOpeningBalanceLiability,-20:F2} | {TotalTurnoverDebit,-20:F2} | {TotalTurnoverCredit,-20:F2} | {TotalClosingBalanceAsset,-20:F2} | {TotalClosingBalanceLiability,-20:F2} 
        """;
}