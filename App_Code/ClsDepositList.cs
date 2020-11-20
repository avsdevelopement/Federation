using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsDepositList
/// </summary>
public class ClsDepositList
{
    DataTable Dt =new DataTable();
    DbConnection Conn= new DbConnection();

	public ClsDepositList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable DepositList(string FBC, string GL, string FDate, string FD) 
    {
        try
        {
            Dt = Conn.GetDatatable("Exec RptTopDepositList '" + FBC + "' ,'" + GL + "' ,'" + Conn.ConvertDate(FDate).ToString() + "' ,'" + FD + "' ");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}