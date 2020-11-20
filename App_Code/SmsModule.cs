using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for SmsModule
/// </summary>
public class SmsModule
{
    public SmsModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    string sql = "";
    private Double Balance = 0.0F;
    private static string _MessageFormat;
    public string Message = "";

    public SmsModule(string BankName, string BranchName)
    {
        SMSBalanceFormat(BankName, BranchName);
    }


    public static string MessageFormat
    {
        get
        {
            return _MessageFormat;
        }

    }
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    public void GlCodeBind(DropDownList ddlPrdType)
    {
        try
        {

            sql = "select SUBGLCODE ID,GLNAME+'  ('+Convert(varchar,SUBGLCODE)+')' Name from glmast where GLCODE In('1','3','4','5')";
            conn.FillDDL2(ddlPrdType, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GlCodeBindForDirector(DropDownList ddlPrdType)
    {
        try
        {

            sql = "select SUBGLCODE ID,GLNAME+'  ('+Convert(varchar,SUBGLCODE)+')' Name from glmast where GLCODE In('1','3','4','5')";
            conn.FillDDL2(ddlPrdType, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    #region For Converting Code to Base16BE

    public string ConvertToUnicode(string str)
    {
        string message = HttpUtility.UrlEncode(str);
        byte[] ArrayOfBytes = System.Text.Encoding.Unicode.GetBytes(str);
        string UnicodeString = "";
        int v;
        for (v = 0; v <= ArrayOfBytes.Length - 1; v++)
        {
            if (v % 2 == 0)
            {
                int t = ArrayOfBytes[v];
                ArrayOfBytes[v] = ArrayOfBytes[v + 1];
                ArrayOfBytes[v + 1] = (byte)t;
            }
        }

        for (v = 0; v <= ArrayOfBytes.Length - 1; v++)
        {
            //  string c = Hex(ArrayOfBytes[v]);
            string c = ArrayOfBytes[v].ToString("X"); ;

           
            if (c.Length == 1)
                c = "0" + c;
            UnicodeString = UnicodeString + c;
        }

        return UnicodeString;
    }

    #endregion
    public void GlCodeBindForBal(DropDownList ddlPrdType)
    {
        try
        {

            sql = "select SUBGLCODE ID,GLNAME+'  ('+Convert(varchar,SUBGLCODE)+')' Name from glmast where GLCODE In('1','3','4','5')";
            conn.FillDDL2(ddlPrdType, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public DataTable GetAllGlCode()
    {
        try
        {

            sql = "select SUBGLCODE ,GLNAME Name from glmast where GLCODE In('1','3','4','5')";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }

    public DataTable GetAllDirectorNo()
    {
        try
        {

            sql = "select DIRNAME,POST,MOBILENO Mobile1,CUSTNO ACCNO ,'1' BRCD from DIRECTOR where SMSYN='Y'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }

    public DataTable GetAllGlCodeForBal()
    {
        try
        {

            sql = "select SUBGLCODE ,GLNAME Name from glmast where GLCODE In('1','3','4','5')";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }


    public int SendSMS(string BRCD, string SubGlCode, string Accno, string EntryDate, string MID)
    {
        int SmsStatus = 0;
        try
        {
            Balance = BD.ClBalance(BRCD, SubGlCode.Trim().ToString(), Accno.Trim().ToString(), EntryDate, "ClBal");
            Message = "Your a/c no " + Convert.ToInt32(Accno.Trim().ToString()) + " has been debited with Rs " + Convert.ToDouble(SubGlCode.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
            SmsStatus = BD.InsertSMSRec(BRCD.ToString(), SubGlCode.Trim().ToString(), Accno.Trim().ToString(), Message, MID.ToString(), MID.ToString(), EntryDate.ToString(), "Payment");
        }
        catch (Exception Ex)
        {
            SmsStatus = 0;
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SmsStatus;

    }



    public static string SMSBalanceFormat(string BankName, string BranchName)
    {
        try
        {
            _MessageFormat = "Your a/c no " + "1/10" + "  Current Balance is Rs " + "910" + ".Thank You. From " + BankName + "( " + BranchName + " )";

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return MessageFormat;

    }
    public DataTable GetSpecificGlCode()
    {
        try
        {

            sql = "select SUBGLCODE ,GLNAME Name from glmast where GLCODE In('1','3','4','5')";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }
    public DataTable GetAllCustomer(string BRCD)
    {
        try
        {
            sql = "   With CTE as " +
                  "(select  ROW_NUMBER() over (Partition by A.CustNo order by A.CustNo) as Row_Num,	 A.accno,A.BRCD,C.MOBILE1,A.CustNo,A.SubGlCode from Avs_Acc  A left join Avs_Contactd  C on C.Custno=A.CUSTNO where A.ACC_STATUS not in ('3','99') and ACCNO<>'0'" +
                  "and A.GLCODE In('1','3','4','5')   )" +
                  " select * from CTE where Row_Num=1";

            // sql = "  select A.accno,A.BRCD,C.MOBILE1,A.CustNo,A.SubGlCode from Avs_Acc  A left join Avs_Contactd  C on C.Custno=A.CUSTNO where A.ACC_STATUS not in ('3','99') and ACCNO<>'0' and  A.GLCODE In('1','3','4','5') ";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }



    public DataTable GetAllGlRecord(string CustNo, string BRCD)
    {
        try
        {

            sql = "select SubGlCode,AccNo,BRCD from avs_Acc where CUSTNO='" + CustNo + "' and acc_status<>'9'  and SUBGLCODE<>'0' and BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            DT = null;
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }
    public DataTable GetSpecificCustomer(string SubglCode, string FromCust, string ToCust, string BRCD)
    {
        try
        {
            if (FromCust == "" && ToCust == "")
            {
                sql = " select A.accno,A.BRCD,C.MOBILE1 from Avs_Acc  A left join Avs_Contactd  C on C.Custno=A.CUSTNO where A.Subglcode='" + SubglCode + "' and C.Mobile1<>'0' and A.ACC_STATUS<>'3' and A.BRCD='" + BRCD + "' ";
            }
            else if (FromCust != "" && ToCust == "")
            {
                sql = " select A.accno,A.BRCD,C.MOBILE1 from Avs_Acc  A left join Avs_Contactd  C on C.Custno=A.CUSTNO where A.Subglcode='" + SubglCode + "' and A.accno='" + FromCust + "' and Mobile1<>'0' and A.ACC_STATUS<>'3' and A.BRCD='" + BRCD + "' ";
            }
            else if (FromCust != "" && ToCust != "")
            {
                sql = " select A.accno,A.BRCD,C.MOBILE1 from Avs_Acc  A left join Avs_Contactd  C on C.Custno=A.CUSTNO where A.Subglcode='" + SubglCode + "' and A.accno between '" + FromCust + "' and '" + ToCust + "' and Mobile1<>'0' and A.ACC_STATUS<>'3' and A.BRCD='" + BRCD + "'";
            }
            DT = conn.GetDatatable(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }
    public DataTable GetCustName(string SubglCode, string FromCust)
    {
        try
        {

            sql = " select * from master M left join avs_acc AC on AC.BRCD=M.BRCD and AC.Custno=M.CustNo Where AC.ACCNO='" + FromCust + "' and AC.Subglcode='" + SubglCode + "'";
            DT = conn.GetDatatable(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }

    public DataTable GetCustNameByGlCode(string GlCode, string FromCust)
    {
        try
        {

            sql = " select * from master M left join avs_acc AC on AC.BRCD=M.BRCD and AC.Custno=M.CustNo Where AC.ACCNO='" + FromCust + "' and AC.Glcode='" + GlCode + "'";
            DT = conn.GetDatatable(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;


    }
}