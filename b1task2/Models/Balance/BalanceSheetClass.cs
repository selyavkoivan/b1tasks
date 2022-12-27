namespace b1task2.Models.Balance;

public class BalanceSheetClass
{
    public int BalanceSheetClassId { get; set; }
    public string ClassName { get; set; }
    
    public virtual List<BalanceLineBlock> BalanceLineBlocks { get; set; }

    public double TotalOpeningBalanceAsset => BalanceLineBlocks.Select(l => l.TotalOpeningBalanceAsset).Sum();
    public double TotalOpeningBalanceLiability => BalanceLineBlocks.Select(l => l.TotalOpeningBalanceLiability).Sum();
    public double TotalTurnoverDebit => BalanceLineBlocks.Select(l => l.TotalTurnoverDebit).Sum();
    public double TotalTurnoverCredit => BalanceLineBlocks.Select(l => l.TotalTurnoverCredit).Sum();
    public double TotalClosingBalanceAsset => BalanceLineBlocks.Select(l => l.TotalClosingBalanceAsset).Sum();
    public double TotalClosingBalanceLiability => BalanceLineBlocks.Select(l => l.TotalClosingBalanceLiability).Sum();
    
    public BalanceSheetClass()
    {
        
    }
    
    public BalanceSheetClass(string className)
    {
        ClassName = className;
        BalanceLineBlocks = new () {new ()};
        
    }

}