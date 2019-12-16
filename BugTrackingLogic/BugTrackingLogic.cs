using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingData;

namespace BugTrackingLogic {
    public class Session {

    }
    public class BugTracking {
    }

    public static class BugTrackingFactory {
        public static void InitializeBugTracking(string connectionString, string adminPassword) {
            if (null==connectionString)
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be empty");
            if(""==connectionString)
                throw new ArgumentException("Connection string cannot be empty",nameof(connectionString));
            if (null==adminPassword)
                throw new ArgumentNullException(nameof(adminPassword),"Admin password cannot be null");
            if (adminPassword.Length < Auxiliary.PasswordLength)
                throw new ArgumentOutOfRangeException(nameof(adminPassword),
                    $"Passwords must have at least {Auxiliary.PasswordLength} characters");
            using (var c=new BTContext(connectionString)){
                c.Database.Delete();
                c.Database.Create();
                var admin =c.Admins.Create();
                admin.Address=new Address();
                admin.Address.Country = "Nowhere";
                admin.Address.Street = "Boh";
                admin.BirthDate=new DateTime(1900,1,1);
                admin.FiscalCode = "ABCDEF12G34H567I";
                admin.Login = "administrator";
                admin.Password = PwManagement.PwManagement.HashPassword(adminPassword, Auxiliary.HashedPwSize,
                    Auxiliary.SaltSize, Auxiliary.IterationNumber);
                c.Admins.Add(admin);
                c.SaveChanges();
            }
        }

        public static BugTracking LoadBugTracking(string connectionString, string adminPassword){
            throw new NotImplementedException();
        }
    }


}
