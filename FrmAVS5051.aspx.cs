using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmAVS5051 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInstallmen LI = new ClsLoanInstallmen();
    ClsAVS5051 RP = new ClsAVS5051();
    DataTable DT = new DataTable();
    scustom customcs = new scustom();
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

                TxtCol.Focus();
                ViewState["Flag"] = "AD";
                BtnSubmit.Text = "Submit";
                autoglname.ContextKey = Session["BRCD"].ToString();
                autoglpost.ContextKey = Session["BRCD"].ToString();
                autoexname.ContextKey = Session["BRCD"].ToString();
                 BindGrid("ED");
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

            int RS = RP.BindGrid(GrdDisp, "", Session["BRCD"].ToString());
        }
        else if (Flag == "PA")
        {
            int RS = RP.BindGrid(GrdDisp, TxtPno.Text, Session["BRCD"].ToString());
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Txtvalue.Text = (Txtvalue.Text == "" ? "0" : Txtvalue.Text);
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (TxtPno.Text != "" && TxtPname.Text != "")
                {

                    int Result = RP.insert(TxtCol.Text, TxtPno.Text, TxtPostPrd.Text, TxtExrec.Text, TxtShort.Text, TxtMarati.Text, "1001", Session["MID"].ToString(), conn.PCNAME(), Session["BRCD"].ToString(), Txtvalue.Text, Txttype.Text, Txtrate.Text, "Insert");
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data saved successfully..!!", this.Page);

                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Add _" + TxtPno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                    WebMsgBox.Show("Enter the details..!!", this.Page);
                }
            }

            else if (ViewState["Flag"].ToString() == "MD")
            {
                int Result = RP.Modify(TxtCol.Text, TxtPno.Text, TxtPostPrd.Text, TxtExrec.Text, TxtShort.Text, TxtMarati.Text, Session["MID"].ToString(), conn.PCNAME(), Session["BRCD"].ToString(), Txtvalue.Text, Txttype.Text, Txtrate.Text, "Modify", ViewState["Id"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);

                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Mod _" + TxtPno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                int mid = Convert.ToInt32(RP.CheckMid(ViewState["Id"].ToString()));
                if (mid == Convert.ToInt32(Session["MID"].ToString()))
                {
                    WebMsgBox.Show("User is restricted to authorise..!!", this.Page);
                }
                else
                {
                    int Result = RP.Authorise(Session["MID"].ToString(), ViewState["Id"].ToString(), "Autho", Session["BRCD"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Authorised successfully..!!", this.Page);

                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Autho _" + TxtPno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                int stage = Convert.ToInt32(RP.CheckStage(ViewState["Id"].ToString()));
                if (stage != 1003)
                {
                    int Result = RP.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete", Session["BRCD"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);

                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Del _" + TxtPno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                        int Result = RP.Delete(Session["MID"].ToString(), ViewState["Id"].ToString(), "Delete", Session["BRCD"].ToString());
                        if (Result > 0)
                        {
                            WebMsgBox.Show("Data Deleted successfully..!!", this.Page);

                            BindGrid("PA");
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RecoveryPara_Del _" + TxtPno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
        TxtCol.Text = "";
        TxtPno.Text = "";
        TxtRecPname.Text = "";
        TxtPostPrd.Text = "";
        TxtPname.Text = "";
        TxtShort.Text = "";
        TxtMarati.Text = "";
        Txtvalue.Text = "";
        Txttype.Text = "";
        Txtrate.Text = "";
        TxtExrec.Text = "";
        TxtEname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtRecPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtRecPname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtRecPname.Text = custnob[0].ToString();
                TxtPno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LI.Getaccno(TxtPno.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();
                TxtPostPrd.Focus();

            }
            else
            {
                TxtPno.Focus();
                return;
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtPname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtPname.Text = custnob[0].ToString();
                TxtPostPrd.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LI.Getaccno(TxtPostPrd.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();
                TxtShort.Focus();

            }
            else
            {
                TxtPostPrd.Focus();
                return;
            }
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtEname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtEname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtEname.Text = custnob[0].ToString();
                TxtExrec.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LI.Getaccno(TxtExrec.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();
                TxtMarati.Focus();

            }
            else
            {
                TxtExrec.Focus();
                return;
            }
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPostPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtPostPrd.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            ViewState["GLCODE"] = GLCODE[1].ToString();
           string PDName = customcs.GetProductName(TxtPostPrd.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtPname.Text = PDName;
                TxtShort.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtPostPrd.Text = "";
                TxtPostPrd.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtExrec_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtExrec.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            ViewState["GLCODE"] = GLCODE[1].ToString();
           string PDName = customcs.GetProductName(TxtExrec.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtEname.Text = PDName;
                TxtMarati.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtExrec.Text = "";
                TxtExrec.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtPno.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            ViewState["GLCODE"] = GLCODE[1].ToString();
           string PDName = customcs.GetProductName(TxtPno.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtRecPname.Text = PDName;
                TxtPostPrd.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtPno.Text = "";
                TxtPno.Focus();
            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkModify_Click(object sender, EventArgs e)
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
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["Flag"] = "AT";
            BtnSubmit.Text = "Authorise";
            CallEdit();


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
    public void CallEdit()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DT = RP.GetInfo(ViewState["Id"].ToString(), "Disp", Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    TxtPno.Text = DT.Rows[0]["REC_PRD"].ToString();
                    string AC1;
                     AC1 = LI.Getaccno(TxtPno.Text, Session["BRCD"].ToString());
                    if (AC1 != null)
                    {
                        string[] AC = AC1.Split('_'); ;
                        TxtRecPname.Text = AC[1].ToString();
                    }
                    TxtPostPrd.Text = DT.Rows[0]["POST_PRD"].ToString();
                    string AC2;
                    AC2 = LI.Getaccno(TxtPostPrd.Text, Session["BRCD"].ToString());
                    if (AC2 != null)
                    {
                        string[] ACp = AC2.Split('_'); ;
                        TxtPname.Text = ACp[1].ToString();
                    }
                    string AC3;
                    TxtExrec.Text = DT.Rows[0]["EXRECCODE"].ToString();
                    AC3 = LI.Getaccno(TxtExrec.Text, Session["BRCD"].ToString());
                    if (AC3 != null)
                    {
                        string[] ACe = AC3.Split('_'); ;
                        TxtEname.Text = ACe[1].ToString();
                    }
                    TxtCol.Text = DT.Rows[0]["COLUMNNO"].ToString();
                    TxtShort.Text = DT.Rows[0]["SHORTNAME"].ToString();
                    TxtMarati.Text = DT.Rows[0]["SHORTMARATHI"].ToString();
                    Txtvalue.Text = DT.Rows[0]["VALUE"].ToString();
                    Txttype.Text = DT.Rows[0]["TYPE"].ToString();
                    Txtrate.Text = DT.Rows[0]["RATE"].ToString();
                  
                    
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
}