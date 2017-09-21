using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Com.WxPayAPI
{
    public class QrCodeMaker
    {
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="data">字符串（生成二维码图片）</param>
        /// <param name="version">0</param>
        /// <param name="scale">4</param>
        /// <returns></returns>
        public static byte[] PNG(string data, int version, int scale)
        {
            byte[] buffer = null;

            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    //初始化二维码生成工具
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    qrCodeEncoder.QRCodeVersion = version;
                    qrCodeEncoder.QRCodeScale = scale;

                    //将字符串生成二维码图片
                    Bitmap image = qrCodeEncoder.Encode(data, Encoding.Default);

                    //保存为PNG到内存流  
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        buffer = ms.GetBuffer();
                    }
                }
                catch { }
            }

            return buffer;
        }
    }
}
