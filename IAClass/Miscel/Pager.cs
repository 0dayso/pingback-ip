using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/* 设置控件的 CssClass="pager"
.pager a{display:inline-block; background:url(pager-bg.png) left top no-repeat; height:17px; padding-left:5px; line-height:17px; text-decoration:none; margin:3px 3px 3px 0; vertical-align:middle;}
.pager a span{ background:url(pager-bg.png) right top no-repeat; padding-right:5px; display:inline-block; cursor:pointer; height:17px; line-height:17px;}
.pager a.current{background:url(pager-bg.png) left -17px no-repeat;}
.pager a.current span{background:url(pager-bg.png) right -17px no-repeat; color:#fff;}
.pager a:link,.pager a:visited{color:#999;}
.pager a:hover{background-position:left -17px; color:#fff;}
.pager a.current:hover{color:#fff;}
.pager a:hover span{background-position:right -17px;}
.pager a.current:link,.pager a.current:visited{color:#fff;}
*/
/// <summary>
/// Control that displays a list of page numbers based on the selected page,
/// number of displayed pages, and the count of pages
/// </summary>
namespace MyWebControl
{
    public class Pager : WebControl, IPostBackEventHandler
    {
        private int m_SelectedPage, m_Count, m_displayedPages;

        public Pager()
        {
            //No constructor logic.
        }

        public int SelectedPage
        {
            get
            {
                if (m_SelectedPage == 0)
                {
                    object o = ViewState["SelectedPage"];
                    m_SelectedPage = (o != null) ? (int)o : 1;
                }
                return m_SelectedPage;
            }
            set
            {
                ViewState["SelectedPage"] = value;
                m_SelectedPage = value;
            }
        }


        public int Count
        {
            get
            {
                if (m_Count == 0)
                {
                    object o = ViewState["Count"];
                    m_Count = (o != null) ? (int)o : 1;
                }
                return m_Count;
            }
            set
            {
                ViewState["Count"] = value;
                m_Count = value;
            }
        }

        public int DisplayedPages
        {
            get
            {
                if (m_displayedPages == 0)
                {
                    object o = ViewState["DisplayedPages"];
                    m_displayedPages = (o != null) ? (int)o : 1;
                }
                return m_displayedPages;
            }
            set
            {
                ViewState["DisplayedPages"] = value;
                m_displayedPages = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
                // uncomment for a table
                // return HtmlTextWriterTag.Table;
            }
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            int prevListCount, nextListCount, startPage, endPage;

            prevListCount = Math.Abs((m_displayedPages - 1) / 2);
            if (m_SelectedPage <= prevListCount) prevListCount = m_SelectedPage - 1;
            nextListCount = m_displayedPages - prevListCount - 1;
            if (m_SelectedPage + nextListCount > m_Count) nextListCount = m_Count - m_SelectedPage;

            startPage = m_SelectedPage - prevListCount;
            endPage = m_SelectedPage + nextListCount;

            // uncomment for a table
            // writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            if (startPage > 1)
            {
                renderItem(writer, "首页", 1);
            }

            if (SelectedPage > 1)
            {
                renderItem(writer, "上页", SelectedPage - 1);
            }

            for (int count = startPage; count <= endPage; count++)
            {
                string label;
                label = count.ToString();

                if (count == m_SelectedPage)
                {
                    renderItem(writer, label, 0);
                }
                else
                {
                    renderItem(writer, label, count);
                }
            }

            if (SelectedPage < m_Count)
            {
                renderItem(writer, "下页", SelectedPage + 1);
            }

            if (endPage < m_Count)
            {
                renderItem(writer, "尾页", m_Count);
            }

            if (m_Count > 0)
            {
                renderItem(writer, "( 第" + SelectedPage + "页/共" + m_Count + "页 每页10条 )", -1);
            }
        }

        void renderItem(HtmlTextWriter writer, string text, int pageNum)
        {
            if (pageNum != -1)
            {
                if (pageNum > 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, pageNum.ToString()));
                else if (pageNum == 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "current");

                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(text);
            writer.RenderEndTag();

            if (pageNum != -1)
            {
                writer.RenderEndTag();
            }
        }

        private static readonly object EventSelectedPageChanged = null;

        public event EventHandler SelectedPageChanged
        {
            add
            {
                Events.AddHandler(EventSelectedPageChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventSelectedPageChanged, value);
            }
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            int newPage;
            if (int.TryParse(eventArgument, out newPage))
            {
                this.SelectedPage = newPage;
                OnSelectedPageChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnSelectedPageChanged(EventArgs e)
        {
            EventHandler changehandler = (EventHandler)Events[EventSelectedPageChanged];
            if (changehandler != null)
            {
                changehandler(this, e);
            }
        }
    }
}
