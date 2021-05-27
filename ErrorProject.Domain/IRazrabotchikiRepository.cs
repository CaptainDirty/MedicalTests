using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface IRazrabotchikiRepository
    {
        IQueryable<Razrabotchiki> All { get; }

        void InsertOrUpdate(Razrabotchiki razrabotchiki);

        void Remove(Razrabotchiki razrabotchiki);

        void Save();
    }
}