using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;

public partial class FrmLogin : System.Web.UI.Page
{
    ClsLogin LG = new ClsLogin();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DbConnection conn = new DbConnection();
    string cmd = "";
    string sResult = "", FL = "", OC = "", Host = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lbl_Error.Visible = false;
            if (Request.QueryString["FL"] != null)
            {
                if (Session["UGRP"] != null)
                {
                    OC = "0";
                    int RC = LG.UpdateLoginsts(Session["UID"].ToString(), encrypt(Session["PWD"].ToString()), OC, Session["BRCD"].ToString());
                    Logout();
                }

            }
            if (Request.QueryString["UG"] != "1" && Request.QueryString["BD"] != null && Request.QueryString["LG"] != "1")
            {
                int TTT = ForceLogout(Request.QueryString["BD"].ToString(), Request.QueryString["UG"].ToString(), Request.QueryString["LG"].ToString());
                if (TTT > 0)
                {
                    WebMsgBox.Show("User " + Request.QueryString["LG"].ToString() + " is Logged out due to Inactivity...!", this.Page);
                    return;
                }
            }
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove
            this.Request.QueryString.Remove("FL");
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        sResult = LG.CheckLoginStage(Userid.Value, encrypt(pass.Value));
        if (sResult == "1004")
        {
            WebMsgBox.Show("Login Failed!! User is Suspended!!", this.Page);
            return;
        }
        else
        {
            //Below commented by Abhishek as per requirement on 19-02-2018
            string RC = LG.CheckLoginStatus(Userid.Value, encrypt(pass.Value));
            if (RC == "1")
            {
                pass.Focus();
                pass.Value = "";
                Lbl_Error.Visible = true;
                Lbl_Error.Text = "Sorry User is Already Logged in ..!!";
                return;
            }

            //cmd = "Select B.BankCD As BankCode, B.BankName, B.MidName As BranchName, " +
            //          "(Select IsNull(BrCd, 0) From BankName Where BranchDesc = 'HO') As HOBRCD, U.BrCd, " +
            //          "Left(U.UserName, Case When CharIndex(' ', U.UserName) > 0 Then CharIndex(' ', U.UserName) Else Len(U.UserName) End) As UserName, " +
            //          "U.LoginCode, U.PermissionNo, U.UserGroup, U.MultiLog, U.EPassWord From UserMaster U " +
            //          "Inner Join BankName B With(NoLock) On U.BrCd = B.BrCd "+
            //"Where U.LoginCode = '" + "ATUL7" + "' And EPassword = '" + "RuKiLynSotB0F4ROIpY4GQ==" + "' ";
            //SqlCommand cd = new SqlCommand(cmd);
            //string cs= ConfigurationManager.ConnectionStrings["ocbs"].ConnectionString;

            //SqlConnection con = new SqlConnection(cs);

            //con.Open();

            //SqlDataAdapter dt = new SqlDataAdapter(cmd, con);


            DT = LG.GetDetaile(Userid.Value, encrypt(pass.Value));
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con = line.Split("\n".ToCharArray());
            string Date = Decrypt(con[5].ToString());

            string Date1 = LG.getdate(Date);
            if (Date == null || Date1 == "")
            {

                WebMsgBox.Show("Error code-305", this.Page);
                return;
            }
            else
            {
                if (DT.Rows.Count > 0)
                {
                    Session["SessionTimeout"] = LG.GetSessionTime();
                    //  Added by amol on 21/11/2018 (for bank details)
                    Session["BankCode"] = DT.Rows[0]["BankCode"].ToString();
                    Session["BankName"] = DT.Rows[0]["BankName"].ToString();
                    Session["BranchName"] = DT.Rows[0]["BranchName"].ToString();
                    //  End added by amol on 21/11/2018

                    Session["BRCD"] = DT.Rows[0]["BrCd"].ToString();
                    Session["HOBRCD"] = DT.Rows[0]["HOBRCD"].ToString();
                    Session["MID"] = DT.Rows[0]["PermissionNo"].ToString();
                    Session["LOGINCODE"] = DT.Rows[0]["LoginCode"].ToString();
                    Session["UserName"] = DT.Rows[0]["UserName"].ToString();
                    Session["UGRP"] = DT.Rows[0]["UserGroup"].ToString();
                    Session["MULTILOG"] = DT.Rows[0]["MultiLog"].ToString();

                    if (DT.Rows.Count > 0)//Dhanya Shetty-To store the activity in Avs500 table as log details//
                    {
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Login _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    }
                    if (DT.Rows[0]["EPassWord"].ToString() == "")
                    {
                        if (DT.Rows[0]["UserGroup"].ToString() == "1" || DT.Rows[0]["UserGroup"].ToString() == "2" || DT.Rows[0]["UserGroup"].ToString() == "3")
                        {
                            string url = "FrmSetPWD.aspx";
                            NewTab(url);
                        }
                    }
                    else
                    {
                        int RS = LG.UpdateLoginsts(Userid.Value, encrypt(pass.Value), "1", Session["BRCD"].ToString());
                        if (RS > 0)
                        {
                            Session["UID"] = Userid.Value;
                            Session["PWD"] = pass.Value;
                            Response.Redirect("FrmBlank.aspx", true);
                        }
                    }
                }
                else
                {
                    Lbl_Error.Visible = true;
                    Lbl_Error.Text = "Login Fail Please Check UserId or Password ...!!";
                    Userid.Focus();
                    return;
                }
            }
        }
    }

    public void Logout()
    {
        try
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            int RC = LG.UpdateLoginsts(Session["UID"].ToString(), encrypt(Session["PWD"].ToString()), OC, Session["BRCD"].ToString());
            if (RC > 0)
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Logout _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string GetMACAddress()// amruta 24/04/2017
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        return sMacAddress;
    }

    public void GetAddress()// amruta 24/04/2017
    {
        DataTable dt = new DataTable();
        dt = LG.InseryMAC(hdnAddress.Value, Host);
        if (dt.Rows.Count == 0 || dt.Rows.Count == null)
        {
            LG.GetMAC(hdnAddress.Value, Host);

        }
    }

    public void UpdatePassword()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = LG.getpassword();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LG.updatePassword(dt.Rows[i]["LoginCode"].ToString(), dt.Rows[i]["password"].ToString(), encrypt(dt.Rows[i]["password"].ToString()));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public void NewTab(string url)
    {
        string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=500,left=100,top=100,resizable=yes');";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=500,left=100,top=100,resizable=no');", true);
    }

    protected void Userid_TextChanged(object sender, EventArgs e)
    {

    }

    public int ForceLogout(string BRCD, string UGRP, string LC)
    {
        int TT = 0;
        try
        {
            if (UGRP != "1")
            {
                int Res = LG.RealizedUser(HttpContext.Current.Session["LOGINCODE"].ToString(), HttpContext.Current.Session["BRCD"].ToString());
                if (Res > 0)
                    TT = 1;
            }
            else
            {
                TT = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TT;
    }

    public string GetHostAddress()//Amruta 24/04/2017
    {
        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
        String ecname = System.Environment.MachineName;
        return ecname;
    }

}