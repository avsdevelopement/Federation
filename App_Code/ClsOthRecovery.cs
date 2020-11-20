using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ClsOthRecovery
/// </summary>
public class ClsOthRecovery
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "" ,sResult = "";
    string GlCode, AccNo = "";
    string rtn = "";
    int Res = 0;
    int Result = 0;

	public ClsOthRecovery()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public int AddECS(string Brcd , string SubGlcode, string CustNo, string Accno,string StartDate, string FL, string Amount, string IntRate, string Period, string IntAmt, string MatAmt, string EndDate, string MID)
    {
        try
        {
            sql = "Exec Sp_OtherRecAccAdd @Brcd='" + Brcd + "',@SubGlcode='" + SubGlcode + "',@CustNo='" + CustNo + "',@Accno='" + Accno + "',@OpeningDate='" + conn.ConvertDate(StartDate) + "', " +
                " @Status='" + FL + "',@PrnAmt='" + Amount + "',@IntRate='" + IntRate + "',@Period='" + Period + "', " +
                " @InstAmt='" + IntAmt + "',@MatAmt='" + MatAmt + "',@MatDate='" + conn.ConvertDate(EndDate) + "',@MID='" + MID + "',@Flag='Insert' ";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int UpdateECS(string Brcd, string SubGlcode, string CustNo, string Accno, string StartDate, string FL, string Amount, string IntRate, string Period, string IntAmt, string MatAmt, string EndDate, string MID)
    {
        try
        {
            sql = "Exec Sp_OtherRecAccAdd @Brcd='" + Brcd + "',@SubGlcode='" + SubGlcode + "',@CustNo='" + CustNo + "',@Accno='" + Accno + "',@OpeningDate='" + conn.ConvertDate(StartDate) + "', " +
                " @Status='" + FL + "',@PrnAmt='" + Amount + "',@IntRate='" + IntRate + "',@Period='" + Period + "', " +
                " @InstAmt='" + IntAmt + "',@MatAmt='" + MatAmt + "',@MatDate='" + conn.ConvertDate(EndDate) + "',@MID='" + MID + "',@Flag='Modify' ";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int AuthoECS(string Brcd, string SubGlcode, string Accno, string Mid)
    {
        try
        {
            sql = "Exec Sp_OtherRecAccAdd  @Brcd='" + Brcd + "',@SubGlcode='" + SubGlcode + "',@Accno='" + Accno + "', @Flag='Autho',@Mid='" + Mid + "'";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int AuthoDelete (string Brcd, string SubGlcode, string Accno, string Mid)
    {
        try
        {
            sql = "Exec Sp_OtherRecAccAdd  @Brcd='" + Brcd + "',@SubGlcode='" + SubGlcode + "',@Accno='" + Accno + "', @Flag='Delete',@Mid='" + Mid + "'";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    
    public void CreateSharAccounts(string Brcd, string CustNo, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Rec_Prd From AVS_RS Where Rec_Prd = '" + PrCode + "'";
            DT = conn.GetDatatable(sql);

            string Acc = "";

            sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
            Acc = conn.sExecuteScalar(sql);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    GlCode = ""; AccNo = "";

                    sql = "Select GlCode From GlMast Where BrCd = '" + Brcd + "' And SubGlCode = '" + PrCode + "' "; // Rakesh Add 12-02-2019
                    GlCode = conn.sExecuteScalar(sql);

                    if (Acc == "Y")
                    {
                        sql = "select " + CustNo + "";
                    }
                    else
                    {
                        if (GlCode == "2")
                            sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + Brcd + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                        else
                            sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + Brcd + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                    }

                    AccNo = conn.sExecuteScalar(sql);
                    if (Convert.ToDouble(AccNo.ToString()) > 0)
                    {
                        sql = "Insert Into Avs_Acc(BrCd, GlCode, SubGlCode, CustNo, AccNo, OpeningDate, Acc_Status, Stage, Mid, PcMac, Acc_Type, Opr_Type, SystemDate) " +
                            "Values('" + Brcd + "', '" + GlCode + "', '" + DT.Rows[i]["Rec_Prd"].ToString() + "', '" + CustNo + "', '" + AccNo + "', '" + conn.ConvertDate(EDate).ToString() + "', '1', '1001', '" + Mid + "','" + conn.PCNAME() + "', '1', '1', GetDate())";
                        Result = conn.sExecuteQuery(sql);

                        if (Result > 0)
                        {
                            if (GlCode == "2")
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + Brcd + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
                            else
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + Brcd + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
                            Result = conn.sExecuteQuery(sql);
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public string CustNoOpenYN(string PrCode)
    {
        try
        {
            sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }


    public int GridBindData(GridView Gview, string Brcd, string GL, string CustNo)
    {
        try
        {
            sql = "Exec Sp_OtherRecAccAdd  @Flag='Grid',@Brcd='" + Brcd + "',@SubGlcode='" + GL + "',@CustNo='" + CustNo + "'";
            Res = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    //public int GridBindData (GridView Gview,string Brcd)
    //{
    //    try
    //    {
    //        sql = "Exec Sp_OtherRecAccAdd  @Flag='Grid'";
    //        Res = conn.sBindGrid(Gview, sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Res;
    //}
    //public DataTable GetAllFieldData(string ProCode, string AccNo, string brcd)
    //{
    //    try
    //    {
    //        sql = " SELECT isnull(DP.RECSRNO,'0') RECSRNO,DP.RECEIPT_NO,DP.STAGE,M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO,DP.TRFSUBTYPE,DP.TRFACCNO,AC.ACC_TYPE, AC.OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE from DEPOSITINFO DP " +
    //              " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO=AC.CUSTNO AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
    //              " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.CUSTNO=DP.CUSTNO  " +
    //              " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' "; //AND DP.LMSTATUS='1' 
    //        //AND DP.LMSTATUS='1'";
    //        DT = conn.GetDatatable(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //        //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
    //    }
    //    return DT;
    //}


    public DataTable GetAllFieldData(string ProCode, string AccNo, string brcd)
    {
        try
        {
            sql = " SELECT isnull(DP.RECSRNO,'0') RECSRNO,DP.RECEIPT_NO,DP.STAGE,M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO,DP.TRFSUBTYPE,DP.TRFACCNO,AC.ACC_TYPE, AC.OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE,DP.LMSTATUS from DEPOSITINFO DP " +
                  " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO=AC.CUSTNO AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
                  " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.CUSTNO=DP.CUSTNO  " +
                  " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' "; //AND DP.LMSTATUS='1' 
            //AND DP.LMSTATUS='1'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public string GetProductName(string PrdCode, string BRCD)//BRCD ASHOK
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "'";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName;
    }
    public void BindProductName(DropDownList DDL, string ProCode)
    {
        sql = "SELECT Convert(varchar(100),REC_PRD)+'-'+Convert(varchar(100),SHORTNAME) name,REC_PRD id from AVS_RS WHERE REC_GLCODE=5 ORDER BY BRCD";
        conn.FillDDL(DDL, sql);
    }
    public string GetProductName_1(string PrdCode)
    {
        string sql = "SELECT Convert(varchar(100),REC_PRD)+'-'+Convert(varchar(100),SHORTNAME) name,REC_PRD id  from AVS_RS WHERE REC_GLCODE=5 And REC_PRD ='" + PrdCode + "' And VALUE <> 0 ORDER BY BRCD";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName; 
    }
}