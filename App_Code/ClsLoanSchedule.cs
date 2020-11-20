using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public class ClsLoanSchedule
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
    public ClsLoanSchedule()
    {

    }
    public string ProcessLoanSchedule(string BRCD,string FaccT,string TAccT,string FAccNo,string TAccNo,string MID)
    {

        //string CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, PERIOD, INSTALLMENT, BONDNO, INTRATE, MID; ;
        //CUSTNO = CUSTACCNO = LOANGLCODE = LIMIT = SANSSIONDATE = PERIOD = INSTALLMENT = BONDNO = INTRATE = MID = "";
        //double BALANCE, INTR, INSTALLAMT, INSTOG, INTRAMT;
        //try 
        //{
        //sql = "SELECT  * FROM LOANINFO WHERE BRCD='" + BRCD + "' AND Lmstatus=1 and loanglcode between '"+FaccT+"' and '"+TAccT+"' and custaccno between '"+FAccNo+"' and '"+TAccNo+"'";
        //DataTable DT = conn.GetDatatable(sql);
        //if (DT.Rows.Count > 0)
        //{
        //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
        //    {
        //        CUSTNO = DT.Rows[i]["CUSTNO"].ToString();
        //        CUSTACCNO = DT.Rows[i]["CUSTACCNO"].ToString();
        //        LOANGLCODE = DT.Rows[i]["LOANGLCODE"].ToString();
        //        LIMIT = DT.Rows[i]["LIMIT"].ToString();
        //        INSTALLMENT = DT.Rows[i]["Installment"].ToString();
        //        PERIOD = DT.Rows[i]["PERIOD"].ToString();
        //        BONDNO = DT.Rows[i]["BONDNO"].ToString();
        //        INTRATE = DT.Rows[i]["Intrate"].ToString();
        //        MID = DT.Rows[i]["MID"].ToString();
        //        SANSSIONDATE = Convert.ToDateTime(DT.Rows[i]["SANSSIONDATE"]).ToString("dd/MM/yyyy");
        //        int PR = Convert.ToInt32(PERIOD);
        //        BALANCE = Convert.ToDouble(LIMIT);
        //        INTR = Convert.ToDouble(INTRATE);
        //        INSTALLAMT = Convert.ToDouble(INSTALLMENT);
        //        INSTOG = Convert.ToDouble(INSTALLMENT);
        //        INTRAMT = 0;
        //        DateTime DTST;

        //        sql = "select distinct custno,custaccno,Loanglcode from Loanschedule where brcd='" + BRCD + "' and loanglcode='" + DT.Rows[i]["LOANGLCODE"].ToString() + "' and custno='" + DT.Rows[i]["CustNo"].ToString() + "' and custaccno='" + DT.Rows[i]["custaccno"].ToString() + "'";
        //        DataTable DT1 = new DataTable();
        //        DT1 = conn.GetDatatable(sql);
        //        if (DT1.Rows.Count > 0)
        //        {
        //            sql = "update Loanschedule stage=1004,vid='" + MID + "0000' where brcd='" + BRCD + "' and loanglcode='" + DT.Rows[i]["LOANGLCODE"].ToString() + "' and custno='" + DT.Rows[i]["CustNo"].ToString() + "' and custaccno='" + DT.Rows[i]["custaccno"].ToString() + "'";
        //            Result = conn.sExecuteQuery(sql);
        //            if (Result > 0)
        //            {
        //            }

        //        }
        //        for (int I = 0; I <= PR; I++)
        //        {
        //            INTRAMT = Math.Round((((BALANCE * INTR) / 100) / 12), 0);
        //            INSTALLAMT = Math.Round(Convert.ToDouble(INSTALLMENT), 0);
        //           // DTST = Convert.ToDateTime(SANSSIONDATE.ToString()).AddMonths(I);
        //           string dyst = conn.AddMonthDay(SANSSIONDATE, Convert.ToString(I), "M");
        //            sql = "INSERT INTO LOANSCHEDULE(CUSTNO,CUSTACCNO,LOANGLCODE,SANLIMIT,LIMIT,INSTALLMENT,DISBURSTMENT_AMOUNT,INSTDATE,PERIOD,BALANCE,INTRATE,BONDNO,INTEREST_RECV,MID,PCMAC,STAGE,BRCD)" +
        //                " VALUES('" + CUSTNO + "','" + CUSTACCNO + "','" + LOANGLCODE + "','" + LIMIT + "','" + LIMIT + "','" + INSTALLMENT + "','" + LIMIT + "','" + conn.ConvertDate(dyst.ToString()) + "','" + PERIOD + "','" + BALANCE + "','" + INTRATE + "','" + BONDNO + "','" + INTRAMT + "','" + MID + "','" + conn.PCNAME() + "','1003','" + BRCD + "')";
        //            Result = conn.sExecuteQuery(sql);
        //            BALANCE = (BALANCE - INSTALLAMT);
        //            if (BALANCE < INSTOG)
        //            {
        //                INSTALLMENT = BALANCE.ToString();
        //            }
        //        }
        //    }


        try
        {
            sql = "EXEC SP_LOAN_SCHEDULE @FLAG='LS',@FBRCD='" + BRCD + "',@TBRCD='" + BRCD + "',@FSBGL='" + FaccT + "',@TSBGL='" + TAccT+ "',@FACCNO='" + FAccNo + "',@TACCNO='" + TAccNo + "',@MID='" + MID + "'";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                FaccT = "Success";
            }
            else
            {
                FaccT = "No Record Found";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return FaccT;
    }
    public string GetAccTypeGL(string AccT, string BRCD)
    {
        try 
        {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND GLCODE='3' AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return AccT;
    }
    public string GetAccName(string ACNO, string FAT,string TAT, string BRCD)
    {
        try 
        {
        sql = "SELECT M.CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD WHERE AC.SUBGLCODE BETWEEN '" + FAT + "' AND '"+TAT+"' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";
        ACNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return ACNO;
    }
}