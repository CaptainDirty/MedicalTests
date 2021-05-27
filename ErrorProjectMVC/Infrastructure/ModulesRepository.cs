using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class ModulesRepository : IModulesRepository
    {
        AccountsDatabase _context;

        public ModulesRepository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<Modules> IModulesRepository.All
        {
            get { return _context.Modules; }
        }

        void IModulesRepository.InsertOrUpdate(Modules modules)
        {
            if (modules.ID_Module == default(int))
            {
                _context.Modules.Add(modules);
            }
            else
            {
                _context.Entry(modules).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IModulesRepository.Remove(Modules modules)
        {
            _context.Entry(modules).State = System.Data.Entity.EntityState.Deleted;
        }

        void IModulesRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}