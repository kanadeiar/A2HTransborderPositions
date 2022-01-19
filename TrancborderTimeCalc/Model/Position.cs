namespace TrancborderTimeCalc.Model;

/// <summary> Позиция </summary>
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

    private bool _IsInput;
    /// <summary> Вход в камеру созревания </summary>
    public bool IsInput
    {
        get => _IsInput;
        set => Set(ref _IsInput, value);
    }

    private bool _IsOutput;
    /// <summary> Выход из камеры созревания </summary>
    public bool IsOutput
    {
        get => _IsOutput;
        set => Set(ref _IsOutput, value);
    }

    private bool _IsHitTable1;
    /// <summary> Ударный стол 1 </summary>
    public bool IsHitTable1
    {
        get => _IsHitTable1;
        set => Set(ref _IsHitTable1, value);
    }

    private bool _IsHitTable2;
    /// <summary> Ударный стол 2 </summary>
    public bool IsHitTable2
    {
        get => _IsHitTable2;
        set => Set(ref _IsHitTable2, value);
    }
}

