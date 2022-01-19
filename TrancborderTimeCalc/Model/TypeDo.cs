namespace TrancborderTimeCalc.Model;

/// <summary> Действие </summary>
public class TypeDo : Entity
{
    private string _Name;
    /// <summary> Название действия </summary>
    public string Name
    {
        get => _Name;
        set => Set(ref _Name, value);
    }
    private TypeDoing _Doing;
    /// <summary> Действие, вид действия </summary>
    public TypeDoing Doing
    {
        get => _Doing;
        set => Set(ref _Doing, value);
    }
    private int _PositionNumber;
    /// <summary> Номер места куда или откуда делается действие </summary>
    public int PositionNumber
    {
        get => _PositionNumber;
        set => Set(ref _PositionNumber, value);
    }
}

/// <summary> Вид действия </summary>
public enum TypeDoing
{
    FromInputToHitTable1,
    FromInputToHitTable2,
    FromInputToPosition,
    FromTable1ToPosition,
    FromTable2ToPosition,
    FromPositionToOutput,
}
