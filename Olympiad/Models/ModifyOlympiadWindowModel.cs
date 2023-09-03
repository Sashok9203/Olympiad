using data_access.Entities;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private ModifyOlympiadWindow? modifyOlympiadWindow;

        private Sport? sportFilter;

        private Country? countryFilter;

        private Season? bSeason;

        private List<City> cts;

        private List<Season>? ssn = null;

        private int bYear;

        private bool isNew;

        private Olympiad_ getNewOlympiad()
        {
            Olympiad_ newOlympiad = new()
            {
                Year = BYear,
                City = BCity,
                Season = BSeason
            };
            if (BAwardOlympiads.Count > 0)
                foreach (var item in BAwardOlympiads)
                    newOlympiad.SportsmanAward.Add(item);
            return newOlympiad;
        }

        private void setOlympiadBValues(Olympiad_? olympiad = null)
        {

            BYear = olympiad?.Year ?? 0;
            BCity = olympiad?.City;
            BSeason = olympiad?.Season;
            BAwardOlympiads.Clear();
            if (olympiad?.SportsmanAward.Count > 0)
                foreach (var item in olympiad.SportsmanAward)
                    BAwardOlympiads.Add(item);
           
        }

        private void setOlympiadFromBValues(Olympiad_ olympiad)
        {

            olympiad.Year = BYear;
            olympiad.City  = BCity;
            olympiad.Season = BSeason;
            olympiad.SportsmanAward.Clear();
            if (BAwardOlympiads.Count > 0)
                foreach (var item in BAwardOlympiads)
                    olympiad.SportsmanAward.Add(item);

        }

        private void ModifyOlimpiad(bool newOlympiad)
        {
            modifyOlympiadWindow = new() { DataContext = this };
            if (newOlympiad) setOlympiadBValues();
            else setOlympiadBValues(BOlympiadForEdit);
            if (modifyOlympiadWindow.ShowDialog() == true)
            {
                if (newOlympiad) unitOW.Olympiads.Insert(getNewOlympiad());
                else
                {
                    setOlympiadFromBValues(BOlympiadForEdit);
                    unitOW.Olympiads.Update(BOlympiadForEdit);
                }
                unitOW.Save();
                olmp = null;
                sptms = null;
                spAwOl = null;

                if (!newOlympiad) OnPropertyChanged("AllSportsmans");
                OnPropertyChanged("ComboBoxOlympiad");
                OnPropertyChanged("TopCountry");
            }
        }

        private void addAwardSportsmen(object o)
        {
            BAwardOlympiads.Add(new() { Sportsman = BOlympiadSportsman, Award = BAward });
            BOlympiadSportsman = null;
            BAward = null;
            OnPropertyChanged("BAward");
            OnPropertyChanged("BOlympiadSportsman");
            OnPropertyChanged("OlympiadSportsmans");
        }

       

        public Sport? SportFilter
        {
            get => sportFilter;
            set
            {
                sportFilter = value;
                if (sportFilter != null && countryFilter != null && bSeason != null)
                    OnPropertyChanged("OlympiadSportsmans");
            } 
        }

        public Country? CountryFilter 
        {
            get => countryFilter;
            set
            {
                countryFilter = value;
                if (sportFilter != null && countryFilter != null && bSeason != null) OnPropertyChanged("OlympiadSportsmans");
                
            }
        }

        public IEnumerable<Season>? seasons => ssn ??= unitOW.Seasons.Get().ToList();

        public IEnumerable<Season>? Seasons => seasons?.Where(x =>(!isNew && (x.Id == BSeason?.Id || !x.Olympiads.Any(y => y.Year == BYear))) || !x.Olympiads.Any(y => y.Year == BYear));

        public IEnumerable<City>? Cities => cts;


        public IEnumerable<Sport>? OlympiadSports => Sports?.Where(x => x.Id ==-1 || x.SeasonId == BSeason?.Id);

        public IEnumerable<Sportsman> OlympiadSportsmans => AllSportsmans.Where(x => idFilter(x.SportId , SportFilter?.Id) 
                                                                                         && idFilter(x.CountryId, CountryFilter?.Id)
                                                                                         && x.Sport.SeasonId == BSeason?.Id
                                                                                         && !BAwardOlympiads.Any(y=>y.Sportsman.Id == x.Id)
                                                                                         && BYear - x.Birthday.Year >= 16
                                                                                         && (BYear - x.Birthday.Year <= 39));


        public RelayCommand AddSportsmanAward => new((o) => addAwardSportsmen(o), (o) => BOlympiadSportsman != null);

        public RelayCommand SaveOlympiad => new((o) => saveButton(o), (o) => BYear >= 1896
                                                                              && BCity !=null
                                                                              && BSeason != null);
        //Binding Properties

        public Olympiad_? BOlympiadForEdit { get; set; }

        public Sportsman? BOlympiadSportsman { get; set; }

        public int  BYear
        {
            get => bYear;
            set
            {
                bYear = value;
                if (BAwardOlympiads.Count > 0)
                {
                    List<SportsmanAwardOlympiad> temp = new();
                    foreach (var item in BAwardOlympiads)
                    {
                        int age = bYear - item.Sportsman.Birthday.Year;
                        if (age < 16 || age > 39) temp.Add(item);
                    }
                    if (temp.Count > 0)
                        foreach (var item in temp)
                            BAwardOlympiads.Remove(item);
                }
                OnPropertyChanged("OlympiadSportsmans");
                OnPropertyChanged("Cities");
                OnPropertyChanged("Seasons");
            }
        }

       

        public City? BCity { get; set; }

        public Season? BSeason
        {
            get => bSeason;
            set
            {
               if (countryFilter != null) OnPropertyChanged("OlympiadSportsman");
               BAwardOlympiads.Clear();
               bSeason = value;
               OnPropertyChanged("OlympiadSports");
               OnPropertyChanged("Cities");
               OnPropertyChanged("OlympiadSportsmans");
            }
        }

    }
}
