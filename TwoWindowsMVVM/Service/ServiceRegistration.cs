﻿using Microsoft.Extensions.DependencyInjection;
using TwoWindowsMVVM.Service.MessageBus;
using TwoWindowsMVVM.Service.UserDialogService;

namespace TwoWindowsMVVM.Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) => services
            .AddTransient<IUserDialogService, WindowsUserDialogService>()
            .AddSingleton<IMessageBus, MessageBusService>()
            ;

    }
}