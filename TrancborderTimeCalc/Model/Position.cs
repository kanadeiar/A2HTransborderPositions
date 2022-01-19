namespace TrancborderTimeCalc.Model;

public class Position : Entity
{
    private string _Name;
    /// <summary> Название позиции </summary>
    public string Name
    {
        get => _Name;
        set => Set(ref _Name, value );
    }

    private int _Row;
    /// <summary> Ряд </summary>
    public int Row
    {
        get => _Row;
        set => Set(ref _Row, value);
    }

    private int _Massif;
    /// <summary> Массив на этом месте, его номер по порядку </summary>
    public int Massif
    {
        get => _Massif;
        set => Set(ref _Massif, value);
    }
}

