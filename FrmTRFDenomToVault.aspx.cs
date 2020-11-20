using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmTRFDenomToVault : System.Web.UI.Page
{
    //ClsBindDropdown BD = new ClsBindDropdown();
    //ClsTRFDenomToVault TV = new ClsTRFDenomToVault();
    //DataTable DT;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["Flag"].ToString() == "FR")
            {
                //DIVTRFFROM.Visible = true;
                //DIVTRFTO.Visible = false;
                //txtStnNo1.Text = Session["UserName"].ToString();
                //BD.BindBrCode(ddlBrCode1,Session["BRCD"].ToString());
                //VaultBal();
            }
            else if (Request.QueryString["Flag"].ToString() == "TO")
            {
                //DIVTRFTO.Visible = true;
                //DIVTRFFROM.Visible = false;
                //BD.BindBrCode(ddlBrCode,Session["BRCD"].ToString());
                //StationBal();
            }
        }
    }

    //public void VaultBal()
    //{
    //    DT = new DataTable();
    //    DT = TV.BindVaultData();
    //    if (DT.Rows.Count > 0)
    //    {
    //        txtAvlblCnt1.Text = DT.Rows[0]["TWOTNTS"].ToString();
    //        txtAvlblCnt2.Text = DT.Rows[0]["ONETNTS"].ToString();
    //        txtAvlblCnt3.Text = DT.Rows[0]["FHUNDNTS"].ToString();
    //        txtAvlblCnt4.Text = DT.Rows[0]["HUNDNTS"].ToString();
    //        txtAvlblCnt5.Text = DT.Rows[0]["FIFTYNTS"].ToString();
    //        txtAvlblCnt6.Text = DT.Rows[0]["TWENTYNTS"].ToString();
    //        txtAvlblCnt7.Text = DT.Rows[0]["TENNTS"].ToString();
    //        txtAvlblCnt8.Text = DT.Rows[0]["FIVENTS"].ToString();
    //        txtAvlblCnt9.Text = DT.Rows[0]["TWONTS"].ToString();
    //        txtAvlblCnt10.Text = DT.Rows[0]["ONENTS"].ToString();
    //        txtVaultCoins1.Text = DT.Rows[0]["CHILLER"].ToString();
    //        txtVaultCashBal.Text = DT.Rows[0]["TOTAL"].ToString();
    //        txtTRFFrom.Text = DT.Rows[0]["VAULTNO"].ToString();
    //    }
    //}

    //public void StationBal()
    //{
    //    DT = new DataTable();
    //    DT = TV.BindStationData();
    //    if (DT.Rows.Count > 0)
    //    {
    //        txtAvlblC1.Text = DT.Rows[0]["TWOTNTS"].ToString();
    //        txtAvlblC2.Text = DT.Rows[0]["ONETNTS"].ToString();
    //        txtAvlblC3.Text = DT.Rows[0]["FHUNDNTS"].ToString();
    //        txtAvlblC4.Text = DT.Rows[0]["HUNDNTS"].ToString();
    //        txtAvlblC5.Text = DT.Rows[0]["FIFTYNTS"].ToString();
    //        txtAvlblC6.Text = DT.Rows[0]["TWENTYNTS"].ToString();
    //        txtAvlblC7.Text = DT.Rows[0]["TENNTS"].ToString();
    //        txtAvlblC8.Text = DT.Rows[0]["FIVENTS"].ToString();
    //        txtAvlblC9.Text = DT.Rows[0]["TWONTS"].ToString();
    //        txtAvlblC10.Text = DT.Rows[0]["ONENTS"].ToString();
    //        txtCAvlbl.Text = DT.Rows[0]["CHILLER"].ToString();
    //        txtAvlblCBal.Text = DT.Rows[0]["TOTAL"].ToString();
    //        txtStationNo.Text = Session["UserName"].ToString();
    //    }
    //}
    
    //protected void btnOkFrom_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string UsrGrp = TV.CheckUsrGrp(Session["BRCD"].ToString(), Session["UserName"].ToString());

    //        if (UsrGrp == "5")
    //        {
    //            //int i = TV.InsertData(ddlBrCode1.SelectedValue, txtTCnt1.Text == "" ? "0" : txtTCnt1.Text, txtTCnt2.Text == "" ? "0" : txtTCnt2.Text, txtTCnt3.Text == "" ? "0" : txtTCnt3.Text, txtTCnt4.Text == "" ? "0" : txtTCnt4.Text, txtTCnt5.Text == "" ? "0" : txtTCnt5.Text, txtTCnt6.Text == "" ? "0" : txtTCnt6.Text, txtTCnt7.Text == "" ? "0" : txtTCnt7.Text, txtTCnt8.Text == "" ? "0" : txtTCnt8.Text, txtTCnt9.Text == "" ? "0" : txtTCnt9.Text, txtTCnt10.Text == "" ? "0" : txtTCnt10.Text, txtCoinTake1.Text == "" ? "0" : txtCoinTake1.Text, txtBalTake.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtTRFFrom.Text);

    //            //if (i > 0)
    //            //{
    //            //    lblMessage.Text = "Balance Transfer Successfully...!";
    //            //    ModalPopup.Show(this.Page);
    //            //    return;
    //            //}
    //        }
    //        else
    //        {
    //            lblMessage.Text = "User is Not Cashier...!";
    //            ModalPopup.Show(this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //        return;
    //    }
    //}
}