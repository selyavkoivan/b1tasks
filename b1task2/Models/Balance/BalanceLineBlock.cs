namespace b1task2.Models.Balance;

public class BalanceLineBlock
{
    public int BalanceLineBlockId { get; set; }
    public int BalanceLineBlockNumber { get; set; }

    public virtual List<BalanceLine> BalanceLines { get; set; } = new();

    public double TotalOpeningBalanceAsset => BalanceLines.Select(l => l.OpeningBalanceAsset).Sum();
    public double TotalOpeningBalanceLiability => BalanceLines.Select(l => l.OpeningBalanceLiability).Sum();
    public double TotalTurnoverDebit => BalanceLines.Select(l => l.TurnoverDebit).Sum();
    public double TotalTurnoverCredit => BalanceLines.Select(l => l.TurnoverCredit).Sum();
    public double TotalClosingBalanceAsset => BalanceLines.Select(l => l.ClosingBalanceAsset).Sum();
    public double TotalClosingBalanceLiability => BalanceLines.Select(l => l.ClosingBalanceLiability).Sum();

    public override string ToString() =>
        $"""
        {string.Join('\n', BalanceLines.Select(b => b.ToString()))}
        {BalanceLineBlockNumber,-10} | {TotalOpeningBalanceAsset,-20:F2} | {TotalOpeningBalanceLiability,-20:F2} | {TotalTurnoverDebit,-20:F2} | {TotalTurnoverCredit,-20:F2} | {TotalClosingBalanceAsset,-20:F2} | {TotalClosingBalanceLiability,-20:F2} 
        """;
}