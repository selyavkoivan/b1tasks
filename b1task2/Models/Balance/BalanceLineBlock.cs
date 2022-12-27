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

    public BalanceLineBlock()
    {
        
    }
    
    public BalanceLineBlock(int balanceLineBlockNumber)
    {
        BalanceLineBlockNumber = balanceLineBlockNumber;
        BalanceLines = new();
    }
}