﻿using DesktopCharacter.Model;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using Livet.Commands;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Util.Messenger.Message;
using DesktopCharacter.Util.Converter;

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
                    CharacterNotify.Instance.CharacterLoad(_modelJsonPath);
                    //!< 次回からこちらをロードする
                    var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
                    var config = repo.GetConfig();
                    config.ModelJsonPath = _modelJsonPath;
                    repo.Save(config);
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

    class CharacterSettingViewModel : Livet.ViewModel, SettingViewModelBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// ばぶみのコンフィグファイル
        /// </summary>
        private BabumiConfig _babumiConfig;
        /// <summary>
        /// 読み込みファイル一覧
        /// </summary>
        private ObservableCollection<CharacterName> _listCollection;
        public ObservableCollection<CharacterName> ListCollection
        {
            get { return this._listCollection; }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            set
            {
                _zoomLevel = _babumiConfig.ZoomLevel = value;
                _babumiConfig.AddZoomLevel(value);
                this.RaisePropertyChanged("CharacterScaleRate");
                try
                {
                    CharacterNotify.Instance.WindowResizeMessage(_babumiConfig.ZoomLevel);
                }
                catch (Exception e)
                {
                    Messenger.Raise(new CloseMessage(true, e.Message, "Error"));
                }
            }
            get { return _zoomLevel; }
        }

        private bool _topmostFlag;
        public bool TopmostFlag
        {
            set
            {
                _topmostFlag = _babumiConfig.Topmost = value;
                this.RaisePropertyChanged("TopmostFlag");
                CharacterNotify.Instance.TopMostMessage(_topmostFlag);
            }
            get { return _topmostFlag; }
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

        public CharacterSettingViewModel()
        {
            _listCollection = new ObservableCollection<CharacterName>();
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            _babumiConfig = repo.GetConfig();
            _topmostFlag = _babumiConfig.Topmost;
            _zoomLevel = _babumiConfig.ZoomLevel;
            Reload();
        }

        private void Reload()
        {
            _listCollection.Clear();

            try
            {
                var fileList = Util.File.DirectoryUtility.GetFileList(_babumiConfig.Live2DResourceDir, ".model.json");
                if (fileList.Count != 0)
                {
                    foreach (var file in fileList)
                    {
                        //!< name.model.jsonなので二回繰り返すとファイル名がもってこれる
                        _listCollection.Add(new CharacterName(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file)), file));
                    }
                }
            }
            catch (Exception)
            {
                logger.Error(Properties.Resources.CharacterSetting_ErrorLoadModelFileList);
            }
        }

        public void OnClose()
        {
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            repo.Save(_babumiConfig);
        }
    }
}
