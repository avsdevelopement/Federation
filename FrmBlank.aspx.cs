using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

public partial class FrmBlank : System.Web.UI.Page
{
    ClsSIPost SP = new ClsSIPost();
    DbConnection conn = new DbConnection();
    ClsCommon CMN = new ClsCommon();
    ClsLogin LG = new ClsLogin();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsAVS5090 AT = new ClsAVS5090();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!String.IsNullOrEmpty(Request.QueryString["ShowMessage"]))// Dhanya Shetty//07/09/2017
        {
            WebMsgBox.Show("Access Denied for this user...!!", this.Page);
        }
        string EDT = LG.openDay(Session["BRCD"].ToString());
        string SIDT = SP.GetSI_EXEU(Session["BRCD"].ToString());
        SIDT = string.IsNullOrEmpty(SIDT) ? "01/01/1990" : SIDT;

        if (Convert.ToDateTime(conn.ConvertDate(SIDT)) == Convert.ToDateTime(conn.ConvertDate(EDT)))
        {
            string MSG = CMN.GetUniversalPara("SI_MSG");
            Lbl_Show.Text = MSG + "," + SIDT.ToString();
        }
        BindGrid();
        // BD.BindDateHis(ddlDate, "BM");

    }
    public void BindGrid()
    {
        try
        {
            DT = AT.GetDetails(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            //lblname.Text = DateTime.Now.Date.ToString("dd.MM.yyyy");
            GridDetails.DataSource = DT;
            GridDetails.DataBind();
            if (ddlDate.SelectedItem.Text != "--Select--")
            {
                //lblname.Text = ddlDate.SelectedItem.Text;
                DT = AT.GetDetails(ddlDate.SelectedItem.Text);
                GridDetails.DataSource = DT;
                GridDetails.DataBind();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDetails.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void LnkDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            float id = float.Parse((sender as LinkButton).CommandArgument);
            SqlConnection Conn = new SqlConnection(conn.DbName());
            SqlCommand com = new SqlCommand("Select Requirement as Name,ImageCode as data,Image_Name from AVS5066 where Id='" + Convert.ToDouble(id) + "' ", Conn);
            Conn.Open();
            SqlDataReader dr = com.ExecuteReader();

            if (dr.Read())
            {
                if (dr["Image_Name"].ToString() != "")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "Application/pdf";
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite((byte[])dr["data"]);
                    Response.End();

                }
                else
                {
                    WebMsgBox.Show("There is no document", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "There is no document ", true);
                    String x = "<script type='text/javascript'>self.close();</script>";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", x, false);
                }
            }
            else
            {
                Response.Redirect("FrmBlank.aspx");
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
}