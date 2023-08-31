using data_access.Entities;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private void deleteButton(object o)
        {
            var olympiadsAwardsToDelete = SpAwOlympiads.Where(x => x.SportsmanId == SlectedSportsmanForEdit.Id);
            foreach (var item in olympiadsAwardsToDelete)
                unitOW.SAOlympiad.Delete(item);
            unitOW.Sportsmans.Delete(SlectedSportsmanForEdit);
           
            unitOW.Save();
            sptms = null;
            spAwOl = null;
            OnPropertyChanged("AllSportsmans");
            OnPropertyChanged("Sportsmans");
            OnPropertyChanged("CountryResult");
            OnPropertyChanged("MedalTable");
        }

        private void editButton(object o) => ModifySportsman(false);
        

        private void exitButton(object o) => editSpotrsmanWindow.DialogResult = true;
       

        public RelayCommand DeleteButton => new((o)=> deleteButton(o));
        public RelayCommand EditButton => new((o) => editButton(o));
        public RelayCommand ExitButton => new((o) => exitButton(o));

    }
}
