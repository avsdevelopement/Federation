
using System.Data;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

public partial class FrmAVS51187 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    int Res = 0;
    TextBox tb;
    static int i = 0;
    string sroname = "", AC_Status = "", stage = "", usrgrp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
              ViewState["FLAG"] = "AD";
              if (ViewState["FLAG"].ToString() == "AD")
              {
                  txtDate.Text = Session["EntryDate"].ToString();
                  BD.BindAccDemandD(DdlMODE: ddlstatus);
              }
        }

    }
   
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void tctCaseNo_TextChanged(object sender, EventArgs e)
    {
        txtstage.Text = SRO.GetCaseStatusStage(txtCaseY.Text, tctCaseNo.Text);
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        stage = SRO.GETSTAGEM( tctCaseNo.Text, txtCaseY.Text);
        if (ViewState["FLAG"].ToString() == "AD")
        {
            result = SRO.InsertCaseStatus(CASENO: tctCaseNo.Text, CASE_YEAR: txtCaseY.Text, CASE_STAGE: txtstage.Text, REMARK: txtRemark.Text, DATE:txtDate.Text,PAY_AMT:Convert.ToString( txtAwardAmt.Text).ToString(), PAY_DATE: txtamtdate.Text, CASE_STATUS: ddlstatus.SelectedValue, BANK_ATTCH_DATE: TxtSAODT.Text, IMM_ATTCH_DATE: txtImmuvableDate.Text, MOV_ATTCH_DATE: txtMovable.Text, MID: Session["MID"].ToString(), BRCD: Session["BRCD"].ToString(), STAGE: "1001");
          if (result > 0)
            {
               
                WebMsgBox.Show("Data Saved successfully", this.Page);

            }
        }
        else
             if (ViewState["FLAG"].ToString() == "MD")
        {
            result = SRO.ModifyCaseStatus(CASENO: tctCaseNo.Text, CASE_YEAR: txtCaseY.Text, CASE_STAGE: txtstage.Text, REMARK: txtRemark.Text, DATE: txtDate.Text, PAY_AMT: txtAwardAmt.Text, PAY_DATE: txtamtdate.Text, CASE_STATUS: ddlstatus.SelectedValue.ToString(), BANK_ATTCH_DATE: TxtSAODT.Text, IMM_ATTCH_DATE: txtImmuvableDate.Text, MOV_ATTCH_DATE: txtMovable.Text, MID: Session["MID"].ToString(), BRCD: Session["BRCD"].ToString(), STAGE: "1002");
              if (result > 0)
            {
               
                WebMsgBox.Show("Data Saved successfully", this.Page);

            }
        }
           else if (ViewState["FLAG"].ToString() == "AT")
            {
                if (stage != "1003" && stage != "1004")
                {
                    result = SRO.AuthoriseCaseStatus(BRCD:Session["BRCD"].ToString(), CASENO: tctCaseNo.Text, CASE_YEAR: txtCaseY.Text, MID: Session["MID"].ToString());
            
                    if (result >= 1)
                    {
                        WebMsgBox.Show("Data Authorised successfully..!!", this.Page);

                       
                    }
                    else
                    {

                        WebMsgBox.Show("Warning: User is restricted to perform this operation..........!!", this.Page);

                    }
                }
            }
             else if (ViewState["FLAG"].ToString() == "CA")
             {
                 if (stage != "1004")
                 {
                     result = SRO.CancleCaseStatus(BRCD: Session["BRCD"].ToString(), CASENO: tctCaseNo.Text, CASE_YEAR: txtCaseY.Text, MID: Session["MID"].ToString());

                     if (result > 0)
                     {
                               WebMsgBox.Show("Data Canceled successfully..!!", this.Page);                     
                     }
                 }
                 else
                 {

                     WebMsgBox.Show("Warning:Data Already  Canceled..!!", this.Page);

                 }

             }
    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');

            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            tctCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();


            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(Session["MID"].ToString(), Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already Deleted..!!", this.Page);

                        return;
                    }

                }

            }

            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
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
            ViewState["FLAG"] = "CA";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
           
            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            tctCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
          
       
            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(Session["MID"].ToString(), Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already Deleted..!!", this.Page);
                       
                        return;
                    }

                }

            }
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Delete";
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
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
            ViewState["FLAG"] = "CA";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');

            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            tctCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();


            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(Session["MID"].ToString(), Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already Deleted..!!", this.Page);

                        return;
                    }

                }

            }
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Authorize";
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
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
            ViewState["FLAG"] = "CA";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');

            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            tctCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();


            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(Session["MID"].ToString(), Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already Deleted..!!", this.Page);

                        return;
                    }

                }

            }
            BtnSubmit.Visible = false;
            BtnSubmit.Text = "View";
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }

    public void BindGrd()
    {
        try
        {
            result = SRO.BindGrdCaseStatus(GrdDemand, tctCaseNo.Text, txtCaseY.Text);
           
            if (result > 0)
            {
               
                if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void ViewDetails(string CASENO, string CASE_YEAR)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsCaseStatus( CASENO, CASE_YEAR);
        
         
            if (DT.Rows.Count > 0)
            {
                txtstage.Text = DT.Rows[0]["CASE_STAGE"].ToString();



                txtDate.Text = DT.Rows[0]["[DATE]"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["[DATE]"].ToString();
                ddlstatus.SelectedValue = DT.Rows[0]["CASE_STATUS"].ToString();

                txtRemark.Text = DT.Rows[0]["REMARK"].ToString();


                txtAwardAmt.Text = DT.Rows[0]["PAY_AMT"].ToString();


                txtamtdate.Text = DT.Rows[0]["PAY_DATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["PAY_DATE"].ToString();
                TxtSAODT.Text = DT.Rows[0]["BANK_ATTCH_DATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["BANK_ATTCH_DATE"].ToString();
                txtImmuvableDate.Text = DT.Rows[0]["IMM_ATTCH_DATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["IMM_ATTCH_DATE"].ToString();
                txtMovable.Text = DT.Rows[0]["MOV_ATTCH_DATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["MOV_ATTCH_DATE"].ToString();
              
            }
            else
            {
                WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}