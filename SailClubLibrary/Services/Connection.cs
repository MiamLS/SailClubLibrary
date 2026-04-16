using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public abstract class Connection
    {
        protected String connectionString = Secret.ConnectionString;
    }
}
