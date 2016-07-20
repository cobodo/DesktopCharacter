﻿using System;
using System.Collections.Generic;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Util.Web;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Service.Codic.Format;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Service.Codic
{
    class CodicService
    {
        private CodicUser _codicRepository;

        public CodicService()
        {
            var codicRepository = ServiceLocator.Instance.GetInstance<CodicRepository>();
            _codicRepository = codicRepository.Load();
            if (_codicRepository == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// テキストをCodicに投げて翻訳を受け取ります
        /// </summary>
        /// <param name="text"></param>
        public IObservable<List<CodictTanslateResult>> GetTranslateAsync(string text)
        {
            CodicAPI api = new CodicAPI();

            ParameterBuild parameter = new ParameterBuild();
            parameter.Parameter.Add("text", text);
            if (_codicRepository.Casing != string.Empty)
            {
                parameter.Parameter.Add("casing", _codicRepository.Casing);
            }
            return api.GetTranslateAscyn(parameter.Convert(), _codicRepository.Token);
        }

        /// <summary>
        /// Codicに登録してあるプロジェクトを取得します
        /// </summary>
        /// <returns></returns>
        public IObservable<List<CodicProject>> GetUserProjectsAync()
        {
            CodicAPI api = new CodicAPI();
            return api.GetUserProjectsAync(_codicRepository.Token);
        }
    }
}
