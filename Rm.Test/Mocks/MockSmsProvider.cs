using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Test.Mocks
{
    internal class MockSmsProvider : ISmsProvider
    {
        public void SendSms(string mobilePhone, string content)
        {
            // sms mocks
        }
    }
}
