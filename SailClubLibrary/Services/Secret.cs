using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public static class Secret
    {
        //Der kunne være password gemt i connectionstring
        private static string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SailClubDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //Dette er en test af secret...
        public static string ConnectionString
        {
            get { return _connectionString; }

        }
    }
}
