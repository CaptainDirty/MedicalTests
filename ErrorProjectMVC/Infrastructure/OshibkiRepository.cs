using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class OshibkiRepository : IOshibkiRepository
    {
        AccountsDatabase _context;

        public OshibkiRepository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<Oshibki> IOshibkiRepository.All
        {
            get { return _context.Oshibki; }
        }

        void IOshibkiRepository.InsertOrUpdate(Oshibki oshibki)
        {
            if (oshibki.ID_Oshibki == default(int))
            {
                _context.Oshibki.Add(oshibki);
            }
            else
            {
                _context.Entry(oshibki).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IOshibkiRepository.Remove(Oshibki oshibki)
        {
            _context.Entry(oshibki).State = System.Data.Entity.EntityState.Deleted;
        }

        void IOshibkiRepository.Save()
        {
            _context.SaveChanges();
        }

    }
}