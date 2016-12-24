using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using MyVocabulary.Model.EntityFramework;
using MyVocabulary.Model.TranslateEngines;

namespace MyVocabulary.ViewModel
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public MainWindowModel()
        {
            using (var context = new VocabularyContext())
            {
                Words = new ObservableCollection<Word>(context.Words);
                translator = new YandexEngine();
                languages = translator.Languages;
                currentLanguage = languages.FirstOrDefault(l => l.LanguageName == "Russian");
            }
        }

        #region RelayCommands
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    var word = new Word
                    {
                        StartWord = this.StartWord,
                        TranslateWord = this.TranslateWord
                    };
                    Words.Add(word);
                    currentWord = word;
                }));
            }
        }

        private RelayCommand translateCommand;
        public RelayCommand TranslateCommand
        {
            get
            {
                return translateCommand ?? (translateCommand = new RelayCommand(obj =>
                {
                    TranslateWord = translator.TranslationResult(StartWord, currentLanguage);
                }));
            }
        }
        #endregion RelayCommands

        #region Translate

        private ITranslateEngine translator;

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

        #endregion Translate

        #region Vocabulary

        private Word currentWord;
        public Word CurrentWord
        {
            get { return currentWord; }
            set
            {
                currentWord = value;
                OnPropertyChanged(nameof(CurrentWord));
            }
        }

        private ObservableCollection<Word> words;
        public ObservableCollection<Word> Words
        {
            get { return words; }
            set
            {
                words = value;
                OnPropertyChanged(nameof(Words));
            }
        }
        #endregion Vocabulary

        #region INotifyPropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion INotifyPropertyChanged Realization
    }
}