using Component.Base;
using Model.Common;
using Service;
using System;

namespace Component.Controllers.Log
{
    public class ErrorLogController : LogInfoController
    {
        protected void Log(Exception ex, string error)
        {
            try
            {
                string method = string.Empty;

                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();

                if (st.FrameCount > 1)
                {
                    method = st.GetFrame(1).GetMethod().Name;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    error += " ";
                }

                if (ex != null)
                {
                    error += string.Concat(ex.Message, " ", ex.StackTrace);
                }

                DataContext.ErrorLog(this.GetType().Name, method, "", error);
            }
            catch { }
        }

        protected void Log(string error)
        {
            try
            {
                string method = string.Empty;

                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();

                if (st.FrameCount > 1)
                {
                    method = st.GetFrame(1).GetMethod().Name;
                }

                DataContext.ErrorLog(this.GetType().Name, method, "", error);
            }
            catch { }
        }
    }
}
