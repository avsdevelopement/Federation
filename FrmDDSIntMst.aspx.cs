using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDDSIntMst : System.Web.UI.Page
{
    int result = 0;
    ClsDDSIntMst DD = new ClsDDSIntMst();
    ClsCommon COMON = new ClsCommon();
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    ClsAccopen accop = new ClsAccopen();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ClsBindDropdown bindddl = new ClsBindDropdown();
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                autoglname.ContextKey = Session["BRCD"].ToString();
                bindddl.BindAccType(DDLMembType);
                Bindgrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtFGL_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtTGL_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtproid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtproid.Text != "")
            {
                string proname = COMON.GetProductName(Convert.ToInt32(txtproid.Text.ToString()), Session["BRCD"].ToString());
                if (proname != null)
                {
                    txtproname.Text = proname;
                    ddlperiodtypefrm.Focus();
                }
                else
                {
                    WebMsgBox.Show("Invalid product Code", this.Page);
                    Clear();
                    txtproid.Focus();
                }
            }
            else
            {
                txtproname.Text = "";
                ddlperiodtypefrm.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtproname_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        Btnsubmit.Visible = true;
        BtnModify.Visible = false;
        BtnDelete.Visible = false;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Btnsubmit.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string ID = objlink.CommandArgument;
            ViewState["ID"] = ID;
            dt = DD.GetIntrestMasterID(ID, Session["BRCD"].ToString());
            AssignValues(dt);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Btnsubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string ID = objlink.CommandArgument;
            ViewState["ID"] = ID;
            dt = DD.GetIntrestMasterID(ID, Session["BRCD"].ToString());
            AssignValues(dt);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdIntrstMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIntrstMaster.PageIndex = e.NewPageIndex;
        Bindgrid();
    }
    protected void Bindgrid()
    {
        try
        {
            DD.GetDDSIntrestMaster(grdIntrstMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {
        DDLMembType.SelectedIndex = 0;
        txtproid.Text = "";
        txtproname.Text = "";
        txtperiodFrm.Text = "";
        ddlperiodtypefrm.SelectedIndex = 0;
        txtperiodTo.Text = "";
        txtRate.Text = "";
        txtPenalty.Text = "";
        TxtAftrMat.Text = "";
    }
    protected void AssignValues(DataTable dt)
    {
        try
        {
            DDLMembType.SelectedValue = dt.Rows[0]["TDCUSTTYPE"].ToString();
            txtproid.Text = dt.Rows[0]["DEPOSITGL"].ToString();
            txtproname.Text = dt.Rows[0]["GLNAME"].ToString();
            txtperiodFrm.Text = dt.Rows[0]["PERIODFROM"].ToString();
            ddlperiodtypefrm.SelectedValue = dt.Rows[0]["PERIODTYPE"].ToString();
            txtperiodTo.Text = dt.Rows[0]["PERIODTO"].ToString();
            txtRate.Text = dt.Rows[0]["RATE"].ToString();
            txtPenalty.Text = dt.Rows[0]["PENALTY"].ToString();
            txteffectdate.Text = dt.Rows[0]["EFFECTDATE"].ToString();
            TxtAftrMat.Text = dt.Rows[0]["AFTERMATROI"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            result = DD.ModifyIntrestMaster(DDLMembType.SelectedValue.ToString(), txtproid.Text.ToString(), ddlperiodtypefrm.SelectedValue.ToString(), txtperiodFrm.Text.ToString(), txtperiodTo.Text.ToString(), txtRate.Text.ToString(), txtPenalty.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), "PCMAC", txteffectdate.Text.ToString(), ViewState["ID"].ToString(), TxtAftrMat.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Modified Succesfully......!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSIntMast_Mod_" + txtproid.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                Clear();
            }
            Bindgrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            result = DD.DeleteIntMast(Session["BRCD"].ToString(), ViewState["ID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Deleted Succesfully......!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSIntMast_Del_" + txtproid.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                Clear();
            }
            Bindgrid();


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtproname_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtproname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtproname.Text = custnob[0].ToString();
                txtproid.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                //TxtGLCD.Text = custnob[2].ToString();
                string[] AC = accop.Getaccno(txtproid.Text, Session["BRCD"].ToString(), "").Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                txtperiodFrm.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            result = 0;
            TxtAftrMat.Text = (TxtAftrMat.Text == "" ? "0" : TxtAftrMat.Text);
            if (DDLMembType.SelectedValue != "0" && txtproid.Text != "" && ddlperiodtypefrm.SelectedValue != "0" && txtperiodFrm.Text != "" && txtperiodTo.Text != "" && txteffectdate.Text != "" &&
                txtRate.Text != "" && txtPenalty.Text != ""  && TxtAftrMat.Text != "")
            {
                result = DD.EntryInterest(DDLMembType.SelectedValue.ToString(), txtproid.Text.ToString(), ddlperiodtypefrm.SelectedValue.ToString(), txtperiodFrm.Text.ToString(), txtperiodTo.Text.ToString(), txtRate.Text.ToString(), txtPenalty.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), "PCMAC", txteffectdate.Text.ToString(), TxtAftrMat.Text);
                if (result > 0)
                {
                    WebMsgBox.Show("Record Added Success", this.Page);
                    Bindgrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSIntMast_Add_" + txtproid.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                    txtproid.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Please enter all the details ", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

}