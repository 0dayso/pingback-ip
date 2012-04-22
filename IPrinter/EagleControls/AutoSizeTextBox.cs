using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;
using System.Drawing;
namespace Hob.Toolbox.Controls
{
    /// <summary>
    /// 锁定的位置
    /// </summary>
    public enum LockPosition
    {
        LockTopLeft,
        LockTopHCenter,
        LockTopRight,
        LockVCenterRight,
        LockBottomRight,
        LockBottomHCenter,
        LockBottomLeft,
        LockVCenterLeft,
        LockVCenterHCenter
    }
    public class AutoSizeTextBox : TextBox
    {
        private Point m_unchangeLocation;//不变化的位置坐标
        private bool m_unchangleLocationInit;//是否已经初始化m_unchangeLocation
        private IntPtr m_FontHandleWhenNotRound;//不采用四舍五入时的font句柄

        private Color m_BorderColor;
        [Category("Appearance"), Description("边框的颜色")]
        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
                InvalidateFrame();
            }
        }

        private string m_waterMarkText;
        [Category("Appearance"), Description("水印文字")]
        public string WaterMarkText
        {
            get { return m_waterMarkText; }
            set
            {
                m_waterMarkText = value;
            }
        }

        private Color m_waterMarkTextColor = Color.DarkGray;
        [Category("Appearance"), Description("水印文字颜色")]
        public Color WaterMarkTextColor
        {
            get { return m_waterMarkTextColor; }
            set
            {
                m_waterMarkTextColor = value;
            }
        }

        private LockPosition m_LockPosition;
        [Category("Layout"), Description("锁定的不变位置"), DefaultValue(LockPosition.LockTopLeft)]
        public LockPosition TextLockPosition
        {//修改名字为TextLockPosition，使得InitializeComponent在text改变后加载TextLockPosition属性
            get
            {
                return m_LockPosition;
            }
            set
            {
                m_LockPosition = value;
                UpdateunchangeLocation();
            }
        }

        private bool m_RoundFontHeight;
        [Category("Appearance"), Description("是否对字体高度进行四舍五入"), DefaultValue(true)]
        public bool RoundFontHeight
        {
            get { return m_RoundFontHeight; }
            set
            {
                m_RoundFontHeight = value;
                if (!value)
                    SetWindowFont();
                //set auto size 
                AdjustSize();
            }
        }

        /// <summary>
        /// 覆盖Location
        /// </summary>
        public new Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
                UpdateunchangeLocation();
            }
        }

        #region windows api
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = string.Empty;
        }
        private const uint WM_NCPAINT = 0x0085;
        private const uint WM_PAINT = 0x000F;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        public const uint WM_SETFONT = 0x0030;
        [DllImport("user32.dll", EntryPoint = "GetWindowDC", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("gdi32.dll", EntryPoint = "CreateFontIndirect", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFontIndirect(LOGFONT lf);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wparam, IntPtr lparam);
        #endregion

        public AutoSizeTextBox()
        {
            m_LockPosition = LockPosition.LockTopLeft;
            m_BorderColor = Color.Gray;
            m_unchangleLocationInit = false;
            m_RoundFontHeight = true;
            m_FontHandleWhenNotRound = IntPtr.Zero;
            this.MinimumSize = new Size(100, 21);//源生textbox的默认尺寸
        }
        /// <summary>
        /// 恢复到最小尺寸
        /// </summary>
        public void MiniSize()
        {
            ScrollBars = ScrollBars.None;
            AdjustSize(MinimumSize);
        }
        /// <summary>
        /// 调整大小(自适应)
        /// </summary>
        public void AdjustSize()
        {
            Size size = GetRightClientSize();
            AdjustSize(size);
        }
        /// <summary>
        /// 调整大小
        /// </summary>
        public void AdjustSize(Size size)
        {
            if (!m_unchangleLocationInit) return;

            if (size.Width > MaximumSize.Width && size.Height > MaximumSize.Height)
                ScrollBars = ScrollBars.Both;
            else if (size.Width > MaximumSize.Width)
                ScrollBars = ScrollBars.Horizontal;
            else if (size.Height > MaximumSize.Height)
                ScrollBars = ScrollBars.Vertical;

            this.Size = size;//ClientSize = size;

            switch (m_LockPosition)//保持位置
            {
                case LockPosition.LockTopLeft:
                    Location = m_unchangeLocation;
                    break;
                case LockPosition.LockTopHCenter:
                    Location = new Point(m_unchangeLocation.X - Size.Width / 2, m_unchangeLocation.Y);
                    break;
                case LockPosition.LockTopRight:
                    Location = new Point(m_unchangeLocation.X - Size.Width, m_unchangeLocation.Y);
                    break;
                case LockPosition.LockVCenterRight:
                    Location = new Point(m_unchangeLocation.X - Size.Width, m_unchangeLocation.Y - Size.Height / 2);
                    break;
                case LockPosition.LockBottomRight:
                    Location = new Point(m_unchangeLocation.X - Size.Width, m_unchangeLocation.Y - Size.Height);
                    break;
                case LockPosition.LockBottomHCenter:
                    Location = new Point(m_unchangeLocation.X - Size.Width / 2, m_unchangeLocation.Y - Size.Height);
                    break;
                case LockPosition.LockBottomLeft:
                    Location = new Point(m_unchangeLocation.X, m_unchangeLocation.Y - Size.Height);
                    break;
                case LockPosition.LockVCenterLeft:
                    Location = new Point(m_unchangeLocation.X, m_unchangeLocation.Y - Size.Height / 2);
                    break;
                case LockPosition.LockVCenterHCenter:
                    Location = new Point(m_unchangeLocation.X - Size.Width / 2, m_unchangeLocation.Y - Size.Height / 2);
                    break;
            }

            InvalidateFrame();
        }
        /// <summary>
        /// 重绘边框
        /// </summary>
        private void InvalidateFrame()
        {
            if (BorderStyle == BorderStyle.Fixed3D)
                SetWindowPos(Handle, (IntPtr)0, 0, 0, 0, 0,
                 SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED);
        }
        /// <summary>
        /// 得到合适的客户区大小
        /// </summary>
        /// <returns></returns>
        private Size GetRightClientSize()
        {
            Size size = GetRightTextSize();
            int borderwidth = GetBorderWidth();
            size.Width += borderwidth * 2;
            size.Height += borderwidth * 2;
            return size;
        }
        /// <summary>
        /// 得到合适的字体尺寸
        /// </summary>
        /// <returns></returns>
        private Size GetRightTextSize()
        {
            String str = Text;
            int len = str.Length;
            if (len < 2) str = "t";//至少一个宽度
            int count = Lines.Length;
            if (count >= 1)
                if (Lines[count - 1] == "")
                    str += "t";
            Size size;
            if (m_RoundFontHeight)
            {
                //采用了四舍五入，但是TextRenderer.MeasureText不是四舍五入来计算的
                //所以我们要传给他正确的font
                LOGFONT logfont = new LOGFONT();
                Font.ToLogFont(logfont);
                Font measureFont = Font.FromLogFont(logfont);
                size = TextRenderer.MeasureText(str, measureFont);
                measureFont.Dispose();
            }
            else
                size = TextRenderer.MeasureText(str, Font);

            return size;
        }
        /// <summary>
        /// 得到合适的大小(客户区和边框一起)
        /// </summary>
        /// <returns></returns>
        private Size GetRightSize()
        {
            Size size = GetRightClientSize();
            int borderwidth = GetBorderWidth();
            size.Width += borderwidth * 2;
            size.Height += borderwidth * 2;
            return size;
        }
        /// <summary>
        /// 当文本改变时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            //call base
            base.OnTextChanged(e);
            //set auto size 
            AdjustSize();
        }
        /// <summary>
        /// 字体变化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AdjustSize();
        }
        /// <summary>
        /// border改变
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBorderStyleChanged(EventArgs e)
        {
            base.OnBorderStyleChanged(e);
            AdjustSize();
        }
        /// <summary>
        /// 得到边框宽度
        /// </summary>
        /// <returns></returns>
        private int GetBorderWidth()
        {
            if (BorderStyle == BorderStyle.Fixed3D)
                return 2;
            else if (BorderStyle == BorderStyle.FixedSingle)
                return 1;
            else //none
                return 0;
        }
        /// <summary>
        /// 重载wndproc
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //字体不采用四舍五入时,那么我们纠正字体
            if (!m_RoundFontHeight && m.Msg == WM_SETFONT)
                m.WParam = CreateFontHandleWhenNotRound();
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
                DrawFixedSingle();
            if (m.Msg == WM_NCPAINT)
                DrawFixed3D();
            if (m.Msg == WM_PAINT || m.Msg == WM_NCPAINT)
                WmPaint(ref m);
        }
        private void WmPaint(ref Message m)
        {
            using (Graphics graphics = Graphics.FromHwnd(base.Handle))
            {
                if (Text.Length == 0 && !string.IsNullOrEmpty(m_waterMarkText) && !Focused)
                {
                    TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }

                    TextRenderer.DrawText(graphics, m_waterMarkText, Font, base.ClientRectangle, m_waterMarkTextColor, format);
                }
            }
        }
        /// <summary>
        /// 绘制非客户区边框
        /// </summary>
        private void DrawFixed3D()
        {
            if (BorderStyle == BorderStyle.Fixed3D)
            {
                Pen pen = new Pen(this.m_BorderColor);
                IntPtr windc = GetWindowDC(Handle);
                Graphics borderG = Graphics.FromHdc(windc);
                borderG.DrawRectangle(pen, 0, 0, Size.Width - 1, Size.Height - 1);
                ReleaseDC(Handle, windc);
                borderG.Dispose();
                pen.Dispose();
            }
        }
        /// <summary>
        /// 绘制客户区边框
        /// </summary>
        private void DrawFixedSingle()
        {
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                Pen pen = new Pen(this.m_BorderColor);
                Graphics dc = Graphics.FromHwnd(Handle);
                dc.DrawRectangle(pen, 0, 0, Size.Width - 1, Size.Height - 1);
                dc.Dispose();
                pen.Dispose();
            }
        }
        /// <summary>
        /// 更新不变的位置
        /// </summary>
        private void UpdateunchangeLocation()
        {
            //求不变的位置坐标
            switch (m_LockPosition)//求保持的位置
            {
                case LockPosition.LockTopLeft:
                    m_unchangeLocation = Location;
                    break;
                case LockPosition.LockTopHCenter:
                    m_unchangeLocation = new Point(Location.X + Size.Width / 2, Location.Y);
                    break;
                case LockPosition.LockTopRight:
                    m_unchangeLocation = new Point(Location.X + Size.Width, Location.Y);
                    break;
                case LockPosition.LockVCenterRight:
                    m_unchangeLocation = new Point(Location.X + Size.Width, Location.Y + Size.Height / 2);
                    break;
                case LockPosition.LockBottomRight:
                    m_unchangeLocation = new Point(Location.X + Size.Width, Location.Y + Size.Height);
                    break;
                case LockPosition.LockBottomHCenter:
                    m_unchangeLocation = new Point(Location.X + Size.Width / 2, Location.Y + Size.Height);
                    break;
                case LockPosition.LockBottomLeft:
                    m_unchangeLocation = new Point(Location.X, Location.Y + Size.Height);
                    break;
                case LockPosition.LockVCenterLeft:
                    m_unchangeLocation = new Point(Location.X, Location.Y + Size.Height / 2);
                    break;
                case LockPosition.LockVCenterHCenter:
                    m_unchangeLocation = new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
                    break;
            }
            m_unchangleLocationInit = true;
        }
        //重载禁止调整高度
        //protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        //{
        //    Size size = GetRightSize();
        //    if (width != size.Width && ((specified & BoundsSpecified.Width) == BoundsSpecified.Width))
        //        width = size.Width;
        //    if (height != size.Height && ((specified & BoundsSpecified.Height) == BoundsSpecified.Height))
        //        height = size.Height;
        //    //call base
        //    base.SetBoundsCore(x, y, width, height, specified);
        //}
        private IntPtr CreateFontHandleWhenNotRound()
        {
            if (m_FontHandleWhenNotRound == IntPtr.Zero)
            {
                LOGFONT logfont = new LOGFONT();
                Font.ToLogFont(logfont);
                Graphics dc = Graphics.FromHwnd(Handle);
                //求字体高度
                int num = (int)Math.Ceiling(Font.Size * dc.DpiX / 72f);
                dc.Dispose();
                logfont.lfHeight = -num;
                m_FontHandleWhenNotRound = CreateFontIndirect(logfont);
            }
            return m_FontHandleWhenNotRound;
        }
        private void DeleteFontHandleWhenNotRound()
        {
            if (m_FontHandleWhenNotRound != IntPtr.Zero)
            {
                DeleteObject(m_FontHandleWhenNotRound);
                m_FontHandleWhenNotRound = IntPtr.Zero;
            }
        }
        private void SetWindowFont()
        {
            SendMessage(Handle, WM_SETFONT, (IntPtr)0, (IntPtr)0);
        }
        /// <summary>
        /// 重载dispose,释放非托管资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //do something
            if (!IsDisposed)
            {
                if (disposing)
                {
                    //free managed resource
                }
                //free unmanaged resource
                DeleteFontHandleWhenNotRound();
            }
            base.Dispose(disposing);
        }
    }
}