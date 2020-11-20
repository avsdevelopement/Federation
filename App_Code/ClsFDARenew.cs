using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsFDARenew
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();

    public ClsFDARenew()
    {
    }

    public int CreateTable(string UserName, string BRCD)
    {
        try
        {
            sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + UserName + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "Drop Table " + UserName;
                Result = conn.sExecuteQuery(sql);
            }
            sql = "create table " + UserName + "(ID INT IDENTITY(1,1),GLCODE INT,SUBGLCODE INT,ACCNO INT,CUSTNO INT ,AMOUNT NUMERIC(18,2),PAMOUNT NUMERIC(18,2),TRXTYPE INT,ROI NUMERIC(5,2),FD INT,PERIOD CHAR(1),PNO INT,INSTAMT NUMERIC(18,2),DUEDATE DATETIME,MAMT NUMERIC(18,2),TSGLCD INT,TACCNO INT,STAGE INT,INTPAYOUT INT,RECNO VARCHAR(100),OPENDATE DATETIME,OPRTYPE Int,ACCTYPE int,ACTIVITY int,PMTMODE Varchar(100),FLAG Varchar(100))";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertTemp(string GL, string SGL, string AC, string AMT, string TRX,string CT , string TBName,string ACT=null,string PMTMD=null,string FL="YES")
    {
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='INSERTWR' ,@GLCODE='" + GL + "',@SUBGLCODE='" + SGL + "',@ACCNO='" + AC + "',@AMOUNT='" + AMT + "',@TRXTYPE='" + TRX + "',@TBNAME='" + TBName + "',@CUSTNO='" + CT + "',@ACTIVITY='" + ACT + "',@PMTMODE='" + PMTMD + "',@FL='" + FL + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }




    public string GetDUEDATE(string DPGL, string BRCD, string flag, string ACCNO, string CUSTNO, string RecSrno)
    {
        string RT = "";
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='" + flag + "',@SUBGLCODE='" + DPGL + "',@BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + ACCNO + "',@RecSrno='" + RecSrno + "'";
            RT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RT;
    }
     public int InsertTempMultiple(string FL,string BRCD,string EDT,string SETNO, string TBName)
    {
        try
        {
            sql = "Exec Isp_TDAMulti_Closure @Flag='" + FL + "',@Brcd='" + BRCD + "',@Edt='" + conn.ConvertDate(EDT) + "',@Setno='" + SETNO + "',@TBName='" + TBName + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
   
    public int InsertTempNew(string GL, string SGL, string AC, string AMT, string TRX, string CT, string TBName,string ROI,string PNAME,string PNO,string INSTAMT,string DUEDATE,string MAMT,string TSGLCD,string TACCNO,string INTPAYOUT,string OPENDATE="1990-01-01",string RECNO="0",string OPRTYPE="1",string ACCTYPE="1")
    {
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='INSERTFD' ,@GLCODE='" + GL + "',@SUBGLCODE='" + SGL + "',@ACCNO='" + AC + "',@AMOUNT='" + AMT + "',@TRXTYPE='" + TRX + "',@TBNAME='" + TBName + "',@CUSTNO='" + CT + "',@ROI='" + Convert.ToDouble(ROI) + "',@PNAME='" + PNAME + "',@PNO='" + PNO + "',@INSTAMT='" + Convert.ToDouble(INSTAMT) + "',@DUEDATE='" + conn.ConvertDate(DUEDATE) + "',@MAMT='" + Convert.ToDouble(MAMT) + "',@TSGLCD='" + TSGLCD + "',@TACCNO='" + TACCNO + "',@INTPAYOUT='" + INTPAYOUT + "',@ODATE='" + conn.ConvertDate(OPENDATE) + "',@RECNO='" + RECNO + "',@OPTYPE='" + OPRTYPE + "',@ACTYPE='" + ACCTYPE + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void BindGrid(GridView Gview, string TBNAME)
    {
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='SELECT',@TBNAME='" + TBNAME + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string GetCurrentBalance(string TBNAME,string WR)
    {
        try
        {
            string FL = "";
            if (WR == "Y")
                FL = "BAL";
            else
                FL = "BALCR";
            sql = "Exec SP_FDTEMPDATA @FLAG='"+FL+"',@TBNAME='" + TBNAME + "'";
            TBNAME = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TBNAME;
    }

    public string Getrate(string PRD, string DPGL,string PT,string ACT,string BRCD,string EDT)
    {
        string RT = "";
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='FD',@ENTRYDATE='" + conn.ConvertDate(EDT) + "',@PERIOD='" + PRD + "',@SUBGLCODE='" + DPGL + "',@GLCODE='" + PT + "',@CUSTTYPE='" + ACT + "' ,@BRCD='" + BRCD + "'";
            RT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RT;
    }

    public string GetDUEDATE( string DPGL, string BRCD, string flag, string ACCNO, string CUSTNO)
    {
        string RT = "";
        try
        {
            sql = "Exec SP_FDTEMPDATA @FLAG='" + flag + "',@SUBGLCODE='" + DPGL + "',@BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + ACCNO + "'";
            RT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RT;
    }

    public string PostData(string EDT, string BRCD, string MID, string CT, string TBNM, string opendate, string OPRTYPE, string ACCTYPE, string REFID, string INTPAYOUT, string RECEIPTNO)
    {
        string RSC="";
        try
        {
            string[] TD = EDT.Split('/');
            string AVSM="AVSM_"+TD[2].ToString()+TD[1].ToString();
            sql = "Exec SP_FDTEMPDATA @FLAG='POST',@ENTRYDATE='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "',@BRCD='" + BRCD + "',@TBNAME='" + TBNM + "',@AVSM='" + AVSM + "',@OPENDATE='" + conn.ConvertDate(opendate) + "',@PARTI='FD RENEWAL',@ACCTYPE='" + ACCTYPE + "',@OPRTYPE='" + OPRTYPE + "',@REFNO='" + REFID + "',@INTPAYOUT='" + INTPAYOUT + "',@RECNO='" + RECEIPTNO + "'";
            RSC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RSC;
    }
    public string PostMultipleClosure(string FL,string SFL,string BRCD,string EDT,string SETNO,string TBNAME,string MID,string PRDCODE,string PRNAMT,string CNO,string ACCTYPE,string RECNO,string ROI,
                                    string PERIODTYPE,string PERIOD,string DUEDATE,string INTRAMT,string MATAMT,string INTPAYOUT,string TGLCODE,string TSUBGL,string TACCNO,string OPDATE)
    {
        string RSC = "";
        try
        {

            sql = "Exec Isp_TDAMulti_Closure @Flag='"+FL+"',@SFlag='"+SFL+"',@Brcd='"+BRCD+"',@Edt='"+conn.ConvertDate(EDT)+"',@Setno='"+SETNO+"',@TBName='"+TBNAME+"',@MID='"+MID+"',@PrdCode='"+PRDCODE+"'," +
                " @PrnAmt='"+PRNAMT+"',@CNO='"+CNO+"',@AccTp='"+ACCTYPE+"',@RecNo='"+RECNO+"', " +
                " @Roi='"+ROI+"',@PeriodType='"+PERIODTYPE+"',@Period='"+PERIOD+"',@DueDate='"+conn.ConvertDate(DUEDATE)+"',@IntrAmt='"+INTRAMT+"',@MatAmt='"+MATAMT+"',@IntPayOut='"+INTPAYOUT+"',@TGlcode='"+TGLCODE+"', " +
                " @TSubglcode='"+TSUBGL+"',@TAccno='"+TACCNO+"',@OpenDate='"+conn.ConvertDate(OPDATE)+"'";
            RSC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RSC;
    }
    public string GetIntPaidtoPara(string Brcd)
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where brcd='" + Brcd + "' and LISTFIELD='FDINTPAID_5'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    
}