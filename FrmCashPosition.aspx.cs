using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class FrmCashPosition : System.Web.UI.Page
{
    ClsCashPosition CP = new ClsCashPosition();
    int Result=0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                Txttwothousand.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex) ;
        }
    }

    #region Text Changes Event
    
    protected void Txttwothousand_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtonethousand.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtonethousand_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtfivehundrd.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtfivehundrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTwoHundred.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtTwoHundred_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txthundred.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txthundred_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtfifty.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtfifty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txttwenty.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txttwenty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtten.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtten_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtfive.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtfive_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txttwo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txttwo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txtone.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtone_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void Cleardata()
    {
        try
        {
            Txttwothousand.Text = "";
            Txttwothoutotal.Text = "";
            Txtonethousand.Text = "";
            Txtonethoutotal.Text = "";
            Txtfivehundrd.Text = "";
            Txtfivehuntotal.Text = "";
            txtTwoHundred.Text = "";
            txtTwoHunTotal.Text = "";
            Txthundred.Text = "";
            Txthundrdtotal.Text = "";
            Txtfifty.Text = "";
            Txtfiftytotal.Text = "";
            Txttwenty.Text = "";
            Txttwntytotal.Text = "";
            Txtten.Text = "";
            Txttentotal.Text = "";
            Txtfive.Text = "";
            Txtfivetotal.Text = "";
            Txttwo.Text = "";
            Txttwototal.Text = "";
            Txtone.Text = "";
            Txtonetotal.Text = "";
            txtTotalCshBal.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event
    
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UGRP"].ToString() == "3")
            {
                if (Txttwothousand.Text == "" && Txtonethousand.Text == "" && Txtfivehundrd.Text == "" && txtTwoHundred.Text == "" && Txthundred.Text == "" && Txtfifty.Text == "" && Txttwenty.Text == "" && Txtten.Text == "" && Txtfive.Text == "" &&
                    Txttwo.Text == "" && Txtone.Text == "")
                {
                    lblMessage.Text = "Please enter No of notes...!!";
                    ModalPopup.Show(this.Page);
                }
                else
                {
                    //  Update all Vault balance to zero first
                    Result = CP.UpdateAllVault(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (Result > 0)
                    {
                        //  Update Vault (999) balance Here
                        double TwoThousand = Convert.ToDouble(Txttwothousand.Text.Trim().ToString() == "" ? "0" : Txttwothousand.Text.Trim().ToString());
                        double Thousand = Convert.ToDouble(Txtonethousand.Text.Trim().ToString() == "" ? "0" : Txtonethousand.Text.Trim().ToString());
                        double FiveHundred = Convert.ToDouble(Txtfivehundrd.Text.Trim().ToString() == "" ? "0" : Txtfivehundrd.Text.Trim().ToString());
                        double TwoHundred = Convert.ToDouble(txtTwoHundred.Text.Trim().ToString() == "" ? "0" : txtTwoHundred.Text.Trim().ToString());
                        double Hundred = Convert.ToDouble(Txthundred.Text.Trim().ToString() == "" ? "0" : Txthundred.Text.Trim().ToString());
                        double Fifty = Convert.ToDouble(Txtfifty.Text.Trim().ToString() == "" ? "0" : Txtfifty.Text.Trim().ToString());
                        double Twenty = Convert.ToDouble(Txttwenty.Text.Trim().ToString() == "" ? "0" : Txttwenty.Text.Trim().ToString());
                        double Ten = Convert.ToDouble(Txtten.Text.Trim().ToString() == "" ? "0" : Txtten.Text.Trim().ToString());
                        double Five = Convert.ToDouble(Txtfive.Text.Trim().ToString() == "" ? "0" : Txtfive.Text.Trim().ToString());
                        double Two = Convert.ToDouble(Txttwo.Text.Trim().ToString() == "" ? "0" : Txttwo.Text.Trim().ToString());
                        double One = Convert.ToDouble(Txtone.Text.Trim().ToString() == "" ? "0" : Txtone.Text.Trim().ToString());

                        Result = CP.UpdateVaultBalance(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TwoThousand, Thousand, FiveHundred, TwoHundred, Hundred, Fifty, Twenty, Ten, Five, Two, One, Session["MID"].ToString());

                        if (Result > 0)
                        {

                            lblMessage.Text = "Updated  balance successfully for vault 999...!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Update _voult_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Cleardata();
                            return;
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Sorry...Allow only for a manager to update cash...!!";
                ModalPopup.Show(this.Page);
                return;
            }
         }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Cleardata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}