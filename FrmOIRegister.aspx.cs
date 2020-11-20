using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmOIRegister : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsOIRegister R = new ClsOIRegister();
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

                string Edate = Session["EntryDate"].ToString();
                ViewState["Flag"] = Request.QueryString["Flag"];
                if (ViewState["Flag"].ToString() == "OW")
                {
                    BtnReport.Text = "Outward Register";
                    LblReport.Text = "Outward";


                }
                if (ViewState["Flag"].ToString() == "IN")
                {
                    BtnReport.Text = "Inward Register";
                    LblReport.Text = "Inward";
                }
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
               
                
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ClearData()
    {
        TxtFBankcode.Text = "";
        TxtFBankname.Text = "";

        TxtTBankcode.Text = "";
        TxtTBankname.Text = "";
    }





     protected void Exit_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }


     protected void BtnTextReport_Click(object sender, EventArgs e)
     {
         try
         {
             List<object> lst = new List<object>();
             if (ViewState["Flag"].ToString() == "OW")
             {
                 lst.Add("Outward Report");
             }
             else
             {
                 lst.Add("Inward Report");
             }
             lst.Add(Session["UserName"].ToString());
             lst.Add(Session["BRCD"].ToString());
             if (RdbChkDate.Checked == true || RdbChkBoth.Checked == true)
             {
                 lst.Add(TxtFDate.Text);
             }
             else
             {
                 lst.Add("01/01/1990");
             }

             if (RdbChkDate.Checked == true || RdbChkBoth.Checked == true)
             {
                 lst.Add(TxtTDate.Text);
             }
             else
             {
                 lst.Add(Session["EntryDate"].ToString());
             }
            
             if (RdbChkBank.Checked == true || RdbChkBoth.Checked == true)
             {
                 lst.Add(TxtFBankcode.Text);

             }
             else
             {
                 lst.Add("1");
             }

             if (RdbChkBank.Checked == true || RdbChkBoth.Checked == true)
             {
                 lst.Add(TxtTBankcode.Text);

             }
             else
             {
                 lst.Add("5900");
             }
             
             lst.Add(TxtFBankname.Text);
             lst.Add(TxtTBankname.Text);
             lst.Add(ViewState["Flag"].ToString());
             OIReportText OR = new OIReportText();
             OR.RInit(lst);
             OR.Start();
             WebMsgBox.Show("Report Generated Succesfully!!!....",this.Page);
             RdbChkDate.Focus();
             FL = "Insert";//ankita 15/09/2017
             string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OiReg_Txt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
     }

     protected void RdbChkDate_CheckedChanged(object sender, EventArgs e)
     {
         FTBANK.Visible = false;
         FTDT.Visible = true;
         TxtFDate.Focus();
     }
     protected void RdbChkBank_CheckedChanged(object sender, EventArgs e)
     {
         FTBANK.Visible = true;
         FTDT.Visible = false;
         TxtFBankcode.Focus();
     
     }
     protected void RdbChkBoth_CheckedChanged(object sender, EventArgs e)
     {
         FTBANK.Visible = true;
         FTDT.Visible = true;
         TxtFDate.Focus();
     
     }
     protected void BtnReport_Click(object sender, EventArgs e)
     {
         string[] year = Session["EntryDate"].ToString().Split('/');

         
         try
         {
             string FD, TD, FBC, TBC;
             if (RdbChkDate.Checked == true)
             {
                 FBC = "1";
                 TBC = "5900";
                 FD = TxtFDate.Text;
                 TD = TxtTDate.Text;
             }
             else if (RdbChkBank.Checked == true)
             {
                 FBC = TxtFBankcode.Text;
                 TBC = TxtTBankcode.Text;
                 FD = "01/"+ year[1] +"/" + year[2] +"";
                 TD = Session["EntryDate"].ToString();
             }

             else
             {
                 FD = TxtFDate.Text;
                 TD = TxtTDate.Text;
                 FBC = TxtFBankcode.Text;
                 TBC = TxtTBankcode.Text;
             }
             FL = "Insert";//ankita 15/09/2017
             string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OIReg_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
             if (ViewState["Flag"].ToString() == "OW")
             {
                 string redirectURL = "FrmRView.aspx?FBanKCode=" + FBC + "&ToBankCode=" + TBC + "&UserName=" + Session["UserName"].ToString() + "&FDATE=" + FD + "&TDATE=" + TD + "&rptname=RptOutRegister.rdlc";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
             }
             else
             {

                 string redirectURL = "FrmRView.aspx?FBanKcode=" + FBC + "&ToBankCode=" + TBC + "&UserName=" + Session["UserName"].ToString() + "&FDATE=" + FD + "&TDATE=" + TD + "&rptname=RptInwardReg.rdlc";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
           
             }
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }

     }
         
}
    
