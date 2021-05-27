using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface ICategoriesRepository
    {
        IQueryable<Categories> All { get; }

        void InsertOrUpdate(Categories categories);

        void Remove(Categories categories);

        void Save();
    }
}