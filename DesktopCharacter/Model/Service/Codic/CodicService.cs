using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Util;
using System.IO;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Service.Codic.Format;

namespace DesktopCharacter.Model.Service.Codic
{
    class CodicService
    {
        private CodicUser _codicRepository;

        public CodicService()
        {
            var codicRepository = ServiceLocator.Instance.GetInstance<CodicRepository>();
            _codicRepository = codicRepository.Load();
        }

        /// <summary>
        /// テキストをCodicに投げて翻訳を受け取ります
        /// </summary>
        /// <param name="text"></param>
        public async Task<CodictTanslateResult> GetTranslateAsync(string text)
        {
            CodicAPI api = new CodicAPI();

            ParameterBuild parameter = new ParameterBuild();
            parameter.Parameter.Add("text", text);
            parameter.Parameter.Add("casing", _codicRepository.Casing);

            CodictTanslateResult result = await api.GetTranslateAscyn(parameter.Convert(), _codicRepository.Token);
            return result;
        }

        public async Task<List<CodicProject>> GetUserProjectsAync(string text)
        {
            CodicAPI api = new CodicAPI();
            var result = await api.GetUserProjectsAync(_codicRepository.Token);
            return result;
        }
    }
}
