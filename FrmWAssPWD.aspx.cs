using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmWAssPWD : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsWAssPWD cls = new ClsWAssPWD();
    string FL = "";
    int Result;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

    }

    #endregion

    #region Button Click

    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UGRP"].ToString() != "1")// ADDED BY ANKITA 17/11/2017 TO ALLOW ONLY ADMIN TO ASSIGN PASSWRD
            {
                WebMsgBox.Show("Only ADMIN User can Assign new password to new user..!!", this.Page);
                return;
            }
            Result = cls.AssignPass(encrypt(txtEnterpwd.Text), txtlogincode.Text, Session["BRCD"].ToString());

            if (Result > 0)
            {
                lblMessage.Text = "Password assign successfully...!";
                ModalPopup.Show(this.Page);
                clear();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Assign_NewPasswrd _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

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

    public void clear()
    {
        txtlogincode.Text = "";
        txtconfirmpwd.Text = "";
        txtEnterpwd.Text = "";
    }

    #endregion
}