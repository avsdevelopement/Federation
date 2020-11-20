using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAppliedInterest : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsAppliedInterest intCalc = new ClsAppliedInterest();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //added by ankita 07/10/2017 to make user frndly
                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtBrCode.Text = Session["BRCD"].ToString();
                txtAsOnDate.Text = Session["EntryDate"].ToString();
                BindBranch(ddlBrName);
                ddlBrName.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();
            autoglname.ContextKey = txtBrCode.Text.Trim().ToString();
            txtPrCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtPrCode.Text.Trim().ToString(), txtBrCode.Text.Trim().ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["GlCode"] = GLCODE[1].ToString();
            string PDName = customcs.GetProductName(txtPrCode.Text.Trim().ToString(), txtBrCode.Text.Trim().ToString());
            if (PDName != null && PDName != "")
            {
                txtPrName.Text = PDName;
                txtAsOnDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtPrCode.Text = "";
                txtPrCode.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtPrName.Text.ToString();
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtPrName.Text = CT[0].ToString();
                txtPrCode.Text = CT[1].ToString();

                string[] GLS = BD.GetAccTypeGL(txtPrCode.Text.Trim().ToString(), txtBrCode.Text.Trim().ToString()).Split('_');
                ViewState["GlCode"] = GLS[1].ToString();

                txtAsOnDate.Focus();
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
            string redirectURL = "FrmReportViewer.aspx?BRCD=" + txtBrCode.Text.Trim().ToString() + "&GLCD=" + ViewState["GlCode"].ToString() + "&PRCD=" + txtPrCode.Text.Trim().ToString() + "&EDate=" + txtAsOnDate.Text.Trim().ToString() + "&TYPE=" + ddlCalType.SelectedValue.ToString() + "&rptname=RptAppliedInterest.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_TextReport_Click(object sender, EventArgs e)
    {
        try
        {
            

            DataTable dt = new DataTable();
            dt = intCalc.GetInterestCalTextRpt(txtBrCode.Text.Trim().ToString(), ViewState["GlCode"].ToString(), txtPrCode.Text.Trim().ToString(), txtAsOnDate.Text.Trim().ToString(), ddlCalType.SelectedValue.ToString());
            StringWriter writer = new StringWriter();
            if (dt == null)
            {
                WebMsgBox.Show("Report Generation Fail!", this.Page);
                return;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                WebMsgBox.Show("Data not found to generate report!", this.Page);
                return;
            }

            string filename = "DividenCalc.txt";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    writer.WriteLine(dt.Rows[i][0]);
                }
            }
            writer.Close();
            Response.Output.Write(writer);


            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

   


}