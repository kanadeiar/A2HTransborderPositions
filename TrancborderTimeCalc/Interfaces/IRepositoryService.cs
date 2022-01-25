namespace TrancborderTimeCalc.Interfaces;

/// <summary> Сервис данных по камере созревания </summary>
public interface IRepositoryService
{
    /// <summary> Получить стандартные тестовые позиции камеры созревания </summary>
    /// <returns>Позиции</returns>
    public IEnumerable<Position> GetPositions();
    /// <summary> Получить изначальные позиции и данные по камере созревания </summary>
    /// <returns>Позиции</returns>
    public List<Position> GetInitPositions();
    /// <summary> Получить стандартныые тестовые шаги трансбордера </summary>
    /// <returns>Шаги</returns>
    public IEnumerable<Do> GetDos();
}
