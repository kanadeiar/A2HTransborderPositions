namespace TrancborderTimeCalc.Services;

public class GeneratorService : IGeneratorService
{
    private readonly ISettings _settings;
    private bool table1work;

    public GeneratorService(ISettings settings)
    {
        _settings = settings;
    }

    public IEnumerable<Do> GenerateDos(IEnumerable<Position> positions, TypeFirstAction type)
    {
        int currentMassif = positions.Where(p => p.Massif != 0).Min(p => p.Massif);
        var maxMassif = positions.Max(p => p.Massif);
        List<Do> list = new List<Do>();
        var order = 1;
        list.Add(new Do { Order = order++, Name = "С заливки на ударный стол 1", Doing = TypeDoing.FromInputToHitTable1 });
        table1work = true;
        list.Add(new Do { Order = order++, Name = "C камеры на кран-кантователь", Doing = TypeDoing.FromPositionToOutput, MassifNumber = currentMassif });

        while (currentMassif < maxMassif)
        {
            if (type == TypeFirstAction.FromInput)
            {
                if (table1work)
                {
                    list.Add(new Do { Order = order++, Name = "С заливки на ударный стол 2", Doing = TypeDoing.FromInputToHitTable2 });
                    var nextrow = positions.Single(p => p.Massif == currentMassif + 1).Row;
                    if (positions.Any(p => p.Row == nextrow && p.Massif == 0 && p.Number != 406 && p.Number != 210 && p.Number != 220 && p.Number != 301))
                    {
                        list.Add(new Do { Order = order++, Name = "С ударного стола 1 на СВОБОДНОЕ рядом со следующим", Doing = TypeDoing.FromTable1ToPosition, MassifNumber = currentMassif + 1 });
                    }
                    else
                    {
                        list.Add(new Do { Order = order++, Name = "С ударного стола 1 на место камеры созревания", Doing = TypeDoing.FromTable1ToPosition, MassifNumber = currentMassif });
                    }
                    table1work = false;
                }
                else
                {
                    list.Add(new Do { Order = order++, Name = "С заливки на ударный стол 1", Doing = TypeDoing.FromInputToHitTable1 });
                    var nextrow = positions.Single(p => p.Massif == currentMassif + 1).Row;
                    if (positions.Any(p => p.Row == nextrow && p.Massif == 0 && p.Number != 406 && p.Number != 210 && p.Number != 220 && p.Number != 301))
                    {
                        list.Add(new Do { Order = order++, Name = "С ударного стола 2 на СВОБОДНОЕ рядом со следующим", Doing = TypeDoing.FromTable2ToPosition, MassifNumber = currentMassif + 1 });
                    }
                    else
                    {
                        list.Add(new Do { Order = order++, Name = "С ударного стола 2 на место камеры созревания", Doing = TypeDoing.FromTable2ToPosition, MassifNumber = currentMassif });
                    }
                    table1work = true;
                }
                list.Add(new Do { Order = order++, Name = "C места камеры созревания на кран-кантователь", Doing = TypeDoing.FromPositionToOutput, MassifNumber = currentMassif + 1 });
            }
            currentMassif++;
        }

        if (table1work)
        {
            list.Add(new Do { Order = order++, Name = "С ударного стола 1 на место камеры созревания", Doing = TypeDoing.FromTable1ToPosition, MassifNumber = currentMassif });
        }
        else
        {
            list.Add(new Do { Order = order++, Name = "С ударного стола 2 на место камеры созревания", Doing = TypeDoing.FromTable2ToPosition, MassifNumber = currentMassif });
        }

        var row = 9;
        foreach (var item in list)
        {
            var sec = 0;
            var sb = new StringBuilder();
            //to 1
            if (item.Doing == TypeDoing.FromInputToHitTable1 || item.Doing == TypeDoing.FromInputToHitTable2 || item.Doing == TypeDoing.FromInputToPosition)
            {
                sec += Math.Abs(row - 1) * _settings.TimeOnRow;
                row = 1;
            }
            if (item.Doing == TypeDoing.FromTable1ToPosition)
            {
                sec += Math.Abs(row - 6) * _settings.TimeOnRow;
                row = 6;
            }
            if (item.Doing == TypeDoing.FromTable2ToPosition)
            {
                sec += Math.Abs(row - 5) * _settings.TimeOnRow;
                row = 5;
            }
            if (item.Doing == TypeDoing.FromPositionToOutput)
            {
                var nextrow = positions.Single(p => p.Massif == item.MassifNumber).Row;
                sec += Math.Abs(row - nextrow) * _settings.TimeOnRow;
                row = nextrow;
            }
            sb.Append(sec.ToString());
            //catch
            sec += _settings.TimeFromPlace;
            sb.Append($" + {_settings.TimeFromPlace}");
            var sec2 = 0;
            //to 2
            if (item.Doing == TypeDoing.FromInputToHitTable1)
            {
                sec2 += Math.Abs(row - 6) * _settings.TimeOnRow;
                row = 6;
            }
            if (item.Doing == TypeDoing.FromInputToHitTable2)
            {
                sec2 += Math.Abs(row - 5) * _settings.TimeOnRow;
                row = 5;
            }
            if (item.Doing == TypeDoing.FromInputToPosition || item.Doing == TypeDoing.FromTable1ToPosition || item.Doing == TypeDoing.FromTable2ToPosition)
            {
                var nextrow = positions.Single(p => p.Massif == item.MassifNumber).Row;
                sec2 += Math.Abs(row - nextrow) * _settings.TimeOnRow;
                row = nextrow;
            }
            if (item.Doing == TypeDoing.FromPositionToOutput)
            {
                sec2 += Math.Abs(row - 9) * _settings.TimeOnRow;
                row = 9;
            }
            sb.Append($" + {sec2}");
            //throw
            sec2 += _settings.TimeToPlace;
            sb.Append($" + {_settings.TimeToPlace}");

            item.Seconds = sec + sec2;
            item.Calc = sb.ToString();
        }

        return list;
    }
}

