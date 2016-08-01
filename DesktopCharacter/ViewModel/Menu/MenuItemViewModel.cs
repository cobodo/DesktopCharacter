using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet.Commands;
using Livet.Messaging.Windows;
using Livet.Messaging;
using System.Diagnostics;
using Microsoft.Win32;
using DesktopCharacter.ViewModel.Tool;
using System.Windows;
using DesktopCharacter.Util.Messenger.Message;

namespace DesktopCharacter.ViewModel.Menu
{
    class MenuItemViewModel : Livet.ViewModel
    {
        public CharacterViewModel CharacterVM
        {
            set; private get;
        }

        public MenuItemViewModel(CharacterViewModel vm)
        {
            IsOpen = true;
            CharacterVM = vm;
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

        private bool _isToolOpen = false;
        public bool IsToolOpen
        {
            get
            {
                return _isToolOpen;
            }
            set
            {
                _isToolOpen = value;
                RaisePropertyChanged();
            }
        }

        private ViewModelCommand _moveToMainMenuCommand;
        public ViewModelCommand MoveToMainMenuCommand
        {
            get
            {
                if (_moveToMainMenuCommand == null)
                {
                    _moveToMainMenuCommand = new ViewModelCommand(() =>
                    {
                        IsToolOpen = false;
                        IsOpen = true;
                    });
                }
                return _moveToMainMenuCommand;
            }
        }

        private ViewModelCommand _moveToToolCommand;
        public ViewModelCommand MoveToToolCommand
        {
            get
            {
                if (_moveToToolCommand == null)
                {
                    _moveToToolCommand = new ViewModelCommand(() =>
                    {
                        IsToolOpen = true;
                        IsOpen = false;
                    });
                }
                return _moveToToolCommand;
            }
        }

        private ViewModelCommand _moveToLauncherItems;
        public ViewModelCommand MoveToLauncherItems
        {
            get
            {
                if (_moveToLauncherItems == null)
                {
                    _moveToLauncherItems = new ViewModelCommand(() =>
                    {
                        using (var vm = new ViewModel.Menu.LauncherMenuViewModel(this, CharacterVM.ScreenSize))
                        {
                            IsOpen = false;
                            Messenger.Raise(new TransitionMessage(vm, "LauncherItems"));
                        }
                    });
                }
                return _moveToLauncherItems;
            }
        }

        private ViewModelCommand _menuCloseCommand;
        public ViewModelCommand MenuCloseCommand
        {
            get
            {
                if (_menuCloseCommand == null)
                {
                    _menuCloseCommand = new ViewModelCommand(() =>
                    {
                        IsOpen = false;
                        Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                    });
                }
                return _menuCloseCommand;
            }
        }

        private ViewModelCommand mTalkCommand;
        public ViewModelCommand TalkCommand
        {
            get
            {
                if (mTalkCommand == null)
                {
                    mTalkCommand = new ViewModelCommand(() =>
                    {
                        DesktopCharacter.Model.CharacterNotify.Instance.Talk("にゃーん");
                    });
                }
                return mTalkCommand;
            }
        }

        private ViewModelCommand mSettingCommand;
        public ViewModelCommand SettingCommand
        {
            get
            {
                if (mSettingCommand == null)
                {
                    mSettingCommand = new ViewModelCommand(() =>
                    {
                        using (var vm = new ViewModel.SettingViewModel())
                        {
                            Messenger.Raise(new TransitionMessage(vm, "Setting"));
                        }
                    });
                }
                return mSettingCommand;
            }
        }

        private ViewModelCommand mCloseCommand;
        public ViewModelCommand CloseCommand
        {
            get
            {
                return mCloseCommand == null
                    ? mCloseCommand = new ViewModelCommand(() => { CharacterVM.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close")); })
                    : mCloseCommand;
            }
        }

        private ViewModelCommand mTimerSettingOpenCommand;
        public ViewModelCommand TimerSettingOpenCommand
        {
            get
            {
                if (mTimerSettingOpenCommand == null)
                {
                    mTimerSettingOpenCommand = new ViewModelCommand(() =>
                    {
                        using (var vm = new Tool.Timer.TimerSettingViewModel(CharacterVM))
                        {
                            Messenger.Raise(new TransitionMessage(vm, "TimerSetting"));
                        }
                    });
                }
                return mTimerSettingOpenCommand;
            }
        }

        private ViewModelCommand _codicWindowOpenCommand;
        public ViewModelCommand CodicWindowOpenCommand
        {
            get
            {
                return _codicWindowOpenCommand ?? (_codicWindowOpenCommand = new ViewModelCommand(() => 
                {
                    try {
                        using (var vm = new Tool.Translate.TranslateViewModel())
                        {
                            Messenger.Raise(new TransitionMessage(vm, "CodicWindow"));
                        }
                    } 
                    catch(NullReferenceException)
                    {
                        MessageBox.Show(Properties.Resources.MenuItem_ErrorCoidcAccesToken);
                    }
                }));
            }
        }

        public void Initialize()
        {
            Messenger.Raise(new ReszieMessage("WindowResizeMessage", CharacterVM.ScreenSize));
        }
    }
}
