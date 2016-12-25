using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MyVocabulary.Model.EntityFramework;
using MyVocabulary.Model.TranslateEngines;

namespace MyVocabulary.ViewModel
{
    public class AddWindowModel:ViewModelBase
    {
        private readonly ITranslateEngine translateEngine;

        public AddWindowModel()
        {
            translateEngine = new YandexEngine();
            languages = translateEngine.Languages;
            currentLanguage = languages.FirstOrDefault(l => l.LanguageName == "Russian");
        }

        private RelayCommand translateCommand;
        public RelayCommand TranslateCommand
        {
            get
            {
                return translateCommand ?? (translateCommand = new RelayCommand(obj =>
                {
                    TranslateWord = translateEngine.TranslationResult(StartWord, currentLanguage);
                }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(obj =>
                {
                    using (var context = new VocabularyContext())
                    {
                        context.Words.Add(new Word
                        {
                            StartWord = startWord,
                            TranslateWord = translateWord
                        });
                        context.SaveChanges();
                        CloseAction();
                    }
                }));
            }
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get { return closeCommand ?? (closeCommand = new RelayCommand(obj => CloseAction())); }
        }

        private IEnumerable<ILanguage> languages;
        public IEnumerable<ILanguage> Languages
        {
            get { return languages; }
            set
            {
                languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private ILanguage currentLanguage;
        public ILanguage CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                OnPropertyChanged(nameof(CurrentLanguage));
            }
        }

        private string startWord;
        public string StartWord
        {
            get { return startWord; }
            set
            {
                startWord = value;
                OnPropertyChanged(nameof(StartWord));
            }
        }

        private string translateWord;
        public string TranslateWord
        {
            get { return translateWord; }
            set
            {
                translateWord = value;
                OnPropertyChanged(nameof(TranslateWord));
            }
        }
    }
}
