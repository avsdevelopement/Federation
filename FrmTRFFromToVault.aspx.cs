using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmTRFDenomToVault : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsTRFDenomToVault TV = new ClsTRFDenomToVault();
    DataTable DT;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Flag"] = Request.QueryString["Flag"].ToString();
        if(!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            if (Request.QueryString["Flag"].ToString() == "FR")
            {
                DIVTRFFROM.Visible = true;
                DIVTRFTO.Visible = false;
                VaultBal("FR");
                StationBal("FR");
                txtTRFTo.Focus();
            }
            else if (Request.QueryString["Flag"].ToString() == "TO")
            {
                DIVTRFTO.Visible = true;
                DIVTRFFROM.Visible = false;
                VaultBal("TO");
                StationBal("TO");
                txtTRFto1.Focus();
            }
        }
    }

    public void VaultBal(string Flag)
    {
        int VaultBal = 0;
        try
        {
            DT = new DataTable();
            if (Flag == "FR")
            {
                DT = TV.BindVaultData(Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        switch (DT.Rows[i]["NOTE_TYPE"].ToString())
                        {
                            case "2000":
                                txtAvlblCnt1.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt1.Text) * 2000);
                                break;
                            case "1000":
                                txtAvlblCnt2.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt2.Text) * 1000);
                                break;
                            case "500":
                                txtAvlblCnt3.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt3.Text) * 500);
                                break;
                            case "200":
                                txtAvlblCnt12.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt12.Text) * 200);
                                break;
                            case "100":
                                txtAvlblCnt4.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt4.Text) * 100);
                                break;
                            case "50":
                                txtAvlblCnt5.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt5.Text) * 50);
                                break;
                            case "20":
                                txtAvlblCnt6.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt6.Text) * 20);
                                break;
                            case "10":
                                txtAvlblCnt7.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt7.Text) * 10);
                                break;
                            case "5":
                                txtAvlblCnt8.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt8.Text) * 5);
                                break;
                            case "2":
                                txtAvlblCnt9.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt9.Text) * 2);
                                break;
                            case "1":
                                txtAvlblCnt10.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblCnt10.Text) * 1);
                                break;
                        }
                    }
                    txtTRFFrom.Text = DT.Rows[0]["V_TYPE"].ToString();
                    txtVaultCashBal.Text = VaultBal.ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void StationBal(string Flag)
    {
        int VaultBal = 0;
        try
        {
            DT = new DataTable();
            if (Flag == "TO")
            {
                DT = TV.BindStationData(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        switch (DT.Rows[i]["NOTE_TYPE"].ToString())
                        {
                            case "2000":
                                txtAvlblC1.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC1.Text) * 2000);
                                break;
                            case "1000":
                                txtAvlblC2.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC2.Text) * 1000);
                                break;
                            case "500":
                                txtAvlblC3.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC3.Text) * 500);
                                break;
                            case "200":
                                txtAvlblC12.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC12.Text) * 200);
                                break;
                            case "100":
                                txtAvlblC4.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC4.Text) * 100);
                                break;
                            case "50":
                                txtAvlblC5.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC5.Text) * 50);
                                break;
                            case "20":
                                txtAvlblC6.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC6.Text) * 20);
                                break;
                            case "10":
                                txtAvlblC7.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC7.Text) * 10);
                                break;
                            case "5":
                                txtAvlblC8.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC8.Text) * 5);
                                break;
                            case "2":
                                txtAvlblC9.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC9.Text) * 2);
                                break;
                            case "1":
                                txtAvlblC10.Text = DT.Rows[i]["NO_OF_NOTES"].ToString();
                                VaultBal = VaultBal + (int.Parse(txtAvlblC10.Text) * 1);
                                break;
                        }
                    }
                    txtTRFFrom1.Text = DT.Rows[0]["V_TYPE"].ToString();
                    txtAvlblCBal.Text = VaultBal.ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void btnOkFrom_Click(object sender, EventArgs e)
    {
        try
        {
            string UsrGrp = TV.CheckUsrGrp(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (UsrGrp == "5")
            {
                int cnt = TV.GetData(Session["BRCD"].ToString(), txtTCnt10.Text, txtTCnt9.Text, txtTCnt8.Text, txtTCnt7.Text, txtTCnt6.Text, txtTCnt5.Text, txtTCnt4.Text, txtTCnt12.Text, txtTCnt3.Text, txtTCnt2.Text, txtTCnt1.Text, txtCoinTake1.Text, txtSolidNtsTake.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtTRFFrom.Text, txtTRFTo.Text);
                    if(cnt > 0)
                    {
                        
                        VaultBal("FR");
                        lblMessage.Text = "Transfer Successfully To Vault '" + txtTRFTo.Text.ToString() + "'...!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";
                        ClearAll();
                        return;
                    }
            }
            else
            {
                lblMessage.Text = "User is Not Cashier...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return;
        }
    }

    public void ClearAll()
    {
        if (ViewState["Flag"].ToString() == "FR")
        {
            txtTRFTo.Text = "";
            txtCoinTake1.Text = "";
            txtSolidNtsTake.Text = "";
            txtBalTake.Text = "";
            txtTCnt1.Text = "";
            txtTAmt1.Text = "";
            txtTCnt2.Text = "";
            txtTAmt2.Text = "";
            txtTCnt3.Text = "";
            txtTAmt3.Text = "";
            txtTCnt12.Text = "";
            txtTAmt12.Text = "";
            txtTCnt4.Text = "";
            txtTAmt4.Text = "";
            txtTCnt5.Text = "";
            txtTAmt5.Text = "";
            txtTCnt6.Text = "";
            txtTAmt6.Text = "";
            txtTCnt7.Text = "";
            txtTAmt7.Text = "";
            txtTCnt8.Text = "";
            txtTAmt8.Text = "";
            txtTCnt9.Text = "";
            txtTAmt9.Text = "";
            txtTCnt10.Text = "";
            txtTAmt10.Text = "";
        }
        if (ViewState["Flag"].ToString() == "TO")
        {
            txtTRFto1.Text = "";
            txtCgive.Text = "";
            txtSGive.Text = "";
            txtBalGive.Text = "";
            txtCnt1.Text = "";
            txtAmt1.Text = "";
            txtCnt2.Text = "";
            txtAmt2.Text = "";
            txtCnt3.Text = "";
            txtAmt3.Text = "";
            txtCnt12.Text = "";
            txtAmt12.Text = "";
            txtCnt4.Text = "";
            txtAmt4.Text = "";
            txtCnt5.Text = "";
            txtAmt5.Text = "";
            txtCnt6.Text = "";
            txtAmt6.Text = "";
            txtCnt7.Text = "";
            txtAmt7.Text = "";
            txtCnt8.Text = "";
            txtAmt8.Text = "";
            txtCnt9.Text = "";
            txtAmt9.Text = "";
            txtCnt10.Text = "";
            txtAmt10.Text = "";
        }
    }

    protected void btnGive_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "";
            string usrgrp = TV.CheckUsrGrp(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (usrgrp == "5")
            {
                Type = TV.CashType(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());
                
                if (Type == "2")
                {
                    int cnt = TV.InsertData(Session["BRCD"].ToString(), txtCnt10.Text, txtCnt9.Text, txtCnt8.Text, txtCnt7.Text, txtCnt6.Text, txtCnt5.Text, txtCnt4.Text, txtCnt12.Text, txtCnt3.Text, txtCnt2.Text, txtCnt1.Text, txtCgive.Text, txtSGive.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtTRFFrom1.Text, txtTRFto1.Text);
                    if (cnt > 0)
                    {
                        
                        StationBal("TO");
                        lblMessage.Text = "Transfer Successfully To Vault '" + txtTRFto1.Text.ToString() + "'...!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";
                        ClearAll();
                        return;
                    }
                }
                else if (Type == "3")
                {
                    if (txtTRFto1.Text != "999")
                    {
                        int cnt = TV.InsertData(Session["BRCD"].ToString(), txtCnt10.Text, txtCnt9.Text, txtCnt8.Text, txtCnt7.Text, txtCnt6.Text, txtCnt5.Text, txtCnt4.Text, txtCnt12.Text, txtCnt3.Text, txtCnt2.Text, txtCnt1.Text, txtCgive.Text, txtSGive.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtTRFFrom1.Text, txtTRFto1.Text);
                        if (cnt > 0)
                        {
                            
                            StationBal("TO");
                            lblMessage.Text = "Transfer Successfully To Vault '" + txtTRFto1.Text.ToString() + "'...!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";
                            ClearAll();
                            return;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Not Allow For Sub Cashier To Transfer Main Vault...!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "User is Not Cashier...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return;
        }
    }

    protected void txtTRFTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Type = "";

            // Check User is Main Cashier Or Not (2 for main Cashier)(3 for sub cashier)
            Type = TV.CashType(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());
            if (Type == "2")
            {
                string AT = "";

                //Check Vault Number Is Present Or Not for that branch Code
                AT = TV.Getstage(Session["BRCD"].ToString(), txtTRFTo.Text);

                if (AT != null)
                {
                    txtTRFTo.Text = AT.ToString();
                }
                else
                {
                    txtTRFTo.Text = "";
                    lblMessage.Text = "Vault Number Is Not Present...!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                txtTRFTo.Focus();
            }
            else
            {
                txtTRFTo.Text = "";
                lblMessage.Text = "Not Allow For Sub Cashier...!";
                ModalPopup.Show(this.Page);
                txtTRFTo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}