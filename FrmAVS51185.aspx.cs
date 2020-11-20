using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51185 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
     string FL = "";
    int result = 0;
    string sroname = "", AC_Status = "", results="";

    protected void Page_Load(object sender, EventArgs e)
    {

        
       
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["brcd"].ToString();
            ViewState["FLAG"] = "AD";
            if (ViewState["FLAG"].ToString() == "AD")
            {
                lblActivity.Text = "Add File";
                BD.BindOccupation(ddlOccupation);
                txtCaseY.Focus();
                BindGrdMain();
                BD.BindWard(txtWard);
            }

        }
    }
    //protected void lnkAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ViewState["FLAG"] = "AD";
            
    //        BtnSubmit.Text = "Submit";
    //        lblActivity.Text = "Add File";

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }

    //}
   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "DLT";

            BtnSubmit.Text = "Submit";
            lblActivity.Text = "Add File";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "ATH";

            BtnSubmit.Text = "Authorise";
            lblActivity.Text = "Add File";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCaseY.Text == "")
            {
                WebMsgBox.Show("Enter Case Year.", this.Page);
                txtCaseY.Text = "";
                txtCaseY.Focus(); ;
            }
               
            else
            if (txtCaseNO.Text == "")
            {
                WebMsgBox.Show("Enter Case No.", this.Page);
                txtCaseNO.Text = "";
                txtCaseNO.Focus();
            }
            else
                if (txtMember.Text == "")
                {
                    WebMsgBox.Show("Enter Member No.", this.Page);
                    txtMember.Text = "";
                    txtMember.Focus();
                  
                }
                else
                    if (txtDefName.Text == "")
                    {
                        WebMsgBox.Show("Enter Defaulter Name.", this.Page);
                        txtDefName.Text = "";
                        txtDefName.Focus();

                    }

            if (ViewState["FLAG"].ToString() == "AD")
            {
                results = SRO.CheckCASENOEXISTS(caseno: txtCaseNO.Text, caseyear: txtCaseY.Text);
                if (results != null)
                {
                    results = SRO.CHKMEMBERNOEXISTS(caseyear: txtCaseY.Text, caseno: txtCaseNO.Text, MEMBERNO: txtMember.Text);
                    if (results != null)
                    {
                       // results = SRO.CHKMID(caseyear: txtCaseY.Text, MEMBERNO: txtMember.Text,caseno:txtCaseNO.Text);

                       // if (results == null)
                      //  {
                            result = SRO.InsertDefaulter(APPLICATIONDATE: Session["EntryDate"].ToString(), CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text, BRCD: Session["brcd"].ToString(), DEFAULTER_NAME: txtDefName.Text, MEMBERNO: txtMember.Text, DEFAULTER_PROPERTY: txtDefaultProperty.Text, CORSPOND_ADD: txtCorrespondence.Text, WARD: txtWard.Text, CITY: txtCity.Text, PINCODE: txtPincode.Text, OCC_ADD: txtOccupationAdd.Text, OCC_DETAIL: ddlOccupation.SelectedValue, MOBILE1: txtMob1.Text, MOBILE2: txtmob2.Text, MID: Session["MID"].ToString());
                            if (result > 0)
                            {
                                WebMsgBox.Show("Data Saved successfully..!!", this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Add _" + txtCaseNO.Text + "_" + txtCaseY.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                Clear();
                                BindGrdMain();
                            }
                       // }
                    }

                    WebMsgBox.Show("Member Not  Present.", this.Page); 
                }
                WebMsgBox.Show("Caseno Not Present..!!", this.Page);
            }
            if (ViewState["FLAG"].ToString() == "ATH")
            {
                results = SRO.CHKMID(caseyear: txtCaseY.Text, MEMBERNO: txtMember.Text,caseno:txtCaseNO.Text);

                if (results != Session["UID"].ToString())
                {
                    results = SRO.GETSTAGE1(MEMNo: txtMember.Text, CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text);
                    if (results != "1003")
                    {

                        result = SRO.AuthorizeDefaulter(CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text, MEMBERNO: txtMember.Text);
                        WebMsgBox.Show(" Authorize successfully..!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Add _" + txtCaseNO.Text + "_" + txtCaseY.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Clear();
                        BindGrdMain();
                    }
                    WebMsgBox.Show("Data has Already Authorized..!!", this.Page);
                    Clear();
                    return;
                }
                WebMsgBox.Show("Your Not Authorized Person..!!", this.Page);
                
                return;
            }

            if (ViewState["FLAG"].ToString() == "DLT")
            {
                results = SRO.GETSTAGE1(MEMNo: txtMember.Text, CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text);
                if (results != "1004")
                {

                    result = SRO.DeletDefaulter(CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text, MEMBERNO: txtMember.Text);
                    WebMsgBox.Show(" Delete successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Add _" + txtCaseNO.Text + "_" + txtCaseY.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                    BindGrdMain();
                }
                WebMsgBox.Show("Data has Already Delete..!!", this.Page);
                Clear();
                return;
            }
            }
        
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
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
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {
        DT = SRO.GetDefaulter(caseyear:txtCaseY.Text,caseno:txtCaseNO.Text);
        if (DT.Rows.Count > 0)
        {
            txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
            //txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
            txtDefName.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();
        }
    }
    protected void txtMember_TextChanged(object sender, EventArgs e)
    {
     //  txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
    }
    protected void txtMemberName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtMemberName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtMemberName.Text = CT[0].ToString();
                txtMember.Text = CT[1].ToString();


            }
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
            SRO.BindGrdDefaulter(GrdDemand);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void Clear()
    {
        txtCaseNO.Text = "";
        txtCaseY.Text = "";
        txtMember.Text = "";
        txtMemberName.Text = "";
        txtDefName.Text = "";
        txtDefaultProperty.Text = "";
        txtCorrespondence.Text = "";
        txtWard.SelectedValue = "0";
        txtMob1.Text = "";
        txtmob2.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtOccupationAdd.Text = "";
        ddlOccupation.SelectedValue = "0";


    }


    public void ViewDetails( string CASENO, string CASE_YEAR)
    {
        try
        {
            DT = SRO.ViewDetailsDefaulter(CASENO, CASE_YEAR);
            if (DT.Rows.Count > 0)
            {
                
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
               // txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
                txtDefName.Text = DT.Rows[0]["DEFAULTER_NAME"].ToString();
                txtDefaultProperty.Text = DT.Rows[0]["DEFAULTER_PROPERTY"].ToString();

                txtMob1.Text = DT.Rows[0]["MOBILE1"].ToString() == "0" ? "" : DT.Rows[0]["MOBILE1"].ToString();
                txtmob2.Text = DT.Rows[0]["MOBILE2"].ToString() == "0" ? "" : DT.Rows[0]["MOBILE1"].ToString();
                txtCorrespondence.Text = DT.Rows[0]["CORSPOND_ADD"].ToString();
                txtCity.Text = DT.Rows[0]["CITY"].ToString();
                txtWard.SelectedValue = DT.Rows[0]["WARD"].ToString();
                txtPincode.Text = DT.Rows[0]["PINCODE"].ToString();
                txtOccupationAdd.Text = DT.Rows[0]["OCC_ADD"].ToString();
                ddlOccupation.SelectedValue = DT.Rows[0]["OCC_DETAIL"].ToString();
         
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
    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "DLT";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
           
            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            txtCaseNO.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();

            results = SRO.GETSTAGE1(MEMNo:txtMember.Text,CASENO: txtCaseNO.Text,CASEY: txtCaseY.Text);
            if (results == "1004")
                  {
               
                        WebMsgBox.Show("Data has Already Delete..!!", this.Page);
                        Clear();
                        return;
                    }
            
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Cancel";
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
           
            lblActivity.Text = "Cancel Demand";
            ENTF(false);
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
            ViewState["FLAG"] = "ATH";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');

            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            txtCaseNO.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();

            results = SRO.GETSTAGE(Session["brcd"].ToString(), txtCaseNO.Text, txtCaseY.Text);
            if (results == "1004")
            {

                WebMsgBox.Show("Data has Already authorized..!!", this.Page);
                Clear();
                return;
            }

            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Authorize";
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());

            lblActivity.Text = "Cancel Demand";
            ENTF(false);
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
            ViewState["FLAG"] = "VW";
            

            BtnSubmit.Visible = false;
          
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
         
            ViewState["CASENO"] = id[0].ToString();
            ViewState["CASE_YEAR"] = id[1].ToString();
            txtCaseNO.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
          
           
            ViewDetails(ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            lblActivity.Text = "View Demand";
            ENTF(false);
          
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public bool ENTF(bool TF)
    {
        try
        {
            txtCaseNO.Enabled = TF;
            txtCaseY.Enabled = TF;
            txtMember.Enabled = TF;
            txtMemberName.Enabled = TF;
            txtDefName.Enabled = TF;
            txtDefaultProperty.Enabled = TF;
            txtMob1.Enabled = TF;
            txtmob2.Enabled = TF;
            txtCorrespondence.Enabled = TF;
            txtWard.Enabled = TF;
            txtCity.Enabled = TF;
            txtPincode.Enabled = TF;
            txtOccupationAdd.Enabled = TF;
            ddlOccupation.Enabled = TF;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return TF;
    }
    protected void GrdDemand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}