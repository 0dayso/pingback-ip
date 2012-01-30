using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace EagleForms.General
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            //  初始化 AboutBox 以显示程序集信息中包含的产品信息。
            //  也可以通过以下方法更改应用程序的程序集信息设置:
            //  - 项目->属性->应用程序->程序集信息
            //  - AssemblyInfo.cs
            this.Text = String.Format("关于 {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("版本 2.0.20090113Alpha");
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = "武汉易格网科技有限公司";//AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            this.textBoxDescription.Text = "版权声明-软件许可协议：\r\n" +
                "本协议是您(个人或单一实体)与武汉易格网公司之间关于《易格航空订票系统》系列软件产品的法律协议，请认真阅读。\r\n\r\n" +
                "《易格航空订票系统》系列软件包括计算机软件，并可能包括与之相关的媒体和任何的印刷材料，以及电子文档(下称“软件产品”或“软件”)。一旦安装、复制或以其他方式使用本软件产品，即表示同意接受协议各项条件的约束。如果您不同意协议的条件，则不能获得使用本软件产品的权力。\r\n\r\n" +
                "1.本软件产品受中华人民共和国版权法及国际版权条约和其他知识产权法及条约的保护。用户获得的只是本软件产品的使用权。\r\n\r\n" +
                "2.本软件产品的版权归武汉易格网科技有限公司所有，受到适用版权法及其他知识产权法及条约的保护。\r\n\r\n" +
                "3.您不得:\r\n" +
 "    删除本软件及其他副本上一切关于版权的信息；\r\n" +
 "    销售、出租此软件产品的任何部分；\r\n" +
 "    制作和提供该软件的注册机及破解程序；\r\n" +
 "    对本软件进行反向工程，如反汇编、反编译等。\r\n\r\n" +
 "4.如果您未遵守本协议的任一条款，易格网公司有权立即终止本协议，且您必须立即终止使用本软件并销毁本软件产品的所有副本。\r\n\r\n" +
 "5.使用本软件可能存在一定风险，请用户经陪训后，严格按照所在代理商的要求规范操作！\r\n\r\n" +
 "6.若您对本协议内容有任何疑问，请与武汉易格网公司联系:027-85777575";



            //LogoPicture.pictures pic = new LogoPicture.pictures();
            //switch (GlobalVar.serverAddr)
            //{
            //    case GlobalVar.ServerAddr.Eagle:
            //        this.logoPictureBox.Image = pic.pictureBox2.Image;
            //        break;
            //    case GlobalVar.ServerAddr .ZhenZhouJiChang:
            //        this.labelProductName.Text = "郑州机场航空售票系统";
            //        this.labelCopyright.Text = "本系统仅限河南省内使用";
            //        this.labelCompanyName.Text = "新郑机场";
            //        this.logoPictureBox.Image = pic.pictureBox9.Image;
            //        this.textBoxDescription.Text = "";
            //        this.Text = "关于 " + GlobalVar.exeTitle;
            //        break;
            //    case GlobalVar.ServerAddr.KunMing:
            //        this.labelProductName.Text = "龙生航空订票系统";
            //        this.labelCopyright.Text = "";
            //        this.labelCompanyName.Text = "龙生航空服务有限公司";
            //        //this.logoPictureBox.Image = pic.pictureBox9.Image;
            //        this.textBoxDescription.Text = "";
            //        this.Text = "关于 " + GlobalVar.exeTitle;
            //        break;
            //}
            
        }

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                // 获取此程序集上的所有 Title 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 如果至少有一个 Title 属性
                if (attributes.Length > 0)
                {
                    // 请选择第一个属性
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 如果该属性为非空字符串，则将其返回
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // 如果没有 Title 属性，或者 Title 属性为一个空字符串，则返回 .exe 的名称
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // 获取此程序集的所有 Description 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 如果 Description 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Description 属性，则返回该属性的值
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // 获取此程序集上的所有 Product 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 如果 Product 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Product 属性，则返回该属性的值
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // 获取此程序集上的所有 Copyright 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 如果 Copyright 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Copyright 属性，则返回该属性的值
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // 获取此程序集上的所有 Company 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 如果 Company 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Company 属性，则返回该属性的值
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion




    }
}
