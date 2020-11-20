using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmSharesClosure : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsShareClosure SC = new ClsShareClosure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BD.BindPayment(ddlPayType, "1");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtMemberNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtMemberName_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            ViewState["PAYTYPE"] = "CASH";
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By Cash";
            autoglname1.ContextKey = Session["BRCD"].ToString();

            txtAmount.Text = txtShrValue.Text.Trim().ToString() == "" ? "0" : txtShrValue.Text.Trim().ToString();

            Clear();
            btnSubmit.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            ViewState["PAYTYPE"] = "TRANSFER";
            Transfer.Visible = true;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            txtAmount.Text = txtShrValue.Text.Trim().ToString() == "" ? "0" : txtShrValue.Text.Trim().ToString();

            Clear();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            ViewState["PAYTYPE"] = "CHEQUE";
            Transfer.Visible = true;
            Transfer1.Visible = true;
            DivAmount.Visible = true;
            txtNarration.Text = "TRANSFER";
            txtAmount.Text = txtShrValue.Text.Trim().ToString() == "" ? "0" : txtShrValue.Text.Trim().ToString();

            Clear();
            txtProdType1.Focus();
        }
        else
        {
            Clear();
            Transfer.Visible = false;
            Transfer1.Visible = false;
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtProdType1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }

    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = SC.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GLCODE1"] = AC[0].ToString();
                txtProdName1.Text = AC[1].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = SC.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

                    TxtAccNo1.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                txtBalance.Text = "";
                txtProdType1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName1.Text = custnob[0].ToString();
                txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = SC.Getaccno(txtProdType1.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE1"] = AC[0].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text;

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = SC.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

                    TxtAccNo1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SC.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        txtBalance.Text = SC.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                        TxtChequeNo.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtBalance.Text = SC.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                TxtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}