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
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Locator;
using System.Drawing;
using System.Windows.Media;
using DesktopCharacter.Model.Database.Domain;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

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
        /// <summary>
        /// コンフィグファイルのリポジトリ
        /// </summary>
        private BabumiConfigRepository _babumiConfigRepository;

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
                        MenuVM.IsMainMenuOpen = true;
                    });
                }
                return _moveToMenuCommand;
            }
        }

        public LauncherMenuViewModel(MenuItemViewModel vm, Util.Math.Point screenSize)
        {
            _babumiConfigRepository = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            MenuVM = vm;
            IsOpen = true;
            ScreenSize = screenSize;
            this.ItemSource = new List<RadialMenu.Controls.RadialMenuItem>();

            Func<LauncherSettingsDataSet, Tuple<WrapPanel, ICommand>> createItem = new Func<LauncherSettingsDataSet, Tuple<WrapPanel, ICommand>>((LauncherSettingsDataSet item ) =>
            {
                var wrapPanel = new WrapPanel();
                System.Windows.Controls.Image soruce = new System.Windows.Controls.Image();
                soruce.Source = Imaging.CreateBitmapSourceFromHIcon(Icon.ExtractAssociatedIcon(item.Path).Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                wrapPanel.Orientation = Orientation.Vertical;
                wrapPanel.Children.Add(soruce);
                wrapPanel.Children.Add( new TextBlock { Text = item.DisplayName } );
                return new Tuple<WrapPanel, ICommand>( wrapPanel, new ViewModelCommand(() => { Process.Start(item.Path); }));
            });

            foreach ( var launcherItem in _babumiConfigRepository.GetConfig().Dataset )
            {
                var item = createItem(launcherItem);
                this.ItemSource.Add(new RadialMenu.Controls.RadialMenuItem
                {
                    Content = item.Item1,
                    Command = item.Item2
                });
            }
        }

        public void Initialize()
        {
            Messenger.Raise(new ReszieMessage("WindowResizeMessage", ScreenSize));
        }
    }
}
