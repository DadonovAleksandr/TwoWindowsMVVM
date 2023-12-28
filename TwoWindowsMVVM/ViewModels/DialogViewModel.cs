using System;
using TwoWindowsMVVM.ViewModels.Base;

namespace TwoWindowsMVVM.ViewModels;

internal class DialogViewModel : BaseViewModel
{
    public event EventHandler? DialogComplete;

    protected virtual void OnDialogComplete(EventArgs e) => DialogComplete?.Invoke(this, e);



}