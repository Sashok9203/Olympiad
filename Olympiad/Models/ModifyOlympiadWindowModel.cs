﻿using data_access.Entities;
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

        private List<City>? sts = null;

        public  List<City>? cities => sts ??= unitOW.Cities.Get().OrderBy(x => x.Name).ToList();

        private List<Season>? ssn = null;
        public IEnumerable<Season>? seasons => ssn ??= unitOW.Seasons.Get().ToList();

        private int bYear;

        private void saveOlympiadButton(object o)
        {

            if (MessageBox.Show("Are you sure?", "Save changes",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No,
                   MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                modifyOlympiadWindow.DialogResult = true;
        }

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

        private void ModifyOlimpiad(bool isNew)
        {
            modifyOlympiadWindow = new() { DataContext = this };
            setOlympiadBValues();
            if (modifyOlympiadWindow.ShowDialog() == true)
            {
                if (isNew) unitOW.Olympiads.Insert(getNewOlympiad());
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
        }

        public IEnumerable<Season>? Seasons => seasons.Where(x=>!x.Olympiads.Any(y => y.Year == BYear));

        public IEnumerable<City>? Cities => cities;

        

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

        public IEnumerable<Sport>? OlympiadSports => Sports?.Where(x => x.Id ==-1 || x.SeasonId == BSeason?.Id);

        public IEnumerable<Sportsman> OlympiadSportsmans => AllSportsmans.Where(x => idFilter(x.SportId , SportFilter?.Id) 
                                                                                         && idFilter(x.CountryId, CountryFilter?.Id)
                                                                                         && x.Sport.SeasonId == BSeason?.Id
                                                                                         && !BAwardOlympiads.Any(y=>y.Sportsman.Id == x.Id)
                                                                                         && BYear > x.Birthday.Year + 16);


        public RelayCommand AddSportsmanAward => new((o) => addAwardSportsmen(o), (o) => BOlympiadSportsman != null);

        public RelayCommand SaveOlympiad => new((o) => saveOlympiadButton(o), (o => BYear >= 1896
                                                                              && BCity !=null
                                                                              && BSeason != null));
        //Binding Properties

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
                        if (bYear < item.Sportsman.Birthday.Year + 16) temp.Add(item);
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
