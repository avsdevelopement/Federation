using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmExchangeWithCustomer : System.Web.UI.Page
{
    ClsCashDenom CD = new ClsCashDenom();
    DataTable DT = new DataTable();
    double TotalBalance = 0;
    string SetNo = "";
    int i;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VaultBal();
        }
    }

    public void VaultBal()
    {
        DT = new DataTable();
        DT = CD.BindVaultData(Session["MID"].ToString(), Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["NOTE_TYPE"].ToString())
                {
                    case "2000":
                        TotalBalance = TotalBalance + (2000 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl1.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "1000":
                        TotalBalance = TotalBalance + (1000 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl2.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "500":
                        TotalBalance = TotalBalance + (500 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl3.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "200":
                        TotalBalance = TotalBalance + (200 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl12.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "100":
                        TotalBalance = TotalBalance + (100 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl4.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "50":
                        TotalBalance = TotalBalance + (50 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl5.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "20":
                        TotalBalance = TotalBalance + (20 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl6.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "10":
                        TotalBalance = TotalBalance + (10 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl7.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "5":
                        TotalBalance = TotalBalance + (5 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl8.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "2":
                        TotalBalance = TotalBalance + (2 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl9.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                    case "1":
                        TotalBalance = TotalBalance + (1 * Convert.ToDouble(DT.Rows[i]["NO_OF_NOTES"].ToString()));
                        txtavlbl10.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                        break;
                }
            }

            AvlblTotBalance.Value = Convert.ToDouble(TotalBalance).ToString();
            txtBalAvlbl.Text = TotalBalance.ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string usrgrp = CD.CheckUsrGrp(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (usrgrp == "5")
            {
                if (Convert.ToDouble(txtGBal.Text.Trim().ToString()) == Convert.ToDouble(txtTBal.Text.Trim().ToString()))
                {
                    if (Convert.ToInt32(Convert.ToInt16(txtTCnt1.Text == "" ? "0" : txtTCnt1.Text) + Convert.ToInt32(txtTCnt2.Text == "" ? "0" : txtTCnt2.Text) + Convert.ToInt32(txtTCnt3.Text == "" ? "0" : txtTCnt3.Text) + Convert.ToInt32(txtTCnt12.Text == "" ? "0" : txtTCnt12.Text) + Convert.ToInt32(txtTCnt4.Text == "" ? "0" : txtTCnt4.Text) + Convert.ToInt32(txtTCnt5.Text == "" ? "0" : txtTCnt5.Text) + Convert.ToInt32(txtTCnt6.Text == "" ? "0" : txtTCnt6.Text) + Convert.ToInt32(txtTCnt7.Text == "" ? "0" : txtTCnt7.Text) + Convert.ToInt32(txtTCnt8.Text == "" ? "0" : txtTCnt8.Text) + Convert.ToInt32(txtTCnt9.Text == "" ? "0" : txtTCnt9.Text) + Convert.ToInt32(txtTCnt10.Text == "" ? "0" : txtTCnt10.Text)) > 0)
                        i = CD.InsertDataIn(Session["BRCD"].ToString(), "0", "0", Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", txtTCnt1.Text, txtTCnt2.Text, txtTCnt3.Text, txtTCnt12.Text, txtTCnt4.Text, txtTCnt5.Text, txtTCnt6.Text, txtTCnt7.Text, txtTCnt8.Text, txtTCnt9.Text, txtTCnt10.Text, "0");//txtCTake.Text);
                    if (Convert.ToInt32(Convert.ToInt32(txtGCnt1.Text == "" ? "0" : txtGCnt1.Text) + Convert.ToInt32(txtGCnt2.Text == "" ? "0" : txtGCnt2.Text) + Convert.ToInt32(txtGCnt3.Text == "" ? "0" : txtGCnt3.Text) + Convert.ToInt32(txtGCnt12.Text == "" ? "0" : txtGCnt12.Text) + Convert.ToInt32(txtGCnt4.Text == "" ? "0" : txtGCnt4.Text) + Convert.ToInt32(txtGCnt5.Text == "" ? "0" : txtGCnt5.Text) + Convert.ToInt32(txtGCnt6.Text == "" ? "0" : txtGCnt6.Text) + Convert.ToInt32(txtGCnt7.Text == "" ? "0" : txtGCnt7.Text) + Convert.ToInt32(txtGCnt8.Text == "" ? "0" : txtGCnt8.Text) + Convert.ToInt32(txtGCnt9.Text == "" ? "0" : txtGCnt9.Text) + Convert.ToInt32(txtGCnt10.Text == "" ? "0" : txtGCnt10.Text)) > 0)
                        i = CD.InsertDataOut(Session["BRCD"].ToString(), "0", "0", Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", txtGCnt1.Text, txtGCnt2.Text, txtGCnt3.Text, txtGCnt12.Text, txtGCnt4.Text, txtGCnt5.Text, txtGCnt6.Text, txtGCnt7.Text, txtGCnt8.Text, txtGCnt9.Text, txtGCnt10.Text, "0");//txtCGive.Text);

                    if (i > 0)
                    {
                        ClearText();
                        VaultBal();
                        lblMessage.Text = "Successfully Complete...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Balance Take and Give Not Match With Each Other...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
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

    public void ClearText()
    {
        try
        {
            txtTCnt1.Text = "";
            txtTAmt1.Text = "";
            txtGCnt1.Text = "";
            txtGAmt1.Text = "";

            txtTCnt2.Text = "";
            txtTAmt2.Text = "";
            txtGCnt2.Text = "";
            txtGAmt2.Text = "";

            txtTCnt3.Text = "";
            txtTAmt3.Text = "";
            txtGCnt3.Text = "";
            txtGAmt3.Text = "";

            txtTCnt12.Text = "";
            txtTAmt12.Text = "";
            txtGCnt12.Text = "";
            txtGAmt12.Text = "";

            txtTCnt4.Text = "";
            txtTAmt4.Text = "";
            txtGCnt4.Text = "";
            txtGAmt4.Text = "";

            txtTCnt5.Text = "";
            txtTAmt5.Text = "";
            txtGCnt5.Text = "";
            txtGAmt5.Text = "";

            txtTCnt6.Text = "";
            txtTAmt6.Text = "";
            txtGCnt6.Text = "";
            txtGAmt6.Text = "";

            txtTCnt7.Text = "";
            txtTAmt7.Text = "";
            txtGCnt7.Text = "";
            txtGAmt7.Text = "";

            txtTCnt8.Text = "";
            txtTAmt8.Text = "";
            txtGCnt8.Text = "";
            txtGAmt8.Text = "";

            txtTCnt9.Text = "";
            txtTAmt9.Text = "";
            txtGCnt9.Text = "";
            txtGAmt9.Text = "";

            txtTCnt10.Text = "";
            txtTAmt10.Text = "";
            txtGCnt10.Text = "";
            txtGAmt10.Text = "";

            txtBalAvlbl.Text = "";
            txtTBal.Text = "";
            txtGBal.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}