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

        private List<City>? sts;

        private List<Season>? ssn;

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
            BAwardOlympiads.Clear();
            return newOlympiad;
        }

        private void ModifyOlimpiad(bool isNew)
        {
            countries?.RemoveAt(0);
            sports?.RemoveAt(0);

            modifyOlympiadWindow = new() { DataContext = this };
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

            countries?.Insert(0, new() { Name = "All", Id = -1 });
            sports?.Insert(0, new() { Name = "All", Id = -1 });

        }

        public IEnumerable<Season>? Seasons => ssn ??= unitOW.Seasons.Get().ToList();

        public IEnumerable<City>? Cities => sts ??= unitOW.Cities.Get().OrderBy(x => x.Name).ToList();

        

        private void addAwardSportsmen(object o)
        {
            BAwardOlympiads.Add(new() { Sportsman = BOlympiadSportsman, Award = BAward });
            BOlympiadSportsman = null;
            BAward = null;
            OnPropertyChanged("SelectedOlympiadSportsman");
            OnPropertyChanged("SelectedAward");
            OnPropertyChanged("OlympiadSportsman");
        }

        public SportsmanAwardOlympiad SelectedOlympiadSportsmanAward { get; set; }

        public Sport? SportFilter
        {
            get => sportFilter;
            set
            {
                sportFilter = value;
                if (sportFilter != null && countryFilter != null && bSeason != null)
                {
                    OnPropertyChanged("OlympiadSportsman");
                   
                }
            } 
        }

        public Country? CountryFilter 
        {
            get => countryFilter;
            set
            {
                countryFilter = value;
                if (sportFilter != null && countryFilter != null && bSeason != null) OnPropertyChanged("OlympiadSportsman");
                
            }
        }

        public IEnumerable<Sport>? OlympiadSports => Sports?.Where(x => x.SeasonId == BSeason?.Id);

        public IEnumerable<Sportsman> OlympiadSportsman => AllSportsmans.Where(x => x.SportId == SportFilter?.Id 
                                                                                         && x.CountryId == CountryFilter?.Id 
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
                OnPropertyChanged("OlympiadSportsman");
            }
        }

       

    public City BCity { get; set; }

       

        public Season? BSeason
        {
            get => bSeason;
            set
            {
               if (countryFilter != null) OnPropertyChanged("OlympiadSportsman");
               BAwardOlympiads.Clear();
               bSeason = value;
               OnPropertyChanged("OlympiadSports");
            }
        }

    }
}
