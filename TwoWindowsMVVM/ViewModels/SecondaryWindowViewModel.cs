using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TwoWindowsMVVM.Infrastructure.Commands;
using TwoWindowsMVVM.Model;
using TwoWindowsMVVM.Service.UserDialogService;
using TwoWindowsMVVM.ViewModels.Base;

namespace TwoWindowsMVVM.ViewModels;

internal class SecondaryWindowViewModel : DialogViewModel
{
    private readonly IUserDialogService _userDialogService;

    public SecondaryWindowViewModel(IUserDialogService userDialogService)
    {
        Title = $"Вторичное окно";
        _userDialogService = userDialogService;

        #region Commands
        SendMessage = new RelayCommand(OnSendMessageExecuted, p => p is string { Length: > 0 });
        OpenMainWindow = new RelayCommand(OnOpenMainWindowExecuted, CanOpenMainWindowExecute);
        ChangeToMainWindow = new RelayCommand(OnChangeToMainWindowExecuted, CanChangeToMainWindowExecute);
        
        #endregion
    }

    /* ------------------------------------------------------------------------------------------------------------ */
    #region Commands

    #region SendMessage
    public ICommand SendMessage { get; }
    private void OnSendMessageExecuted(object p) { }
    #endregion

    #region  OpenSecondWindow
    public ICommand OpenMainWindow { get; }
    private void OnOpenMainWindowExecuted(object p) 
    {
        _userDialogService.OpenMainWindow();
    }
    private bool CanOpenMainWindowExecute(object p) => true;
    #endregion

    #region ChangeToSecondWindow
    public ICommand ChangeToMainWindow { get; }
    private void OnChangeToMainWindowExecuted(object p) 
    {
        _userDialogService.OpenMainWindow();
        OnDialogComplete(EventArgs.Empty);
    }
    private bool CanChangeToMainWindowExecute(object p) => true;
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