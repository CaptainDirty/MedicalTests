using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class StatusesRepository : IStatusesRepository
    {
        AccountsDatabase _context;

        public StatusesRepository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<Statuses> IStatusesRepository.All
        {
            get { return _context.Statuses; }
        }

        void IStatusesRepository.InsertOrUpdate(Statuses statuses)
        {
            if (statuses.ID_Status == default(int))
            {
                _context.Statuses.Add(statuses);
            }
            else
            {
                _context.Entry(statuses).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IStatusesRepository.Remove(Statuses statuses)
        {
            _context.Entry(statuses).State = System.Data.Entity.EntityState.Deleted;
        }

        void IStatusesRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}