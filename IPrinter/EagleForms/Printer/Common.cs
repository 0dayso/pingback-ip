﻿using System;
using System.Collections.Generic;
using System.Text;
using EagleWebService;
using System.Net;
using System.IO;

namespace EagleForms.Printer
{
    public struct BirthAndGender
    {
        public DateTime Birth;
        public Gender Gender;
    }

    class Common
    {
        public static BirthAndGender GetBirthAndSex(string identityCard)
        {
            string birthday = "";
            string genderStr = "";
            Gender gender;

            if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                genderStr = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                genderStr = identityCard.Substring(12, 3);
            }

            if (int.Parse(genderStr) % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                gender = Gender.Female;
            }
            else
            {
                gender = Gender.Male;
            }

            BirthAndGender birthAndGender = new BirthAndGender { Birth = DateTime.Parse(birthday), Gender = gender };
            return birthAndGender;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string GetResponse(string requestURL)
        {
            HttpWebRequest hwrequest = (HttpWebRequest)System.Net.HttpWebRequest.Create(requestURL);
            hwrequest.KeepAlive = true;
            hwrequest.ContentType = "text/xml";
            hwrequest.Method = "Get";
            hwrequest.AllowAutoRedirect = true;

            try
            {
                HttpWebResponse res = (HttpWebResponse)hwrequest.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);
                string result = sr.ReadToEnd();
                res.Close();
                sr.Close();
                return result;
            }
            catch
            {
                EagleString.EagleFileIO.LogWrite(requestURL);
                throw;
            }
        }
    }
}
