namespace TrancborderTimeCalc.Services;

public class RepositoryService
{
    private List<Position> _positions = GetInitPositions();
    private List<Do> _dos = GetInitDos();
    public RepositoryService()
    { }

    public IEnumerable<Position> GetPositions() => _positions;
    public IEnumerable<Do> GetDos() => _dos;

    private static List<Position> GetInitPositions()
    {
        var positions = new List<Position>
        {
            new Position { Id = 1, Number = 406, Name = "406-е место приём", Row = 1, IsInput = true },
            new Position { Id = 2, Number = 2, Name = "2-е место", Row = 1, Massif = 1 },
            new Position { Id = 3, Number = 3, Name = "3-е место", Row = 2, Massif = 2 },
            new Position { Id = 4, Number = 4, Name = "4-е место", Row = 2, Massif = 3 },
            new Position { Id = 5, Number = 5, Name = "5-е место", Row = 3, Massif = 4 },
            new Position { Id = 6, Number = 6, Name = "6-е место", Row = 3, Massif = 5 },
            new Position { Id = 7, Number = 8, Name = "8-е место", Row = 4, Massif = 6 },
            new Position { Id = 8, Number = 220, Name = "2-й ударный стол", Row = 5, IsHitTable2 = true },
            new Position { Id = 9, Number = 10, Name = "10-е место", Row = 5, Massif = 7 },
            new Position { Id = 10, Number = 210, Name = "1-й ударный стол", Row = 6, IsHitTable1 = true },
            new Position { Id = 11, Number = 12, Name = "12-е место", Row = 6, Massif = 8 },
            new Position { Id = 12, Number = 14, Name = "14-е место", Row = 7, Massif = 9 },
            new Position { Id = 13, Number = 15, Name = "15-е место", Row = 8, Massif = 10 },
            new Position { Id = 14, Number = 16, Name = "16-е место", Row = 8, Massif = 11 },
            new Position { Id = 15, Number = 301, Name = "301-е место выдача", Row = 9, IsOutput = true },
            new Position { Id = 16, Number = 18, Name = "18-е место", Row = 9, Massif = 12 },
            new Position { Id = 17, Number = 19, Name = "19-е место", Row = 10, Massif = 13 },
            new Position { Id = 18, Number = 20, Name = "20-е место", Row = 10, Massif = 14 },
            new Position { Id = 19, Number = 21, Name = "21-е место", Row = 11, Massif = 15 },
            new Position { Id = 20, Number = 22, Name = "22-е место", Row = 11, Massif = 16 },
            new Position { Id = 21, Number = 23, Name = "23-е место", Row = 12, Massif = 17 },
            new Position { Id = 22, Number = 24, Name = "24-е место", Row = 12, Massif = 18 },
            new Position { Id = 23, Number = 25, Name = "25-е место", Row = 13 },
            new Position { Id = 24, Number = 26, Name = "26-е место", Row = 13 },
            new Position { Id = 25, Number = 27, Name = "27-е место", Row = 14 },
            new Position { Id = 26, Number = 28, Name = "28-е место", Row = 14 },
            new Position { Id = 27, Number = 29, Name = "29-е место", Row = 15 },
            new Position { Id = 28, Number = 30, Name = "30-е место", Row = 15 },
            new Position { Id = 29, Number = 31, Name = "31-е место", Row = 16 },
            new Position { Id = 30, Number = 32, Name = "32-е место", Row = 16 },
            new Position { Id = 31, Number = 33, Name = "33-е место", Row = 17 },
            new Position { Id = 32, Number = 34, Name = "34-е место", Row = 17 },
        };
        return positions;
    }
    private static List<Do> GetInitDos()
    {
        var dos = new List<Do>
        {
            new Do { Name = "С заливки на ударный стол 1", Doing = TypeDoing.FromInputToHitTable1, },
            new Do { Name = "С заливки на ударный стол 2", Doing = TypeDoing.FromInputToHitTable2, },
            new Do { Name = "C камеры на кран-кантователь", Doing = TypeDoing.FromPositionToOutput, PositionNumber = 2, },
            new Do { Name = "С ударного стола 1 в камеру", Doing = TypeDoing.FromTable1ToPosition, PositionNumber = 2 },
            new Do { Name = "С заливки на ударный стол 1", Doing = TypeDoing.FromInputToHitTable1, },
            new Do { Name = "С камеры на кран-кантователь", Doing = TypeDoing.FromPositionToOutput, PositionNumber = 3 },
            new Do { Name = "С ударного стола 2 в камеру", Doing = TypeDoing.FromTable2ToPosition, PositionNumber = 3 },
            new Do { Name = "С заливки на ударный стол 2", Doing = TypeDoing.FromInputToHitTable1, },
            new Do { Name = "C камеры на кран-кантователь", Doing = TypeDoing.FromPositionToOutput, PositionNumber = 4, },
            new Do { Name = "С ударного стола 1 в камеру", Doing = TypeDoing.FromTable1ToPosition, PositionNumber = 4 },
            new Do { Name = "С заливки на ударный стол 1", Doing = TypeDoing.FromInputToHitTable1, },
        };
        return dos;
    }
}

