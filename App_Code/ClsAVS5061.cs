using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

/// <summary>
/// Summary description for ClsAVS5061
/// </summary>
public class ClsAVS5061
{

    DbConnection conn = new DbConnection();
    ClsAuthorized AZ = new ClsAuthorized();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    DataTable DTTEST = new DataTable();
    ClsOpenClose OC = new ClsOpenClose();
    string sql = "", sResult = "";
    int Result;

	public ClsAVS5061()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetAgentName(string AGNO, string BRCD)
    {
        try
        {
            //sql = "select GLNAME+'_'+glcode from GLMAST where SUBGLCODE='" + AGNO + "' AND GLCODE='' AND BRCD='" + BRCD + "'";
            // sql = "SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), PLACCNO) FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (2,1) AND BRCD='" + BRCD + "'";
            // CHANGED and Above Commented FOR NULL EXCEPTION WHEN AGENT NUMBER IS SEARCHED --Abhishek

            sql = "SELECT (CASE WHEN cOUNT(A)=0 THEN '0' " +
                " ELSE (SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), isnull(PLACCNO,0)) AS A " +
                " FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (5) AND BRCD='" + BRCD + "')  END) " +
                " FROM (SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), isnull(PLACCNO,0)) AS A " +
                " FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (5) AND BRCD='" + BRCD + "') AS B ";
            AGNO = conn.sExecuteScalar(sql);
            return AGNO;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return AGNO;
    }
    public string GetAgentName_IR(string AGNO, string BRCD)
    {
        try
        {
            //sql = "select GLNAME+'_'+glcode from GLMAST where SUBGLCODE='" + AGNO + "' AND GLCODE='' AND BRCD='" + BRCD + "'";
            // sql = "SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), PLACCNO) FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (2,1) AND BRCD='" + BRCD + "'";
            // CHANGED and Above Commented FOR NULL EXCEPTION WHEN AGENT NUMBER IS SEARCHED --Abhishek

            sql = "SELECT (CASE WHEN cOUNT(A)=0 THEN '0' " +
                " ELSE (SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), isnull(IR,0))+'_'+CONVERT(varchar(10), isnull(PLACCNO,0)) AS A " +
                " FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (5) AND BRCD='" + BRCD + "')  END) " +
                " FROM (SELECT GLNAME+'_'+CONVERT(varchar(10), GLCODE)+'_'+CONVERT(varchar(10), isnull(IR,0))+'_'+CONVERT(varchar(10), isnull(PLACCNO,0)) AS A  " +
                " FROM GLMAST WHERE SUBGLCODE='" + AGNO + "' AND GLCODE in (5) AND BRCD='" + BRCD + "') AS B ";
            AGNO = conn.sExecuteScalar(sql);
            return AGNO;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return AGNO;
    }


    public DataTable GetInfo(string AGNO, string BRCD, string EDT)
    {
        double BALANCE = 0;
        string[] DTR;
        string DTR1 = "";
        int ACCNO;
        try
        {
            
            sql = "SELECT Distinct RIGHT('00000'+ CONVERT(VARCHAR,Accno),6) AS ACCNO, " +
                "  LEFT (CONVERT(VARCHAR,M.CUSTNAME)+'        ',16)AS CUSTNAME " +
                "  FROM AVS_ACC A  INNER JOIN MASTER M ON M.CUSTNO=A.CUSTNO AND A.BRCD=M.Brcd  " +
                " WHERE A.SUBGLCODE='" + AGNO + "'  AND A.GLCODE='5' AND A.BRCD='" + BRCD + "' AND A.ACC_STATUS=1";
            DataTable DTACC = new DataTable();
            DTACC = conn.GetDatatable(sql);
            DT = new System.Data.DataTable();
            DT.Columns.Add("Accno");
            DT.Columns.Add("Blank1");
            DT.Columns.Add("ACCNAME");
            DT.Columns.Add("BALANCE");
            DT.Columns.Add("Entrydate");
            DT.Columns.Add("Blank2");
            if (DTACC.Rows.Count > 0)
            {
                string[] TD = EDT.Split('/');

                for (int k = 0; k <= DTACC.Rows.Count - 1; k++)
                {
                    BALANCE = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), AGNO, DTACC.Rows[k]["ACCNO"].ToString(), BRCD, EDT, "2");

                    string BAL = "";
                    BAL = BALANCE.ToString();
                    if (BAL.Length == 1)
                    {
                        BAL = "00000" + BAL;
                    }
                    else if (BAL.Length == 2)
                    {
                        BAL = "0000" + BAL;
                    }
                    else if (BAL.Length == 3)
                    {
                        BAL = "000" + BAL;
                    }
                    else if (BAL.Length == 4)
                    {
                        BAL = "00" + BAL;
                    }
                    else if (BAL.Length == 5)
                    {
                        BAL = "0" + BAL;
                    }
                    DataRow DR = DT.NewRow();
                    DR["Accno"] = DTACC.Rows[k]["ACCNO"].ToString();
                    DR["Blank1"] = "000000";
                    DR["ACCNAME"] = DTACC.Rows[k]["CUSTNAME"].ToString();
                    DR["BALANCE"] = BAL;
                    DTR = EDT.ToString().Split('/');// DateTime.Parse(EDT).ToString("dd.MM.yy");//Convert.ToDateTime(EDT).ToString("dd.MM.yy");
                    DTR1 = DTR[0].ToString() + "." + DTR[1].ToString() + "." + DTR[2].ToString();

                    DR["EntryDate"] = DTR1.ToString();
                    DR["Blank2"] = "000000";
                    DT.Rows.Add(DR);
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetAccInfo(string PT, string BRCD, string ACCNO)
    {
        try
        {
            sql = "SELECT A.ACCNO ,A.Acc_Status,(CASE WHEN A.Acc_Status=3 THEN 'CLOSED' ELSE 'OPEN' END) Accsts ,M.CUSTNAME,Lno.Name,A.ACC_TYPE,A.CUSTNO,A.D_AMOUNT FROM AVS_ACC A" +
                " INNER JOIN MASTER M ON M.CUSTNO=A.CUSTNO " +
                " LEFT JOIN (SELECT DESCRIPTION NAME,SRNO ID FROM Lookupform1 WHERE LNO='1017') LNO ON LNO.ID=A.ACC_TYPE" +
                " WHERE A.SUBGLCODE='" + PT + "' AND A.GLCODE='5' AND A.BRCD='" + BRCD + "' AND A.Accno='" + ACCNO + "' and A.ACC_STATUS<>3";
            DT = new System.Data.DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetDDSInfo(string SUBGLCODE,string BRCD)
    {
        try
        {
            sql = "SELECT * FROM AVS_DDS order by effect_dt"; //BRCD NOT ADDED BECAUSE TABLE do not CONTAIN BRCD --Abhishek
            DT = new System.Data.DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public string GetSetNo(string SRNO)
    {
        try
        {   //Abhishek
            //sql = "SELECT ISNULL(MAX(LASTNO),0)+1 FROM AVS1000 WHERE SRNO='" + SRNO + "'";
            sql = " Update avs1000 set Lastno=(SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE SRNO='46') where srno='46' " +
                         " SELECT MAX(LASTNO) FROM AVS1000 WHERE SRNO='46'";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return SRNO;
    }
    public int UpdateSetNo(string SRNO, string LNO)
    {
        try
        {
            sql = "UPDATE AVS1000 SET LASTNO='" + Convert.ToInt32(LNO) + "' WHERE SRNO='" + SRNO + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }


    public int UpdateAcc(string gl, string sgl, string ac, string brcd, string EDT)
    {
        int Result = 0;
        string sql2 = "";
        try
        {
            sql = "update avs_acc set acc_status=3,CLOSINGDATE='" + conn.ConvertDate(EDT) + "' where glcode='" + gl + "' and subglcode='" + sgl + "' and accno='" + ac + "' and brcd='" + brcd + "'";
            sql2 = "update DEPOSITINFO SET LMSTATUS=99 WHERE DEPOSITGLCODE=" + sgl + " AND CUSTACCNO=" + ac + " and brcd='" + brcd + "'";

            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                Result = conn.sExecuteQuery(sql2);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int UpdatePCRX(string ACNO, string BRCD, string AMT, string EDT, string AG)
    {
        try
        {
            sql = "UPDATE AFTEKR SET TRANSAMT='" + AMT + "' WHERE  ACNO='" + ACNO + "' AND BRCD='" + BRCD + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND AGENTCODE='" + AG + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int InsertData(string AG, string BRCD, string MID, string EDT)
    {
        try
        {
            sql = "insert into AFTEKR(ENTRYDATE,TRANSAMT,AGENTCODE,BRCD,ACNO,STAGE,MID,SYSTEMDATE)" +
                "select '" + conn.ConvertDate(EDT) + "','0',SUBGLCODE,BRCD,ACCNO,'1001','" + MID + "',GETDATE() from AVS_ACC where GLCODE=2 and SUBGLCODE in ('" + AG + "') and BRCD='" + BRCD + "' and ACC_STATUS in (1,4)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int InsertDataAkyt(string AG, string BRCD, string MID, string EDT)
    {
        try
        {
            sql = "insert into AFTEKR(ENTRYDATE,TRANSAMT,AGENTCODE,BRCD,ACNO,STAGE,MID,SYSTEMDATE)" +
                "select '" + conn.ConvertDate(EDT) + "','0',SUBGLCODE,BRCD,ACCNO,'1001','" + MID + "',GETDATE() from AVS_ACC " +
                " where GLCODE in (2,1,5) and SUBGLCODE in ('" + AG + "',303,1) and BRCD='" + BRCD + "' and ACC_STATUS in (1,4)"; // Hardcoded for as per reiurement
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateAFTData(string AG, string EDT, string PDT, string AC, string BRCD, string AMT, string TDT, string FL)
    {
        try
        {
            PDT = PDT.Replace(" 12:00:00", "");
            PDT = PDT.Replace(" 12:00:00 AM", "");

            if (FL == "ADDACC")
            {
                //  sql = "update AFTEKR set TRANSAMT='" + AMT + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
                //sql = "update AFTEKR set TRANSAMT='" + AMT + "',POSTINGDATE='" + conn.ConvertDate(PDT) + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
                sql = "update AFTEKR set TRANSAMT='" + AMT + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            }
            else if (FL == "CHGAMT")
            {
                //sql = "update AFTEKR set TRANSAMT=isnull(TRANSAMT,0)+'" + AMT + "',POSTINGDATE='" + conn.ConvertDate(PDT) + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
                sql = "update AFTEKR set TRANSAMT=isnull(TRANSAMT,0)+'" + AMT + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            }

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int UpdateAFTDataMOB(string AG, string EDT, string PDT, string AC, string BRCD, string AMT, string TDT, string MID)
    {
        try
        {
            //  sql = "update AFTEKR set TRANSAMT='" + AMT + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            //sql = "update AFTEKR set TRANSAMT='" + AMT + "',POSTINGDATE='" + conn.ConvertDate(PDT) + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "',VID='" + MID + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            sql = "update AFTEKR set TRANSAMT='" + AMT + "',TRANSACTIONDATE='" + conn.ConvertDate(EDT) + "',VID='" + MID + "' where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetAgentCol(string AG, string BRCD, string EDT)
    {
        try
        {
            sql = "Select sum(TRANSAMT) from  AFTEKR where agentcode='" + AG + "' and brcd='" + BRCD + "' and ENTRYDATE= '" + conn.ConvertDate(EDT) + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetAgentColAkyt(string AG, string BRCD, string EDT)
    {
        try
        {
            sql = "Select sum(TRANSAMT) from  AFTEKR where agentcode in ('" + AG + "','303','1') and brcd='" + BRCD + "' and ENTRYDATE= '" + conn.ConvertDate(EDT) + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }


    public string GetAgentColMob(string AG, string BRCD)
    {
        try
        {
            sql = "Select sum(TRANSAMT) from  AFTEKR where agentcode='" + AG + "' and brcd='" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetAmtInfo(string AG, string AC, string EDT, string BRCD)
    {
        try
        {
            sql = "SELECT TRANSAMT FROM AFTEKR where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string GetAmtInfoMOb(string AG, string AC, string EDT, string BRCD)
    {
        try
        {
            sql = "SELECT TRANSAMT FROM AFTEKR where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and AGENTCODE='" + AG.Trim() + "' AND  BRCD ='" + BRCD.Trim() + "' AND ACNO='" + AC.Trim() + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public double GetInterest(string BRCD, string GLCD, string SGLCD, string ACCNO, string TDATE)
    {
        double IntAmt = 0;
        try
        {
            sql = "EXEC Isp_AVS0095 'CAL','" + BRCD + "','" + GLCD + "','" + SGLCD + "','" + ACCNO + "','" + conn.ConvertDate(TDATE) + "'";
            IntAmt = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntAmt;
    }
    public string GetStatus(string BRCD, string SUBGL, string Accno)
    {
        try
        {
            sql = "Select Acc_Status from avs_acc where Subglcode='" + SUBGL + "' and Accno='" + Accno + "' and brcd='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetSubgLc(string BRCD)
    {
        try
        {
            sql = "select subglcode from glmast where glcode='6' and brcd='" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string CheckACValid(string BRCD, string ACCNO, string SUBGL)
    {
        try
        {
            sql = "SELECT count(*) FROM AVS_ACC WHERE SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "' AND Convert(int,ACCNO)='" + ACCNO + "' AND ACC_STATUS in (1,4)";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string Getaccno(string BRCD, string AT)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    //For Mobile Upload Process --Abhishek
    public DataTable GetMobileUpload(string BRCD, string SBGL, string EDT)
    {
        try
        {
            sql = "SELECT ACCNO,AMOUNT, FROM ALLVCR WHERE BRCD='" + BRCD + "' AND PMTMODE='MOB-COL' AND SUBGLCODE='" + SBGL + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetAftekrStatus(string BRCD, string SBGL)
    {
        try
        {
            sql = "Select count(*) from  AFTEKR where agentcode='" + SBGL + "' and brcd='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int DeleteAftekr(string BRCD, string SBGL)
    {
        try
        {
            sql = "Delete from AFTEKR where agentcode='" + SBGL + "' and brcd='" + BRCD + "'";
            Result = conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string BalajiParameter(string BRCD)
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='BalajiM' AND BRCD=" + BRCD + "";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int InsertData_Mob(string EDT, string BRCD, string SBGL)
    {
        try
        {
            sql = "insert into AFTEKR(ENTRYDATE,POSTINGDATE,TRANSAMT,AGENTCODE,BRCD,ACNO,STAGE,MID,SYSTEMDATE) " +
                " select Convert(varchar(11),ENTRYDATE,120)ENTRYDATE,Convert(varchar(11),POSTINGDATE,120)POSTINGDATE,CREDIT,SUBGLCODE,BRCD,ACCNO,'1001','" + BRCD + "',GETDATE() from ALLVCR " +
                " where GLCODE=2 and SUBGLCODE='" + SBGL + "' and BRCD='" + BRCD + "' AND STAGE IN (1001,1002) and pmtmode='MOB-COL'"; //ASHOK MISAL AND STAGE NOT IN (1005,1003)  CHANGES

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetAccMaster(string BRCD, string SBGL, string EDT, string FL)
    {
        try
        {
            if (FL == "ACC")
            {
                sql = "SELECT GLCODE,SUBGLCODE,ACCNO,OPENINGDATE,ACC_STATUS,BRCD,LASTINTDT FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "' AND ACC_STATUS=1";
            }
            else if (FL == "AFTEKR")
            {
                sql = "SELECT ENTRYDATE,TRANSAMT,AGENTCODE,BRCD,ACNO,STAGE,MID,SYSTEMDATE FROM AFTEKR WHERE BRCD='" + BRCD + "' AND AGENTCODE='" + SBGL + "'";
            }
            else if (FL == "INV")
            {
                sql = "Select ROW_NUMBER () OVER (PARTITION BY SUBGLCODE order by ACCNO) as ID ,'' as NAME,ACCNO,'' as AMOUNT,'A/C not created' REMARK From allvcr Where Brcd='" + BRCD + "' And Subglcode='" + SBGL + "' " +
                    "   and pmtmode='MOB-COL' And Entrydate='" + conn.ConvertDate(EDT) + "' And Accno Not in (Select Accno From avs_acc Where Brcd='" + BRCD + "' And Subglcode='" + SBGL + "')";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int UpdateALLVCRStage(string BRCD, string SBGL)
    {
        try
        {
            sql = "UPDATE ALLVCR SET STAGE='1003' WHERE PMTMODE='MOB-COL' AND BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    //END PROCESS

    public int BindCalcu(GridView GD, string BRCD, string GLCD, string SGLCD, string ACCNO, string TDATE)
    {
        try
        {
            sql = "EXEC Isp_AVS0095 'GRID','" + BRCD + "','" + GLCD + "','" + SGLCD + "','" + ACCNO + "','" + conn.ConvertDate(TDATE) + "'";
            Result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable BindCalcuReport(string BRCD, string GLCD, string SGLCD, string ACCNO, string TDATE)
    {
        try
        {
            sql = "EXEC Isp_AVS0095 'GRID','" + BRCD + "','" + GLCD + "','" + SGLCD + "','" + ACCNO + "','" + conn.ConvertDate(TDATE) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetWritePCTX(string BRCD, string SUBGL, string GLCODE, string EDT, string FL, string SFL)
    {
        try
        {
            string SFL_Valid = "";
            if (SFL == "ZB_Y")
            {
                SFL_Valid = ",@SFLAG='" + SFL + "'";
            }
            else
            {
                SFL_Valid = "";
            }
            if (FL == "DDSCT")
            {
                sql = "EXEC SP_AGENT_DDSCT @FLAG='" + FL + "',@GLCODE='" + GLCODE + "',@SUBGLCODE='" + SUBGL + "',@BRCD='" + BRCD + "',@ASONDATE='" + conn.ConvertDate(EDT) + "'" + SFL_Valid + "";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetPostingData(string AGCD, string BRCD, string EDT, string FL)
    {
        try
        {
            if (FL != "MOB")
            {
                sql = "Select ACNO, TRANSAMT,  TRANSACTIONDATE TRANSACTIONDATE,convert(nvarchar(10),POSTINGDATE,121) as POSTINGDATE from  AFTEKR where agentcode='" + AGCD + "' and brcd='" + BRCD + "' and ENTRYDATE='" + conn.ConvertDate(EDT) + "' and TRANSAMT>0";
            }
            else
            {
                sql = "Select ACNO, TRANSAMT,  TRANSACTIONDATE TRANSACTIONDATE,convert(nvarchar(10),POSTINGDATE,121) as POSTINGDATE from  AFTEKR where agentcode='" + AGCD + "' and brcd='" + BRCD + "' and TRANSAMT>0";
            }
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetPostingDataAkyt(string AGCD, string BRCD, string EDT, string FL)
    {
        try
        {
            if (FL != "MOB")
            {
                sql = "Select B.GLCODE,A.AGENTCODE,A.ACNO,A.TRANSAMT,A.TRANSACTIONDATE,convert(nvarchar(10),A.POSTINGDATE,121) as POSTINGDATE from  AFTEKR A " +
                    " inner join GLMAST B			on A.Brcd=B.Brcd and A.AGENTCODE=B.SUBGLCODE " +
                    " where A.AGENTCODE in ('" + AGCD + "','303','1') " +
                    " and A.BRCD='" + BRCD + "'  " +
                    " and A.TRANSAMT>0 " +
                    " and A.ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
            }
            else
            {
                sql = "Select ACNO, TRANSAMT,  TRANSACTIONDATE TRANSACTIONDATE,convert(nvarchar(10),POSTINGDATE,121) as POSTINGDATE from  AFTEKR where agentcode in ('" + AGCD + "','303','1') and brcd='" + BRCD + "' and TRANSAMT>0";
            }
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public void InsertRecord(string Agentcode, string BRCD, string MID, string EDT, string Path)
    {
        try
        {
            sql = "Exec sp_InsertDataAFTEKR @Agent='" + Agentcode + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@EDT='" + EDT + "',@Path='" + Path + "'";
            conn.sExecuteQuery(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public DataTable GetData(string Agentcode, string BRCD)//amruta 18/04/2017
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select * from  AFTEKR where agentcode='" + Agentcode + "' and brcd='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return dt;
    }
    public void DeleteData(string Agentcode, string BRCD)//amruta 18/04/2017
    {
        try
        {
            sql = "Delete from AFTEKR where agentcode='" + Agentcode + "' and brcd='" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }
    public string GetINTAUTO()
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='INTAUTO'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetSBINTAUTO()
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='SBINTAUTO'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetParaModify()
    {
        try
        {
            sql = "select LISTVALUE from parameter where listfield='DB2PC'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetDDSCashDeduct()
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='COMMDEDCASH'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable getDetails(string brcd, string subgl, string accno)////Added by ankita on 19/06/2017 to display lien details
    {

        try
        {
            sql = "SELECT LOANGLCODE,LOANACCNO,LOANAMT FROM AVS_LIENMARKDETAILS WHERE DEPOSITGLCODE='" + subgl + "' AND DEPOSITACCNO='" + accno + "' AND BRCD='" + brcd + "' and status=4 and stage<>1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetLoanTotalAmount(string BrCode, string PrCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Exec RptAllLoanBalances '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int CloseLoanAcc(string BRCD, string SubglCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set LmStatus = 99, Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Stage = 1003, Vid = '" + Mid + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BRCD = '" + BRCD + "' AND LOANGLCODE = '" + SubglCode + "' AND CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Convert.ToInt32(Result) > 0)
            {
                sql = "Update Avs_Acc Set Acc_status = 3, Stage = 1003, LastIntDt = '" + conn.ConvertDate(EDate).ToString() + "', CLOSINGDATE = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + Mid + "' Where BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SubglCode + "' AND ACCNO = '" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetGSTPara()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='GSTYN'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public DataTable UpdateAvsACC(string AgentCode, string BRCD, string EDAT, string Flag)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "exec ISP_AVS0051 '" + AgentCode + "','" + BRCD + "','" + conn.ConvertDate(EDAT) + "','" + Flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public void DeleteAVSACC(string AgentCode, string BRCD, string BCode, string Flag)
    {
        try
        {
            if (Flag == "2")
            {
                sql = " delete from MASTER_" + BCode + " where agentCode='" + AgentCode + "' and BRCD='" + BRCD + "' ";
                conn.sExecuteQueryMob(sql);
            }
            else
            {
                sql = "Delete from AVS_ACC_" + BCode + " where Ref_Agent='" + AgentCode + "' and BRCD='" + BRCD + "'";
                conn.sExecuteQueryMob(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void InsertAvsACC(DataTable dt, string BCODE, string Flag)
    {
        try
        {
            if (Flag == "1")
            {
                using (SqlConnection con = new SqlConnection(conn.DBNameMob()))
                {
                    con.Open();
                    SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DBNameMob());
                    //Give your Destination table name
                    sqlBulk.DestinationTableName = "AVS_ACC_" + BCODE;
                    sqlBulk.WriteToServer(dt);
                    con.Close();
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(conn.DBNameMob()))
                {
                    con.Open();
                    SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DBNameMob());
                    //Give your Destination table name
                    sqlBulk.DestinationTableName = "Master_" + BCODE;
                    sqlBulk.WriteToServer(dt);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable getAllVCRData(string AgentCode, string BRCD, string BankCD, string EDAT, string Flag)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec ISP_AVS0052 '" + AgentCode + "','" + BRCD + "','" + BankCD + "','" + conn.ConvertDate(EDAT) + "','" + Flag + "'";
            dt = conn.GetDatatableMob(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public void InsertAllVCRData(DataTable dt)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(conn.DbName()))
            {
                con.Open();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DbName());
                //Give your Destination table name
                sqlBulk.DestinationTableName = "ALLVCR";
                sqlBulk.WriteToServer(dt);
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void InsertMTABLlE(DataTable dt, string Date)
    {
        try
        {
            string date = conn.ConvertDate(Date);
            string[] Date1 = date.Split('-');
            using (SqlConnection con = new SqlConnection(conn.DbName()))
            {
                con.Open();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DbName());
                //Give your Destination table name
                sqlBulk.DestinationTableName = "AVSM_" + Date1[0].ToString() + Date1[1].ToString();
                sqlBulk.WriteToServer(dt);
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public int DataOperateAFTEKR(string AGCD, string BRCD)
    {
        string Str = "";
        try
        {
            sql = "Select Case when Count(*)=0 then 'A' else 'B' end from  AFTEKR where agentcode in ('" + AGCD + "','303','1') and brcd='" + BRCD + "'";
            Str = conn.sExecuteScalar(sql);
            if (Str == "B")
            {
                sql = "Delete from AFTEKR where agentcode in ('" + AGCD + "','303','1') and brcd='" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
                Result = 0;// If data deleted the go further
            }
            else
            {
                Result = 0;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int BindContactakyt(GridView Gview, string AGCD, string BRCD)
    {
        int Result = 0;
        try
        {
            string sql = "SELECT Convert(varchar(11),ENTRYDATE,103)ENTRYDATE,Convert(varchar(11),POSTINGDATE,103)POSTINGDATE,AGENTCODE,BRCD,TRANSACTIONDATE,ACNO,TRANSAMT ,RECEIPTNO,NOOFRECEIPTPRINTED,TRANSTIME ,STAGE from AFTEKR  WHERE AGENTCODE in ('" + AGCD + "',303,1) AND STAGE <>'1004'  " +
            " AND BRCD='" + BRCD + "' AND TRANSAMT <>0 order by entrydate,acno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int InsertBatch(string GLCD, string SGLCD, string ACCNO, string ACNAME, string CT, string AMT, string TRX, string ACT, string PMT, string NR, string NR1, string BRCD, string MID, string EDT, string ST, string INSTNO, string INSTDT)
    {
        try
        {
            string INS = INSTNO.ToString() == "" ? "0" : INSTNO;
            string inDT = INSTDT.ToString() == "" ? "1900-01-01" : INSTDT;

            if (Convert.ToDouble(AMT) > 0)
            {
                sql = "INSERT INTO TEMPBATCHRD (GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,NR1,BRCD,MID,MID_DATE,SETNO,INSTRUMENTNO,INSTRUMENTDATE) " +
                    "VALUES('" + GLCD + "','" + SGLCD + "','" + ACCNO + "','" + ACNAME + "','" + CT + "','" + AMT + "','" + TRX + "','" + ACT + "','" + PMT + "','" + NR + "','" + NR1 + "','" + BRCD + "','" + MID + "','" + conn.ConvertDate(EDT) + "','" + ST + "','" + INS + "','" + conn.ConvertDate(INSTDT) + "')";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public string PostEntry(string MID, string SGLCD, string ACCNO, string EDT, string BRCD, string STT, string FL)
    {
        string SetNo = "", ST = "";
        string CR, DR;
        string REFNO = "";
        CR = DR = "";

        try
        {
            string PAYMAST = "";
            if (FL == "RDCL")
            {
                PAYMAST = "RDCLOSE";
            }
            else if (FL == "LINST")
            {
                PAYMAST = "LOANINSTALLMENT";
            }

            string REFERENCEID = "";
            REFERENCEID = BD.GetMaxRefid(BRCD, EDT, "REFID");
            REFERENCEID = (Convert.ToInt32(REFERENCEID) + 1).ToString(); //ABHISHEK

            sql = "SELECT ISNULL(MAX(LASTNO),0)+1 FROM AVS1000 WHERE SRNO='1' and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
            REFNO = conn.sExecuteScalar(sql);

            //Added by amol on 09/11/2017 as per ambika madam instruction (only when payment mode is cash)
            sql = "SELECT TOP 1 ACTIVITY FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "'  AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND SETNO='" + STT + "' And ACTIVITY = '3' ORDER BY ACTIVITY DESC";
            sResult = conn.sExecuteScalar(sql);

            if (sResult == "3")
            {
                //For Cash Receipt set
                DT = new DataTable();
                sql = "SELECT * FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "'  AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND SETNO='" + STT + "' And ACTIVITY = '3' And AMt > 0 ORDER BY ACTIVITY DESC";
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    ST = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();
                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        if (DT.Rows[K]["TRXTYPE"].ToString() == "1")
                        {
                            CR = DT.Rows[K]["AMT"].ToString();
                            DR = "0";
                        }
                        else
                        {
                            DR = DT.Rows[K]["AMT"].ToString();
                            CR = "0";
                        }
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), ST, "0", "01/01/1900", "0", "0", "1001", "01/01/1900", BRCD, MID, "0", "0", PAYMAST, DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), REFERENCEID, "0");
                    }
                }

                SetNo = ST;
                //For Cash Payment set
                DT = new DataTable();
                sql = "SELECT * FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "'  AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND SETNO='" + STT + "' And ACTIVITY = '4' And AMt > 0 ORDER BY ACTIVITY DESC";
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    ST = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();
                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        if (DT.Rows[K]["TRXTYPE"].ToString() == "1")
                        {
                            CR = DT.Rows[K]["AMT"].ToString();
                            DR = "0";
                        }
                        else
                        {
                            DR = DT.Rows[K]["AMT"].ToString();
                            CR = "0";
                        }
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), ST, "0", "01/01/1900", "0", "0", "1001", "01/01/1900", BRCD, MID, "0", "0", PAYMAST, DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), REFERENCEID, "0");
                    }
                }
                SetNo = SetNo + "_" + ST;
            }
            else
            {
                sql = "SELECT * FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "'  AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND SETNO='" + STT + "' ORDER BY ACTIVITY DESC";
                DataTable DT = new DataTable();
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    ST = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();
                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        if (DT.Rows[K]["TRXTYPE"].ToString() == "1")
                        {
                            CR = DT.Rows[K]["AMT"].ToString();
                            DR = "0";
                        }
                        else
                        {
                            DR = DT.Rows[K]["AMT"].ToString();
                            CR = "0";
                        }
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), ST, "0", "01/01/1900", "0", "0", "1001", "01/01/1900", BRCD, MID, "0", "0", PAYMAST, DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), REFERENCEID, "0");
                    }
                }
                SetNo = ST + "_0";
            }
            sql = "DELETE FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "'  AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND SETNO='" + STT + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SetNo;
    }
    public int GetInfoRD(GridView Gview, string MID, string SGLCD, string ACCNO, string EDT, string BRCD, string ST, string TP)
    {

        try
        {
            sql = "SELECT SETNO,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,(CASE WHEN TRXTYPE='1' THEN 'CR' ELSE 'DR' END ) TXRTYPE,ACTIVITY,PMT,NR,BRCD,MID,MID_DATE FROM TEMPBATCHRD WHERE MID='" + MID + "' AND BRCD='" + BRCD + "' AND SETNO='" + ST + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND PMT='" + TP + "'";
            Result = conn.sBindGrid(Gview, sql);
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return Result;
    }
    public int GetinfotableRD(GridView Gview, string smid, string sbrcd, string EDT, string paymst)
    {
        try
        {
            string TableName = "";
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10),isnull(a.SETNO,'0'))+'_'+ConVert(VarChar(10),isnull(A.Amount,'0'))+'_'+ConVert(VarChar(10),isnull(ACC.SUBGLCODE,'0'))+'_'+ConVert(VarChar(10),isnull(a.ACCNO,'0')) Dens, " +
                  "A.SETNO SETNO, ACC.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.Amount, A.PARTICULARS PARTICULARS,A.INSTRUMENTNO,A.INSTRUMENTDATE, UM.USERNAME MAKER " +
                  "From " + TableName + " A Left Join UserMaster UM ON UM.PERMISSIONNO=A.MID " +
                  "Left Join Avs_Acc ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE " +
                  "Left Join Master M ON M.CUSTNO=ACC.CUSTNO  AND M.BRCD=ACC.BRCD " +
                  "Where A.BRCD='" + sbrcd + "' AND A.STAGE = '1001' AND A.TrxType <> '1' AND A.ACTIVITY='4'  AND A.ENTRYDATE = '" + conn.ConvertDate(EDT).ToString() + "' and A.PAYMAST='" + paymst + "' " +
                  "Order By A.SETNO,A.SCROLLNO ";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetDDSInfo(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from avs_dds where paraid='" + id + "'";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }
    //For Getting OpeningDate ,Period and Closing for RD Clsoe -- ABHISHEK 15-03-2018
    public DataTable GetOpeningDate(string BRCD, string SUBGL, string ACCNO, string EDT)
    {
        try
        {
            sql = " SELECT CONVERT(VARCHAR(20),A.OPENINGDATE,103) OPENINGDATE,B.PERIOD AS PERIOD, " +
                    " CONVERT(VARCHAR(20),B.DUEDATE,103) CLOSINGDATE,CONVERT(VARCHAR(20),isnull(Case when B.LASTINTDATE<B.OPENINGDATE then B.OPENINGDATE else B. LASTINTDATE end,B.OPENINGDATE),103) LASTINTDATE " +
                    " FROM AVS_ACC			A " +
                    " left join DEPOSITINFO	B		on A.Brcd=B.BRCD and A.SUBGLCODE=B.DEPOSITGLCODE and A.ACCNO=B.CUSTACCNO " +
                    " WHERE A.SUBGLCODE='" + SUBGL + "' " +
                    " AND A.ACCNO='" + ACCNO + "' " +
                    " AND A.BRCD='" + BRCD + "' " +
                    " And A.ACC_STATUS<>'3'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}