<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        TABLE
        {
            font-family: "宋体";
            font-size: 9pt;
            line-height: 12pt;
        }
        td
        {
            font-size: 12px;
            color: #000000;
            text-decoration: none;
        }
        
        .button
        {
            border: 1px solid #034DA2;
            padding: 1px;
            margin: 1px;
            background-color: #DDECFF;
        }
        a
        {
            font-size: 12px;
            color: #000000;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="3">
        <tr>
            <td align="right" style="width: 70px; height: 21px;">
                &nbsp;
            </td>
            <td align="left" style="height: 21px">
                &nbsp;
            </td>
            <td align="left" style="height: 21px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 70px; height: 21px;">
                <img alt="" src="../images/print.gif" />
            </td>
            <td align="left" style="height: 21px">
                <a href="../UpdateFiles/setupIA.exe">下载单证打印客户端</a>
            </td>
            <td align="left" style="height: 21px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 70px; height: 21px;">
                <img alt="" src="../images/info.gif" />
            </td>
            <td align="left" style="height: 21px">
                <a href="../UpdateFiles/dotnetfx2.exe">下载插件</a>（如果无法安装打印客户端，请先下载并安装该插件）
            </td>
            <td align="left" style="height: 21px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 70px; height: 21px;">
                <img alt="" src="../images/check.gif" />
            </td>
            <td align="left" style="height: 21px">
                <a href="../UpdateFiles/任我行出单系统说明书.pdf">使用说明书</a>
            </td>
            <td align="left" style="height: 21px">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
