using data_access.Entities;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private ModifyOlympiadWindow? modifyOlympiadWindow;

        private Sport? sportFilter;

        private Country? countryFilter;

        private Season? olympiadSeason;

        public Olympiad_ EditableOlympiad { get; set; }

        private List<City>? sts;

        public IEnumerable<City>? Cities => sts ??= unitOW.Cities.Get().OrderBy(x => x.Name).ToList();

        private List<Season>? ssn;

        private void saveOlympiadButton(object o) => modifyOlympiadWindow.DialogResult = true;

        public IEnumerable<Season>? Seasons => ssn ??= unitOW.Seasons.Get().ToList();

        public Sportsman? SelectedOlympiadSportsman { get; set; }

        public Season? OlympiadSeason
        {
            get => olympiadSeason;
            set
            {
                olympiadSeason = value;
                if (olympiadSeason != null)
                {
                    EditableOlympiad.Season = olympiadSeason;
                    OnPropertyChanged("OlympiadSports");
                    if (countryFilter != null) OnPropertyChanged("OlympiadSportsman");
                }
                else EditableOlympiad.SportsmanAward.Clear();
            }
        }
        public IEnumerable<SportsmanAwardOlympiad>? OlympiadSportsmanAwards => EditableOlympiad?.SportsmanAward.ToArray();
        private void addAwardSportsmen(object o)
        {
            SportsmanAwardOlympiad temp = new() { Sportsman = SelectedOlympiadSportsman, Award = SelectedAward };
            EditableOlympiad?.SportsmanAward.Add(temp);

            SelectedOlympiadSportsman = null;
            SelectedAward = null;
            OnPropertyChanged("SelectedOlympiadSportsman");
            OnPropertyChanged("SelectedAward");
            OnPropertyChanged("OlympiadSportsmanAwards");
            OnPropertyChanged("OlympiadSportsman");
        }

        public SportsmanAwardOlympiad SelectedOlympiadSportsmanAward { get; set; }

        public Sport? SportFilter
        {
            get => sportFilter;
            set
            {
                sportFilter = value;
                if (sportFilter != null && countryFilter != null && olympiadSeason != null) OnPropertyChanged("OlympiadSportsman");
            } 
        }

        public Country? CountryFilter 
        {
            get => countryFilter;
            set
            {
                countryFilter = value;
                if (sportFilter != null && countryFilter != null && olympiadSeason != null) OnPropertyChanged("OlympiadSportsman");
                
            }
        }

        public IEnumerable<Sport>? OlympiadSports => Sports?.Where(x => x.SeasonId == OlympiadSeason?.Id);

        public IEnumerable<Sportsman> OlympiadSportsman => AllSportsmans.Where(x => x.SportId == SportFilter?.Id 
                                                                                         && x.CountryId == CountryFilter?.Id 
                                                                                         && !EditableOlympiad.SportsmanAward.Any(y=>y.Sportsman.Id == x.Id));


        public RelayCommand AddSportsmanAward => new((o) => addAwardSportsmen(o), (o) => SelectedOlympiadSportsman != null);

        public RelayCommand SaveOlympiad => new((o) => saveOlympiadButton(o), (o => EditableOlympiad.Year >= 1896
                                                                              && EditableOlympiad.City !=null
                                                                              && EditableOlympiad.Season != null));

        private void ModifyOlimpiad(bool isNew)
        {
            EditableOlympiad = new();
            countries?.RemoveAt(0);
            sports?.RemoveAt(0);

            modifyOlympiadWindow = new() { DataContext = this };
            if (modifyOlympiadWindow.ShowDialog() == true)
            {
                if (isNew) unitOW.Olympiads.Insert(EditableOlympiad);
                else
                {
                    //copySportsman(EditableSportsman, SlectedSportsmanForEdit);
                    //unitOW.Sportsmans.Update(SlectedSportsmanForEdit);
                }
                unitOW.Save();
                olmp = null;
                sptms = null;
                spAwOl = null;
               
                if (!isNew) OnPropertyChanged("AllSportsmans");
                OnPropertyChanged("ComboBoxOlympiad");
            }

            countries?.Insert(0, new() { Name = "All", Id = -1 });
            sports?.Insert(0, new() { Name = "All", Id = -1 });

        }

    }
}
