using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class CommandPool
    {
        private LoginInfo m_li;
        public CommandPool()
        {
        }
        public CommandPool(LoginInfo li)
        {
            m_li = li;
        }
        /// <summary>
        /// 最后一条指令串
        /// </summary>
        public string LAST { get { if (ls.Count == 0)return ""; else return ls[ls.Count - 1]; } }
        /// <summary>
        /// 最后一条指令类型
        /// </summary>
        public ETERM_COMMAND_TYPE TYPE { get { return m_cmdtype; } }
        /// <summary>
        /// 若当前最后一条指令类型为PN,取PN之前的指令类型，否则为最后一条类型
        /// </summary>
        public ETERM_COMMAND_TYPE TYPELAST
        {
            get
            {
                for (int i = ls.Count - 1; i >= 0; --i)
                {
                    ETERM_COMMAND_TYPE t = CommandType(ls[i]);
                    if (t == ETERM_COMMAND_TYPE.PN) continue;
                    else return t;
                }
                return ETERM_COMMAND_TYPE.NONE;
            }
        }
        public void Clear()
        {
            m_cmdtype = ETERM_COMMAND_TYPE.NONE;
            ls.Clear();
        }
        /// <summary>
        /// 正在用RT指令操作的PNR
        /// </summary>
        public string PNRing { get { return m_rtpnr; } set { m_rtpnr = value; } }

        string m_rtpnr = "";
        /// <summary>
        /// 发送过的指令列表
        /// </summary>
        List<string> ls = new List<string>();
        ETERM_COMMAND_TYPE m_cmdtype = ETERM_COMMAND_TYPE.NONE;
        /// <summary>
        /// 将待发送的指令送到池中处理，得到具体要发送的文本
        /// </summary>
        public string HandleCommand(string cmd)
        {

            m_cmdtype = CommandType(cmd);
            switch (m_cmdtype)
            {
                case ETERM_COMMAND_TYPE.AV:
                    if (EagleString.egString.right(cmd.ToLower().Trim(), 3) == "/eg")
                    {
                        System.Windows.Forms.MessageBox.Show("AV指令不可以自动翻页");

                        return "";
                    }
                    break;
                case ETERM_COMMAND_TYPE.DETR:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.DETRF:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.TRFD:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.TRFU:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.TRFX:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.QT:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.TPR:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.TSL:
                    ls.Clear();
                    break;
                case ETERM_COMMAND_TYPE.ETDZ:
                    if (ls[0].ToLower().IndexOf("rt") != 0) throw new Exception("未产生PNR,不能ETDZ");
                    break;
            }
            if (!RelationCommand(cmd)) return cmd;
            string s = cmd.ToLower();
            if (s == "i" || s == "ig")
            {
                m_cmdtype = ETERM_COMMAND_TYPE.NONE;
                ls.Clear();
                return "i";
            }
            if (ls.Count > 0)
            {
                string last = ls[ls.Count - 1];
                if (SameCommand(s))
                {
                    if (s.IndexOf("xe") == 0 || s.IndexOf("pn") == 0 || s.IndexOf("pb") == 0)
                    {
                        ls.Add(s);//不重复上一次指令才增加(XE,PN,PB除外)
                    }
                    else
                    {
                        ls.RemoveAt(ls.Count - 1);
                        ls.Add(s);//替换掉最后一个指令
                    }
                }
                else
                {
                    if (last.IndexOf("@") >= 0 || last.IndexOf("\\") >= 0)//若上一条为封口，则先清空
                    {
                        ls.Clear();
                    }
                    ls.Add(s);
                }
            }
            else
            {
                ls.Add(s);
            }
            CommandSendControl(s);
            return "i~" + string.Join("~", ls.ToArray());
        }
        /// <summary>
        /// 设置指令类型
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public void SetCommandType(string cmd)
        {
            m_cmdtype = CommandType(cmd);
        }
        /// <summary>
        /// 若该指令前后无相关性，直接发送
        /// </summary>
        private bool RelationCommand(string cmd)
        {
            string s = cmd.ToLower();
            string[] a = new string[] { "cd", "cntd", "co", "cv", "date", "dsm", "tim", "time", "wf","da","di","si","so" };
            for (int i = 0; i < a.Length; ++i)
            {
                if(s.IndexOf(a[i])==0)return false;
            }
            return true;
        }
        private bool SameCommand(string cmd)
        {
            if (ls.Count == 0) return false;
            string s = cmd.ToLower();
            string t = ls[ls.Count - 1];
            if (s == t) return true;
            if (s.IndexOf("av") == 0 && t.IndexOf("av") == 0) return true;
            return false;
        }
        /// <summary>
        /// 在池中处理前，判断该指令的类型，以便与返回结果结合做相应处理
        /// </summary>
        private ETERM_COMMAND_TYPE CommandType(string cmd)
        {
            string s = cmd.ToLower().Trim();
            ETERM_COMMAND_TYPE ret = ETERM_COMMAND_TYPE.NONE;
            if (s.IndexOf("a") == 0)//AV指令可缩写为A
            {
                ret = ETERM_COMMAND_TYPE.AV;
            }
            else if (s.IndexOf("etdz") == 0)
            {
                ret = ETERM_COMMAND_TYPE.ETDZ;
            }
            else if (s.IndexOf("trfd") == 0)
            {
                ret = ETERM_COMMAND_TYPE.TRFD;
            }
            else if (s.IndexOf("trfx") == 0)
            {
                ret = ETERM_COMMAND_TYPE.TRFX;
            }
            else if (s.IndexOf("rt") == 0)
            {
                if (s.Length >= 7)
                {
                    ret = ETERM_COMMAND_TYPE.RT;
                    if (EagleString.egString.right(s, 3).ToLower() == "/eg" && s.Length>=10)
                    {
                        string temp = s.Substring(0, s.Length - 3).Trim();
                        //m_rtpnr = egString.right(temp, 5);//modified by king 2010.12.07 升级6位PNR
                        m_rtpnr = s.Substring(temp.LastIndexOf("/") + 1);
                    }
                    else
                    {
                        //m_rtpnr = egString.right(s, 5);//modified by king 2010.12.07 升级6位PNR
                        m_rtpnr = s.Substring(s.LastIndexOf("/") + 1);
                    }
                }
            }
            else if (s.IndexOf("sfc") == 0)
            {
                ret = ETERM_COMMAND_TYPE.SFC;
            }
            else if (s.IndexOf("pat") == 0)
            {
                ret = ETERM_COMMAND_TYPE.PAT;
            }
            else if (IsEntireSsCommand(s))
            {
                ret = ETERM_COMMAND_TYPE.SS;
            }
            else if (s.IndexOf("xepnr") == 0)
            {
                ret = ETERM_COMMAND_TYPE.XEPNR;
            }
            else if (s.IndexOf("pn") == 0)
            {
                ret = ETERM_COMMAND_TYPE.PN;
            }
            else if (s.IndexOf("detr") == 0 && s.IndexOf(",f") > 0)
            {
                ret = ETERM_COMMAND_TYPE.DETRF;
            }
            else if (s.IndexOf("detr") == 0)
            {
                ret = ETERM_COMMAND_TYPE.DETR;
            }
            else if (s.IndexOf("tpr") == 0)
            {
                ret = ETERM_COMMAND_TYPE.TPR;
            }
            else if (s.IndexOf("tsl") == 0)
            {
                ret = ETERM_COMMAND_TYPE.TSL;
            }
            else if (s[0] >= '0' && s[0] <= '9')
            {
                ret = ETERM_COMMAND_TYPE.MODIFYING;
            }
            else if (s.IndexOf("fd") == 0)
            {
                ret = ETERM_COMMAND_TYPE.FD;
            }
            return ret;
        }
        /// <summary>
        /// 在窗口中操作的特殊类型
        /// </summary>
        /// <param name="type"></param>
        public void SetType(ETERM_COMMAND_TYPE type)
        {
            switch (type)
            {
                case ETERM_COMMAND_TYPE.RECEIPT_PRINT:
                    this.m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.RECEIPT_CANCEL:
                    this.m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.ETDZ_ONEKEY_RT:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.ETDZ_ONEKEY_PAT:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.ETDZ:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.PNR_ORDER_SUBMIT:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.QUEUE_CLEAR_AUTO:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE .RRT_AIRCODE2PNR:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.TPR_IMPORT:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.TOL_INCOMING:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.SS_4PassengerAddForm:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.DETR_ExpiredTicketFind:
                    m_cmdtype = type;
                    break;
                case ETERM_COMMAND_TYPE.DETR_GetReceiptNoFinance:
                    m_cmdtype = type;
                    break;
                default:
                    throw new Exception("不能通过SetType设置，需要调用HandleCommand");
            }

        }
        /// <summary>
        /// 在ls中是否有sd或ss指令
        /// </summary>
        private bool HasSsInList()
        {
            for (int i = 0; i < ls.Count; ++i)
            {
                if (ls[i].ToLower().IndexOf("ss") == 0 || ls[i].ToLower().IndexOf("sd") == 0)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 是否完整的SS指令
        /// </summary>
        /// <param name="cmd">当前指令</param>
        /// <returns></returns>
        private bool IsEntireSsCommand(string cmd)
        {
            cmd = cmd.ToLower();
            if ((cmd.IndexOf("ss") == 0 || cmd.IndexOf("sd") == 0) && (cmd.IndexOf("@") > 0 || cmd.IndexOf("\\") > 0)) return true;
            if (HasSsInList() && (cmd.IndexOf("@") >= 0 || cmd.IndexOf("\\") >= 0)) return true;
            return false;
        }
        /// <summary>
        /// 在发送前的指令控制,如XEPNR需要检查是否有权限取消,若无权限则抛出异常
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool CommandSendControl(string cmd)
        {
            switch (m_cmdtype)
            {
                case ETERM_COMMAND_TYPE.XEPNR:
                    CheckXepnr();
                    break;
            }
            return true;
        }
        void CheckXepnr()
        {
            ETERM_COMMAND_TYPE t = new ETERM_COMMAND_TYPE();
            for (int i = 0; i < ls.Count; ++i)
            {
                t = CommandType(ls[i]);
                if (t == ETERM_COMMAND_TYPE.RT) return;
            }
            
            throw new Exception("\r\n请先RT操作");
        }
    }
    public enum ETERM_COMMAND_TYPE : byte
    {
        AV = 0,
        ETDZ = 1,
        TRFD = 2,
        RT = 3,
        SFC = 4,
        PAT = 5,
        /// <summary>
        /// 完整的SS指令:ss,nm,ct,tktl,@
        /// </summary>
        SS = 6,
        XEPNR = 7,
        PN = 8,
        DETR = 9,
        DETRF = 10,
        RECEIPT_PRINT = 11,
        RECEIPT_CANCEL = 12,
        TPR = 13,
        TSL = 14,
        ETDZ_ONEKEY_RT = 15,
        ETDZ_ONEKEY_PAT = 16,
        TRFX = 17,
        TRFU = 18,
        PNR_ORDER_SUBMIT = 19,
        QT = 20,
        QUEUE_CLEAR_AUTO = 21,
        RRT_AIRCODE2PNR = 22,
        TPR_IMPORT = 23,
        TOL_INCOMING = 24,
        SS_4PassengerAddForm = 25,
        SS_ONLY = 26,//表示没有封口的SS
        MODIFYING = 27,//表示数字开头的指令,主要用于检查RR指令
        FD = 28,
        RT_BACKGROUND = 29,
        DETR_ExpiredTicketFind = 30,
        DETR_GetReceiptNoFinance=31,
        NONE = 99
    }
}
