
using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace MedicalTestsMVC.Infrastructure
{
    public class UserRespository : IUserProfileRepository
    {
        AccountsDatabase _context;

        public UserRespository(AccountsDatabase context)
        {
            _context = context;
        }

        IQueryable<UserProfile> IUserProfileRepository.All
        {
            get
            {
                return _context.UserProfiles;
            }
        }

        UserProfile IUserProfileRepository.CurrentUser
        {
            get
            {
                return _context
                    .UserProfiles
                    .Include("Categories")
                    .Include("Modules")
                    .Include("Oshibki")
                    .Include("Razrabotchiki")
                    .Include("Statuses")
                    .Where(u => u.ID_User == WebSecurity.CurrentUserId)
                    .FirstOrDefault();
            }
        }

        void IUserProfileRepository.InsertOrUpdate(UserProfile user)
        {
            if (user.ID_User == default(int))
            {
                _context.UserProfiles.Add(user);
            }
            else
            {
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IUserProfileRepository.Remove(UserProfile user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
        }

        void IUserProfileRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}