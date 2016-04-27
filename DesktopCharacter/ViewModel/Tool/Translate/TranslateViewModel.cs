using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Service.Codic;
using System.Collections.ObjectModel;

namespace DesktopCharacter.ViewModel.Tool.Translate
{

    class TranslateResultModel : Livet.NotificationObject
    {
        private ObservableCollection<TranslateResultModel> _list = new ObservableCollection<TranslateResultModel>(  );
        public ObservableCollection<TranslateResultModel> List { get { return _list; } }

        public TranslateResultModel()
        {

        }

        public TranslateResultModel(string text)
        {
            Text = text;
        }

        string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; this.RaisePropertyChanged("Text"); }
        }
    }

    class TranslateViewModel : Livet.ViewModel
    {

        private TranslateResultModel _translateResultModel = new TranslateResultModel();
        public TranslateResultModel ResultModel
        {
            get { return _translateResultModel; }
            set
            {
                _translateResultModel = value;
                this.RaisePropertyChanged("ResultModel");
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
              _text = value;
                this.RaisePropertyChanged("Text");
            }   
        }

        private CodicService _codicService;

        public TranslateViewModel()
        {
            _codicService = new CodicService();
        }

        public async void TranslateRun()
        {
            ResultModel.List.Clear();
            var result = await _codicService.translateAsync(Text);
            result.words[0].candidates.ForEach(e => _translateResultModel.List.Add( new TranslateResultModel(e.text)));
        }
    }
}
