using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.DapperConnection
{
    public interface IDapperContext
    {
        IDbConnection GetDbConnection();
    }
}
