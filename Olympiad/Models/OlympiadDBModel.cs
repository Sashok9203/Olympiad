using data_access.Entities;
using data_access.Entityes;
using data_access.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Olympiad.Models
{
    internal class MTable
    {
        public string country { get; set; }
        public int gold { get; set; }
        public int silver { get; set; }
        public int bronze { get; set; }
        public int total  => gold + silver + bronze;
    }
    internal class OlympiadDBModel : INotifyPropertyChanged, IDisposable
    {
        private readonly IUnitOW unitOW = new UnitOfWork();

        private bool disposedValue;

        public IEnumerable<Sportsman> Sportsmans => unitOW.Sportsmans.Get(includeProperties: "Sport,Genre", orderBy: x => x.OrderBy(c => c.SportId));
        public IEnumerable<Sport> Sports => unitOW.Sports.Get(includeProperties: "Season");
        public IEnumerable<MTable> MedalTable => unitOW.SAOlympiad.Get(includeProperties: "Sportsman,Award").GroupBy(x=>x.Sportsman.Country).Select(y => new MTable
                                                                                                                     {
                                                                                                                         country = y.Key.Name,
                                                                                                                         gold =  y.Where( x =>  x.Award?.Name == "Gold" ).Count(),
                                                                                                                         silver = y.Where(x =>  x.Award?.Name == "Silver").Count(),
                                                                                                                         bronze = y.Where(x =>  x.Award?.Name == "Bronze").Count(),
                                                                                                                     });

        public OlympiadDBModel() 
        {
            unitOW.Awards.Get();
            unitOW.Sportsmans.Get(includeProperties: "Country");
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
