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

namespace DesktopCharacter.ViewModel
{
    class MenuItemViewModel : Livet.ViewModel
    {
        private ModelView MainViewRef = Util.WindowInstance.MainInstnace;

        private ObservableCollection<MenuItemViewModel> mChildren;      //!< 現在の子供
        public ObservableCollection<MenuItemViewModel> Children
        {
            get { return this.mChildren; }
        }

        private string mHeadder;
        public string Headder
        {
            get { return mHeadder; }
            set
            {
                mHeadder = value;
                this.RaisePropertyChanged("Headder");
            }
        }

        public Livet.Commands.ViewModelCommand Command { get; private set; }

        public MenuItemViewModel(string headder, Livet.Commands.ViewModelCommand command)
        {
            Headder = headder;
            Command = command;
        }

        public MenuItemViewModel()
        {
            this.mChildren = new ObservableCollection<MenuItemViewModel>(new MenuItemViewModel[] {
                new MenuItemViewModel("notepad.exe", new ViewModelCommand( () => { Process.Start("notepad.exe"); }) ),
                new MenuItemViewModel("追加", new ViewModelCommand(() => 
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Filter = "Setting Files|*.exe|All Files (*.*)|*.*";
                    bool? result = openFileDialog.ShowDialog();
                    if (result == true)
                    {
                        this.mChildren.Insert( this.mChildren.Count - 1, 
                            new MenuItemViewModel
                            (
                                openFileDialog.SafeFileName, new ViewModelCommand( () => { Process.Start( openFileDialog.FileName ); } )
                            ) );
                    }
                })),
            });
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
                        Util.WindowInstance.TalkInstance.AddMessage("バカなの？バカなの？バカなの？バカなの？");
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
                    ? mCloseCommand = new ViewModelCommand(() => { MainViewRef.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close")); })
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
                        using (var vm = new ViewModel.TimerSettingModelView())
                        {
                            Messenger.Raise(new TransitionMessage(vm, "TimerSetting"));
                        }
                    });
                }
                return mTimerSettingOpenCommand;
            }
        }
    }
}
