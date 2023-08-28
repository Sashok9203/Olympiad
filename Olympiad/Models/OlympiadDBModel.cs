using data_access.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Olympiad.Models
{
    internal  class MedalTableInfo
    {
        public string Country { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int Total  => Gold + Silver + Bronze;
    }

    internal class ComboBoxOlympiadInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    internal partial class OlympiadDBModel : INotifyPropertyChanged, IDisposable
    {
        private bool disposedValue;

        private readonly IUnitOW unitOW = new UnitOfWork();

        private bool olympiadFilter(int index)
        {
            if (SelectedOlympiad.Id == -1) return true;
            else return index == SelectedOlympiad.Id;
        }

        private List<ComboBoxOlympiadInfo> oStr;
        
        private List<ComboBoxOlympiadInfo> olympStr
        {
            get 
            {
                oStr = new() { new() { Name = "All" ,Id = -1} };
                var temp = unitOW.Olympiads.Get(includeProperties: "City,Season")
                                                                      .OrderBy(x => x.Year)
                                                                      .Select(x => new ComboBoxOlympiadInfo() 
                                                                      {
                                                                          Name =$"{x.City.Country.Name} - \"{x.City.Name} {x.Year} {x.Season.Name}\"",
                                                                          Id = x.Id 
                                                                      });
                oStr.AddRange(temp);
                return oStr;
            }
        }

        public ComboBoxOlympiadInfo selectedOlympiad;
        public ComboBoxOlympiadInfo SelectedOlympiad 
        {
            get=> selectedOlympiad;
            set
            {
                selectedOlympiad = value;
                OnPropertyChanged("MedalTable");
            }
        }  

        public IEnumerable<ComboBoxOlympiadInfo> ComboBoxOlympiad => olympStr;

        public IEnumerable<MedalTableInfo> MedalTable => unitOW.SAOlympiad.Get(includeProperties: "Sportsman,Award,Olympiad")
                                                                          .Where(x=> olympiadFilter(x.OlympiadId))
                                                                          .GroupBy(x=>x.Sportsman.Country)
                                                                          .Select(y => 
                                                                          new MedalTableInfo()
                                                                            {
                                                                                Country = y.Key.Name,
                                                                                Gold =  y.Where(x => x.Award?.Name == "Gold").Count(),
                                                                                Silver = y.Where(x => x.Award?.Name == "Silver").Count(),
                                                                                Bronze = y.Where(x => x.Award?.Name == "Bronze").Count()
                                                                            }).OrderByDescending(x=>x.Total);

        public OlympiadDBModel() 
        {
            unitOW.Awards.Get();
            unitOW.Sportsmans.Get(includeProperties: "Country,Sport,");
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
