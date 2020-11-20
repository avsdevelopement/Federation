using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class FrmChangeBank : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogin LG = new ClsLogin();
    ClsChangeBranch CB = new ClsChangeBranch();
    DataTable DT = new DataTable();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindBranch();
            ddlBrName.Focus();
        }
    }

    public void BindBranch()
    {
        BD.BindBRANCHNAME(ddlBrName,null);
    }

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["BRCD"].ToString() != ddlBrName.SelectedValue.ToString())
        {
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();
            btnUpdate.Focus();
        }
        else
        {
            ddlBrName.Focus();
            WebMsgBox.Show("Already in same branch ...!!", this.Page);
            return;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DT = new DataTable();
        //string MultiBr = CB.CheckMultiBrAccess(Session["UID"].ToString(), Session["BRCD"].ToString());
        //if (MultiBr != "N")
        //{
        //    //int RS = CB.UpdateBranch(Session["UID"].ToString(), Session["PWD"].ToString(), "1", txtBrCode.Text.Trim().ToString(), Session["BRCD"].ToString());
        //    //if (RS > 0)
        //    //{
        //DT = LG.GetDetaile(Session["UID"].ToString(), Session["PWD"].ToString());
            DT = LG.GetDetaile1(Session["UID"].ToString(), encrypt(Session["PWD"].ToString()),Session["BRCD"].ToString());//Amruta 24/04/2017
            if (DT.Rows.Count > 0)
            {
                Session["MID"] = DT.Rows[0]["PERMISSIONNO"].ToString();
                Session["LOGINCODE"] = DT.Rows[0]["LOGINCODE"].ToString();
                Session["UserName"] = DT.Rows[0]["USERNAME"].ToString();
                Session["BRCD"] = txtBrCode.Text.Trim().ToString() == "" ? "0" : txtBrCode.Text.Trim().ToString();
                Session["UGRP"] = DT.Rows[0]["USERGROUP"].ToString();
                Session["UID"] = Session["UID"].ToString();
                Session["PWD"] =Session["PWD"].ToString();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Branch_change _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                Response.Redirect("FrmBlank.aspx", true);
            }
            else
            {
                lblMessage.Text = "User Does Not Have Multi Branch Access...!!";
                ModalPopup.Show(this.Page);
            }
        //}
        //}
        //else
        //{
        //    lblMessage.Text = "User Does Not Have Multi Branch Access...!!";
        //    ModalPopup.Show(this.Page);
        //}
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
}