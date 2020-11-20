using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public class ClsAVS5171_1
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();

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
    public string MID { get; set; }

	public ClsAVS5171_1()
	{
		//
	}

    public DataTable FnBL_GetRecoveryStatRep(ClsAVS5171_1 RS)
    {
        try
        {
            //   sql = "Exec Isp_Recovery_Statement @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@OnDate='" + Conn.ConvertDate(RS.ASONDT) + "',@RecDiv=" + RS.RECDIV + ",@RecCode=" + RS.RECCODE + ",@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
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
    public DataTable FnBL_GetRecoveryStatRep_1009(ClsAVS5171_1 RS)
    {
        try
        {
            sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@OnDate='" + Conn.ConvertDate(RS.ASONDT) + "',@RecDiv=" + RS.RECDIV + ",@RecCode=" + RS.RECCODE + ",@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
            RS.DT = Conn.GetDatatable(RS.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RS.DT;
    }

    public int FnBl_GetPostings(ClsAVS5171_1 RS)
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

    public int FnBl_GetCustPostings(ClsAVS5171_1 RS)
    {
        try
        {
            if (RS.BANKCODE == "1009")
            {
                Sql = "Exec Isp_Recovery_CustStatement_X_1009 @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
            }
            else
            {
                Sql = "Exec Isp_Recovery_CustStatement_X @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@RecCode='" + RS.RECCODE + "',@RecDiv='" + RS.RECDIV + "',@CustNo='" + RS.CustNo + "'";
            }
            Res = Conn.sBindGrid(RS.GD, Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public string FnBl_GetPostingTotal(ClsAVS5171_1 RS)
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
            }
            RES = Conn.sExecuteScalar(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }

    public string FnBl_GetBANKCode(ClsAVS5171_1 RS)
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

    public DataTable GetCodes(string BRCD, string RECDIV)
    {
        try
        {
            sql = "Select RECDIV,RECCODE,DESCR,STAGE,BRCD from PAYMAST   " +
                            " where BRCD='" + BRCD + "' " +
                            " and RECDIV='" + RECDIV + "' " +
                             " and RECDIV<>RECCODE " +
                            " and STAGE='1003' " +
                            " order  by RECCODE";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }


    public string Fn_GetSMSPara()
    {
        string RES = "";
        try
        {
            Sql = " Exec Isp_Rec_Sms @Flag='GetPara'";
            RES = Conn.sExecuteScalar(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
    public int Fn_InsertSMS(ClsAVS5171_1 RS)
    {
        try
        {

            Sql = " Exec Isp_Rec_SMS @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "', " +
                " @RecDiv='" + RS.RECDIV + "',@RecCode='" + RS.RECCODE + "',@AuthMid='" + RS.MID + "',@CreateMid='" + RS.MID + "', " +
                " @EDate='" + Conn.ConvertDate(RS.ASONDT) + "'";

            Res = Conn.sExecuteQuery(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public DataTable Fn_GetCustno(ClsAVS5171_1 RS)
    {
        try
        {

            Sql = " Exec Isp_Rec_SMS @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "', " +
                " @RecDiv='" + RS.RECDIV + "',@RecCode='" + RS.RECCODE + "'," +
                " @EDate='" + Conn.ConvertDate(RS.ASONDT) + "'";

            DT = Conn.GetDatatable(Sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}