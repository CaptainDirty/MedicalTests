using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface IStatusesRepository
    {
        IQueryable<Statuses> All { get; }

        void InsertOrUpdate(Statuses statuses);

        void Remove(Statuses statuses);

        void Save();
    }
}