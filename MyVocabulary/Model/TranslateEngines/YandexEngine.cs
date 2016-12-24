using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using MyVocabulary.Model.TranslateEngines.YandexClasses;
using Newtonsoft.Json;

namespace MyVocabulary.Model.TranslateEngines
{
    public class YandexEngine : ITranslateEngine
    {
        private readonly List<YandexLanguage> languages;
        private const string Url = "https://translate.yandex.net/api/v1.5/tr.json/";
        private readonly string key = $"key={ConfigurationManager.AppSettings["YandexApiKey"]}";

        public YandexEngine()
        {
            using (var client = new WebClient())
            {
                var result = client.DownloadString(new Uri($"{Url}getLangs?{key}&ui=en", UriKind.Absolute));
                var langs = JsonConvert.DeserializeObject<YandexLanguagesResult>(result).Langs;
                languages = langs.Select(x => new YandexLanguage { LanguageCode = x.Key, LanguageName = x.Value }).ToList();
            }
        }

        public IEnumerable<ILanguage> Languages => languages;

        public string TranslationResult(string inputText, ILanguage resultLanguage)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(new Uri($"{Url}translate?{key}&text={inputText}&lang={(resultLanguage as YandexLanguage).LanguageCode}", UriKind.Absolute));
                var yandexResponse = JsonConvert.DeserializeObject<YandexResponse>(result);
                return string.Join(Environment.NewLine, yandexResponse.Text);
            }
        }
    }
}
