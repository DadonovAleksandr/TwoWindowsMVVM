using Microsoft.Extensions.DependencyInjection;
using TwoWindowsMVVM.ViewModels.MainWindowVm;

namespace TwoWindowsMVVM.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}