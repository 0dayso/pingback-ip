using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/*用法
        SqlCommand command = new SqlCommand(this.cmdText, new SqlConnection(connStr), null);
        //正向
        SqlCommandSerializable esCommand = new ESqlCommand(command);
        //反向
        SqlCommand command2 = esCommand.ToSqlCommand(connStr);
 * */
/// <summary>
/// 可序列化的SqlCommand。（System.Data.SqlClient.SqlCommand是不可序列化的）
/// </summary>
[Serializable]
public class SqlCommandSerializable
{
    private SqlParameterSerializable[] esParas;
    private string cmdText;
    private CommandType cmdType;

    public SqlCommandSerializable(SqlCommand command)
    {
        this.cmdText = command.CommandText;
        this.cmdType = command.CommandType;

        this.esParas = new SqlParameterSerializable[command.Parameters.Count];
        int index = 0;
        foreach (SqlParameter para in command.Parameters)
        {
            this.esParas[index] = new SqlParameterSerializable(para);
            index++;
        }
    }

    public SqlCommand ToSqlCommand(string connStr)
    {
        SqlCommand command = new SqlCommand(this.cmdText, new SqlConnection(connStr), null);

        for (int i = 0; i < this.esParas.Length; i++)
        {
            command.Parameters.Add(this.esParas[i].ToSqlParameter());
        }

        command.CommandType = this.cmdType;
        return command;
    }
}

[Serializable]
public class SqlParameterSerializable
{
    public SqlParameterSerializable(SqlParameter sPara)
    {
        this.paraName = sPara.ParameterName;
        this.paraLen = sPara.Size;
        this.paraVal = sPara.Value;
        this.sqlDbType = sPara.SqlDbType;
    }

    public SqlParameter ToSqlParameter()
    {
        SqlParameter para = new SqlParameter(this.paraName, this.sqlDbType, this.paraLen);
        para.Value = this.paraVal;

        return para;
    }

    #region ParaName
    private string paraName = "";
    public string ParaName
    {
        get
        {
            return this.paraName;
        }
        set
        {
            this.paraName = value;
        }
    }
    #endregion

    #region ParaLen
    private int paraLen = 0;
    public int ParaLen
    {
        get
        {
            return this.paraLen;
        }
        set
        {
            this.paraLen = value;
        }
    }
    #endregion

    #region ParaVal
    private object paraVal = null;
    public object ParaVal
    {
        get
        {
            return this.paraVal;
        }
        set
        {
            this.paraVal = value;
        }
    }
    #endregion

    #region SqlDbType
    private SqlDbType sqlDbType = SqlDbType.NVarChar;
    public SqlDbType SqlDbType
    {
        get
        {
            return this.sqlDbType;
        }
        set
        {
            this.sqlDbType = value;
        }
    }
    #endregion

}