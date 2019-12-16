using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingLogic;
namespace BugTrackingUI {
    class Program {
        static void Main(string[] args) {
            const string adminPw = "la qualunque basta";
            const string connectionString = @"Data Source=ARYA;Initial Catalog=EFLabo;Integrated Security=SSPI; MultipleActiveResultSets=True";

            BugTrackingFactory.InitializeBugTracking(connectionString,adminPw);
            Console.ReadLine();
        }
    }
}