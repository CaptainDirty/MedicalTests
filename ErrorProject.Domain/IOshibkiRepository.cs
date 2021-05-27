using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface IOshibkiRepository
    {
        IQueryable<Oshibki> All { get; }

        void InsertOrUpdate(Oshibki oshibki);

        void Remove(Oshibki oshibki);

        void Save();
    }
}