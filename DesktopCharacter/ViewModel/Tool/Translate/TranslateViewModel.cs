using System;
using System.Linq;
using DesktopCharacter.Model.Service.Codic;
using System.Collections.ObjectModel;
using System.Windows;
using Livet.Messaging.Windows;

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
            try
            {
                _codicService = new CodicService();
            }
            catch
            {
                throw;
            }
        }

        public void TranslateRun()
        {
            _codicService.GetTranslateAsync(Text).Subscribe
            (
                value =>
                {
                    ResultModel.List.Clear();
                    var result = value.FirstOrDefault();
                    if (result != null && result.successful)
                    {
                        if (result.words.Count >= 1)
                        {
                            result.words[0].candidates.ForEach(e => _translateResultModel.List.Add(new TranslateResultModel(e.text_in_casing)));
                        }
                        else
                        {
                            _translateResultModel.List.Add(new TranslateResultModel(result.translated_text));
                        }
                    }
                },
                error =>
                {
                    System.Console.WriteLine(error.Message);
                },
                () => System.Console.WriteLine("TranslateRun(...) Complete")
            );
        }

        public void Close()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
    }
}
