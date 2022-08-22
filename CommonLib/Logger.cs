using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Logger
    {
        public static readonly ILog nlog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
