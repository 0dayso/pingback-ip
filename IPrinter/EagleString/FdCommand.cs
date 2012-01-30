using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class FdCommand
    {
        public FdCommand(AvResult avr, string fd)
        {
        }
        public FdCommand(RtResult rtr, string fd)
        {
        }
        public FdCommand(string fd)
        {
        }
        /// <summary>
        /// 单条无关联fd指令
        /// </summary>
        /// <param name="fd">fd:(空格,斜杠,无,冒号)wuhpvg01may/ca</param>
        private void init(string fd)
        {
        }
    }
}
