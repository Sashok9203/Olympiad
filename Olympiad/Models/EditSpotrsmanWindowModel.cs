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
        private void deleteButton(object o)
        {
            if (MessageBox.Show("Are you sure you want to delete this sportsman?","Delete sportsman", 
                MessageBoxButton.YesNo,MessageBoxImage.Warning,MessageBoxResult.No,MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.No ) return;
            BSportsmanForEdit?.AwardOlympiads.Clear();
            unitOW.Sportsmans.Update(BSportsmanForEdit);
            unitOW.Sportsmans.Delete(BSportsmanForEdit);
            unitOW.Save();
            sptms = null;
            spAwOl = null;
            OnPropertyChanged("AllSportsmans");
            OnPropertyChanged("Sportsmans");
            OnPropertyChanged("EditAllSportsmans");
            OnPropertyChanged("CountryResult");
            OnPropertyChanged("MedalTable");
        }


        private void editButton(object o) => ModifySportsman(false);


        public IEnumerable<Sportsman> EditAllSportsmans => AllSportsmans.Where(x=> (BSport?.Id == -1 || x.SportId == BSport?.Id) && (BCountry?.Id == -1 || x.CountryId == BCountry?.Id));

        public RelayCommand DeleteButton => new((o)=> deleteButton(o));
        public RelayCommand EditButton => new((o) => editButton(o));
    }
}
