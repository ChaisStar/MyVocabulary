using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using MyVocabulary.Model.EntityFramework;
using MyVocabulary.Model.TranslateEngines;

namespace MyVocabulary.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {
            UpdateWords();
        }

        private void UpdateWords()
        {
            using (var context = new VocabularyContext())
            {
                Words = new ObservableCollection<Word>(context.Words);
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
                    ViewShower.ShowDialog(ViewShowerWindowTypes.AddWindow, b =>
                    {
                        if (b == true) UpdateWords();
                    });
                }));
            }
        }
        #endregion RelayCommands

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
    }
}