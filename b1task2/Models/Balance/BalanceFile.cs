namespace b1task2.Models.Balance;

public class BalanceFile
{
    public int BalanceFileId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public DateTime CreatingDate { get; set; }
    public virtual List<BalanceSheetClass> BalanceSheetClasses { get; set; } = new();
    
    public double TotalOpeningBalanceAsset => BalanceSheetClasses.Select(l => l.TotalOpeningBalanceAsset).Sum();
    public double TotalOpeningBalanceLiability => BalanceSheetClasses.Select(l => l.TotalOpeningBalanceLiability).Sum();
    public double TotalTurnoverDebit => BalanceSheetClasses.Select(l => l.TotalTurnoverDebit).Sum();
    public double TotalTurnoverCredit => BalanceSheetClasses.Select(l => l.TotalTurnoverCredit).Sum();
    public double TotalClosingBalanceAsset => BalanceSheetClasses.Select(l => l.TotalClosingBalanceAsset).Sum();
    public double TotalClosingBalanceLiability => BalanceSheetClasses.Select(l => l.TotalClosingBalanceLiability).Sum();

    public override string ToString() =>
        $"""
        {FileName}
        {BankName}
        Оборотная ведомость по балансовым счетам
        {Period}
        {CreatingDate}
        {string.Join('\n', BalanceSheetClasses.Select(b => b.ToString()))}
        {"БАЛАНС",-10} | {TotalOpeningBalanceAsset,-20:F2} | {TotalOpeningBalanceLiability,-20:F2} | {TotalTurnoverDebit,-20:F2} | {TotalTurnoverCredit,-20:F2} | {TotalClosingBalanceAsset,-20:F2} | {TotalClosingBalanceLiability,-20:F2} 
        """;
}