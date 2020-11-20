using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsRecoveryStatement
/// </summary>
public class ClsRecoveryStatement
{
    string sql = "";
    public int Res = 0;
    public string Sql = "";

    public GridView GD { get; set; }
    public string FL { get; set; }
    public string BRCD { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }
    public string ASONDT { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }
    public string CustNo { get; set; }
    public string BANKCODE { get; set; }

	public ClsRecoveryStatement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();



    public DataTable FnBL_GetRecoveryStatRep(ClsRecoveryStatement RS)
    {
        try
        {
            //sql = "Exec Isp_Recovery_Statement @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@OnDate='" + Conn.ConvertDate(RS.ASONDT) + "',@RecDiv=" + RS.RECDIV + ",@RecCode=" + RS.RECCODE + ",@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
            sql = "Exec Isp_Recovery_Create @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@OnDate='" + Conn.ConvertDate(RS.ASONDT) + "',@RecDiv=" + RS.RECDIV + ",@RecCode=" + RS.RECCODE + ",@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
            RS.DT = Conn.GetDatatable(RS.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RS.DT;
    }

    //1009 15-11-2017
    public DataTable FnBL_GetRecoveryStatRep_1009(ClsRecoveryStatement RS)
    {
        try
        {
            sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@OnDate='" + Conn.ConvertDate(RS.ASONDT) + "',@RecDiv=" + RS.RECDIV + ",@RecCode=" + RS.RECCODE + ",@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
            
            RS.DT = Conn.GetDatatable(RS.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RS.DT;
    }

    public int FnBl_GetPostings(ClsRecoveryStatement RS)
    {
        try
        {
            if (RS.BANKCODE == "1009")
            {
               // Sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
                Sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
            }
            else
            {
                //Sql = "Exec Isp_Recovery_Statement @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
                Sql = "Exec Isp_Recovery_Create @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
            }
            Res = Conn.sBindGrid(RS.GD, Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int FnBl_GetCustPostings(ClsRecoveryStatement RS)
    {
        try
        {
            if (RS.BANKCODE == "1009")
            {
                Sql = "Exec Isp_Recovery_CustStatement_X_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
                //Sql = "Exec Isp_Recovery_CustStatement_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
            }
            else
            {
                Sql = "Exec Isp_Recovery_CustStatement_X @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
                //Sql = "Exec Isp_Recovery_CustStatement @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
            }
            Res = Conn.sBindGrid(RS.GD, Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public string FnBl_GetPostingTotal(ClsRecoveryStatement RS)
    {
        string RES = "";
        try
        {
            if (RS.BANKCODE == "1009")
            {
                //Sql = "Exec Isp_Recovery_CustStatement_1009 @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
                Sql = "Exec Isp_Recovery_CustStatement_X_1009 @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
            }
            else
            {
                Sql = "Exec Isp_Recovery_CustStatement_X @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
                //Sql = "Exec Isp_Recovery_CustStatement @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
            }
            RES = Conn.sExecuteScalar(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
    public string FnBl_GetPostingTotal_1(ClsRecoveryStatement RS1)
    {
        string RES = "";
        try
        {
            if (RS1.BANKCODE == "1009")
            {
                //Sql = "Exec Isp_Recovery_CustStatement_1009 @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
                Sql = "Exec Isp_Recovery_CustStatement_X_1009 @Custno='" + RS1.CustNo + "',@Flag='" + RS1.FL + "',@Brcd='" + RS1.BRCD + "',@ForMM='" + RS1.MM + "',@ForYY='" + RS1.YY + "',@RecCode='" + RS1.RECCODE + "',@RecDiv='" + RS1.RECDIV + "'";
            }
            else
            {
                Sql = "Exec Isp_Recovery_CustStatement_X @Custno='" + RS1.CustNo + "',@Flag='" + RS1.FL + "',@Brcd='" + RS1.BRCD + "',@ForMM='" + RS1.MM + "',@ForYY='" + RS1.YY + "',@RecCode='" + RS1.RECCODE + "',@RecDiv='" + RS1.RECDIV + "'";
                //Sql = "Exec Isp_Recovery_CustStatement @Custno='" + RS.CustNo + "',@Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "'";
            }
            RES = Conn.sExecuteScalar(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
    public string FnBl_GetBANKCode(ClsRecoveryStatement RS)
    {
        string RES = "";
        try
        {
            Sql = "Select BANKCD from BANKNAME where BRCD=0";
            RES = Conn.sExecuteScalar(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
}