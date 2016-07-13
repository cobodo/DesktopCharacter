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

namespace DesktopCharacter.ViewModel.Menu
{
    class MenuItemViewModel : Livet.ViewModel
    {
        public CharacterViewModel CharacterVM
        {
            set; private get;
        }

        public MenuItemViewModel()
        {

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
                        DesktopCharacter.Model.CharacterNotify.Instance.Talk("バカなの？バカなの？バカなの？バカなの？aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
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
                        MessageBox.Show("Coidcのアクセストークンが設定されていません");
                    }
                }));
            }
        }

    }
}
