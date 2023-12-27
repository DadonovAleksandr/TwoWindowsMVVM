using ProjectVersionInfo;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using TwoWindowsMVVM.Infrastructure.Commands;
using TwoWindowsMVVM.Model;
using TwoWindowsMVVM.Model.AppSettings.AppConfig;
using TwoWindowsMVVM.Service.UserDialogService;
using TwoWindowsMVVM.ViewModels.Base;

namespace TwoWindowsMVVM.ViewModels.MainWindowVm
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly IAppConfig _appConfig;
        private readonly IUserDialogService _userDialogService;
        /* ------------------------------------------------------------------------------------------------------------ */
        public MainWindowViewModel(IUserDialogService userDialogService)
        {
            _log.Debug($"Вызов конструктора {GetType().Name}");
            _appConfig = AppConfig.GetConfigFromDefaultPath();
            _userDialogService = userDialogService;

            var prjVersion = new ProjectVersion(Assembly.GetExecutingAssembly());
            //Title = $"{AppConst.Get().AppDesciption} {prjVersion.Version}";
            Title = $"Главное окно";

            #region Commands
            Exit = new RelayCommand(OnExitExecuted, CanExitExecute);
            SendMessage = new RelayCommand(OnSendMessageExecuted, p => p is string { Length: > 0});
            OpenSecondWindow = new RelayCommand(OnOpenSecondWindowExecuted, CanOpenSecondWindowExecute);
            ChangeToSecondWindow = new RelayCommand(OnChangeToSecondWindowExecuted, CanChangeToSecondWindowExecute);
            #endregion

        }

        /// <summary>
        /// Действия выполняемые при закрытии основной формы
        /// </summary>
        public void OnExit()
        {
            //_projectConfigurationRepository?.Save();
        }
        /* ------------------------------------------------------------------------------------------------------------ */
        #region Commands

        #region Exit
        public ICommand Exit { get; }
        private void OnExitExecuted(object p) { }
        private bool CanExitExecute(object p) => true;
        #endregion

        #region SendMessage
        public ICommand SendMessage { get; }
        private void OnSendMessageExecuted(object p) { }
        #endregion

        #region  OpenSecondWindow
        public ICommand OpenSecondWindow { get; }
        private void OnOpenSecondWindowExecuted(object p) { }
        private bool CanOpenSecondWindowExecute(object p) => true;
        #endregion

        #region ChangeToSecondWindow
        public ICommand ChangeToSecondWindow { get; }
        private void OnChangeToSecondWindowExecuted(object p) { }
        private bool CanChangeToSecondWindowExecute(object p) => true;
        #endregion

        #endregion

        /* ------------------------------------------------------------------------------------------------------------ */

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get => Get<string>(); set => Set(value); }

        public string Message { get => Get<string>(); set => Set(value); }

        private readonly ObservableCollection<TextMessageModel> _Messages = new();
        public IEnumerable<TextMessageModel> Messages => _Messages;

    }
}