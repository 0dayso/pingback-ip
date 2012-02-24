using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;

namespace ChinaLife_BJ
{
    class Issuing : IAClass.Issuing.IIssuing
    {
        static TransportAccidentForSaveServer wsSave = new TransportAccidentForSaveServer();
        static TransportAccidentForEndorseServer wsCancel = new TransportAccidentForEndorseServer();

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "1";
                case IdentityType.护照:
                    return "2";
                case IdentityType.军官证:
                    return "0";
                case IdentityType.港澳通行证:
                    return "3";
                default:
                    return "6";//其他
            }
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }

        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            //提交订单
            TransportAccidentForSaveRequsetDto dto = new TransportAccidentForSaveRequsetDto();
            dto.birthDate = entity.Birthday.ToShortDateString();
            dto.effDate = entity.EffectiveDate.ToString("yyyy-MM-dd HH:mm:ss");
            dto.functionId = "1";
            dto.gender = entity.Gender == Gender.Female ? "女" : "男";
            dto.idNo = entity.ID;
            dto.idType = entity.IDType.ToString();
            //解决该接口不接受起保日期和终止日期是同一天的投保问题
            dto.matuDate = entity.EffectiveDate.Date == entity.ExpiryDate.Date ? entity.ExpiryDate.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") : entity.ExpiryDate.ToString("yyyy-MM-dd HH:mm:ss");
            dto.name = entity.Name;
            dto.units = "1";
            dto.customerPhone = "";// entity.PhoneNumber;
            dto.customerFlightNo = entity.FlightNo;

            string number = entity.CaseId;
            number = number.Length > 6 ? number.Substring(number.Length - 6) : number.PadLeft(6, '0');
            dto.orderCode = "TB2727" + DateTime.Today.ToString("yyyyMMdd") + number;

            //try
            //{
            //    Common.LogIt(Common.Serialize<TransportAccidentForSaveRequsetDto>(dto));
            //}
            //catch (Exception ee) { Common.LogIt(ee.ToString()); }

            TransportAccidentResponseDto ret = null;

            try
            {
                ret = wsSave.savePolicy("zgrs", "zgrs", dto);

                if (ret == null)
                    throw new Exception("北京国寿WebService返回为空！");
            }
            catch
            {
                Common.LogIt(wsSave.Url);
                throw;
            }

            if (string.IsNullOrEmpty(ret.error))
                if(string.IsNullOrEmpty(ret.policyNo))
                {
                    throw new Exception("北京国寿保单号为空！？");
                }
                else
                    result.PolicyNo = ret.policyNo;
            else
            {
                string request = Common.XmlSerialize<TransportAccidentForSaveRequsetDto>(dto);
                Common.LogIt("投保参数" + request + System.Environment.NewLine + "北京国寿投保：" + ret.error);
                result.Trace.ErrorMsg = ret.error;
            }

            return result;
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity trace = new TraceEntity();

            if (string.IsNullOrEmpty(entity.PolicyNo))
                return trace;//兼容之前未对接的已出单证（没有正式保单号）

            TransportAccidentForEndorseRequsetDto dto = new TransportAccidentForEndorseRequsetDto();
            dto.policyNo = entity.PolicyNo;
            dto.functionId = "2";
            TransportAccidentResponseDto ret = null;

            try
            {
                ret = wsCancel.endorPolicy("zgrs", "zgrs", dto);

                if (ret == null)
                    throw new Exception("北京国寿WebService返回为空！");
            }
            catch
            {
                Common.LogIt(wsCancel.Url);
                throw;
            }

            if (!string.IsNullOrEmpty(ret.error))
            {
                Common.LogIt("北京国寿 退保：" + ret.error);
                trace.ErrorMsg = ret.error;
            }

            return trace;
        }
    }
}
