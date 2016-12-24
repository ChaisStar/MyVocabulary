using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using MyVocabulary.Model.TranslateEngines.TransltrClasses;
using Newtonsoft.Json;

namespace MyVocabulary.Model.TranslateEngines
{
    public class TransltrEngine : ITranslateEngine
    {
        private readonly List<TransltrLanguage> languages;

        public TransltrEngine()
        {
            using (var client = new WebClient())
            {
                var text = client.DownloadString(new Uri("http://www.transltr.org/api/getlanguagesfortranslate", UriKind.Absolute));
                languages = (JsonConvert.DeserializeObject<TransltrLanguage[]>(text)).ToList();
            }
        }

        public IEnumerable<ILanguage> Languages => languages;

        public string TranslationResult(string inputText, ILanguage resultLanguage)
        {
            const string url = "http://www.transltr.org/api/translate";
            using (var client = new WebClient())
            {
                if (resultLanguage is TransltrLanguage)
                {
                    var language = (TransltrLanguage) resultLanguage;
                    var pars = new NameValueCollection
                    {
                        {"text", inputText},
                        {"to", language.LanguageCode}
                    };
                    var response = Encoding.UTF8.GetString(client.UploadValues(url, pars));
                    var result = JsonConvert.DeserializeObject<TransltrResponse>(response);
                    return result.TranslationText;
                }
                else
                {
                    return "Cannot translate";
                }
            }
        }
    }
}
