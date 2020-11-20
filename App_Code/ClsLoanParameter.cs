using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanParameter
{
    string sql = "";
    int Result;
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsLoanParameter()
	{
		
	}

    public DataTable GetData(string BRCD)//BRCD ADDED --Abhishek
    {

        try
        {
            sql = "select LOANGLCODE,LOANTYPE,LOANCATEGORY,HOGLMAST,ROI,PERIOD,PGL,PPL,INTCALTYPE,CURRANCY,LOANLIMIT,PENALINT,STATUS,EFFECTIVEDATE,INTREC,LASTINTAPP,OVERRESERVEGL from LOANGL where BRCD='" + BRCD + "' ";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDataDeposit(string BRCD) //BRCD ADDED --Abhishek
    {

        try
        {
            sql = "select DEPOSITGLCODE,DEPOSITTYPE,DEPOSITGLBALANCE,STATUS,CATEGORY,INTERESTTYPE1,INTERESTTYPE2,PLACCNO from DEPOSITGL where BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}