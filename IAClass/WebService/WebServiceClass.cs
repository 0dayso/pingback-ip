using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAClass.Entity;
using NBearLite;
using NBear.Mapping;
using System.Text.RegularExpressions;
using System.Threading;

namespace IAClass.WebService
{
    public class WebServiceClass
    {
        public static PolicyResponseEntity GetPolicy(string username, string password, string caseNo)
        {
            PolicyResponseEntity response = new PolicyResponseEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(username, password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    response.Policy = Case.Get(caseNo);
                }
                else
                {
                    response.Trace = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                response.Trace.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }

        public static PolicyListResponseEntity GetPolicyList(string username, string password, int pageSize)
        {
            PolicyListResponseEntity response = new PolicyListResponseEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(username, password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    response.PolicyList = Case.GetList(username, pageSize);
                }
                else
                {
                    response.Trace = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                response.Trace.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }

        public static PolicyListResponseEntity GetPolicyListBetween(string username, string password, DateTime dtStart, DateTime dtEnd)
        {
            PolicyListResponseEntity response = new PolicyListResponseEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(username, password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    response.PolicyList = Case.GetPolicyListBetween(username, dtStart, dtEnd);
                }
                else
                {
                    response.Trace = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                response.Trace.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }

        public static TraceEntity Feedback(t_Feedback req)
        {
            TraceEntity response = new TraceEntity();
            try
            {
                Common.DB.Insert(Tables.t_Feedback)
                    .AddColumn(Tables.t_Feedback.fromIP, System.Web.HttpContext.Current.Request.UserHostAddress)
                    .AddColumn(Tables.t_Feedback.fromLocation, req.fromLocation)
                    .AddColumn(Tables.t_Feedback.fromWho, req.fromWho)
                    .AddColumn(Tables.t_Feedback.remark, req.remark)
                    .AddColumn(Tables.t_Feedback.subject, req.subject)
                    .AddColumn(Tables.t_Feedback.timer, req.timer)
                    .Execute();
            }
            catch (Exception e)
            {
                response.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }

        private static bool IsIpChecked(string ipLocation, string include, string exclude)
        {
            if (!string.IsNullOrEmpty(include))
            {
                bool isIncluded = false;
                foreach (string region in include.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (ipLocation.Contains(region))
                    {
                        isIncluded = true;
                        break;
                    }
                    else
                        continue;
                }

                if (!isIncluded)
                {
                    return false;
                }
                else
                    return true;
            }
            else if (!string.IsNullOrEmpty(exclude))
            {
                foreach (string region in exclude.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (ipLocation.Contains(region))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
                return true;
        }

        public static PurchaseResponseEntity Purchase(PurchaseRequestEntity request)
        {
            return Purchase(request, false);
        }

        public static PurchaseResponseEntity Purchase(PurchaseRequestEntity request, bool isExternal)
        {
            return Purchase(request, false, false);
        }

        /// <summary>
        /// 出单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isExternal">是否外部(第三方机构)调用</param>
        /// <returns></returns>
        public static PurchaseResponseEntity Purchase(PurchaseRequestEntity request, bool isExternal, bool isSync)
        {
            PurchaseResponseEntity response = new PurchaseResponseEntity();

            if (Common.CheckIfSystemFailed(response))
            {
                return response;
            }

            try
            {
                DateTime dtNow = Common.GetDatetime();//DateTime.Now;

                if (!isSync)
                {
                    #region 有效性验证
                    if (request.flightDate < dtNow.AddMinutes(Common.IssuingDeadline))//如果填写的起飞时间已过
                    {
                        if (request.flightDate.Date == dtNow.Date)//当日起飞，则进行时间部分的验证
                        {
                            if (request.flightDate.TimeOfDay.Ticks > 0)
                                response.Trace.ErrorMsg = "请输入准确的起飞时间！";
                            else//时间部分为零（PNR未能导入）
                                response.Trace.ErrorMsg = "请输入起飞时间！";
                            return response;
                        }
                        else//日期是昨天、昨天以前
                        {
                            response.Trace.ErrorMsg = "请输入正确的乘机日期！";
                            return response;
                        }
                    }
                    else if ((request.flightDate - dtNow) > new TimeSpan(180, 0, 0, 0, 0))
                    {
                        response.Trace.ErrorMsg = "乘机时间太过遥远（已超过180天）！";
                        return response;
                    }

                    if (string.IsNullOrEmpty(request.flightNo))
                    {
                        response.Trace.ErrorMsg = "航班号不能为空！";
                        return response;
                    }

                    if (string.IsNullOrEmpty(request.customerID))
                    {
                        response.Trace.ErrorMsg = "乘客证件号码不能为空！";
                        return response;
                    }
                    else
                    {
                        request.customerID = request.customerID.ToUpper();

                        if (request.customerIDType == IdentityType.身份证 && !Common.CheckIDCard(request.customerID))
                        {
                            response.Trace.ErrorMsg = "身份证号码填写有误,请核对！";
                            return response;
                        }
                    }

                    if (string.IsNullOrEmpty(request.customerName))
                    {
                        response.Trace.ErrorMsg = "乘客姓名不能为空！";
                        return response;
                    }
                    else
                    {
                        if (request.customerName.Contains("  "))
                        {
                            response.Trace.ErrorMsg = "客户名称不合法: 姓名不能有连续空格！";
                            return response;
                        }
                    }

                    if (!string.IsNullOrEmpty(request.customerPhone))
                    {
                        if (!Regex.IsMatch(request.customerPhone, "^1[3458][0-9]{9}$"))
                        {
                            response.Trace.ErrorMsg = "手机号码格式不正确！";
                            return response;
                        }
                    }
                    #endregion
                }

                UserLoginResponse userLogin = UserClass.AccessCheck(request.username, request.password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    if (UserClass.IsParentDisabled(request.username))
                    {
                        response.Trace.ErrorMsg = "由于您的上级账号已被冻结，所以您无法出单，请联系您的上级用户！";
                        return response;
                    }

                    if (Common.PaymOnline)
                    {
                        if (userLogin.Balance <= 0)
                        {
                            response.Trace.ErrorMsg = "账户余额不足，请充值或联系您的上级用户！";
                            return response;
                        }
                    }

                    //if (Case.IsIssued(request.flightDate, request.customerName, request.customerID))
                    //{
                    //    response.Trace.ErrorMsg = "该旅客信息已经入库，请勿重复打印！";
                    //    return response;
                    //}

                    DataSet dsProduct = Product.GetProduct(request.InsuranceCode);

                    if (dsProduct.Tables[0].Rows.Count == 0)
                    {
                        response.Trace.ErrorMsg = "找不到您所指定的产品，或者该产品已停用，请重新选择！";
                        return response;
                    }

                    DataRow drProduct = dsProduct.Tables[0].Rows[0];
                    bool isIssuingRequired = Convert.ToBoolean(drProduct["IsIssuingRequired"]);
                    bool isIssuingLazyEnabled = Convert.ToBoolean(drProduct["IsIssuingLazyEnabled"]);
                    bool isMobileNoRequired = Convert.ToBoolean(drProduct["IsMobileNoRequired"]);
                    if (isMobileNoRequired)
                    {
                        if (Regex.IsMatch(request.customerPhone, "^1[3458][0-9]{9}$"))
                        {
                            //处理销售点仅用一个手机号给所有乘客出保险的偷懒行为
                            if (Case.CountMobile(request.customerPhone, request.username) > 5)
                            {
                                response.Trace.ErrorMsg = "该手机号已重复使用多次，请如实填写以提供短信服务！";
                                return response;
                            }
                        }
                        else
                        {
                            response.Trace.ErrorMsg = "该款产品必须提供手机号,请检查是否正确！";
                            return response;
                        }
                    }
                    else if (!string.IsNullOrEmpty(request.customerPhone))
                    {
                        if (Regex.IsMatch(request.customerPhone, "^1[3458][0-9]{9}$"))
                        {
                            //处理销售点仅用一个手机号给所有乘客出保险的偷懒行为
                            if (Case.CountMobile(request.customerPhone, request.username) > 5)
                            {
                                response.Trace.ErrorMsg = "该手机号已重复使用多次，请如实填写,或者不填！";
                                return response;
                            }
                        }
                        else
                        {
                            response.Trace.ErrorMsg = "请正确填写手机号,或者不填！";
                            return response;
                        }
                    }

                    int interface_Id = isIssuingRequired ? Convert.ToInt32(drProduct["interface_Id"]) : 0;//若非对接产品,接口ID置为0,以免影响接口出单统计
                    object caseSupplier = drProduct["productSupplier"];
                    int caseDuration = Convert.ToInt32(drProduct["productDuration"]);
                    object productName = drProduct["productName"];

                    //基于产品的 IP 过滤
                    string include = drProduct["FilterInclude"].ToString().Trim();
                    string exclude = drProduct["FilterExclude"].ToString().Trim();
                    string comment = drProduct["FilterComment"].ToString();
                    string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    string ipLocation = string.Empty;
                    bool isValidIpLocation = true;
                    try
                    {
                        Ip2Location.Ip2Location ip2lo = new Ip2Location.Ip2Location();
                        ipLocation = ip2lo.GetLocation(ip);
                        if (!IsIpChecked(ipLocation, include, exclude))
                        {
                            string strLog = "ProductFilter User:{0} IP:{1} Location:{2} Include:{3} Exclude:{4}";
                            strLog = string.Format(strLog, request.username, ip, ipLocation, include, exclude);
                            Common.LogIt(strLog);
                            response.Trace.Detail = comment;
                            response.Trace.ErrorMsg = "无法出单！(e001)";// "产品区域限制，禁止出单！";
                            return response;
                        }
                    }
                    catch (Exception ee)
                    {
                        isValidIpLocation = false;
                        ipLocation = ee.Message.Substring(0, 20);
                    }

                    response.AgentName = userLogin.DisplayName;
                    //response.ValidationPhoneNumber = UserClass.GetValidationPhoneNumber(request.username);

                    #region 数据库事务
                    //2008.4.19 改用全手工方式 因为 Nbear 的事务处理似乎不能及时释放掉connection,越来越的连接…满负荷…导致客户端访问webservice连接超时
                    using (SqlConnection cnn = new SqlConnection(Common.ConnectionString))
                    {
                        cnn.Open();
                        using (SqlTransaction tran = cnn.BeginTransaction())
                        {
                            string strSql = "";

                            try
                            {
                                //有限的行级排它锁with(rowlock,xlock,readpast)，保证该行不会被其他进程读取,同时不会阻塞其他行的读取和主键更新
                                //2011.9.28 日志发现该语句依然可能导致死锁 原因：该语句除了对那一行上X锁之外，对top2、top3…（满足where条件的行）上IX锁
                                //而X和IX锁是互斥的，
                                //                                strSql = @"
                                //select top 1 * from t_serial with(rowlock,xlock,readpast)
                                //where caseOwner = '{0}' and caseSupplier = '{1}'
                                //order by caseNo asc";
                                strSql = @"
  delete top(1)
  from t_serial WITH (rowlock, READPAST)
  output deleted.caseNo, deleted.caseSupplier,
  deleted.locationInclude, deleted.locationExclude, deleted.locationComment
  where caseOwner = '{0}' and caseSupplier = '{1}'";
                                strSql = string.Format(strSql, request.username, caseSupplier);
                                DataSet dsSerial = new DataSet();
                                SqlCommand cmm = new SqlCommand("", cnn, tran);
                                cmm.CommandText = strSql;
                                SqlDataAdapter sda = new SqlDataAdapter(cmm);
                                sda.Fill(dsSerial);

                                if (dsSerial.Tables[0].Rows.Count == 0)
                                {
                                    tran.Rollback();
                                    response.Trace.ErrorMsg = "没有可用的单证号，请联系相关业务人员！";
                                    return response;
                                }

                                DataRow drSerial = dsSerial.Tables[0].Rows[0];
                                string caseNo = drSerial["caseNo"].ToString();
                                response.CaseNo = caseNo;

                                if (isValidIpLocation)//上面验证若通过则此处继续审查
                                {
                                    //基于号段的 IP 过滤
                                    include = drSerial["locationInclude"].ToString();
                                    exclude = drSerial["locationExclude"].ToString();
                                    comment = drSerial["locationComment"].ToString();
                                    if (!IsIpChecked(ipLocation, include, exclude))
                                    {
                                        tran.Rollback();
                                        string strLog = "SerialFilter User:{0} IP:{1} Location:{2} Include:{3} Exclude:{4}";
                                        strLog = string.Format(strLog, request.username, ip, ipLocation, include, exclude);
                                        Common.LogIt(strLog);
                                        response.Trace.Detail = comment;
                                        response.Trace.ErrorMsg = "无法出单！(e002)";//"号段区域限制，禁止出单！";
                                        return response;
                                    }
                                }

                                //insert 语句本身不会阻塞,但是却引起其他线程的select阻塞
                                /*用MsSql2005的 output inserted.caseID 新语法代替原来的 SELECT CAST(scope_identity() AS int) 以返回自增列ID;*/
                                strSql = @"
insert into t_Case
(caseNo,caseOwner,caseSupplier,productID,customerFlightDate,customerFlightNo,customerID,customerName,customerPhone,
    parentPath,datetime,isPrinted,enabled,caseDuration, ip, IpLocation, reserved, customerGender, customerBirth, interface_Id)
output inserted.caseID
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}', '{16}', '{17}', '{18}', '{19}');";

                                string parentPath = userLogin.ParentPath + request.username + "/";//用于判定单证的层级归属
                                string gender = request.customerGender == Gender.Female ? "女" : "男";
                                //if (interface_Id == null)//无需判断,空值、空格使用单引号括起插入到int型字段的时候，数据库会自动转成0值
                                //    interface_Id = 0;

                                strSql = string.Format(strSql, caseNo, request.username, drSerial["caseSupplier"], request.InsuranceCode.Trim(),
                                    request.flightDate, request.flightNo.Trim(), request.customerID.Trim(), request.customerName.Trim(), request.customerPhone.Trim(), parentPath, dtNow.ToString(),
                                    0, 1, caseDuration, ip, ipLocation, request.PNR, gender, request.customerBirth, interface_Id);
                                cmm.CommandText = strSql;
                                int caseId = Convert.ToInt32(cmm.ExecuteScalar());//返回自增列id
                                //cmm.ExecuteNonQuery();
                                strSql = string.Empty;//清空SQL语句,以防干扰下面的语句合并

                                if (isIssuingRequired)
                                {
                                    #region 投保数据对接
                                    string IOC_Class_Alias = drProduct["IOC_Class_Alias"].ToString();
                                    string IOC_Class_Parameters = drProduct["IOC_Class_Parameters"].ToString();
                                    IssueEntity entity = new IssueEntity();
                                    entity.Name = request.customerName;
                                    entity.ID = request.customerID;
                                    entity.IDType = request.customerIDType;
                                    entity.Gender = request.customerGender;
                                    entity.Birthday = request.customerBirth;
                                    //如果是今天之后的乘机日期,则时间部分置为0
                                    entity.EffectiveDate = request.flightDate.Date > DateTime.Today ? request.flightDate.Date : request.flightDate;
                                    entity.ExpiryDate = request.flightDate.AddDays(caseDuration - 1);
                                    entity.PhoneNumber = request.customerPhone;
                                    entity.FlightNo = request.flightNo;

                                    entity.IsLazyIssue = isIssuingLazyEnabled;
                                    entity.DbCommand = cmm;
                                    entity.IOC_Class_Alias = IOC_Class_Alias;
                                    entity.IOC_Class_Parameters = IOC_Class_Parameters;
                                    entity.CaseNo = caseNo;
                                    entity.CaseId = caseId.ToString();
                                    entity.InterfaceId = interface_Id;
                                    entity.Title = productName.ToString();

                                    TraceEntity validate = Case.Validate(entity);
                                    if (string.IsNullOrEmpty(validate.ErrorMsg))
                                    {
                                        if (entity.IsLazyIssue && !isExternal)
                                        {
                                            //Thread th = new Thread(Case.IssueAsync);
                                            //th.Start(entity);
                                            entity.ConnectionString = Common.ConnectionString;
                                            //entity.MaxRedelivery = 3;
                                            Common.AQ_Issuing.EnqueueObject(entity);
                                        }
                                        else
                                        {
                                            IssuingResultEntity result = Case.Issue(entity);
                                            if (string.IsNullOrEmpty(result.Trace.ErrorMsg))
                                            {
                                                response.SerialNo = result.PolicyNo;//暂借用该SerialNo字段
                                                response.PolicyNo = result.PolicyNo;
                                                response.Insurer = result.Insurer;
                                                response.AmountInsured = result.AmountInsured;
                                                response.ValidationWebsite = result.Website;
                                                response.ValidationPhoneNumber = result.CustomerService;
                                                //如果中途转投了别的接口
                                                if (entity.InterfaceId != interface_Id)
                                                {
                                                    strSql = "UPDATE [t_Case] SET [interface_Id] = {0} WHERE caseNo = '{1}'; ";
                                                    strSql = string.Format(strSql, entity.InterfaceId, caseNo);
                                                }
                                            }
                                            else
                                            {
                                                tran.Rollback();
                                                response.Trace = result.Trace;
                                                return response;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tran.Rollback();
                                        response.Trace = validate;
                                        return response;
                                    }

                                    #endregion
                                    if (Common.Debug)
                                    {
                                        Test.TestIt(entity);
                                    }
                                }

                                //修改出单量 主键更新 合并到下面的SQL语句一起执行
                                strSql += "update t_user set CountConsumed = CountConsumed + 1 where username = '" + request.username + "'; ";

                                #region 扣款
                                string[] parentArray = parentPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);//形如：/admin/tianzhi/bcaaa/bcaaa0150/
                                string root = parentArray[0];
                                string distributor = "";
                                if (parentArray.Length > 1)
                                {
                                    distributor = parentArray[1];//第二级用户名既是一级分销商
                                    strSql += "update t_User set balance = balance - price where username = '{0}'; update t_User set balance = balance - price where username = '{1}'";
                                    strSql = string.Format(strSql, root, distributor);
                                }
                                else//管理员自己出单
                                {
                                    strSql += "update t_User set balance = balance - price where username = '{0}'";
                                    strSql = string.Format(strSql, root);
                                }
                                cmm.CommandText = strSql;
                                cmm.ExecuteNonQuery();
                                #endregion

                                tran.Commit();//提交事务
                            }
                            catch
                            {
                                tran.Rollback();
                                string msg = "产品名称：{0}" + Environment.NewLine + "Last SQL：{1}";
                                msg = string.Format(msg, productName, strSql);
                                Common.LogIt(msg);
                                throw;
                            }
                        }
                    }
                    #endregion
                    return response;
                }
                else
                {
                    response.Trace = userLogin.Trace;
                    return response;
                }
            }
            catch (System.Exception ex)
            {
                Common.LogIt(ex.ToString());
                response.Trace.ErrorMsg = "下单未成功，请稍后重试。";
                return response;
            }
        }

        public static TraceEntity DiscardIt(string username, string password, string caseNo)
        {
            TraceEntity response = new TraceEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(username, password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    response = Case.Discard(username, caseNo);
                }
                else
                {
                    response = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                Common.LogIt(e.ToString());
                response.ErrorMsg = "未能作废，请稍后重试。";
            }

            return response;
        }

        public static int CountBalance(string username, string password)
        {
            int count = Common.DB.Select(Tables.t_Serial, Tables.t_Serial.caseNo.Count())
                                    .Where(Tables.t_Serial.caseOwner == username)
                                    .ToScalar<int>();
            return count;
        }

        public static int CountConsumed(string username, string password)
        {
            int count = Common.DB.Select(Tables.t_Case, Tables.t_Case.caseNo.Count())
                                    .Where(Tables.t_Case.caseOwner == username)
                                    .ToScalar<int>();
            return count;
        }

        /// <summary>
        /// 登录,亦为其他接口提供权限验证服务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserLoginResponse Login(string username, string password)
        {
            return UserClass.Login(username, password, System.Web.HttpContext.Current.Request.UserHostAddress);
        }

        public static TraceEntity Logout(LogoutRequestEntity request)
        {
            TraceEntity response = new TraceEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(request.Username, request.Password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    UserClass.Logout(request);
                }
                else
                {
                    response = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                response.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }

        public static ProductListResponseEntity GetProductList(string username, string password)
        {
            ProductListResponseEntity response = new ProductListResponseEntity();

            try
            {
                UserLoginResponse userLogin = UserClass.AccessCheck(username, password);

                if (string.IsNullOrEmpty(userLogin.Trace.ErrorMsg))
                {
                    response.ProductList = Product.GetProductList();
                }
                else
                {
                    response.Trace = userLogin.Trace;
                }
            }
            catch (Exception e)
            {
                response.Trace.ErrorMsg = e.Message;
                Common.LogIt(e.ToString());
            }

            return response;
        }
    }
}
