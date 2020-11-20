using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5159
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    double Balance = 0;
    int Result = 0;

	public ClsAVS5159()
	{
		
	}

    public void BindBranch(DropDownList DDL, string BRCD)
    {
        DT = new DataTable();
        DS = new DataSet();
        try
        {
            sql = "Select ConVert(VarChar(10), BrCd)+'-'+ ConVert(VarChar(200), MidName) As Name, BrCd As ID " +
                  "From BankName Where BrCd <> 0 Order By BrCd";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;

            DDL.DataSource = DS;
            DDL.DataTextField = "Name";
            DDL.DataValueField = "ID";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetProdDetails(string BrCode, string PrCode)
    {
        DT = new DataTable();
        try
        {
            sql = "Select GlCode, SubGlCode, GlName, IsNull(UnOperate, '0') As UnOperate, ISNULL(IntAccYN,'N') As IntAccYN " +
                  "From GlMast With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAccDetails(string BrCode, string PrCode, string AccNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Select G.GlCode, G.SubGlCode, G.GlName, Ac.AccNo, Ac.RecSrNo, Ac.CustNo, M.CustName, Ac.Acc_Status, Ac.Acc_Type, Opr_Type, Minor_Acc, Ac.Stage" +
                  "From Avs_Acc Ac With(NoLock) " +
                  "Left Join GlMast G With(NoLock) " +
                  "Left Join Master M With(NoLock) " +
                  "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + PrCode + "' And Ac.AccNo = '" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetSubGlCode(string BrCode)
    {
        try
        {
            sql = "Select SubGlCode From GlMast Where GlCode = '6' And BrCd = '" + BrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public double GetOpenClose(string Flag, string Fyear, string FMonth, string BrCode, string GlCode, string PrCode, string AccNo, string EDate, string ACCYN = "OPT", string RECSRNO = "0")
    {
        try
        {
            sql = "Exec SP_OPENCLOSE @P_FLAG='" + Flag + "',@P_FYEAR='" + Fyear + "',@P_FMONTH='" + FMonth + "',@p_job='" + PrCode + "',@p_job1='" + AccNo + "',@p_job2='" + BrCode + "',@p_job3='" + conn.ConvertDate(EDate) + "',@p_job4='" + GlCode + "',@ACCYN='" + ACCYN + "'";
            Balance = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Balance;
    }

    public DataTable GetMobAppBalance(string BakCode, string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='MobApps', @sFlag2 = 'Balance', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '" + PrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            DT = conn.GetDatatableMob1(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCBSBalance(string BakCode, string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='CBS', @sFlag2 = 'Balance', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '" + PrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }


    public DataTable GetMobAppBranchBalance(string BakCode, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='MobApps', @sFlag2 = 'AllBranchBal', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '0000', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            DT = conn.GetDatatableMob1(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCBSBranchBalance(string BakCode, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='CBS', @sFlag2 = 'AllBranchBal', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '0000', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int OnlineCollection(string BakCode, string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='MobApps', @sFlag2 = 'Receive', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '" + PrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQueryMob(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UnAuthorize(string BakCode, string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='MobApps', @sFlag2 = 'Authorize', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '" + PrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQueryMob(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteDate(string BakCode, string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_MobileCollection @sFlag1='CBS', @sFlag2 = 'Delete', @BankCode = '" + BakCode + "', @BrCode = '" + BrCode + "', " +
                   "@ProdCode = '" + PrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}