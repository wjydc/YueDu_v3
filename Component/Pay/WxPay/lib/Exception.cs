﻿using System;
using System.Collections.Generic;
using System.Web;

namespace Com.WxPayAPI
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}