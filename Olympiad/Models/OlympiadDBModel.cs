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
        private readonly IUnitOW unitOW = new UnitOfWork(); 
       
        private bool disposedValue;

        public IEnumerable<Sportsman> Sportsmans { get; set; }
        public IEnumerable<Sport> Sports { get; set; }
        public OlympiadDBModel() 
        {
            Sportsmans = unitOW.Sportsmans.Get(includeProperties:"Country,Sport,Genre",orderBy: x=>x.OrderBy(c=>c.SportId));
            Sports = unitOW.Sports.Get(includeProperties: "Season");
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
