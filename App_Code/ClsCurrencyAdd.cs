using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsCurrencyAdd
/// </summary>
public class ClsCurrencyAdd
{
    string sql = "";
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();
   
    int Result = 0;
	public ClsCurrencyAdd()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Get_GridDetails(GridView GRD,string BRCD,string MID,string EDT)
    {
        try
        {

            sql = "SELECT ID,V_TYPE,NOTE_TYPE,NO_OF_NOTES,TOTAL_VALUE,STAGE FROM AVS5011 WHERE STAGE<>1003 AND STAGE<>1004 and BRCD='" + BRCD + "'";//BRCD ADDED --Abhishek
            
                Result = DBconn.sBindGrid(GRD, sql);
     
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int OPR_Currency(string Vtype, string Noofnt, string NtType, string Total, string Brcd, string Edt, string Mid, string FL)
    {
        try
        {
            string Pcmac = DBconn.PCNAME();
            if (FL == "INSERT")
            {
                sql = "EXEC SP_CURRENCY_OPR @FLAG1='"+FL+"',@EDT='"+DBconn.ConvertDate(Edt)+"',@MID='"+Mid+"',@BRCD='"+Brcd+"',@NOTETYPE='"+NtType+"',@VTYPE='"+Vtype+"',@NOOFNOTES='"+Noofnt+"',@TOTAL='"+Total+"',@PCMAC='"+Pcmac+"'";
            }
            else if (FL == "DELETE")
            {
                sql = "EXEC SP_CURRENCY_OPR @FLAG1='" + FL + "',@MID='" + Mid + "',@BRCD='" + Brcd + "',@NOTETYPE='" + NtType + "',@VTYPE='" + Vtype + "'";
            }
            else if (FL == "MODIFY") 
            {
                sql = "EXEC SP_CURRENCY_OPR @FLAG1='" + FL + "',@EDT='" + DBconn.ConvertDate(Edt) + "',@MID='" + Mid + "',@BRCD='" + Brcd + "',@NOTETYPE='" + NtType + "',@VTYPE='" + Vtype + "',@NOOFNOTES='" + Noofnt + "',@TOTAL='" + Total + "',@PCMAC='" + Pcmac + "'"; ;
            }
            else if (FL == "AUTHORIZE")
            {
                sql = "EXEC SP_CURRENCY_OPR @FLAG1='" + FL + "',@MID='" + Mid + "',@BRCD='" + Brcd + "',@NOTETYPE='" + NtType + "',@VTYPE='" + Vtype + "'";
            }
           
            Result = DBconn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int View_curr(GridView GRD,string FL,string FL2 )
    {

        try
        {
            if (FL == "VIEW")
            {
                if (FL2 == "CREATED")
                {
                    sql = "EXEC SP_CURRENCY_OPR @FLAG1='" + FL + "',@FLAG2='" + FL2 + "'";
                }
               
                else if (FL2 == "AUTHO")
                {
                    sql = "EXEC SP_CURRENCY_OPR @FLAG1='"+FL+"',@FLAG2='"+FL2+"'";
                }
                else if (FL2 == "ALL")
                {
                    sql = "EXEC SP_CURRENCY_OPR @FLAG1='" + FL + "',@FLAG2='" + FL2 + "'";
                }
            }
           // DT = DBconn.GetDatatable(sql);
            Result = DBconn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable Fill_TextBox(string ID, string BRCD,string MID)
    {
        try
        {
            sql = "SELECT * FROM AVS5011 WHERE ID='" + ID + "' AND BRCD='"+BRCD+"' AND MID='"+MID+"'";
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public string Get_Vault_Type(string MID,string BRCD)
    {
        try
        {
            sql = "SELECT TOP 1(V_TYPE) FROM AVS5011 WHERE MID='" + MID + "' and BRCD='" + BRCD + "'";  //BRCD ADDED --Abhishek
            MID = DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return MID;
    }

}