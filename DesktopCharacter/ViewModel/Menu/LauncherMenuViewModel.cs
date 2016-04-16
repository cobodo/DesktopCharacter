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

namespace DesktopCharacter.ViewModel.Menu
{
    class LauncherMenuViewModel : Livet.ViewModel
    {
        private ObservableCollection<LauncherMenuViewModel> mChildren;      //!< 現在の子供
        public ObservableCollection<LauncherMenuViewModel> Children
        {
            get { return this.mChildren; }
        }

        public string Headder { get; set; }
        public ViewModelCommand Command { get; private set; }
        public object Focus { get; set; }

        public LauncherMenuViewModel(string headder, ViewModelCommand command)
        {
            Headder = headder;
            Command = command;
        }

        public LauncherMenuViewModel()
        {
            this.mChildren = new ObservableCollection<LauncherMenuViewModel>(new LauncherMenuViewModel[] {
                new LauncherMenuViewModel("notepad.exe", new ViewModelCommand( () => { Process.Start("notepad.exe"); }) ),
                new LauncherMenuViewModel("追加", new ViewModelCommand(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Filter = "Setting Files|*.exe|All Files (*.*)|*.*";
                    bool? result = openFileDialog.ShowDialog();
                    if (result == true)
                    {
                        this.mChildren.Insert( this.mChildren.Count - 1,
                        new LauncherMenuViewModel
                        (
                            openFileDialog.SafeFileName, new ViewModelCommand( () => { Process.Start( openFileDialog.FileName ); } )
                        ) );
                    }
                })),
            });
        }
    }
}
