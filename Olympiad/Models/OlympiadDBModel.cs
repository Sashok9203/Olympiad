using data_access.Entities;
using data_access.Repositories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OlympiadWPF.Models
{

    internal partial class OlympiadDBModel : INotifyPropertyChanged, IDisposable
    {
        private EditSpotrsmenWindow? editSpotrsmanWindow;

        private EditOlympiadWindow? editOlympiadWindow;

        private bool disposedValue;

        private readonly IUnitOW unitOW;

        private readonly Olympiad_ all_Olympiads = new() {  Id = -1  };

        private readonly Country all_Countries = new() { Name = "All", Id = -1 };

        private readonly Sport all_Sports = new() { Name = "All", Id = -1 };

        private bool idFilter(int id, int? selId)
        {
            if (selId == null || selId == -1) return true;
            else return id == selId;
        }

        private List<Country>? countries = null;

        private List<Sport>? sports = null;

        private List<Olympiad_>? olmp = null;

        private List<Sportsman>? sptms = null;

        private List<SportsmanAwardOlympiad>? spAwOl = null;

        private bool withMedals = true;

        private Sport? selectedSport;

        private Olympiad_? selectedOlympiadMT;

        private Olympiad_? selectedOlympiadM;

        private Olympiad_? selectedOlympiadCR;

        private Country? selectedCountry;

        private Country? selectedCountryCR;

        private List<SportsmanAwardOlympiad> SpAwOlympiads => spAwOl ??= unitOW.SAOlympiad.Get(includeProperties: "Sportsman,Award,Olympiad").ToList();

        private void addSportsman(object o)
        {
            isNew = true;
            ModifySportsman(isNew);
        }

        private void saveButton(object o)
        {
            if (MessageBox.Show("Are you sure?", "Save changes",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No,
                   MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                (o as Window).DialogResult = true;
        }

        private void editSportsman(object o)
        {
            isNew = false;
            editSpotrsmanWindow = new() {DataContext = this };
            editSpotrsmanWindow.ShowDialog();
        }

        private void addOlympiad(object o)
        {
            isNew = true;
            ModifyOlimpiad(isNew);
        }
        
        private void editOlympiad(object o)
        {
            isNew = false;
            editOlympiadWindow = new() { DataContext = this };
            editOlympiadWindow.ShowDialog();
        }


        public List<Sportsman> AllSportsmans => sptms ??= unitOW.Sportsmans.Get(includeProperties: "Country,Sport,Gender").ToList();

        public Country? TopCountry => Countries?.Where(x=>x.Id != -1 && x.Cities.Any(z=>z.Olympiads.Count>0)).OrderByDescending(y=>y.Cities.SelectMany(z=>z.Olympiads).Count()).FirstOrDefault();

        public bool WithMedals 
        {
            get => withMedals;
            set
            {
                withMedals = value;
                OnPropertyChanged("Sportsmans");
            }
        }
      
        public  Sport? SelectedSport
        {
            get => selectedSport;
            set
            {
                selectedSport = value;
                OnPropertyChanged("Sportsmans");
            }
        }
       
        public Olympiad_? SelectedOlympiadMT 
        {
            get=> selectedOlympiadMT;
            set
            {
                selectedOlympiadMT = value;
                OnPropertyChanged("MedalTable");
            }
        }
       
        public Olympiad_? SelectedOlympiadM
        {
            get => selectedOlympiadM;
            set
            {
                selectedOlympiadM = value;
                if (selectedOlympiadM?.SeasonId != SelectedSport?.SeasonId)
                {
                    SelectedSport = all_Sports;
                    OnPropertyChanged("SelectedSport");
                    OnPropertyChanged("ComboboxSports");
                }
                OnPropertyChanged("Sportsmans");
            }
        }
        
        public Olympiad_? SelectedOlympiadCR
        {
            get => selectedOlympiadCR;
            set
            {
                selectedOlympiadCR = value;
                OnPropertyChanged("CountryResult");
            }
        }

        public Country? SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged("Sportsmans");
            }
        }
        
        public Country? SelectedCountryCR
        {
            get => selectedCountryCR;
            set
            {
                selectedCountryCR = value;
                OnPropertyChanged("CountryResult");
            }
        }

        public IEnumerable<Country>? Countries
        {
            get
            {
                if (countries == null)
                {
                    countries = new() { all_Countries };
                    countries.AddRange(unitOW.Countries.Get(orderBy: (x) => x.OrderBy(z => z.Name)));
                }
                return countries;
            }
        }

        public IEnumerable<Sport>? Sports
        {
            get
            {
                if (sports == null)
                {
                    sports = new() { all_Sports };
                    sports.AddRange(unitOW.Sports.Get(includeProperties: "Season",orderBy:(x)=>x.OrderBy(z=>z.Name)));
                }
                return sports;
            }
        }

        public IEnumerable<Sport>? ComboboxSports => Sports?.Where( x=>x.Id == -1 || selectedOlympiadM?.SeasonId == 0 || (x.Id != -1 && x.SeasonId == selectedOlympiadM?.SeasonId));

        public IEnumerable<Olympiad_>? Olympiads => olmp ??= unitOW.Olympiads.Get(includeProperties: "City,Season", orderBy: (x) => x.OrderBy(z => z.Year)).ToList();

        public IEnumerable<Olympiad_> ComboBoxOlympiad
        {
            get
            {
                 List<Olympiad_> olymp = new() { all_Olympiads };
                 olymp.AddRange(Olympiads);
                 OnPropertyChanged("SelectedOlympiadMT");
                 OnPropertyChanged("SelectedOlympiadM");
                 OnPropertyChanged("SelectedOlympiadCR");
                 return olymp;
            }
               
            
        }

        public IEnumerable<CountryResultInfo>? CountryResult => sports?.Where(x => x.Id != -1 )
                                                                      .Select(x => new CountryResultInfo()
                                                                      {
                                                                          Sport = x,
                                                                          Gold = x.Sportsmans.Where(q=>idFilter(q.CountryId,SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Gold" && idFilter(z.OlympiadId,SelectedOlympiadCR?.Id))).Count(),
                                                                          Silver = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Silver" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count(),
                                                                          Bronze = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Bronze" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count()
                                                                      }).OrderByDescending(x => x.Gold).ThenByDescending(x => x.Silver).ThenByDescending(x => x.Bronze);

        public IEnumerable<SpotrsmenInfo> Sportsmans => AllSportsmans.Where(x => idFilter(x.SportId ,SelectedSport?.Id) && x.AwardOlympiads.Any(y=>((y.Award != null) || !WithMedals) && idFilter(y.Sportsman.CountryId ,SelectedCountry?.Id) && idFilter(y.OlympiadId,SelectedOlympiadM?.Id)))
                                                                  .Select(x=> new SpotrsmenInfo()
                                                                  {
                                                                      Sportsman = x,
                                                                      Gold = x.AwardOlympiads.Where(y=>y.Award?.Name == "Gold" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                      Silver = x.AwardOlympiads.Where(y => y.Award?.Name == "Silver" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                      Bronze = x.AwardOlympiads.Where(y => y.Award?.Name == "Bronze" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                  }).OrderByDescending(x=>x.Gold).ThenByDescending(x=>x.Silver).ThenByDescending(x=>x.Bronze);

        public IEnumerable<MedalTableInfo> MedalTable => SpAwOlympiads.Where(x=> idFilter(x.OlympiadId, SelectedOlympiadMT?.Id))
                                                                    .GroupBy(x=>x.Sportsman.Country)
                                                                    .Select(y => 
                                                                    new MedalTableInfo()
                                                                    {
                                                                        Country = y.Key,
                                                                        Gold =  y.Where(x => x.Award?.Name == "Gold").Count(),
                                                                        Silver = y.Where(x => x.Award?.Name == "Silver").Count(),
                                                                        Bronze = y.Where(x => x.Award?.Name == "Bronze").Count(),
                                                                    }).OrderByDescending(x=>x.Gold).ThenByDescending(x=>x.Silver).ThenByDescending(x=>x.Bronze);

        public OlympiadDBModel() 
        {
            unitOW = new UnitOfWork();
            cts = unitOW.Cities.Get(orderBy: (x) => x.OrderBy(z => z.Name)).ToList();
            olmp = unitOW.Olympiads.Get(includeProperties: "City,Season").ToList();
            selectedOlympiadMT = all_Olympiads;
            selectedOlympiadM = all_Olympiads;
            selectedOlympiadCR = all_Olympiads;
        }

        public RelayCommand AddSportsman => new((o) => addSportsman(o));
        public RelayCommand EditSportsman => new((o) => editSportsman(o));
        public RelayCommand AddOlympiad => new((o) => addOlympiad(o));
        public RelayCommand EditOlympiad => new((o) => editOlympiad(o));

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    unitOW.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
