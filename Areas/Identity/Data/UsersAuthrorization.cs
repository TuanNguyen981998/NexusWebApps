using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Areas.Identity.Data
{
    
    public class UsersAuthrorization
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = RolesAndOperationsConstants.CreateSong};
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = RolesAndOperationsConstants.ReadSong };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = RolesAndOperationsConstants.UpdateSong };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = RolesAndOperationsConstants.DeleteSong };
    }

    public class RolesAndOperationsConstants
    {
        public const string CreateSong = "Create";
        public const string ReadSong = "Read";
        public const string UpdateSong = "Update";
        public const string DeleteSong = "Delete";
        public const string AdministratorsRole = "Admin";
        public const string UsersRole = "User";
    }
}
