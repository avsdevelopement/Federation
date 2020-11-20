using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmCKYCData : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCommon cmn = new ClsCommon();
    ClsDDSIntView DV = new ClsDDSIntView();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                txtCustNo.Text = Request.QueryString["CUSTNO"].ToString();
                TxtBrID.Text = Session["Brcd"].ToString();
                DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
        protected void txtCustNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DT = CD.GetStage(txtCustNo.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
                {
                    WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                    return;
                }
                else
                {
                    DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                    txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

        protected void txtCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string CUNAME = txtCustName.Text;
                string[] custnob = CUNAME.Split('_');

                if (custnob.Length > 1)
                {
                    txtCustName.Text = custnob[0].ToString();
                    txtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    DT = CD.GetStage(txtCustNo.Text);

                    if (DT.Rows[0]["STAGE"].ToString() == "1001")
                    {
                        WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                        return;
                    }
                    else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                    {
                        WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                        return;
                    }
                    else
                    {

                        DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

        protected void TxtToCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string CUNAME = TxtToCustName.Text;
                string[] custnob = CUNAME.Split('_');

                if (custnob.Length > 1)
                {
                    TxtToCustName.Text = custnob[0].ToString();
                    TxtToCustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    DT = CD.GetStage(TxtToCustno.Text);

                    if (DT.Rows[0]["STAGE"].ToString() == "1001")
                    {
                        WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                        return;
                    }
                    else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                    {
                        WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                        return;
                    }
                    else
                    {

                        DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

        protected void TxtToCustno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DT = CD.GetStage(TxtToCustno.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
                {
                    WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                    return;
                }
                else
                {
                    DT1 = CD.GetCustName(TxtToCustno.Text.ToString());
                    TxtToCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtFDate.Text != "")
                {
                    FL = "Insert";
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "KYCReport" + "_" + TxtFDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + TxtFDate.Text + "&FCustNo=" + txtCustNo.Text + "&TCustNo=" + TxtToCustno.Text + "&RFlag=" + Rdb_No.SelectedValue + " &rptname=RptCKYCList.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    WebMsgBox.Show("Enter the details", this.Page);
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
