using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmExchDenom : System.Web.UI.Page
{
    ClsExchangeDenom ED = new ClsExchangeDenom();
    DataTable DT;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInstDate.Text = Session["EntryDate"].ToString();
            txtName.Text = Session["UserName"].ToString();
            BrVaultBal();
        }
    }

    public void BrVaultBal()
    {
        DT = new DataTable();
        DT = ED.BindBrVaultData(Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtavlbl1.Text = DT.Rows[0]["TWOTNTS"].ToString();
            txtavlbl2.Text = DT.Rows[0]["ONETNTS"].ToString();
            txtavlbl3.Text = DT.Rows[0]["FHUNDNTS"].ToString();
            txtavlbl4.Text = DT.Rows[0]["HUNDNTS"].ToString();
            txtavlbl5.Text = DT.Rows[0]["FIFTYNTS"].ToString();
            txtavlbl6.Text = DT.Rows[0]["TWENTYNTS"].ToString();
            txtavlbl7.Text = DT.Rows[0]["TENNTS"].ToString();
            txtavlbl8.Text = DT.Rows[0]["FIVENTS"].ToString();
            txtavlbl9.Text = DT.Rows[0]["TWONTS"].ToString();
            txtavlbl10.Text = DT.Rows[0]["ONENTS"].ToString();
            txtCAvlbl.Text = DT.Rows[0]["CHILLER"].ToString();
            txtBalAvlbl.Text = DT.Rows[0]["TOTAL"].ToString();
            ViewState["ID"] = DT.Rows[0]["ID"].ToString(); 
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Flag"].ToString() == "FR")
            {
                string UsrGrp = ED.CheckUsrGrp(Session["BRCD"].ToString(), Session["UserName"].ToString());

                if (UsrGrp == "5")
                {
                    int i = ED.InsertDataFromBVault(ViewState["ID"].ToString(), Session["BRCD"].ToString(), txtGCnt1.Text == "" ? "0" : txtGCnt1.Text, txtGCnt2.Text == "" ? "0" : txtGCnt2.Text, txtGCnt3.Text == "" ? "0" : txtGCnt3.Text, txtGCnt4.Text == "" ? "0" : txtGCnt4.Text, txtGCnt5.Text == "" ? "0" : txtGCnt5.Text, txtGCnt6.Text == "" ? "0" : txtGCnt6.Text, txtGCnt7.Text == "" ? "0" : txtGCnt7.Text, txtGCnt8.Text == "" ? "0" : txtGCnt8.Text, txtGCnt9.Text == "" ? "0" : txtGCnt9.Text, txtGCnt10.Text == "" ? "0" : txtGCnt10.Text, txtCGive.Text == "" ? "0" : txtCGive.Text, txtGBal.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (i > 0)
                    {
                        lblMessage.Text = "Transfer Successfully From Branch Vault...!";
                        ModalPopup.Show(this.Page);
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
            else if (Request.QueryString["Flag"].ToString() == "TO")
            {
                int i = ED.InsertDataFromCashier(ViewState["ID"].ToString(), Session["BRCD"].ToString(), txtTCnt1.Text == "" ? "0" : txtTCnt1.Text, txtTCnt2.Text == "" ? "0" : txtTCnt2.Text, txtTCnt3.Text == "" ? "0" : txtTCnt3.Text, txtTCnt4.Text == "" ? "0" : txtTCnt4.Text, txtTCnt5.Text == "" ? "0" : txtTCnt5.Text, txtTCnt6.Text == "" ? "0" : txtTCnt6.Text, txtTCnt7.Text == "" ? "0" : txtTCnt7.Text, txtTCnt8.Text == "" ? "0" : txtTCnt8.Text, txtTCnt9.Text == "" ? "0" : txtTCnt9.Text, txtTCnt10.Text == "" ? "0" : txtTCnt10.Text, txtCTake.Text == "" ? "0" : txtCTake.Text, txtTBal.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (i > 0)
                {
                    lblMessage.Text = "Transfer Successfully To Branch Vault...!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return;
        }
    }
}