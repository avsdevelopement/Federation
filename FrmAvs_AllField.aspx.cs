using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class FrmAvs_AllField : System.Web.UI.Page
{
    ClsAccopen accop = new ClsAccopen();
    ClsAuthoriseCommon CMN = new ClsAuthoriseCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAvs_AllField AF = new ClsAvs_AllField();
    DataTable dt = new DataTable();
    string FL = "", FL1 = "";
    int Result = 0;
    int Res = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string AID, PostingDate, FundingDate, PARTICULARS2, AMOUNT_1, AMOUNT_2, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, RTIME, MID, CID, VID, PCMAC, PAYMAST, RefBrcd, 
         RecPrint, OrgBrCd, ResBrCd, RefId, TokenNo, Ref_Agent, stage, Custname, Entrydate, SubGlCode, HeadDesc, Stage, MID_EntryDate, OldGL, VID_EntryDate, Scrollno, FLS;

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
                else if ((Session["UGRP"].ToString() != "77"))
                {
                    HttpContext.Current.Response.Redirect("FrmBlank.aspx", false);
                }
                else
                {
                    txtbrcd.Text = Session["BRCD"].ToString();
                    ViewState["Flag"] = Request.QueryString["Flag"];
                    txtdate.Text = Session["EntryDate"].ToString();
                    txtdate.Focus();
                    BD.BindPmtMode(DdlPmtMode);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtpcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = accop.Getaccno(txtpcode.Text, Session["BRCD"].ToString(), "");
            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                txtaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtpcode.Focus();
                txtpcode.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void txtpname_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //       // string CUNAME = txtpname.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            //txtpname.Text = custnob[0].ToString();
    //            txtpcode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //            string[] AC = accop.Getaccno(txtpcode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
    //            ViewState["ACCNO"] = AC[0].ToString();
    //            ViewState["GLCODE"] = AC[1].ToString();
    //           // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtpcode.Text;
    //         }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    
    protected void txtcstno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (txtcstno.Text == "")
            //{
            //    return;
            //}
            //string custname = accop.Getcustname(txtcstno.Text, Session["BRCD"].ToString());
            //string[] name = custname.Split('_');
            //txtInstNo.Focus();
            
            //WebMsgBox.Show("Customer not found", this.Page);
            //txtcstno.Text = "";
            //txtcstno.Focus();
            //return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void txtname_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //         // string CUNAME = txtname.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //           // txtname.Text = custnob[0].ToString();
    //            txtcstno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //          }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string custname;
            //string[] name;
            ////custname = accop.Getcustname(txtcstno.Text, Session["BRCD"].ToString());
            ////name = custname.Split('_');
            //string GL = BD.GetAccTypeGL(txtpcode.Text, Session["BRCD"].ToString());
            //string[] GLCODE = GL.Split('_');

            //ViewState["Gl"] = GLCODE[1].ToString();
            //AF.BindDetails(Grdcustdisp, "Details", ViewState["Gl"].ToString(), Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void txtAccName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //       // string CUNAME = txtAccName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            ViewState["CustNo"] = custnob[2].ToString();
    //            //txtAccName.Text = custnob[0].ToString();
    //            txtaccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //          }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    protected void TxtGlcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtGlcode.Text = Convert.ToInt32(AF.dispglcode(txtbrcd.Text, txtpcode.Text)).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text != "" && txtInstNo.Text != "")
            {
                FL = "MD";
            }

            //  Move data into backup table (AVS5041) before modify here
            Result = CMN.MoveVoucher(Session["BRCD"].ToString(), txtdate.Text.ToString(), txtInstNo.Text.ToString(), Session["MID"].ToString());
            Result = AF.Modify(FL, txtdate.Text.ToString(), txtInstNo.Text.ToString(), Session["BRCD"].ToString(), txtaccno.Text.ToString(), txtpcode.Text.ToString(), txtcstno.Text.ToString(), txtparticular.Text.ToString(), txtactivity.Text.ToString(), DdlPmtMode.SelectedItem.ToString(), txtscroll.Text.ToString(), txtcredit.Text.ToString(), txtdebit.Text.ToString(), ViewState["ScrollNo"].ToString());
            if (Result > 0)
            {
                FL = "Getdata";
                Bindgrid();
                Cleardata();
                WebMsgBox.Show("Updated Successfully ....!", this.Page);
                FL1 = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            dt = AF.GetInfoid(FL, Session["BRCD"].ToString(), txtdate.Text, txtInstNo.Text.ToString(), ViewState["ScrollNo"].ToString());
            AssignInsrtdata(dt);

            Result = AF.Insertdata("1", txtdate.Text.ToString(), txtdate.Text.ToString(), txtdate.Text.ToString(), TxtGlcode.Text.ToString(), txtpcode.Text.ToString(), txtaccno.Text.ToString(), txtparticular.Text.ToString(), "Save", txtcredit.Text.ToString(), "", "", txtdebit.Text.ToString(), txtactivity.Text.ToString(), DdlPmtMode.SelectedItem.Text.ToString(), txtInstNo.Text.ToString(),
                INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, stage, RTIME, txtbrcd.Text.ToString(), MID, CID, VID, PCMAC, PAYMAST, txtcstno.Text, Custname, RefBrcd, RecPrint, OrgBrCd, ResBrCd, RefId, TokenNo, Ref_Agent);

            if (TxtGlcode.Text.ToString() == "11")
            {
                if (Result > 0)
                {
                    Res = AF.InstLN(txtbrcd.Text.ToString(), txtpcode.Text.ToString(), SubGlCode, txtaccno.Text.ToString(), HeadDesc, txtdebit.Text.ToString(), txtactivity.Text.ToString(), txtparticular.Text.ToString(), txtcredit.Text.ToString(), txtInstNo.Text.ToString(), stage,
                        Scrollno, MID, MID_EntryDate, PCMAC, txtdate.Text, RefId, OldGL, VID, VID_EntryDate);
                }
            }

            if (Result > 0)
            {
                FL = "Getdata";
                Bindgrid();
                Cleardata();
                WebMsgBox.Show("Saved Successfully...!!", this.Page);
                FL1 = "Insert";
                string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _lstIntdt_update_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btnlstintdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Gl"].ToString() == "5")
            {
                Result = AF.LastIntdtDep(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text, TxtLstIntDate.Text);
            }
            else if (ViewState["Gl"].ToString() == "3")
            {
                Result = AF.LastIntdtLoan(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text, TxtLstIntDate.Text);
            }
            else
            {
                Result = AF.LastIntdt(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text, TxtLstIntDate.Text);
            }
            if (Result > 0)
            {
                WebMsgBox.Show("LastIntDate   updated  Successfully ....!", this.Page);
                FL1 = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _lstIntdt_update_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text != "" && txtaccno.Text != "" && txtpcode.Text != "" && txtInstNo.Text != "")
            {
                {
                    FL = "View";
                }
            }
            else if (txtdate.Text != "" && txtaccno.Text != "")
            {
                {
                    FL = "Select";
                }
            }
            else if (txtdate.Text != "" && txtInstNo.Text != "")
            {
                FL = "Getdata";
            }
            Bindgrid();
            string FL1 = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            ViewState["ScrollNo"] = str.ToString();
            FL = "Display";

            dt = AF.GetInfoid(FL, Session["BRCD"].ToString(), txtdate.Text, txtInstNo.Text.ToString(), ViewState["ScrollNo"].ToString());
            AssignValues(dt);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnAccsts_Click(object sender, EventArgs e)
    {
        try
        {
            if (RbdStatus.SelectedValue == "1")
            {

                Result = AF.Openstatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                if (Result > 0)
                {
                    WebMsgBox.Show("Acc_status  Opened Successfully ....!", this.Page);
                    FL1 = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Acsts_open_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                Result = AF.Closestatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                if (Result > 0)
                {
                    WebMsgBox.Show("Acc_status  Closed Successfully ....!", this.Page);
                    FL1 = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Acsts_Close_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnLmsts_Click(object sender, EventArgs e)
    {
        try
        {
            if (RbdLMstatus.SelectedValue == "1")
            {
                if (ViewState["Gl"].ToString() == "5")
                {
                    Result = AF.OpenLMstatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                }
                else if (ViewState["Gl"].ToString() == "3")
                {
                    Result = AF.OpenLLMstatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                }
                if (Result > 0)
                {
                    WebMsgBox.Show("LM_status  Opened Successfully ....!", this.Page);
                    FL1 = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _LM_open_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                if (ViewState["Gl"].ToString() == "5")
                {
                    Result = AF.CloseLMstatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                }
                else if (ViewState["Gl"].ToString() == "3")
                {
                    Result = AF.CloseLLMstatus(Session["BRCD"].ToString(), txtpcode.Text, txtaccno.Text);
                }
                if (Result > 0)
                {
                    WebMsgBox.Show("LM_status  Closed Successfully ....!", this.Page);
                    FL1 = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _LM_Close_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnACS_Click(object sender, EventArgs e)
    {
        try
        {
            Accdisplay.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnLMS_Click(object sender, EventArgs e)
    {
        try
        {
            Lmstatus.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnLPD_Click(object sender, EventArgs e)
    {
        try
        {
            LastIntdt.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    public void Cleardata()
    {
        txtbrcd.Text = "";
        TxtGlcode.Text = "";
        txtpcode.Text = "";
        txtcstno.Text = "";
        txtaccno.Text = "";
        txtcredit.Text = "";
        txtdebit.Text = "";
        txtactivity.Text = "";
        txtparticular.Text = "";
        DdlPmtMode.SelectedItem.Text = "--Select--";
        txtscroll.Text = "";
        txtbrcd.Text = Session["BRCD"].ToString();
    }

    protected void Bindgrid()
    {
        try
        {
            AF.GetInfo(grdAvs_Field, FL, txtdate.Text.ToString(), txtaccno.Text.ToString(), Session["BRCD"].ToString(), txtpcode.Text.ToString(), txtInstNo.Text.ToString());

            //Added by amol on 15/12/2017 because show voucher Cr and Dr Total
            dt = AF.VoucherTotal(Session["BRCD"].ToString(), txtdate.Text.ToString(), txtInstNo.Text.ToString());
            if (dt.Rows.Count > 0)
            {
                txtTotalDr.Text = dt.Rows[0]["TotalDr"].ToString();
                txtTotalCr.Text = dt.Rows[0]["TotalCr"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Assinln(DataTable dt)
    {
        try
        {
            SubGlCode = dt.Rows[0]["SubGlCode"].ToString();
            HeadDesc = dt.Rows[0]["HeadDesc"].ToString();
            Stage = dt.Rows[0]["Stage"].ToString();
            MID = dt.Rows[0]["MID"].ToString();
            MID_EntryDate = dt.Rows[0]["MID_EntryDate"].ToString();
            PCMAC = dt.Rows[0]["PCMAC"].ToString();
            RefId = dt.Rows[0]["RefId"].ToString();
            OldGL = dt.Rows[0]["OldGL"].ToString();
            VID = dt.Rows[0]["VID"].ToString();
            VID_EntryDate = dt.Rows[0]["VID_EntryDate"].ToString();
            Scrollno = AF.Getscrollno(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Entrydate);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void AssignValues(DataTable dt)
    {
        try
        {
            DdlPmtMode.SelectedItem.Text = dt.Rows[0]["PMTMODE"].ToString();
            txtpcode.Text = dt.Rows[0]["SUBGLCODE"].ToString();
            txtaccno.Text = dt.Rows[0]["ACCNO"].ToString();
            txtcstno.Text = dt.Rows[0]["CUSTNO"].ToString();
            txtcredit.Text = dt.Rows[0]["AMOUNT"].ToString();
            txtparticular.Text = dt.Rows[0]["PARTICULARS"].ToString();
            txtdebit.Text = dt.Rows[0]["TRXTYPE"].ToString();
            txtactivity.Text = dt.Rows[0]["ACTIVITY"].ToString();
            txtscroll.Text = dt.Rows[0]["SCROLLNO"].ToString();
            txtbrcd.Text = dt.Rows[0]["BRCD"].ToString();
            txtdate.Text = dt.Rows[0]["EDate"].ToString();
            txtInstNo.Text = dt.Rows[0]["SETNO"].ToString();
            TxtGlcode.Text = dt.Rows[0]["GLCODE"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void AssignInsrtdata(DataTable dt)
    {
        try
        {
            AID = dt.Rows[0]["AID"].ToString();
            PostingDate = dt.Rows[0]["PostingDate"].ToString();
            FundingDate = dt.Rows[0]["FundingDate"].ToString();
            PARTICULARS2 = dt.Rows[0]["PARTICULARS2"].ToString();
            AMOUNT_1 = dt.Rows[0]["AMOUNT_1"].ToString();
            AMOUNT_2 = dt.Rows[0]["AMOUNT_2"].ToString();
            INSTRUMENTNO = dt.Rows[0]["INSTRUMENTNO"].ToString();
            INSTRUMENTDATE = dt.Rows[0]["InstDate"].ToString();
            INSTBANKCD = dt.Rows[0]["INSTBANKCD"].ToString();
            INSTBRCD = dt.Rows[0]["INSTBRCD"].ToString();
            RTIME = dt.Rows[0]["RTIME"].ToString();
            MID = dt.Rows[0]["MID"].ToString();
            CID = dt.Rows[0]["CID"].ToString();
            VID = dt.Rows[0]["VID"].ToString();
            PCMAC = dt.Rows[0]["PCMAC"].ToString();
            PAYMAST = dt.Rows[0]["PAYMAST"].ToString();
            RefBrcd = dt.Rows[0]["RefBrcd"].ToString();
            RecPrint = dt.Rows[0]["RecPrint"].ToString();
            OrgBrCd = dt.Rows[0]["OrgBrCd"].ToString();
            ResBrCd = dt.Rows[0]["ResBrCd"].ToString();
            RefId = dt.Rows[0]["RefId"].ToString();
            TokenNo = dt.Rows[0]["TokenNo"].ToString();
            Ref_Agent = dt.Rows[0]["Ref_Agent"].ToString();
            stage = dt.Rows[0]["stage"].ToString();
            Entrydate = dt.Rows[0]["Entrydate"].ToString();
            Custname = AF.Getcustname(txtbrcd.Text, txtcstno.Text);
            Scrollno = AF.Getscrollno(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Entrydate);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdAvs_Field_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        grdAvs_Field.PageIndex = e.NewPageIndex;
        FL = "Select";
        Bindgrid();
    }

}
