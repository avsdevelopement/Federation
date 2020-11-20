using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCashDenom : System.Web.UI.Page
{
    ClsCashDenom CD = new ClsCashDenom();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    double Amount = 0;
    string SetNo = "";
    int i;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SetNo"] = Session["densset"].ToString();
            ViewState["Amount"] = Session["densamt"].ToString();
            txtamount.Text = Session["densamt"].ToString();
            VaultBal();
        }
    }

    public void VaultBal()
    {
        DT = new DataTable();
        DT = CD.BindVaultData(Session["MID"].ToString(),Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["NOTE_TYPE"].ToString())
                {
                    case "2000":
                        txtavlbl1.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "1000":
                        txtavlbl2.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "500":
                        txtavlbl3.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "200":
                        txtavlbl12.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "100":
                        txtavlbl4.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "50":
                        txtavlbl5.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "20":
                        txtavlbl6.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "10":
                        txtavlbl7.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "5":
                        txtavlbl8.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "2":
                        txtavlbl9.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "1":
                        txtavlbl10.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string usrgrp = CD.CheckUsrGrp(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());//  Dhanya Shetty 04/08/2017 changed username to logincode

            if (usrgrp == "5")
            {
                if (Convert.ToInt32(Convert.ToInt16(txtTCnt1.Text == "" ? "0" : txtTCnt1.Text) + Convert.ToInt32(txtTCnt2.Text == "" ? "0" : txtTCnt2.Text) + Convert.ToInt32(txtTCnt3.Text == "" ? "0" : txtTCnt3.Text) + Convert.ToInt32(txtTCnt12.Text == "" ? "0" : txtTCnt12.Text) + Convert.ToInt32(txtTCnt4.Text == "" ? "0" : txtTCnt4.Text) + Convert.ToInt32(txtTCnt5.Text == "" ? "0" : txtTCnt5.Text) + Convert.ToInt32(txtTCnt6.Text == "" ? "0" : txtTCnt6.Text) + Convert.ToInt32(txtTCnt7.Text == "" ? "0" : txtTCnt7.Text) + Convert.ToInt32(txtTCnt8.Text == "" ? "0" : txtTCnt8.Text) + Convert.ToInt32(txtTCnt9.Text == "" ? "0" : txtTCnt9.Text) + Convert.ToInt32(txtTCnt10.Text == "" ? "0" : txtTCnt10.Text)) > 0)
                    i = CD.InsertDataIn(Session["BRCD"].ToString(), Session["denssubgl"].ToString(), Session["densact"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), txtTCnt1.Text, txtTCnt2.Text, txtTCnt3.Text, txtTCnt12.Text, txtTCnt4.Text, txtTCnt5.Text, txtTCnt6.Text, txtTCnt7.Text, txtTCnt8.Text, txtTCnt9.Text, txtTCnt10.Text, "0");//txtCTake.Text);
                if (Convert.ToInt32(Convert.ToInt32(txtGCnt1.Text == "" ? "0" : txtGCnt1.Text) + Convert.ToInt32(txtGCnt2.Text == "" ? "0" : txtGCnt2.Text) + Convert.ToInt32(txtGCnt3.Text == "" ? "0" : txtGCnt3.Text) + Convert.ToInt32(txtGCnt12.Text == "" ? "0" : txtGCnt12.Text) + Convert.ToInt32(txtGCnt4.Text == "" ? "0" : txtGCnt4.Text) + Convert.ToInt32(txtGCnt5.Text == "" ? "0" : txtGCnt5.Text) + Convert.ToInt32(txtGCnt6.Text == "" ? "0" : txtGCnt6.Text) + Convert.ToInt32(txtGCnt7.Text == "" ? "0" : txtGCnt7.Text) + Convert.ToInt32(txtGCnt8.Text == "" ? "0" : txtGCnt8.Text) + Convert.ToInt32(txtGCnt9.Text == "" ? "0" : txtGCnt9.Text) + Convert.ToInt32(txtGCnt10.Text == "" ? "0" : txtGCnt10.Text)) > 0)
                    i = CD.InsertDataOut(Session["BRCD"].ToString(), Session["denssubgl"].ToString(), Session["densact"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), txtGCnt1.Text, txtGCnt2.Text, txtGCnt3.Text, txtGCnt12.Text, txtGCnt4.Text, txtGCnt5.Text, txtGCnt6.Text, txtGCnt7.Text, txtGCnt8.Text, txtGCnt9.Text, txtGCnt10.Text, "0");//txtCGive.Text);

                if (i > 0)
                {
                    Session["densset"] = null;
                    Session["densamt"] = null;
                    Session["denssubgl"] = null;
                    Session["densact"] = null;

                    WebMsgBox.Show("Successfully Complete...!!", this.Page);
                    FL = "Insert";
                    String x = "<script type='text/javascript'>self.close();</script>";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", x, false);
                    return;
                }
            }
            else
            {
                Session["densset"] = null;
                Session["densamt"] = null;
                Session["denssubgl"] = null;
                Session["densact"] = null;

                lblMessage.Text = "Sorry...User is Not Cashier...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            lblMessage.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return;
        }
    }

    protected void btnAddExisting_Click(object sender, EventArgs e)
    {
        try
        {
            DivSet.Visible = true;
            txtSetNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtSetNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtSetNo.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter voucher number first...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            else
            {
                string Cash = CD.CheckCashSet(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtSetNo.Text.Trim().ToString());

                if (Cash != null && Cash != "")
                {
                    Amount = CD.GetVoucherAmount(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtSetNo.Text.Trim().ToString());
                    txtamount.Text = Convert.ToDouble(Convert.ToDouble(txtamount.Text.Trim().ToString()) + Convert.ToDouble(Amount)).ToString();
                }
                else
                {
                    lblMessage.Text = "Voucher is not of type-cash...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}