namespace TrancborderTimeCalc.Data;

public class Settings : ISettings
{
    public int TimeToPlace { get; set; } = 20;
    public int TimeFromPlace { get; set; } = 20;
    public int TimeOnRow { get; set; } = 10;
}

