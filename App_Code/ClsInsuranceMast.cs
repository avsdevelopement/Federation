using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
/// <summary>
/// Summary description for ClsInsuranceMast
/// </summary>
public class ClsInsuranceMast
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    string sql;
	public ClsInsuranceMast()
	{
	}
    public int Insertdata(string prdcode,string Accno,string PolicyNo,string InstAmt,string Startdate ,string Expirydate ,string Closedate ,string PremAmt ,string PolStatus ,string InsuranceCom,
    string DescriptionN, string EndorsementStatus, string SentDate, string ReceivedDate, string Brcd, string Stage, string Mid, string FL, string Systemdate)
    {
        try
        {
            sql = "EXEC Isp_AVS0008 @Prdcode='" + prdcode + "',@Accno='" + Accno + "',@PolicyNo='" + PolicyNo + "',@InstAmt='" + InstAmt + "',@Startdate='" + conn.ConvertDate(Startdate).ToString() + "' ,@Expirydate='" + conn.ConvertDate(Expirydate).ToString() + "' ," +
                "@Closedate='"+conn.ConvertDate(Closedate).ToString()+"' ,@PremAmt='"+PremAmt+"' ,@PolStatus='"+PolStatus+"' ,@InsuranceCom='"+InsuranceCom+"',@DescriptionN='"+DescriptionN+"',"+
                "@EndorsementStatus='"+EndorsementStatus+"',@SentDate='"+conn.ConvertDate(SentDate).ToString()+"',@ReceivedDate='"+conn.ConvertDate(ReceivedDate).ToString()+"',"+
                "@Brcd='" + Brcd + "' ,@Stage='" + Stage + "' ,@Mid='" + Mid + "',@Flag='" + FL + "',@Systemdate='" + conn.ConvertDate(Systemdate).ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        } 
        return Result;

    }
    public int ModifyData(string prdcode, string Accno, string PolicyNo, string InstAmt, string Startdate, string Expirydate, string Closedate, string PremAmt, string PolStatus, string InsuranceCom,
    string DescriptionN, string EndorsementStatus, string SentDate, string ReceivedDate, string Brcd, string Stage, string Mid, string FL, string ID, string Systemdate)
    {
        try
        {
            sql = "EXEC Isp_AVS0008 @Prdcode='" + prdcode + "',@Accno='" + Accno + "',@PolicyNo='" + PolicyNo + "',@InstAmt='" + InstAmt + "',@Startdate='" + conn.ConvertDate(Startdate).ToString() + "' ,@Expirydate='" + conn.ConvertDate(Expirydate).ToString() + "' ," +
                "@Closedate='" + conn.ConvertDate(Closedate).ToString() + "' ,@PremAmt='" + PremAmt + "' ,@PolStatus='" + PolStatus + "' ,@InsuranceCom='" + InsuranceCom + "',@DescriptionN='" + DescriptionN + "'," +
                "@EndorsementStatus='" + EndorsementStatus + "',@SentDate='" + conn.ConvertDate(SentDate).ToString() + "',@ReceivedDate='" + conn.ConvertDate(ReceivedDate).ToString() + "'," +
                "@Brcd='" + Brcd + "' ,@Stage='" + Stage + "' ,@Mid='" + Mid + "',@Flag='" + FL + "',@Id='" + ID + "',@Systemdate='" + conn.ConvertDate(Systemdate).ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int AuthoriseData(string prdcode, string Accno, string Brcd, string PolicyNo, string FL, string ID, string Mid)
    {
        try
        {
            sql = "EXEC Isp_AVS0008 @Prdcode='" + prdcode + "',@Accno='" + Accno + "',@Brcd='" + Brcd + "' ,@PolicyNo='" + PolicyNo + "',@Flag='" + FL + "',@Id='" + ID + "',@Mid='" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string prdcode, string Accno, string Brcd, string PolicyNo, string FL, string ID, string Mid)
    {
        try
        {
            sql = "EXEC Isp_AVS0008 @Prdcode='" + prdcode + "',@Accno='" + Accno + "',@Brcd='" + Brcd + "' ,@PolicyNo='" + PolicyNo + "',@Flag='" + FL + "',@Id='" + ID + "',@Mid='" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGrid(GridView Gview, string brcd, string EDate, string PT, string AC)
    {
        try
        {

            if (PT == "" && AC == "")
            {
                sql = "Select Id,PolicyNo,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS5021 L" +
                    " INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                        " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO  Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in(1004) and L.Brcd='" + brcd + "' and L.SYSTEMDATE='" + conn.ConvertDate(EDate) + "' order by PRDCODE, ACCTNO ";
            }
            else
            {

                sql = "Select Id,PolicyNo,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS5021 L" +
                    " INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                        " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO  Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in(1004) and L.Brcd='" + brcd + "' and L.PRDCODE='" + PT + "' and L.ACCTNO='" + AC + "' order by PRDCODE, ACCTNO ";
            }
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string brcd, string PolicyNo, string ID, string prdcode, string Accno)
    {
        try
        {
            sql = "Select Id,PRDCODE,ACCTNO,PolicyNo,InstAmt,CONVERT(VARCHAR(11),Startdate,103) AS Startdate ,CONVERT(VARCHAR(11),Expirydate,103) AS Expirydate ,CONVERT(VARCHAR(11),Closedate,103) AS Closedate ,PremAmt ,PolStatus ,InsuranceCom,DescriptionN,EndorsementStatus,CONVERT(VARCHAR(11),SentDate,103) AS SentDate,CONVERT(VARCHAR(11),ReceivedDate,103) AS ReceivedDate from AVS5021 where " +
            "  Brcd='" + brcd + "' and PolicyNo='" + PolicyNo + "' and ID='" + ID + "' and PRDCODE='" + prdcode + "' and ACCTNO='" + Accno + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetStage(string BRCD, string Prdcode, string ACC)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS5021 WHERE BRCD='" + BRCD + "' AND PRDCODE='" + Prdcode + "' and ACCTNO='" + ACC + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
}