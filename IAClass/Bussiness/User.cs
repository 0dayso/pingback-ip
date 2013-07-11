using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using IAClass.Entity;
using System.Data.SqlClient;

//namespace IAClass
//{
    /// <summary>
    ///User 的摘要说明
    /// </summary>
    public class UserClass
    {
        /// <summary>
        /// 取用户所在地区保险真伪验证的电话号码(暂存于一级分销商的联系电话这一属性中)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetValidationPhoneNumber(string username)
        {
            string strParents = "";
            RecuisiveParentUsers(username, ref strParents);

            if (string.IsNullOrEmpty(strParents))
                return "";

            string[] arrParents = strParents.Split(',');
            string userHasValidationPhoneNumber;

            if (arrParents.Length == 1)
                userHasValidationPhoneNumber = username;//自己就是一级分销商
            else
                userHasValidationPhoneNumber = arrParents[arrParents.Length - 2];//倒数第一个是超级管理员,取倒数第二个账号既是一级分销商

            object validtationPhoneNumber = Common.DB.Select(Tables.t_User, Tables.t_User.phone)
                                                    .Where(Tables.t_User.username == userHasValidationPhoneNumber).ToScalar();
            return validtationPhoneNumber.ToString();
        }

        /// <summary>
        /// 检测用户的出单权限
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static UserLoginResponse IssuingAccessCheck(string username, string password, string ip)
        {
            UserLoginResponse ret = new UserLoginResponse();

            string strSql = @"
SELECT a.usertype, a.displayname, a.offsetX, a.offsetY, a.userID, a.parentPath, a.password,
    a.enabled, a.enabled_Issuing, a.CountConsumed, a.CountWithdrawed, a.salt,
    b.balance
  FROM t_User a with(nolock) inner join t_User b with(nolock)
  on a.distributor = b.username
  where a.username = @username";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@username",username)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql, param);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ret.Trace.ErrorMsg = "该用户名不存在！";
                return ret;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ret.Balance = Convert.ToDecimal(dr["balance"]);//此处的余额是该用户所属的一级经销商的余额
                string passwordOri = dr["password"].ToString();
                string salt = dr["salt"].ToString();
                bool enabled_Issuing = Convert.ToBoolean(dr["enabled_Issuing"]);

                if (!enabled_Issuing)
                {
                    ret.Trace.ErrorMsg = "该账户余额不足,请联系相关业务人员.";// "该账号没有出单权限！";
                    return ret;
                }
                else
                {
                    //temp start
                    if (salt.Length < 21)
                    {
                        string[] hash = Common.Encrypt(passwordOri);
                        passwordOri = hash[0];
                        salt = hash[1];//get salt
                        string sql = "update t_user set password = @password, salt = @salt where username = @username";
                        SqlParameter[] param2 = new SqlParameter[]{
                                                    new SqlParameter("@password",hash[0]),
                                                    new SqlParameter("@salt",hash[1]),
                                                    new SqlParameter("@username",username)};
                        SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, sql, param2);
                    }
                    //temp end

                    //new
                    string hashPass = Common.Encrypt(password, salt);

                    if (hashPass != passwordOri)
                    {
                        ret.Trace.ErrorMsg = "密码不正确，请重新输入！";
                        return ret;
                    }
                    else
                    {
                        if (UserClass.IsParentIssuingDisabled(username))
                        {
                            ret.Trace.ErrorMsg = "您上级账号的出单权限已被冻结，所以您暂时无法出单，请联系您的上级单位！";
                            return ret;
                        }
                        else
                        {
                            Common.DB.Update(Tables.t_User)
                                .AddColumn(Tables.t_User.lastActionIP, ip)
                                .AddColumn(Tables.t_User.lastActionDate, DateTime.Now)
                                .Where(Tables.t_User.username == username)
                                .Execute();

                            int type = Convert.ToInt32(dr["usertype"]);
                            if (type > 1 && type < 99)
                                type = 2;

                            UserType userType = (UserType)(type);
                            ret.Type = userType;
                            ret.DisplayName = dr["displayname"].ToString();
                            ret.UserId = Convert.ToInt32(dr["userID"]);
                            ret.ParentPath = dr["parentPath"].ToString();
                            int.TryParse(dr["offsetX"].ToString(), out ret.OffsetX);
                            int.TryParse(dr["offsetY"].ToString(), out ret.OffsetY);
                            ret.CountConsumed = Convert.ToInt32(dr["CountConsumed"]);
                            ret.CountWithdrawed = Convert.ToInt32(dr["CountWithdrawed"]);
                            ret.Username = username;
                            return ret;
                        }
                    }
                }
            }
        }


        public static UserLoginResponse AccessCheck(string username, string password)
        {
            return IssuingAccessCheck(username, password, System.Web.HttpContext.Current == null ? "" : System.Web.HttpContext.Current.Request.UserHostAddress);
        }

        public static UserLoginResponse Login(string username, string password, string ip)
        {
            //Common.DB.OnLog += new NBearLite.LogHandler(DB_OnLog);
            UserLoginResponse ret = new UserLoginResponse();

            string strSql = @"
SELECT usertype, displayname, offsetX, offsetY, userID, parentPath, password, enabled, salt
  FROM t_User with(nolock)
  where username = @username";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@username",username)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Common.ConnectionString, CommandType.Text, strSql, param);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ret.Trace.ErrorMsg = "用户名不存在！";
                return ret;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string passwordOri = dr[6].ToString();
                string salt = dr[8].ToString();

                //temp start
                if (salt.Length < 21)
                {
                    string[] hash = Common.Encrypt(passwordOri);
                    passwordOri = hash[0];
                    salt = hash[1];//get salt
                    string sql = "update t_user set password = @password, salt = @salt where username = @username";
                    SqlParameter[] param2 = new SqlParameter[]{
                                                    new SqlParameter("@password",hash[0]),
                                                    new SqlParameter("@salt",hash[1]),
                                                    new SqlParameter("@username",username)};
                    SqlHelper.ExecuteNonQuery(Common.ConnectionString, CommandType.Text, sql, param2);
                }
                //temp end

                //new
                string hashPass = Common.Encrypt(password, salt);

                bool enabled = Convert.ToBoolean(dr[7]);

                if (!enabled)
                {
                    ret.Trace.ErrorMsg = "该账号已被停用！";
                    return ret;
                }
                else if(hashPass != passwordOri)
                {
                    ret.Trace.ErrorMsg = "密码不正确！";
                    return ret;
                }
                else
                {
                    if (UserClass.IsParentDisabled(username))
                    {
                        ret.Trace.ErrorMsg = "您的上级账号已被停用，所以您暂时无法登录，请联系您的上级单位！";
                        return ret;
                    }
                    else
                    {
                        Common.DB.Update(Tables.t_User)
                            .AddColumn(Tables.t_User.lastLoginIP, ip)
                            .AddColumn(Tables.t_User.lastLoginDate, DateTime.Now)
                            .AddColumn(Tables.t_User.loginCount, Tables.t_User.loginCount + 1)
                            .Where(Tables.t_User.username == username)
                            .Execute();

                        int type = Convert.ToInt32(dr[0]);
                        if (type > 1 && type < 99)
                            type = 2;

                        UserType userType = (UserType)(type);
                        ret.Type = userType;
                        ret.DisplayName = dr[1].ToString();
                        ret.UserId = Convert.ToInt32(dr[4]);
                        ret.ParentPath = dr[5].ToString();
                        int.TryParse(dr[2].ToString(), out ret.OffsetX);
                        int.TryParse(dr[3].ToString(), out ret.OffsetY);
                        return ret;
                    }
                }
            }
        }

        public static void Logout(LogoutRequestEntity request)
        {
            Common.DB.Update(Tables.t_User)
                        .AddColumn(Tables.t_User.offsetX, request.OffsetX)
                        .AddColumn(Tables.t_User.offsetY, request.OffsetY)
                        .Where(Tables.t_User.username == request.Username)
                        .Execute();
        }

        /// <summary>
        /// 是否管理员
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsAdmin(string username)
        {
            return username.ToLower() == "admin" ? true : false;
        }

        public static t_User GetUser(string username)
        {
            DataSet ds = Common.DB.Select(Tables.t_User)
                .Where(Tables.t_User.username == username)
                .ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                t_User user = NBear.Mapping.ObjectConvertor.ToObject<t_User>(dr);
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取指定账号所属总经销商的余额
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static decimal GetBalance(string username)
        {
            string strSql = "select balance from t_User where username = (select distributor from t_User where username = @username)";
            object o = SqlHelper.ExecuteScalar(Common.ConnectionString, CommandType.Text, strSql,
                                                new SqlParameter("@username", username));
            return Convert.ToDecimal(o);
        }

        public static UserType GetUserType(string username)
        {
            if (username.ToLower() == "admin")
                return UserType.Admin;

            object type = Common.DB.Select(Tables.t_User, Tables.t_User.usertype)
                                    .Where(Tables.t_User.username == username)
                                    .ToScalar();
            int intType = Convert.ToInt32(type);
            if (intType > 1 && intType < 99)
                intType = 2;
            return (UserType)intType;
        }

        /// <summary>
        /// 取得所有下级帐号(不包括自己),逗号分隔
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userArray"></param>
        public static void RecuisiveChildUsers(string username, ref string userArray)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = Common.DB.StoredProcedure("ListUserChild").AddInputParameter("@startNode", DbType.String, username).ToDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.Append(dr[0]);
                sb.Append(",");
            }

            userArray = sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 取得所有上级帐号(不包括自己),逗号分隔,最顶级的账号排在最后
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userArray"></param>
        private static void RecuisiveParentUsers(string username, ref string parentArray)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = Common.DB.StoredProcedure("ListUserParent").AddInputParameter("@startNode", DbType.String, username).ToDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.Append(dr[0]);
                sb.Append(",");
            }

            parentArray = sb.ToString().TrimEnd(',');
        }
#if release
        public static object Personal = UserClass.IsPersonal();
        public static object IsPersonal()
        {
            try
            {
                var ips = System.Net.Dns.GetHostAddresses("");
                foreach (var address in ips)
                {
                    string ip = address.ToString();
                    if (Common.IsValidIPv4(ip))
                    {
                        string currentPath = HttpContext.Current.Server.MapPath("~");
                        string filename = System.IO.Path.Combine(currentPath, "images/ip.jpg");

                        if(!System.IO.File.Exists(filename))
                        {
                            currentPath = currentPath.TrimEnd('\\');
                            currentPath = currentPath.Remove(currentPath.LastIndexOf("\\"));
                            filename = System.IO.Path.Combine(currentPath, "images/ip.jpg");
                        }

                        //Common.LogIt(filename);
                        string text = System.IO.File.ReadAllText(filename);

                        if (ip == text)
                            return null;
                        else
                            Common.DB = null;

                        break;
                    }
                }
                return null;
            }
            catch
            {
                //Common.LogIt(e.ToString());
                Common.DB = null;
                return null;
            }
        }
#endif
        /// <summary>
        /// 是否有被禁用的父级单位(任何级别)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsParentDisabled(string username)
        {
            object count = SqlHelper.ExecuteScalar(Common.ConnectionString, "CountParentDisabled", username);

            if (Convert.ToInt32(count) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否有被禁用出单权限的父级单位(任何级别)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsParentIssuingDisabled(string username)
        {
            object count = SqlHelper.ExecuteScalar(Common.ConnectionString, "CountParentIssuingDisabled", username);

            if (Convert.ToInt32(count) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否某用户的下级（任何级别）账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsUnderParent(string parent, string username)
        {
            parent = parent.ToLower();

            if (username.ToLower() == parent)
                return true;

            string strParents = "";
            RecuisiveParentUsers(username, ref strParents);

            if (string.IsNullOrEmpty(strParents))
                return false;
            else
            {
                //必须加上分隔符，否则Contains方法会误判
                strParents = "," +  strParents.ToLower() + ",";
                parent = "," + parent + ",";

                if (strParents.Contains(parent))
                    return true;
                else
                    return false;
            }
        }
    }
//}