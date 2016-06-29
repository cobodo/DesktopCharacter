using DesktopCharacter.Model;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class CharacterName : Livet.NotificationObject
    {
        public CharacterName(string name, string modelJsonPath)
        {
            _fileName = name;
            _modelJsonPath = modelJsonPath;
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
                if (mIsSelected)
                {
                    CharacterPropertyNotify.Instance.CharacterLoad(_modelJsonPath);
                }
                this.RaisePropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// ファイル名
        /// </summary>
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; this.RaisePropertyChanged("FileName"); }
        }

        private string _modelJsonPath;
        public string ModelJsonPath
        {
            get { return _modelJsonPath; }
            set { _modelJsonPath = value; this.RaisePropertyChanged("ModelJsonPath"); }
        }
    }

    class CharacterSettingViewModel : Livet.ViewModel
    {
        private BabumiConfig _babumiConfig;
        /// <summary>
        /// 読み込みファイル一覧
        /// </summary>
        private ObservableCollection<CharacterName> _listCollection;
        public ObservableCollection<CharacterName> ListCollection
        {
            get { return this._listCollection; }
        }

        public CharacterSettingViewModel()
        {
            _listCollection = new ObservableCollection<CharacterName>();
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            _babumiConfig = repo.GetConfig();
            Reload();
        }

        private void Reload()
        {
            _listCollection.Clear();
            string[] dirs = Directory.GetDirectories(_babumiConfig.Live2DResourceDir);
            if (dirs.Length == 0)
            {
                return;
            }
            foreach (var directory in dirs)
            {
                //!< 下の階層ファイルを検索して*.model.jsonのパスを探す
                string[] files = System.IO.Directory.GetFiles(directory, "*", System.IO.SearchOption.AllDirectories);
                foreach( var file in files)
                {
                    if ( file.Contains( ".model.json" ) )
                    {
                        _listCollection.Add(new CharacterName(System.IO.Path.GetFileName(directory), file));
                        break;
                    }
                }
            }
        }

        private ViewModelCommand _reloadCommand;
        public ViewModelCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new ViewModelCommand(() =>
                    {
                        Reload();
                    });
                }
                return _reloadCommand;
            }
        }
    }
}
