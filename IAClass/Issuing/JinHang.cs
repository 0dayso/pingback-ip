using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;
using System.Net;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Web;

namespace JinHang
{
    class Issuing : IAClass.Issuing.IIssuing
    {
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            IssuingResultEntity result = new IssuingResultEntity();

            XmlDocument policy = XMLString.Issuing.Clone() as XmlDocument;
            //基本信息
            policy.SelectSingleNode("message/batchNo").InnerText = entity.CaseId;//最长8位数字（千万级）
            policy.SelectSingleNode("message/transDate").InnerText = DateTime.Now.ToString("yyyyMMdd");
            policy.SelectSingleNode("message/transTime").InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");

            //姓名处理
            string fn = entity.Name.Substring(0, 1);
            string ln = entity.Name.Replace(fn, string.Empty);
            if (entity.Name.Length > 2)
            {
                if (IsLongFN(entity.Name))
                {
                    fn = fn.Substring(0, 2);
                    ln = entity.Name.Replace(fn, string.Empty);
                }
            }
            //保单信息
            XmlNode para = policy.SelectSingleNode("message/dataSet/record");
            XmlNode paraNew = para.Clone();
            paraNew.SelectSingleNode("effDate").InnerText = entity.EffectiveDate.ToString("yyyyMMdd");
            paraNew.SelectSingleNode("phFn").InnerText = fn;
            paraNew.SelectSingleNode("phLn").InnerText = ln;
            paraNew.SelectSingleNode("phGender").InnerText = entity.Gender == Gender.Female ? "F" : "M";
            paraNew.SelectSingleNode("phBirth").InnerText = entity.Birthday.ToString("yyyyMMdd");
            paraNew.SelectSingleNode("phIdtype").InnerText = GetIdType(entity.IDType);
            paraNew.SelectSingleNode("phId").InnerText = entity.ID;
            paraNew.SelectSingleNode("phMobile").InnerText = string.IsNullOrEmpty(entity.PhoneNumber) ? entity.CaseId : entity.PhoneNumber;
            //paraNew.SelectSingleNode("phAdd1").InnerText = null;
            //paraNew.SelectSingleNode("phAdd2").InnerText = null;
            //paraNew.SelectSingleNode("phEmail").InnerText = null;
            string sig = paraNew.SelectSingleNode("productId").InnerText
                + paraNew.SelectSingleNode("effDate").InnerText
                + paraNew.SelectSingleNode("phId").InnerText
                + paraNew.SelectSingleNode("phId").InnerText;
            paraNew.SelectSingleNode("Signature").InnerText = EncryptStringByAES(sig, "poolwin");

            para.ParentNode.AppendChild(paraNew);
            para.ParentNode.RemoveChild(para);

            string ret = "";
            //try
            {
                ret = GetResponse(policy.OuterXml);
                if (string.IsNullOrEmpty(ret))
                    throw new Exception("金航网投保接口返回为空！");
            }
            //catch (Exception e)
            //{
            //    Common.LogIt(e.ToString());
            //    result.Trace.Detail = "金航网投保接口访问失败！";
            //    return result;
            //}

            //try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(ret);
                string errorCode = xml.SelectSingleNode("message/dataSet/record/retCode").InnerText;
                if (errorCode != "000000")
                {
                    Common.LogIt("投保参数" + policy.OuterXml + System.Environment.NewLine + "金航网投保：" + ret);
                    string error = xml.SelectSingleNode("message/dataSet/record/errMsg").InnerText;

                    if (!string.IsNullOrEmpty(error))
                        result.Trace.ErrorMsg = error;
                    else
                        result.Trace.ErrorMsg = "错误码" + errorCode;
                    //throw new Exception(errorCode);
                }
                else
                {
                    result.PolicyNo = xml.SelectSingleNode("message/dataSet/record/policyRef").InnerText;
                }

                return result;
            }
            //catch(Exception e)
            //{
            //    Common.LogIt("投保参数：" + policy.OuterXml + System.Environment.NewLine
            //                    + "金航网返回：" + ret + System.Environment.NewLine
            //                    + e.ToString());
            //    result.Trace.Detail = "返回结果解析失败！";
            //    return result;
            //}            
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            TraceEntity result = new TraceEntity();
            //result.ErrorMsg = "该产品仅支持退单。";
            return result;
        }

        static string GetResponse(string postValue)
        {
            string result = "";
            postValue = "userName=renwox&method=0&xml=" + System.Web.HttpUtility.UrlEncode(postValue);//浏览器的话会自动对xml字符串进行UrlEncode,故要模拟
            byte[] data = Encoding.UTF8.GetBytes(postValue);

            HttpWebRequest hwrequest = (HttpWebRequest)System.Net.HttpWebRequest.Create("http://www.poolwin.com/AnLianWeb/test.do");
            hwrequest.KeepAlive = true;
            hwrequest.ContentType = "application/x-www-form-urlencoded";// "text/xml"; 注意类型,否则会失败
            hwrequest.Method = "POST";
            hwrequest.ContentLength = data.Length;
            hwrequest.AllowAutoRedirect = true;
            try
            {
                //获取用于请求的数据流
                using (Stream reqStream = hwrequest.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                //获取回应
                using (HttpWebResponse res = (HttpWebResponse)hwrequest.GetResponse())
                {
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    result = sr.ReadToEnd();
                    sr.Close();
                }

                return result;
            }
            catch
            {
                Common.LogIt(hwrequest.RequestUri);
                throw;
            }
        }

        static Boolean IsLongFN(string firstName)
        {
            List<string> longFN = new List<string>();
            longFN.Add("欧阳"); longFN.Add("太史"); longFN.Add("端木"); longFN.Add("上官"); longFN.Add("司马");
            longFN.Add("东方"); longFN.Add("独孤"); longFN.Add("南宫"); longFN.Add("万俟"); longFN.Add("闻人");
            longFN.Add("夏侯"); longFN.Add("诸葛"); longFN.Add("尉迟"); longFN.Add("公羊"); longFN.Add("赫连");
            longFN.Add("澹台"); longFN.Add("皇甫"); longFN.Add("宗政"); longFN.Add("濮阳"); longFN.Add("公冶");
            longFN.Add("太叔"); longFN.Add("申屠"); longFN.Add("公孙"); longFN.Add("慕容"); longFN.Add("仲孙");
            longFN.Add("钟离"); longFN.Add("长孙"); longFN.Add("宇文"); longFN.Add("司徒"); longFN.Add("鲜于");
            longFN.Add("司空"); longFN.Add("闾丘"); longFN.Add("子车"); longFN.Add("亓官"); longFN.Add("司寇");
            longFN.Add("巫马"); longFN.Add("公西"); longFN.Add("颛孙"); longFN.Add("壤驷"); longFN.Add("公良");
            longFN.Add("漆雕"); longFN.Add("乐正"); longFN.Add("宰父"); longFN.Add("谷梁"); longFN.Add("拓跋");
            longFN.Add("夹谷"); longFN.Add("轩辕"); longFN.Add("令狐"); longFN.Add("段干"); longFN.Add("百里");
            longFN.Add("呼延"); longFN.Add("东郭"); longFN.Add("南门"); longFN.Add("羊舌"); longFN.Add("微生");
            longFN.Add("公户"); longFN.Add("公玉"); longFN.Add("公仪"); longFN.Add("梁丘"); longFN.Add("公仲");
            longFN.Add("公上"); longFN.Add("公门"); longFN.Add("公山"); longFN.Add("公坚"); longFN.Add("左丘");
            longFN.Add("公伯"); longFN.Add("西门"); longFN.Add("公祖"); longFN.Add("第五"); longFN.Add("公乘");
            longFN.Add("贯丘"); longFN.Add("公皙"); longFN.Add("南荣"); longFN.Add("东里"); longFN.Add("东宫");
            longFN.Add("仲长"); longFN.Add("子书"); longFN.Add("子桑"); longFN.Add("即墨"); longFN.Add("达奚");
            longFN.Add("褚师"); longFN.Add("吴铭");

            return longFN.Contains(firstName);
        }

        static string GetIdType(IdentityType type)
        {
            switch (type)
            {
                case IdentityType.身份证:
                    return "9";
                case IdentityType.护照:
                    return "6";
                case IdentityType.军官证:
                    return "3";
                case IdentityType.港澳通行证:
                    return "5";
                default:
                    return "4";//其他
            }
        }

        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        //AES加密
        //sAdesKey即为商户密码
        public string EncryptStringByAES(string toEntryptString, string sAdesKey)
        {
            //分组加密算法  
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEntryptString);//得到需要加密的字节数组      
            //设置密钥及密钥向量
            byte[] pwdBytes = Encoding.UTF8.GetBytes(sAdesKey);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            des.Key = keyBytes;
            des.IV = _key1;


            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组  
            cs.Close();
            ms.Close();

            return Convert.ToBase64String(cipherBytes);
        }

    }

    class XMLString
    {
        static XmlDocument issuing;

        public static XmlDocument Issuing
        {
            get
            {
                if (issuing == null)
                {
                    issuing = new XmlDocument();
                    issuing.Load(System.IO.Path.Combine(Common.BaseDirectory, "App_Data/JinHang/Issuing.xml"));
                }

                return issuing;
            }
        }
    }
}
