using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Common;

namespace YueDu.App_Code
{
    public class EnvSettings
    {
        public struct Novel
        {
            public static string Filter(int CPID, string prefix = "")
            {
                if (CPID > 0)
                {
                    return string.Format(" and {1}CPID != {0} ", CPID, prefix);
                }

                return "";
            }

            public static string Filter(Constants.Novel.ShowLocation showLocation, Constants.Novel.ShowSourceType showSourceType = Constants.Novel.ShowSourceType.wap, string prefix = "")
            {
                return string.Format(" and (({0}ShowSourceType & {1}) = {1} and ({0}ShowLocation & {2}) = {2}) ", prefix, (int)showSourceType, (int)showLocation);
            }
        }
    }
}