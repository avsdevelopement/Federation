using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmAVS5052 : System.Web.UI.Page
{
    ClsAVS5052 LM = new ClsAVS5052();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    int Result;
    DbConnection conn = new DbConnection();
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

                TxtLoanSan.Focus();
                ViewState["Flag"] = "AD";
                BtnSubmit.Text = "Submit";
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {


        int RS = LM.BindGrid(GrdDisp, Session["BRCD"].ToString());


    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {

                if (TxtLoanSan.Text != "")
                {

                    int Result = LM.insert(TxtLoanSan.Text, Txtdaily.Text, Txtmonthly.Text, Txtlocking.Text, Txtshares.Text, Txtperiod.Text, Txtprinciple.Text, Txtint.Text, Txttotal.Text, TxtReq_Principle.Text, Txtdiff.Text, Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), conn.PCNAME(), "Insert");
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data saved successfully..!!", this.Page);

                        BindGrid();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lockingmaster_add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Data submission failed..!!", this.Page);
                    }
                }
                else
                {
                    WebMsgBox.Show("Please enter the details..!!", this.Page);
                }

            }


            else if (ViewState["Flag"].ToString() == "MD")
            {
                int Result = LM.Modify(TxtLoanSan.Text, Txtdaily.Text, Txtmonthly.Text, Txtlocking.Text, Txtshares.Text, Txtperiod.Text, Txtprinciple.Text, Txtint.Text, Txttotal.Text, TxtReq_Principle.Text, Txtdiff.Text, "Modify", ViewState["Id"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);

                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lockingmaster_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    return;
                }
                else
                {
                    WebMsgBox.Show("Data Modified failed..!!", this.Page);
                }
            }

            else if (ViewState["Flag"].ToString() == "DL")
            {
                int stage = Convert.ToInt32(LM.CheckStage(ViewState["Id"].ToString()));
                if (stage != 1003)
                {
                    int Result = LM.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete");
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);

                        BindGrid();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lockingmaster_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                        int Result = LM.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete");
                        if (Result > 0)
                        {
                            WebMsgBox.Show("Data Deleted successfully..!!", this.Page);

                            BindGrid();
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
    public void ClearData()
    {
        TxtLoanSan.Text = "";
        Txtdaily.Text = "";
        Txtmonthly.Text = "";
        Txtlocking.Text = "";
        Txtshares.Text = "";
        Txtperiod.Text = "";
        Txtprinciple.Text = "";
        Txtint.Text = "";
        Txttotal.Text = "";
        TxtReq_Principle.Text = "";
        Txtdiff.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }



    public void CallEdit()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {

                DT = LM.GetInfo(ViewState["Id"].ToString(), "Disp");
                if (DT.Rows.Count > 0)
                {
                    TxtLoanSan.Text = DT.Rows[0]["LoanSanction"].ToString();

                    Txtdaily.Text = DT.Rows[0]["Daily"].ToString();
                    Txtmonthly.Text = DT.Rows[0]["Monthly"].ToString();
                    Txtlocking.Text = DT.Rows[0]["Locking"].ToString();
                    Txtshares.Text = DT.Rows[0]["Shares"].ToString();
                    Txtperiod.Text = DT.Rows[0]["Period"].ToString();
                    Txtprinciple.Text = DT.Rows[0]["Principal"].ToString();
                    Txtint.Text = DT.Rows[0]["Int"].ToString();
                    Txttotal.Text = DT.Rows[0]["Total"].ToString();
                    TxtReq_Principle.Text = DT.Rows[0]["Req_Principal"].ToString();
                    Txtdiff.Text = DT.Rows[0]["Diff"].ToString();

                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Lnkmodify_Click(object sender, EventArgs e)
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

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDelete_Click1(object sender, EventArgs e)
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

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdaily_TextChanged(object sender, EventArgs e)
    {
        Txtmonthly.Focus();
    }
    protected void Txtmonthly_TextChanged(object sender, EventArgs e)
    {
        Txtlocking.Focus();
    }
    protected void Txtlocking_TextChanged(object sender, EventArgs e)
    {
        Txtshares.Focus();
    }
    protected void Txtshares_TextChanged(object sender, EventArgs e)
    {
        Txtperiod.Focus();
    }
    protected void Txtperiod_TextChanged(object sender, EventArgs e)
    {
        Txtprinciple.Focus();
    }
    protected void Txtprinciple_TextChanged(object sender, EventArgs e)
    {
        Txtint.Focus();
    }
    protected void Txtint_TextChanged(object sender, EventArgs e)
    {
        Txttotal.Focus();
    }
    protected void Txttotal_TextChanged(object sender, EventArgs e)
    {
        TxtReq_Principle.Focus();
    }
    protected void TxtReq_Principle_TextChanged(object sender, EventArgs e)
    {
        Txtdiff.Focus();
    }
    protected void Txtdiff_TextChanged(object sender, EventArgs e)
    {
        BtnSubmit.Focus();
    }
}