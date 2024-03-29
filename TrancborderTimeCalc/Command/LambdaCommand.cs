﻿namespace TrancborderTimeCalc.Command;

class LambdaCommand : Base.Command
{
    private readonly Action<object> _Execute;
    private readonly Func<object, bool> _CanExecute;
    public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _CanExecute = canExecute;
    }
    protected override bool CanExecute(object p) => _CanExecute?.Invoke(p) ?? true;
    protected override void Execute(object p) => _Execute(p);
}

