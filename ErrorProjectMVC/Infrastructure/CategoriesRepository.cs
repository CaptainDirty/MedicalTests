using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class CategoriesRepository : ICategoriesRepository
    {
        AccountsDatabase _context;

        public CategoriesRepository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<Categories> ICategoriesRepository.All
        {
            get { return _context.Categories; }
        }

        void ICategoriesRepository.InsertOrUpdate(Categories categories)
        {
            if (categories.ID_Category == default(int))
            {
                _context.Categories.Add(categories);
            }
            else
            {
                _context.Entry(categories).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void ICategoriesRepository.Remove(Categories categories)
        {
            _context.Entry(categories).State = System.Data.Entity.EntityState.Deleted;
        }

        void ICategoriesRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}