using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmChequeStockMaintain : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsChequeStockMaintain CK = new ClsChequeStockMaintain();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Flag"] = Request.QueryString["Flag"].ToString();
        BindGrid();
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["BRCD"].ToString();
            if (ViewState["Flag"].ToString() == "AD")
            {
                btnSubmit.Visible = true;
                btnModify.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                btnModify.Visible = true;
                btnSubmit.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnAuthorize.Visible = true;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = true;
            }
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = CK.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
            txtnoLeaves.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName.Text = custnob[0].ToString();
                txtProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtnoLeaves.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        string sql = "";
        int Result;
        Result = CK.BindGrid(grdChequeStock, Session["BRCD"].ToString());
    }

    public void ClearData()
    {
        if (Request.QueryString["Flag"].ToString() == "AD")
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtnoLeaves.Text = "";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = CK.ChequeStockInst(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtnoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["EntryDate"].ToString());

            if (cnt > 0)
            {
                BindGrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeStock_Add _" + txtProdCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "Successfully Inserted..!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = CK.ChequeStockModfy(ViewState["id"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtnoLeaves.Text.ToString());

            if (cnt > 0)
            {
                BindGrid();
              
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeStock_Mod _" + txtProdCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "Successfully Modified..!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = CK.ChequeStockDel(ViewState["id"].ToString(), Session["BRCD"].ToString());

            if (cnt > 0)
            {
                BindGrid();
               
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeStock_Del _" + txtProdCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "Successfully Deleted..!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorised_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = CK.ChequeStockAth(ViewState["id"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());

            if (cnt > 0)
            {
                BindGrid();
              
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeStock_Autho _" + txtProdCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "Successfully Authorised..!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else
            {
                lblMessage.Text = "Sorry Same User Not Authorised..!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void callData()
    {
        try
        {
            CheckStage();
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = CK.GetInfo(Session["BRCD"].ToString(), ViewState["id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtProdCode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    txtProdName.Text = DT.Rows[0]["SUBGLNAME"].ToString();
                    txtnoLeaves.Text = DT.Rows[0]["NOOFLEAVES"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                LinkButton objlink = (LinkButton)sender;
                string id = objlink.CommandArgument;
                ViewState["id"] = id;
                callData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CheckStage()
    {
        try
        {
            string ST = CK.GetStage(Session["BRCD"].ToString(), txtProdCode.Text, ViewState["id"].ToString());
            if (ST == "1003")
            {
                WebMsgBox.Show("Record Already Authorize......!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdChequeStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeStock.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}