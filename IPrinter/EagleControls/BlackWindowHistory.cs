using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EagleControls
{
    /// <summary>
    /// ��������ʷָ����
    /// </summary>
    public class BlackWindowHistory
    {
        public BlackWindowHistory(int maxHistory)
        {
            m_HistoryMax = maxHistory;
        }
        /// <summary>
        /// �α�
        /// </summary>
        private int m_HistoryPos = 0;
        private int m_HistoryMax = 64;
        private List<string> m_HistoryQueue = new List<string> ();
        /// <summary>
        /// �����Ϸ����·�ָ��ʱ��true,�����б�������ʷָ��ʱ��Ϊfalse
        /// </summary>
        private bool m_history_selecting = false;
        /// <summary>
        /// ���������ʷָ��
        /// </summary>
        private string m_history_inserted = "";

        /// <summary>
        /// ����һ����ʷָ��
        /// </summary>
        public void History_Add(string txt)
        {
            m_history_selecting = false;
            if (m_HistoryQueue.Count >= m_HistoryMax)
            {
                m_HistoryQueue.Remove(m_HistoryQueue[0]);
            }
            m_HistoryQueue.Add(txt);
            m_HistoryPos = m_HistoryQueue.Count;
        }
        /// <summary>
        /// ȡ��һ����ʷָ��
        /// </summary>
        private string History_Back()
        {
            if (m_HistoryQueue.Count == 0) return "";
            --m_HistoryPos;
            if (m_HistoryPos < 0) m_HistoryPos = 0;
            return m_HistoryQueue[m_HistoryPos];
        }
        /// <summary>
        /// ȡ��һ����ʷָ��
        /// </summary>
        private string History_Next()
        {
            if (m_HistoryQueue.Count == 0) return "";
            ++m_HistoryPos;

            if (m_HistoryPos >= m_HistoryMax || m_HistoryPos >= m_HistoryQueue.Count)
            {
                
                m_HistoryPos = m_HistoryQueue.Count;
                return "";
            }
            
            return m_HistoryQueue[m_HistoryPos];
        }
        /// <summary>
        /// �����ʷָ��
        /// </summary>
        private void History_Clear()
        {
            m_HistoryQueue.Clear();
        }
        /// <summary>
        /// ��ѡ������ʷָ����뵱ǰλ��
        /// </summary>
        public void HistoryInsert(RichTextBox rtb,bool next)
        {
            string cmd = (next ? History_Next() : History_Back());
            //if selecting, remove last selected in editbox
            int newstart = rtb.SelectionStart;
            if (m_history_selecting)
            {
                newstart = rtb.SelectionStart - m_history_inserted.Length;
                rtb.Text = rtb.Text.Remove(newstart, m_history_inserted.Length);
            }
            m_history_selecting = true;
            rtb.SelectionStart = newstart;
            m_history_inserted = cmd;
            InsertString(rtb,cmd);
        }
        /// <summary>
        /// �ڵ�ǰλ�ò��룬���ѵ�ǰλ����Ϊԭλ��+���볤��
        /// </summary>
        /// <param name="str"></param>
        private void InsertString(RichTextBox rtb, string str)
        {
            int pos = rtb.SelectionStart;
            rtb.Text = rtb.Text.Insert(pos, str);
            rtb.SelectionStart = pos + str.Length;
        }
        /// <summary>
        /// �����·���ʷָ����ʱ������selecting
        /// </summary>
        public void ResetSelecting()
        {
            m_history_selecting = false;
        }
        public void ResetPos()
        {
            m_HistoryPos = m_HistoryQueue.Count;
        }
    }
 
}
