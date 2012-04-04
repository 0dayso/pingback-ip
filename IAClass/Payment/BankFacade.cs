using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using IAClass.Entity;

namespace IAClass.Payment
{
    public class BankFacade : Unity
    {
        IBank instance;

        public void Transfer(PaymentEntity entity)
        {
            try
            {
                //注入实例
                instance = container.Resolve<IBank>(entity.IOC_Class_Alias);
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }

            instance.Transfer(entity);
        }
    }
}
