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
        private bool disposedValue;

        private readonly IUnitOW unitOW;

        private bool idFilter(int id, int? selId)
        {
            if (selId == null || selId == -1) return true;
            else return id == selId;
        }

        private List<Country>? countries = null;

        private List<Sport>? sports = null;

        private List<Olympiad_>? olympiads;

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
       
        private void addSportsman(object o) => ModifySportsman(true);


        private void editSportsman(object o)
        {
            editSpotrsmanWindow = new() {DataContext = this };
            editSpotrsmanWindow.ShowDialog();
        }


        private void addOlympiad(object o) => ModifyOlimpiad(true);
        
        private void editOlympiad(object o)
        {

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
                    countries = new() { new() { Name = "All", Id = -1 } };
                    countries.AddRange(unitOW.Countries.Get().OrderBy(x => x.Name));
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
                    sports = new() { new() { Name = "All", Id = -1 } };
                    sports.AddRange(unitOW.Sports.Get(includeProperties: "Season").OrderBy(x => x.Name));
                }
                return sports;
            }
        }

        public IEnumerable<Olympiad_> ComboBoxOlympiad
        {
            get
            {
                if (olympiads == null)
                {
                    olympiads = new() { new() { Id = -1 } };
                    var temp = unitOW.Olympiads.Get(includeProperties: "City,Season").OrderBy(x => x.Year);
                    olympiads.AddRange(temp);
                    selectedOlympiadMT = olympiads[0];
                    selectedOlympiadM = olympiads[0];
                    selectedOlympiadCR = olympiads[0];
                    OnPropertyChanged("SelectedOlympiadMT");
                    OnPropertyChanged("SelectedOlympiadM");
                    OnPropertyChanged("SelectedOlympiadCR");
                }
                return olympiads;
            }
        }

        public IEnumerable<CountryResultInfo>? CountryResult => sports?.Where(x => x.Id != -1 )
                                                                      .Select(x => new CountryResultInfo()
                                                                      {
                                                                          Sport = x,
                                                                          Gold = x.Sportsmans.Where(q=>idFilter(q.CountryId,SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Gold" && idFilter(z.OlympiadId,SelectedOlympiadCR?.Id))).Count(),
                                                                          Silver = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Silver" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count(),
                                                                          Bronze = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Bronze" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count()
                                                                      });

        public IEnumerable<SpotrsmenInfo> Sportsmans => AllSportsmans.Where(x => idFilter(x.SportId ,SelectedSport?.Id) && x.AwardOlympiads.Any(y=>((y.Award != null) || !WithMedals) && idFilter(y.Sportsman.CountryId ,SelectedCountry?.Id) && idFilter(y.OlympiadId,SelectedOlympiadM?.Id)))
                                                                  .Select(x=> new SpotrsmenInfo()
                                                                  {
                                                                      Sportsman = x,
                                                                      Gold = x.AwardOlympiads.Where(y=>y.Award?.Name == "Gold" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                      Silver = x.AwardOlympiads.Where(y => y.Award?.Name == "Silver" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                      Bronze = x.AwardOlympiads.Where(y => y.Award?.Name == "Bronze" && idFilter(y.OlympiadId, SelectedOlympiadM?.Id)).Count(),
                                                                  }).OrderByDescending(x=>x.Gold);

        public IEnumerable<MedalTableInfo> MedalTable => SpAwOlympiads.Where(x=> idFilter(x.OlympiadId, SelectedOlympiadMT?.Id))
                                                                    .GroupBy(x=>x.Sportsman.Country)
                                                                    .Select(y => 
                                                                    new MedalTableInfo()
                                                                    {
                                                                        Country = y.Key,
                                                                        Gold =  y.Where(x => x.Award?.Name == "Gold").Count(),
                                                                        Silver = y.Where(x => x.Award?.Name == "Silver").Count(),
                                                                        Bronze = y.Where(x => x.Award?.Name == "Bronze").Count(),
                                                                    }).OrderByDescending(x=>x.Total);

        public OlympiadDBModel() 
        {
            unitOW = new UnitOfWork();
            cts = unitOW.Cities.Get().OrderBy(x => x.Name).ToList();
            unitOW.Olympiads.Get();
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
