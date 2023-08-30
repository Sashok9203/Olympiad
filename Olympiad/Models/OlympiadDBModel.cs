using data_access.Entities;
using data_access.Repositories;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OlympiadWPF.Models
{

    internal partial class OlympiadDBModel : INotifyPropertyChanged, IDisposable
    {
        private bool disposedValue;

        private readonly IUnitOW unitOW;

        private bool idFilter(int id, int? selId)
        {
            if (selId == null || selId == -1) return true;
            else return id == selId;
        }

        private List<Country>? countr = null;

        private List<Sport>? sprt = null;

        private List<Olympiad>? olmp;

        private List<Sportsman>? sptms = null;

        private List<SportsmanAwardOlympiad>? spAwOl = null;

        private bool withMedals = true;

        private Sport? selectedSport;

        private Olympiad? selectedOlympiadMT;

        private Olympiad? selectedOlympiadM;

        private Olympiad? selectedOlympiadCR;

        private Country? selectedCountry;

        private Country? selectedCountryCR;


        private List<Country>? countries
        {
            get
            {
                if (countr == null)
                {
                    countr = new() { new() { Name = "All", Id = -1 } };
                    countr.AddRange(unitOW.Countries.Get());
                }
                return countr;
            }
        }

        private List<Sport>? sports
        {
            get
            {
                if (sprt == null)
                {
                    sprt = new() { new() { Name = "All", Id = -1 } };
                    sprt.AddRange(unitOW.Sports.Get(includeProperties: "Season"));
                }
                return sprt;
            }
        }

        private List<Olympiad> olympiads
        {

            get
            {
                if (olmp == null)
                {
                    olmp = new() { new() { Id = -1 } };
                    var temp = unitOW.Olympiads.Get(includeProperties: "City,Season").OrderBy(x => x.Year);
                    olmp.AddRange(temp);
                }
                return olmp;
            }
        }

        private List<Sportsman> sportsmans => sptms ??= unitOW.Sportsmans.Get(includeProperties: "Country,Sport,Genre").ToList();

        private List<SportsmanAwardOlympiad> SpAwOlympiads
        {
            get
            {
                unitOW.Countries.Get();
                return  spAwOl ??= unitOW.SAOlympiad.Get(includeProperties: "Sportsman,Award,Olympiad").ToList();
            }
        }   

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
       
        public Olympiad? SelectedOlympiadMT 
        {
            get=> selectedOlympiadMT;
            set
            {
                selectedOlympiadMT = value;
                OnPropertyChanged("MedalTable");
            }
        }
       
        public Olympiad? SelectedOlympiadM
        {
            get => selectedOlympiadM;
            set
            {
                selectedOlympiadM = value;
                OnPropertyChanged("Sportsmans");
            }
        }
        
        public Olympiad? SelectedOlympiadCR
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

        public IEnumerable<Country>? Countries => countries;

        public IEnumerable<Sport>? Sports => sports;

        public IEnumerable<Olympiad> ComboBoxOlympiad => olympiads;

        public IEnumerable<CountryResultInfo>? CountryResult => sports?.Where(x => x.Id != -1 )
                                                                      .Select(x => new CountryResultInfo()
                                                                      {
                                                                          Sport = x,
                                                                          Gold = x.Sportsmans.Where(q=>idFilter(q.CountryId,SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Gold" && idFilter(z.OlympiadId,SelectedOlympiadCR?.Id))).Count(),
                                                                          Silver = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Silver" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count(),
                                                                          Bronze = x.Sportsmans.Where(q => idFilter(q.CountryId, SelectedCountryCR?.Id)).SelectMany(y => y.AwardOlympiads.Where(z => z.Award?.Name == "Bronze" && idFilter(z.OlympiadId, SelectedOlympiadCR?.Id))).Count()
                                                                      });

        public IEnumerable<SpotrsmenInfo> Sportsmans => sportsmans.Where(x => idFilter(x.SportId ,SelectedSport?.Id) && x.AwardOlympiads.Any(y=>((y.Award != null) || !WithMedals) && idFilter(y.Sportsman.CountryId ,SelectedCountry?.Id) && idFilter(y.OlympiadId,SelectedOlympiadM?.Id)))
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
                                                                        Country = y.Key.Name,
                                                                        Flag = y.Key.FlagPath,
                                                                        Gold =  y.Where(x => x.Award?.Name == "Gold").Count(),
                                                                        Silver = y.Where(x => x.Award?.Name == "Silver").Count(),
                                                                        Bronze = y.Where(x => x.Award?.Name == "Bronze").Count(),
                                                                    }).OrderByDescending(x=>x.Total);

        public OlympiadDBModel() 
        {
            unitOW = new UnitOfWork();
        }

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
