using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public class ClsCashReciept
{
    string sql, sResult, TableName, sqlc, sqld;
    int Result = 0;
    double Limit = 0;
    string AppNo = "0";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    
    public ClsCashReciept()
    {

    }

    public int InsertNewSetNo(string Entrydate, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string AMOUNT, string SETNO, string PARTICULARS2, string BRCD, string MID, string PCMAC, string CUSTNO, string CUSTNAME)
    {
        int result = 0;
        try
        {
            sqlc = "INSERT INTO ALLVCR (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + GLCODE + "', '" + SUBGLCODE + "', '" + ACCNO + "', '" + PARTICULARS + "', '" + AMOUNT + "', '0', '3', 'CASH','" + SETNO + "','1','" + PARTICULARS2 + "','1001','" + BRCD + "','" + MID + "','" + PCMAC + "','" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE))";
            sqld = "INSERT INTO ALLVCR (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '99', '0', '99', '" + PARTICULARS + "', '" + '0' + "', '" + AMOUNT + "', '3', 'CASH','" + SETNO + "','1','" + PARTICULARS2 + "','1001','" + BRCD + "','" + MID + "','" + PCMAC + "','" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE))";

            string sqlu = "update AVS1000 SET LASTNO='" + SETNO + "' WHERE  TYPE='CASH-R' AND BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek

            result = conn.sExecuteQuery(sqlc);
            int result1 = conn.sExecuteQuery(sqld);
            int result2 = conn.sExecuteQuery(sqlu);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable Insert_Receipt(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL)
    {
        try
        {
            sql = "EXEC SP_RECEIPT_PRINT  @FLAG='" + FL + "',@SETNO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CNO='" + CNO + "',@ACNAME='" + NAME + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable Insert_ReceiptH(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL)
    {
        try
        {
            sql = "EXEC SP_RECEIPT_PRINT_H  @FLAG='" + FL + "',@SETNO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CNO='" + CNO + "',@ACNAME='" + NAME + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable Insert_ReceiptHSFM(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL,string Scroll)
    {
        try
        {
            sql = "EXEC SP_RECEIPT_PRINT  @FLAG='" + FL + "',@SETNO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CNO='" + CNO + "',@ACNAME='" + NAME + "',@SCROLLNO='" + Scroll + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable RECOVERYACCSTATMENTHSFM(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL)
    {
        try
        {
            sql = "EXEC SP_Statment_ACC_REC  @FLAG='R',@CASENO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CASEY='" + CNO + "',@ACNAME='" + NAME + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }



    public string GetPanNo(string BRCD, string CustNo)
    {
        string No = "";
        try
        {
            sql = "select DOC_NO from identity_proof where DOC_TYPE=3 and BRcd='" + BRCD + "' and Custno='" + CustNo + "'";
            No = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return No;
    }
    // Get Grid Data
    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT,string paymst)
    {
        try
        {
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10), IsNull(A.SETNO,'0'))+'_'+ConVert(VarChar(10), IsNull(A.Amount,'0'))+'_'+ " +
                  "ConVert(VarChar(10), IsNull(A.SUBGLCODE,'0')) +'_'+ConVert(VarChar(10), IsNull(A.ACCNO,'0')) Dens,A.SETNO SETNO, " +
                  "A.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.AMOUNT,  A.PARTICULARS PARTICULARS, UM.USERNAME MAKER " +
                  "From " + TableName + " A " +
                  "Inner Join USERMASTER UM ON UM.PERMISSIONNO=A.MID " +
                  "Left Join AVS_ACC ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE " +
                  "Left Join MASTER M ON M.CUSTNO = ACC.CUSTNO " +
                  "Where A.BRCD= '" + sbrcd + "' AND A.STAGE = '1003' AND A.TrxType='2'  AND A.ENTRYDATE = '" + conn.ConvertDate(EDT) + "' " +   //AND A.ACTIVITY='3'  AND A.ACTIVITY='3' and A.PAYMAST= '" + paymst + "'
                  " " +
                  "Order By A.SETNO,A.SCROLLNO";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string getsetno(string BRCD)
    {
        string setnumb = "";
        try
        {
            sql = "select LASTNO from avs1000 where TYPE='CASH-R' AND BRCD='" + BRCD + "'";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (Convert.ToInt32(setnumb) + 1).ToString();
    }

    public string CheckDenom(string BrCode, string SetNo, string EntryDate)
    {
        try
        {
            sql = "Select SetNo From Avs5012 Where BrCd = '" + BrCode + "' And EffectDate = '" + conn.ConvertDate(EntryDate).ToString() + "' " +
                  "And SetNo = '" + SetNo + "' And Stage <> '1004'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string getGlCode(string BRCD, string subglcode)
    {
        string glcode = "";
        try
        {
            string sql = "SELECT GLCODE FROM GLMAST WHERE  SUBGLCODE='" + subglcode + "' and BRCD='" + BRCD + "'";
            glcode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return glcode;
    }

    public string GetAccountNo(string AccNo, string SubGlCode, string brcd)
    {
        string Accname = "";
        try
        {
            string sql = "SELECT B.CUSTNO FROM AVS_ACC A Inner Join MASTER B On A.CUSTNO = B.CUSTNO WHERE A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
            Accname = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Accname;
    }

    // Cancel / Delete Cash receipt
    public int CancelCashreceipt(string setno, string brcd, string edt)
    {
        int result = 0;
        try
        {
            string[] tbname = edt.Split('/');
            string avsm = "AVSM_" + tbname[2].ToString() + tbname[1].ToString() + "";
            string avsb = "AVSB_" + tbname[2].ToString() + tbname[1].ToString() + "";

            string sql = "update ALLVCR SET STAGE = '1004' WHERE SETNO='" + setno + "' AND BRCD='" + brcd + "' and EntryDate='" + conn.ConvertDate(edt) + "'";
            int result1 = conn.sExecuteQuery(sql);

            string sql1 = "update " + avsm + " SET STAGE = '1004' WHERE SETNO='" + setno + "' AND BRCD='" + brcd + "' and EntryDate='" + conn.ConvertDate(edt) + "'";
            int result2 = conn.sExecuteQuery(sql1);

            if (result1 > 0 || result2 > 0)
            {
                result = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int UpdateSet(string setno, string BRCD)
    {
        try
        {
            sql = "UPDATE AVS1000 SET LASTNO='" + setno + "'  WHERE Activityno='3' AND BRCD='" + BRCD + "' AND TYPE='CASH-R'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetAccStatDetails(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            if (Convert.ToInt32(PFYEAR.ToString()) < 2000)
            {
                PFYEAR = "2000";
                string[] aa = @PFDT.ToString().Split('/');
                @PFDT = aa[0].ToString() + '/' + aa[1].ToString() + '/' + PFYEAR;
            }

            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetFinStartDate(string EDate)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SP_GetCurrFinStartDate '" + conn.ConvertDate(EDate).ToString() + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int GetInfo(GridView GrdView, string BRCD, string edate, string accno, string subgl)
    {
        try
        {
            sql = "EXEC A_VOUCHINFO 'MODAL','" + subgl + "','" + BRCD + "','" + conn.ConvertDate(edate) + "','" + accno + "'";

            Result = conn.sBindGrid(GrdView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfoTbl(string BRCD, string edate, string accno, string subgl)
    {
        DataTable dtmodal = new DataTable();
        try
        {
            sql = "EXEC A_VOUCHINFO 'MODAL','" + subgl + "','" + BRCD + "','" + conn.ConvertDate(edate) + "','" + accno + "'";
            dtmodal = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dtmodal;
    }
    public string Getjointname(string BRCD, string accno, string subgl)//added by ankita to display jointname 07-06-17
    {
        string jname = "";
        try
        {

            sql = "select REPLACE(JOINTNAME,'-',' ')JOINTNAME from JOINT where subglcode='" + subgl + "' and brcd='" + BRCD + "' and accno='" + accno + "'";
            jname = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return jname;
    }
    public string GetAcctype(string acctypeno)//added by ankita to get acctype 07-06-17
    {
        string acctype = "";
        try
        {
            sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1017' AND SRNO='" + acctypeno + "'";
            acctype = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return acctype;
    }

    public int GetAccDetails(GridView GView,string BrCode, string CustNo, string EDate)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec sGetAccDetails '" + BrCode + "','" + CustNo + "', '" + conn.ConvertDate(EDate).ToString() + "'";
            Result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetTotalAmt(string brcd, string setno, string edate)
    {
        string amt = "";
        try
        {
            string[] tbname = edate.Split('/');
            string avsm = "AVSM_" + tbname[2].ToString() + tbname[1].ToString() + "";
            sql = "select SUM(AMOUNT) AMOUNT from " + avsm + " where SETNO='" + setno + "' and PMTMODE not in ('TR-INT','TR_INT') AND BRCD='" + brcd + "' AND TRXTYPE=1 AND ENTRYDATE='" + conn.ConvertDate(edate) + "'";
            amt = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return amt;
    }

   



    public string GetPanNo(string CustNo)
    {
        try
        {
            //  Added by amol on 28/05/2018 (pan card details not display)
            //sql = "select DOC_NO from identity_proof where DOC_TYPE=3 and BRcd='" + BRCD + "' and Custno='" + CustNo + "'";
            sql = "Select DOC_NO From Identity_Proof Where CustNo = '" + CustNo + "' And DOC_TYPE = '3' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }


    public string GetAadharNo(string BRCD, string CustNo)
    {
        string No = "";
        try
        {
            sql = "select DOC_NO from identity_proof where DOC_TYPE=2 and BRcd='" + BRCD + "' and Custno='" + CustNo + "'";
            No = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return No;
    }

    public string CheckValidation(string ProdCode, string BRCD)
    {
        string No = "";
        try
        {
            sql = "select TRFDR from GLMAST where SUBGLCODE='" + ProdCode + "' and BRCD='" + BRCD + "'";
            No = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return No;
    }

    public double CheckLimit(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select Limit From LoanInfo Where BrCd='" + BrCode + "' And LoanGlCode='" + ProdCode + "' And CustAccNo = '" + AccNo + "'";
            Limit = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Limit;
    }

    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+Convert(VARCHAR(10),CUSTNO)) CUSTNAME FROM MASTER WHERE CUSTNO='" + custno + "' And Stage <> '1004'";
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    //public DataTable CheckCashSet(string BrCode, string EDate, string SetNo)
    //{
    //    try
    //    {
    //        string[] TD;

    //        TD = EDate.Split('/');
    //        TableName = "AVSM_" + TD[2].ToString() + "" + TD[1].ToString();

    //        sql = "Select Activity, Stage From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And Stage <> '1004' And Activity = '3' And PayMast = 'CASHR'";
    //        DT = conn.GetDatatable(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return DT;
    //}

    //  Commited and changes by amol on 2018-07-04 (As per instruction by ambika mam for take all types of set)
    public DataTable CheckCashSet(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD;

            TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + "" + TD[1].ToString();

            sql = "Select Activity, Stage From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And Activity = '3' And Stage <> '1004'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
    public DataTable RECYSPM(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL)
    {
        try
        {
            sql = "EXEC SP_RECEIPT_PRINT  @FLAG='" + FL + "',@SETNO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CNO='" + CNO + "',@ACNAME='" + NAME + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetFDPara(string Flag)
    {
        try
        {
            sql = "Exec AVS_RDPara @Flag='" + Flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public int InsertData(DataTable dt, string BRCD, string MID)
    {
        int result = 0;
        int Re = 0;
        DataTable dt1 = new DataTable();
        try
        {
            sql = "select * from avs_receipt where brcd='" + BRCD + "' and MID='" + MID + "'";
            dt1 = conn.GetDatatable(sql);
            if (dt1.Rows.Count > 0 && dt1.Rows == null)
            {
                sql = "delete from avs_receipt where brcd='" + BRCD + "' and MID='" + MID + "'";
                Re = conn.sExecuteQuery(sql);
                if (Re > 0)
                {
                    using (SqlConnection con = new SqlConnection(conn.DbName()))
                    {
                        con.Open();
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DbName());
                        //Give your Destination table name
                        sqlBulk.DestinationTableName = "avs_receipt";
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                        result = 1;
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(conn.DbName()))
                {
                    con.Open();
                    SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DbName());
                    //Give your Destination table name
                    sqlBulk.DestinationTableName = "avs_receipt";
                    sqlBulk.WriteToServer(dt);
                    con.Close();
                    result = 1;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            result = 0;
        }
        return result;
    }
    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string InsertData(string BrCode, string ApplType, string MemberNo, string CustNo, string NoOfShr, string ShrValue, string TotalShr, string SetNo, string PmtMode, string Remark, string Parti, string EDate, string Mid)
    {
        try
        {
            if (ApplType == "2")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'A')A";
                AppNo = conn.sExecuteScalar(sql);
            }

            sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @SHRValue = '" + ShrValue + "', @NoOfSHR = '" + NoOfShr + "', @TotSHRValue = '" + TotalShr + "', @SetNo = '" + SetNo + "', @PMTMode = '" + PmtMode + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='ADD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result.ToString() + "_" + AppNo.ToString();
    }

    public int GetShareSuspGl(string BrCode)
    {
        try
        {
            sql = "Select IsNull(SHARES_GL, 0) As SHARES_GL From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable Insert_Receipt_Yuva(string SETNO, string EDT, string brcd, string CNO, string NAME, string FL)
    {
        try
        {
            sql = "EXEC ISP_AVS0174  @FLAG='" + FL + "',@SETNO='" + SETNO + "',@BRCD='" + brcd + "',@EDT='" + conn.ConvertDate(EDT) + "',@CNO='" + CNO + "',@ACNAME='" + NAME + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}