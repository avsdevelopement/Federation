using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ClsDocRegister
/// </summary>
public class ClsDocRegister
{
    string sql = "";
    DbConnection DBconn = new DbConnection();
    int result;
    DataTable DT = new DataTable();

    public ClsDocRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable CreateDocReg(string FDU, string TDU, string FDT, string TDT, string BRCD)
    {
        DataTable DT = new DataTable();
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        try
        {
           

            sql = " SELECT AV.ACCNO,M.CUSTNAME,AV.DATEOFUPLOAD,AV.Photo_Type FROM AVS1011 AV " +
                  " INNER JOIN AVS_ACC AA ON AV.CUSTNO = AA.CUSTNO " +
                  " INNER JOIN MASTER M ON AV.CUSTNO=M.CUSTNO " +
                  " WHERE AV.DATEOFUPLOAD BETWEEN '" + DBconn.ConvertDate(FDU) + "' AND '" + DBconn.ConvertDate(TDU) + "' " +
                  " AND AV.Photo_Type BETWEEN '"+ FDT +"' AND '"+ TDT +"' AND AV.STAGE<>1004 and AV.BRCD='"+BRCD+"'";


            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int Insert_data()
    {
        return 0;
    }
    public void DropReport()
    {

    }
    public DataTable GetAccountInfo()
    {
        return DT;
    }
    public DataTable FDRecipt(string BRCD, string SGL, string accno,string MID,string EDT,string FL)
    {		
    	
        
            sql = "Exec SP_FDPRINTING @Flag='"+FL+"', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno  + "',@MID='"+MID+"',@EDT='"+DBconn.ConvertDate(EDT)+"'";
             DataTable DT = new DataTable();
    		   DT = DBconn.GetDatatable(sql);
    		   return DT;
             
    }
    public DataTable FDDetailsPrint(string BRCD, string SGL, string accno, string MID, string EDT, string FL) // Marathwada FD Print
    {


        sql = "Exec RptFDPrint @Flag='" + FL + "', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "',@MID='" + MID + "',@EDT='" + DBconn.ConvertDate(EDT) + "'";
             DataTable DT = new DataTable();
    		   DT = DBconn.GetDatatable(sql);
    		   return DT;
             
    }
    public DataTable FDPrint_Palghar(string BRCD, string SGL, string accno, string MID, string EDT, string FL)  // Palghar FD Print
    {
        sql = "Exec RptFDPrint_Palghar @Flag='" + FL + "', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "',@MID='" + MID + "',@EDT='" + DBconn.ConvertDate(EDT) + "'";
             DataTable DT = new DataTable();
    		   DT = DBconn.GetDatatable(sql);
    		   return DT;
    }
    public DataTable FDPrint_Chikotra(string BRCD, string SGL, string accno, string MID, string EDT, string FL)  // Palghar FD Print
    {
        sql = "Exec RptFDPrint_Chikotra @Flag='" + FL + "', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "',@MID='" + MID + "',@EDT='" + DBconn.ConvertDate(EDT) + "'";
        DataTable DT = new DataTable();
        DT = DBconn.GetDatatable(sql);
        return DT;
    }
    public DataTable FDShivsamarth(string BRCD, string SGL, string accno, string MID, string EDT, string FL)
    {


        sql = "Exec Isp_AVS0026 @Flag='" + FL + "', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "',@MID='" + MID + "',@EDT='" + DBconn.ConvertDate(EDT) + "'";
        DataTable DT = new DataTable();
        DT = DBconn.GetDatatable(sql);
        return DT;

    }
    public int FDReceiptGrid(GridView GD, string BRCD, string SGL, string accno)
    {
        try
        {
            sql = "Exec SP_FDPRINTING @Flag='GRID', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "'";
            result = DBconn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
      return result;
    }

    public string GetPrintStatus(string GL, string SBGL, string ACCNO, string BRCD)
    {

        try
        {
            sql = "SELECT print_status from Depositinfo where BRCD='" + BRCD + "' and CUSTACCNO='" + ACCNO + "' and DEPOSITGLCODE='" + SBGL + "'";
            SBGL=DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SBGL;
    }
    public DataTable GetFdDetails(string FL,string Brcd,string Subgl,string Accno,string Edt)
    {
        try
        {
            sql = "Exec  Isp_FDPrinting @Flag='" + FL + "',@Brcd='" + Brcd + "',@Subgl='" + Subgl + "',@Accno='" + Accno + "',@Edt='" + DBconn.ConvertDate(Edt) + "'";
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}