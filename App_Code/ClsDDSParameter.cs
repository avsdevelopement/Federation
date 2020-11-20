using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDDSParameter
/// </summary>
public class ClsDDSParameter
{
    
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();
   
	public ClsDDSParameter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertDDS(string txteffectdate, string txtcommAcc, string txtcommRevAcc, string txtIntacc, string txtServAcc, string txtcommRate, string txtTDSpayacc, string txtTDSrate, string txtOperMon, string txtDorMon, string txtommdedRate, string txtServChrgamt, string ddlpara, string ddlparaid)
    {
        try 
        {
        //sql = "insert into AVS_DDS (EFFECT_DT,COMM_ACCNO, COMM_ACC_REV, INT_ACC, SERV_ACC, COMM_RATE, TDS_ACC, TDS_RATE, INOPERATIVE, DORMANT, COMM_DED_RT, SERV_CHG,PARANAME,PARAID) values('" + EntryDate + "','" + txtcommAcc + "','" + txtcommRevAcc + "','" + txtIntacc + "','" + txtServAcc + "','" + txtcommRate + "','" + txtTDSpayacc + "','" + txtTDSrate + "','" + txtOperMon + "','" + txtDorMon + "','" + txtommdedRate + "','" + txtServChrgamt + "','" + ddlpara + "','" + ddlparaid + "')";
        sql = "insert into AVS_DDS (EFFECT_DT,COMM_ACCNO, COMM_ACC_REV, INT_ACC, SERV_ACC, COMM_RATE, TDS_ACC, TDS_RATE, INOPERATIVE, DORMANT, COMM_DED_RT, SERV_CHG,PARANAME,PARAID) values('" +txteffectdate.ToString() + "','" + txtcommAcc + "','" + txtcommRevAcc + "','" + txtIntacc + "','" + txtServAcc + "','" + txtcommRate + "','" + txtTDSpayacc + "','" + txtTDSrate + "','" + txtOperMon + "','" + txtDorMon + "','" + txtommdedRate + "','" + txtServChrgamt + "','" + ddlpara + "','" + ddlparaid + "')";
        conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
    }


    public int BindDDS(GridView Gview)
    {
        try 
        {
        sql = "select EFFECT_DT ,COMM_ACCNO, COMM_ACC_REV, INT_ACC, SERV_ACC, COMM_RATE, TDS_ACC, TDS_RATE, INOPERATIVE, DORMANT, COMM_DED_RT, SERV_CHG,PARANAME,PARAID ,ID from AVS_DDS";
       // sql = "select * from  AVS_DDS";
        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public void modifyDDS(string txtcommAcc, string txtcommRevAcc, string txtIntacc, string txtServAcc, string txtcommRate, string txtTDSpayacc, string txtTDSrate, string txtOperMon, string txtDorMon, string txtommdedRate, string txtServChrgamt, string ddlpara,string ddlparaid, string ID)
    {
        try 
        {
        sql = " update AVS_DDS set COMM_ACCNO ='" + txtcommAcc + "', COMM_ACC_REV='" + txtcommRevAcc + "', INT_ACC='" + txtIntacc + "', SERV_ACC='" + txtServAcc + "', COMM_RATE='" + txtcommRate + "', TDS_ACC='" + txtTDSpayacc + "', TDS_RATE='" + txtTDSrate + "', INOPERATIVE='" + txtOperMon + "', DORMANT='" + txtDorMon + "', COMM_DED_RT='" + txtommdedRate + "', SERV_CHG='" + txtServChrgamt + "',PARANAME='" + ddlpara + "',PARAID='" + ddlparaid + "' where ID='" + ID + "'";
        conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
    }

    

    public DataTable GetDDSParareport(string FL)
    {
        try 
        {
        //sql = "SELECT * FROM AVS_DDS WHERE ID='" + ID + "'";
        sql = "SELECT COMM_ACCNO, COMM_ACC_REV, INT_ACC, SERV_ACC, COMM_RATE, TDS_ACC, TDS_RATE, INOPERATIVE, DORMANT, COMM_DED_RT, SERV_CHG,PARANAME,PARAID FROM AVS_DDS ";
        DT = new DataTable();
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;

    }
}