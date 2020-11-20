using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmShareRegister : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
   ClsAccountSTS AST = new ClsAccountSTS();
   ClsBindDropdown BD = new ClsBindDropdown();
   ClsLogMaintainance CLM = new ClsLogMaintainance();
   string FL = "";
   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           try
           {
               if (Session["UserName"] == null)
               {
                   Response.Redirect("FrmLogin.aspx");
               }

               //added by ankita 07/10/2017 to make user frndly 
               TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
               TxtTDate.Text = Session["EntryDate"].ToString();
           TxtBRCD.Focus();
           }
           catch (Exception Ex)
           {
               ExceptionLogging.SendErrorToText(Ex);
           }
       }
   }
    
     protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBRCD.Text != "" && TxtToBrcd.Text != "" && TxtFDate.Text != "" && TxtTDate.Text != "")
                {
                    FL = "Insert";//ankita 14/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ShareRegi_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FBRCD=" + TxtBRCD.Text + "&TBRCD=" + TxtToBrcd.Text + "&Edate=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=ShareRegister.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    WebMsgBox.Show("Please enter the details",this.Page);
                }
               
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


        protected void TxtBRCD_TextChanged(object sender, EventArgs e)
        {
            TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
            TxtToBrcd.Focus();
        }
        protected void TxtToBrcd_TextChanged(object sender, EventArgs e)
        {
            TxtTBname.Text = AST.GetBranchName(TxtToBrcd.Text);
            TxtFDate.Focus();
        }
}

    
