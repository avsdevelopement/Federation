using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5029 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    ClsBindDropdown bd = new ClsBindDropdown();
    ClsAccopen accop = new ClsAccopen();
    ClsAVS5029 avs5029 = new ClsAVS5029();
    DbConnection conn = new DbConnection();
    ClsCommon com = new ClsCommon();

    DataTable DT = new DataTable();
    DataTable dt2 = new DataTable();
    string sResult = "";
    int RES = 0, result = 0;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                string allow = com.ChkECS(Session["BRCD"].ToString());
                if (allow == "Y")
                {
                    AutoCompleteExtenderminor.ContextKey = Session["BRCD"].ToString();
                    autooffcname.ContextKey = Session["BRCD"].ToString();
                    bd.BindDesig(ddlDesig);
                    bd.BindACCTYPE(ddlCustType);
                    if (!string.IsNullOrEmpty(Request.QueryString["CUSTNO"]))
                    {
                        TxtCustno.Text = Request.QueryString["CUSTNO"].ToString();
                        CustNoChanged();
                    }
                    EmptyGridBind();
                }
                else
                {
                    Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
                }

                TxtCustno.Focus();
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CustNoChanged();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtCustname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtCustname.Text.Split('_');
            if (custnob.Length > 1)
            {
                TxtCustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                CustNoChanged();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void CustNoChanged()
    {
        try
        {
            string sql, AT;
            sql = AT = "";

            if (TxtCustno.Text == "")
            {
                return;
            }
            string custname = accop.GetcustnameYN(accop.GetCENTCUST(), TxtCustno.Text, Session["BRCD"].ToString());

            if (custname != null)
            {
                string[] name = custname.Split('_');
                TxtCustname.Text = name[0].ToString();
                TxtEmpNo.Text = TxtCustno.Text;
                bindgrid();
                TxtDivNO.Focus();
            }

            string RC = TxtCustname.Text;
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                TxtCustno.Text = "";
                TxtCustno.Focus();
                return;
            }
            RES = avs5029.ChkExist(TxtCustno.Text);
            if (RES > 0)
            {
                BtnAddNew.Enabled = false;
                div_Main.Visible = false;
                BtnAddNew.Visible = true;
                BtnModify.Visible = false;
                Submit.Visible = false;
                BtnAuthorise.Visible = false;
                BtnDelete.Visible = false;
                Exit.Visible = false;
            }
            else
            {
                BtnAddNew.Enabled = true;
                div_Main.Visible = true;
                BtnAddNew.Visible = false;
                BtnModify.Visible = false;
                Submit.Visible = true;
                BtnAuthorise.Visible = false;
                BtnDelete.Visible = false;
                Exit.Visible = false;
                bindgrid();
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    protected void TxtDOJ_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtRetPeriod.Text != "")
            {
                TxtRetireDt.Text = conn.AddYear(TxtDOJ.Text, TxtRetPeriod.Text);
            }
            TxtRetPeriod.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtRetireDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtConfDt.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtRetPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtRetireDt.Text = conn.AddYear(TxtDOJ.Text, TxtRetPeriod.Text);
            TxtConfDt.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            clear();

            div_Main.Visible = true;
            divSuretyStart.Visible = false;
            BtnAddNew.Visible = false;
            BtnModify.Visible = false;
            Submit.Visible = true;
            BtnAuthorise.Visible = false;
            BtnDelete.Visible = false;
            Exit.Visible = true;
            lblActivity.Text = "Add Customer";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["TYPE"]) == "EMP")
            {
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                    string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                    string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                    string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                    string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;

                    if (bnkname != "" && brname != "" && Accno != "")
                        result = avs5029.Insertbnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                }

                RES = avs5029.Insert(TxtCustno.Text, TxtEmpNo.Text, txtmemno.Text,  TxtDivName.Text, TxtOffName.Text, TxtDOJ.Text, TxtRetireDt.Text, TxtRetPeriod.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), ddlDesig.SelectedValue.ToString(), TxtConfDt.Text, TxtDivNO.Text, TxtOffNo.Text, txtSAPNo.Text, adhar: txtAdhar.Text, pan: txtPancard.Text);
                if (RES > 0)
                {
                    WebMsgBox.Show("Data added successfully..!!", this.Page);
                    div_Main.Visible = false;
                    BtnAddNew.Visible = true;
                    BtnModify.Visible = false;
                    Submit.Visible = false;
                    BtnAuthorise.Visible = false;
                    BtnDelete.Visible = false;
                    Exit.Visible = false;
                    bindgrid();
                    clear();
                }
            }
            else if (Convert.ToString(ViewState["TYPE"]) == "SUR")
            {
                if (ddlOption.SelectedValue == "1")
                {
                    txtPAmt.Text = txtPAmt.Text == "" ? "0" : txtPAmt.Text;
                    txtIntAmt.Text = txtIntAmt.Text == "" ? "0" : txtIntAmt.Text;

                    sResult = avs5029.CheckSurety(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtCustno.Text, ViewState["MemberNo"].ToString(), "1");
                    if (Convert.ToDouble(sResult) <= 0)
                    {
                        result = avs5029.StartSurety(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtCustno.Text, ViewState["MemberNo"].ToString(), txtPAmt.Text, txtIntAmt.Text, Session["MID"].ToString());
                        if (result > 0)
                        {
                            WebMsgBox.Show("sucessfully surity start ..!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Already surety start ..!!", this.Page);
                        return;
                    }
                }
                else if (ddlOption.SelectedValue == "2")
                {
                    WebMsgBox.Show("Only surety start here ..!!", this.Page);
                    return;
                }
            }
            else
            {
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                    string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                    string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                    string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                    string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;

                    if (bnkname != "" && brname != "" && Accno != "")
                        result = avs5029.Insertbnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                }

                RES = avs5029.Insert(TxtCustno.Text, TxtEmpNo.Text,txtmemno.Text.ToString(), TxtDivName.Text, TxtOffName.Text, TxtDOJ.Text, TxtRetireDt.Text, TxtRetPeriod.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), ddlDesig.SelectedValue.ToString(), TxtConfDt.Text, TxtDivNO.Text, TxtOffNo.Text, SAPNO: txtSAPNo.Text,adhar:txtAdhar.Text.ToString(),pan:txtPancard.Text);
                if (RES > 0)
                {
                    WebMsgBox.Show("Data added successfully..!!", this.Page);
                    div_Main.Visible = false;
                    BtnAddNew.Visible = true;
                    BtnModify.Visible = false;
                    Submit.Visible = false;
                    BtnAuthorise.Visible = false;
                    BtnDelete.Visible = false;
                    Exit.Visible = false;
                    bindgrid();
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["TYPE"]) == "EMP")
            {
                int cnt = avs5029.getBnkCnt(TxtCustno.Text, Session["BRCD"].ToString());
                if (cnt == grdInsert.Rows.Count)
                {
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "")
                            result = avs5029.Modifybnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                    }
                }
                else
                {
                    avs5029.deletebnk(TxtCustno.Text, Session["BRCD"].ToString());
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "")
                            result = avs5029.Insertbnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                    }
                }
                RES = avs5029.Modify(ViewState["ID"].ToString(), TxtCustno.Text, TxtEmpNo.Text, TxtDivName.Text, TxtOffName.Text, TxtDOJ.Text, TxtRetireDt.Text, TxtRetPeriod.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), ddlDesig.SelectedValue.ToString(), TxtConfDt.Text, TxtDivNO.Text, TxtOffNo.Text, SAPNO: txtSAPNo.Text, AGE: txtAge.Text,MobNo: txtNominee.Text,ADHAR: txtAdhar.Text,CUSTTYPE: ddlCustType.SelectedValue,EMAILID: txtEmailId.Text,BLOODGRP: txtBloodGrp.Text,Pancard:txtPancard.Text,MemMobNo:txtmemno.Text);
                if (RES > 0)
                {
                    div_Main.Visible = false;
                    BtnAddNew.Visible = true;
                    BtnModify.Visible = false;
                    Submit.Visible = false;
                    BtnAuthorise.Visible = false;
                    BtnDelete.Visible = false;
                    Exit.Visible = false;
                    
                    bindgrid();
                    clear();
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "EmployerDetails_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
            else if (Convert.ToString(ViewState["TYPE"]) == "SUR")
            {

            }
            else
            {
                int cnt = avs5029.getBnkCnt(TxtCustno.Text, Session["BRCD"].ToString());
                if (cnt == grdInsert.Rows.Count)
                {
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "")
                            result = avs5029.Modifybnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                    }
                }
                else
                {
                    avs5029.deletebnk(TxtCustno.Text, Session["BRCD"].ToString());
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "")
                            result = avs5029.Insertbnk(Session["BRCD"].ToString(), TxtCustno.Text, srnumber, bnkname, brname, Accno, IFSC, Session["MID"].ToString());
                    }
                }
                //RES = avs5029.Modify(ViewState["ID"].ToString(), TxtCustno.Text, TxtEmpNo.Text, TxtDivName.Text, TxtOffName.Text, TxtDOJ.Text, TxtRetireDt.Text, TxtRetPeriod.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), ddlDesig.SelectedValue.ToString(), TxtConfDt.Text, TxtDivNO.Text, TxtOffNo.Text, SAPNO: txtSAPNo.Text);
                //if (RES > 0)
                //{
                //    div_Main.Visible = false;
                //    BtnAddNew.Visible = true;
                //    BtnModify.Visible = false;
                //    Submit.Visible = false;
                //    BtnAuthorise.Visible = false;
                //    BtnDelete.Visible = false;
                //    Exit.Visible = false;
                //    bindgrid();
                //    clear();
                //    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                //    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "EmployerDetails_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                //    return;
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["TYPE"]) == "EMP")
            {
                string STAGE = avs5029.GetStage(ViewState["ID"].ToString());
                if (STAGE == "1003")
                {
                    WebMsgBox.Show("Record is already authorised..!! Cannot be delete...!!", this.Page);
                    div_Main.Visible = false;
                    BtnAddNew.Visible = true;
                    BtnModify.Visible = false;
                    Submit.Visible = false;
                    BtnAuthorise.Visible = false;
                    BtnDelete.Visible = false;
                    Exit.Visible = false;
                    clear();
                    return;
                }
                else
                {

                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "" && IFSC != "")
                            result = avs5029.Deletebnk(TxtCustno.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    RES = avs5029.Delete(ViewState["ID"].ToString(), TxtCustno.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
                    if (RES > 0)
                    {
                        div_Main.Visible = false;
                        BtnAddNew.Visible = true;
                        BtnModify.Visible = false;
                        Submit.Visible = false;
                        BtnAuthorise.Visible = false;
                        BtnDelete.Visible = false;
                        Exit.Visible = false;
                        bindgrid();
                        clear();
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "EmployerDetails_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else if (Convert.ToString(ViewState["TYPE"]) == "SUR")
            {
                if (ddlOption.SelectedValue == "2")
                {
                    DT = avs5029.GetSuretyDetails(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtCustno.Text, ViewState["MemberNo"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        sResult = avs5029.CheckSurety(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtCustno.Text, ViewState["MemberNo"].ToString(), "2");
                        if (Convert.ToDouble(sResult) <= 0)
                        {
                            result = avs5029.StopSurety(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtCustno.Text, ViewState["MemberNo"].ToString(), Session["MID"].ToString());
                            if (result > 0)
                            {
                                WebMsgBox.Show("sucessfully surety stop ..!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Already surety stoped ..!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("No recorts exists to surety stoped ..!!", this.Page);
                        return;
                    }
                }
                else if (ddlOption.SelectedValue == "1")
                {
                    WebMsgBox.Show("Only surety stop here ..!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["TYPE"]) == "EMP")
            {
                string STAGE = avs5029.GetStage(ViewState["ID"].ToString());
                if (STAGE == "1003")
                {
                    div_Main.Visible = false;
                    BtnAddNew.Visible = true;
                    BtnModify.Visible = false;
                    Submit.Visible = false;
                    BtnAuthorise.Visible = false;
                    BtnDelete.Visible = false;
                    Exit.Visible = false;
                    clear();
                    WebMsgBox.Show("Record is already authorised..!!", this.Page);
                    return;
                }
                else
                {
                    string mid = avs5029.GetMid(ViewState["ID"].ToString());
                    if (mid == Session["MID"].ToString())
                    {
                        WebMsgBox.Show("Same user cannot authorise record..!!", this.Page);
                        clear();
                        return;
                    }
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string srnumber = ((TextBox)gvRow.FindControl("txtSrNo")).Text;
                        string bnkname = ((TextBox)gvRow.FindControl("TxtBnkName")).Text;
                        string brname = ((TextBox)gvRow.FindControl("TxtBrnchName")).Text;
                        string Accno = ((TextBox)gvRow.FindControl("TxtAccNo")).Text;
                        string IFSC = ((TextBox)gvRow.FindControl("TxtIFSC")).Text;
                        if (bnkname != "" && brname != "" && Accno != "")
                            result = avs5029.Authorisebnk(TxtCustno.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
                    }

                    RES = avs5029.Authorise(ViewState["ID"].ToString(), TxtCustno.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
                    if (RES > 0)
                    {
                        div_Main.Visible = false;
                        BtnAddNew.Visible = true;
                        BtnModify.Visible = false;
                        Submit.Visible = false;
                        BtnAuthorise.Visible = false;
                        BtnDelete.Visible = false;
                        Exit.Visible = false;
                        clear();

                        WebMsgBox.Show("Data Authorised successfully..!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "EmployerDetails_Autho _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else if (Convert.ToString(ViewState["TYPE"]) == "SUR")
            {

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void clear()
    {
        Submit.Text = "Submit";
        TxtCustno.Text = "";
        TxtCustname.Text = "";
        ddlDesig.SelectedValue = "0";
        TxtEmpNo.Text = "";
        TxtDivNO.Text = "";
        TxtDivName.Text = "";
        TxtOffNo.Text = "";
        TxtOffName.Text = "";
        TxtRetireDt.Text = "";
        TxtRetPeriod.Text = "";
        TxtDOJ.Text = "";
        TxtConfDt.Text = "";

        BtnAddNew.Enabled = true;
        BtnAddNew.Visible = true;
        div_Main.Visible = false;
        divSuretyStart.Visible = false;

        BtnModify.Visible = false;
        Submit.Visible = false;
        BtnAuthorise.Visible = false;
        BtnDelete.Visible = false;
        Exit.Visible = false;

        GrdEmpDetails.DataSource = null;
        GrdEmpDetails.DataBind();

        GrdFromSurity.DataSource = null;
        GrdFromSurity.DataBind();
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            EmptyGridBind();

            ViewState["TYPE"] = "EMP";
            div_Main.Visible = true;
            divSuretyStart.Visible = false;

            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            ddlCustType.Enabled = false;
            BtnAddNew.Enabled = true;
            BtnAddNew.Visible = false;
            BtnModify.Visible = true;
            Submit.Visible = false;
            BtnAuthorise.Visible = false;
            BtnDelete.Visible = false;
            Exit.Visible = true;
            ViewDetails(ViewState["ID"].ToString(), Session["BRCD"].ToString());
            div_Main.Visible = true;
            lblActivity.Text = "Modify Customer";
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
            ViewState["TYPE"] = "EMP";
            div_Main.Visible = true;
            divSuretyStart.Visible = false;

            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            BtnAddNew.Visible = false;
            BtnModify.Visible = false;
            Submit.Visible = false;
            BtnAuthorise.Visible = false;
            BtnDelete.Visible = true;
            Exit.Visible = true;
            ViewDetails(ViewState["ID"].ToString(), Session["BRCD"].ToString());
            div_Main.Visible = true;
            lblActivity.Text = "Delete Customer";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["TYPE"] = "EMP";
            div_Main.Visible = true;
            divSuretyStart.Visible = false;

            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            BtnAddNew.Visible = false;
            BtnModify.Visible = false;
            Submit.Visible = false;
            BtnAuthorise.Visible = true;
            BtnDelete.Visible = false;
            Exit.Visible = true;
            ViewDetails(ViewState["ID"].ToString(), Session["BRCD"].ToString());
            div_Main.Visible = true;
            lblActivity.Text = "Authorise Customer";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string[] strnumId = objlink.CommandArgument.Split('-');
            ViewState["LoanGlCode"] = strnumId[0].ToString();
            ViewState["CustAccNo"] = strnumId[1].ToString();
            ViewState["MemberNo"] = strnumId[2].ToString();

            ViewState["TYPE"] = "SUR";
            div_Main.Visible = false;
            divSuretyStart.Visible = true;

            BtnAddNew.Visible = false;
            BtnModify.Visible = false;
            BtnAuthorise.Visible = false;

            Submit.Text = "Save";
            Submit.Visible = true;
            BtnDelete.Text = "Delete";
            BtnDelete.Visible = true;
            Clear.Visible = true;
            Exit.Visible = true;

            DT = avs5029.GetSuretyDetails(Session["BRCD"].ToString(), ViewState["LoanGlCode"].ToString(), ViewState["CustAccNo"].ToString(), TxtEmpNo.Text, ViewState["MemberNo"].ToString());
            if (DT.Rows.Count > 0)
            {
                ddlOption.SelectedValue = Convert.ToString(DT.Rows[0]["RecType"]);
                txtPAmt.Text = Convert.ToString(DT.Rows[0]["Principle"]);
                txtIntAmt.Text = Convert.ToString(DT.Rows[0]["Interest"]);
            }
            else
            {
                ddlOption.SelectedValue = "2";
                txtPAmt.Text = "";
                txtIntAmt.Text = "";
            }

            ddlOption.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void bindgrid()
    {
        try
        {
            string Flag = "";
            divSurety.Visible = true;

            avs5029.Bind(GrdEmpDetails, TxtCustno.Text, Session["BRCD"].ToString());
                CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), TxtCustno.Text, Session["EntryDate"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void ViewDetails(string ID, string BRCD)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = avs5029.GetDetails(ID);
            if (dt1.Rows.Count > 0)
            {
                TxtCustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                string custname = accop.GetcustnameYN(accop.GetCENTCUST(), TxtCustno.Text, Session["BRCD"].ToString());
                TxtCustname.Text = custname.Split('_')[0].ToString();
                TxtEmpNo.Text = dt1.Rows[0]["EMPNO"].ToString();
                TxtDivNO.Text = dt1.Rows[0]["DIVNO"].ToString();
                TxtDivName.Text = avs5029.GetDivName(TxtDivNO.Text);
                TxtOffNo.Text = dt1.Rows[0]["OFFNO"].ToString();
                TxtOffName.Text = avs5029.GetOffcName(TxtDivNO.Text, TxtOffNo.Text);
                TxtDOJ.Text = dt1.Rows[0]["DOB"].ToString();
                txtPancard.Text = dt1.Rows[0]["Pancard"].ToString();
                txtmemno.Text = dt1.Rows[0]["Mobile"].ToString();
                TxtConfDt.Text = dt1.Rows[0]["OPENINGDATE"].ToString();
                TxtRetPeriod.Text = dt1.Rows[0]["RTGAGE"].ToString();
                TxtRetireDt.Text = dt1.Rows[0]["DOR"].ToString();
                txtAge.Text = dt1.Rows[0]["CUSTAGE"].ToString();  
                txtEmailId.Text = dt1.Rows[0]["EMAILID"].ToString();
                txtBloodGrp.Text=dt1.Rows[0]["BLOODGROUP"].ToString();
                txtAdhar.Text = dt1.Rows[0]["Adharcard"].ToString();
                ddlCustType.SelectedValue = string.IsNullOrEmpty(dt1.Rows[0]["CUSTTYPE"].ToString()) ? "0" : dt1.Rows[0]["CUSTTYPE"].ToString();
                txtNominee.Text = dt1.Rows[0]["MobNo"].ToString();
                 if(dt1.Rows[0]["DESIGNATION"] != null)
                {
                    ddlDesig.SelectedValue = Convert.ToInt32(dt1.Rows[0]["DESIGNATION"].ToString()).ToString();}
                txtSAPNo.Text = dt1.Rows[0]["SAPNO"].ToString();
            }

            dt2 = avs5029.GetDetailsBnk(TxtCustno.Text.ToString(), BRCD);
            if (dt2.Rows.Count > 0)
            {
                for (int i = dt2.Rows.Count; i < 2; i++)
                    dt2.Rows.Add("");

                grdInsert.DataSource = dt2;
                grdInsert.DataBind();
            }
            else
            {
                for (int i = dt2.Rows.Count; i < 2; i++)
                    dt2.Rows.Add("");
                grdInsert.DataSource = dt2;
                grdInsert.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("bankname", typeof(string));
            dtnew.Columns.Add("branchname", typeof(string));
            dtnew.Columns.Add("Accno", typeof(string));
            dtnew.Columns.Add("IFSCCode", typeof(string));
            foreach (GridViewRow row in grdInsert.Rows)
            {
                string TxtBnkName = ((TextBox)row.FindControl("TxtBnkName")).Text;
                string TxtBrnchName = ((TextBox)row.FindControl("TxtBrnchName")).Text;
                string TxtAccNo = ((TextBox)row.FindControl("TxtAccNo")).Text;
                string TxtIFSC = ((TextBox)row.FindControl("TxtIFSC")).Text;
                dtnew.Rows.Add(TxtBnkName, TxtBrnchName, TxtAccNo, TxtIFSC);
            }

            if (dtnew.Rows.Count > 0)
            {
                int cnt = dtnew.Rows.Count;
                for (int i = dtnew.Rows.Count; i <= cnt; i++)
                {
                    dtnew.Rows.Add("");
                }
                grdInsert.DataSource = dtnew;
                grdInsert.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtAddDesig.Text != "")
            {
                string sql;
                sql = "INSERT INTO LOOKUPFORM1 VALUES(1101,'Designation','" + TxtAddDesig.Text + "',(SELECT isnull(max(SRNO),0)+1 FROM LOOKUPFORM1 WHERE LNO=1101),'" + TxtAddDesig.Text + "',0)";
                conn.sExecuteQuery(sql);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Designation Added to the list....!');", true);

            }
            else
            {
                WebMsgBox.Show("Please Enter Designation name", this.Page);
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("location.reload();");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnAdDesig_Click(object sender, EventArgs e)
    {
        try
        {
            string Modal_Flag = "ADDESIGNATION";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#" + Modal_Flag + "').modal('show');");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void EmptyGridBind()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("bankname", typeof(string)),new DataColumn("branchname", typeof(int)),
                            new DataColumn("Accno", typeof(string)),new DataColumn("IFSCCode", typeof(int))});

        for (int i = dt.Rows.Count; i < 2; i++)
        {
            dt.Rows.Add("");
        }
        dt.AcceptChanges();
        grdInsert.DataSource = dt;
        grdInsert.DataBind();
    }

    protected void TxtDivNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtDivName.Text = avs5029.GetDivName(TxtDivNO.Text);
            TxtOffNo.Focus();
            autooffcname.ContextKey = TxtDivNO.Text;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtDivName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtDivName.Text = TxtDivName.Text.Split('_')[0].ToString();
            TxtDivNO.Text = TxtDivName.Text.Split('_')[1].ToString();
            autooffcname.ContextKey = TxtDivNO.Text;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtOffNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtOffName.Text = avs5029.GetOffcName(TxtDivNO.Text, TxtOffNo.Text);
            ddlDesig.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtOffName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtOffName.Text = TxtOffName.Text.Split('_')[0].ToString();
            TxtOffNo.Text = TxtOffName.Text.Split('_')[1].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnAddNew_1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmOtherRecovery.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkView11_Click(object sender, EventArgs e)
    {
        try
        {
            EmptyGridBind();

            ViewState["TYPE"] = "VE";
            div_Main.Visible = true;
            divSuretyStart.Visible = false;

            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            BtnAddNew.Enabled = true;
            BtnAddNew.Visible = false;
            BtnModify.Visible = false;
            Submit.Visible = false;
            BtnAuthorise.Visible = false;
            BtnDelete.Visible = false;
            Exit.Visible = true;
            ViewDetails(ViewState["ID"].ToString(), Session["BRCD"].ToString());
            div_Main.Visible = true;
            lblActivity.Text = "View Customer";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlCustType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}