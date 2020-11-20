using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsDDSToLoan
/// </summary>
public class ClsDDSToLoan
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;
	public ClsDDSToLoan()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert_SI(string dbsubgl, string dbglname, string dbaccno, string dbaccname, string crsubgl, string crglname, string craccno,string SITYPE, string craccname,
                         string emiamt, string totalemi, string fdate, string tdate, string nxtdate, string prevdate, string trfday,string Parti, string edt, string brcd, string mid,string fl,string SI_TYPETRF)
    {

        try
        {
           // string setno = BD.GetSetNo(edt, "DaySetNo").ToString();
            string dgl = BD.GetAccTypeGL(dbsubgl, brcd);
            string[] dbgl = dgl.Split('_');
            string cgl=BD.GetAccTypeGL(crsubgl, brcd);
            string[] crgl = cgl.Split('_');
            string PACMAC = conn.PCNAME();
            if (fl == "INSERT")
            {

                sql = "EXEC SP_SI_INSERT @FLAG='" + fl + "',@DBGL='" + dbgl[1].ToString() + "',@DBPRDCD='" + dbsubgl + "',@DBACCNO='" + dbaccno + "',@CRGL='" + crgl[1].ToString() + "',@CRPRDCD='" + crsubgl + "'," +
                                       "@CRACCNO ='" + craccno + "',@SITYPE='" + SITYPE + "',@EMIAMOUNT='" + emiamt + "',@TOTALNOEMI='" + totalemi + "',@FDATE='" + conn.ConvertDate(fdate) + "',@TDATE ='" + conn.ConvertDate(tdate) + "',@NEXTTDATE ='" + conn.ConvertDate(nxtdate) + "'," +
                                       "@PREVTDATE ='" + conn.ConvertDate(prevdate) + "',@TRANSFERDAY='" + trfday + "',@STAGE=1001,@MID='" + mid + "',@MIDDATE ='" + conn.ConvertDate(edt) + "'," +
                                       "@BRCD ='" + brcd + "',@EDT ='" + conn.ConvertDate(edt) + "',@LASTPOSTDATE ='" + conn.ConvertDate(edt) + "',@PARTI='" + Parti + "',@PACMAC ='" + PACMAC + "',@SITYPETRF='" + SI_TYPETRF + "'";

                Result = conn.sExecuteQuery(sql);
            }
          
           
            

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int Delete_SI(string fl,string sino,string emiamt,string craccno,string dbaccno,string MID,string BRCD)
    {
        try
        {
            if (fl == "DELETE")
            {

                sql = "EXEC SP_SI_INSERT @FLAG='" + fl + "',@SINO='" + sino + "',@EMIAMOUNT='" + emiamt + "',@CRACCNO='" + craccno + "',@DBACCNO='" + dbaccno + "',@MID='" + MID + "', @BRCD='" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int Authoize_SI(string fl, string sino,string MID,string BRCD)
    {
        try
        {
            if (fl == "AUTHO")
            {

                sql = "EXEC SP_SI_INSERT @FLAG='" + fl + "',@SINO='" + sino + "',@MID='" + MID + "',@BRCD='" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string Check_MID(string FL, string sino, string MID, string BRCD)
    {
        try
        {
            sql = "EXEC SP_SI_INSERT @FLAG='" + FL + "',@SINO='" + sino + "',@MID='" + MID + "',@BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int Modify_SI(string dbsubgl, string dbglname, string dbaccno, string dbaccname, string crsubgl, string crglname, string craccno,string SITYPE, string craccname,
                         string emiamt, string totalemi, string fdate, string tdate, string nxtdate, string prevdate, string trfday, string edt, string brcd, string mid, string fl, string sino,string SI_TYPETRF)
    
    {
        try
        {
            string dgl = BD.GetAccTypeGL(dbsubgl, brcd);
            string[] dbgl = dgl.Split('_');
            string cgl = BD.GetAccTypeGL(crsubgl, brcd);
            string[] crgl = cgl.Split('_');
            if (fl == "MODIFY")
            {
                sql = "EXEC SP_SI_INSERT @FLAG='" + fl + "',@DBGL='" + dbgl[1].ToString() + "',@DBPRDCD='" + dbsubgl + "',@DBACCNO='" + dbaccno + "',@CRGL='" + crgl[1].ToString() + "',@CRPRDCD='" + crsubgl + "'," +
                                      "@CRACCNO ='" + craccno + "',@SITYPE='" + SITYPE + "',@EMIAMOUNT='" + emiamt + "',@TOTALNOEMI='" + totalemi + "',@FDATE='" + conn.ConvertDate(fdate) + "',@TDATE ='" + conn.ConvertDate(tdate) + "',@NEXTTDATE ='" + conn.ConvertDate(nxtdate) + "'," +
                                      "@PREVTDATE ='" + conn.ConvertDate(prevdate) + "',@TRANSFERDAY='" + trfday + "',@PARTI='',@STAGE=1001,@MID='" + mid + "',@CID ='',@MIDDATE ='',@CIDDATE ='',@REMARK =''," +
                                      "@BRCD ='" + brcd + "',@EDT ='" + conn.ConvertDate(edt) + "',@LASTPOSTDATE ='',@SYSDATE ='',@PACMAC ='',@SINO='" + sino + "',@SITYPETRF='" + SI_TYPETRF + "'";

                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

     public int GetEMIAMT(string loanacc,string loansubgl,string BRCD)
    {
        try
        {
            sql = "SELECT INSTALLMENT FROM LOANINFO WHERE CUSTACCNO='" + loanacc + "' AND LOANGLCODE='" + loansubgl + "' and BRCD='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
   

     public DataTable GetSI_Report1(string BRCD, string FDT, string TDT, string MID,string FL)
     {
         try
         {
             sql = "EXEC Isp_SI_DdsToLoan @Flag='" + FL + "',@Brcd='" + BRCD + "',@FDate='" + conn.ConvertDate(FDT) + "',@TDate='" + conn.ConvertDate(TDT) + "'";
             DT=conn.GetDatatable(sql);
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
         return DT;
     }

    

}