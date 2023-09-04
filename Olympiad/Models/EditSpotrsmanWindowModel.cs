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

        private Sport bESSport;

        public Sport BESSport
        {
            get => bESSport;
            set 
            {
                bESSport = value;
                OnPropertyChanged("EditAllSportsmans");
            }
        }

        private Country bESCountry;

        public Country BESCountry
        {
            get => bESCountry;
            set
            {
                bESCountry = value;
                OnPropertyChanged("EditAllSportsmans");
            }
        }
        public IEnumerable<Sportsman> EditAllSportsmans => AllSportsmans.Where(x=> (BESSport?.Id == -1 || x.SportId == BESSport?.Id) && (BESCountry?.Id == -1 || x.CountryId == BESCountry?.Id));

        public RelayCommand DeleteButton => new((o)=> deleteButton(o));
        public RelayCommand EditButton => new((o) => editButton(o));
    }
}
