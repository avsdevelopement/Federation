using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmChequeStandingInstruction : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsChequeStandingInst CSI = new ClsChequeStandingInst();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();

        if (!IsPostBack)
        {
            rbtnBlank.Checked = true;
            if (Flag == "AD")
            {
                btnSubmit.Visible = true;
                btnAuthorised.Visible = false;
                btnDelete.Visible = false;
                btnModify.Visible = false;
            }
            else if (Flag == "MD")
            {
                BindGrid();
                btnModify.Visible = true;
                btnSubmit.Visible = false;
                btnAuthorised.Visible = false;
                btnDelete.Visible = false;
            }
            else if (Flag == "AT")
            {
                BindGrid();
                btnSubmit.Visible = false;
                btnAuthorised.Visible = true;
                btnDelete.Visible = false;
                btnModify.Visible = false;
            }
            else if (Flag == "DL")
            {
                BindGrid();
                btnSubmit.Visible = false;
                btnAuthorised.Visible = false;
                btnDelete.Visible = true;
                btnModify.Visible = false;
            }
        }
    }

    protected void rbtnPersonalised_CheckedChanged(object sender, EventArgs e)
    {
        rbtnPersonalised.Checked = true;
        rbtnBlank.Checked = false;
        divSeries.Visible = true;
        txtFromSeries.Focus();
    }

    protected void rbtnBlank_CheckedChanged(object sender, EventArgs e)
    {
        rbtnBlank.Checked = true;
        rbtnPersonalised.Checked = false;
        divSeries.Visible = false;
    }

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), txtProdCode.Text);
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise.........!!";
                ModalPopup.Show(this.Page);
                txtAccNo.Text = "";
                txtAccName.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
            }
            else
            {

                DataTable DT = new DataTable();
                DT = CSI.GetCustName(txtProdCode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtAccName.Text = DT.Rows[0]["CustName"].ToString();
                }

                rbtnPersonalised.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = CSI.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
            txtAccNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromSeries_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string CHQT;
            //if (rbtnBlank.Checked)
            //{
            //    CHQT = "Blank";
            //}
            //else
            //{
            //    CHQT = "Personalized";
            //}

            //dt = new DataTable();
            //dt = CSI.CheckSeriesLastUsed(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), CHQT.ToString());
            //if (dt.Rows.Count > 0)
            //{
                dt = new DataTable();
                dt = CSI.CheckSeriesAvailable(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtFromSeries.Text.ToString());

                if (dt.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = CSI.CheckSeriesAllocated(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtFromSeries.Text.ToString());

                    if (dt.Rows.Count > 0)
                    {
                        lblMessage.Text = "Series Number Allready Allocated..!";
                        ModalPopup.Show(this.Page);
                        txtFromSeries.Text = "";
                        txtFromSeries.Focus();
                    }
                    txtToSeries.Focus();
                }
                else
                {
                    lblMessage.Text = "Series Number Not Available..!";
                    ModalPopup.Show(this.Page);
                    txtFromSeries.Text = "";
                    txtFromSeries.Focus();
                }
            //}
            //else
            //{
            //    lblMessage.Text = "Series Number Allready Allocated up to '" + dt.Rows[0]["LASTUSED"].ToString() + "'!";
            //    ModalPopup.Show(this.Page);
            //    txtFromSeries.Text = "";
            //    txtFromSeries.Focus();
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtToSeries_TextChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        dt = CSI.CheckSeriesAvailable(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtToSeries.Text.ToString());

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
            dt = CSI.CheckSeriesAllocated(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtToSeries.Text.ToString());

            if (dt.Rows.Count > 0)
            {
                lblMessage.Text = "Series Number Allready Allocated..!";
                ModalPopup.Show(this.Page);
                txtFromSeries.Text = "";
                txtToSeries.Text = "";
                txtFromSeries.Focus();
            }
            else
            {
                int count1 = Convert.ToInt32(txtFromSeries.Text);
                int count2 = Convert.ToInt32(txtToSeries.Text);
                int Result = Math.Abs(count1 - (count2 + 1));
                lblSeriesTotal.Text = Math.Abs(Result).ToString();
            }
        }
        else
        {
            lblMessage.Text = "Series Number Not Available..!";
            ModalPopup.Show(this.Page);
            txtToSeries.Text = "";
            txtToSeries.Focus();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(lblSeriesTotal.Text) == Convert.ToInt32(txtNoLeaves.Text))
            {
                string ChqType = "";
                string leaf;

                if (rbtnPersonalised.Checked)
                {
                    ChqType = "Personalized";
                }
                else if (!rbtnPersonalised.Checked)
                {
                    ChqType = "Blank";
                }

                int cnt = CSI.ChequeReq(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtFromSeries.Text.ToString(), txtToSeries.Text.ToString(), ChqType.ToString(), txtNoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["UserName"].ToString(), Session["EntryDate"].ToString());

                if (cnt > 0)
                {
                    BindGrid();
                    ClearData();
                    lblMessage.Text = "Successfully Inserted..!";
                    ModalPopup.Show(this.Page);
                }
            }
            else
            {
                lblMessage.Text = "Please Enter Proper Series Number Or No Of Books..!";
                ModalPopup.Show(this.Page);
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

        sql = "SELECT id,SUBGLCODE, SUBGLNAME, ACCNO, FSERIES, TSERIES, CHEQUETYPE, LEAF, CHEQUEBOOK, NOOFLEAF FROM AVS_ChequeBookRequest WHERE BRCD = '" + Session["BRCD"].ToString() + "' AND STAGE = 1001";
        Result = conn.sBindGrid(grdChequeRequest, sql);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string ChqType = "";
            string leaf;

                if (rbtnPersonalised.Checked)
                {
                    ChqType = "Personalized";
                }
                else if (!rbtnPersonalised.Checked)
                {
                    ChqType = "Blank";
                }

                int cnt = CSI.ChequeReqDel(ViewState["id"].ToString(),Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtFromSeries.Text.ToString(), txtToSeries.Text.ToString(), ChqType.ToString(), txtNoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["UserName"].ToString(), Session["EntryDate"].ToString());

                if (cnt > 0)
                {
                    BindGrid();
                    ClearData();
                    lblMessage.Text = "Successfully Deleted..!";
                    ModalPopup.Show(this.Page);
                }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtNoOfLeaf_TextChanged(object sender, EventArgs e)
    {
        int count;
        int total = Convert.ToInt32(txtNoLeaves.Text);

    }

    public void ClearData()
    {
        if (Request.QueryString["Flag"].ToString() == "AD")
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtFromSeries.Text = "";
            txtToSeries.Text = "";
            lblSeriesTotal.Text = "";
            rbtnBlank.Checked = true;
            txtNoLeaves.Text = "";
        }
    }

    protected void grdChequeRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeRequest.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            string ChqType = "";
            string leaf;

                if (rbtnPersonalised.Checked)
                {
                    ChqType = "Personalized";
                }
                else if (!rbtnPersonalised.Checked)
                {
                    ChqType = "Blank";
                }

                int cnt = CSI.ChequeReqAth(ViewState["id"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtFromSeries.Text.ToString(), txtToSeries.Text.ToString(), ChqType.ToString(), txtNoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["UserName"].ToString(), Session["EntryDate"].ToString());

                if (cnt > 0)
                {
                    BindGrid();
                    ClearData();
                    lblMessage.Text = "Successfully Authorised..!";
                    ModalPopup.Show(this.Page);
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

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            if (!rbtnBlank.Checked)
            {
                if (Convert.ToInt32(lblSeriesTotal.Text) == Convert.ToInt32(txtNoLeaves.Text))
                {
                    string ChqType = "";
                    string leaf;

                    if (rbtnPersonalised.Checked)
                    {
                        ChqType = "Personalized";
                    }
                    else if (!rbtnPersonalised.Checked)
                    {
                        ChqType = "Blank";
                    }

                    int cnt = CSI.ChequeModfy(ViewState["id"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtFromSeries.Text.ToString(), txtToSeries.Text.ToString(), ChqType.ToString(), txtNoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["UserName"].ToString(), Session["EntryDate"].ToString());

                    if (cnt > 0)
                    {
                        BindGrid();
                        ClearData();
                        lblMessage.Text = "Successfully Modified..!";
                        ModalPopup.Show(this.Page);
                    }
                }
                else
                {
                    lblMessage.Text = "Please Enter Proper Series Number Or No Of Books..!";
                    ModalPopup.Show(this.Page);
                }
            }
            else
            {
                string ChqType = "";
                string leaf;

                if (rbtnPersonalised.Checked)
                {
                    ChqType = "Personalized";
                }
                else if (!rbtnPersonalised.Checked)
                {
                    ChqType = "Blank";
                }

                int cnt = CSI.ChequeModfy(ViewState["id"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtFromSeries.Text.ToString(), txtToSeries.Text.ToString(), ChqType.ToString(), txtNoLeaves.Text.ToString(), Session["MID"].ToString(), conn.PCNAME().ToString(), Session["UserName"].ToString(), Session["EntryDate"].ToString());

                if (cnt > 0)
                {
                    BindGrid();
                    ClearData();
                    lblMessage.Text = "Successfully Modified..!";
                    ModalPopup.Show(this.Page);
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
            if (Request.QueryString["Flag"].ToString() != "AD")
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

    public void callData()
    {
        try
        {
            string RC = CheckStage();
            if (RC == "1003")
            {
                WebMsgBox.Show("Record Already Authorized......!!", this.Page);
                return;
            }
            if (Request.QueryString["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = CSI.GetInfo(Session["BRCD"].ToString(), ViewState["id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtProdCode.Text = DT.Rows[0]["Subglcode"].ToString();
                    txtProdName.Text = DT.Rows[0]["Subglname"].ToString();
                    txtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                    
                    if (DT.Rows[0]["CHEQUETYPE"].ToString() == "Blank")
                    {
                        rbtnBlank.Checked = true;
                        rbtnPersonalised.Checked = false;
                    }
                    else
                    {
                        rbtnPersonalised.Checked = true;
                        rbtnBlank.Checked = false;
                        divSeries.Visible = true;
                        txtFromSeries.Text = DT.Rows[0]["Fseries"].ToString();
                        txtToSeries.Text = DT.Rows[0]["Tseries"].ToString();
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckStage()
    {
        string RC = "";
        try
        {
            string sql = "SELECT STAGE FROM AVS_ChequeBookRequest WHERE SUBGLCODE='" + txtProdCode.Text.ToString() + "' AND ACCNO = '"+ txtAccNo.Text.ToString() +"' AND stage <> 1004 AND ID = '" + ViewState["id"].ToString() + "'";
            RC = conn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }

    protected void txtNoLeaves_TextChanged(object sender, EventArgs e)
    {

    }
}