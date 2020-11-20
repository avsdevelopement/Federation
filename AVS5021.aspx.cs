using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AVS5021 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
               // Div_Monthly.Visible = false;
               // Div_Daily.Visible = false;
                string YEAR = "";
                //DateTime DT = Convert.ToDateTime(Convert.ToDateTime(Session["EntryDate"].ToString()).ToString("dd/MM/yyyy"));
                YEAR = DateTime.Now.Date.Year.ToString();//DT.Year.ToString();
                TxtFDate.Text = Session["EntryDate"].ToString();
                TxtTDate.Text = Session["EntryDate"].ToString();
               // Txtdate.Text = Session["EntryDate"].ToString();
                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
                //autoglname.ContextKey = Session["BRCD"].ToString();
             //   autoglname1.ContextKey = Session["BRCD"].ToString();
                TxtBRCD.Focus();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
              //  TxtATName.Text = AST.GetAccType(TxtAccType.Text, TxtBRCD.Text);
                //string[] GL = BD.GetAgentType(TxtAccType.Text, TxtBRCD.Text).Split('_');
             //   TxtATName.Text = GL[0].ToString();
              //  ViewState["GL"] = GL[1].ToString();
                TxtFDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
           //     TxtAccType.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
               // string custno = TxtATName.Text;
               // string[] CT = custno.Split('_');
             //   if (CT.Length > 0)
                {
                  //  TxtAccType.Text = CT[0].ToString();
                  //  TxtATName.Text = CT[1].ToString();
                    // string[] GLS = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
                    ViewState["GL"] = "2";
                    TxtFDate.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
              //  TxtATName.Text = "";
                TxtBRCD.Focus();
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

            string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + TxtBRCD.Text + "&rptname=Rpt_AVS0001.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    //protected void TxtACode_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtBRCD.Text != "")
    //        {
    //            TxtAName.Text = AST.GetAccType(TxtACode.Text, TxtBRCD.Text);
    //            string[] GL = BD.GetAgentType(TxtACode.Text, TxtBRCD.Text).Split('_');
    //            TxtAName.Text = GL[0].ToString();
    //            ViewState["GL"] = GL[1].ToString();
    //            Txtdate.Focus();
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Enter Branch Code First....!", this.Page);
    //            TxtACode.Text = "";
    //            TxtBRCD.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void TxtAName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtBRCD.Text != "")
    //        {
    //            string custno = TxtAName.Text;
    //            string[] CT = custno.Split('_');
    //            if (CT.Length > 0)
    //            {
    //                TxtACode.Text = CT[0].ToString();
    //                TxtAName.Text = CT[1].ToString();
    //                // string[] GLS = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
    //                ViewState["GL"] = "2";
    //            }
    //            else
    //            {
    //                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
    //                TxtAName.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void Rdb_No_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        if (Rdb_No.SelectedValue == "2")
    //        {
    //            Div_Daily.Visible = false;
    //            Div_Monthly.Visible = true;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
}