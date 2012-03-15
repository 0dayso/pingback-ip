using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using IAClass.Issuing;
using System.IO;
using System.Web;

namespace IAClass
{
    class Test
    {
        public static void TestIt(object t)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(TestIssuing));
            th.Start(t);
        }

        private static void TestIssuing(object r)
        {
            try
            {
                IssueEntity entity = (IssueEntity)r;

                entity.ProductId = 6;// int.Parse(request.InsuranceCode);
                entity.IOC_Class_Alias = "test";

                IAClass.Issuing.IssuingFacade facade = new Issuing.IssuingFacade();
                IssuingResultEntity result = facade.Issue(entity);
                //PurchaseResponseEntity result = WebServiceClass.Purchase(request);

                if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
                {
                    Common.LogIt(result.PolicyNo);
                }
                else
                {
                    Common.LogIt(result.Trace.ErrorMsg);
                }
            }
            catch (Exception e)
            {
                Common.LogIt("TEST:" + e.ToString());
            }
        }
    }
}
