using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCustAccnoUpdation : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCustomerUpdation CU = new ClsCustomerUpdation();
    ClsAccopen accop = new ClsAccopen();
    DataTable DT = new DataTable();
    int Result = 0;
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
                autoglname1.ContextKey = Session["BRCD"].ToString();
                //autoglname2.ContextKey = "";
                txtFromCustNo.Focus();
                btnUpdate.Enabled = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string AT = CU.GetStage(Session["BRCD"].ToString(), txtFromCustNo.Text);

                if (AT == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (AT == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else if (AT == "" || AT == null)
                {
                    WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                    return;
                }
                else
                {
                    txtFromCustName.Text = BD.GetCustName(txtFromCustNo.Text, Session["BRCD"].ToString());
                    BindGridDetls(txtFromCustNo.Text,Session["EntryDate"].ToString());
                    //txtNewCustNo.Focus();
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = CU.GetStage(Session["BRCD"].ToString(), txtFromCustNo.Text);

            if (AT == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (AT == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else
            {
                string CUNAME = txtFromCustName.Text;
                string[] custnob = CUNAME.Split('_');

                if (custnob.Length > 1)
                {
                    txtFromCustName.Text = custnob[0].ToString();
                    txtFromCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    BindGridDetls(txtFromCustNo.Text, Session["EntryDate"].ToString());
                    //txtNewCustNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdCustDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCustDetails.PageIndex = e.NewPageIndex;
            //BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //BindGrid();
    }

    //public void BindGrid()
    //{
    //    int Result;
    //    Result = CU.BindGrid(grdCustDetails, Session["BRCD"].ToString(), txtFromCustNo.Text);

    //    if (Result > 0)
    //    {
    //        btnUpdate.Enabled = true;
    //    }
    //}

    public void BindGridDetls(string custno, string FDate)
    {
        int Result;
        string Dex1 = "";

        if (CHK_SKIP_STD.Checked == true)
        {
            Dex1 = "Y";
        }
        else if (CHK_SKIP_STD.Checked == false)
        {
            Dex1 = "N";
        }

        Result = CU.GETAccDetails(GrdVwAcDetails, custno, Session["EntryDate"].ToString(), Dex1);

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Result = CU.ChangeCustomer(TxtBRCD.Text, txttype.Text.ToString(), txtaccno.Text.ToString(), txtFromCustNo.Text.ToString(), txtNewCustNo.Text.ToString());

            if (Result > 0)
            {
                lblMessage.Text = "Customer Number Updated Successfully...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";
                ClearAll();

                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Update_Customer_ _" + txtFromCustNo.Text + "_" + txtNewCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
            else
            {
                lblMessage.Text = "Customer Number Not Updated Successfully...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void GrdVwAcDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdVwAcDetails.PageIndex = e.NewPageIndex;
        BindGridDetls(txtFromCustNo.Text,Session["EntryDate"].ToString());
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["BRCD"] = ARR[0].ToString(); 
            ViewState["SUBGLCODE"] = ARR[1].ToString();
            ViewState["ACCNO"] = ARR[2].ToString();
            ViewState["Flag"] = "VW";
            
            //callaccnInfo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txttype_TextChanged(object sender, EventArgs e)
    {
        try
        {
                string AC1;
                AC1 = accop.Getaccno(txttype.Text, Session["BRCD"].ToString(), "");

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('-'); ;
                    ViewState["ACCNO"] = AC[0].ToString();
                    if (ViewState["Flag"].ToString() != "AD")
                    {
                        txtaccno.Enabled = true;
                        txtaccno.Focus();
                    }
                    ViewState["GLCODE"] = AC[1].ToString();
                    txttynam.Text = AC[2].ToString();
                    txtaccno.Focus();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text + "_" + ViewState["GLCODE"].ToString();
                    txtaccno.Focus();
                }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txttynam_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txttynam.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txttynam.Text = custnob[0].ToString();
                txttype.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                // TxtGLCD.Text = "";// custnob[2].ToString();
                string[] AC = accop.Getaccno(txttype.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text;// +"_" + ViewState["GLCODE"].ToString();
                txtaccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {

    }

    protected void GrdVwAcDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdVwAcDetails.SelectedRow;

            TxtBRCD.Text = (row.FindControl("lblBRCD") as Label).Text;
            TxtBrname.Text = (row.FindControl("lblBRname") as Label).Text;
            txttype.Text = (row.FindControl("lblSUBGLCODE") as Label).Text;
            txttynam.Text = (row.FindControl("lblPrdName") as Label).Text;
            txtaccno.Text = (row.FindControl("lblACCNO") as Label).Text;
            TxtClearBal.Text = (row.FindControl("lblCLBal") as Label).Text;
            TxtOPDT.Text = (row.FindControl("lblOPENINGDATE") as Label).Text;
            TxtACStatus.Text = (row.FindControl("lblACC_STATUS") as Label).Text;
            TxtAccName.Text = txtFromCustName.Text;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtNewCustname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = CU.GetStage(Session["BRCD"].ToString(), txtNewCustNo.Text);

            if (AT == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (AT == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else
            {
                string CUNAME = txtNewCustname.Text;
                string[] custnob = CUNAME.Split('_');

                if (custnob.Length > 1)
                {
                    txtNewCustname.Text = custnob[0].ToString();
                    txtNewCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtNewCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string AT = CU.GetStage(Session["BRCD"].ToString(), txtNewCustNo.Text);

                if (AT == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (AT == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else if (AT == "" || AT == null)
                {
                    WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                    return;
                }
                else
                {
                    txtNewCustname.Text = BD.GetCustName(txtNewCustNo.Text, Session["BRCD"].ToString());
                    btnUpdate.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
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
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearAll()
    {
        try
        {
            TxtBRCD.Text        = "";
            TxtBrname.Text      = "";
            txttype.Text        = ""; 
            txttynam.Text       = "";
            txtaccno.Text       = "";
            TxtClearBal.Text    = "";
            TxtOPDT.Text        = "";
            TxtACStatus.Text    = "";
            TxtAccName.Text     = "";
            txtNewCustNo.Text   = "";
            txtNewCustname.Text = "";
            txtFromCustNo.Text  = "";
            txtFromCustName.Text = "";
            //CHK_SKIP_STD.Checked == false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}