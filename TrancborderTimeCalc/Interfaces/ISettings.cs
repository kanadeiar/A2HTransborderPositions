﻿namespace TrancborderTimeCalc.Interfaces;

/// <summary> Сервис настроек </summary>
public interface ISettings
{
    /// <summary> Время на установку на место </summary>
    public int TimeToPlace { get; set; } 
    /// <summary> Время на снятие с места </summary>
    public int TimeFromPlace { get; set; } 
    /// <summary> Время на прохождение одного ряда </summary>
    public int TimeOnRow { get; set; } 
}
