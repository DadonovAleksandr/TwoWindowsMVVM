using System;
using TwoWindowsMVVM.Infrastructure.Commands.Base;

namespace TwoWindowsMVVM.Infrastructure.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Delegate? _Execute;
        private readonly Delegate? _CanExecute;

        public RelayCommand(Action<object?> Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action<object?> Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object? p)
        {
            if(!base.CanExecute(p)) return false;
            return _CanExecute switch
            {
                null => true,
                Func<bool> can_exec => can_exec(),
                Func<object?, bool> can_exec => can_exec(p),
                _ => throw new InvalidOperationException($"Тип делегата {_CanExecute.GetType()} не поддерживается командой")

            };
        }

        protected override void Execute(object? p)
        {
            switch(_Execute)
            {
                case Action execute: execute(); break;
                case Action<object?> execute: execute(p); break;

                case null: throw new InvalidOperationException($"Не указан делегат вызова для команды");
                default: throw new InvalidOperationException($"Тип делегата {_Execute.GetType()} не поддерживается командой");
                
            }
        }
    }

    internal class RelayCommand<T> : Command
    {
        private readonly Delegate? _Execute;
        private readonly Delegate? _CanExecute;

        public RelayCommand(Action<T?> Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action<T?> Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public RelayCommand(Action Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object? p)
        {
            if (!base.CanExecute(p)) return false;
            return _CanExecute switch
            {
                null => true,
                Func<bool> can_exec => can_exec(),
                Func<object?, bool> can_exec => can_exec((T?)Convert.ChangeType(p, typeof(T))),
                _ => throw new InvalidOperationException($"Тип делегата {_CanExecute.GetType()} не поддерживается командой")

            };
        }

        protected override void Execute(object? p)
        {
            switch (_Execute)
            {
                case Action execute: execute(); break;
                case Action<object?> execute: execute((T?)Convert.ChangeType(p, typeof(T))); break;

                case null: throw new InvalidOperationException($"Не указан делегат вызова для команды");
                default: throw new InvalidOperationException($"Тип делегата {_Execute.GetType()} не поддерживается командой");

            }
        }
    }
}