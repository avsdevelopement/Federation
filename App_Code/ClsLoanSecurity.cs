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
/// Summary description for ClsLoanSecurity
/// </summary>
public class ClsLoanSecurity
{
    DbConnection conn = new DbConnection();

    string sql;
    DataTable DT;
    int result;
    string Result="", srno = "";

	public ClsLoanSecurity()
	{
		
	}
    public int InsertData(string BRCD, string PRDCODE, string ACCTNO, string SRNO, string PRIMARYCO, string REFAC_DATE, string MAINCLASS, string SUBCLASS, string CBSSECNO, string REFERENCENO, 
        string SECURITYDATE, string LIMITEXPDATE, string VALUE, string MARGIN, string MKTVALUE, string LASTDPDATE, string DESCP, string MODEOFCHARGE, string DEPECATEDVAL, string TYPEOFCHARGE,
        string PERDEP, string SECSTATUS, string SECDATE, string VALUATIONDATE, string RELEASEDATE, string STAGE, string MID, string SYSTEMDATE)
    {
        try
        {
            sql = "insert into AVS_LNSECURITY( BRCD, PRDCODE, ACCTNO, SRNO, PRIMARYCO, REFAC_DATE, MAINCLASS, SUBCLASS, CBSSECNO, REFERENCENO, SECURITYDATE, LIMITEXPDATE, VALUE, MARGIN, MKTVALUE, LASTDPDATE	,"+
           "DESCP, MODEOFCHARGE, DEPECATEDVAL, TYPEOFCHARGE, PERDEP, SECSTATUS, SECDATE, VALUATIONDATE, RELEASEDATE, STAGE,MID,SYSTEMDATE)values( '" + BRCD + "', '" + PRDCODE + "', '" + ACCTNO + "', '" + SRNO + "', '" + PRIMARYCO + "'," +
           "'"+ conn.ConvertDate(REFAC_DATE).ToString()+"','"+ MAINCLASS+"', '"+SUBCLASS+"','"+ CBSSECNO+"', '"+REFERENCENO+"', '"+conn.ConvertDate(SECURITYDATE).ToString()+"','"+conn.ConvertDate( LIMITEXPDATE).ToString()+"',"+
           "'"+ VALUE+"','"+ MARGIN+"','"+ MKTVALUE+"', '"+conn.ConvertDate(LASTDPDATE).ToString()+"','"+ DESCP+"','"+ MODEOFCHARGE+"', '"+DEPECATEDVAL+"', '"+TYPEOFCHARGE+"', '"+PERDEP+"', '"+SECSTATUS+"','"+ conn.ConvertDate(SECDATE).ToString()+"',"+
           "'" + conn.ConvertDate(VALUATIONDATE).ToString() + "','" + conn.ConvertDate(RELEASEDATE).ToString() + "', '" + STAGE + "','" + MID + "',GetDate())";
          
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

    public int ModifyData(string BRCD, string PRDCODE, string ACCTNO, string SRNO, string PRIMARYCO, string REFAC_DATE, string MAINCLASS, string SUBCLASS, string CBSSECNO, string REFERENCENO, string SECURITYDATE,
        string LIMITEXPDATE, string VALUE, string MARGIN, string MKTVALUE, string LASTDPDATE , string DESCP, string MODEOFCHARGE, string DEPECATEDVAL, string TYPEOFCHARGE, string PERDEP, string SECSTATUS, string SECDATE,
        string VALUATIONDATE, string RELEASEDATE,string MID, string SYSTEMDATE, string id)
    {
        try
        {
            sql = "Update AVS_LNSECURITY set stage=1002, BRCD='" + BRCD + "',PRDCODE='" + PRDCODE + "',ACCTNO='" + ACCTNO + "',SRNO='" + SRNO + "',PRIMARYCO='" + PRIMARYCO + "',"+
                "REFAC_DATE='" + conn.ConvertDate(REFAC_DATE).ToString() + "',MAINCLASS='" + MAINCLASS + "',SUBCLASS='" + SUBCLASS + "',CBSSECNO='" + CBSSECNO + "',"+
                "REFERENCENO='" + REFERENCENO + "',SECURITYDATE='" + conn.ConvertDate(SECURITYDATE).ToString() + "',LIMITEXPDATE='" + conn.ConvertDate(LIMITEXPDATE).ToString() + "',"+
                "VALUE='" + VALUE + "',MARGIN='" + MARGIN + "',LASTDPDATE='" + conn.ConvertDate(LASTDPDATE).ToString() + "',DESCP='" + DESCP + "',MODEOFCHARGE='" + MODEOFCHARGE + "',"+
                "DEPECATEDVAL='" + DEPECATEDVAL + "',TYPEOFCHARGE='" + TYPEOFCHARGE + "',PERDEP='" + PERDEP + "',SECSTATUS='" + SECSTATUS + "',SECDATE='" + conn.ConvertDate(SECDATE).ToString() + "',MID='" + MID + "',SYSTEMDATE='" + conn.ConvertDate(SYSTEMDATE).ToString() + "'," +
                "VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE).ToString() + "', RELEASEDATE='" + conn.ConvertDate(RELEASEDATE).ToString() + "' where  BRCD='" + BRCD + "'AND PRDCODE='" + PRDCODE + "'AND ACCTNO='" + ACCTNO + "' and ID='" + id + "'";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }

    }

    public int DeleteData(string BRCD, string PRDCODE, string ACCTNO, string MID, string id)
    {
        try
        {
            sql = "update AVS_LNSECURITY set stage=1004,MID='" + MID + "' where  BRCD='" + BRCD + "'AND PRDCODE='" + PRDCODE + "'AND ACCTNO='" + ACCTNO + "' and ID='" + id + "' and stage not in(1003,1004)";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

    public int AuthoriseData(string BRCD, string PRDCODE, string ACCTNO, string MID, string id)
    {
        try
        {
            sql = "update AVS_LNSECURITY set stage=1003,MID='" + MID + "' where BRCD='" + BRCD + "'AND PRDCODE='" + PRDCODE + "'AND ACCTNO='" + ACCTNO + "'and ID='" + id + "' and stage<>1003 ";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
        ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string GetData(string FL, string code)
  {
        try
        {
            if (FL == "MCS")
            {
                sql = "select Description from Lookupform1 where LNO='1082' and SRNO='" + code + "'";
            }
            else if (FL == "SCS")
            {
                sql = "select Description from Lookupform1 where LNO='1083' and SRNO='" + code + "'";
            }
         
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
          }
        return Result;
  }

    public DataTable GetInfo(string BRCD,string id, string PRDCODE, string ACCTNO)
    {
          DataTable DT = new DataTable();
        try
        {
            sql = " select S.BRCD, S.PRDCODE,S. ACCTNO, S.SRNO, S.PRIMARYCO, CONVERT(VARCHAR(11),S.REFAC_DATE,103) AS REFAC_DATE ,S. MAINCLASS,L1.DESCRIPTION AS Mname,S. SUBCLASS,L2.DESCRIPTION AS SName,S. CBSSECNO, S.REFERENCENO," +
           "CONVERT(VARCHAR(11),S.SECURITYDATE,103) AS SECURITYDATE,CONVERT(VARCHAR(11),S.LIMITEXPDATE,103) AS  LIMITEXPDATE, S.VALUE,S. MARGIN	,S. MKTVALUE,CONVERT(VARCHAR(11),S.LASTDPDATE,103) AS LASTDPDATE,S. DESCP,S. MODEOFCHARGE,S. DEPECATEDVAL, S.TYPEOFCHARGE,L4.DESCRIPTION AS TName," +
           "S. PERDEP, S.SECSTATUS,L5.DESCRIPTION AS Secname, S.SECDATE,S. VALUATIONDATE,CONVERT(VARCHAR(11),S.RELEASEDATE,103) AS  RELEASEDATE, S.STAGE from AVS_LNSECURITY S " +
              "left join LOOKUPFORM1 L1 ON S.MAINCLASS=L1.SRNO AND  L1.LNO='1082'" +
            " left join LOOKUPFORM1 L2 ON S.SUBCLASS=L2.SRNO AND L2.LNO='1083'" +
          "left join LOOKUPFORM1 L4 ON S.TYPEOFCHARGE=L4.SRNO AND L4.LNO='1084'" +
             "left join LOOKUPFORM1 L5 ON S.SECSTATUS=L5.SRNO AND L5.LNO='1085'" +
             "where S.BRCD='" + BRCD + "' and S.ID='" + id + "' and  S.PRDCODE='" + PRDCODE + "' and S.ACCTNO='" + ACCTNO + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); 
        }
        return DT;
    }
    public int BindGrid(GridView Gview, string brcd, string EDate, string PT, string AC)
    {
        try
        {
            if (PT == "" && AC == "")
            {
                sql = "Select Id,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS_LNSECURITY  L INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                        " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO  Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in(1004) and L.Brcd='" + brcd + "'and L.SYSTEMDATE='" + conn.ConvertDate(EDate) + "' order by PRDCODE, ACCTNO ";

               
            }
            else
            {
                sql = "Select Id,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS_LNSECURITY  L INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                      " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO  Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in(1004) and L.Brcd='" + brcd + "' and L.PRDCODE='" + PT + "' and L.ACCTNO='" + AC + "' order by PRDCODE, ACCTNO ";
            }
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable GetSubmitData(string FL)
    {
        try
        {
            if (FL == "TOC")
            {
                sql = "select SRNO, Description from Lookupform1 where LNO='1084' ";
                DT = conn.GetDatatable(sql);
            }
             if (FL == "SECS")
            {
                sql = "select SRNO, Description from Lookupform1 where LNO='1085' ";
                DT = conn.GetDatatable(sql);
            }
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
            sql = "SELECT STAGE FROM AVS_LNSECURITY WHERE BRCD='" + BRCD + "' AND PRDCODE='" + Prdcode + "' and ACCTNO='" + ACC + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string GetSrno(string brcd, string Prdcode, string ACC,string type)
    {
        try
        {
            sql = "select (isnull(max(srno),0))+1 srno from AVS_LNSECURITY where BRCD='" + brcd + "' AND PRDCODE='" + Prdcode + "' and ACCTNO='" + ACC + "' and PRIMARYCO='" + type + "' and  STAGE<>'1004'";
            srno = conn.sExecuteScalar(sql);
           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return srno;
    }

    public string GetCustNo(string brcd, string Prdcode, string ACC)//Pranali Pawar /14/07/2018//for Gridview
    {
        try
        {
            sql = "select CUSTNO from avs_acc where BRCD='" + brcd + "' AND SUBGLCODE='" + Prdcode + "' and ACCNO='" + ACC + "'";
            srno = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return srno;
    }
    
    public DataTable RptNOCPermit(string BRCD, string PRDCD, string ACCNO, string SRNO)//Pranali Pawar /11/07/2018//N.O.C Certificate
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0173 @brcd='" + BRCD + "',@prdcd='" + PRDCD + "',@accno='" + ACCNO + "',@srno='" +SRNO+"'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}