using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Service.Codic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DesktopCharacter.ViewModel.Tool.Translate
{

    class TranslateResultModel : Livet.NotificationObject
    {
        private ObservableCollection<TranslateResultModel> _list = new ObservableCollection<TranslateResultModel>(  );
        public ObservableCollection<TranslateResultModel> List { get { return _list; } }

        public TranslateResultModel SelectItem
        {
            set
            {
                if( value == null)
                {
                    return;
                }
                Clipboard.SetText(value.Text);
            }
        }

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

        public void TranslateRun()
        {
            _codicService.GetTranslateAsync(Text).Subscribe
            (
                value =>
                {
                    ResultModel.List.Clear();
                    var result = value.FirstOrDefault();
                    if( result != null)
                    {
                        result.words[0].candidates.ForEach(e => _translateResultModel.List.Add(new TranslateResultModel(e.text_in_casing)));
                    }
                },
                error => System.Console.WriteLine(error.Message),
                () => System.Console.WriteLine("")
            );
            
        }
    }
}
