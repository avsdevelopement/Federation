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
/// Summary description for ClsLoanBasic
/// </summary>
public class ClsLoanBasic
{
    DbConnection conn = new DbConnection();
    string sql;
    DataTable DT;
    int result;
    string Result;
    string  Period;
	public ClsLoanBasic()
	{
		
	}

    public int InsertData(string BRCD, string PRD, string ACCNT, string CATNAME, string BRWTYPE, string INDTYPE, string INDSUBTYPE, string PURPCODE, string SUBPURPCODE, string PRIORITY,
        string CATGRY1, string SUBPRIORTY, string WEAKRSEC, string CATGRY2, string SUBWKR, string LOAN, string HEALTH, string SECURED, string SEC, string DIRCT, string EXEDATE, string RESNO,
        string Stage,string MID,string SYSTEMDATE)
    {
        try
        {
            sql = "insert into AVS_LNBASIC(BRCD,PRDCODE,ACCTNO,CATCODE,BRWTYPE,INDTYPE,INDSUBTYPE,PRPCODE,SUBPRPCODE,PRIORITY,CATEGORY1,SUBPRIORITY,WEAKERSEC,CATEGORY2,SUBWEAKERSEC, " +
            " LOANTRM,HEALTHCODE,SECURED,SEC58,DIRCT_INDIRCT,EXEDATE,RESLTNNO,STAGE,MID,SYSTEMDATE)VALUES('" + BRCD + "','" + PRD + "','" + ACCNT + "','" + CATNAME + "','" + BRWTYPE + "','" + INDTYPE + "','" + INDSUBTYPE + "','" + PURPCODE + "',  " +
          "'" + SUBPURPCODE + "','" + PRIORITY + "','" + CATGRY1 + "','" + SUBPRIORTY + "','" + WEAKRSEC + "', '" + CATGRY2 + "','" + SUBWKR + "','" + LOAN + "','" + HEALTH + "','" + SECURED + "', "+
          "'" + SEC + "','" + DIRCT + "','" + conn.ConvertDate(EXEDATE).ToString() + "','" + RESNO + "','" + Stage + "','" + MID + "','" + conn.ConvertDate(SYSTEMDATE).ToString() + "')";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

    public string GetData(string FL, string code)
        {
        try
        {
            if (FL == "CAT")
            {
                sql = "select Description from Lookupform1 where LNO='1070' and SRNO='" + code + "'";
             }
            else if (FL == "SPRIO")
            {
                sql = "select Description from Lookupform1 where LNO='1080' and SRNO='" + code + "'";
            }
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
          }
        return Result;
       }

    public int ModifyData(string BRCD, string PRD, string ACCNT, string CATNAME, string BRWTYPE, string INDTYPE, string INDSUBTYPE, string PURPCODE, string SUBPURPCODE, string PRIORITY,
        string CATGRY1, string SUBPRIORTY, string WEAKRSEC, string CATGRY2, string SUBWKR, string LOAN, string HEALTH, string SECURED, string SEC,  string DIRCT, string EXEDATE, string RESNO,
        string MID, string SYSTEMDATE, string id)
    {
        try
        {
            sql = "Update AVS_LNBASIC set STAGE=1002 ,BRCD='" + BRCD + "',PRDCODE='" + PRD + "',ACCTNO='" + ACCNT + "',CATCODE='" + CATNAME + "',BRWTYPE='" + BRWTYPE + "',INDTYPE='" + INDTYPE + "',"+
                "INDSUBTYPE='" + INDSUBTYPE + "',PRPCODE='" + PURPCODE + "',SUBPRPCODE='" + SUBPURPCODE + "',PRIORITY='" + PRIORITY + "',CATEGORY1='" + CATGRY1 + "',SUBPRIORITY='" + SUBPRIORTY + "',"+
                "WEAKERSEC='" + WEAKRSEC + "',CATEGORY2='" + CATGRY2 + "',SUBWEAKERSEC='" + SUBWKR + "',LOANTRM='" + LOAN + "',HEALTHCODE='" + HEALTH + "',SECURED='" + SECURED + "',SEC58='" + SEC + "',"+
                "DIRCT_INDIRCT='" + DIRCT + "',EXEDATE='" + conn.ConvertDate(EXEDATE).ToString() + "', RESLTNNO='" + RESNO + "',MID='" + MID + "',SYSTEMDATE='" + conn.ConvertDate(SYSTEMDATE).ToString() + "' "+
                " where  BRCD='" + BRCD + "'AND PRDCODE='" + PRD + "'AND ACCTNO='" + ACCNT + "' and ID='" + id + "'";
           result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

    public int DeleteData(string BRCD, string PRD, string ACCNT, string MID, string id)

    {
        try
        {
            sql = "update AVS_LNBASIC set stage=1004,MID='" + MID + "' where  BRCD='" + BRCD + "'AND PRDCODE='" + PRD + "'AND ACCTNO='" + ACCNT + "' and ID='" + id + "' and stage not in(1003,1004)";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
        }

    public int AuthoriseData(string BRCD, string PRD, string ACCNT, string MID, string id)
    {
         try 
	            {
                    sql = "update AVS_LNBASIC set stage=1003,MID='" + MID + "' where BRCD='" + BRCD + "'AND PRDCODE='" + PRD + "'AND ACCTNO='" + ACCNT + "' and ID='" + id + "' and stage<>1003";
                          result = conn.sExecuteQuery(sql);
                        return result;
	            }
	            catch (Exception Ex)
	            {
		
		            ExceptionLogging.SendErrorToText(Ex);
         }
        return result;
    }

    public DataTable GetInfo(string BRCD,string id,string PRD, string ACCNT )
    {
          DataTable DT = new DataTable();
        try
        {
           sql="Select L.BRCD,L.PRDCODE,L.ACCTNO,L.CATCODE,L1.DESCRIPTION AS CATNAME,L.BRWTYPE,L2.DESCRIPTION AS BRWNAME,L.INDTYPE,L3.DESCRIPTION AS INDNAME,L.INDSUBTYPE,L4.DESCRIPTION AS ISUBNAME,L.PRPCODE,L5.DESCRIPTION AS PPNAME,L.SUBPRPCODE,L6.DESCRIPTION AS SPPNAME,L.PRIORITY,L.CATEGORY1,L7.DESCRIPTION AS CATNAME1,L.SUBPRIORITY,L8.DESCRIPTION AS SUBPNAME,"+
                " L.WEAKERSEC,L.CATEGORY2,L9.DESCRIPTION AS CATNAME2,L.SUBWEAKERSEC,L10.DESCRIPTION AS SBWEAKERNM,L.LOANTRM,L11.DESCRIPTION AS LNTRM,L.HEALTHCODE,L12.DESCRIPTION AS HLNAME,L.SECURED,L.SEC58,L.DIRCT_INDIRCT,CONVERT(VARCHAR(11), L.EXEDATE,103) AS EXEDATE, L.RESLTNNO from AVS_LNBASIC  L" +
                " left join LOOKUPFORM1 L1 ON L.CATCODE=L1.SRNO AND  L1.LNO='1070'"+
                " left join LOOKUPFORM1 L2 ON L.BRWTYPE=L2.SRNO AND L2.LNO='1071'" +
                " left join LOOKUPFORM1 L3 ON L.INDTYPE=L3.SRNO  AND L3.LNO='1072'" +
                " left join LOOKUPFORM1 L4 ON L.INDSUBTYPE=L4.SRNO AND L4.LNO='1073'" +
                " left join LOOKUPFORM1 L5 ON L.PRPCODE=L5.SRNO AND L5.LNO='1074'" +
                " left join LOOKUPFORM1 L6 ON L.SUBPRPCODE=L6.SRNO AND L6.LNO='1075'" +
                " left join LOOKUPFORM1 L7 ON L.CATEGORY1=L7.SRNO AND L7.LNO='1079'" +
                " left join LOOKUPFORM1 L8 ON L.SUBPRIORITY=L8.SRNO  AND L8.LNO='1080'" +
                " left join LOOKUPFORM1 L9 ON L.CATEGORY2=CONVERT(VARCHAR(30),L9.SRNO) AND L9.LNO='1081'" +
                " left join LOOKUPFORM1 L10 ON L.SUBWEAKERSEC=L10.SRNO AND L10.LNO='1078'" +
                " left join LOOKUPFORM1 L11 ON L.LOANTRM=L11.SRNO AND L11.LNO='1086'" +
                " left join LOOKUPFORM1 L12 ON L.HEALTHCODE=L12.SRNO  AND L12.LNO='1087'" +
                " where L.BRCD='"+BRCD+"'and L.ID='"+id+"' and L.PRDCODE='"+PRD+"' and L.ACCTNO='"+ACCNT+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex )
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
                sql = "Select Id,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS_LNBASIC  L INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                        " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in (1004) and L.Brcd='" + brcd + "' and L.SYSTEMDATE='" + conn.ConvertDate(EDate) + "' order by PRDCODE, ACCTNO";
            }
            else
            {
                sql = "Select Id,PRDCODE,ACCTNO,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName  from AVS_LNBASIC  L INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCTNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " +
                       " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO  Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  where L.stage not in (1004) and L.Brcd='" + brcd + "' and L.PRDCODE='" + PT + "' and L.ACCTNO='" + AC + "' order by PRDCODE, ACCTNO";

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
            if (FL == "CAT")
            {
                sql = "select SRNO, Description from Lookupform1 where LNO='1070' ";
                DT = conn.GetDatatable(sql);
            }
            else  if (FL == "SPRIO")
            {
                sql = "select SRNO, Description from Lookupform1 where LNO='1080' ";
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
            sql = "SELECT STAGE FROM AVS_LNBASIC WHERE BRCD='" + BRCD + "' AND PRDCODE='" + Prdcode + "' and ACCTNO='" + ACC + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string GetPeriod(string BRCD, string Prdcode, string ACC)
    {
        try
        {
            sql = "select Period from LOANINFO where BRCD='"+BRCD+"' and LOANGLCODE='"+Prdcode+"' and CUSTACCNO='"+ACC+"' and   LMSTATUS=1";
            Period = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
             ExceptionLogging.SendErrorToText(Ex);;
        }
        return Period;
    }
  }
