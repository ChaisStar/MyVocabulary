using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVocabulary.ViewModel
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        #region INotifyPropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion INotifyPropertyChanged Realization
    }
}
