using data_access.Entities;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private void deleteOlympiadButton(object o)
        {
            if (MessageBox.Show("Are you sure you want to delete this olympiad?", "Delete olympiad",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.No) return;
            BOlympiadForEdit?.SportsmanAward.Clear();
            unitOW.Olympiads.Update(BOlympiadForEdit);
            unitOW.Olympiads.Delete(BOlympiadForEdit);
            unitOW.Save();
            olmp = null;
            sptms = null;
            spAwOl = null;
            OnPropertyChanged("Olympiads");
            OnPropertyChanged("Sportsmans");
            OnPropertyChanged("CountryResult");
            OnPropertyChanged("MedalTable");
        }

        private void editOlympButton(object o) => ModifyOlimpiad(false);
        public RelayCommand DeleteOlympButton => new((o) => deleteOlympiadButton(o));
        public RelayCommand EditOlympButton => new((o) => editOlympButton(o));

    }
}
