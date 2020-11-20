using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Datecall
/// </summary>
public class Datecall
{
    string sql;
    int nom1;
    string AccT;
    

    DbConnection conn = new DbConnection();
    public string AccNodisplay(string BRCD, string PRDCODE, string ACCNO)
    {
        string val = "";
        try
        {
            sql = "select convert(nvarchar(10),LASTINTDATE,103) from LOANINFO where loanglcode='"+PRDCODE+"' and CUSTACCNO='"+ACCNO+"' and BRCD='"+BRCD+"' and LMSTATUS=1";
           //sql = "select convert(varchar(10),LASTINTDT,103)  from LOANINFO where  brcd='" + BRCD + "' and LoanGLCODE='" + PRDCODE + "' and custACCNO='" + ACCNO + "'and LMSTATUS='1'";
            val = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return val;
    }

    public string GetAccTypeGL(string AccT, string BRCD)
    {
        
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
   
    public int nomiupdate1(string BRCD,string PREV_INTDT,string LASTINT, string PRDCODE, string ACCNO)
    {
        try
        {

            sql = "update LOANINFO set PREV_INTDT='" + conn.ConvertDate(LASTINT).ToString() + "' ,LASTINTDATE='" + conn.ConvertDate(PREV_INTDT).ToString() + "' where   brcd='" + BRCD + "' and LoanGLCODE='" + PRDCODE + "' and custACCNO='" + ACCNO + "'and LMSTATUS='1'";
         
            nom1 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom1;

    }


   


    
    
}