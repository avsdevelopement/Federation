using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

   
public class ClsInwordOutwordCharges
{
    int rtn;
    string sql;
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    int result = 0;





    public DataTable GetIntrestMaster(GridView GView,string BRCD)
    {
        try
        {
            sql = "select * from AVS5018";
            conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }


    public int EntryInterest(string brcd, string Acct_Type, string Retrun_Type, string Effective_Date, string Skip_charges, string Frequency, string Allow, string Flat_rate, string Interset_rate, string Pl_CRAccid, string min_charges, string max_charges, string GST_SUBGL, string GST_InterestRate, string GST_Amount, string Reason, string Reason_description, string Flat_Charges, string Percent_Charge, string Last_ApplDate, string Particular)
    {
        try
        {
            sql = "EXEC Sp_AVS5018  @FLAG='INSERT', @BRCD= '" + brcd + "', @Acct_Type= '" + Acct_Type + "', @Retrun_Type= '" + Retrun_Type + "', @Effective_Date= '" + conn.ConvertDate(Effective_Date).ToString() + "', @Skip_charges= '" + Skip_charges + "', @Frequency= '" + Frequency + "', @Allow= '" + Allow + "', @Flat_rate= '" + Flat_rate + "', @Interset_rate= '" + Interset_rate + "', @PLACCNO= '" + Pl_CRAccid + "', @min_charges= '" + min_charges + "', @max_charges= '" +  max_charges + "', @GST_SUBGL= '" + GST_SUBGL + "', @GST_InterestRate= '" + GST_InterestRate + "', @GST_Amount= '" +  GST_Amount + "', @Reason= '" + Reason + "', @Reason_description= '" + Reason_description + "', @Flat_Charges= '" + Flat_Charges + "', @Percent_Charge= '" + Percent_Charge + "', @Last_ApplDate= '" + conn.ConvertDate(Last_ApplDate).ToString() + "', @Particular= '" + Particular + "'";
            result = conn.sExecuteQuery(sql);
                                                                                          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtn;
    }


    public int EntryAuthorize(string id, string BRCD, string MID)
    {
        try
        {


            sql = "EXEC Sp_AVS5018  @FLAG='Authorize', @ID= '" +id + "',  @BRCD= '" + BRCD + "', @VID= '" + MID + "'";
            result = conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtn;
    }


    public int DeleteIntMast(string BRCD, string ID)
    {
        try
        {
            

            sql = "DELETE from AVS5018  WHERE  ID = '" + ID + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }




    public int ModifyIntrestMaster(string id,string brcd, string Acct_Type, string Retrun_Type, string Effective_Date, string Skip_charges, string Frequency, string Allow, string Flat_rate, string Interset_rate, string Pl_CRAccid, string min_charges, string max_charges, string GST_SUBGL, string GST_InterestRate, string GST_Amount, string Reason, string Reason_description, string Flat_Charges, string Percent_Charge, string Last_ApplDate, string Particular)
    {
        try
        {
            sql = "EXEC Sp_AVS5018  @FLAG='UPDATE',@ID='"+id+"', @BRCD= '" + brcd + "', @Acct_Type= '" + Acct_Type + "', @Retrun_Type= '" + Retrun_Type + "', @Effective_Date= '" + conn.ConvertDate(Effective_Date) + "', @Skip_charges= '" + Skip_charges + "', @Frequency= '" + Frequency + "', @Allow= '" + Allow + "', @Flat_rate= '" + Flat_rate + "', @Interset_rate= '" + Interset_rate + "', @PLACCNO= '" + Pl_CRAccid + "', @min_charges= '" + min_charges + "', @max_charges= '" + max_charges + "', @GST_SUBGL= '" + GST_SUBGL + "', @GST_InterestRate= '" + GST_InterestRate + "', @GST_Amount= '" + GST_Amount + "', @Reason= '" + Reason + "', @Reason_description= '" + Reason_description + "', @Flat_Charges= '" + Flat_Charges + "', @Percent_Charge= '" + Percent_Charge + "', @Last_ApplDate= '" + conn.ConvertDate(Last_ApplDate) + "', @Particular= '" + Particular + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
          
        }
        return result;
    }



    public int GetIntrestMasterID(string ID, string brcd, string Acct_Type, string Retrun_Type, string Effective_Date, string Skip_charges, string Frequency, string Allow, string Flat_rate, string Interset_rate, string Pl_CRAccid, string min_charges, string max_charges, string GST_SUBGL, string GST_InterestRate, string GST_Amount, string Reason, string Reason_description, string Flat_Charges, string Percent_Charge, string Last_ApplDate, string Particular)
    {
        int Result = 0;

        try
        {


            sql = "EXEC Sp_AVS5018  @FLAG='UPDATE', @ID= '" + ID + "', @BRCD= '" + brcd + "', @Acct_Type= '" + Acct_Type + "', @Retrun_Type= '" + Retrun_Type + "', @Effective_Date= '" + conn.ConvertDate(Effective_Date)+ "', @Skip_charges= '" + Skip_charges + "', @Frequency= '" + Frequency + "', @Allow= '" + Allow + "', @Flat_rate= '" + Flat_rate + "', @Interset_rate= '" + Interset_rate + "', @PLACCNO= '" + Pl_CRAccid + "', @min_charges= '" + (string.IsNullOrEmpty(min_charges) ? "0" : min_charges) + "', @max_charges= '" + (string.IsNullOrEmpty(max_charges) ? "0" : max_charges) + "', @GST_SUBGL= '" + GST_SUBGL + "', @GST_InterestRate= '" + GST_InterestRate + "', @GST_Amount= '" + (string.IsNullOrEmpty(GST_Amount) ? "0" : GST_Amount) + "', @Reason= '" + Reason + "', @Reason_description= '" + Reason_description + "', @Flat_Charges= '" + Flat_Charges + "', @Percent_Charge= '" + Percent_Charge + "', @Last_ApplDate= '" + conn.ConvertDate(Last_ApplDate)+ "', @Particular= '" + Particular + "'";
             Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }


    public int GetAthurization(string ID, string BRCD, string MID)
    {
        try
        {


            sql = "EXEC Sp_AVS5018  @FLAG='Authorize', @ID= '" + ID + "',  @BRCD= '" + BRCD + "', @VID= '" + MID + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }


    public DataTable GetIntrestViewID(string ID, string BRCD)
    {
        try
        {


            sql = "DELETE from AVS5018  WHERE  ID = '" + ID + "' AND STAGE<>'1004'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }



    public DataTable GetIntrestMasterID(string ID, string BRCD)
    {
        try
        {

            sql = "select * from AVS5018 WHERE  ID = '" + ID + "'"; 
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }


}

