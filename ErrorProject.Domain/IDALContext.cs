using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public interface IDALContext
    {
        IUserProfileRepository Users { get; }
        
        ICategoriesRepository Categories { get; }

        IModulesRepository Modules { get; }

        IRazrabotchikiRepository Razrabotchiki { get; }

        IStatusesRepository Statuses { get; }

        IOshibkiRepository Oshibki { get; }
    }
}