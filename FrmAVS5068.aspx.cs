using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVS5068 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsRDFDSettng RF = new ClsRDFDSettng();
    DataTable DT = new DataTable();
    int res = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
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
                BD.BindRDHeading(ddlheading1);
                BD.BindFDHeading(ddlHeading);
                if (ddlSelection.SelectedValue == "0")
                {
                    div_Receipt.Visible = true;
                    Div_RDGrd.Visible = true;
                    div_FD.Visible = false;
                    Div_FDGrd.Visible = false;
                   bindgrid();
                    ddlheading1.Focus();
                }
                else
                {
                    div_Receipt.Visible = false;
                    Div_RDGrd.Visible = false;
                    div_FD.Visible = true;
                    Div_FDGrd.Visible = true;
                    
                    bindgrid();
                    ddlHeading.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            RF.BindRD(GrdRD);
            RF.BindFD(GrdFD);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlSelection_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSelection.SelectedValue == "0")
        {
            div_Receipt.Visible = true;
            Div_RDGrd.Visible = true;
            div_FD.Visible = false;
            Div_FDGrd.Visible = false;
            bindgrid();
            ddlheading1.Focus();
        }
        else
        {
            div_Receipt.Visible = false;
            Div_RDGrd.Visible = false;
            div_FD.Visible = true;
            Div_FDGrd.Visible = true;

            bindgrid();
            ddlHeading.Focus();
        }
    }
    protected void ddlheading1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        try
        {
            res = RF.InsertRD(ddlheading1.SelectedItem.Text, TxtRowNo.Text, TxtColNo.Text, Session["MID"].ToString(), ddlheading1.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Saved Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RDSetting_Add _" + ddlheading1.SelectedItem.Text + "," + TxtRowNo.Text + "," + TxtColNo.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clearRD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnMod_Click(object sender, EventArgs e)
    {
        try
        {
            res = RF.ModifyRD(ViewState["ID"].ToString(), ddlheading1.SelectedItem.Text, TxtRowNo.Text, TxtColNo.Text, Session["MID"].ToString(), ddlheading1.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Modified Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RDSetting_Mod _" + ddlheading1.SelectedItem.Text + "," + TxtRowNo.Text + "," + TxtColNo.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clearRD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        try
        {
            res = RF.DeleteRD(ViewState["ID"].ToString());
            if (res > 0)
            {
                WebMsgBox.Show("Data Deleted Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RDSetting_Del_" + ddlheading1.SelectedItem.Text + "," + TxtRowNo.Text + "," + TxtColNo.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clearRD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdRD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RF.BindRD(GrdRD);
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            div_Receipt.Visible = true;
            div_FD.Visible = false;
            Div_RDGrd.Visible = true;
            Div_FDGrd.Visible = false;
            BtnSub.Visible = true;
            BtnMod.Visible = false;
            BtnDel.Visible = false;
            clearRD();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnSub.Visible = false;
            BtnMod.Visible = true;
            BtnDel.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetails(string id)
    {
        try
        {

            DT = RF.GetRD(id);
            if (DT.Rows.Count > 0)
            {
                ddlheading1.SelectedValue = DT.Rows[0]["ColumnStatus"].ToString();
                TxtRowNo.Text = DT.Rows[0]["PRows"].ToString();
                TxtColNo.Text = DT.Rows[0]["PColumn"].ToString();
             }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnSub.Visible = false;
            BtnMod.Visible = false;
            BtnDel.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd1_Click(object sender, EventArgs e)
    {
        try
        {
            div_Receipt.Visible = false;
            div_FD.Visible = true;
            Div_RDGrd.Visible = false;
            Div_FDGrd.Visible = true;
            BtnADDF.Visible = true;
            BtnModifyF.Visible = false;
            BtnDeleteF.Visible = false;
            ClearFD();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkSelect1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetailsFD(ViewState["ID"].ToString());
            BtnADDF.Visible = false;
            BtnModifyF.Visible = true;
            BtnDeleteF.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetailsFD(ViewState["ID"].ToString());
            BtnADDF.Visible = false;
            BtnModifyF.Visible = false;
            BtnDeleteF.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetailsFD(string id)
    {
        try
        {
           DT = RF.GetFDDetails(id);
           if (DT.Rows.Count > 0)
            {
                ddlHeading.SelectedValue = DT.Rows[0]["ColumnStatus"].ToString();
                TxtBAlign.Text = DT.Rows[0]["PRows"].ToString();
                TxtAAlign.Text = DT.Rows[0]["PColumn"].ToString();
             }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdFD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void BtnADDF_Click(object sender, EventArgs e)
    {

        try
        {
            res = RF.InsertFD(ddlHeading.SelectedItem.Text, TxtBAlign.Text, TxtAAlign.Text, Session["MID"].ToString(), ddlHeading.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Saved Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDSetting_Add_" + ddlHeading.SelectedItem.Text + "," + TxtBAlign.Text + "," + TxtAAlign.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearFD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModifyF_Click(object sender, EventArgs e)
    {
        try
        {
            res = RF.ModifyFD(ViewState["ID"].ToString(),ddlHeading.SelectedItem.Text, TxtBAlign.Text, TxtAAlign.Text, Session["MID"].ToString(), ddlHeading.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Saved Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDSetting_Mod_" + ddlHeading.SelectedItem.Text + "," + TxtBAlign.Text + "," + TxtAAlign.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearFD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDeleteF_Click(object sender, EventArgs e)
    {
        try
        {
            res = RF.DeleteFD(ViewState["ID"].ToString());
            if (res > 0)
            {
                WebMsgBox.Show("Data Deleted Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDSetting_Del_" + ddlHeading.SelectedItem.Text + "," + TxtBAlign.Text + "," + TxtAAlign.Text + "," + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearFD();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clearRD()
    {
        ddlheading1.SelectedValue = "0";
        TxtRowNo.Text="";
            TxtColNo.Text="";
    }
    public void ClearFD()
    {
        ddlHeading.SelectedValue = "0";
            TxtBAlign.Text="";
            TxtAAlign.Text="";
   }

}