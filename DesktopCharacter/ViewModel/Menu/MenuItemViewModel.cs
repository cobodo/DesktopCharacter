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
            CharacterVM = vm;
            Task.Run(async () =>
            {
                await Task.Delay(200);
                //!< 遅延させないで開くとアニメーションしつつウィンドウの移動が行われるので
                //!< ガクッと動いてしまうのを抑える
                IsMainMenuOpen = true;
            });
        }

        private bool _isMainMenuOpen = false;
        public bool IsMainMenuOpen
        {
            get
            {
                return _isMainMenuOpen;
            }
            set
            {
                _isMainMenuOpen = value;
                RaisePropertyChanged();
            }
        }

        private bool _isToolMenuOpen = false;
        public bool IsToolMenuOpen
        {
            get
            {
                return _isToolMenuOpen;
            }
            set
            {
                _isToolMenuOpen = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentSeqnecen { get; set; } = "MainMenu";

        private ListenerCommand<string> _moveToCommand;
        public ListenerCommand<string> MoveToCommand
        {
            get
            {
                if (_moveToCommand == null)
                {
                    _moveToCommand = new ListenerCommand<string>(( string seqeunceName ) =>
                    {
                        switch (seqeunceName)
                        {
                            case "MainMenu":
                                IsMainMenuOpen = true;
                                IsToolMenuOpen = false;           
                                break;
                            case "ToolMenu":
                                IsMainMenuOpen = false;
                                IsToolMenuOpen = true;
                                break;
                            case "LauncerMenu":
                                IsMainMenuOpen = false;
                                using( var vm =  new ViewModel.Menu.LauncherMenuViewModel(this, CharacterVM.ScreenSize))
                                {
                                    Messenger.Raise(new TransitionMessage(vm, "LauncherItems"));
                                }     
                                break;
                            case "SettingMenu":
                                using (var vm = new ViewModel.SettingViewModel())
                                {
                                    Messenger.Raise(new TransitionMessage(vm, "Setting"));
                                }
                                break;
                            case "Timer":
                                using (var vm = new Tool.Timer.TimerSettingViewModel(CharacterVM))
                                {
                                    Messenger.Raise(new TransitionMessage(vm, "TimerSetting"));
                                }
                                break;
                            case "Codic":
                                try
                                {
                                    using (var vm = new Tool.Translate.TranslateViewModel())
                                    {
                                        Messenger.Raise(new TransitionMessage(vm, "CodicWindow"));
                                    }
                                }
                                catch (NullReferenceException)
                                {
                                    MessageBox.Show(Properties.Resources.MenuItem_ErrorCoidcAccesToken);
                                }
                                break;
                            case "Talk":
                                DesktopCharacter.Model.CharacterNotify.Instance.Talk("にゃーん");
                                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                                break;
                            case "MenuClose":
                                IsMainMenuOpen = false;
                                Task.Run(async () =>
                                {
                                    await Task.Delay(200);
                                    //!< アニメーション終了してからウィンドウを閉じるために遅延させている
                                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                                });
                                
                                break;
                            case "Close":
                                CharacterVM.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                                break;


                        }
                        CurrentSeqnecen = seqeunceName;
                    });
                }
                return _moveToCommand;
            }
        }

        public void Initialize()
        {
            Messenger.Raise(new ReszieMessage("WindowResizeMessage", CharacterVM.ScreenSize));
        }
    }
}
