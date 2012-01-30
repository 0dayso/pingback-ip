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
        /// ���һ��ָ�
        /// </summary>
        public string LAST { get { if (ls.Count == 0)return ""; else return ls[ls.Count - 1]; } }
        /// <summary>
        /// ���һ��ָ������
        /// </summary>
        public ETERM_COMMAND_TYPE TYPE { get { return m_cmdtype; } }
        /// <summary>
        /// ����ǰ���һ��ָ������ΪPN,ȡPN֮ǰ��ָ�����ͣ�����Ϊ���һ������
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
        /// ������RTָ�������PNR
        /// </summary>
        public string PNRing { get { return m_rtpnr; } set { m_rtpnr = value; } }

        string m_rtpnr = "";
        /// <summary>
        /// ���͹���ָ���б�
        /// </summary>
        List<string> ls = new List<string>();
        ETERM_COMMAND_TYPE m_cmdtype = ETERM_COMMAND_TYPE.NONE;
        /// <summary>
        /// �������͵�ָ���͵����д����õ�����Ҫ���͵��ı�
        /// </summary>
        public string HandleCommand(string cmd)
        {

            m_cmdtype = CommandType(cmd);
            switch (m_cmdtype)
            {
                case ETERM_COMMAND_TYPE.AV:
                    if (EagleString.egString.right(cmd.ToLower().Trim(), 3) == "/eg")
                    {
                        System.Windows.Forms.MessageBox.Show("AVָ������Զ���ҳ");

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
                    if (ls[0].ToLower().IndexOf("rt") != 0) throw new Exception("δ����PNR,����ETDZ");
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
                        ls.Add(s);//���ظ���һ��ָ�������(XE,PN,PB����)
                    }
                    else
                    {
                        ls.RemoveAt(ls.Count - 1);
                        ls.Add(s);//�滻�����һ��ָ��
                    }
                }
                else
                {
                    if (last.IndexOf("@") >= 0 || last.IndexOf("\\") >= 0)//����һ��Ϊ��ڣ��������
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
        /// ����ָ������
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public void SetCommandType(string cmd)
        {
            m_cmdtype = CommandType(cmd);
        }
        /// <summary>
        /// ����ָ��ǰ��������ԣ�ֱ�ӷ���
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
        /// �ڳ��д���ǰ���жϸ�ָ������ͣ��Ա��뷵�ؽ���������Ӧ����
        /// </summary>
        private ETERM_COMMAND_TYPE CommandType(string cmd)
        {
            string s = cmd.ToLower().Trim();
            ETERM_COMMAND_TYPE ret = ETERM_COMMAND_TYPE.NONE;
            if (s.IndexOf("a") == 0)//AVָ�����дΪA
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
                        //m_rtpnr = egString.right(temp, 5);//modified by king 2010.12.07 ����6λPNR
                        m_rtpnr = s.Substring(temp.LastIndexOf("/") + 1);
                    }
                    else
                    {
                        //m_rtpnr = egString.right(s, 5);//modified by king 2010.12.07 ����6λPNR
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
        /// �ڴ����в�������������
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
                    throw new Exception("����ͨ��SetType���ã���Ҫ����HandleCommand");
            }

        }
        /// <summary>
        /// ��ls���Ƿ���sd��ssָ��
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
        /// �Ƿ�������SSָ��
        /// </summary>
        /// <param name="cmd">��ǰָ��</param>
        /// <returns></returns>
        private bool IsEntireSsCommand(string cmd)
        {
            cmd = cmd.ToLower();
            if ((cmd.IndexOf("ss") == 0 || cmd.IndexOf("sd") == 0) && (cmd.IndexOf("@") > 0 || cmd.IndexOf("\\") > 0)) return true;
            if (HasSsInList() && (cmd.IndexOf("@") >= 0 || cmd.IndexOf("\\") >= 0)) return true;
            return false;
        }
        /// <summary>
        /// �ڷ���ǰ��ָ�����,��XEPNR��Ҫ����Ƿ���Ȩ��ȡ��,����Ȩ�����׳��쳣
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
            
            throw new Exception("\r\n����RT����");
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
        /// ������SSָ��:ss,nm,ct,tktl,@
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
        SS_ONLY = 26,//��ʾû�з�ڵ�SS
        MODIFYING = 27,//��ʾ���ֿ�ͷ��ָ��,��Ҫ���ڼ��RRָ��
        FD = 28,
        RT_BACKGROUND = 29,
        DETR_ExpiredTicketFind = 30,
        DETR_GetReceiptNoFinance=31,
        NONE = 99
    }
}
