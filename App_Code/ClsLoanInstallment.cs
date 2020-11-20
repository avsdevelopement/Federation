using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsLoanInstallment
{
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown(); 
    DbConnection conn = new DbConnection();
    ClsAuthorized AZ = new ClsAuthorized();
    DataTable DT = new DataTable();
    string sql, SetNo1, SetNo2, sResult, RefNo = "";
    int Result = 0;

    public ClsLoanInstallment()
    {
    }
    public string SetNO(string BRCD) //BRCD ADDED --Abhishek
    {
        string ST = "";
        try
        {
            try
            {
                sql = " Update avs1000 set Lastno=(SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE SRNO='46' and BRCD='" + BRCD + "') where srno='46' and BRCD='" + BRCD + "' " +
                         " SELECT MAX(LASTNO) FROM AVS1000 WHERE SRNO='46' and BRCD='" + BRCD + "'";
                ST = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                return "-1";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ST;
    }

    public int InsertBatch(string GLCD, string SGLCD, string ACCNO, string ACNAME, string CT, string AMT, string TRX, string ACT, string PMT, string NR,string NR1, string BRCD, string MID, string EDT, string ST,string INSTNO,string INSTDT)
    {
        try
        {
            string INS = INSTNO.ToString() == "" ? "0" : INSTNO;
            string inDT = INSTDT.ToString() == "" ? "1900-01-01" : INSTDT;

                sql = "INSERT INTO TEMPBATCH (GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,NR1,BRCD,MID,MID_DATE,SETNO,INSTRUMENTNO,INSTRUMENTDATE) " +
                    "VALUES('" + GLCD + "','" + SGLCD + "','" + ACCNO + "','" + ACNAME + "','" + CT + "','" + AMT + "','" + TRX + "','" + ACT + "','" + PMT + "','" + NR + "','" + NR1 + "','" + BRCD + "','" + MID + "','" + conn.ConvertDate(EDT) + "','" + ST + "','" + INS + "','" + conn.ConvertDate(INSTDT) + "')";
                Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int checkData(string ST, string BRCD, string EDT)
    {
        try
        {
            sql = "SELECT * FROM TEMPBATCH WHERE SETNO='" + ST + "' AND BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "DELETE FROM TEMPBATCH WHERE SETNO='" + ST + "' AND BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "'";
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

    public int GetInfo(GridView Gview, string MID, string SGLCD, string ACCNO, string EDT, string BRCD, string ST,string TP)
    {

        try
        {
            sql = "SELECT SETNO,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,(CASE WHEN TRXTYPE='1' THEN 'CR' ELSE 'DR' END ) TXRTYPE,ACTIVITY,PMT,NR,BRCD,MID,MID_DATE FROM TEMPBATCH WHERE MID='" + MID + "' AND BRCD='" + BRCD + "' AND SETNO='" + ST + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND PMT='"+TP+"'";
            Result = conn.sBindGrid(Gview, sql);
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            
        }
        return Result;
    }

    public string PostEntry(string BRCD, string EDT, string MID, string FL)
    {
        try
        {
            SetNo1 = ""; SetNo2 = "";
            RefNo = BD.GetMaxRefid(BRCD, EDT, "REFID");
            RefNo = (Convert.ToInt32(RefNo) + 1).ToString();

            //Added by amol on 09/11/2017 as per ambika madam instruction (only when payment mode is cash)
            sql = "SELECT TOP 1 ACTIVITY FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' And SetNo = '0' And ACTIVITY = '3' And Amt > 0 ORDER BY SYSTEMDATE";
            sResult = conn.sExecuteScalar(sql);
            
            if (sResult == "3")
            {
                //For Cash Receipt set
                DT = new DataTable();
                sql = "SELECT ID,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,SETNO,BRCD,MID,Convert(varchar(11),MID_DATE,120)as MID_DATE,Convert(varchar(11),SYSTEMDATE,120) SYSTEMDATE,INSTRUMENTNO,Convert(varchar(11),INSTRUMENTDATE,120)INSTRUMENTDATE,NR1 FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' And ACTIVITY <> '4' And Amt > 0 ORDER BY SYSTEMDATE";
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    SetNo1 = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();
                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), SetNo1.ToString(), DT.Rows[K]["INSTRUMENTNO"].ToString(), DT.Rows[K]["INSTRUMENTDATE"].ToString(), "0", "0", "1003", "01/01/1900", BRCD, MID, "0", "0", "DDSCLOSE", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), RefNo, "0");
                        //Stage=1003 done by abhihsek for Closing blance purpose on Authorize
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    sql = "SELECT * From TempLnTrx Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(EDT).ToString() + "' And Mid = '" + MID + "' And Amount > 0 Order By SystemDate";
                    DT = new DataTable();
                    DT = conn.GetDatatable(sql);
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        Result = ITrans.LoanTrx(BRCD, DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo1.ToString(), "1003", MID, "0", EDT.ToString(), RefNo.ToString());
                    }
                }

                //For Cash Payment set
                DT = new DataTable();
                sql = "SELECT ID,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,SETNO,BRCD,MID,Convert(varchar(11),MID_DATE,120)as MID_DATE,Convert(varchar(11),SYSTEMDATE,120) SYSTEMDATE,INSTRUMENTNO,Convert(varchar(11),INSTRUMENTDATE,120)INSTRUMENTDATE,NR1 FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' And ACTIVITY = '4' And Amt > 0 ORDER BY SYSTEMDATE";
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    SetNo2 = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();
                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        //  added by abhisheck On 29/01/2018 (Stage changed 1003 to 1001)
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), SetNo2.ToString(), DT.Rows[K]["INSTRUMENTNO"].ToString(), DT.Rows[K]["INSTRUMENTDATE"].ToString(), "0", "0", "1001", "01/01/1900", BRCD, MID, "0", "0", "DDSCLOSE", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), RefNo, "0");
                    }
                }
                SetNo1 = SetNo1 + "_" + (SetNo2 == "" ? "0" : SetNo2);
            }
            else
            {
                sql = "SELECT ID,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,SETNO,BRCD,MID,Convert(varchar(11),MID_DATE,120)as MID_DATE,Convert(varchar(11),SYSTEMDATE,120) SYSTEMDATE,INSTRUMENTNO,Convert(varchar(11),INSTRUMENTDATE,120)INSTRUMENTDATE,NR1 FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' AND SETNO = '0' And Amt > 0 ORDER BY SYSTEMDATE";
                DataTable DT = new DataTable();
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    SetNo1 = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();

                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), SetNo1.ToString(), DT.Rows[K]["INSTRUMENTNO"].ToString(), DT.Rows[K]["INSTRUMENTDATE"].ToString(), "0", "0", "1001", "01/01/1900", BRCD, MID, "0", "0", "DDSCLOSE", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), RefNo, "0");
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    sql = "SELECT * From TempLnTrx Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(EDT).ToString() + "' And Mid = '" + MID + "' And SetNo = '0' And Amount > 0 Order By SystemDate";
                    DT = new DataTable();
                    DT = conn.GetDatatable(sql);
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        Result = ITrans.LoanTrx(BRCD, DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo1.ToString(), "1001", MID, "0", EDT.ToString(), RefNo.ToString());
                    }
                }

                sql = "SELECT ID,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,TRXTYPE,ACTIVITY,PMT,NR,SETNO,BRCD,MID,Convert(varchar(11),MID_DATE,120)as MID_DATE,Convert(varchar(11),SYSTEMDATE,120) SYSTEMDATE,INSTRUMENTNO,Convert(varchar(11),INSTRUMENTDATE,120)INSTRUMENTDATE,NR1 FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' AND SETNO = '1' And Amt > 0 ORDER BY SYSTEMDATE";
                DT = new DataTable();
                DT = conn.GetDatatable(sql);
                if (DT.Rows.Count > 0)
                {
                    SetNo2 = BD.GetSetNo(EDT, "DaySetNo", BRCD).ToString();

                    for (int K = 0; K < DT.Rows.Count; K++)
                    {
                        Result = AZ.Authorized(EDT, EDT, Convert.ToDateTime(DT.Rows[K]["MID_DATE"]).ToString("dd/MM/yyyy"), DT.Rows[K]["GL"].ToString(), DT.Rows[K]["SUBGL"].ToString(), DT.Rows[K]["ACCNO"].ToString(), DT.Rows[K]["NR"].ToString(), DT.Rows[K]["NR1"].ToString(), DT.Rows[K]["AMT"].ToString(), DT.Rows[K]["TRXTYPE"].ToString(), DT.Rows[K]["ACTIVITY"].ToString(), DT.Rows[K]["PMT"].ToString(), SetNo2.ToString(), DT.Rows[K]["INSTRUMENTNO"].ToString(), DT.Rows[K]["INSTRUMENTDATE"].ToString(), "0", "0", "1003", "01/01/1900", BRCD, MID, "0", "0", "DDSCLOSE", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["ACCNAME"].ToString(), RefNo, "0");
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    sql = "SELECT * From TempLnTrx Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(EDT).ToString() + "' And Mid = '" + MID + "' And SetNo = '1' And Amount > 0 Order By SystemDate";
                    DT = new DataTable();
                    DT = conn.GetDatatable(sql);
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        Result = ITrans.LoanTrx(BRCD, DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo2.ToString(), "1003", MID, "0", EDT.ToString(), RefNo.ToString());
                    }
                }
                SetNo1 = SetNo1 + "_" + (SetNo2 == "" ? "0" : SetNo2);
            }

            sql = "DELETE FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "'";
            conn.sExecuteQuery(sql);
            sql = "DELETE FROM TempLnTrx Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(EDT).ToString() + "' And Mid = '" + MID + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            
        }
        return SetNo1;
    }

    public int GetInfo(GridView Gview, string BRCD, string EDT, string MID)
    {

        try
        {
            sql = "SELECT SETNO,GL,SUBGL,ACCNO,ACCNAME,CUSTNO,AMT,(CASE WHEN TRXTYPE='1' THEN 'CR' ELSE 'DR' END ) TXRTYPE,ACTIVITY,PMT,NR,BRCD,MID,MID_DATE FROM TEMPBATCH WHERE BRCD='" + BRCD + "' AND MID_DATE='" + conn.ConvertDate(EDT) + "' AND MID='" + MID + "' AND PMT<>'TR_INT'";
            Result = conn.sBindGrid(Gview, sql);
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return Result;
    }

    public double GetOtherIntRate(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select OTHERCHG From LOANGL Where BrCd = '" + BrCode + "' and LoanGlCode='" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult == null)
                sResult = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult == "" ? "0.00" : sResult);
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

}