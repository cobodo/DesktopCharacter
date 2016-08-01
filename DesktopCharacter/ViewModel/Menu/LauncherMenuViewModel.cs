using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;
using DesktopCharacter.Util.Messenger.Message;
using System.Windows.Controls;
using Livet.Messaging.Windows;

namespace DesktopCharacter.ViewModel.Menu
{
    class LauncherMenuViewModel : Livet.ViewModel
    {
        public MenuItemViewModel MenuVM
        {
            set; private get;
        }

        public Util.Math.Point ScreenSize
        {
            set; private get;
        }

        private List<RadialMenu.Controls.RadialMenuItem> _itemSource;
        public List<RadialMenu.Controls.RadialMenuItem> ItemSource
        {
            set { _itemSource = value; this.RaisePropertyChanged("ItemSource"); }
            get { return this._itemSource; }
        }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                _isOpen = value;
                RaisePropertyChanged();
            }
        }

        private ViewModelCommand _moveToMenuCommand;
        public ViewModelCommand MoveToMenuCommand
        {
            get
            {
                if (_moveToMenuCommand == null)
                {
                    _moveToMenuCommand = new ViewModelCommand(() =>
                    {
                        IsOpen = false;
                        Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                        MenuVM.IsOpen = true;
                    });
                }
                return _moveToMenuCommand;
            }
        }

        public LauncherMenuViewModel(MenuItemViewModel vm, Util.Math.Point screenSize)
        {
            MenuVM = vm;
            IsOpen = true;
            ScreenSize = screenSize;
            this.ItemSource = new List<RadialMenu.Controls.RadialMenuItem>(new RadialMenu.Controls.RadialMenuItem[] {
                new RadialMenu.Controls.RadialMenuItem { Content = new TextBlock { Text = "notepad.exe" }, Command = new ViewModelCommand( () => { Process.Start("notepad.exe"); })},
                new RadialMenu.Controls.RadialMenuItem { Content = new TextBlock { Text = Properties.Resources.Launcher_Add }, Command = new ViewModelCommand(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Filter = "Setting Files|*.exe|All Files (*.*)|*.*";
                    bool? result = openFileDialog.ShowDialog();
                    if (result == true)
                    {
                        this.ItemSource.Add(
                        new RadialMenu.Controls.RadialMenuItem
                        {
                            Content = new TextBlock { Text = openFileDialog.SafeFileName }, Command = new ViewModelCommand( () => { Process.Start( openFileDialog.FileName ); } )
                        });
                    }
                    IsOpen = false;
                    Task.Run(async () => {
                        await Task.Delay(200);
                        IsOpen = true;
                    });
                })},
            });
        }

        public void Initialize()
        {
            Messenger.Raise(new ReszieMessage("WindowResizeMessage", ScreenSize));
        }
    }
}
