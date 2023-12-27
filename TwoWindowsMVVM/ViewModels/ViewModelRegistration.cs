using Microsoft.Extensions.DependencyInjection;
using TwoWindowsMVVM.ViewModels.MainWindowVm;

namespace TwoWindowsMVVM.ViewModels
{
    public static class ViewModelRegistration
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            ;

    }
}