using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmCustAccnoChange : System.Web.UI.Page
{
    CLsCustNoChanges CLS=new CLsCustNoChanges();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        string AT = "";
       
        AT = CLS.Getstage1(txtCustNo.Text, Session["BRCD"].ToString());
        if (AT != "1003")
        {
            lblMessage.Text = "Sorry Customer not Authorise.........!!";
            ModalPopup.Show(this.Page);
            txtCustNo.Text = "";
            txtcustname.Text = "";
            txtCustNo.Focus();
        }
        else
        {
            string CUSTNAME = CLS.GetCustNAme(txtCustNo.Text, Session["BRCD"].ToString());
            if (CUSTNAME != null || CUSTNAME != "")
            {
                txtcustname.Text = CUSTNAME;
                BINDCUSTGRID();
                TxtAccType.Focus();

            }
            else
            {
                lblMessage.Text = "Cust No Not Exit In Master   ...!!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
    }
    protected void GRDCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GRDACC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void BINDCUSTGRID()
    {
        DataTable DT = new DataTable();
        DT = CLS.GETGRIDCUST(txtCustNo.Text, Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            GRDCust.DataSource = DT;
            GRDCust.DataBind();
        }
    
    
    }
    public void BINDACCNOGRID()
    {
        DataTable DT = new DataTable();
        DT = CLS.GETGRIDACCNO(txtCustNo.Text, Session["BRCD"].ToString(), TxtAccType.Text);
        if (DT.Rows.Count > 0)
        {
            GRDACC.DataSource = DT;
            GRDACC.DataBind();
        }


    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            int Result = CLS.UPDATECUSTNO(Session["BRCD"].ToString(), txtCustNo.Text, TxtAccType.Text, TxtAccno.Text, txtnewCustNo.Text);
            if (Result > 0)
            {
                clearall();
                lblMessage.Text = "Record Updated Successfully ....!!!";
                ModalPopup.Show(this.Page);
                return;

            }
            else
            {
                lblMessage.Text = "Record Not Updated please Check Fields ...!!";
                ModalPopup.Show(this.Page);
                return;

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void clearall()
    {
        txtCustNo.Text = "";
        txtcustname.Text = "";
        TxtAccType.Text = "";
        TxtATName.Text = "";
        TxtAccno.Text = "";
        TxtAccHName.Text = "";
        txtnewCustNo.Text = "";
        txtnewcustname.Text = "";
        TxtOPDT.Text = "";
    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        clearall();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {

    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        string GLNAME = CLS.GetAccType(TxtAccType.Text, Session["BRCD"].ToString());
        TxtATName.Text = GLNAME;
        GRDCust.Visible = false;
        BINDACCNOGRID();
        TxtAccno.Focus();
    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        string AT = "";
        AT = BD.Getstage1(TxtAccno.Text, Session["BRCD"].ToString(), TxtAccType.Text);
        if (AT != "1003")
        {
            lblMessage.Text = "Sorry Customer not Authorise.........!!";
            ModalPopup.Show(this.Page);
            TxtAccHName.Text = "";
            TxtAccno.Text = "";
            TxtAccno.Focus();
        }
        else
        {
            DataTable DT = new DataTable();
            DT = CLS.GetAccName(TxtAccType.Text, TxtAccno.Text, Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                string[] TD = TxtOPDT.Text.Split('/');
                TxtAccHName.Text = DT.Rows[0]["CustName"].ToString();
                TxtOPDT.Text = Convert.ToDateTime(DT.Rows[0]["OPENINGDATE"]).ToString("dd/MM/yyyy");
            }
            txtnewCustNo.Focus();
        }
    }
    protected void TxtAccHName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtnewCustNo_TextChanged(object sender, EventArgs e)
    {
        string AT = "";

        AT = CLS.Getstage1(txtCustNo.Text, Session["BRCD"].ToString());
        if (AT != "1003")
        {
            lblMessage.Text = "Sorry Customer not Authorise.........!!";
            ModalPopup.Show(this.Page);
            txtnewCustNo.Focus();
        }
        else
        {
            string CUSTNAME = CLS.GetCustNAme(txtnewCustNo.Text, Session["BRCD"].ToString());
            if (CUSTNAME != null || CUSTNAME != "")
            {
                txtnewcustname.Text = CUSTNAME;
                Submit.Focus();

            }
            else
            {
                lblMessage.Text = "Cust No Not Exit In Master   ...!!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
    }
    protected void RdbSingle_CheckedChanged(object sender, EventArgs e)
    {
        Single.Visible = true;
        Div2.Visible = false;
        Div3.Visible = false;
        Multiple.Visible = false;
    }
    protected void RdbMultiple_CheckedChanged(object sender, EventArgs e)
    {
        Single.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Multiple.Visible = true;
    }
    protected void txtnewAccno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAaccno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAaccname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtnewAccno_TextChanged1(object sender, EventArgs e)
    {

    }
 
    protected void btnaexit_Click(object sender, EventArgs e)
    {

    }
    protected void accclear_Click(object sender, EventArgs e)
    {

    }
    protected void btnaccupdate_Click(object sender, EventArgs e)
    {

    }
    protected void txtpname_TextChanged(object sender, EventArgs e)
    {

    }
}