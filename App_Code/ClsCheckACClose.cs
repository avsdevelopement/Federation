using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for ClsCheckACClose
/// </summary>
public class ClsCheckACClose
{
    string sql="",ResultStr="";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsAuthorized AT = new ClsAuthorized();
	public ClsCheckACClose()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetLabel(string Fl,string BRCD)
    {
        string CR="";
        try
        {
            if (Fl == "UCC")
                CR = "CHARGESTYPE =1";
            else if (Fl == "ECC")
                CR = " CHARGESTYPE =2";
            else if (Fl == "SC")
                CR = " CHARGESTYPE =3";
            else if (Fl == "ST")
                CR = " CHARGESTYPE =4";
            else if (Fl == "CA")
                CR = " CHARGESTYPE =5";
            else if (Fl == "BC")
                CR = " CHARGESTYPE =6";
           

            sql = "SELECT DESCRIPTION FROM CHARGESMASTER WHERE " + CR + ""; //BRCD NOT ADDED BECAUSE TABLE DOESNT CONTAIN BRCD --Abhishek
            ResultStr = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResultStr;
    }

    public string GetIA_LBL(string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE=564 AND GLCODE=100 AND BRCD='" + BRCD + "'";//BRCD ADDED --Abhishek
            ResultStr = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResultStr.Replace("SAVING DEPOSIT","SB");
    }

    public int Post_Charges(string MAXCHRGS,string OTHERCHRGS,string INTAPP,string ECC,string UCC,string SC,string ST,string CA,string BCTT,string CRCC,string BRCD,string EDT,string MID,string CUSTNO,string ACCNAME,string SETNO,string ACCNO,string GLCODE,string SUBGL)
    {
        try
        {
            double TotalChrDebit = Convert.ToDouble(MAXCHRGS) + Convert.ToDouble(OTHERCHRGS) + Convert.ToDouble(ECC) + Convert.ToDouble(UCC) +
                                Convert.ToDouble(SC) + Convert.ToDouble(ST) + Convert.ToDouble(CA) + Convert.ToDouble(BCTT) + Convert.ToDouble(CRCC);
            if (TotalChrDebit != 0)
            {
                Result = AT.Authorized(EDT, EDT, EDT, GLCODE, SUBGL,
                                      ACCNO, "BY TRF", "BY TRF", TotalChrDebit.ToString(), "2", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                      BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1","0");
            }

            if (MAXCHRGS != "0")
            {
                
                    Result = AT.Authorized(EDT, EDT, EDT, "100", "576",
                                           "576", "BY TRF", "BY TRF", MAXCHRGS, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                           BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
                
            }
            if (OTHERCHRGS != "0")
            {
                    Result = AT.Authorized(EDT, EDT, EDT, "100", "575",
                                         "575", "BY TRF", "BY TRF", OTHERCHRGS, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                          BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (INTAPP!= "0")
            {
                Result = AT.Authorized(EDT, EDT, EDT, "100", "564",
                                    ACCNO, "BY TRF", "BY TRF", INTAPP, "2", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                    BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
                if (Result > 0)
                {
                    Result = AT.Authorized(EDT, EDT, EDT, GLCODE, SUBGL,
                                   ACCNO, "BY TRF", "BY TRF", INTAPP, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                   BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
                }
            }
            if (UCC != "0")
            { 
                   Result = AT.Authorized(EDT, EDT, EDT, "100", "571",
                                        "571", "BY TRF", "BY TRF", UCC, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                        BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (SC != "0")
            { 
                    Result = AT.Authorized(EDT, EDT, EDT, "100", "527",
                                        "527", "BY TRF", "BY TRF", SC, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                        BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (ECC!= "0")
            {
                     Result = AT.Authorized(EDT, EDT, EDT, "100", "572",
                                         "572", "BY TRF", "BY TRF", ECC, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                         BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (ST!= "0")
            {
                     Result = AT.Authorized(EDT, EDT, EDT, "100", "546",
                                         "546", "BY TRF", "BY TRF", ST, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                         BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (CA!= "0")
            {
                     Result = AT.Authorized(EDT, EDT, EDT, "100", "573",
                                         "573", "BY TRF", "BY TRF", CA, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                         BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (BCTT!= "0")
            {
                     Result = AT.Authorized(EDT, EDT, EDT, "100", "574",
                                         "574", "BY TRF", "BY TRF", BCTT, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                        BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
            if (CRCC!= "0")
            {
                     Result = AT.Authorized(EDT, EDT, EDT, "100", "577",
                                         "577", "BY TRF", "BY TRF", CRCC, "1", "7", "CHRGS-TRF", SETNO, "0", "1900/01/01", "0", "0", "1001", EDT,
                                         BRCD, MID, "0", "0", "TR", CUSTNO, ACCNAME, "1", "0");
            
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int Operate_Closure(string BRCD, string EDT, string SUBGL, string ACCNO)
    {
        try
        {
            sql = "UPDATE AVS_ACC SET ACC_STATUS=3, STAGE=1004 ,CLOSINGDATE='" + conn.ConvertDate(EDT) + "' WHERE SUBGLCODE='" + SUBGL + "' AND ACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}