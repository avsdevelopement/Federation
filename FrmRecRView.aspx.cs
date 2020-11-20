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
using System.Linq;

public partial class FrmRecRView : System.Web.UI.Page
{
    public static string RptName, fileName, BkName, BrName, UID, ASONDT, RECDIV, RECCODE, MM, YY, RECFOR, FL, SFL, BKCD, REPTYPE,Div,Dep;
    public string BRCD,FBRCD,TBRCD;
    DataTable DT1 = new DataTable();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();
    ClsRecoveryPosting RP = new ClsRecoveryPosting();
    ClsLogin LG = new ClsLogin();
    ClsPTRegister PT = new ClsPTRegister();
    ClsLrAndNr LNR = new ClsLrAndNr();
    ClsDemandReport DR = new ClsDemandReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RptName = Request.QueryString["rptname"].ToString();
            #region Report name Conditions

            if (RptName == "RptDemandSummary.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptRecoveryStatement_Total.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UID = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECDIV"] != null)
                {
                    RECDIV = Request.QueryString["RECDIV"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECCODE"] != null)
                {
                    RECCODE = Request.QueryString["RECCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptDemandDetails.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptLrAndNr.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECDIV"] != null)
                {
                    RECDIV = Request.QueryString["RECDIV"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECCODE"] != null)
                {
                    RECCODE = Request.QueryString["RECCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECFOR"] != null)
                {
                    RECFOR = Request.QueryString["RECFOR"].ToString().Replace("%27", "");
                }
                
            }
            if (RptName == "RptRecoveryStatement.rdlc" || RptName == "RptRecoveryStatement_1009.rdlc" || RptName == "RptRecoveryStatement_1010.rdlc" || RptName == "RptRecoveryStatement_ALL.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UID = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECDIV"] != null)
                {
                    RECDIV = Request.QueryString["RECDIV"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECCODE"] != null)
                {
                    RECCODE = Request.QueryString["RECCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECFOR"] != null)
                {
                    RECFOR = Request.QueryString["RECFOR"].ToString().Replace("%27", "");
                }

            }

            if (RptName == "RptExRecBeforePost.rdlc")
            {

                if (Request.QueryString["REPTYPE"] != null)
                {
                    REPTYPE = Request.QueryString["REPTYPE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BKCD"] != null)
                {
                    BKCD = Request.QueryString["BKCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SFL = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UID = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECDIV"] != null)
                {
                    RECDIV = Request.QueryString["RECDIV"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECCODE"] != null)
                {
                    RECCODE = Request.QueryString["RECCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECFOR"] != null)
                {
                    RECFOR = Request.QueryString["RECFOR"].ToString().Replace("%27", "");
                }

            }


            if (RptName == "RptRecoveryAfterPost.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UID = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECDIV"] != null)
                {
                    RECDIV = Request.QueryString["RECDIV"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECCODE"] != null)
                {
                    RECCODE = Request.QueryString["RECCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RECFOR"] != null)
                {
                    RECFOR = Request.QueryString["RECFOR"].ToString().Replace("%27", "");
                }

            }
            if (RptName == "RptPTRegister.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UID = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BANKCD"] != null)
                {
                    BKCD = Request.QueryString["BANKCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    Div = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    Dep = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }

            }


            #endregion

            #region Calling Function
            DataSet thisDataSet = new DataSet();
            DataSet thisDataSet1 = new DataSet();

            if (RptName == "RptDemandSummary.rdlc")
            {
                thisDataSet = GetDemandRep(FL,FBRCD,TBRCD,MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptDemandDetails.rdlc")
            {
                thisDataSet = GetDemandRep(FL, FBRCD, TBRCD, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLrAndNr.rdlc")
            {
                thisDataSet = GetLrAndNr(BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            // RptRecoveryStatement
            if (RptName == "RptRecoveryStatement.rdlc")
            {
                thisDataSet = GetRecoveryStatRep(BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptRecoveryStatement_1010.rdlc")
            {
                thisDataSet = GetRecoveryStatRep(BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptExRecBeforePost.rdlc")
            {
                thisDataSet = GetRecoveryExRep(FL, SFL, BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "RptRecoveryStatement_1009.rdlc" || RptName == "RptRecoveryStatement_ALL.rdlc")
            {
                thisDataSet = GetRecoveryStatRep_1009(BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            // RptRecoveryStatement
            if (RptName == "RptRecoveryAfterPost.rdlc")
            {
                thisDataSet = GetRecoveryAftrePost(BRCD, RECDIV, RECCODE, MM, YY);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptRecoveryStatement_Total.rdlc")
            {
                thisDataSet = PT.GetRecoveryStatement_Total(ASONDT, BRCD, MM, YY, BKCD, Div, Dep);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptPTRegister.rdlc")
            {
                thisDataSet1 = PT.GetPtRegister(FL, ASONDT, BRCD, MM, YY, BKCD,Div,Dep);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            #endregion


            #region Report Parameter
            ReportDataSource DataSource = new ReportDataSource("ReportDS", thisDataSet.Tables["Table1"]);
            ReportDataSource DataSource1 = new ReportDataSource("ReportDS1", thisDataSet1.Tables["Table1"]);

            RdlcPrint.LocalReport.ReportPath = Server.MapPath("~/" + RptName + "");
            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource);

            if (RptName == "RptBalanceS.rdlc")
            {
                RdlcPrint.LocalReport.DataSources.Add(DataSource);
                RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            }
            if (RptName == "RptDlyCshPosWDenom.rdlc")
            {
                RdlcPrint.LocalReport.DataSources.Add(DataSource);
                RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            }
            RdlcPrint.LocalReport.Refresh();


            DataTable DT = new DataTable();

            DT = LG.GetBankName(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                BkName = DT.Rows[0]["BankName"].ToString();
                BrName = DT.Rows[0]["BranchName"].ToString();
            }
            if (RptName == "RptDemandSummary.rdlc")
            {

                RptName = "Recovery DemandSummary";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["LOGINCODE"].ToString());
                ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                ReportParameter rp7 = new ReportParameter("MM", MM.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp6, rp7 });

            }
            if (RptName == "RptDemandDetails.rdlc")
            {

                RptName = "Recovery DemandDetails";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["LOGINCODE"].ToString());
                ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                ReportParameter rp7 = new ReportParameter("MM", MM.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3,rp6, rp7 });

            }

            if (RptName == "RptLrAndNr.rdlc")
            {
                
                    RptName = "Recovery Lr and Nr";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["LOGINCODE"].ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp5, rp4, rp6, rp7 });
                
            }

            if (RptName == "RptRecoveryStatement_Total.rdlc")
            {
                fileName = "Recovery Statement";
                if (RptName == "RptRecoveryStatement_Total.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp6 = new ReportParameter("MM", MM.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                }
            }

            if (RptName == "RptRecoveryAfterPost.rdlc")
            {
                fileName = "Recovery After Post";
                if (RptName == "RptRecoveryAfterPost.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());
                    ReportParameter rp8 = new ReportParameter("REPORT_NAME", FL.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }

            if (RptName == "RptRecoveryStatement.rdlc")
            {
                fileName = "Recovery Statement";
                if (RptName == "RptRecoveryStatement.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());
                    ReportParameter rp8 = new ReportParameter("REPORT_NAME", FL.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }
            if (RptName == "RptRecoveryStatement_1010.rdlc")
            {
                fileName = "Recovery Statement";
                if (RptName == "RptRecoveryStatement_1010.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());
                    ReportParameter rp8 = new ReportParameter("REPORT_NAME", FL.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }
            if (RptName == "RptRecoveryStatement_1009.rdlc" || RptName == "RptRecoveryStatement_ALL.rdlc")
            {
                fileName = "Recovery Statement";
                if (RptName == "RptRecoveryStatement_1009.rdlc" || RptName == "RptRecoveryStatement_ALL.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());
                    ReportParameter rp8 = new ReportParameter("REPORT_NAME", FL.ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }
            if (RptName == "RptExRecBeforePost.rdlc")
            {
                fileName = "Recovery Statemen tEx Report";
                if (RptName == "RptExRecBeforePost.rdlc")
                {
                    RptName = "Recovery Statement";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserId", UID.ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    ReportParameter rp5 = new ReportParameter("REC_FOR", RECFOR.ToString());
                    ReportParameter rp6 = new ReportParameter("YYYY", YY.ToString());
                    ReportParameter rp7 = new ReportParameter("MM", MM.ToString());
                    ReportParameter rp8 = new ReportParameter("REPORT_NAME", "Excess Recovery Report Before Posting");

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }

            if (RptName == "RptPTRegister.rdlc")
            {
                fileName = "P.T Register";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BkName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BrName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", UID.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

        }
    }
            #endregion


    #region U Functions
    public DataSet GetDemandRep(string FL,string FBRCD,string TBRCD,string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            DR.FBRCD = FBRCD;
            DR.TBRCD = TBRCD;
            DR.ASONDT = ASONDT;
            DR.MM = MM;
            DR.YY = YY;
            DR.FL = FL;
            DT1 = DR.Fn_BLGetDemandRep(DR);
            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    
    public DataSet GetLrAndNr(string BC, string RDiv, string RCode, string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            LNR.BRCD = BC;
            LNR.RECCODE = RCode;
            LNR.RECDIV = RDiv;
            LNR.ASONDT = ASONDT;
            LNR.MM = MM;
            LNR.YY = YY;
            LNR.FL = FL;
            DT1 = LNR.Fn_BLGetLNRNRReport(LNR);
            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetRecoveryStatRep(string BC, string RDiv, string RCode, string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            RS.BRCD = BC;
            RS.RECCODE = RCode;
            RS.RECDIV = RDiv;
            RS.ASONDT = ASONDT;
            RS.MM = MM;
            RS.YY = YY;
            RS.FL = FL;
            DT1 = RS.FnBL_GetRecoveryStatRep(RS);
            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetRecoveryExRep(string FL, string SFL, string BC, string RDiv, string RCode, string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            RP.BRCD = BC;
            RP.FL = FL;
            RP.SFL = SFL;
            RP.RECCODE = RCode;
            RP.RECDIV = RDiv;
            RP.ASONDT = ASONDT;
            RP.MM = MM;
            RP.YY = YY;
            RP.MID = Session["MID"].ToString();
            RP.BANKCODE = BKCD;
            if (REPTYPE == "S")
            {
                DT1 = RP.FnBl_RecoveryExReportSpecific(RP);
            }
            else
            {
                DT1 = RP.FnBl_RecoveryExReport(RP);
            }

            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetRecoveryStatRep_1009(string BC, string RDiv, string RCode, string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            RS.BRCD = BC;
            RS.RECCODE = RCode;
            RS.RECDIV = RDiv;
            RS.ASONDT = ASONDT;
            RS.MM = MM;
            RS.YY = YY;
            RS.FL = FL;
            DT1 = RS.FnBL_GetRecoveryStatRep_1009(RS);
            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetRecoveryAftrePost(string BC, string RDiv, string RCode, string MM, string YY)
    {
        DataSet ds1 = new DataSet();
        try
        {

            RS.BRCD = BC;
            RS.RECCODE = RCode;
            RS.RECDIV = RDiv;
            RS.ASONDT = ASONDT;
            RS.MM = MM;
            RS.YY = YY;
            RS.FL = FL;
            DT1 = RS.FnBL_GetRecoveryStatRep(RS);
            ds1.Tables.Add(DT1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    #endregion
}