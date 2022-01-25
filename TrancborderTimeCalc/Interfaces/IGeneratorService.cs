namespace TrancborderTimeCalc.Interfaces;

/// <summary> Сервис создания шагов и расчета результатов </summary>
public interface IGeneratorService
{
    /// <summary> Сгенерировать шаги трансбордера по данным позиций форм </summary>
    /// <param name="positions">Позиции форм</param>
    /// <returns>Шаги трансбордера</returns>
    public IEnumerable<Do> GenerateDos(IEnumerable<Position> positions);
}
