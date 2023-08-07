using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Infrastructure
{
    public class SmsProvider : ISmsProvider
    {
        public void SendSms(string mobilePhone, string content)
        {
            /// Send sms
        }
    }
}
