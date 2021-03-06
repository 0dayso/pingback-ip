﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAClass.Entity;

namespace IAClass.Issuing
{
    /*
    public class Issuing : IIssuing
    {
        public IssuingResultEntity Issue(IssueEntity entity)
        {
            return new IssuingResultEntity();
        }

        public TraceEntity Withdraw(WithdrawEntity entity)
        {
            return new TraceEntity();
        }

        public TraceEntity Validate(IssueEntity entity)
        {
            return new TraceEntity();
        }
    }
     */

    /// <summary>
    /// 投保操作类接口定义
    /// </summary>
    interface IIssuing
    {
        /// <summary>
        /// 投保
        /// </summary>
        /// <param name="entity">投保信息实体类</param>
        /// <returns></returns>
        IssuingResultEntity Issue(IssueEntity entity);

        /// <summary>
        /// 退保
        /// </summary>
        /// <param name="policyNo">正式保单号</param>
        /// <returns></returns>
        TraceEntity Withdraw(WithdrawEntity entity);

        /// <summary>
        /// 检查数据完整性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TraceEntity Validate(IssueEntity entity);
    }
}
