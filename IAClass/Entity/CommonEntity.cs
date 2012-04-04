using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    public enum PaymentStatus
    {
        等待付款 = 1, 交易完成 = 9
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        Female = 0, Male = 1
    }

    public struct BirthAndGender
    {
        public DateTime Birth;
        public Gender Gender;
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IdentityType
    {
        身份证 = 0, 护照 = 1, 军官证 = 2, 士兵证 = 3, 港澳通行证 = 4, 其他证件 = 5
    }
}
