using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ePlus
{
    class mystring
    {
        static public string right(string s, int i)
        {
            if (s.Length < i) return s;
            return s.Substring(s.Length - i, i);
        }
        static public string left(string s, int i)
        {
            if (i > s.Length) return s;
            return s.Substring(0, i);
        }
        static public string substring(string oldstring, int start, int length)
        {
            if (length < 0) return "";
            if (start < 0 || start > oldstring.Length) return "";
            if (start + length > oldstring.Length) return oldstring.Substring(start);
            return oldstring.Substring(start, length);
        }
        static public string trim(string s)
        {
            string t = s;
            while (t.Length > 0 && t[0] <= ' ') t = t.Substring(1);
            while (t.Length > 0 && right(t, 1)[0] <= ' ') t = left(t, t.Length - 1);
            return t;

        }
        static public string trim(string s, char c)
        {
            string t = s;
            while (t.Length > 0 && t[0] == c) t = t.Substring(1);
            while (t.Length > 0 && right(t, 1) == c.ToString()) t = left(t, t.Length - 1);
            return t;
        }
        static public string trim(string s, string c)
        {
            string t = s;
            while (t.Length > 0 && c.IndexOf(t[0]) > -1) t = t.Substring(1);
            while (t.Length > 0 && c.IndexOf(right(t, 1)[0]) > -1) t = left(t, t.Length - 1);
            return t;
        }
        static public string commandOptimizePN(string strSerial)
        {
            string ret = "";
            if (strSerial == "") return ret;
            List<string> ls = new List<string>();
            string[] arr = strSerial.Split('~');
            int pnCount = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                switch (arr[i].ToLower())
                {
                    case "pn":
                        pnCount++;
                        break;
                    default:
                        if (pnCount > 1) ls.Add("pg" + pnCount.ToString());
                        ls.Add(arr[i]);
                        pnCount = 1;
                        break;

                }
            }
            return "";
        }
        static public string serialFromArray(string[] arr)
        {
            if (arr.Length <= 0) return "";
            string ret = "";
            for (int i = 0; i < arr.Length; i++)
            {
                ret += arr[i] + "~";
            }
            ret = mystring.left(ret, ret.Length - 1);
            return ret;
        }
        static private string GetSpell(char source)
        {
            if (source <= 'z') return source.ToString();
            string s = source.ToString();
            byte[] b = System.Text.Encoding.Default.GetBytes(s);
            short uH = b[0];
            uH = (short)(uH & (short)0xff);
            short uL = b[1];
            uL = (short)(uL & 0xff);

            int iWord = uH * 0x100 + uL - 65536;
            string ret = "";
            #region if iword >
            if (iWord >= -20319) ret = "a";
            if (iWord >= -20317) ret = "ai";
            if (iWord >= -20304) ret = "an";
            if (iWord >= -20295) ret = "ang";
            if (iWord >= -20292) ret = "ao";
            if (iWord >= -20283) ret = "ba";
            if (iWord >= -20265) ret = "bai";
            if (iWord >= -20257) ret = "ban";
            if (iWord >= -20242) ret = "bang";
            if (iWord >= -20230) ret = "bao";
            if (iWord >= -20051) ret = "bei";
            if (iWord >= -20036) ret = "ben";
            if (iWord >= -20032) ret = "beng";
            if (iWord >= -20026) ret = "bi";
            if (iWord >= -20002) ret = "bian";
            if (iWord >= -19990) ret = "biao";
            if (iWord >= -19986) ret = "bie";
            if (iWord >= -19982) ret = "bin";
            if (iWord >= -19976) ret = "bing";
            if (iWord >= -19805) ret = "bo";
            if (iWord >= -19784) ret = "bu";
            if (iWord >= -19775) ret = "ca";
            if (iWord >= -19774) ret = "cai";
            if (iWord >= -19763) ret = "can";
            if (iWord >= -19756) ret = "cang";
            if (iWord >= -19751) ret = "cao";
            if (iWord >= -19746) ret = "ce";
            if (iWord >= -19741) ret = "ceng";
            if (iWord >= -19739) ret = "cha";
            if (iWord >= -19728) ret = "chai";
            if (iWord >= -19725) ret = "chan";
            if (iWord >= -19715) ret = "chang";
            if (iWord >= -19540) ret = "chao";
            if (iWord >= -19531) ret = "che";
            if (iWord >= -19525) ret = "chen";
            if (iWord >= -19515) ret = "cheng";
            if (iWord >= -19500) ret = "chi";
            if (iWord >= -19484) ret = "chong";
            if (iWord >= -19479) ret = "chou";
            if (iWord >= -19467) ret = "chu";
            if (iWord >= -19289) ret = "chuai";
            if (iWord >= -19288) ret = "chuan";
            if (iWord >= -19281) ret = "chuang";
            if (iWord >= -19275) ret = "chui";
            if (iWord >= -19270) ret = "chun";
            if (iWord >= -19263) ret = "chuo";
            if (iWord >= -19261) ret = "ci";
            if (iWord >= -19249) ret = "cong";
            if (iWord >= -19243) ret = "cou";
            if (iWord >= -19242) ret = "cu";
            if (iWord >= -19238) ret = "cuan";
            if (iWord >= -19235) ret = "cui";
            if (iWord >= -19227) ret = "cun";
            if (iWord >= -19224) ret = "cuo";
            if (iWord >= -19218) ret = "da";
            if (iWord >= -19212) ret = "dai";
            if (iWord >= -19038) ret = "dan";
            if (iWord >= -19023) ret = "dang";
            if (iWord >= -19018) ret = "dao";
            if (iWord >= -19006) ret = "de";
            if (iWord >= -19003) ret = "deng";
            if (iWord >= -18996) ret = "di";
            if (iWord >= -18977) ret = "dian";
            if (iWord >= -18961) ret = "diao";
            if (iWord >= -18952) ret = "die";
            if (iWord >= -18783) ret = "ding";
            if (iWord >= -18774) ret = "diu";
            if (iWord >= -18773) ret = "dong";
            if (iWord >= -18763) ret = "dou";
            if (iWord >= -18756) ret = "du";
            if (iWord >= -18741) ret = "duan";
            if (iWord >= -18735) ret = "dui";
            if (iWord >= -18731) ret = "dun";
            if (iWord >= -18722) ret = "duo";
            if (iWord >= -18710) ret = "e";
            if (iWord >= -18697) ret = "en";
            if (iWord >= -18696) ret = "er";
            if (iWord >= -18526) ret = "fa";
            if (iWord >= -18518) ret = "fan";
            if (iWord >= -18501) ret = "fang";
            if (iWord >= -18490) ret = "fei";
            if (iWord >= -18478) ret = "fen";
            if (iWord >= -18463) ret = "feng";
            if (iWord >= -18448) ret = "fo";
            if (iWord >= -18447) ret = "fou";
            if (iWord >= -18446) ret = "fu";
            if (iWord >= -18239) ret = "ga";
            if (iWord >= -18237) ret = "gai";
            if (iWord >= -18231) ret = "gan";
            if (iWord >= -18220) ret = "gang";
            if (iWord >= -18211) ret = "gao";
            if (iWord >= -18201) ret = "ge";
            if (iWord >= -18184) ret = "gei";
            if (iWord >= -18183) ret = "gen";
            if (iWord >= -18181) ret = "geng";
            if (iWord >= -18012) ret = "gong";
            if (iWord >= -17997) ret = "gou";
            if (iWord >= -17988) ret = "gu";
            if (iWord >= -17970) ret = "gua";
            if (iWord >= -17964) ret = "guai";
            if (iWord >= -17961) ret = "guan";
            if (iWord >= -17950) ret = "guang";
            if (iWord >= -17947) ret = "gui";
            if (iWord >= -17931) ret = "gun";
            if (iWord >= -17928) ret = "guo";
            if (iWord >= -17922) ret = "ha";
            if (iWord >= -17759) ret = "hai";
            if (iWord >= -17752) ret = "han";
            if (iWord >= -17733) ret = "hang";
            if (iWord >= -17730) ret = "hao";
            if (iWord >= -17721) ret = "he";
            if (iWord >= -17703) ret = "hei";
            if (iWord >= -17701) ret = "hen";
            if (iWord >= -17697) ret = "heng";
            if (iWord >= -17692) ret = "hong";
            if (iWord >= -17683) ret = "hou";
            if (iWord >= -17676) ret = "hu";
            if (iWord >= -17496) ret = "hua";
            if (iWord >= -17487) ret = "huai";
            if (iWord >= -17482) ret = "huan";
            if (iWord >= -17468) ret = "huang";
            if (iWord >= -17454) ret = "hui";
            if (iWord >= -17433) ret = "hun";
            if (iWord >= -17427) ret = "huo";
            if (iWord >= -17417) ret = "ji";
            if (iWord >= -17202) ret = "jia";
            if (iWord >= -17185) ret = "jian";
            if (iWord >= -16983) ret = "jiang";
            if (iWord >= -16970) ret = "jiao";
            if (iWord >= -16942) ret = "jie";
            if (iWord >= -16915) ret = "jin";
            if (iWord >= -16733) ret = "jing";
            if (iWord >= -16708) ret = "jiong";
            if (iWord >= -16706) ret = "jiu";
            if (iWord >= -16689) ret = "ju";
            if (iWord >= -16664) ret = "juan";
            if (iWord >= -16657) ret = "jue";
            if (iWord >= -16647) ret = "jun";
            if (iWord >= -16474) ret = "ka";
            if (iWord >= -16470) ret = "kai";
            if (iWord >= -16465) ret = "kan";
            if (iWord >= -16459) ret = "kang";
            if (iWord >= -16452) ret = "kao";
            if (iWord >= -16448) ret = "ke";
            if (iWord >= -16433) ret = "ken";
            if (iWord >= -16429) ret = "keng";
            if (iWord >= -16427) ret = "kong";
            if (iWord >= -16423) ret = "kou";
            if (iWord >= -16419) ret = "ku";
            if (iWord >= -16412) ret = "kua";
            if (iWord >= -16407) ret = "kuai";
            if (iWord >= -16403) ret = "kuan";
            if (iWord >= -16401) ret = "kuang";
            if (iWord >= -16393) ret = "kui";
            if (iWord >= -16220) ret = "kun";
            if (iWord >= -16216) ret = "kuo";
            if (iWord >= -16212) ret = "la";
            if (iWord >= -16205) ret = "lai";
            if (iWord >= -16202) ret = "lan";
            if (iWord >= -16187) ret = "lang";
            if (iWord >= -16180) ret = "lao";
            if (iWord >= -16171) ret = "le";
            if (iWord >= -16169) ret = "lei";
            if (iWord >= -16158) ret = "leng";
            if (iWord >= -16155) ret = "li";
            if (iWord >= -15959) ret = "lia";
            if (iWord >= -15958) ret = "lian";
            if (iWord >= -15944) ret = "liang";
            if (iWord >= -15933) ret = "liao";
            if (iWord >= -15920) ret = "lie";
            if (iWord >= -15915) ret = "lin";
            if (iWord >= -15903) ret = "ling";
            if (iWord >= -15889) ret = "liu";
            if (iWord >= -15878) ret = "long";
            if (iWord >= -15707) ret = "lou";
            if (iWord >= -15701) ret = "lu";
            if (iWord >= -15681) ret = "lv";
            if (iWord >= -15667) ret = "luan";
            if (iWord >= -15661) ret = "lue";
            if (iWord >= -15659) ret = "lun";
            if (iWord >= -15652) ret = "luo";
            if (iWord >= -15640) ret = "ma";
            if (iWord >= -15631) ret = "mai";
            if (iWord >= -15625) ret = "man";
            if (iWord >= -15454) ret = "mang";
            if (iWord >= -15448) ret = "mao";
            if (iWord >= -15436) ret = "me";
            if (iWord >= -15435) ret = "mei";
            if (iWord >= -15419) ret = "men";
            if (iWord >= -15416) ret = "meng";
            if (iWord >= -15408) ret = "mi";
            if (iWord >= -15394) ret = "mian";
            if (iWord >= -15385) ret = "miao";
            if (iWord >= -15377) ret = "mie";
            if (iWord >= -15375) ret = "min";
            if (iWord >= -15369) ret = "ming";
            if (iWord >= -15363) ret = "miu";
            if (iWord >= -15362) ret = "mo";
            if (iWord >= -15183) ret = "mou";
            if (iWord >= -15180) ret = "mu";
            if (iWord >= -15165) ret = "na";
            if (iWord >= -15158) ret = "nai";
            if (iWord >= -15153) ret = "nan";
            if (iWord >= -15150) ret = "nang";
            if (iWord >= -15149) ret = "nao";
            if (iWord >= -15144) ret = "ne";
            if (iWord >= -15143) ret = "nei";
            if (iWord >= -15141) ret = "nen";
            if (iWord >= -15140) ret = "neng";
            if (iWord >= -15139) ret = "ni";
            if (iWord >= -15128) ret = "nian";
            if (iWord >= -15121) ret = "niang";
            if (iWord >= -15119) ret = "niao";
            if (iWord >= -15117) ret = "nie";
            if (iWord >= -15110) ret = "nin";
            if (iWord >= -15109) ret = "ning";
            if (iWord >= -14941) ret = "niu";
            if (iWord >= -14937) ret = "nong";
            if (iWord >= -14933) ret = "nu";
            if (iWord >= -14930) ret = "nv";
            if (iWord >= -14929) ret = "nuan";//修正位置 与 nv
            if (iWord >= -14928) ret = "nue";
            if (iWord >= -14926) ret = "nuo";
            if (iWord >= -14930) ret = "nv";
            if (iWord >= -14922) ret = "o";
            if (iWord >= -14921) ret = "ou";
            if (iWord >= -14914) ret = "pa";
            if (iWord >= -14908) ret = "pai";
            if (iWord >= -14902) ret = "pan";
            if (iWord >= -14894) ret = "pang";
            if (iWord >= -14889) ret = "pao";
            if (iWord >= -14882) ret = "pei";
            if (iWord >= -14873) ret = "pen";
            if (iWord >= -14871) ret = "peng";
            if (iWord >= -14857) ret = "pi";
            if (iWord >= -14678) ret = "pian";
            if (iWord >= -14674) ret = "piao";
            if (iWord >= -14670) ret = "pie";
            if (iWord >= -14668) ret = "pin";
            if (iWord >= -14663) ret = "ping";
            if (iWord >= -14654) ret = "po";
            if (iWord >= -14645) ret = "pu";
            if (iWord >= -14630) ret = "qi";
            if (iWord >= -14594) ret = "qia";
            if (iWord >= -14429) ret = "qian";
            if (iWord >= -14407) ret = "qiang";
            if (iWord >= -14399) ret = "qiao";
            if (iWord >= -14384) ret = "qie";
            if (iWord >= -14379) ret = "qin";
            if (iWord >= -14368) ret = "qing";
            if (iWord >= -14355) ret = "qiong";
            if (iWord >= -14353) ret = "qiu";
            if (iWord >= -14345) ret = "qu";
            if (iWord >= -14170) ret = "quan";
            if (iWord >= -14159) ret = "que";
            if (iWord >= -14151) ret = "qun";
            if (iWord >= -14149) ret = "ran";
            if (iWord >= -14145) ret = "rang";
            if (iWord >= -14140) ret = "rao";
            if (iWord >= -14137) ret = "re";
            if (iWord >= -14135) ret = "ren";
            if (iWord >= -14125) ret = "reng";
            if (iWord >= -14123) ret = "ri";
            if (iWord >= -14122) ret = "rong";
            if (iWord >= -14112) ret = "rou";
            if (iWord >= -14109) ret = "ru";
            if (iWord >= -14099) ret = "ruan";
            if (iWord >= -14097) ret = "rui";
            if (iWord >= -14094) ret = "run";
            if (iWord >= -14092) ret = "ruo";
            if (iWord >= -14090) ret = "sa";
            if (iWord >= -14087) ret = "sai";
            if (iWord >= -14083) ret = "san";
            if (iWord >= -13917) ret = "sang";
            if (iWord >= -13914) ret = "sao";
            if (iWord >= -13910) ret = "se";
            if (iWord >= -13907) ret = "sen";
            if (iWord >= -13906) ret = "seng";
            if (iWord >= -13905) ret = "sha";
            if (iWord >= -13896) ret = "shai";
            if (iWord >= -13894) ret = "shan";
            if (iWord >= -13878) ret = "shang";
            if (iWord >= -13870) ret = "shao";
            if (iWord >= -13859) ret = "she";
            if (iWord >= -13847) ret = "shen";
            if (iWord >= -13831) ret = "sheng";
            if (iWord >= -13658) ret = "shi";
            if (iWord >= -13611) ret = "shou";
            if (iWord >= -13601) ret = "shu";
            if (iWord >= -13406) ret = "shua";
            if (iWord >= -13404) ret = "shuai";
            if (iWord >= -13400) ret = "shuan";
            if (iWord >= -13398) ret = "shuang";
            if (iWord >= -13395) ret = "shui";
            if (iWord >= -13391) ret = "shun";
            if (iWord >= -13387) ret = "shuo";
            if (iWord >= -13383) ret = "si";
            if (iWord >= -13367) ret = "song";
            if (iWord >= -13359) ret = "sou";
            if (iWord >= -13356) ret = "su";
            if (iWord >= -13343) ret = "suan";
            if (iWord >= -13340) ret = "sui";
            if (iWord >= -13329) ret = "sun";
            if (iWord >= -13326) ret = "suo";
            if (iWord >= -13318) ret = "ta";
            if (iWord >= -13147) ret = "tai";
            if (iWord >= -13138) ret = "tan";
            if (iWord >= -13120) ret = "tang";
            if (iWord >= -13107) ret = "tao";
            if (iWord >= -13096) ret = "te";
            if (iWord >= -13095) ret = "teng";
            if (iWord >= -13091) ret = "ti";
            if (iWord >= -13076) ret = "tian";
            if (iWord >= -13068) ret = "tiao";
            if (iWord >= -13063) ret = "tie";
            if (iWord >= -13060) ret = "ting";
            if (iWord >= -12888) ret = "tong";
            if (iWord >= -12875) ret = "tou";
            if (iWord >= -12871) ret = "tu";
            if (iWord >= -12860) ret = "tuan";
            if (iWord >= -12858) ret = "tui";
            if (iWord >= -12852) ret = "tun";
            if (iWord >= -12849) ret = "tuo";
            if (iWord >= -12838) ret = "wa";
            if (iWord >= -12831) ret = "wai";
            if (iWord >= -12829) ret = "wan";
            if (iWord >= -12812) ret = "wang";
            if (iWord >= -12802) ret = "wei";
            if (iWord >= -12607) ret = "wen";
            if (iWord >= -12597) ret = "weng";
            if (iWord >= -12594) ret = "wo";
            if (iWord >= -12585) ret = "wu";
            if (iWord >= -12556) ret = "xi";
            if (iWord >= -12359) ret = "xia";
            if (iWord >= -12346) ret = "xian";
            if (iWord >= -12320) ret = "xiang";
            if (iWord >= -12300) ret = "xiao";
            if (iWord >= -12120) ret = "xie";
            if (iWord >= -12099) ret = "xin";
            if (iWord >= -12089) ret = "xing";
            if (iWord >= -12074) ret = "xiong";
            if (iWord >= -12067) ret = "xiu";
            if (iWord >= -12058) ret = "xu";
            if (iWord >= -12039) ret = "xuan";
            if (iWord >= -11867) ret = "xue";
            if (iWord >= -11861) ret = "xun";
            if (iWord >= -11847) ret = "ya";
            if (iWord >= -11831) ret = "yan";
            if (iWord >= -11798) ret = "yang";
            if (iWord >= -11781) ret = "yao";
            if (iWord >= -11604) ret = "ye";
            if (iWord >= -11589) ret = "yi";
            if (iWord >= -11536) ret = "yin";
            if (iWord >= -11358) ret = "ying";
            if (iWord >= -11340) ret = "yo";
            if (iWord >= -11339) ret = "yong";
            if (iWord >= -11324) ret = "you";
            if (iWord >= -11303) ret = "yu";
            if (iWord >= -11097) ret = "yuan";
            if (iWord >= -11077) ret = "yue";
            if (iWord >= -11067) ret = "yun";
            if (iWord >= -11055) ret = "za";
            if (iWord >= -11052) ret = "zai";
            if (iWord >= -11045) ret = "zan";
            if (iWord >= -11041) ret = "zang";
            if (iWord >= -11038) ret = "zao";
            if (iWord >= -11024) ret = "ze";
            if (iWord >= -11020) ret = "zei";
            if (iWord >= -11019) ret = "zen";
            if (iWord >= -11018) ret = "zeng";
            if (iWord >= -11014) ret = "zha";
            if (iWord >= -10838) ret = "zhai";
            if (iWord >= -10832) ret = "zhan";
            if (iWord >= -10815) ret = "zhang";
            if (iWord >= -10800) ret = "zhao";
            if (iWord >= -10790) ret = "zhe";
            if (iWord >= -10780) ret = "zhen";
            if (iWord >= -10764) ret = "zheng";
            if (iWord >= -10587) ret = "zhi";
            if (iWord >= -10544) ret = "zhong";
            if (iWord >= -10533) ret = "zhou";
            if (iWord >= -10519) ret = "zhu";
            if (iWord >= -10331) ret = "zhua";
            if (iWord >= -10329) ret = "zhuai";
            if (iWord >= -10328) ret = "zhuan";
            if (iWord >= -10322) ret = "zhuang";
            if (iWord >= -10315) ret = "zhui";
            if (iWord >= -10309) ret = "zhun";
            if (iWord >= -10307) ret = "zhuo";
            if (iWord >= -10296) ret = "zi";
            if (iWord >= -10281) ret = "zong";
            if (iWord >= -10274) ret = "zou";
            if (iWord >= -10270) ret = "zu";
            if (iWord >= -10262) ret = "zuan";
            if (iWord >= -10260) ret = "zui";
            if (iWord >= -10256) ret = "zun";
            if (iWord >= -10254) ret = "zuo";
            if (iWord >= -10247) ret = "";

            if (iWord == -4929) ret = "wei";
            if (iWord == -9293) ret = "yan";//鄢 十六进制值-65536
            if (iWord == -7226) ret = "yan";//闫
            if (iWord == -6461) ret = "ting";//婷
            if (iWord == -5958) ret = "tao";//韬
            if (iWord == -5427) ret = "hui";//晖        
            if (iWord == -18756) ret = "dou";//都(多音字)
            if (iWord == -21608) ret = "pei";//珮
            if (iWord == -3613) ret = "xu";//胥
            if (iWord == -3133) ret = "zhu";//竺
            /*
        if(iWord == -15650)ret = "luo";//罗
        if(iWord == -15642)ret = "luo";//骆
            if(iWord == -15657)ret = "lun";//伦
            if(iWord == -15658)ret = "lun";//轮
             */
            if (iWord == -6191) ret = "mou";//缪
            if (iWord == -6151) ret = "qi";//琦       
            #endregion
            if (iWord == -4929) ret = "wei";
            if (iWord == -9293) ret = "yan";//鄢 十六进制值-65536
            if (iWord == -7226) ret = "yan";//闫
            if (iWord == -6461) ret = "ting";//婷
            if (iWord == -5958) ret = "tao";//韬
            if (iWord == -5427) ret = "hui";//晖
            if (iWord == -15650) ret = "luo";//罗
            if (iWord == -6191) ret = "mou";//缪
            if (iWord == -6151) ret = "qi";//琦
            if (iWord == -7183) ret = "long";//泷
            if (iWord == -3907) ret = "luan";//鸾
            if (iWord == -9707) ret = "qian";//倩
            //                if(source<='z')return String.valueOf(source);
            //        String s = String.valueOf(source);
            //        byte [] b = s.getBytes();
            //        short uH = b[0];
            //        uH = (short)(uH & (short)0xff);
            //        short uL = b[1];
            //        uL = (short)(uL & 0xff);
            //        
            //        int iWord = uH * 0x100 + uL - 65536 ;
            try
            {
                if (ret==(""))
                {
                    string path = System.Windows.Forms.Application.StartupPath + "\\py.Dat";
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    //计算偏移
                    int high = uH - 0x81;
                    int low = uL - 0x40;
                    int off = (high << 8) + low - high * 0x40;
                    byte[] temp = new byte[9];
                    if (off >= 0)
                    {
                        //fr.skip(off * 8);
                        fs.Seek(off * 8, SeekOrigin.Begin);
                        fs.Read(temp, 0, 8);
                        //fr.read(temp,)                
                        //fr.read(temp, 0, 8);
                        //fr.close();
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        if (temp[i] > 'z' || temp[i] < 'A')
                            continue;
                        ret += Convert.ToString ((char)temp[i]);//.ToString();
                    }
                    try { sr.Close(); }
                    catch { }
                    try { fs.Close(); }
                    catch { }
                }
            }
            catch (Exception ee)
            {
                //System.out.println("错误：取拼音 "+source+" 得到"+ee.getMessage());
            }
            return ret;

        }

        static public string GetSpell(string source)
        {
            String ret = "";
            for (int i = 0; i < source.Length; i++)
            {
                ret += GetSpell(source[i]);
            }
            //System.out.println("得到中文拼音"+ret);
            return ret.ToUpper(); ;

        }

        static public string GetSpellInitial(string source)
        {
            String ret = "";
            for (int i = 0; i < source.Length; i++)
            {
                ret += GetSpell(source[i]).Substring(0, 1);
            }
            //System.out.println("得到中文拼音"+ret);
            return ret.ToUpper(); ;

        }

        static public void sortStringListByPinYin(List<string> lsName)
        {
            try
            {
                for (int i = 0; i < lsName.Count; i++)
                {
                    lsName[i] = mystring.GetSpell(lsName[i]) + "~" + lsName[i];
                }
                lsName.Sort();
                for (int i = 0; i < lsName.Count; i++)
                {
                    lsName[i] = lsName[i].Split('~')[1];
                }
            }
            catch
            {
                lsName.Sort();
            }
        }

    }
    class rtstring
    {
        public rtstring(string rt, string pnr)
        {

        }

        int get_people_count(string rtsub,string pnr)
        {
            return 0;
        }
    }
}
/*
>rt:n/TFK2S
**ELECTRONIC TICKET PNR** ----get_people_count
 0.15PAX NM15 TFK2S 
 1.代宗长 2.龚正华 3.何香玉 4.黄士秋 5.李洁 6.林兵
 7.刘光成 8.汪国辉 9.吴军 10.吴训正 11.胥娟 12.徐留元   13.张春艳 14.张明照 15.张先宏
16.  CZ3356 X   MO15JAN  SZXWUH RR15  2040 2220          E
17.*
18.TL/1700/10JAN/WUH129 
19.SSR FOID 
20.SSR FOID 
21.SSR FOID 
22.SSR FOID 
23.SSR FOID                                                                    +
> 
>pn

24.SSR FOID CZ HK1 NI420601195112250017/P12                                    -
25.SSR FOID CZ HK1 NI420601196009217658/P8
26.SSR FOID CZ HK1 NI420623197001012028/P13 
27.SSR FOID CZ HK1 NI420601580817761/P15
28.SSR FOID CZ HK1 NI420606197801152524/P11 
29.SSR FOID CZ HK1 NI420621196408011214/P14 
30.SSR FOID CZ HK1 NI420619196407010429/P3
31.SSR FOID CZ HK1 NI510122196505030012/P1
32.SSR FOID CZ HK1 NI420623196107191015/P7
33.SSR FOID CZ HK1 NI420684198911299037/P9
34.SSR GRPS 1E TCP15 PAX
35.RMK BO/XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                                    +
> 
>pn

36.MPD                                                                         -
37.RMK CA/EXHYX 
38.RMK CLAIM PNR ACK RECEIVED
39.WUH129
>
*/