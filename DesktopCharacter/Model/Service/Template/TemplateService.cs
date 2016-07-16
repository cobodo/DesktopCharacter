using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NLog;
using RazorEngine;
using RazorEngine.Templating;

namespace DesktopCharacter.Model.Service.Template
{
    /// <summary>
    /// セリフ生成のためのテンプレート機能を提供するサービス
    /// プラグインごとに名前空間を持ち、その中で固有のIDでテンプレートを識別します
    /// </summary>
    interface ITemplateService
    {
        /// <summary>
        /// テンプレートを登録する
        /// </summary>
        /// <param name="templateNamespace">テンプレート識別の名前空間</param>
        /// <param name="id">名前空間に固有のID</param>
        /// <param name="defaultTemplate">デフォルトのテンプレート</param>
        void RegisterTemplate(string templateNamespace, string id, string defaultTemplate);

        /// <summary>
        /// テンプレートから文字列を生成します
        /// </summary>
        /// <param name="templateNamespace">テンプレート識別の名前空間</param>
        /// <param name="id">名前空間固有のID</param>
        /// <param name="variables">テンプレートに与える引数</param>
        /// <returns>テンプレートから生成された文字列</returns>
        string ProcessTemplate(string templateNamespace, string id, dynamic variables);
    }

    class TemplateService: ITemplateService
    {
        private readonly IDictionary<string, string> _templates = new SortedDictionary<string, string>();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void RegisterTemplate(string templateNamespace, string id, string defaultTemplate)
        {
            var templateKey = CreateTemplateKey(templateNamespace, id);
            _logger.Info("[Template] Register: templateNamespace={0}, id={1}, defaultTemplate={2}", templateKey, id, defaultTemplate);
            _templates.Add(templateKey, defaultTemplate);
        }

        public string ProcessTemplate(string templateNamespace, string id, object variables)
        {
            var templateKey = CreateTemplateKey(templateNamespace, id);
            var template = _templates[templateKey];
            if (template == null)
            {
                throw new KeyNotFoundException("template id not found: " + templateKey);
            }

            var result = Engine.Razor.RunCompile(template, templateKey, null, variables);

            return result;
        }

        private string CreateTemplateKey(string templateNamespace, string id)
        {
            var reg = "^[a-zA-Z0-9\\.-]{5,64}?$";
            if (!Regex.IsMatch(templateNamespace, reg))
            {
                throw new ArgumentException("templateNamespace is an illegal format.\n" +
                                            "Please follow the following format.\" + reg + \"");
            }
            if (!Regex.IsMatch(id, reg))
            {
                throw new ArgumentException("id is an illegal format.\n" +
                                            "Please follow the following format.\" + reg + \"");
            }
            return templateNamespace + ":" + id;
        }
    }
}
