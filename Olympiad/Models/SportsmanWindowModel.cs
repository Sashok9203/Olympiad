using data_access.Entities;
using Google.Protobuf.WellKnownTypes;
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

        private int selectedSportIndex;

        public int SelectedSportIndex
        {
            get { return selectedSportIndex; }
            set 
            {
                selectedSportIndex = value;
                OnPropertyChanged("EditWindowComboBoxOlympiads");
                EditableSportsman?.AwardOlympiads?.Clear();
            }
        }


        private List<Gender>? gndr = null;

        private List<Gender> genders => gndr ??= unitOW.Genders.Get().ToList();

       

        

        private List<Award>? awd = null;

        private List<Award> awards => awd ??= unitOW.Awards.Get().ToList();

        public IEnumerable<Award> Awards => awards;

        public IEnumerable<Olympiad_> EditWindowComboBoxOlympiads => olympiads.Where(x => x.SeasonId == EditableSportsman?.Sport?.SeasonId && !EditableSportsman.AwardOlympiads.Any(y=>y.Olympiad.Id == x.Id));

        public IEnumerable<Gender> Genders => genders;

        public IEnumerable<SportsmanAwardOlympiad>? EditableSportsmanAwardsOlympiads => EditableSportsman?.AwardOlympiads.ToArray();

        public Olympiad_? SelectedOlympiad { get; set; }

        public Award? SelectedAward { get; set; }



        public RelayCommand Save => new((o) =>save(o),(o=> !string.IsNullOrEmpty(EditableSportsman?.Name) 
                                                                              && !string.IsNullOrEmpty(EditableSportsman.Surname)
                                                                              && EditableSportsman.Sport!=null 
                                                                              && EditableSportsman.Country!=null
                                                                              && EditableSportsman.Gender!= null));

        public RelayCommand Add => new((o) => addAwardOlympiad(o),(o)=> SelectedOlympiad != null && SelectedAward!=null);


        private void save(object o) => SportsmanWindow.DialogResult = true;

        private void addAwardOlympiad(object o)
        {
            SportsmanAwardOlympiad temp = new() { Olympiad = SelectedOlympiad, Award = SelectedAward };
            EditableSportsman?.AwardOlympiads.Add(temp);
           
            SelectedOlympiad = null;
            SelectedAward = null;
            OnPropertyChanged("SelectedOlympiad");
            OnPropertyChanged("SelectedAward");
            OnPropertyChanged("EditableSportsmanAwardsOlympiads");
            OnPropertyChanged("EditWindowComboBoxOlympiads");
        }



        private void addSportsman(object o)
        {
            EditableSportsman = new ();
            sports?.RemoveAt(0);
            olympiads?.RemoveAt(0);
            SportsmanWindow = new() { DataContext = this };
            if (SportsmanWindow.ShowDialog() == true)
            {
                unitOW.Sportsmans.Insert(EditableSportsman);
                unitOW.Save();
                sptms = null;
                spAwOl = null;
                OnPropertyChanged("Sportsmans");
                OnPropertyChanged("CountryResult");
                OnPropertyChanged("MedalTable");
            }
            countries?.Insert(0, new() { Name = "All",Id = -1});
            sports?.Insert(0, new() { Name = "All", Id = -1 });
            olympiads?.Insert(0, new() { Id = -1 });
        }
    }
}
