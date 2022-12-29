namespace b1task2.Models.Balance;

public class BalanceLine
{
    public int BalanceLineId { get; set; }

    public int BalanceLineNumber { get; set; }

    public double OpeningBalanceAsset { get; set; }
    public double OpeningBalanceLiability { get; set; }

    public double TurnoverDebit { get; set; }
    public double TurnoverCredit { get; set; }

    public double ClosingBalanceAsset =>
        OpeningBalanceAsset == 0 ? 0 : OpeningBalanceAsset + TurnoverDebit - TurnoverCredit;

    public double ClosingBalanceLiability =>
        OpeningBalanceLiability == 0 ? 0 : OpeningBalanceLiability + TurnoverCredit - TurnoverDebit;
    
    public override string ToString() =>
        $"{BalanceLineNumber,-10} | {OpeningBalanceAsset,-20:F2} | {OpeningBalanceLiability,-20:F2} | {TurnoverDebit,-20:F2} | {TurnoverCredit,-20:F2} | {ClosingBalanceAsset,-20:F2} | {ClosingBalanceLiability,-20:F2}";
}