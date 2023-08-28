using data_access.Entities;
using data_access.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Models
{
    internal class OlympiadDBModel : INotifyPropertyChanged,IDisposable
    {
        private IRepository<Sportsman>? sportsmans;
        private IRepository<Sport>? sports;
        private bool disposedValue;

        public IEnumerable<Sportsman> Sportsmans { get; set; }
        public IEnumerable<Sport> Sports { get; set; }
        public OlympiadDBModel() 
        {
            sportsmans = new OlympiadRepository<Sportsman>();
            sports = new OlympiadRepository<Sport>();
            Sportsmans = sportsmans.Get(includeProperties:"Country,Sport,Genre",orderBy: x=>x.OrderBy(c=>c.SportId));
            Sports = sports.Get(includeProperties: "Season");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sportsmans?.Dispose();
                    sports?.Dispose();
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
