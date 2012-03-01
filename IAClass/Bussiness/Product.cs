using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Mapping;
using IAClass.Entity;
using NBearLite;

//namespace IAClass.Bussiness
//{
    public class Product
    {
        public static DataSet GetProduct(string productId)
        {
            DataSet ds = Common.DB.Select(Tables.t_Product, QueryColumn.All(Tables.t_Product), Tables.t_Interface.IOC_TypeName)
                                    .LeftJoin(Tables.t_Interface, Tables.t_Product.Interface_Id == Tables.t_Interface.Id)
                                    .Where(Tables.t_Product.productID == productId)
                                    .Where(Tables.t_Product.Enabled == true)
                                    .ToDataSet();
            return ds;
        }

        public static List<t_Product> GetProductList()
        {
            List<t_Product> productList = new List<t_Product>();

            DataSet dsProduct = Common.DB.Select(Tables.t_Product)
                                            .Join(Tables.t_User, Tables.t_User.username == Tables.t_Product.productSupplier)
                                            .Where(Tables.t_Product.Enabled == true)
                                            .Where(Tables.t_User.enabled == true)//若保险公司账号被禁用，则产品亦不可用
                                            .ToDataSet();

            foreach (DataRow dr in dsProduct.Tables[0].Rows)
            {
                t_Product product = NBear.Mapping.ObjectConvertor.ToObject<t_Product>(dr);
                productList.Add(product);
            }

            return productList;
        }

        public static List<t_Product> GetProductList(string insurer)
        {
            List<t_Product> productList = new List<t_Product>();

            DataSet dsProduct = Common.DB.Select(Tables.t_Product)
                                            .Where(Tables.t_Product.productSupplier == insurer)
                                            .Where(Tables.t_Product.Enabled == true)
                                            .ToDataSet();

            foreach (DataRow dr in dsProduct.Tables[0].Rows)
            {
                t_Product product = NBear.Mapping.ObjectConvertor.ToObject<t_Product>(dr);
                productList.Add(product);
            }

            return productList;
        }
    }
//}
