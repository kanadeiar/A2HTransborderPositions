namespace TrancborderTimeCalc.Data;

public class Settings : ISettings
{
    public int TimeToPlace { get; set; } = 10;
    public int TimeFromPlace { get; set; } = 10;
    public int TimeOnRow { get; set; } = 4;
}

