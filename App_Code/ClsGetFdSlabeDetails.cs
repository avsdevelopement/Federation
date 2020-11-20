using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// Summary description for ClsGetFdSlabeDetails
/// </summary>
public class ClsGetFdSlabeDetails
{
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    string sql1 = "";

    public ClsGetFdSlabeDetails()
    {

    }

    public DataTable FDSlab(string BRCD, string ason, string FL)
    {
        try
        {
            if (FL == "D")
                sql = "Exec RptFDSlabWiseDetails '" + BRCD + "', '" + Conn.ConvertDate(ason) + "' ";
            else
                sql = "Exec RptFDSlabWiseSummary '" + BRCD + "', '" + Conn.ConvertDate(ason) + "' ";

            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetABRALRDt(string BRCD, string ason)
    {
        try
        {
            sql = "Exec Isp_AVS0029 @Brcd='" + BRCD + "', @FisicalYear='" + Conn.ConvertDate(ason) + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetDtDivPayTrans_DT(string sFlag)
    {
        try
        {
            sql = "Select T.ID, T.BrCd, T.GlCode, T.SubGlCode, T.AccNo, T.CustNo, T.CustName, T.RecDept, T.DesCr, T.BranchName, T.DesCrName, T.Balance, T.BankAccNo, T.IMSBalance, T.AGMBalance " +
                  "From TempTableDIVData T With(NoLock) Where T.ID In (" + sFlag + ") Order By T.ID";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

}