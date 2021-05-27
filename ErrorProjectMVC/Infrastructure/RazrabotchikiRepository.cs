using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class RazrabotchikiRepository : IRazrabotchikiRepository
    {
        AccountsDatabase _context;

        public RazrabotchikiRepository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<Razrabotchiki> IRazrabotchikiRepository.All
        {
            get { return _context.Razrabotchiki; }
        }

        void IRazrabotchikiRepository.InsertOrUpdate(Razrabotchiki razrabotchiki)
        {
            if (razrabotchiki.ID_Razrabotchik == default(int))
            {
                _context.Razrabotchiki.Add(razrabotchiki);
            }
            else
            {
                _context.Entry(razrabotchiki).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IRazrabotchikiRepository.Remove(Razrabotchiki razrabotchiki)
        {
            _context.Entry(razrabotchiki).State = System.Data.Entity.EntityState.Deleted;
        }

        void IRazrabotchikiRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}