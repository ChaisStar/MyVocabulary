using System.Collections.Generic;

namespace MyVocabulary.Model.TranslateEngines.YandexClasses
{
    public class YandexLanguagesResult
    {
        public string[] Dirs { get; set; }

        public Dictionary<string,string> Langs { get; set; }
        //public YandexLanguage[] Langs { get; set; }
    }
}
