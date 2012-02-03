using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;

namespace IAClass.SMS
{
    public interface ISMS
    {
        TraceEntity Send(SMSEntity entity);

        int GetBalance(string IOC_TypeName);
    }
}
