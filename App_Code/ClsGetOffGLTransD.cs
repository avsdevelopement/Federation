using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetOffGLTransD
/// </summary>
public class ClsGetOffGLTransD
{
    DataTable Dt = new DataTable();
    DataSet DS = new DataSet();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetOffGLTransD()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet GetOffGLTransDReg(string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = FDate.ToString().Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TDate.ToString().Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

            sql = "Exec SP_OFFICEACCSTATUS_R '" + fmonth + "','" + tmonth + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "','" + fyear + "','" + tyear + "','" + PT + "','" + FBC + "'";
            Dt = Conn.GetDatatable(sql);

            if (Dt.Rows.Count > 0)
                DS.Tables.Add(Dt);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet GetOffGLTransDReg_BrAdj(string FBC, string FDate, string TDate)
    {
        try
        {
            string fmonth, fyear;
            string tmonth, tyear;

            string[] fdate = FDate.ToString().Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();

            string[] tdate = TDate.ToString().Split('/');
            tmonth = tdate[1].ToString();
            tyear = tdate[2].ToString();

            sql = "Exec SP_OFFICEACCSTATUS_BrAdj '" + fmonth + "','" + tmonth + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "','" + fyear + "','" + tyear + "','" + FBC + "'";
            Dt = Conn.GetDatatable(sql);

            if (Dt.Rows.Count > 0)
                DS.Tables.Add(Dt);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataTable GetOffGLTransDRegSumry (string FBC, string PT, string FDate, string TDate)
    {
        try
        {
                string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = FDate.ToString().Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TDate.ToString().Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

            sql = "Exec SP_OFFICEACCSTATUS_R '" + fmonth + "','" + tmonth + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "','" + fyear + "','" + tyear + "','" + PT + "','" + FBC + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}