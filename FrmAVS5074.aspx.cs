using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5074 : System.Web.UI.Page
{
    ClsAVS5074 CLS = new ClsAVS5074();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCashPaymentt CP = new ClsCashPaymentt();
    DataTable DT = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    int tempaccno = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CustNo = Request.QueryString["CUSTNO"].ToString();
            txtCustNo.Text = CustNo;
            txtCustName.Text = CLS.GetCustName(txtCustNo.Text, Session["BRCD"].ToString());
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtFBrcdName.Text = CLS.GetBRCD(Session["BRCD"].ToString());
            GetAccDetails();

        }
    }

    public void GetAccDetails()
    {
        try
        {
            DT = CLS.GetAccDetails(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "ALL");
            GrdDisp.DataSource = DT;
            GrdDisp.DataBind();
           // CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            //WebMsgBox.Show("hello",this.Page);
            //return;
            if(txtTBrcd.Text=="")
            {

            WebMsgBox.Show("Please enter the To Branch Code",this.Page);
                return;
            }

          
           string Date = CP.openDay(txtTBrcd.Text);
           if (Session["BRCD"].ToString() != txtTBrcd.Text)
           {
               if (Date != Session["EntryDate"].ToString())
               {
                   WebMsgBox.Show("Branch " + txtTBrcd.Text + " not working same date on branch " + Session["BRCD"].ToString() + "...!!", this.Page);
                   return;
               }
               else
               {
                   int Res = 0;
                   int IntResult = 0;
                   int IntResult1 = 0;
                   string SetNo = "";
                   Double BalanceAmt = 0;
                   Double BalanceLoan = 0;
                   DataTable Dtacc = new DataTable();
                   DataTable DtLoan = new DataTable();
                   DataTable DtSurity = new DataTable();
                   DataTable DT2 = new DataTable();
                   Dtacc = CLS.GetAccDetails(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                   DtSurity = CLS.GetSurity(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());

                   SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                   if (Dtacc.Rows.Count > 0)
                   {
                       for (int i = 0; i < Dtacc.Rows.Count; i++)
                       {
                           string Subglcode = "", AccNo = "", IR = "", Bal = "", Value = "", AccNo1 = "", Subglcode1 = "", GlCode1 = "";
                           string CN = "0", CD = "01/01/1990";
                           if (Session["BRCD"].ToString() == Dtacc.Rows[i]["BRCD"].ToString())
                           {
                               Subglcode = Dtacc.Rows[i]["SUBGLCODE"].ToString();
                               //Condition comment as per discuss ambika ma'am
                               //tempaccno = CLS.CheckAccNo(Subglcode, txtTBrcd.Text,Convert.ToString(Dtacc.Rows[i]["Accno"]),Convert.ToString(Dtacc.Rows[i]["CustNo"]));
                               //if (tempaccno >0)
                               //{
                               //    WebMsgBox.Show(tempaccno + " is already  taken ", this.Page);
                               //    return;
                               //}
                               if (rdbsamey.Checked == true)
                               {
                                   AccNo = Convert.ToString(Dtacc.Rows[i]["Accno"]);
                               }
                               else if (rdbsamen.Checked == true)
                               {
                                   AccNo = CLS.getAccNo(Subglcode, txtTBrcd.Text).ToString();
                               }

                               if (Dtacc.Rows[i]["GLCODE"].ToString() == "1" || Dtacc.Rows[i]["GLCODE"].ToString() == "4" || Dtacc.Rows[i]["GLCODE"].ToString() == "5")
                               {
                                   
                                  // Value = CLS.GetNewAcc(txtCustNo.Text, Dtacc.Rows[i]["ACCNO"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString()=="4"?"9":"10", Session["BRCD"].ToString());//commented by prasad for taking saving account also in transfer with same account no
                                   Value = CLS.GetNewAcc(txtCustNo.Text, Dtacc.Rows[i]["ACCNO"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString() == "1" ? "1" : Dtacc.Rows[i]["GLCODE"].ToString() == "4" ? "9" : "10", Session["BRCD"].ToString());
                                   if (Value != "" || Value != null)
                                   {
                                   }
                                   else
                                   {
                                       string[] Splitvalue = Value.Split('_');
                                       AccNo1 = Splitvalue[0].ToString();
                                       Subglcode1 = Splitvalue[1].ToString();
                                       if (Dtacc.Rows[i]["GLCODE"].ToString() == "5")
                                       {
                                           IR = CLS.getIR(Subglcode1, Session["BRCD"].ToString());
                                           GlCode1 = CLS.getGlCode(IR, Session["BRCD"].ToString());
                                       }
                                       Bal = CLS.getBal(txtCustNo.Text, AccNo1, Subglcode1, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                                       if (Bal != "0" || Bal != null || Bal != "")
                                       {
                                           Res = CLS.UpdateAVSAcc(Subglcode1, AccNo1, AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "1", Session["EntryDate"].ToString());
                                           if (Res > 0)
                                           {
                                               IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString() == "4" ? "9" : "10", Subglcode1.ToString(), AccNo1,
                                         "customer transfer - " + txtTBrcd.Text + " - " + txtTBrcd.Text + "/" + AccNo1 + "", "", Convert.ToDouble(Bal), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", txtFBrcd.Text, Session["MID"].ToString(), Subglcode1.ToString() + "/" + AccNo1, "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, Dtacc.Rows[i]["RECSRNO"].ToString());
                                               if (Dtacc.Rows[i]["GLCODE"].ToString() == "5")
                                               {
                                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, IR.ToString(), AccNo.ToString(),
                                             "customer transfer - " + txtFBrcd.Text + " - " + txtFBrcd.Text + "/" + Dtacc.Rows[i]["ACCNO"].ToString() + "", "", Convert.ToDouble(Bal), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                             "0", txtTBrcd.Text, Session["MID"].ToString(), Subglcode1.ToString() + "/" + AccNo.ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, Dtacc.Rows[i]["RECSRNO"].ToString());
                                               }
                                               else
                                               {
                                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString() == "4" ? "9" : "10", Subglcode1.ToString(), AccNo.ToString(),
                                           "customer transfer - " + txtFBrcd.Text + " - " + txtFBrcd.Text + "/" + Dtacc.Rows[i]["ACCNO"].ToString() + "", "", Convert.ToDouble(Bal), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "0", txtTBrcd.Text, Session["MID"].ToString(), Subglcode1.ToString() + "/" + AccNo.ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, Dtacc.Rows[i]["RECSRNO"].ToString());
                                               }

                                               BalanceAmt += Convert.ToDouble(Dtacc.Rows[i]["BALANCE"].ToString());
                                           }
                                       }
                                   }
                               }
                               Res = CLS.UpdateAVSAcc(Subglcode, Dtacc.Rows[i]["ACCNO"].ToString(), AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "1", Session["EntryDate"].ToString());
                               if (Res > 0)
                               {
                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString(), Subglcode.ToString(), Dtacc.Rows[i]["ACCNO"].ToString(),
                                   "customer transfer - " + txtTBrcd.Text + " - " + txtTBrcd.Text + "/" + AccNo + "", "", Convert.ToDouble(Dtacc.Rows[i]["BALANCE"].ToString()), "2", "7", "TR", SetNo, CN, CD, "0", "0", "1001",
                                   "0", txtFBrcd.Text, Session["MID"].ToString(), Subglcode.ToString() + "/" + Dtacc.Rows[i]["ACCNO"].ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, Dtacc.Rows[i]["RECSRNO"].ToString());

                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Dtacc.Rows[i]["GLCODE"].ToString(), Subglcode.ToString(), AccNo.ToString(),
                                  "customer transfer - " + txtFBrcd.Text + " - " + txtFBrcd.Text + "/" + Dtacc.Rows[i]["ACCNO"].ToString() + "", "", Convert.ToDouble(Dtacc.Rows[i]["BALANCE"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001",
                                  "0", txtTBrcd.Text, Session["MID"].ToString(), Subglcode.ToString() + "/" + AccNo.ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, Dtacc.Rows[i]["RECSRNO"].ToString());
                                   BalanceAmt += Convert.ToDouble(Dtacc.Rows[i]["BALANCE"].ToString());
                               }
                           }
                       }
                   }
                   DtLoan = CLS.GetLoanAccDetails(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                   if (DtLoan.Rows.Count > 0)
                   {
                       for (int k = 0; k < DtLoan.Rows.Count; k++)
                       {
                           string Subglcode = "", AccNo = "", glcode = "", IR = "", Bal = "", Value = "",AccNo1="",Subglcode1="",GlCode1="" ;
                           if (Session["BRCD"].ToString() == DtLoan.Rows[k]["BRCD"].ToString())
                           {
                               Subglcode = DtLoan.Rows[k]["SUBGLCODE"].ToString();
                               glcode = CLS.getGlCode(DtLoan.Rows[k]["SUBGLCODE"].ToString(), Session["BRCD"].ToString());
                              
                             //  AccNo = CLS.getAccNo(Subglcode, txtTBrcd.Text).ToString();
                               if (rdbsamey.Checked == true)
                               {
                                   AccNo = Convert.ToString(DtLoan.Rows[k]["Accno"]);
                               }
                               else if (rdbsamen.Checked == true)
                               {
                                   AccNo = CLS.getAccNo(Subglcode, txtTBrcd.Text).ToString();
                               }

                               Value = CLS.GetNewAcc(txtCustNo.Text, DtLoan.Rows[k]["ACCNO"].ToString(), "11",Session["BRCD"].ToString());
                               if (Value != "" || Value != null)
                               {
                               }
                               else
                               {
                                   string[] Splitvalue = Value.Split('_');
                                   AccNo1 = Splitvalue[0].ToString();
                                   Subglcode1 = Splitvalue[1].ToString();
                                   IR = CLS.getIR(Subglcode1, Session["BRCD"].ToString());
                                   GlCode1 = CLS.getGlCode(IR, Session["BRCD"].ToString());
                                   Bal = CLS.getBal(txtCustNo.Text, AccNo1, Subglcode1, Session["BRCD"].ToString(),Session["EntryDate"].ToString());
                                   if (Bal != "0" || Bal != null || Bal != "")
                                   {
                                       Res = CLS.UpdateAVSAcc(Subglcode1, AccNo1, AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "1", Session["EntryDate"].ToString());
                                       if (Res > 0)
                                       {
                                           IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "11", Subglcode1.ToString(), AccNo1,
                                     "customer transfer - " + txtTBrcd.Text + " - " + txtTBrcd.Text + "/" + AccNo1 + "", "", Convert.ToDouble(Bal), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                     "0", txtFBrcd.Text, Session["MID"].ToString(), Subglcode1.ToString() + "/" + AccNo1, "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text,"0");

                                           IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, IR.ToString(), AccNo.ToString(),
                                     "customer transfer - " + txtFBrcd.Text + " - " + txtFBrcd.Text + "/" + DtLoan.Rows[k]["ACCNO"].ToString() + "", "", Convert.ToDouble(Bal), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                     "0", txtTBrcd.Text, Session["MID"].ToString(), Subglcode1.ToString() + "/" + AccNo.ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                                           BalanceLoan += Convert.ToDouble(DtLoan.Rows[k]["BALANCE"].ToString());
                                       }
                                   }
                               }
                               Res = CLS.UpdateAVSAcc(Subglcode, DtLoan.Rows[k]["ACCNO"].ToString(), AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "1", Session["EntryDate"].ToString());
                               if (Res > 0)
                               {
                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, Subglcode.ToString(), DtLoan.Rows[k]["ACCNO"].ToString(),
                             "customer transfer - " + txtTBrcd.Text + " - " + txtTBrcd.Text + "/" + AccNo + "", "", Convert.ToDouble(DtLoan.Rows[k]["BALANCE"].ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                             "0", txtFBrcd.Text, Session["MID"].ToString(), Subglcode.ToString() + "/" + DtLoan.Rows[k]["ACCNO"].ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                                   IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, Subglcode.ToString(), AccNo.ToString(),
                             "customer transfer - " + txtFBrcd.Text + " - " + txtFBrcd.Text + "/" + DtLoan.Rows[k]["ACCNO"].ToString() + "", "", Convert.ToDouble(DtLoan.Rows[k]["BALANCE"].ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                             "0", txtTBrcd.Text, Session["MID"].ToString(), Subglcode.ToString() + "/" + AccNo.ToString(), "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                                   BalanceLoan += Convert.ToDouble(DtLoan.Rows[k]["BALANCE"].ToString());
                               }
                           }
                           }
                       }
                   if (BalanceLoan!=0 || BalanceAmt!=0)
                   {
                       string Para = CLS.CheckPara("SHRALLOT");
                       if (Para == "HO")
                       {
                           if (BalanceLoan > BalanceAmt)
                           {
                               Double amountTotal = 0;
                               amountTotal = BalanceLoan - BalanceAmt;
                               DT2 = CLS.GetADMSubGl("1");
                               IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", txtTBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl("1");
                               IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", txtFBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtFBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", "1", Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtTBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", "1", Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");
                           }
                           else
                           {
                               Double amountTotal = 0;
                               amountTotal = BalanceAmt - BalanceLoan;
                               DT2 = CLS.GetADMSubGl("1");
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", txtTBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl("1");
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", txtFBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtFBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", "1", Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtTBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", "1", Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");
                           }
                       }
                       else
                       {
                           if (BalanceLoan > BalanceAmt)
                           {
                               Double amountTotal = 0;
                               amountTotal = BalanceLoan - BalanceAmt;
                               DT2 = CLS.GetADMSubGl(txtFBrcd.Text);
                               IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", txtTBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtTBrcd.Text);
                               IntResult = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", txtFBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");
                           }
                           else
                           {
                               Double amountTotal = 0;
                               amountTotal = BalanceAmt - BalanceLoan;
                               DT2 = CLS.GetADMSubGl(txtFBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                          "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                          "0", txtTBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");

                               DT2 = CLS.GetADMSubGl(txtTBrcd.Text);
                               IntResult1 = CLS.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                         "customer transfer - " + txtFBrcd.Text + " - " + txtTBrcd.Text + "/ 0 ", "", Convert.ToDouble(amountTotal.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                         "0", txtFBrcd.Text, Session["MID"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString() + "/ 0", "0", "CUST_TR", txtCustNo.Text, txtCustName.Text, "", Session["BRCD"].ToString(), txtTBrcd.Text, "0");
                           }
                       }
                       if (DtSurity.Rows.Count > 0)
                       {
                           for (int m = 0; m < DtSurity.Rows.Count; m++)
                           {
                               string Subglcode = "", AccNo = "";
                               if (Session["BRCD"].ToString() == DtSurity.Rows[m]["BRCD"].ToString())
                               {
                                   Subglcode = DtSurity.Rows[m]["loanglcode"].ToString();
                                   if (m == 0)
                                       AccNo = CLS.getAccNo(Subglcode, txtTBrcd.Text).ToString();
                                   else if (Res > 0)
                                       AccNo = CLS.getAccNo(Subglcode, txtTBrcd.Text).ToString();
                                   if (m == 0)
                                       Res = CLS.UpdateAVSAcc(Subglcode, DtSurity.Rows[m]["loanACCNO"].ToString(), AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "2", Session["EntryDate"].ToString());
                                   else if (Res > 0)
                                       Res = CLS.UpdateAVSAcc(Subglcode, DtSurity.Rows[m]["loanACCNO"].ToString(), AccNo, Session["BRCD"].ToString(), txtCustNo.Text, txtTBrcd.Text, Session["MID"].ToString(), "2", Session["EntryDate"].ToString());
                                   if (Res > 0)
                                   {

                                   }
                               }
                           }
                       }
                   }
                   else
                   {
                       WebMsgBox.Show("Customer must be from same branch", this.Page);
                       return;
                   }
                   if (IntResult > 0 || IntResult1 > 0)
                   {
                       WebMsgBox.Show("Successfully Transfer!!! setno=" + SetNo , this.Page);
                       cleardata();
                   }
               }
           }
           else
           {
               WebMsgBox.Show("Customer Transfer in same branch is not Allow", this.Page);
               return;
           }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void cleardata()
    {
        try
        {
            GrdDisp.DataSource = null;
            GrdDisp.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtTBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTBrcdName.Text = CLS.GetBRCD(txtTBrcd.Text);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}