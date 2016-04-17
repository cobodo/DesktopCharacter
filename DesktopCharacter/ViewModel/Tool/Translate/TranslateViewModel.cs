using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Service.Codic;

namespace DesktopCharacter.ViewModel.Tool.Translate
{
    class TranslateViewModel : Livet.ViewModel
    {

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

        private Livet.Commands.ViewModelCommand _translateCommand;
        public Livet.Commands.ViewModelCommand TranslateCommand
        {
            get
            {
                return _translateCommand ?? (_translateCommand = new Livet.Commands.ViewModelCommand(
                async () =>
                {
                    await _codicService.translateAsync( Text );
                }));
            }
        }

    }
}
