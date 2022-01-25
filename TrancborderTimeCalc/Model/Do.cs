namespace TrancborderTimeCalc.Model;

/// <summary> Действие </summary>
public class Do : Entity
{
    private int _Order;
    /// <summary> Порядковый номер для сортировки </summary>
    public int Order
    {
        get => _Order;
        set => Set(ref _Order, value);
    }

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
    private int _MassifNumber;
    /// <summary> Номер места куда или откуда делается действие </summary>
    public int MassifNumber
    {
        get => _MassifNumber;
        set => Set(ref _MassifNumber, value);
    }
    private int _Seconds;
    /// <summary> Количество секунд на это действие </summary>
    public int Seconds
    {
        get => _Seconds;
        set => Set(ref _Seconds, value);
    }

    private string _Calc;
    /// <summary> Запись расчета </summary>
    public string Calc
    {
        get => _Calc;
        set => Set(ref _Calc, value);
    }
}

/// <summary> Вид действия </summary>
public enum TypeDoing
{
    /// <summary> С входа на ударный стол 1 </summary>
    FromInputToHitTable1,
    /// <summary> С входа на ударный стол 2 </summary>
    FromInputToHitTable2,
    /// <summary> С входа в камеру созревания </summary>
    FromInputToPosition,
    /// <summary> С ударного стола 1 в камеру </summary>
    FromTable1ToPosition,
    /// <summary> С ударного стола 2 в камеру </summary>
    FromTable2ToPosition,
    /// <summary> С камеры на выход </summary>
    FromPositionToOutput,
}
