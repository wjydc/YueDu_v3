using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using Utility;

namespace Com.WxPayAPI
{
    /// <summary>
    /// 回调处理基类
    /// 主要负责接收微信支付后台发送过来的数据，对数据进行签名验证
    /// 子类在此类基础上进行派生并重写自己的回调处理过程
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// 接收从微信后台POST过来的数据
        /// </summary>
        public Stream stream { get; set; }

        public Notify(Stream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// 接收从微信支付后台发送过来的数据并验证签名
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data">微信支付后台返回的数据</param>
        /// <returns></returns>
        public bool GetNotifyData(out WxPayData data)
        {
            StringBuilder builder = new StringBuilder();

            if (stream != null)
            {
                int count = 0;
                byte[] buffer = new byte[1024];
                while ((count = stream.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                stream.Flush();
                stream.Close();
                stream.Dispose();

                Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());
            }

            bool flag = false;

            //转换数据格式并验证签名
            data = new WxPayData();

            try
            {
                flag = !data.FromXml(builder.ToString()).IsNullOrEmpty<string, object>();
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                data = new WxPayData();
                data.SetValue("return_code", "FAIL");
                data.SetValue("return_msg", ex.Message);

                Log.Error(this.GetType().ToString(), "Sign check error : " + data.ToXml());
            }

            Log.Info(this.GetType().ToString(), "Check sign success");

            return flag;
        }

        //派生类需要重写这个方法，进行不同的回调处理
        public virtual void ProcessNotify(out WxPayData data)
        {
            data = null;
        }
    }
}