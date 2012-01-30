using System;
using System.Collections.Generic;
using System.Text;

namespace EagleForms.Printer
{
    public struct FeedbackEntity
    {
        public long elapsedMilliseconds;
        /// <summary>
        /// PNR、票号、大编码
        /// </summary>
        public string etermString;
    }

    public class Feedback
    {
        /// <summary>
        /// 提取 PNR 的速度的反馈信息
        /// </summary>
        /// <param name="stopwatch"></param>
        public static void PnrTimer(object entity)
        {
            try
            {
                FeedbackEntity feedbackEntity = (FeedbackEntity)entity;
                EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
                EagleWebService.t_Feedback req = new EagleWebService.t_Feedback();
                req.fromWho = Options.GlobalVar.IAUsername;
                req.subject = "PNR导入计时";
                req.timer = Convert.ToInt32(feedbackEntity.elapsedMilliseconds);
                req.remark = feedbackEntity.etermString;
                ws.Feedback(req);
            }
            catch
            {
            }
        }
    }
}
