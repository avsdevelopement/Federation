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

public class ClsLoanGLCreation
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();

	public ClsLoanGLCreation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert_GL(string FL, string SFL, string BRCD, string LSUBGL, string GLNAME, string CATEG, string INTTYPE, string AMT, string SHORTNM,
                        string ROI, string PERIOD, string IRCD, string IORCD, string IRNAME, string IORNAME,string PLACCNO,string LASTNO,string PGL,string PPL,string OTHCHG)
    {
        try
        {
            if (FL == "LOANMASTER")
            {
                if (SFL == "AD")
                {
                    sql = "exec SP_GLMASTERADD @FLAG ='" + FL + "',@SUBFLAG ='" + SFL + "',@LOANCODE ='3', @LOANNM ='" + GLNAME + "', @LOANAMT ='" + AMT + "', @LOANCAT ='" + CATEG + "', " +
                            " @SUBGLCODE ='" + LSUBGL + "',  @BRCD ='" + BRCD + "', @PLACCNO='" + PLACCNO + "',  @LASTNO ='" + LASTNO + "',  @IR ='" + IRCD + "',  @IOR ='" + IORCD + "', @ROI ='" + ROI + "', " +
                            " @PERIOD ='" + PERIOD + "', @PGL ='" + PGL + "', @PPL ='" + PPL + "', @OTHCHG ='" + OTHCHG + "', @REPORTNM ='" + SHORTNM + "', @IRGLNAME ='" + IRNAME + "', @IORGLNAME ='" + IORNAME + "'";
                }
                else if (SFL == "MD")
                {
                    sql = "exec SP_GLMASTERADD @FLAG ='" + FL + "',@SUBFLAG ='" + SFL + "',@LOANCODE ='3', @LOANNM ='" + GLNAME + "', @LOANAMT ='" + AMT + "', @LOANCAT ='" + CATEG + "', " +
                            " @SUBGLCODE ='" + LSUBGL + "',  @BRCD ='" + BRCD + "', @IR ='" + IRCD + "',  @IOR ='" + IORCD + "', @ROI ='" + ROI + "', " +
                            " @PERIOD ='" + PERIOD + "', @REPORTNM ='" + SHORTNM + "', @IRGLNAME ='" + IRNAME + "', @IORGLNAME ='" + IORNAME + "'";
                }
                else if (SFL == "DL")
                {
                    sql = "exec SP_GLMASTERADD @FLAG ='" + FL + "',@SUBFLAG ='" + SFL + "',@LOANCODE ='3'," +
                           " @SUBGLCODE ='" + LSUBGL + "',  @BRCD ='" + BRCD + "'";
                            
                }
            }

            else if (FL == "DEPMASTER")
            {
            }
            else if (FL == "GLMASTER")
            {
            }
            Result = DBconn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}