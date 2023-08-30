using data_access.Entities;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private EditSpotrsmanWindow? SportsmanWindow;

        public Sportsman? EditableSportsman { get; set; }

        private List<Gender>? gndr;

        private List<Gender> genders => gndr ??= unitOW.Genders.Get().ToList();

        public IEnumerable<Gender> Genders => genders;

        public RelayCommand Save => new((o) =>save(o),(o=> !string.IsNullOrEmpty(EditableSportsman.Name) 
                                                                              && !string.IsNullOrEmpty(EditableSportsman.Surname)
                                                                              && EditableSportsman.Sport!=null 
                                                                              && EditableSportsman.Country!=null
                                                                              && EditableSportsman.Gender!= null));

        private void save(object o) => SportsmanWindow.DialogResult = true;
        
        


        private void addSportsman(object o)
        {
            EditableSportsman = new ();
            countries?.RemoveAt(0);
            sports?.RemoveAt(0);
            SportsmanWindow = new() { DataContext = this };
            if (SportsmanWindow.ShowDialog() == true)
            {
                unitOW.Sportsmans.Insert(EditableSportsman);
                unitOW.Save();
                sptms = null;
                OnPropertyChanged("Sportsmans");
            }
            countries?.Insert(0, new() { Name = "All",Id = -1});
            sports?.Insert(0, new() { Name = "All", Id = -1 });
        }
    }
}
