using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsAVS51173
/// </summary>
public class ClsAVS51173
{
    DbConnection conn = new DbConnection();
    SqlCommand cmd;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    string sql = "";

    public ClsAVS51173()
    {

    }
    public string VendorMaster(string FLAG, string VENDORID = null, string VENDERNAME = null, string CNTCTPRSNNAME = null, string MOBNO = null, string EMAILID = null, string ADDRESS1 = null, string ADDRESS2 = null, string STATE = null, string CITY = null, string PINCODE = null, string ENTRYDATE = null, string MID = null, string GSTNO=null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_VENDORMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);	

            cmd.Parameters.AddWithValue("@VENDERNAME", VENDERNAME);
            cmd.Parameters.AddWithValue("@CNTCTPRSNNAME", CNTCTPRSNNAME);
            cmd.Parameters.AddWithValue("@MOBNO", MOBNO);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@STATE", Convert.ToInt32(STATE));
            cmd.Parameters.AddWithValue("@CITY", CITY);
            cmd.Parameters.AddWithValue("@PINCODE", PINCODE);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }


    public string ProductMaster(string FLAG, string PRODID = null, string VENDORID = null, string PRODNAME = null, string RATE = null, string SGST = null, string CGST = null, string SGSTPRD = null, string CGSTPRD = null, string MID = null, string ENTRYDATE = null)
    {
        string res = "";
     
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_ProductMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@PRODNAME", PRODNAME);
            cmd.Parameters.AddWithValue("@RATE", RATE);
            cmd.Parameters.AddWithValue("@SGST", (SGST == "" ? "0" : SGST));
                
            cmd.Parameters.AddWithValue("@CGST", (CGST == "" ? "0" : CGST));

            cmd.Parameters.AddWithValue("@SGSTPRD", (SGSTPRD == "" ? "0" : SGSTPRD));
            cmd.Parameters.AddWithValue("@CGSTPRD", (CGSTPRD == "" ? "0" : CGSTPRD));
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }
    public string PurchaseMaster(string FLAG, string PONO = null,string BRCD=null, string PRODID = null, string VENDORID = null,string SRNO=null, string QTY = null, string UNITCOST = null, string SGST = null,   string CGSTPRD = null, string MID = null,string AMOUNT=null, string ENTRYDATE = null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_PURCHASEMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@PONO", PONO);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);
            cmd.Parameters.AddWithValue("@SRNO", SRNO);
            cmd.Parameters.AddWithValue("@QTY", QTY);
            cmd.Parameters.AddWithValue("@UNITCOST", UNITCOST);
            cmd.Parameters.AddWithValue("@SGSTPER", SGST);
           // cmd.Parameters.AddWithValue("@CGSTPER", CGST);
          //  cmd.Parameters.AddWithValue("@SGSTAMT", SGSTPRD);
            cmd.Parameters.AddWithValue("@CGSTAMT", CGSTPRD);
            cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }


    public string OpeningStock(string FLAG,string BRCD=null, string PRODID = null, string VENDORID = null, string QTY = null, string UNITCOST = null, string SGST = null, string CGST = null, string SGSTAMT = null, string CGSTAMT = null, string AMOUNT = null, string MID = null, string ENTRYDATE = null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "sp_OpeningStock";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@QTY", QTY);
            cmd.Parameters.AddWithValue("@UNITCOST", UNITCOST);
            cmd.Parameters.AddWithValue("@SGSTPER", SGST);
            cmd.Parameters.AddWithValue("@CGSTPER", CGST);
            //cmd.Parameters.AddWithValue("@SGSTAMT", SGST);
            //cmd.Parameters.AddWithValue("@CGSTAMT", CGST); 
         //   cmd.Parameters.AddWithValue("@SGSTAMT", SGSTAMT);
            //cmd.Parameters.AddWithValue("@SGSTPER", SGSTAMT);
            //cmd.Parameters.AddWithValue("@CGSTPER", CGSTAMT);
            cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }


    public DataTable VieWVendor(string FLAG, string VENDORID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_VENDORMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

        return conn.GetData(cmd);



    }
   

    public DataTable VieWVendorRpt(string FLAG, string VENDORID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_RPTVENDORMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

        return conn.GetData(cmd);



    }

    public DataTable VieWPURCHASE(string FLAG, string PONO = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_PURCHASEMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@PONO", PONO);

        return conn.GetData(cmd);



    }
    public DataTable VieWPurchaseId(string FLAG, string PRODUCTID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_DEEDSTOCK_WEBSERVICE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@VALUE1", PRODUCTID);

        return conn.GetData(cmd);



    }

    public DataTable ProductCalculate(string FLAG, string PRODUCTID = null,string VendorID=null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_DEEDSTOCK_WEBSERVICE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@VALUE1", PRODUCTID);
        cmd.Parameters.AddWithValue("@VALUE2", VendorID);

        return conn.GetData(cmd);



    }

     public string DeadStock(string FLAG, string VALUE1 = null,string VALUE2=null, string VALUE3 = null,string VALUE4=null, string VALUE5 = null)
    {
          string res = "";

        try
        {
            cmd = new SqlCommand();
        cmd.CommandText = "SP_DEEDSTOCK_WEBSERVICE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@VALUE1", VALUE1);
        cmd.Parameters.AddWithValue("@VALUE2", VALUE2);
        cmd.Parameters.AddWithValue("@VALUE3", VALUE3);
        cmd.Parameters.AddWithValue("@VALUE4", VALUE4);
        cmd.Parameters.AddWithValue("@VALUE5", VALUE5);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
         return res;
    }

    public string GetProductCancle(string FLAG, string PRODID = null, string VENDORID = null)
    {
        string res = "";
        try
        {

            cmd = new SqlCommand();
            cmd.CommandText = "SP_ProductMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);

            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID); 

           


            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;




    }


    public string VieWVendorM(string FLAG, string VENDORID = null)
    {
        string res = "";
        try
        {

            cmd = new SqlCommand();
            cmd.CommandText = "SP_VENDORMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);

            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;




    }

    //public DataTable VieWVendor1(string FLAG, string MID = null)
    //{

    //    cmd = new SqlCommand();
    //    cmd.CommandText = "SP_VENDORMASTER";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@FLAG", FLAG);

    //    cmd.Parameters.AddWithValue("@MID", MID);

    //    return conn.GetData(cmd);



    //}

    public void GridViewBind(string FLAG, string MID, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_VENDORMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
          //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@MID", MID);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindRptVendor(string FLAG, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_RPTVENDORMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
           // cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
          // cmd.Parameters.AddWithValue("@MID", MID);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindForPurchase(string FLAG, string BRCD, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_PURCHASEMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void GridViewProduct(string FLAG, string MID, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_ProductMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@MID", MID);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void GridRPtProduct(string FLAG,  GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_RPTPRODUCTMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
         

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void GridViewStock(string FLAG, string BRCD, GridView GrdAcc, string VENDORID = null, string PRODID = null)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "sp_OpeningStock";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable VieWProduct(string FLAG, string PRODID = null,string VENDORID=null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_ProductMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@PRODID", PRODID);
        cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

        return conn.GetData(cmd);



    }
    public DataTable VieWStockPRID(string FLAG, string VENDORID = null ,string PRODID = null, string BRCD = null)//,
    {

        cmd = new SqlCommand();
        cmd.CommandText = "sp_OpeningStock";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

      cmd.Parameters.AddWithValue("@PRODID", PRODID);
        cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
        cmd.Parameters.AddWithValue("@BRCD", BRCD);

        return conn.GetData(cmd);



    }
    public DataTable VieWStock(string FLAG, string BRCD = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "sp_OpeningStock";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@BRCD", BRCD);

        return conn.GetData(cmd);



    }
    public DataTable Calculate(string FLAG, string PRODID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "Sp_DeedStock_webservice";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@PRODID", PRODID);

        return conn.GetData(cmd);



    }

    public string GetVenderID(string VENDORID, string FLAG)
    {
        string res = "";
        try
        {

            cmd = new SqlCommand();
            cmd.CommandText = "SP_DEEDSTOCK_WEBSERVICE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);

            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string GetName(string VENDORID, string MID)
    {
        sql = "SELECT VENDERNAME+'_'+CONVERT(VARCHAR(10),VENDORID) FROM VendorMaster WHERE VENDORID='" + VENDORID + "'  AND MID='" + MID + "'  and stage<>1004";
        VENDORID = conn.sExecuteScalar(sql);
        return VENDORID;
    }


    public string GetProductName(string PRODID, string MID)
    {
        sql = "SELECT PRODNAME+'_'+CONVERT(VARCHAR(10),PRODID) FROM PRODUCTMASTER WHERE PRODID='" + PRODID + "'  AND MID='" + MID + "'  and stage<>1004";
        PRODID = conn.sExecuteScalar(sql);
        return PRODID;
    }

    public string GetProductID(string PRODID, string MID)
    {
        sql = "SELECT PRODNAME FROM PRODUCTMASTER WHERE PRODID='" + PRODID + "'  AND MID='" + MID + "'  and stage<>1004";
        PRODID = conn.sExecuteScalar(sql);
        return PRODID;
    }

    public string GetVendorID(string VENDORID, string MID)
    {
        sql = "SELECT VENDERNAME FROM VendorMaster WHERE VENDORID='" + VENDORID + "'  and stage<>1004";// AND MID='" + MID + "' 
        VENDORID = conn.sExecuteScalar(sql);
        return VENDORID;
    }


    public string GetProductName1(string PRODID, string VENDORID)
    {
        string res = "";
        try
        {

            cmd = new SqlCommand();
            cmd.CommandText = "SP_DEEDSTOCK_WEBSERVICE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", PRODID);

            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);

            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable rptStock(string FLAG, string VENDORID = null, string PRODID = null, string BRCD = null, string ENTRYDATE = null)
    {
        

        cmd = new SqlCommand();
        cmd.CommandText = "SP_STOCKCALCASON";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);
        cmd.Parameters.AddWithValue("@VENDORID", (VENDORID == "" ? null : VENDORID));
        cmd.Parameters.AddWithValue("@PRODID", (PRODID == "" ? null : PRODID));
        cmd.Parameters.AddWithValue("@BRCD", (BRCD == "" ? null : BRCD));
        cmd.Parameters.AddWithValue("@ENTRYDATE", (ENTRYDATE == "" ? null : conn.ConvertDate(ENTRYDATE)));
        return conn.GetData(cmd);
    }
    public DataTable rptdeedStock(string FLAG, string VENDORID = null, string PRODID = null, string BRCD = null, string ENTRYDATE = null)
    {
        

        cmd = new SqlCommand();
        cmd.CommandText = "SP_STOCKCALCASONRPT";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);
        cmd.Parameters.AddWithValue("@VENDORID", (VENDORID == "" ? null : VENDORID));
        cmd.Parameters.AddWithValue("@PRODID", (PRODID == "" ? null : PRODID));
        cmd.Parameters.AddWithValue("@BRCD", (BRCD == "" ? null : BRCD));
        cmd.Parameters.AddWithValue("@ENTRYDATE", (ENTRYDATE == "" ? null : conn.ConvertDate(ENTRYDATE)));
        return conn.GetData(cmd);
    }

   
    public DataTable rptClosingStock(string FLAG, string VENDORID = null, string PRODID = null, string BRCD = null, string ENTRYDATE = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_RPTSTOCK";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);
        cmd.Parameters.AddWithValue("@VENDORID", (VENDORID == "" ? null : VENDORID));
        cmd.Parameters.AddWithValue("@PRODID", (PRODID == "" ? null : PRODID));
        cmd.Parameters.AddWithValue("@BRCD", (BRCD == "" ? null : BRCD));
        cmd.Parameters.AddWithValue("@ENTRYDATE", (ENTRYDATE == "" ? null : conn.ConvertDate(ENTRYDATE)));
        return conn.GetData(cmd);
    }

    public DataTable rptVendorMaster(string FLAG, string VENDORID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_RPTVENDORMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);
        cmd.Parameters.AddWithValue("@VENDORID", (VENDORID == "" ? null : VENDORID));
        return conn.GetData(cmd);
    }

    public DataTable rptProductMaster(string FLAG, string VENDORID = null, string PRODID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_RPTPRODUCTMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);
        cmd.Parameters.AddWithValue("@VENDORID", (VENDORID == "" ? null : VENDORID));
        cmd.Parameters.AddWithValue("@PRODID", (PRODID == "" ? null : PRODID));
        return conn.GetData(cmd);
    }

    public DataTable StockCalculate(string BRCD = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_STOCKCALC";
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@BRCD", (BRCD == "" ? null : BRCD));
        return conn.GetData(cmd);
    }

    public string getValue(string str)
    {
        str = str.ToString().Trim();
        if (str == "")
        {
            return null;
        }
        return str;
    }
}