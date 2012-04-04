using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using IAClass.Entity;

namespace IAClass.SMS
{
    public class SMSFacade : Unity
    {
        ISMS instance;

        public TraceEntity Send(SMSEntity entity)
        {
            TraceEntity result;

            try
            {
                //注入实例
                instance = container.Resolve<ISMS>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            result = instance.Send(entity);
            return result;
        }

        public int GetBalance(string IOC_Class_Alias)
        {
            try
            {
                //注入实例
                instance = container.Resolve<ISMS>(IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            int result = instance.GetBalance(IOC_Class_Alias);
            return result;
        }
    }
}
