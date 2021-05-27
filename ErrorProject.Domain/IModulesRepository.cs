using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface IModulesRepository
    {
        IQueryable<Modules> All { get; }

        void InsertOrUpdate(Modules modules);

        void Remove(Modules modules);

        void Save();
    }
}