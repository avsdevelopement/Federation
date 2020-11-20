using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ClsSIPost
/// </summary>
public class ClsSIPost
{
    DbConnection conn = new DbConnection();
    ClsEncryptValue EV = new ClsEncryptValue();

    string sql = "",post="",EMD="";
    int Result=0;
	public ClsSIPost()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public int ViewCreatedSI(GridView gd, string brcd, string edt,string UGRP="0") //BRCD ADDED --Abhishek
    {
        try
        {
            if (UGRP == "1" || UGRP == "3")
            {
                sql = "SELECT SINO,CRGL,CRSUBGL,CRCUSTNO,CRACCNO,DRGL,DRSUBGL,DRCUSTNO,DRACCNO,SITYPE,NOOFINSTALLMENT,SIAMOUNT,FROMDATE,TODATE,MONTHEXECUTDAY,LASTEXECUTDATE, " +
                        " NEXTEXECUTIONDATE,REMARK,STATUS,SUID,LASTPOSTINGDATE,MID,MID_DATE,CID,CI_DATE,STAGE,BRCD,SYSDATE, " +
                        " PARTICULARS,PCMAC,SI_TYPETRF FROM AVS5007 where STAGE not in (1004) and BRCD='" + brcd + "'";
            }
            else
            {
                sql = "SELECT SINO,CRGL,CRSUBGL,CRCUSTNO,CRACCNO,DRGL,DRSUBGL,DRCUSTNO,DRACCNO,SITYPE,NOOFINSTALLMENT,SIAMOUNT,FROMDATE,TODATE,MONTHEXECUTDAY,LASTEXECUTDATE, " +
                        " NEXTEXECUTIONDATE,REMARK,STATUS,SUID,LASTPOSTINGDATE,MID,MID_DATE,CID,CI_DATE,STAGE,BRCD,SYSDATE, " +
                        " PARTICULARS,PCMAC,SI_TYPETRF FROM AVS5007 where STAGE not in (1004,1003) and BRCD='" + brcd + "'";
            }
            Result = conn.sBindGrid(gd, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int ViewTrail(GridView GD, string BRCD, string EDT,string DBSUBGL,string DACNO,string CACNO,string CRSUBGL,string EMIAMT)
    {
        try
        {
            sql = "EXEC SP_SI_INSERT @FLAG='POST_TRAIL',@DBPRDCD='"+DBSUBGL+"',@DBACCNO='"+DACNO+"',@CRPRDCD='"+CRSUBGL+"',@CRACCNO='"+CACNO+"',@EMIAMOUNT='"+EMIAMT+"',@EDT='"+conn.ConvertDate(EDT)+"',@BRCD='"+BRCD+"'";
            Result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetDBACCDetail(string BRCD, string SINO)
    {
        try
        {
            sql = "select Convert(varchar(10),DRSUBGL)+'_'+Convert(varchar(10),DRACCNO)+'_'+Convert(varchar(10),SIAMOUNT) from avs5008 " +
                    " where SINO='" + SINO + "' and BRCD='" + BRCD + "' and STATUS<>3 ";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int GetPostTrial(GridView GD, string flag, string BRCD, string ONDATE)////Added by ankita on 12/06/2017 To display SI Post Trial
    {
        try
        {
            sql = "Exec Isp_SI_OprDdsToLoan @Flag='"+flag+"',@SFlag='FT',@Brcd='" + BRCD + "',@OnDate='" + conn.ConvertDate(ONDATE) + "'";
            Result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetPostTrialRpt(string flag, string BRCD, string ONDATE)////Added by ankita on 12/06/2017 To display SI Post Trial
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec Isp_SI_OprDdsToLoan @Flag='" + flag + "',@SFlag='FT',@Brcd='" + BRCD + "',@OnDate='" + conn.ConvertDate(ONDATE) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public string GetPostNo(string flag, string BRCD, string ONDATE,string mid)////Added by ankita on 12/06/2017 To display SI Post Trial
    {
        try
        {
            EMD = EV.GetMK(mid);

            sql = "Exec Isp_SI_OprDdsToLoan @Flag='" + flag + "',@SFlag='POST',@Brcd='" + BRCD + "',@OnDate='" + conn.ConvertDate(ONDATE) + "',@MID='" + mid + "',@F1='" + EMD + "'";
            post = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return post;
    }


    public string GetSI_EXEU(string BRCD)
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where BRCD='" + BRCD + "' and LISTFIELD='SI_EXEU'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
  
}