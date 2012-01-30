using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString.Old
{
    public class api
    {
        /*
        mystring.substring(content, 0, 2).ToLower() == "av" ||
                            mystring.substring(content, 0, 2).ToLower() == "fv" ||
                            mystring.substring(content, 0, 2).ToLower() == "sk" ||
                            mystring.substring(content, 0, 2).ToLower() == "fd" ||
                            mystring.substring(content, 0, 2).ToLower() == "ds"
         */
        /// <summary>
        /// �Ƿ�Ϊ����ָ��"av","fv","sk","fd","ds"
        /// </summary>
        public static bool old_0(string cmd)
        {
            string[] a = new string[] { "av","fv","sk","fd","ds"};
            for (int i = 0; i < a.Length; i++) 
                if (cmd.ToLower().StartsWith(a[i])) 
                    return true;
            return false;
        }
        /// <summary>
        /// �Ż�ָ����
        /// </summary>
        public static string[] EtermCommandGroupOptimize(string cmds)
        {
            EagleFileIO.LogWrite("ָ���Ż�ǰ��" + cmds);
            string[] a = cmds.ToLower().Split('~');
            List<string> ls = new List<string>();
            for (int i = 0; i < a.Length; i++)
            {
                try
                {
                    //case 1:ǰ��ָ����ͬ(xe5,xe5�Լ�pn,pn��)
                    //if (a[i] == a[i + 1]) continue;
                    //case 2:ǰ��ָ���av
                    if (a[i].StartsWith("av") && a[i + 1].StartsWith("av")) continue;
                    //case 3:ǰ��ָ���rtxxxxx
                    if (a[i].StartsWith("rt") && a[i].Length >= 7 && a[i + 1].StartsWith("rt") && a[i + 1].Length >= 7) continue;
                    #region//case 4:av,pn,pn,...pn,av//�Ѱ�����case 6��
                    /*if (a[i].StartsWith("av"))
                    {

                        for (int j = i + 1; j < a.Length; j++)
                        {
                            if (a[j].StartsWith("pn")) continue;
                            else if (a[j].StartsWith("av"))
                            {
                                i = j - 1;//��һ��ѭ����i����,��Ҫ��1���ص���㣬���Ӹ�av���¼��
                                break;
                            }
                            else
                            {
                                ls.Add(a[i]);//��pn����£������ָ�ѭ��������ص����ѭ��continue
                                break;
                            }
                        }
                        continue;
                    }*/
                    #endregion
                    #region//case 5:rtxxxxx,pn,pn,...pn,rtxxxxx
                    if (a[i].StartsWith("rt") && a[i].Length>=7)
                    {

                        for (int j = i + 1; j < a.Length; j++)
                        {
                            if (a[j].StartsWith("pn")) continue;
                            else if (a[i].StartsWith("rt") && a[i].Length >= 7)
                            {
                                i = j - 1;//��һ��ѭ����i����,��Ҫ��1
                                break;
                            }
                            else
                            {
                                ls.Add(a[i]);
                                break;
                            }
                        }
                        continue;
                    }
                    #endregion
                    //case 6:av,��sd,��ss,��s��ͷ...,av
                    if (a[i].StartsWith("av"))
                    {
                        bool b = false;
                        for (int j = i + 1; j < a.Length; j++)
                        {
                            if (a[j].StartsWith("av"))
                            {
                                int index;
                                for (index = i + 1; index < j; index++)
                                {
                                    if (a[index].StartsWith("s")) break;
                                }
                                if (index == j)
                                {
                                    b = true;
                                    i = j - 1;
                                }
                                break;
                            }
                        }
                        if (b) continue;
                    }
                    //////////////////////////
                    ls.Add(a[i]);
                }
                catch
                {
                    ls.Add(a[i]);
                }
            }
            OptimizeRule(ls);
            EagleFileIO.LogWrite("ָ���Ż���" + string.Join(",", ls.ToArray()));
            return ls.ToArray();
        }

        private static void OptimizeRule(List<string> ls)
        {
            //i~av h shawuh+~pn~ss:ca3143/y/09may/shawuh/ll1
            //rule 1: ��¼��һ��ss��λ��,ssǰ�������rt���򲻱䣬����ȥ��ssǰ�������ָ��
            int indexss = -1;
            int indexrt = -1;
            for (int i = 0; i < ls.Count; i++)
            {
                string t = ls[i].ToLower();
                if (t.Length >= 7 && t.StartsWith("rt")) indexrt = i;
                if (t.StartsWith("ss"))
                {
                    indexss = i;
                    break;
                }
            }
            if (indexrt == -1 && indexss>0)
            {
                ls.RemoveRange(1, indexss - 1);
            }
            //rule 2:��¼��һ��rt��λ�ã�ȥ��rtǰ����ָ��
            else if (indexrt > indexss)
            {
                ls.RemoveRange(1, indexrt - 1);
            }
        }
    }
}
