using System;
using System.Linq;
using System.Web.Security;

using ITechArt.Blog.Models;


namespace ITechArt.Blog.Providers
{
    public class BlogRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[] { };
            using (BlogContext db = new BlogContext())
            {
                User user = db.User.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Role userRole = db.Role.Find(user.Role);
                    if (userRole != null)
                    {
                        role = new string[] { userRole.Name };
                    }
                }
            }
            return role;
        }
        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { Name = roleName };
            BlogContext db = new BlogContext();
            db.Role.Add(newRole);
            db.SaveChanges();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            using (BlogContext db = new BlogContext())
            {
                User user = db.User.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Role userRole = db.Role.Find(user.Role);
                    if (userRole != null && userRole.Name == roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}