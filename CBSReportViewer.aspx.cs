using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class CBSReportViewer : System.Web.UI.Page
{
    public static string RptName;
    string BankName, BranchName, UName;
    string BRCD, FBRCD, TBRCD, PRCD, FPRCD, TPRCD, ACCNO, FACCNO, TACCNO;
    string EDATE, FDATE, TDATE, CUSTNO, APPNO;
    string FLAG1, FLAG2, FBKcode, TBKcode, FDate, TDate, UserName, Mid, MM, YY, FMM, FYY, TMM, TYY;
    string LAMT, PERIOD, RATE, REPAYAMT, STARTDATE, PERIODTYPE;
    ClsLogin LG = new ClsLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RptName = Request.QueryString["rptname"].ToString();

            if (RptName == "RptEmiChart.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            DataSet thisDataSet1 = new DataSet();
            DataSet thisDataSet2 = new DataSet();
            DataSet thisDataSet3 = new DataSet();
            DataSet thisDataSet4 = new DataSet();

            //  Added By amol Scrutiny Sheet Report
            if (RptName == "RptEmiChart.rdlc")
            {
                //thisDataSet1 = EE.GetLoanEnquiryReport(Convert.ToDouble(LAMT), Convert.ToDouble(RATE), Convert.ToDouble(PERIOD), STARTDATE, PERIODTYPE);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            ReportDataSource DataSource1 = new ReportDataSource("DataSet1", thisDataSet1.Tables["Table1"]);
            ReportDataSource DataSource2 = new ReportDataSource("DataSet2", thisDataSet2.Tables["Table1"]);
            ReportDataSource DataSource3 = new ReportDataSource("DataSet3", thisDataSet3.Tables["Table1"]);
            ReportDataSource DataSource4 = new ReportDataSource("DataSet4", thisDataSet4.Tables["Table1"]);

            RdlcPrint.LocalReport.ReportPath = Server.MapPath("~/" + RptName + "");
            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource1);


            RdlcPrint.LocalReport.Refresh();
            string fileName = "";
            RdlcPrint.Visible = true;

            DataTable DT = new DataTable();
            DT = LG.GetBankName(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                BankName = DT.Rows[0]["BankName"].ToString();
                BranchName = DT.Rows[0]["BranchName"].ToString();
            }

            if (RptName == "RptEmiChart.rdlc")
            {
                fileName = "";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            RdlcPrint.LocalReport.DataSources.Add(DataSource2);
            RdlcPrint.LocalReport.DataSources.Add(DataSource3);
            RdlcPrint.LocalReport.DataSources.Add(DataSource4);
            RdlcPrint.LocalReport.Refresh();
        }
    }
}