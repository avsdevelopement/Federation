using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDigitalBanking : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    ClsDigitalBanking ClsBanking = new ClsDigitalBanking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            AutoProdName.ContextKey = Session["BRCD"].ToString();
            AutoCompleteExtender1.ContextKey = Session["BRCD"].ToString();
        }
    }
    #region TEXT CHANGE
    protected void txtFromBranch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            DT = ClsBanking.GetBRCD(txtFromBranch.Text);
            if (DT.Rows.Count > 0)
            {
                txtFName.Text = DT.Rows[0]["Midname"].ToString();
                txtToBranch.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Branch!!!", this.Page);
                txtFromBranch.Text = "";
                txtFromBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtToBranch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            DT = ClsBanking.GetBRCD(txtToBranch.Text);
            if (DT.Rows.Count > 0)
            {
                txtTName.Text = DT.Rows[0]["Midname"].ToString();
                txtProdCode.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Branch!!!", this.Page);
                txtToBranch.Text = "";
                txtToBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sResult = "";
            sResult = ClsBanking.GetProductName(Session["BRCD"].ToString(), txtProdCode.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtProdName.Text = CT[0].ToString();
                    txtProdCode.Text = CT[2].ToString();
                    ViewState["GlCode"] = CT[1].ToString();
                    txtDate.Focus();
                }
                else
                {
                    txtProdCode.Text = "";
                    WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                    txtProdCode.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void txtPlCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sResult = "";
            sResult = ClsBanking.GetProductName(Session["BRCD"].ToString(), txtPlCode.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtPlName.Text = CT[0].ToString();
                    txtPlCode.Text = CT[2].ToString();
                    ViewState["GlCode"] = CT[1].ToString();
                    btnTrail.Focus();
                }
                else
                {
                    txtPlCode.Text = "";
                    WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                    txtPlCode.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPlName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sResult = string.Empty;
            sResult = txtPlName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtPlName.Text = CT[0].ToString();
                txtPlCode.Text = CT[1].ToString();
                ViewState["GlCodePL"] = CT[2].ToString();
                btnTrail.Focus();
            }
            else
            {
                txtPlName.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtPlName.Focus();
            }
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
            string sResult = string.Empty;
            sResult = txtProdName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[2].ToString();
                ViewState["GlCode"] = CT[1].ToString();
                txtDate.Focus();
            }
            else
            {
                txtProdCode.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion 

    #region BUTTON CLICK
    protected void btnTrail_Click(object sender, EventArgs e)
    {
        try
        {
            //DT = ClsBanking.getTrailBank(txtFromBranch.Text, txtToBranch.Text, txtProdCode.Text, txtCharges.Text, txtPlCode.Text, txtDate.Text, "1", Session["EntryDate"].ToString(), Session["MID"].ToString());
            //if (DT.Rows.Count > 0)
            //{
            //    GridView1.DataSource=DT;
            //    GridView1.DataBind();
            //    //string txt = string.Empty;

            //    //foreach (TableCell cell in GridView1.HeaderRow.Cells)
            //    //{
            //    //    //Add the Header row for Text file.
            //    //    txt += cell.Text + "\t\t";
            //    //}

            //    ////Add new line.
            //    //txt += "\r\n";

            //    //foreach (GridViewRow row in GridView1.Rows)
            //    //{
            //    //    foreach (TableCell cell in row.Cells)
            //    //    {
            //    //        //Add the Data rows.
            //    //        txt += cell.Text + "\t\t";
            //    //    }

            //    //    //Add new line.
            //    //    txt += "\r\n";
            //    //}

            //    ////Download the Text file.
            //    //Response.Clear();
            //    //Response.Buffer = true;
            //    //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.txt");
            //    //Response.Charset = "";
            //    //Response.ContentType = "application/text";
            //    //Response.Output.Write(txt);
            //    //Response.Flush();
            //    //Response.End();
            //}
             string redirectURL = "FrmRView.aspx?FromBRCD="+txtFromBranch.Text+"&ToBRCD="+txtToBranch.Text+"&Prodcode="+txtProdCode.Text+"&Charges="+txtCharges.Text+"&Date="+txtDate.Text+"&PL="+txtPlCode.Text+"&UName="+Session["UserName"].ToString()+"&rptname=RptDigitalBanking.rdlc";
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            DT = ClsBanking.getTrailBank(txtFromBranch.Text, txtToBranch.Text, txtProdCode.Text, txtCharges.Text, txtPlCode.Text, txtDate.Text, "2",Session["EntryDate"].ToString(),Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCharges.Text = "";
                txtDate.Text = "";
                txtFName.Text = "";
                txtFromBranch.Text = "";
                txtPlCode.Text = "";
                txtPlName.Text = "";
                txtProdCode.Text = "";
                txtProdName.Text = "";
                txtTName.Text = "";
                txtToBranch.Text = "";
                WebMsgBox.Show("Data Transfer Successfully!!! SetNo="+DT.Rows[0]["Massage"].ToString()+"",this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnExist_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmblank.aspx");
    }
    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
             DataTable DT = new DataTable();
            DT = ClsBanking.getTrailBank(txtFromBranch.Text, txtToBranch.Text, txtProdCode.Text, txtCharges.Text, txtPlCode.Text, txtDate.Text, "1", Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                GridView1.DataSource = DT;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void print_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;
        GridView1.DataSource = DT;
        GridView1.DataBind();
        GridView1.UseAccessibleHeader = true;
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        GridView1.Attributes["style"] = "border-collapse:separate";
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
            {
                row.Attributes["style"] = "page-break-after:always;";
            }
        }
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
        sb.Append(style + gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();");
        sb.Append("};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}