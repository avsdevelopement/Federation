using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FedSub
/// </summary>
public class FedSub
{
    DbConnection conn;
    string sqlQuery;
    private readonly string _SPNAME = "USP_FEDSUB";
    public FedSub()
    {
        conn = new DbConnection();
    }
    public DataTable GetDefaultValues()
    {
        DataTable def = new DataTable();

        try
        {
            sqlQuery = "EXEC USP_FEDSUB @FLAG='GETDEFAULT'";
            def = conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return def;
    }
    public DataTable GetCustomerDetails(string Memtype, string MemNo)
    {
        DataTable customerDetails = new DataTable();

        try
        {
            sqlQuery = "SELECT CUSTNAME,M.STAGE,A.BRCD FROM AVS_ACC A INNER JOIN MASTER M ON M.CUSTNO=A.CUSTNO  WHERE  A.ACCNO='" + MemNo + "' AND A.SUBGLCODE='" + Memtype + "' AND M.STAGE<>'1004'";
            //sqlQuery = "select CUSTNAME,STAGE,BRCD from master where CUSTNO='" + custNo + "' and STAGE<>'1004'";
            customerDetails = conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return customerDetails;
    }


    public DataTable GetReceiptDetails(string id, string MEMTYPE, string MEMNO)
    {
        DataTable receiptDetails = new DataTable();

        try
        {
            sqlQuery = "EXEC USP_AVS_FEDSUB_RECEIPT @TRXID=" + id + "  ,@MEMTYPE=" + MEMTYPE + "  ,@MEMNO=" + MEMNO;
            receiptDetails = conn.GetDatatable(sqlQuery);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return receiptDetails;
    }

    public DataTable GetGlDetails(string subGlCode, string brcd)
    {
        DataTable glDetails = new DataTable();

        try
        {
            sqlQuery = "select GLCODE,SUBGLCODE,GLGROUP,GLNAME from GLMAST where SUBGLCODE=" + subGlCode + " and BRCD=" + brcd;
            glDetails = conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return glDetails;
    }

    public DataTable GetGstDetails(string brcd)
    {
        DataTable gstDetails = new DataTable();

        try
        {
            sqlQuery = "EXEC USP_FEDSUB @FLAG='GET_GST',@BRCD='" + brcd + "'";
            gstDetails = conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return gstDetails;
    }


    public DataTable GetstCalculation(string amount, string cgst, string sgst)
    {
        DataTable gstDetails = new DataTable();

        try
        {
            sqlQuery = "EXEC USP_FEDSUB @FLAG='CALC_AMT',@AMOUNT=" + amount + ",@SGST='" + sgst + "',@CGST='" + cgst + "'";
            gstDetails = conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return gstDetails;
    }

    public DataTable GetCustomerStatement(string MEMTYPE, string MEMNO)
    {
        try
        {
            sqlQuery = "EXEC USP_FEDSUB @FLAG='STATEMENT',@MEMTYPE=" + MEMTYPE + ",@MEMNO=" + MEMNO;
            return conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return null;
    }

    public DataTable GetBalance(string MEMTYPE, string MEMNO)
    {
        try
        {
            sqlQuery = "EXEC USP_FEDSUB @FLAG='BALANCE',@MEMTYPE=" + MEMTYPE + ",@MEMNO=" + MEMNO;
            return conn.GetDatatable(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return null;
    }

    public string GetBankName(string bankCode)
    {
        string bankName = String.Empty;

        try
        {
            sqlQuery = "SELECT Top 1 DESCR FROM rbibank WHERE BANKCD='" + bankCode + "' AND BRANCHCD =0 ";
            bankName = conn.sExecuteScalar(sqlQuery);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return bankName;
    }

    public string GetBranchName(string bankCode, string branchCode)
    {
        string branchName = String.Empty;

        try
        {
            sqlQuery = "SELECT Top 1 DESCR FROM rbibank WHERE BANKCD ='" + bankCode + "' AND BRANCHCD ='" + branchCode + "'";
            branchName = conn.sExecuteScalar(sqlQuery);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return branchName;
    }
    public void SetGSTDetails(string isGst, string subGlCode, string narration, string AMT, string gstRate, string sgst, string cgst, string SGSTAMT, string CGSTAMT, string GSTAMT, string AMOUNT, ref DataTable gstData)
    {
        try
        {
            if (gstData.Columns.Count == 0)
            {
                gstData.Columns.Add("GST_YN");
                gstData.Columns.Add("SUBGLCODE");
                gstData.Columns.Add("PARTICULARS");
                gstData.Columns.Add("AMT");
                gstData.Columns.Add("GSTRATE");
                gstData.Columns.Add("SGST");
                gstData.Columns.Add("CGST");
                gstData.Columns.Add("SGSTAMT");
                gstData.Columns.Add("CGSTAMT");
                gstData.Columns.Add("GSTAMT");
                gstData.Columns.Add("AMOUNT");
            }

            DataRow dr = gstData.NewRow();
            dr[0] = isGst;
            dr[1] = subGlCode;
            dr[2] = narration;
            dr[3] = AMT;
            dr[4] = gstRate;
            dr[5] = sgst;
            dr[6] = cgst;
            dr[7] = SGSTAMT;
            dr[8] = CGSTAMT;
            dr[9] = GSTAMT;
            dr[10] = AMOUNT;
            gstData.Rows.Add(dr);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }

    }

    public int SaveDetails(string MemberType, string MemberNo, string brcd, string entryDate, string FROMPERIOD, string TOPERIOD, string mid, string debitSubGlCode,
        string paymentMode, string narration, string instrumentNo, string chequeDate, string bankCode, string branchCode, DataTable gstDetails)
    {

        var previousYear = DateTime.Now.AddYears(-1).Year.ToString();
        var currentYear = DateTime.Now.Year.ToString().Substring(2, 2);
        int setNo = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(conn.DbName()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = _SPNAME;
                    command.Parameters.AddWithValue("@flag", "INSERT");
                    command.Parameters.AddWithValue("@BRCD", brcd);
                    command.Parameters.AddWithValue("@MEMTYPE", MemberType);
                    command.Parameters.AddWithValue("@MEMNO", MemberNo);
                    command.Parameters.AddWithValue("@ENTRYDATE", conn.ConvertDate(entryDate));
                    command.Parameters.AddWithValue("@FROMPERIOD", conn.ConvertDate(FROMPERIOD));
                    command.Parameters.AddWithValue("@TOPERIOD", conn.ConvertDate(TOPERIOD));
                    command.Parameters.AddWithValue("@FINACIALYEAR", previousYear + "-" + currentYear);
                    command.Parameters.AddWithValue("@MID", mid);
                    command.Parameters.AddWithValue("@DR_PRDCODE", debitSubGlCode);
                    command.Parameters.AddWithValue("@PMTMODEID", paymentMode);
                    command.Parameters.AddWithValue("@DR_PARTICULAR", narration);
                    command.Parameters.AddWithValue("@INSTNO", instrumentNo);
                    command.Parameters.AddWithValue("@INSTBRCD", branchCode);
                    command.Parameters.AddWithValue("@INSTBANKCD ", bankCode);
                    command.Parameters.AddWithValue("@INSTRUMENTDATE ", conn.ConvertDate(chequeDate));
                    command.Parameters.Add("@FEDSUB_TYPE", SqlDbType.Structured).Value = gstDetails;

                    if (connection.State == ConnectionState.Closed)
                    {

                        connection.Open();
                    }

                    setNo = Convert.ToInt32(command.ExecuteScalar());

                }
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return setNo;
    }


}