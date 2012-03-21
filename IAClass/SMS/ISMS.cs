using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;

namespace IAClass.SMS
{
    interface ISMS
    {
        TraceEntity Send(SMSEntity entity);

        int GetBalance(string IOC_Class_Alias);
    }
}
