using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyVocabulary.Model.EntityFramework
{
    public class Word:INotifyPropertyChanged
    {
        private int id;
        private string startWord;
        private string translateWord;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string StartWord
        {
            get { return startWord; }
            set
            {
                startWord = value;
                OnPropertyChanged(nameof(startWord));
            }
        }

        public string TranslateWord
        {
            get { return translateWord; }
            set
            {
                translateWord = value;
                OnPropertyChanged(nameof(TranslateWord));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
