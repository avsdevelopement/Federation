using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsSMSRecovery
/// </summary>
public class ClsSMSRecovery
{
    string sql = "", sQuery = "",StrResultCn="",StrResultMN="";
    int Result = 0;
    DbConnection conn = new DbConnection();
    Mobile_Service MS = new Mobile_Service();
	public ClsSMSRecovery()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertSMSRecAutho(string BrCode,string Custno, string Message, string CreateMid, string AuthMid, string EDate, string sFlag)
    {
        try
        {
            if (sFlag == "RecoAuthorize")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'RecoAuthorize' And Activity = 'SMSRecoAutho' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }

            if (sQuery == "Y")
            {
                sql = "Select Count(*) From Avs_Acc Where BrCd = '" + BrCode + "' And CUSTNO= '" + Custno + "'";
                StrResultCn = conn.sExecuteScalar(sql);

                if (StrResultCn != null && StrResultCn != "")
                {
                    sql = "Select Mobile1 From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + Custno + "' And Stage = '1003' And EffectDate = (Select Max(EffectDate) From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + Custno + "' And Stage = '1003')";
                    StrResultMN = conn.sExecuteScalar(sql);

                    if (StrResultMN != null && StrResultMN != "")
                    {
                        sql = "Insert Into AVS1092 (CUSTNO, MOBILE, SMS_DATE, SMS_TYPE, SMS_DESCRIPTION, SMS_STATUS, BRCD, MID, VID, PCMAC, SYSTEMDATE) " +
                            "Values('" + StrResultCn + "','" + StrResultMN + "','" + conn.ConvertDate(EDate) + "','1','" + Message + "', '1', '" + BrCode + "', '" + CreateMid + "', '" + AuthMid + "', '" + conn.PCNAME() + "', GetDate())";
                        Result = conn.sExecuteQuery(sql);

                        //for shoot sms to customer
                        string SMS = MS.Send_SMS(StrResultCn, EDate);
                        //AddSMS_Desc(BrCode.ToString(), sCustNo.ToString(), Mid, EDate.ToString(), sMobNo.ToString(), "MOD");
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}