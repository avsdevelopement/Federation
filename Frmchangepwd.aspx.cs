using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changepwd : System.Web.UI.Page
{
    ClsWchangePWD clsa = new ClsWchangePWD();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["UGRP"].ToString() == "1")
            {
                txtlogincode.Text = Session["LOGINCODE"].ToString();
                divLogin.Visible = true;
                clsa.binddll(ddlUname, Session["BRCD"].ToString());
                ddlUname.SelectedValue=Session["MID"].ToString();
                
            }
            else
            {
                txtlogincode.Text = Session["LOGINCODE"].ToString();
                divLogin.Visible = false;
                clsa.binddll(ddlUname, Session["BRCD"].ToString());
                ddlUname.SelectedValue = Session["MID"].ToString();
            }
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int result = clsa.SubmitWchangepwd(encrypt(txtnewpwd.Text), txtlogincode.Text, encrypt(txtoldpwd.Text), Session["BRCD"].ToString(), ddlUname.SelectedValue.ToString());

            if (result > 0)
            {
                ClearField();
                lblMessage.Text = "Password Change successfully done....!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Password_Submit _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else if (result == 0)
            {
                lblMessage.Text = "Invalid Old Password....!!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearField() 
    {
        txtlogincode.Text = "";
        txtoldpwd.Text = "";
        txtnewpwd.Text = "";
        txtconfirmpwd.Text = "";
    }

    protected void txtlogincode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["LOGINCODE"].ToString() != txtlogincode.Text)
            {
                WebMsgBox.Show("User is restricted to change password..!! Only same user can change password..!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlUname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtlogincode.Text = clsa.getlogincd(ddlUname.SelectedValue.ToString());
            divLogin.Visible = true;
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}