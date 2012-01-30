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
        /// 是否为以下指令"av","fv","sk","fd","ds"
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
        /// 优化指令组
        /// </summary>
        public static string[] EtermCommandGroupOptimize(string cmds)
        {
            EagleFileIO.LogWrite("指令优化前：" + cmds);
            string[] a = cmds.ToLower().Split('~');
            List<string> ls = new List<string>();
            for (int i = 0; i < a.Length; i++)
            {
                try
                {
                    //case 1:前后指令相同(xe5,xe5以及pn,pn等)
                    //if (a[i] == a[i + 1]) continue;
                    //case 2:前后指令都是av
                    if (a[i].StartsWith("av") && a[i + 1].StartsWith("av")) continue;
                    //case 3:前后指令都是rtxxxxx
                    if (a[i].StartsWith("rt") && a[i].Length >= 7 && a[i + 1].StartsWith("rt") && a[i + 1].Length >= 7) continue;
                    #region//case 4:av,pn,pn,...pn,av//已包含在case 6中
                    /*if (a[i].StartsWith("av"))
                    {

                        for (int j = i + 1; j < a.Length; j++)
                        {
                            if (a[j].StartsWith("pn")) continue;
                            else if (a[j].StartsWith("av"))
                            {
                                i = j - 1;//下一次循环，i自增,故要减1，回到外层，并从该av重新检测
                                break;
                            }
                            else
                            {
                                ls.Add(a[i]);//非pn情况下，加入该指令，循环结束后回到外层循环continue
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
                                i = j - 1;//下一次循环，i自增,故要减1
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
                    //case 6:av,非sd,非ss,非s开头...,av
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
            EagleFileIO.LogWrite("指令优化后：" + string.Join(",", ls.ToArray()));
            return ls.ToArray();
        }

        private static void OptimizeRule(List<string> ls)
        {
            //i~av h shawuh+~pn~ss:ca3143/y/09may/shawuh/ll1
            //rule 1: 记录第一个ss的位置,ss前面如果有rt，则不变，否则去掉ss前面的所有指令
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
            //rule 2:记录第一个rt的位置，去掉rt前所有指令
            else if (indexrt > indexss)
            {
                ls.RemoveRange(1, indexrt - 1);
            }
        }
    }
}
