using data_access.Data;
using data_access.Entities;
using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    public class UnitOfWork : IUnitOW
    {
        private OlympiadDBContext context = new ();
        private Repository<Sportsman>? sportsman;
        private Repository<Sport>? sports;
        private Repository<Award>? awards;
        private Repository<Olympiad_>? olympiad;
        private Repository<SportsmanAwardOlympiad>? sAOlympiad;
        private Repository<Country>? countries;
        private Repository<Gender>? genders;
        private bool disposed = false;

        public IRepository<Sportsman> Sportsmans => sportsman ??= new Repository<Sportsman>(context);
      
        public IRepository<Sport> Sports => sports ??= new Repository<Sport>(context);

        public IRepository<Award> Awards => awards ??= new Repository<Award>(context);

        public IRepository<SportsmanAwardOlympiad> SAOlympiad => sAOlympiad ??= new Repository<SportsmanAwardOlympiad>(context);

        public IRepository<Olympiad_> Olympiads => olympiad ??= new Repository<Olympiad_>(context);

        public IRepository<Country> Countries => countries ??= new Repository<Country>(context);

        public IRepository<Gender> Genders => genders ??= new Repository<Gender>(context);

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
