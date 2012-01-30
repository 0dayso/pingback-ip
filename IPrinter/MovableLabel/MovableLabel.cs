using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TomatoControls
{
    [Description("可以使文字滚动的文字标签。作者：来萌")]
    public partial class MovableLabel : Label 
    {
        public MovableLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 文字移动方向。
        /// </summary>
        public enum MoveDirection : byte  
        {
            /// <summary>
            /// 从右到左
            /// </summary>
            RightToLeft = 0,
            /// <summary>
            /// 从左到右
            /// </summary>
            LeftToRight = 1,
            /// <summary>
            /// 自上而下
            /// </summary>
            TopToBottom = 2,
            /// <summary>
            /// 自下而上
            /// </summary>
            BottomToTop = 3
        };


        #region 属性
        
        [
        Category("移动行为"),
        Description("包含的文字是否可移动。"),
        Browsable(true)
        ]
        /// <summary>
        /// 包含的文字是否可移动。
        /// </summary>
        public bool Movable
        {
            get
            {
                return timer.Enabled; 
            }
            set
            {
                this.timer.Enabled = value;
                this.Invalidate(); 
            }
        }

        [
        Category("移动行为"),
        Description("文字移动速度(0慢-9快)。"),
        Browsable(true),
        DefaultValue(3) 
        ]
        /// <summary>
        /// 文字移动速度(0-9)
        /// </summary>
        public int Interval
        {
            get
            {
                return 10 - this.timer.Interval/10;  
            }
            set
            {
                if (value > 9 || value < 0)
                    throw new Exception("文字移动速度为0-9的整数。");   
                this.timer.Interval = (10 - value) * 10; 
            }
        }
        #endregion

        #region 保护变量
        protected Color m_borderColor = Color.Black;
        protected bool m_movable = false;
        protected MoveDirection m_TextMoveDirection = MoveDirection.RightToLeft;

        Timer timer = new Timer(); 
        #endregion

        public event EventHandler SlideOver = null;

        protected virtual void OnSlideOver(EventArgs e)
        {
            if (SlideOver != null)
            {
                SlideOver(this, e); 
            }

        }
        
    }
}