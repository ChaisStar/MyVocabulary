using System.Collections.Generic;

namespace MyVocabulary.Model.TranslateEngines
{
    public interface ITranslateEngine
	{
		IEnumerable<ILanguage> Languages { get; }

        string TranslationResult(string inputText, ILanguage resultLanguage);
	}
}
