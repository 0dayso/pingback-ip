using System;
using System.Collections.Generic;
using System.Text;
using gs.para;
using System.IO;
using System.Xml;

namespace ePlus.Model
{

    class md
    {
        static public bool b_001 = false;//001为 航意险打印--PICC
        static public bool b_002 = false;//002为 机票打印
        static public bool b_003 = false;//003为 电子行程单打印
        static public bool b_004 = false;//004为 终端
        static public bool b_005 = false;//005为保险单打印
        static public bool b_006 = false;//006仅为简版用户
        static public bool b_016 = false;//016简版用户禁止订票
        static public bool b_026 = false;//026简版用户显示政策
        static public bool b_007 = false;//007为永安交意险打印

        static public bool b_008 = false;//008为散客拼团A
        static public bool b_018 = false;//018为散客拼团B
        static public bool b_028 = false;//028为散客拼团c
        static public bool b_038 = false;//038为散客拼团D
        static public bool b_048 = false;//048为散客拼团E

        static public bool b_009 = false;//009为新华保险打印
        static public bool b_00A = false;//00A为行程单脱机打印
        static public bool b_00B = false;//00B为滚动条公告发布

        static public bool b_B01 = false;//B01为保险：华安－交通意外伤害保险单
        static public bool b_B02 = false;//B02为保险：人寿－航空旅客人身意外伤害保险单
        static public bool b_B03 = false;//B03为保险：都邦航意险
        static public bool b_B04 = false;//B04为保险：都帮出行无忧
        static public bool b_B05 = false;//B05为保险：都帮出行乐

        static public bool b_B06 = false;//B06为周游列国会员保险卡
        static public bool b_B07 = false;//B07为Sunshine阳光财险
        static public bool b_B08 = false;//B08为航翼网保险卡
        static public bool b_B09 = false;//B09为易格网保险卡
        static public bool b_B0A = false;//B0A为太平洋航意险
        static public bool b_B0B = false;//B0A为安帮商行通
        static public bool b_B0C = false;//BOC为人寿航空意外险2

        static public bool b_B0D = false;//BOD为易格保险之PICC

        static public bool b_B10 = false;//B10恒臻(保航班)
        static public bool b_B11 = false;//B11恒臻(7天)


        static public bool b_QQQ = false;//QQQ为自动清Q

        static public bool b_0FN = false;//关闭FN项
        static public bool b_0FM = false;//关闭行程单款项

        static public bool b_F12 = false;//直接发送方式

        static public bool b_00C = false;//是否不能取消他人PNR，为true则不能取消，为false则能取消
        static public bool b_00D = false;//独占配置

        static public bool b_00E = false;//IBE，黑屏是否默认为配置(非IBE) 
        static public bool b_00F = false;//IBE，简版(中文专业版界面)是否默认为配置(非IBE) 
        static public bool b_00G = false;//IBE，其它(如打印)是否默认配置(非IBE) 

        static public bool b_00H = false;//隐藏后台管理按钮
        static public bool b_00I = false;//显示细配置

        static public bool b_0CTI = false;//呼叫中心功能

        static public bool b_AutoLockConfig = true;//按键自动独占配置

        static public void SetBoolVars()
        {
            if (GlobalVar2.bTempus)
            {
                b_B09 = true;
                return;
            }
            b_001 = (EagleAPI.GetCmdNameEntire("001", GlobalVar.loginLC.VisuableCommand) != "");
            b_002 = (EagleAPI.GetCmdNameEntire("002", GlobalVar.loginLC.VisuableCommand) != "");
            b_003 = (EagleAPI.GetCmdNameEntire("003", GlobalVar.loginLC.VisuableCommand) != "");
            b_004 = (EagleAPI.GetCmdNameEntire("004", GlobalVar.loginLC.VisuableCommand) != "");
            b_005 = (EagleAPI.GetCmdNameEntire("005", GlobalVar.loginLC.VisuableCommand) != "");
            b_006 = (EagleAPI.GetCmdNameEntire("006", GlobalVar.loginLC.VisuableCommand) != "");
            b_016 = (EagleAPI.GetCmdNameEntire("016", GlobalVar.loginLC.VisuableCommand) != "");
            b_026 = (EagleAPI.GetCmdNameEntire("026", GlobalVar.loginLC.VisuableCommand) != "");


            b_007 = (EagleAPI.GetCmdNameEntire("007", GlobalVar.loginLC.VisuableCommand) != "");

            b_008 = (EagleAPI.GetCmdNameEntire("008", GlobalVar.loginLC.VisuableCommand) != "");
            b_018 = (EagleAPI.GetCmdNameEntire("018", GlobalVar.loginLC.VisuableCommand) != "");
            b_028 = (EagleAPI.GetCmdNameEntire("028", GlobalVar.loginLC.VisuableCommand) != "");
            b_038 = (EagleAPI.GetCmdNameEntire("038", GlobalVar.loginLC.VisuableCommand) != "");
            b_048 = (EagleAPI.GetCmdNameEntire("048", GlobalVar.loginLC.VisuableCommand) != "");

            b_009 = (EagleAPI.GetCmdNameEntire("009", GlobalVar.loginLC.VisuableCommand) != "");
            b_00A = (EagleAPI.GetCmdNameEntire("00A", GlobalVar.loginLC.VisuableCommand) != "");
            b_00B = (EagleAPI.GetCmdNameEntire("00B", GlobalVar.loginLC.VisuableCommand) != "");
            b_B01 = (EagleAPI.GetCmdNameEntire("B01", GlobalVar.loginLC.VisuableCommand) != "");
            b_B02 = (EagleAPI.GetCmdNameEntire("B02", GlobalVar.loginLC.VisuableCommand) != "");
            b_B03 = (EagleAPI.GetCmdNameEntire("B03", GlobalVar.loginLC.VisuableCommand) != "");
            b_B04 = (EagleAPI.GetCmdNameEntire("B04", GlobalVar.loginLC.VisuableCommand) != "");
            b_B05 = (EagleAPI.GetCmdNameEntire("B05", GlobalVar.loginLC.VisuableCommand) != "");
            b_B06 = (EagleAPI.GetCmdNameEntire("B06", GlobalVar.loginLC.VisuableCommand) != "");
            b_B07 = (EagleAPI.GetCmdNameEntire("B07", GlobalVar.loginLC.VisuableCommand) != "");
            b_B08 = (EagleAPI.GetCmdNameEntire("B08", GlobalVar.loginLC.VisuableCommand) != "");
            b_B09 = (EagleAPI.GetCmdNameEntire("B09", GlobalVar.loginLC.VisuableCommand) != "");
            b_B0A = (EagleAPI.GetCmdNameEntire("B0A", GlobalVar.loginLC.VisuableCommand) != "");
            b_B0B = (EagleAPI.GetCmdNameEntire("B0B", GlobalVar.loginLC.VisuableCommand) != "");
            b_B0C = (EagleAPI.GetCmdNameEntire("B0C", GlobalVar.loginLC.VisuableCommand) != "");
            b_B0D = true;// (EagleAPI.GetCmdNameEntire("B0D", GlobalVar.loginLC.VisuableCommand) != "");
            b_B10 = (EagleAPI.GetCmdNameEntire("B10", GlobalVar.loginLC.VisuableCommand) != "");
            b_B11 = (EagleAPI.GetCmdNameEntire("B11", GlobalVar.loginLC.VisuableCommand) != "");


            b_QQQ = (EagleAPI.GetCmdNameEntire("QQQ", GlobalVar.loginLC.VisuableCommand) != "");
            b_0FN = (EagleAPI.GetCmdNameEntire("0FN", GlobalVar.loginLC.VisuableCommand) != "");
            b_0FM = (EagleAPI.GetCmdNameEntire("0FM", GlobalVar.loginLC.VisuableCommand) != "");
            b_F12 = (EagleAPI.GetCmdNameEntire("F12", GlobalVar.loginLC.VisuableCommand) != "");

            b_00C = (EagleAPI.GetCmdNameEntire("00C", GlobalVar.loginLC.VisuableCommand) != "");
            b_00D = (EagleAPI.GetCmdNameEntire("00D", GlobalVar.loginLC.VisuableCommand) != "");

            b_00E = true;//(EagleAPI.GetCmdNameEntire("00E", GlobalVar.loginLC.VisuableCommand) != "");
            b_00F = (EagleAPI.GetCmdNameEntire("00F", GlobalVar.loginLC.VisuableCommand) != "");
            b_00G = (EagleAPI.GetCmdNameEntire("00G", GlobalVar.loginLC.VisuableCommand) != "");

            b_00H = (EagleAPI.GetCmdNameEntire("00H", GlobalVar.loginLC.VisuableCommand) != "");
            b_00I = (EagleAPI.GetCmdNameEntire("00I", GlobalVar.loginLC.VisuableCommand) != "");
            b_0CTI = (EagleAPI.GetCmdNameEntire("0CTI", GlobalVar.loginLC.VisuableCommand) != "");

            b_026 = (Model.md.b_008 || Model.md.b_018 || Model.md.b_028 || Model.md.b_038 || Model.md.b_048);
        }
    }    
}
