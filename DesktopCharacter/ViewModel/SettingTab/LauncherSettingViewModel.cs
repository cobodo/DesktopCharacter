using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Util.Behavior;
using Livet.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class LauncherItem : Livet.NotificationObject
    {
        public LauncherItem(string path)
        {
            Path = path;
            DisplayName = System.IO.Path.GetFileNameWithoutExtension(path);
            Icon = Imaging.CreateBitmapSourceFromHIcon(System.Drawing.Icon.ExtractAssociatedIcon(path).Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// 選択項目 選択された場合trueになる
        /// </summary>
        private bool mIsSelected;
        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// アイコン
        /// </summary>
        private ImageSource _icon;
        public ImageSource Icon
        {
            get { return _icon; }
            set { _icon = value; this.RaisePropertyChanged("Icon"); }
        }

        /// <summary>
        /// ファイル名（拡張子なし名）
        /// </summary>
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; this.RaisePropertyChanged("DisplayName"); }
        }

        /// <summary>
        /// パス名
        /// </summary>
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; this.RaisePropertyChanged("Path"); }
        }
    }

    class LauncherSettingViewModel : Livet.ViewModel, SettingViewModelBase
    {
        /// <summary>
        /// 読み込みファイル一覧
        /// </summary>
        private ObservableCollection<LauncherItem> _listCollection;
        public ObservableCollection<LauncherItem> ListCollection
        {
            get { return this._listCollection; }
        }

        /// <summary>
        /// コンフィグファイルのリポジトリ
        /// </summary>
        private BabumiConfigRepository _babumiConfigRepository;

        public LauncherSettingViewModel()
        {
            _listCollection = new ObservableCollection<LauncherItem>();
            _babumiConfigRepository = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            var config = _babumiConfigRepository.GetConfig();
            LaucnherItemAdd(config.Dataset.Select(e => e.Path).ToArray());
        }

        private ListenerCommand<object> _dropPostCommand;
        public ListenerCommand<object> DropPostCommand
        {
            get
            {
                if (_dropPostCommand == null)
                {
                    _dropPostCommand = new ListenerCommand<object>((object sender) =>
                    {
                        string[] files = sender as string[];
                        LaucnherItemAdd(files);
                    });
                }
                return _dropPostCommand;
            }
        }

        private Livet.Commands.ViewModelCommand _addCommand;
        public Livet.Commands.ViewModelCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new Livet.Commands.ViewModelCommand(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Filter = "Setting Files|*.exe|All Files (*.*)|*.*";
                    bool? result = openFileDialog.ShowDialog();
                    if (result == true)
                    {
                        ListCollection.Add(new LauncherItem(openFileDialog.FileName));
                    }
                }));
            }
        }

        private Livet.Commands.ViewModelCommand _pathResetCommand;
        public Livet.Commands.ViewModelCommand PathResetCommand
        {
            get
            {
                return _pathResetCommand ?? (_pathResetCommand = new Livet.Commands.ViewModelCommand(() =>
                {
                    var list = ListCollection.Where(e => e.IsSelected).ToList();
                    if (list.Any())
                    {
                        var firstValue = list.FirstOrDefault();
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title =  firstValue.DisplayName + " " + Properties.Resources.LauncherSettingTab_OpenFileDialog;
                        openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(firstValue.Path);
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.Filter = "Setting Files|*.exe|All Files (*.*)|*.*";
                        bool? result = openFileDialog.ShowDialog();
                        if (result == true)
                        {
                            ListCollection.Remove(firstValue);
                            ListCollection.Add(new LauncherItem(openFileDialog.FileName));
                        }
                    }
                }));
            }
        }

        private Livet.Commands.ViewModelCommand _deleteCommand;
        public Livet.Commands.ViewModelCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new Livet.Commands.ViewModelCommand(() =>
                {
                    var list = ListCollection.Where(e => e.IsSelected).ToList();

                    foreach (var o in list)
                    {
                        ListCollection.Remove(o);
                    }
                }));
            }
        }

        private void LaucnherItemAdd( string[] items )
        {
            foreach (var item in items)
            {
                if (item.Contains(".exe") || item.Contains(".lnk"))
                {
                    ListCollection.Add(new LauncherItem(item));
                }
            }
        }

        public void OnClose()
        {
            var config = _babumiConfigRepository.GetConfig();
            config.Dataset.Clear();
            foreach (var item in ListCollection)
            {
                config.Dataset.Add(new LauncherSettingsDataSet
                {
                    DisplayName = item.DisplayName,
                    Path = item.Path
                });
            }
            config.Dataset = config.Dataset.Distinct().ToList();
            _babumiConfigRepository.Save(config);
        }
    }
}
