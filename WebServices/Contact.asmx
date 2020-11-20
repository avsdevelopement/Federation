<%@ WebService Language="C#" Class="AutoComplete" %>


using System;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

// import these namespaces
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "", BrCode = "", PrCode = "";

    public AutoComplete()
    {

    }
    [WebMethod]
    public string[] GetRecoveryDiviNames(string prefixText,string BRCD)
    {
        //string[] Spl = ContextKey.ToString().Split('_');
        //string BRCD = Spl[0].ToString();
        //string RECDIV = Spl[1].ToString();

        List<string> RNames = new List<string>();
        DataTable DT = new DataTable();
        try
        {
            DT = new DataTable();
            sql = "Select Convert(varchar(100),DESCR)+'_'+Convert(varchar(100),RECDIV) name,RECDIV id from PAYMAST where RECCODE='0' and DESCR Like '" + prefixText + "%'Order by RECCODE";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["name"].ToString(), DT.Rows[i]["id"].ToString());
                    RNames.Add(item);
                }
            }
        }
        catch (Exception Ex)
        {
            RNames.Add(Ex.Message);
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RNames.ToArray();
    }
    
    [WebMethod]
    public string[] GetRecoveryNames(string prefixText,string RECDIVBRCD)
    {
        string[] Spl = RECDIVBRCD.ToString().Split('_');
        string BRCD = Spl[1].ToString();
        string RECDIV = Spl[0].ToString();

        List<string> RNames = new List<string>();
        DataTable DT = new DataTable();
        try
        {
            DT = new DataTable();
            sql = "Select Convert(varchar(100),DESCR)+'_'+Convert(varchar(100),RECCODE) name,RECCODE id from PAYMAST where RECDIV='" + RECDIV + "' and BRCD='" + BRCD + "' and DESCR Like '" + prefixText + "%'Order by RECCODE";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["name"].ToString(), DT.Rows[i]["id"].ToString());
                    RNames.Add(item);
                }
            }
        }
        catch (Exception Ex)
        {
            RNames.Add(Ex.Message);
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RNames.ToArray();
    }
    [WebMethod]
    public string[] GetCustWiseName(string prefixText, string contextKey)
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, CustNo))) As Name From Master With(NoLock) Where Stage <> 1004 And CustName Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ListNames.ToArray();
    }

    [WebMethod]
    public string[] GetGlWiseName(string prefixText, string contextKey)
    {
        string[] Array = contextKey.ToString().Split('_');
      BrCode = Array[0].ToString();

        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (GlName+'_'+ ConVert(VarChar(10), ConVert(BigInt, GlCode))+'_'+ ConVert(VarChar(10), ConVert(BigInt, SubGlcode))) As Name From GlMast With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And GlName Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ListNames.ToArray();
    }

    [WebMethod]
    public string[] GetProdWiseName(string prefixText, string contextKey)
    {
        string[] Array = contextKey.ToString().Split('_');
        BrCode = Array[0].ToString();
        PrCode = Array[1].ToString();

        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name From Avs_Acc A With(NoLock) " +
                  "Inner Join Master M With(NoLock) On A.CustNo = M.CustNo " +
                  "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + PrCode + "' And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ListNames.ToArray();
    }

    [WebMethod()]
    public string[] GetMemberNames(string prefixText, string contextKey)
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select M.CustName+'_'+ConVert(VarChar(10), A.AccNo)+'_'+ConVert(VarChar(10), A.CustNo) As Name, A.CustNo From Master M With(NoLock) " +
                         "Inner Join Avs_Acc A With(NoLock) ON A.CustNo = M.CustNo Where A.BRCD = '1' AND A.SubGlCode = '4' And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CustNo"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod()]
    public string[] GetCustNames(string prefixText)//ankita 22/11/2017 brcd removed
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, CustNo))) Name From Master With(NoLock) Where Stage <> 1004 " +
                  "And CustName Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }

    [WebMethod()]
    public string[]  GetVendorName(string prefixText)//ankita 22/11/2017 brcd removed
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (VENDERNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, VENDORID))) Name From VendorMaster With(NoLock) Where Stage <> 1004 " +
                  "And VENDERNAME Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);
          
            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }
    [WebMethod()]
    public string[] GetProductName(string prefixText)//ankita 22/11/2017 brcd removed
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (PRODNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, PRODID))) Name From PRODUCTMASTER With(NoLock) Where Stage <> 1004 " +
                  "And PRODNAME Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }
    
    
    [WebMethod()]
    public string[] GetCustNamesBrcd(string prefixText)//amruta 19/01/2018 as per Ambika Madam for shivjyoti requirement
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, CustNo))+'_'+Convert(varchar(10),BRCD)) Name From Master With(NoLock) Where Stage <> 1004 " +
                  "And CustName Like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }


    [WebMethod()]
    public string[] GetBranch(string prefixText) //Amruta 05/06/2017
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "select MIDNAME+'_'+Convert(nvarchar(20),BRCD) as Name,BRCD From Bankname With(NoLock) Where BRCD not in (0,99) And MIDNAME LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["BRCD"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod()]
    public string[] GetMemNames(string prefixText, string contextKey)
    {

        List<string> contactNames = new List<string>();
        try
        {
            string sql = "";
            sql = "select USERNAME+'_'+Convert(nvarchar(20),PERMISSIONNO) as Name,PERMISSIONNO as ACCNO From UserMaster With(NoLock) Where LOGINCODE LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["ACCNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetCustNamesForShare(string prefixText, string contextKey)
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select (M.CustName +'_'+ Convert(Varchar(10),CONVERT(INT, M.CustNo)) +'_'+ Convert(Varchar(10), A.AppNo)) Name, M.CustNo from Master M With(NoLock) " +
                         "Inner Join Avs_ShrApp A With(NoLock) With(NoLock) On A.CustNo = M.CustNo " +
                         "Where M.BrCd = '" + contextKey + "' And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CustNo"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetGlName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name,SUBGLCODE FROM GLMAST With(NoLock) WHERE BRCD='" + BRCD + "' And GLNAME LIKE '%" + prefixText + "%' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetGlNameCommi(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name,SUBGLCODE FROM GLMAST With(NoLock) WHERE BRCD='" + BRCD + "' And GLNAME LIKE '%" + prefixText + "%' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetSuspGlName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select IsNull(SHARES_GL, 0) As SHARES_GL From AVS_SHRPARA Where BRCD = '" + BRCD + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA With(NoLock) Where BRCD = '" + BRCD + "')";
            int ShareSuspGl = Convert.ToInt32(conn.sExecuteScalar(sql));

            sql = "Select M.CustName+'_'+ConVert(VarChar(10), A.CustNo)+'_'+ConVert(VarChar(10), A.AccNo, 10) As Name, A.AccNo From AVS_Acc A With(NoLock) " +
                  "Inner Join Master M With(NoLock) On M.CustNo = A.CustNo " +
                  "Where A.BrCd = '" + BRCD + "' And A.GlCode = '4' And A.SubGlCode = '" + ShareSuspGl + "' And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["AccNo"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetGlInv(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select distinct (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, AM.SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name,AM.SUBGLCODE FROM AVS_InvAccountMaster AM With(NoLock) left join GLMAST g With(NoLock) on AM.subglcode=g.SUBGLCODE and AM.BRCD=g.BRCD where (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, AM.SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) is not null and g.GLNAME like '%" + prefixText + "%' and AM.BRCD='" + BRCD + "' order by am.subglcode";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetINVGlName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "SELECT (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name, SubGlCode FROM GLMAST With(NoLock) WHERE GLNAME LIKE '%" + prefixText + "%' AND BRCD='" + BRCD + "' and GLGroup='INV'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }


    [WebMethod]
    public string[] GetLoanGlName(string SearchText, string contextKey)
    {
        string BrCode = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select (GlName+'_'+ Convert(VarChar(10), ConVert(BigInt, SubGlCode)) +'_'+ Convert(VarChar(10), ConVert(BigInt, GlCode))) Name, SubGlCode From GlMast With(NoLock) Where BrCd = '" + BrCode + "' And GlGroup = 'LNV' And GlName Like '%" + SearchText + "%' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetAgName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select ConVert(VarChar(10), ConVert(BigInt, G.SubGlCode))+'_'+G.GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo)) Name, A.CustNo FROM GLMAST G With(NoLock) INNER JOIN AGENTMAST A With(NoLock) ON A.AGENTCODE = G.SUBGLCODE AND A.BRCD = G.BRCD WHERE G.GLCODE in (1,2,15) AND G.BRCD='" + BRCD + "' and G.GLNAME like'%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CUSTNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetAgNameDDSCL(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select ConVert(VarChar(10), ConVert(BigInt, G.SubGlCode))+'_'+G.GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo)) Name, A.CustNo FROM GLMAST G With(NoLock) INNER JOIN AGENTMAST A With(NoLock) ON A.AGENTCODE = G.SUBGLCODE AND A.BRCD = G.BRCD WHERE G.GLCODE in (2) AND G.BRCD='" + BRCD + "' and G.GLNAME like'%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CUSTNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetLoanCustName(string prefixText, string contextKey)
    {
        string[] QSTR = contextKey.ToString().Split('_');
        string BRCD = QSTR[0].ToString();// contextKey.ToString();
        string FAT = QSTR[1].ToString();

        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))) Name, A.AccNo " +
                  "From Avs_Acc A With(NoLock) " +
                  "Inner Join Master M With(NoLock) ON A.CustNo = M.CustNo " +
                  "Where A.BrCd = '" + BRCD + "' And A.GlCode = '3' And A.SubGlCode = '" + FAT + "' And A.Acc_Status != 3 And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["ACCNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetAccName(string prefixText, string contextKey)//ankita 22/11/2017 brcd removed
    {
        string[] QSTR1 = contextKey.ToString().Split('_');
        string BRCD = QSTR1[0].ToString();// contextKey.ToString();
        string FAT = QSTR1[1].ToString();

        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) Name, A.AccNo " +
                  "From Avs_Acc A With(NoLock) " +
                  "Inner Join Master M With(NoLock) ON A.CustNo = M.CustNo " +
                   "Where A.BrCd = '" + BRCD + "' And A.SubGlCode = '" + FAT + "' And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%'";
                  //"Where A.BrCd = '" + BRCD + "' And A.SubGlCode = '" + FAT + "' And A.Acc_Status != 3 And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["ACCNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }



    [WebMethod]
    public string[] GetDDSAcc(string prefixText, string contextKey)
    {
        string[] Array = contextKey.ToString().Split('-');
        BrCode = Array[0].ToString();
        PrCode = Array[1].ToString();

        string[] PS = PrCode.Split('_');
        string ProCd = PS[0].ToString();
        string FL = PS[1].ToString();
        
            
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            if (FL == "1111")
            {
                sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name From Avs_Acc A With(NoLock) " +
                      "Inner Join Master M With(NoLock) On A.CustNo = M.CustNo " +
                      "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + PrCode + "' And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%' ";
            }
            else
            {
                sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name From Avs_Acc A With(NoLock) " +
                      "Inner Join Master M With(NoLock) On A.CustNo = M.CustNo " +
                      "Where A.BrCd = '" + BrCode + "' And A.Glcode = '2' And A.Stage <> 1004 And M.CustName Like '%" + prefixText + "%' ";
            }
            DT = conn.GetDatatable(sql);
                

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ListNames.ToArray();
    }

    [WebMethod]
    public string[] GetUserName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "select (Logincode+'_'+Permissionno)Name,Permissionno from Usermaster With(NoLock) Where BRCD='" + BRCD + "' AND Logincode LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["Permissionno"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetBankName(string prefixText, string contextKey)
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "SELECT (LTRIM(RTRIM(DESCR))+'_'+CONVERT(VARCHAR(10),BANKRBICD)) DESCR ,BANKRBICD FROM RBIBANK With(NoLock) Where BRANCHRBICD='0' AND STATECD ='400' and DESCR LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["DESCR"].ToString(), DT.Rows[i]["BANKRBICD"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();

    }

    [WebMethod]
    public string[] GetBranchName(string prefixText, string contextKey)
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = sql = "SELECT (LTRIM(RTRIM(DESCR))+'_'+CONVERT(VARCHAR(10),BANKRBICD)) DESCR ,BANKRBICD FROM RBIBANK With(NoLock) Where BANKRBICD='" + BRCD + "'  AND STATECD ='400' and DESCR LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["DESCR"].ToString(), DT.Rows[i]["BANKRBICD"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();

    }

    [WebMethod]
    public string[] SearchCustname(string prefixText, string contextKey)
    {
        List<string> contactNames = new List<string>();
        string flag, YN;
        int br, subg;

        string[] QSTR = contextKey.ToString().Split('-');
        br = Convert.ToInt32(QSTR[0].ToString());
        flag = QSTR[1].ToString();
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MavsBanking"].ToString());
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SP_HSRECEIPT";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 2000;

                sqlCommand.Parameters.Add("@CUSTNAME", SqlDbType.VarChar).Value = prefixText;
                sqlCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = flag;
                sqlCommand.Parameters.Add("@CLID", SqlDbType.VarChar).Value = br;

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    string item = sqlDataReader["CUSTNO"].ToString() + "-" + sqlDataReader["CUSTNAME"].ToString() + "-(" + sqlDataReader["CFS"] + ")";
                    contactNames.Add(item);
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }

        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetSetno(string prefixText, string contextKey)
    {
        List<string> contactNames = new List<string>();
        string flag = "SHRSETNO";
        int br;
        string fdt, tdt;
        fdt = tdt = "";
        string[] QSTR = contextKey.ToString().Split('-');
        br = Convert.ToInt32(QSTR[0].ToString());
        fdt = QSTR[1].ToString();
        tdt = QSTR[2].ToString();
        string[] strArr = fdt.Split('/');
        if (strArr.Length == 3)
        {
            fdt = strArr[2].ToString() + "-" + strArr[1].ToString() + "-" + strArr[0].ToString();
        }
        else
        {
            fdt = fdt;
        }
        string[] strArr1 = tdt.Split('/');
        if (strArr1.Length == 3)
        {
            tdt = strArr1[2].ToString() + "-" + strArr1[1].ToString() + "-" + strArr1[0].ToString();
        }
        else
        {
            tdt = tdt;
        }

        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MavsBanking"].ToString());
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SP_HSRECEIPT";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 2000;

                sqlCommand.Parameters.Add("@CUSTNAME", SqlDbType.VarChar).Value = prefixText;
                sqlCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = flag;
                sqlCommand.Parameters.Add("@CLID", SqlDbType.VarChar).Value = br;
                sqlCommand.Parameters.Add("@CHQDT", SqlDbType.VarChar).Value = fdt;
                sqlCommand.Parameters.Add("@MCDATE", SqlDbType.VarChar).Value = tdt;

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    string item = sqlDataReader["SNO"].ToString();
                    contactNames.Add(item);
                }
            }

            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }

        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetAccNameForJoint(string prefixText, string contextKey)
    {
        string[] QSTR = contextKey.ToString().Split('_');
        string BRCD = QSTR[0].ToString();// contextKey.ToString();
        string FAT = QSTR[1].ToString();

        List<string> contactNames = new List<string>();
        try
        {
            sql = "SELECT (M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.ACCNO)+'_'+CONVERT(VARCHAR(10),AC.CUSTNO)+'_'+CONVERT(VARCHAR(5),AC.GLCODE)) Name,AC.ACCNO " +
                  "FROM MASTER M With(NoLock) INNER JOIN AVS_ACC AC With(NoLock) ON AC.CUSTNO=M.CUSTNO " +
                  "WHERE AC.BRCD='" + BRCD + "' AND AC.SUBGLCODE = '" + FAT + "' AND AC.OPR_TYPE = 2 AND M.CUSTNAME LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["ACCNO"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetPlGlName(string prefixText, string contextKey)//Dhanya Shetty for minimum charges(to get PL name)
    {

        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "SELECT (GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE)+'_'+CONVERT(VARCHAR(10),GLCODE)) Name,SUBGLCODE FROM GLMAST With(NoLock) WHERE GLNAME LIKE '%" + prefixText + "%' AND BRCD='" + BRCD + "' AND GLCODE=100";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetFAFGLName(string prefixText, string contextKey)//Dhanya Shetty for Dead stock(to get FAF name)15-06-2017
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "SELECT (GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE)+'_'+CONVERT(VARCHAR(10),GLCODE)) Name,SUBGLCODE FROM GLMAST WHERE GLNAME With(NoLock) LIKE '%" + prefixText + "%' AND BRCD='" + BRCD + "' AND GLGROUP='FAF' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }

    [WebMethod]
    public string[] GetGlNameL(string prefixText, string contextKey)
    {
        
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "SELECT (GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE)+'_'+CONVERT(VARCHAR(10),GLCODE)) Name,SUBGLCODE FROM GLMAST WHERE GLNAME With(NoLock) LIKE '%" + prefixText + "%' AND BRCD='" + BRCD + "' and glcode='3'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod()]
    public string[] GetMemNamesdash(string prefixText)////ankita on 04/08/2017 for member name for dashboard
    {
        List<string> contactNames = new List<string>();
        try
        {
            sql = "SELECT (M.CUSTNAME+'_'+CONVERT(VARCHAR(10),CONVERT(INT,AC.ACCNO))) Name,M.CustNo FROM MASTER M With(NoLock) " +
                  "INNER JOIN AVS_ACC AC With(NoLock) ON AC.CUSTNO=M.CUSTNO " +
                  "WHERE AC.BrCd = '1' And AC.glcode='4' And AC.Stage In ('1001','1002','1003') And M.CustName Like '%" + prefixText + "%' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CustNo"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetItemName(string prefixText)//Dhanya Shetty for Itemname search //06/09/2017
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = " SELECT (Item_name+'_'+CONVERT(VARCHAR(10),Item_code)) Name,Item_code FROM AVS5029 With(NoLock) WHERE Item_name LIKE '%" + prefixText + "%'  ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["Item_code"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetDivName(string prefixText)//ankita ghadage 09/09/2017 for division name of empdetails from paymast
    {
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "select (DESCR+'_'+convert(varchar(10),RECDIV)) Name,RECDIV from paymast With(NoLock) Where DESCR LIKE '%" + prefixText + "%' and RECCODE='0'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["RECDIV"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetOffcName(string prefixText, string contextKey)//ankita ghadage 09/09/2017 for Offc name of empdetails from paymast
    {
        string recdiv = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            string sql = "select (DESCR+'_'+convert(varchar(10),RECCODE)) Name,RECCODE from paymast With(NoLock) Where DESCR LIKE '%" + prefixText + "%' AND RECDIV='" + recdiv + "' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["RECCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod()]
    public string[] GetMemNamesSur(string prefixText, string contextKey)////ankita on 29/09/2017 for member name for add surity
    {
        string[] type = contextKey.ToString().Split('_');
        List<string> contactNames = new List<string>();
        string sql = "";
        try
        {
            if (type[0].ToString() == "1")
                sql = "SELECT (M.CUSTNAME+'_'+CONVERT(VARCHAR(10),CONVERT(INT,AC.ACCNO))) Name,M.CustNo FROM MASTER M With(NoLock) INNER JOIN AVS_ACC AC With(NoLock) ON AC.CUSTNO=M.CUSTNO WHERE AC.BrCd = '1' And AC.Stage In ('1001','1002','1003') And M.CustName Like '%" + prefixText + "%' and AC.glcode='4' ";
            else
                sql = "SELECT (M.CUSTNAME+'_'+CONVERT(VARCHAR(10),CONVERT(BIGINT,M.CUSTNO))) Name,M.CustNo FROM MASTER M With(NoLock) WHERE M.Stage In ('1001','1002','1003') And M.CustName Like '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["CustNo"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    
    [WebMethod()]
    public string[] GetCustNamesf7(string prefixText)
    {
        List<string> ListNames = new List<string>();
        DataTable DT = new DataTable();
        try
        {
            sql = "Select M.CustName +'_'+ Cast(Ac.CustNo As VarChar(30)) +'_'+ Cast(Ac.AccNo As VarChar(30)) As Name From Avs_Acc Ac " +
                  "Inner Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                  "Where Ac.SubGlCode = '4' And Ac.Acc_Status <> '3' And M.Stage <> '1004' " +
                  "And CustName Like '%" + prefixText + "%' ";
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ListNames.ToArray();
    }
    
    [WebMethod]
    public string[] GetGlNamef7(string prefixText)
    {

        List<string> contactNames = new List<string>();
        try
        {
            string sql = "Select distinct (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name,SUBGLCODE FROM GLMAST With(NoLock) WHERE GLNAME LIKE '%" + prefixText + "%'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod]
    public string[] GetGlNamePT(string prefixText, string contextKey)//Dhanya  Shetty//08/03/2018
    {
        string BRCD = contextKey.ToString();
        List<string> contactNames = new List<string>();
        try
        {
            sql = "Select (GLNAME+'_'+ ConVert(VarChar(10), ConVert(BigInt, SUBGLCODE))+'_'+ ConVert(VarChar(10), ConVert(BigInt, GLCODE))) Name,SUBGLCODE FROM GLMAST With(NoLock) WHERE BRCD='" + BRCD + "' And GLNAME LIKE '%" + prefixText + "%' and  glgroup <> 'CBB' ";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DT.Rows[i]["Name"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString());
                    contactNames.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            contactNames.Add(ex.Message);
        }
        return contactNames.ToArray();
    }
    [WebMethod()]
    public string[] GetCustNamesMob(string prefixText)//Dhanya Shetty//06/07/2018//To fetch custname from mobapps
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            sql = "Select (NAME+'_'+ ConVert(VarChar(20), ConVert(BigInt, CUST_REF_NO))) Name From avs5039 With(NoLock) Where " +
                  " NAME Like '%" + prefixText + "%'";
            DT = conn.GetDatatableMob(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }

    [WebMethod()]
    public string[] GetMenutitle(string prefixText)//Dhanya Shetty//06/07/2018//To fetch custname from mobapps
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            //sql = "Select MenuTitle+'_'+ ConVert(VarChar(20), ConVert(Int, [ParentMenuId])) Name  From avs5016 With(NoLock) Where " +
            //      " MenuTitle Like '%" + prefixText + "%'";

            sql = "select  dbo.MenuName(menuid)+ '#' +cast(menuid as nvarchar)  Name  from AVS5016 c where  PageURL not like '%aspx%' and  MenuTitle like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }

    [WebMethod()]
    public string[] GetMenutitleID(string prefixText)//Dhanya Shetty//06/07/2018//To fetch custname from mobapps
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            // sql = " Select ConVert(VarChar(10),ParentMenuId)+'_' +ConVert(VarChar(10), ConVert(VarChar(100), [MenuTitle])) Name  From avs5016 With(NoLock) Where  ConVert(VarChar(10),ParentMenuId) Like '%" + prefixText + "%'";
            sql = "select cast(menuid as nvarchar) + '#' + dbo.MenuName(menuid) Name  from AVS5016 c where  PageURL not like '%aspx%' and  MenuID like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }

    [WebMethod()]
    public string[] GetMenuForm(string prefixText)//Dhanya Shetty//06/07/2018//To fetch custname from mobapps
    {
        List<string> ListNames = new List<string>();
        try
        {
            DT = new DataTable();
            //sql = "Select MenuTitle+'_'+ ConVert(VarChar(20), ConVert(Int, [ParentMenuId])) Name  From avs5016 With(NoLock) Where " +
            //      " MenuTitle Like '%" + prefixText + "%'";

            sql = "select  dbo.MenuName(menuid)+ '#' +cast(menuid as nvarchar)  Name  from AVS5016  where   MenuTitle like '%" + prefixText + "%'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
    }
    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod]
    public string[] GetIDVendorName(string prefixText)
    {
        List<string> ListNames = new List<string>();

        try
        {
            DT = new DataTable();
            //sql = "Select MenuTitle+'_'+ ConVert(VarChar(20), ConVert(Int, [ParentMenuId])) Name  From avs5016 With(NoLock) Where " +
            //      " MenuTitle Like '%" + prefixText + "%'";

            sql = "SELECT CAST(VENDORID AS NVARCHAR(30)) +''_''+VENDERNAME FROM VendorMaster WHERE VENDORID LIKE '%" + prefixText + "%' AND STAGE<>1004";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow row in DT.Rows)
                    ListNames.Add(row["Name"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ListNames.Add(Ex.Message);
        }
        return ListNames.ToArray();
        
        
      
    }
}


