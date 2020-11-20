using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCurrencyAdd : System.Web.UI.Page
{

    ClsCurrencyAdd CA = new ClsCurrencyAdd();
    int Result = 0;
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            TxtEntrydate.Text = Session["EntryDate"].ToString();
            TxtCloseBal.Focus();
            ViewState["Flag"] = Request.QueryString["Flag"];
            TxtVaultType.Text =CA.Get_Vault_Type(Session["MID"].ToString(),Session["BRCD"].ToString());
            TxtVaultType.Enabled = false;
            if (TxtVaultType.Text == null)
            {
                lblMessage.Text = "";
                lblMessage.Text = "User Not Valid!!....Change User";
                ModalPopup.Show(this.Page);
                return;
               
            }
            if (ViewState["Flag"].ToString() == "AD")
            {
                ltrlHeader.Text = " Add";
                GrdCurr.Columns[5].Visible = false;//stage
                GrdCurr.Columns[6].Visible = false;//modify
                GrdCurr.Columns[7].Visible = false;//autho
                GrdCurr.Columns[8].Visible = false;//delete
                ENDN(true);
                DIVRDB.Visible = false;
                BindGrid();
                

            }

           else if (ViewState["Flag"].ToString() == "MD")
            {
                ltrlHeader.Text = "Modify";
                BtnSubmit.Text = "Modify";
                GrdCurr.Columns[5].Visible = false;
                GrdCurr.Columns[6].Visible = true;
                GrdCurr.Columns[7].Visible = false;
                GrdCurr.Columns[8].Visible = false;
                DIVRDB.Visible = false;
                ENDN(true);
                BindGrid();
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                ltrlHeader.Text = "Delete";
                BtnSubmit.Text = "Delete";
                GrdCurr.Columns[5].Visible = false;
                GrdCurr.Columns[6].Visible = false;
                GrdCurr.Columns[7].Visible = false;
                GrdCurr.Columns[8].Visible = true;
                DIVRDB.Visible = false;
                ENDN(false);
                BindGrid();
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                ltrlHeader.Text = "Authorize";
                BtnSubmit.Text = "Authorize";
                GrdCurr.Columns[5].Visible = false;
                GrdCurr.Columns[6].Visible = false;
                GrdCurr.Columns[7].Visible = true;
                GrdCurr.Columns[8].Visible = false;
                DIVRDB.Visible = false;
                ENDN(false);
                BindGrid();
            }
            else if (ViewState["Flag"].ToString() == "VW")
            {
                ltrlHeader.Text = "View";
                BtnSubmit.Text = "View";
                DIV1.Visible = false;
                DIV2.Visible = false;
                DIV3.Visible = false;
                GrdCurr.Columns[5].Visible = true;
                GrdCurr.Columns[6].Visible = false;
                GrdCurr.Columns[7].Visible = false;
                GrdCurr.Columns[8].Visible = false;
               
              
            }
 
        }
    }
    protected void ClearData()
    {
        //TxtVaultType.Text = "";
        TxtNoOfNotes.Text = "";
        TxtNoteType.Text="";
        TxtTotal.Text="";
        TxtCloseBal.Text = "";
        TxtCloseBal.Focus();

    }
    protected void ENDN(bool TF)
    {
        TxtNoOfNotes.Enabled = TF;
        TxtNoteType.Enabled = TF;
       // TxtTotal.Enabled = TF;
        TxtCloseBal.Enabled = TF;
        

    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void BindGrid()
    {
        try
        {
            int RC = CA.Get_GridDetails(GrdCurr, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
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
           //ADD
            if (ViewState["Flag"].ToString() == "AD")
            {
       
                Result = CA.OPR_Currency(TxtVaultType.Text, TxtNoOfNotes.Text, TxtNoteType.Text, TxtTotal.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "INSERT");
                if (Result > 0)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Record Succesfully Submitted in Vault number '" + TxtVaultType.Text + "'";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Currency_add _"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Note Type already exist in Vault number '" + TxtVaultType.Text + "'";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
            }
                //MODIFY
            else if (ViewState["Flag"].ToString() == "MD")
            {
                Result=CA.OPR_Currency(TxtVaultType.Text, TxtNoOfNotes.Text, TxtNoteType.Text, TxtTotal.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "MODIFY");
                if (Result > 0)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Record Succesfully Modified in Vault number '" + TxtVaultType.Text + "'";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Currency_mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Authorized entry from Vault number '" + TxtVaultType.Text + "' Restricted";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }

            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                Result = CA.OPR_Currency(TxtVaultType.Text, TxtNoOfNotes.Text, TxtNoteType.Text, TxtTotal.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "DELETE");
                if (Result > 0)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Record Succesfully Deleted in Vault number '" + TxtVaultType.Text + "'";
                   
                    
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Currency_del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Authorized entry from Vault number '" + TxtVaultType.Text + "' cannot deleted";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }

            }

            else if (ViewState["Flag"].ToString() == "AT")
            {
                Result = CA.OPR_Currency(TxtVaultType.Text, TxtNoOfNotes.Text, TxtNoteType.Text, TxtTotal.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "AUTHORIZE");
                if (Result > 0)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Record Succesfully Authorized in Vault number '" + TxtVaultType.Text + "'";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Currency_autho _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Warning : Current User is Restricted, Change User";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }

            }
            else if (ViewState["Flag"].ToString() == "VW")
            {
                DIVRDB.Visible = true;
                if(RdbView.SelectedValue=="CO")
                Result = CA.View_curr(GrdCurr,"VIEW", "CREATED");

                else if(RdbView.SelectedValue=="AO")
                Result = CA.View_curr(GrdCurr,"VIEW", "AUTHO");

                else if (RdbView.SelectedValue == "AE")
                    Result = CA.View_curr(GrdCurr,"VIEW", "ALL");
                     
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void LnkModify_Click(object sender, EventArgs e)
    {
        ViewState["GRID_FL"] = "Mod";
    }
    protected void LnkAutho_Click(object sender, EventArgs e)
    {
        ViewState["GRID_FL"] = "Aut";
    }
   
    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["GRID_FL"] = "Del";
    }
    protected void GrdCurr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ID = (GrdCurr.SelectedRow.FindControl("ID") as Label).Text;
            DT = CA.Fill_TextBox(ID, Session["BRCD"].ToString(),Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtVaultType.Text = DT.Rows[0]["V_Type"].ToString();
                TxtNoteType.Text = DT.Rows[0]["NOTE_TYPE"].ToString();
                TxtNoOfNotes.Text = DT.Rows[0]["NO_OF_NOTES"].ToString();
                TxtTotal.Text = DT.Rows[0]["TOTAL_VALUE"].ToString();
            }

            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Warning : User is Restricted to perform this Operation'" + TxtVaultType.Text + "'";
                ModalPopup.Show(this.Page);
                ClearData();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtNoOfNotes_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Tot=0;
            Tot = (Convert.ToInt32(TxtNoOfNotes.Text)) * (Convert.ToInt32(TxtNoteType.Text));
            TxtTotal.Text = Convert.ToString(Tot);
            BtnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    protected void GrdCurr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCurr.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}