using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class FrmAVS5032 : System.Web.UI.Page
{
    ClsLoanInstallmen LI = new ClsLoanInstallmen();
    ClsAVS5032 LP = new ClsAVS5032();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    int Result;
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
                Result=Convert.ToInt32(LP.Checkbranch(Session["LOGINCODE"].ToString()));
                if (Result > 0)
                {
                    Txtprdcode.Focus();
                    ViewState["Flag"] = "AD";
                    BtnSubmit.Text = "Submit";
                    autoglname.ContextKey = Session["BRCD"].ToString();
                    BindGrid("ED");
                    hdEntrydate.Value = Session["EntryDate"].ToString();
                }
                else
                {
                    Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
                    
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid(string Flag)
    {
        if (Flag == "ED")
        {

            int RS = LP.BindGrid(GrdDisp, Session["EntryDate"].ToString(), "", Session["BRCD"].ToString());
        }
        else if (Flag == "PA")
        {
            int RS = LP.BindGrid(GrdDisp, Session["EntryDate"].ToString(), Txtprdcode.Text, Session["BRCD"].ToString());
        }
    }
    protected void Txtprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            AC1 = LI.Getaccno(Txtprdcode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GLCODE"] = AC[0].ToString();
                if (ViewState["GLCODE"].ToString() == "3")
                {
                    Txtprdname.Text = AC[1].ToString();
                    TxtFAmt.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter only Loan Product Code....!", this.Page);
                    Txtprdcode.Text = "";
                    Txtprdcode.Focus();

                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                Txtprdcode.Text = "";
                Txtprdcode.Focus();
                return;
            }
            BindGrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = Txtprdname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtprdname.Text = custnob[0].ToString();
                Txtprdcode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LI.Getaccno(Txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();
                TxtFAmt.Focus();

            }
            else
            {
                Txtprdcode.Focus();
                return;
            }
            BindGrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["Flag"].ToString() == "AD")
            {
                if (Txtprdcode.Text != "" && TxtFAmt.Text != "" && TxtTAmt.Text != "" && TxtROI.Text != "" && TxtPeriod.Text != "" && TxtPenalInt.Text!="")
                {
                    int Data = Convert.ToInt32(LP.Checkdate(Txtprdcode.Text, TxtFAmt.Text, TxtTAmt.Text, TxtROI.Text));
                    if (Data > 0)
                    {
                        WebMsgBox.Show("Record already exists for these details..!!", this.Page);
                    }
                    else
                    {
                        int Result = LP.insert(TxtEffectDate.Text.ToString(), ViewState["GLCODE"].ToString(), Txtprdcode.Text, TxtFAmt.Text, TxtTAmt.Text, TxtROI.Text, "1001", Session["MID"].ToString(), "Insert", "1", TxtPeriod.Text, TxtPenalInt.Text);
                        if (Result > 0)
                        {
                            WebMsgBox.Show("Data saved successfully..!!", this.Page);
                           
                            BindGrid("PA");
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntratePara_Add _" + Txtprdcode.Text + "_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            ClearData();
                            return;
                        }
                        else
                        {
                            WebMsgBox.Show("Data submission failed..!!", this.Page);
                        }
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter the details..!!", this.Page);
                }
            }

            else if (ViewState["Flag"].ToString() == "MD")
            {
                int Result = LP.Modify(Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), Txtprdcode.Text, TxtFAmt.Text, TxtTAmt.Text, TxtROI.Text, Session["MID"].ToString(), "Modfy", ViewState["Id"].ToString(), Session["BRCD"].ToString(), TxtPeriod.Text, TxtPenalInt.Text);
                if (Result > 0)
                {
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                  
                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntratePara_Mod _" + Txtprdcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    return;
                }
                else
                {
                    WebMsgBox.Show("Data Modified failed..!!", this.Page);
                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                int mid = Convert.ToInt32(LP.CheckMid(ViewState["Id"].ToString()));
                if (mid == Convert.ToInt32(Session["MID"].ToString()))
                {
                    WebMsgBox.Show("User is restricted to authorise..!!", this.Page);
                }
                else
                {
                    int Result = LP.Authorise(Session["MID"].ToString(), ViewState["Id"].ToString(), "Autho", Session["BRCD"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Authorised successfully..!!", this.Page);
                    
                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntratePara_Autho _" + Txtprdcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Data Authorised failed..!!", this.Page);
                    }
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                int stage = Convert.ToInt32(LP.CheckStage(Txtprdcode.Text, TxtFAmt.Text, TxtTAmt.Text, TxtROI.Text));
                if (stage != 1003)
                {
                    int Result = LP.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete", Session["BRCD"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                      
                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntratePara_Del _" + Txtprdcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Data Deletion failed..!!", this.Page);
                    }
                }
                else
                {
                    if (Session["UGRP"].ToString() == "1" && stage == 1003)
                    {
                        int Result = LP.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete", Session["BRCD"].ToString());
                        if (Result > 0)
                        {
                            WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                         
                            BindGrid("PA");
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntratePara_Del _" + Txtprdcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            ClearData();
                            return;
                        }
                        else
                        {
                            WebMsgBox.Show("Data Deletion failed..!!", this.Page);
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("User is not admin to delete authorised data..!!", this.Page);
                    }
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["Id"] = ARR[0].ToString();
                ViewState["Flag"] = "MD";
                BtnSubmit.Text = "Modify";
                CallEdit();
                BtnNew.Visible = true;
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["Id"] = ARR[0].ToString();
                int mid = Convert.ToInt32(LP.CheckMid(ViewState["Id"].ToString()));
                if (mid == Convert.ToInt32(Session["MID"].ToString()))
                {
                    WebMsgBox.Show("User is restricted to authorise..!!", this.Page);
                }
                else
                {
                    ViewState["Flag"] = "AT";
                    BtnSubmit.Text = "Authorise";
                    CallEdit();
                    BtnNew.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
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
            try
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["Id"] = ARR[0].ToString();
                ViewState["Flag"] = "DL";
                BtnSubmit.Text = "Delete";
                CallEdit();
                BtnNew.Visible = true;

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        Txtprdcode.Text = "";
        Txtprdname.Text = "";
        TxtFAmt.Text = "";
        TxtTAmt.Text = "";
        TxtROI.Text = "";
        TxtPeriod.Text="";
        TxtPenalInt.Text = "";
    }
    public void CallEdit()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DT = LP.GetInfo(ViewState["Id"].ToString(), "Disp", Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    Txtprdcode.Text = DT.Rows[0]["Subglcode"].ToString();
                    string AC1;

                    AC1 = LI.Getaccno(Txtprdcode.Text, Session["BRCD"].ToString());
                    if (AC1 != null)
                    {
                        string[] AC = AC1.Split('_'); ;
                        Txtprdname.Text = AC[1].ToString();
                        TxtFAmt.Text = DT.Rows[0]["FromAmt"].ToString();
                        TxtTAmt.Text = DT.Rows[0]["ToAmt"].ToString();
                        TxtROI.Text = DT.Rows[0]["ROI"].ToString();
                        ViewState["GLCODE"] = DT.Rows[0]["Glcode"].ToString();
                        TxtPeriod.Text=DT.Rows[0]["Period"].ToString();
                        TxtPenalInt.Text = DT.Rows[0]["PenalInt"].ToString();
                    }
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAmt_TextChanged(object sender, EventArgs e)
    {
        TxtTAmt.Focus();
    }
    protected void TxtTAmt_TextChanged(object sender, EventArgs e)
    {
        TxtROI.Focus();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVS5032.aspx");
    }
}