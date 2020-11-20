using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsNotice_SRO
/// </summary>
public class ClsNotice_SRO
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", result = "";

	public ClsNotice_SRO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet RptNotices_SRO(string BRCD, string PCODE, string ACCNO, string Edate)  
    {
        try
        {
            sql = "Exec SP_RptNotice_SRO @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

 public DataSet RptUpsetCoverletter_Sro(string BRCD, string PCODE, string ACCNO, string Edate)  
    {
        try
        {
            sql = "Exec SP_RptUpsetCoverletter_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
 public DataSet RptUpsetletter_Sro(string BRCD, string PCODE, string ACCNO, string Edate)
 {
     try
     {
         sql = "Exec SP_RptUpsetletter_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
         DS = new DataSet();
         DS.Tables.Add(conn.GetDatatable(sql));
     }
     catch (Exception Ex)
     {
         ExceptionLogging.SendErrorToText(Ex);
     }
     return DS;
 }

    public DataSet RptNotices_Demand(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_DemandNotice @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }


 public DataSet RptSRNOMONTHLYRPT(string flag,string FDATE, string TDATE, string FSRNO, string TSRNO)
    {
        try
        {
            sql = "Exec SP_SROMonthlyRPT @FLAG='" + flag + "', @FDATE='" +conn.ConvertDate( FDATE) + "',@TDATE='" +conn.ConvertDate( TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
 public DataSet RptSRNOMRPT(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO)
 {
     try
     {
         sql = "Exec SP_rptSROMonthly @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "' ";
         DS = new DataSet();
         DS.Tables.Add(conn.GetDatatable(sql));
     }
     catch (Exception Ex)
     {
         ExceptionLogging.SendErrorToText(Ex);
     }
     return DS;
 }
 public DataSet MONTHLYRptSRNOMRPT(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO)
 {
     try
     {
         sql = "Exec SP_rptSROMonthlySummary @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "' ";
         DS = new DataSet();
         DS.Tables.Add(conn.GetDatatable(sql));
     }
     catch (Exception Ex)
     {
         ExceptionLogging.SendErrorToText(Ex);
     }
     return DS;
 }
 public DataSet RptNotice_Visit(string BRCD, string PCODE, string ACCNO, string Edate, string TDATE)
    {
        try
        {
            sql = "Exec SP_RptNotice_Visit @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "',@VISITDATE='" + conn.ConvertDate(TDATE) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptNotice_PropertyAttOrdert(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_PropertyAttOrder @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptPublicLetter_Sro_N(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptPublicLetter_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptNotice_ACAttOrder(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_ACAttOrder @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptNotices_SYMBOLIC(string BRCD, string PCODE, string ACCNO, string Edate,string SYMBOLICD)
    {
        try
        {
            sql = "Exec SP_RptNotice_SYMBOLIC @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "',@SYMOLICDATESYMOLICDATE='" + conn.ConvertDate(SYMBOLICD) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

    public DataSet RptNotices_ATTACHMENTDATE(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_ATTACHMENTNOTICE @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

    public DataSet RptNotices_NBA(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_NBA @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptNotices_Valuation(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_Valuation @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
   public DataSet SP_RptNotice_PossionRpt(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptNotice_PossionRpt @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptProtectionNotice_Sro(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptProtectionNotice_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet RptPossessionNotice_Sro(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptPossessionNotice_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet SP_RptPublicLetterNotice_Sro(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptPublicLetterNotice_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
         public DataSet SP_RptPublicLetter_Sro(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptPublicLetter_Sro @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

          public DataSet SP_RptAuction_BLetter(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptAuction_BLetter @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
          public DataSet Sp_RPTproposalforsale(string BRCD, string PCODE, string ACCNO, string Edate)
          {
              try
              {
                  sql = "Exec Sp_RPTproposalforsale @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
                  DS = new DataSet();
                  DS.Tables.Add(conn.GetDatatable(sql));
              }
              catch (Exception Ex)
              {
                  ExceptionLogging.SendErrorToText(Ex);
              }
              return DS;
          }

          public DataSet SP_RptAuction_Marathi(string BRCD, string PCODE, string ACCNO, string Edate)
          {
              try
              {
                  sql = "Exec SP_RptAuction_BLetter_marathi @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
                  DS = new DataSet();
                  DS.Tables.Add(conn.GetDatatable(sql));
              }
              catch (Exception Ex)
              {
                  ExceptionLogging.SendErrorToText(Ex);
              }
              return DS;
          }
  public DataSet SP_RptIntimation_ToSocity(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptIntimation_ToSocity @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

    

  public DataSet SP_RptIntimation_Cheque(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        try
        {
            sql = "Exec SP_RptIntimation_Cheque @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
  public DataSet SP_Rpt31Remainder_Notice(string BRCD, string PCODE, string ACCNO, string Edate)
  {
      try
      {
          sql = "Exec SP_Rpt31Remainder_Notice @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
          DS = new DataSet();
          DS.Tables.Add(conn.GetDatatable(sql));
      }
      catch (Exception Ex)
      {
          ExceptionLogging.SendErrorToText(Ex);
      }
      return DS;
  }

       public DataSet SP_RptFinalIntimationLetter(string BRCD, string PCODE, string ACCNO, string Edate)
  {
      try
      {
          sql = "Exec SP_RptFinalIntimationLetter @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
          DS = new DataSet();
          DS.Tables.Add(conn.GetDatatable(sql));
      }
      catch (Exception Ex)
      {
          ExceptionLogging.SendErrorToText(Ex);
      }
      return DS;
  }

       public DataSet SP_RptExecutionChargesLetter_ToSocity(string BRCD, string PCODE, string ACCNO, string Edate)
  {
      try
      {
          sql = "Exec SP_RptExecutionChargesLetter_ToSocity @Brcd='" + BRCD + "',@Year='" + PCODE + "',@CaseNo='" + ACCNO + "',@EDATE='" + conn.ConvertDate(Edate) + "' ";
          DS = new DataSet();
          DS.Tables.Add(conn.GetDatatable(sql));
      }
      catch (Exception Ex)
      {
          ExceptionLogging.SendErrorToText(Ex);
      }
      return DS;
  }
}