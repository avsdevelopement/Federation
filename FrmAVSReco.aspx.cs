
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVSReco : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsUpload upd = new ClsUpload();
    ClsAVSReconcilation obj = new ClsAVSReconcilation();
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
                txtBrcd.Text = Session["BRCD"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                lblCount.Text = "Total Row Count: " + grdView.Rows.Count.ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                txtBankCode.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(txtBankCode.Text, txtBrcd.Text.ToString()).Split('_');
                //ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPType.Text + "_" + ViewState["DRGL"].ToString();

                if (TxtProName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtBankCode.Text = "";
                    txtBankCode.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GL = BD.GetAccTypeGL(txtBankCode.Text, txtBrcd.Text.Trim()).Split('_'); ;
            TxtProName.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtBrcd_TextChanged(object sender, EventArgs e)
    {
        autoglname.ContextKey = txtBrcd.Text.ToString();
    }
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcd.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter Branch Code!", this.Page);
                return;
            }
            if (TxtFDate.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter From Date!", this.Page);
                return;
            }

            if (TxtTDate.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter To Date!", this.Page);
                return;
            }

            if (fuBorrDataExcel.HasFile)
            {
                string FilePath = Server.MapPath("~/UploadFiles/" + fuBorrDataExcel.PostedFile.FileName);
                string Extension = System.IO.Path.GetExtension(fuBorrDataExcel.PostedFile.FileName);
                fuBorrDataExcel.SaveAs(FilePath);
                string strMsg = upd.ImportDataFromExcel(FilePath, Extension, "Yes", Session["MID"].ToString(), Session["EntryDate"].ToString());
                //string strMsg = upd.UploadBorrowerFile(FilePath, "C0007", Session["user"].ToString());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CISCS", "alert('" + strMsg + "')", true);
                //oFF.DownloadExcelFile(oBorr.BorrowerData("DOWNLOAD EXCEL", ""));
                grdView.DataSource = obj.getTable("MATCH", Session["MID"].ToString(), txtBrcd.Text.Trim(), txtBankCode.Text, TxtFDate.Text, TxtTDate.Text);
                grdView.DataBind();
                lblCount.Text = "Total Row Count: " + grdView.Rows.Count.ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CISCS", "alert('File not selected!!!')", true);
                return;
            }

            return;
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void btnMatch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcd.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter Branch Code!", this.Page);
                return;
            }
            if (TxtFDate.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter From Date!", this.Page);
                return;
            }

            if (TxtTDate.Text.Trim() == "")
            {
                WebMsgBox.Show("Please Enter To Date!", this.Page);
                return;
            }
            string ans = "";
            ans = obj.getResult("RECO", Session["MID"].ToString(), txtBrcd.Text.Trim(), txtBankCode.Text, TxtFDate.Text, TxtTDate.Text);
            grdView.DataSource = null;
            grdView.DataBind();
            lblCount.Text = "Total Row Count: " + grdView.Rows.Count.ToString();
            if (ans == null || ans == "")
                WebMsgBox.Show("Reconcilation Fail!", this.Page);
            else
                WebMsgBox.Show(ans, this.Page);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnCleatData_Click(object sender, EventArgs e)
    {
        try
        {
            txtBrcd.Text = "";
            TxtFDate.Text = "";
            TxtTDate.Text = "";
            txtBankCode.Text = "";
            TxtProName.Text = "";
            grdView.DataSource = null;
            grdView.DataBind();
            if (Convert.ToInt64(obj.getTable(FLAG: "TOTALCOUNT", MID: Session["MID"].ToString()).Rows[0]["CNT"].ToString()) == 0)
            {
                WebMsgBox.Show("Data not available for clear!", this.Page);
            }
            else
            {
                obj.getTable(FLAG: "DELETERECORDS", MID: Session["MID"].ToString());
                WebMsgBox.Show("Clear Data Successfully!", this.Page);
            }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.ClearContent();
            //response.Clear();
            //response.ContentType = "excel";
            //response.AppendHeader("Content-Disposition", "attachment; filename=AVS_RECONCILATION.xlsx");
            //response.TransmitFile(Server.MapPath("~/Templates/AVS_RECONCILATION.xlsx"));
            //response.Flush();
            //response.End();
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment; filename=~/Templates/AVS_RECONCILATION.xlsx");
            //response.Flush();
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
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