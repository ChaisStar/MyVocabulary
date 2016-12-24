using System;
using System.Collections.Generic;

namespace MyVocabulary.Model.TranslateEngines
{
    public class MicrosoftBingEngine :ITranslateEngine
    {

        public IEnumerable<ILanguage> Languages => throw new NotImplementedException();

        public string TranslationResult(string inputText, ILanguage resultLanguage)
        {
            throw new NotImplementedException();
        }
    }
}
