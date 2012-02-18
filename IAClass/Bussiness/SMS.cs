using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using IAClass.SMS;

//namespace IAClass.Bussiness
//{
    public class SMS
    {
        public static TraceEntity Send(SMSEntity entity)
        {
            TraceEntity result = new TraceEntity();
            try
            {
                result = new SMSFacade().Send(entity);
                if (string.IsNullOrEmpty(result.ErrorMsg))
                {
                    Case.SetSMS(entity.CaseNo);
                }
            }
            catch (Exception ee)
            {
                Common.LogIt(ee.ToString());
                result.ErrorMsg = ee.Message;
            }

            return result;
        }
    }
//}
