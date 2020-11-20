using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsFDIntCalculation
{
    string sql = "", EMD = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue EV = new ClsEncryptValue();
    string EntryMid, verifyMid, DeleteMid = "";
	public ClsFDIntCalculation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable CalculateINT(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP,string MID,string MATACCYN,string MATTACCRATE)
    {
        try
        {
            sql = "EXEC SP_FDINTCALCULATION @FLAG ='CALC',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "',@MatAccYN='" + MATACCYN + "',@MatAccRate='" + MATTACCRATE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT,string SKIP,string MID)
    {
        try
        {
            sql = "EXEC SP_FDINTCALCULATION @FLAG ='TRAILENTRY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
    
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetReport(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT,string SKIP,string MID)
    {
        try
        {
            sql = "EXEC SP_FDINTCALCULATION @FLAG ='TRAILENTRY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID=" + MID + "";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    //public int PostInterest(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE,string SKIP)
    //{
    //    try
    //    {
    //        sql = "EXEC SP_FDINTCALCULATION @FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
    //        Result = Convert.ToInt32(conn.sExecuteScalar(sql));
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    public string PostInterest(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            EMD = EV.GetMK(MID);

            sql = "EXEC SP_FDINTCALCULATION @FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "',@F1='" + EMD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetAvailData(string EDT, string FBRCD, string TBRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_FDINTCALCULATION @FLAG='IFDATA',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@TDATE='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int Recalculate(string EDT, string FBRCD, string TBRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_FDINTCALCULATION @FLAG='RECALC',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@TDATE='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    #region MIS
    public DataTable CalculateINT_MIS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID, string MATACCYN, string MATTACCRATE)
    {
        try
        {
            
            sql = "EXEC Isp_MIS_IntCalc @FLAG ='CALC',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "',@MatAccYN='" + MATACCYN + "',@MatAccRate='" + MATTACCRATE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string PostInterest_MIS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_MIS_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    
    #endregion

    #region FDS
    public DataTable CalculateINT_FDS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID, string MATACCYN, string MATTACCRATE)
    {
        try
        {
            sql = "EXEC Isp_FDS_IntCalc @FLAG ='CALC',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "',@MatAccYN='" + MATACCYN + "',@MatAccRate='" + MATTACCRATE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PostInterest_FDS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_FDS_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
   
    #endregion

    #region LKP
    public DataTable CalculateINT_LKP(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID, string MATACCYN, string MATTACCRATE)
    {
        try
        {
            sql = "EXEC Isp_LKP_IntCalc @FLAG ='CALC',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "',@MatAccYN='" + MATACCYN + "',@MatAccRate='" + MATTACCRATE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PostInterest_LKP(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_LKP_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
  
    #endregion

    #region DD
   

    public string PostInterest_DD(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());
            
            sql = "EXEC Isp_DD_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetReport_DD(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID)
    {
        try
        {
            sql = "EXEC Isp_DD_IntCalc @FLAG ='TRAILENTRY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID=" + MID + "";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    #endregion
    
    #region QIS
   
    public string PostInterest_QIS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_QIS_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetReport_QIS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID)
    {
        try
        {
            sql = "EXEC Isp_QIS_IntCalc @FLAG ='TRAILENTRY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID=" + MID + "";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    #endregion

    #region FDSS
    public DataTable CalculateINT_FDSS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID, string MATACCYN, string MATTACCRATE)
    {
        try
        {
            sql = "EXEC Isp_FDSS_IntCalc @FLAG ='CALC',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID='" + MID + "',@MatAccYN='" + MATACCYN + "',@MatAccRate='" + MATTACCRATE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PostInterest_FDSS(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_FDSS_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
  
    #endregion

    #region CUM
   

    public string PostInterest_CUM(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string MID, string POSTDATE, string SKIP)
    {
        try
        {
            verifyMid = EV.GetMK(MID.ToString());

            sql = "EXEC Isp_CUM_IntCalc @F1='" + verifyMid + "',@FLAG ='APPLY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@PDATE='" + conn.ConvertDate(POSTDATE) + "',@SKIP_DIGIT='" + SKIP + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetReport_CUM(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string SKIP, string MID)
    {
        try
        {
            sql = "EXEC Isp_CUM_IntCalc @FLAG ='TRAILENTRY',@FBRCD ='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRD ='" + FPRD + "',@TPRD ='" + TPRD + "',@FACC ='" + FACC + "',@TACC ='" + TACC + "',@TDATE ='" + conn.ConvertDate(ASDT) + "',@SKIP_DIGIT='" + SKIP + "',@MID=" + MID + "";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    #endregion

    public int Recalculate_ALL(string EDT, string FBRCD, string TBRCD, string MID)
    {
        try
        {
            sql = "EXEC Isp_MIS_IntCalc @FLAG='RECALC',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@TDATE='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void BindCategory(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTIONMAR+'_'+DESCRIPTION name,DESCRIPTION id FROM LOOKUPFORM1 WHERE LNO='2561' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
}