using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Infrastructure
{
    public class DALContext : IDALContext
    {
        AccountsDatabase _database;
        IUserProfileRepository _users;
        ICategoriesRepository _categories;
        IModulesRepository _modules;
        IRazrabotchikiRepository _razrabotchiki;
        IStatusesRepository _statuses;
        IOshibkiRepository _oshibki;


        public DALContext()
        {
            _database = new AccountsDatabase();
        }

        public IUserProfileRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRespository(_database);
                }
                return _users;
            }
        }

        public ICategoriesRepository Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoriesRepository(_database);
                }
                return _categories;
            }
        }

        public IModulesRepository Modules
        {
            get
            {
                if (_modules == null)
                {
                    _modules = new ModulesRepository(_database);
                }
                return _modules;
            }
        }

        public IOshibkiRepository Oshibki
        {
            get
            {
                if (_oshibki == null)
                {
                    _oshibki = new OshibkiRepository(_database);
                }
                return _oshibki;
            }
        }

        public IRazrabotchikiRepository Razrabotchiki
        {
            get
            {
                if (_razrabotchiki == null)
                {
                    _razrabotchiki  = new RazrabotchikiRepository(_database);
                }
                return _razrabotchiki;
            }
        }
        public IStatusesRepository Statuses
        {
            get
            {
                if (_statuses == null)
                {
                    _statuses  = new StatusesRepository(_database);
                }
                return _statuses;
            }
        }

    }
}