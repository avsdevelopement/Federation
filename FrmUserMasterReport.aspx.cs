using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmUserMasterReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAllOKReport AK = new ClsAllOKReport();
    ClsLogin LG = new ClsLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BD.BindExportFile(ddltype);
                
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "3")
        {
            try
            {
                string redirectURL = "FrmRView.aspx?&UserName=" + Session["UserName"].ToString() + "&rptname=RptUserMaster.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        else if (ddltype.SelectedValue == "4")
        { 
        try
                {
                    string BName = "";
                    string BRName = "";
                    DataTable DT = new DataTable();
                    DT = LG.GetBankName(Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        BName = DT.Rows[0]["BankName"].ToString();
                        BRName = DT.Rows[0]["BranchName"].ToString();
                    }

                     List<object> lst = new List<object>();
                    lst.Add(BName);
                    lst.Add(BRName);
                    lst.Add("ALL OK REPORT");
                    lst.Add(Session["USERNAME"].ToString());
               
                 
                   
                 
                    AllOKText AK = new AllOKText();
                    AK.RInit(lst);
                    AK.Start();
                    WebMsgBox.Show("Report Generated Succesfully!!!....", this.Page);
                   
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }

            }
        }
    }
    

