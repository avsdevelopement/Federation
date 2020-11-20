using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class FrmAVS51186 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    ClsAVS51186 MOV = new ClsAVS51186();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "",MEM="";
    int result = 0;
    string sroname = "", AC_Status = "", results = "";
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
                lblActivity.Text = "Add File";
            }

            // BD.BindAccDemandD(DdlMODE: ddlstatus);
            // BD.BindActionStatus(DdlMODE: ddlActstatus); 
          //  DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }

        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "AD";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            lblActivity.Text = "Add File";
        }
        BtnSubmit.Text = "Add";
        // BD.BindAccDemandD(DdlMODE: ddlstatus);
        // BD.BindActionStatus(DdlMODE: ddlActstatus); 
      //  DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
            txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
        }
        BindGrdMain();
        txtCaseY.Focus();

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "MD";
        BindGrdMain();
        BtnSubmit.Text = "Modify";
        
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "DEL";
        BindGrdMain();
        BtnSubmit.Text = "Delete";
    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "AT";
        BindGrdMain();
        BtnSubmit.Text = "Authorize";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (txtCaseY.Text == "")
        {
            WebMsgBox.Show("Please Enter Case Year.", this.Page);
        }
        if (txtCaseNO.Text == "")
        {
            WebMsgBox.Show("Please Enter Case No.", this.Page);
        }
        if (txtSociName.Text=="")
        {
            WebMsgBox.Show("Please Enter Society Name.", this.Page);
        }
        if (txtMovDate.Text == "")
        {
            WebMsgBox.Show("Please Enter Movement Date Year.", this.Page);
        }
        if (txtMovDetails.Text == "")
        {
            WebMsgBox.Show("Please Enter Movement Detail.", this.Page);
        }
        if (TxtSRNO.Text == "")
        {
            WebMsgBox.Show("Please Enter SRNO.", this.Page);
        }
        if (txtRecAmt.Text == "")
        {
            WebMsgBox.Show("Please Enter Rcovery Amount.", this.Page);
        }
        if (txtcasestatusno.Text == "")
        {
            WebMsgBox.Show("Please Enter Case Status.", this.Page);
        }
        if (txtactionno.Text == "")
        {
            WebMsgBox.Show("Please Enter Action Status.", this.Page);
        }

        
         if (ViewState["FLAG"].ToString() == "AD")
         {
        result =MOV.InsertMovemetDetail(  BRCD:Session["BRCD"].ToString(),  CASENO:txtCaseNO.Text,  CASEYAER:txtCaseY.Text,  MEMNO:MEM,  SRO_NO:TxtSRNO.Text,  ENTRYDATE:Session["EntryDate"].ToString(), SOCIETYNAME:txtSociName.Text,
            Movementdate: txtMovDate.Text, MovementdDet: txtMovDetails.Text, RECAMT: txtRecAmt.Text, CASESTATUSE: txtcasestatusno.Text, ACTIONSTATUS: txtactionno.Text, MID: Session["MID"].ToString());
        if (result > 0)
        {
            // result = InsertDefaulterName();

            WebMsgBox.Show("Data Saved successfully", this.Page);

            clear();
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNO.Text + "" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
           
        }
        }
         else
              if (ViewState["FLAG"].ToString() == "MD")
              {
                    result =MOV.ModifyMovemetDetail(  BRCD:Session["BRCD"].ToString(),  CASENO:txtCaseNO.Text,  CASEYAER:txtCaseY.Text,  MEMNO:MEM,  SRO_NO:TxtSRNO.Text,  ENTRYDATE:Session["EntryDate"].ToString(), SOCIETYNAME:txtSociName.Text,
            Movementdate: txtMovDate.Text, MovementdDet: txtMovDetails.Text, RECAMT: txtRecAmt.Text, CASESTATUSE: txtcasestatusno.Text, ACTIONSTATUS: txtactionno.Text, MID: Session["MID"].ToString());
             if (result > 0)
               {
            // result = InsertDefaulterName();

            WebMsgBox.Show("Data Modify successfully", this.Page);
            clear();
           
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNO.Text + "" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
           
               }
             }

        else
              if (ViewState["FLAG"].ToString() == "AT")
              {
                  result = MOV.AuthoriseMovemetDetail(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, VID: Session["MID"].ToString());
             if (result > 0)
               {
            // result = InsertDefaulterName();
                  
            WebMsgBox.Show("Data authorize successfully", this.Page);

            clear();
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNO.Text + "" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
           
               }
             }
         if (ViewState["FLAG"].ToString() == "DEL")
              {
                  result = MOV.deleteMovemetDetail(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text,  VID: Session["MID"].ToString());
             if (result > 0)
               {
            // result = InsertDefaulterName();

            WebMsgBox.Show("Data Modify successfully", this.Page);
            clear();
           
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNO.Text + "" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
           
               }
             }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {
        txtSociName.Text = MOV.GetMemberID(txtCaseNO.Text,txtCaseY.Text,Session["BRCD"].ToString());
        txtMovDate.Focus();

    }
    //protected void txtMember_TextChanged(object sender, EventArgs e)
    //{
    //    txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
    //}
    //protected void txtMemberName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string custno = txtMemberName.Text;
    //        string[] CT = custno.Split('_');
    //        if (CT.Length > 0)
    //        {
    //            txtMemberName.Text = CT[0].ToString();
    //            txtMember.Text = CT[1].ToString();


    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void txtcasestatusno_TextChanged(object sender, EventArgs e)
    {
        sroname = SRO.GetCaseStatus(txtcasestatusno.Text);
        txtcasestatusname.Text = sroname;
        txtactionno.Focus();
    }

    protected void txtactionno_TextChanged(object sender, EventArgs e)
    {
        sroname = SRO.GetActionStatus(txtactionno.Text);
        txtactionname.Text = sroname;
        BtnSubmit.Focus();

    }
    protected void TxtSRNO_TextChanged(object sender, EventArgs e)
    {
        try
        {

            sroname = SRO.GetSROName(TxtSRNO.Text);
            TXTSROName.Text = sroname;
            txtMovDetails.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdMovement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdMovement.PageIndex = e.NewPageIndex;
            BindGrdMain();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrdMain()
    {
        try
        {
            SRO.BindGrdMovement(GrdMovement,Session["BRCD"].ToString());

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
           
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
           string STR = ViewState["BRCD"].ToString();
            txtCaseNO.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
           
            ViewDetails(STR, ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
           // lblActivity.Text = "View Demand";
            // BindGrdMain();
          //  BindGrd();
           // ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        TxtSRNO.Text = "";
        TXTSROName.Text = "";
        txtCaseNO.Text = "";
        txtCaseY.Text = "";
        txtSociName.Text = "";
        txtMovDate.Text = "";
        txtMovDetails.Text = "";
        txtRecAmt.Text = "";
        txtcasestatusno.Text = "";

        txtcasestatusname.Text = "";
        txtactionno.Text = "";

        txtactionname.Text = "";
    }

    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = MOV.ViewDetailsMOV(BRCD, CASENO, CASE_YEAR);


            if (DT.Rows.Count > 0)
            {

                 TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
              txtCaseNO.Text=DT.Rows[0]["Caseno"].ToString();
              txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
              txtSociName.Text = DT.Rows[0]["SOCIETYNAME"].ToString();
              txtMovDate.Text = DT.Rows[0]["MOVEMENTDATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["MOVEMENTDATE"].ToString();
              txtMovDetails.Text = DT.Rows[0]["MOVEMENTDETAIL"].ToString();
              txtRecAmt.Text = DT.Rows[0]["Caseno"].ToString();
              txtcasestatusno.Text = DT.Rows[0]["CASESTATUSNO"].ToString();
              sroname = SRO.GetCaseStatus(txtcasestatusno.Text);
              txtcasestatusname.Text = sroname;
              txtactionno.Text = DT.Rows[0]["ACTIONSTATUSNO"].ToString();
              sroname = SRO.GetActionStatus(txtactionno.Text);
              txtactionname.Text = sroname;
               
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