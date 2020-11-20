using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for InwordCharges
/// </summary>
/// 


public class InwordCharges
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
    int nom1;


    public int InsertDataFromCashier(string brcd, string Acct_Type, string Retrun_Type, string Effective_Date, string Skip_charges, string Frequency, string Allow, string Flat_rate, string Interset_rate, string Pl_CRAccid, string min_charges, string max_charges, string Reason, string Reason_description, string Flat_Charges, string Percent_Charge, string Last_ApplDate, string Particular)
    {
       
        try
        {
            sql = "EXEC sp_INWORD_OUTWORD_CHARGES  @FLAG='INSERT', @BRCD= '" + brcd + "', @Retrun_Type= '" + Retrun_Type + "', @Effective_Date= '" + conn.ConvertDate(Effective_Date).ToString() + "', @Skip_charges= '" + Skip_charges + "', @Frequency= '" + Frequency + "', @Allow= '" + Allow + "', @Flat_rate= '" + Flat_rate + "', @Interset_rate= '" + Interset_rate + "', @Pl_CRAccid= '" + Pl_CRAccid + "', @min_charges= '" + min_charges + "', @max_charges= '" + max_charges + "', @Reason= '" + Reason + "', @Reason_description= '" + Reason_description + "', @Flat_Charges= '" + Flat_Charges + "', @Percent_Charge= '" + Percent_Charge + "', @Last_ApplDate= '" + conn.ConvertDate(Last_ApplDate).ToString() + "', @Particular= '" + Particular + "'";
            nom1 = conn.sExecuteQuery(sql);
        
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }

    public int ModifyData(string brcd, string Acct_Type, string Retrun_Type, string Effective_Date, string Skip_charges, string Frequency, string Allow, string Flat_rate, string Interset_rate, string Pl_CRAccid, string min_charges, string max_charges, string Reason, string Reason_description, string Flat_Charges, string Percent_Charge, string Last_ApplDate, string Particular)
    {
        try
        {
            sql = "EXEC sp_INWORD_OUTWORD_CHARGES  @FLAG='UPDATE', @BRCD= '" + brcd + "', @Acct_Type= '" + Acct_Type + "', @Retrun_Type= '" + Retrun_Type + "', @Effective_Date= '" + conn.ConvertDate(Effective_Date).ToString() + "', @Skip_charges= '" + Skip_charges + "', @Frequency= '" + Frequency + "', @Allow= '" + Allow + "', @Flat_rate= '" + Flat_rate + "', @Interset_rate= '" + Interset_rate + "', @Pl_CRAccid= '" + Pl_CRAccid + "', @min_charges= '" + min_charges + "', @max_charges= '" + max_charges + "', @Reason= '" + Reason + "', @Reason_description= '" + Reason_description + "', @Flat_Charges= '" + Flat_Charges + "', @Percent_Charge= '" + Percent_Charge + "', @Last_ApplDate= '" + conn.ConvertDate(Last_ApplDate).ToString() + "', @Particular= '" + Particular + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }





   
}