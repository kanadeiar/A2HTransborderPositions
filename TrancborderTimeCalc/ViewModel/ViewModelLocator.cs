namespace TrancborderTimeCalc.ViewModel;

class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
}

