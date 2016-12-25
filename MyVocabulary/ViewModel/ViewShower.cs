using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyVocabulary.View;

namespace MyVocabulary.ViewModel
{
    public class ViewShower
    {
        public static void ShowDialog(ViewShowerWindowTypes viewShowerWindowType, Action<bool?> closeAction= null)
        {
            Window window = null;
            ViewModelBase dataContext = null;
            switch (viewShowerWindowType)
            {
                case ViewShowerWindowTypes.AddWindow:
                    window = new AddWindow();
                    dataContext = new AddWindowModel();
                    break;
                case ViewShowerWindowTypes.SettingsWindow:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (window == null) return;

            window.DataContext = dataContext;
            if (dataContext.CloseAction == null)
                dataContext.CloseAction += () => window.Close();
            window.Closed += (s, e) => closeAction?.Invoke(window.DialogResult);
            window.ShowDialog();
        }
    }
}
