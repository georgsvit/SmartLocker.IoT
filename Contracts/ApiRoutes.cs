using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "https://localhost:44305";

        public const string ViolationPost = Base + "/accounting/violation";

        public const string ReturnTool = Base + "/accounting/return";
        public const string TakeTool = Base + "/accounting/take";
        
        public const string GetLockersConfig = Base + "/locker/config";
    }
}
