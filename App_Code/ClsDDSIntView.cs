using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsDDSIntView
/// </summary>
public class ClsDDSIntView
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql="";
    //int Result = 0;
    int Disp;

	public ClsDDSIntView()
	{
		
	}

    public DataTable GetDetails( string FSBGL, string TSBGL, string PRDTYPE, string CSTTYPE, string FL)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public void BindCustType(DropDownList ddlcust)
    {

        sql = "Select DESCRIPTION Name ,SRNO id from LOOKUPFORM1 where LNO=1016 order by SRNO";
        conn.FillDDL(ddlcust,sql);
    }
    //string FBRCD, string TBRCD, BRCD between '" + FBRCD + "' and '" + TBRCD + "'  and  
    public int GetFilter(GridView grid, string CT, string PR, string FPRDTYPE, string TPRDTYPE, string FL)
    {
        try
        {
            if (FL == "BRPC")
            {
                sql = "select SRNO,EFFECTDATE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTO,RATE,PENALTY,BRCD from interestmaster " +
                        " where DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "'  and STAGE<>'1004' order by SRNO";
            }
            else if (FL == "CT")
            {
                sql = "select SRNO,EFFECTDATE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTO,RATE,PENALTY,BRCD from interestmaster where  DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "' and TDCUSTTYPE='" + CT + "' and STAGE<>'1004' order by SRNO";
            }
            else if (FL == "PR")
            {
                sql = "select SRNO,EFFECTDATE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTO,RATE,PENALTY,BRCD from interestmaster where DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "' and PERIODTYPE='" + PR + "' and STAGE<>'1004' order by SRNO";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Disp;
    }
    public DataTable GetDDInfo(string FL, string FPRDTYPE, string TPRDTYPE, string CT, string PR)
    {
        DataTable DT1 = new DataTable();
        try
        {
            //IM.BRCD between '" + FBRCD + "' and '" + TBRCD + "'  and 
            sql = "select IM.SRNO,CONVERT(VARCHAR(11),EFFECTDATE,103) EFFECTDATE ,LO.DESCRIPTION AS CUSTTYPE,IM.DEPOSITGL,IM.PERIODTYPE,IM.PERIODFROM,IM.PERIODTO,IM.RATE,IM.PENALTY,IM.BRCD from interestmaster  IM " +
                    " LEFT JOIN LOOKUPFORM1 LO ON IM.TDCUSTTYPE=LO.SRNO "+" where IM.DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "'  and STAGE<>'1004' AND LO.LNO='1016' order by SRNO";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }
    public DataTable GetSetUtally(string BRCD,  string Fdate, string Tdate,string FYear,string TYear,string Fmonth,string Tmonth)//Dhanya Shetty//14/03/2018
    {
         try
        {
            sql = "EXEC Sp_SetTally @PFDT='" + conn.ConvertDate(Fdate) + "',@PTDT='" + conn.ConvertDate(Tdate) + "',@BRCD='" + BRCD + "',@pfmonth='" + Fmonth + "',@ptmonth='" + Tmonth + "',@pfyear='" + FYear + "',@ptyear='" + TYear + "'";
           DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetFromToDetails(string FDate,string TDate)
    {
        try
        {
            sql = "Select DATEPART(YEAR,'" + conn.ConvertDate(FDate) + "') as FYear, (RIGHT('0'+CAST(MONTH('" + conn.ConvertDate(FDate) + "') AS varchar(2)),2)) as Fmonth,DATEPART(YEAR,'" + conn.ConvertDate(TDate) + "') as TYear, (RIGHT('0'+CAST(MONTH('" + conn.ConvertDate(TDate) + "') AS varchar(2)),2)) as Tmonth";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return DT;
    }
    public DataTable GetKYCREport(string BRCD, string Fdate, string Tdate, string RFlag)//Dhanya Shetty//19/03/2018
    {
        try
        {
            sql = "EXEC ISP_AVS0140 @Fdate='" + conn.ConvertDate(Fdate) + "',@TDate='" + conn.ConvertDate(Tdate) + "',@BRCD='" + BRCD + "',@Flag='" + RFlag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetDormantR(string BRCD, string Date, string CPrd, string Period, string Amt, string DPrd, string Flag)//Dhanya Shetty//21/03/2018
    {
        try
        {

            sql = "EXEC DormantAcListVoucher @PFDT='" + conn.ConvertDate(Date) + "',@SubGlCode='" + CPrd + "',@BRCD='" + BRCD + "',@Period='" + Period + "',@Amt='" + Amt + "',@SUBGLCODE_2='" + DPrd + "',@Flag='" + Flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetCKYCListDTList(string BRCD, string FDate, string FACCNO, string TACCNO, string RFlag)
    {
        try
        {
            sql = "EXEC RptCKYCList @AsOnDate='" + conn.ConvertDate(FDate) + "',@Brcd='" + BRCD + "',@FCustno='" + FACCNO + "',@TCustno='" + TACCNO + "',@Flag='" + RFlag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}