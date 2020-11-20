using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmDeadStockDetails : System.Web.UI.Page
{
    scustom customcs = new scustom();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDeadStock DS = new ClsDeadStock();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
   public static string Flag;
    string AC_Status = "";
    int Result;
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
                txtdate.Text = Session["EntryDate"].ToString();
                txtdate.Enabled = false;
                txtprdcode.Focus();
                autoglname.ContextKey = Session["BRCD"].ToString();
                BD.BindAssestLocation(ddlAstLoc);
                BD.BindAssestLocation(ddlbranchname);
                ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
                Flag = "1";
                ViewState["Flag"] = "AD";
                BtnSubmit.Text = "Submit";
                TblDiv_MainWindow.Visible = false;
                Div_grid.Visible = true;
                BindGrid("ED");
                BD.BindStatus(DDLstatusname);
                BD.BindDItem(DDlitemno);
                BD.BindASL(DDlASL);
                BD.BindDep(DDlDep);
                BD.BindSanction(DDlSanction);
                BD.BindTypeofitm(DDltypeofitm);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid(string Flag)
    {
        if (Flag == "ED")
        {

            int RS = DS.BindGrid(grdDead, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "");

        }
        else
        {
            int RS = DS.BindGrid(grdDead, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtprdcode.Text);
        }
    }
    protected void txtprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string res = "";
            string GL = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            ViewState["DRGL"] = GL[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(txtprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtprdname.Text = PDName;
                txtaccno.Focus();
                //res = DS.AccNodisplay(Session["BRCD"].ToString(), txtprdcode.Text);
                //if (res == "0")
                //{
                //    txtaccno.Text = txtprdcode.Text;
                //    txtaccname.Text = txtprdname.Text;
                //    txtdesc.Focus();
                //    if (ViewState["Flag"].ToString() == "AD")
                //    {
                //        //int checkdata = DS.CheckPrevData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                //        //if (checkdata > 0)
                //        //{
                //        //    WebMsgBox.Show("There is already details available  for this data ", this.Page);
                //        //    txtprdcode.Text = "";
                //        //    txtprdname.Text = "";
                //        //    txtaccno.Text = "";
                //        //    txtaccname.Text = "";
                //        //    txtprdcode.Focus();
                //        //    return;
                //        //}
                //    }
                //}
                //else
                //{
                //    WebMsgBox.Show("Please enter account No..!!", this.Page);
                //    txtaccno.Focus();
                    
                //}
                //string acbal = res == "0" ? "0" : txtaccno.Text;
                //txtclsgbal.Text = DS.GetOpenClose(Session["BRCD"].ToString(), txtprdcode.Text.Trim().ToString(), acbal, Session["EntryDate"].ToString(), "ClBal").ToString();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtprdcode.Text = "";
                txtprdcode.Focus();
            }
            if (ViewState["Flag"].ToString() == "AD")
            {
                subdata();
            }
            if (res == "0")
            {
                CallEdit();
            }
            BindGrid("PA");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string res = "";
            string custno = txtprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtprdname.Text = CT[0].ToString();
                txtprdcode.Text = CT[1].ToString();
                txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
                txtaccno.Focus();
                //res = DS.AccNodisplay(Session["BRCD"].ToString(), txtprdcode.Text);
                //if (res == "0")
                //{
                //    txtaccno.Text = txtprdcode.Text;
                //    txtaccname.Text = txtprdname.Text;
                //    if (ViewState["Flag"].ToString() == "AD")
                //    {
                //        //int checkdata = DS.CheckPrevData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                //        //if (checkdata > 0)
                //        //{
                //        //    WebMsgBox.Show("There is already details available  for this data ", this.Page);
                //        //    txtprdcode.Text = "";
                //        //    txtprdname.Text = "";
                //        //    txtaccno.Text = "";
                //        //    txtaccname.Text = "";
                //        //    txtprdcode.Focus();
                //        //    return;
                //        //}
                //    }
                //}
                //else
                //{
                //    WebMsgBox.Show("Please enter account No..!!", this.Page);

                //}
                //string acbal = res == "0" ? "0" : txtaccno.Text;
                //txtclsgbal.Text = DS.GetOpenClose(Session["BRCD"].ToString(), txtprdcode.Text.Trim().ToString(), acbal, Session["EntryDate"].ToString(), "ClBal").ToString();
            }
            if (ViewState["Flag"].ToString() == "AD")
            {
                subdata();
            }
             if (res == "0")
            {
                CallEdit();
            }
             BindGrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
     }
  
    protected void txtasstno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AName = DS.GetData("TOA", txtasstno.Text);
            if (AName != null)
            {
                txtasstname.Text = AName;
                txtdate.Focus();
            }
            else
            {
                WebMsgBox.Show("Assest Code is Invalid....!", this.Page);
                txtasstno.Text = "";
                txtasstno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtstatusno_TextChanged(object sender, EventArgs e)
    {
        try
        {
         string SName = BD.GetSta(txtstatusno.Text);
            if (SName == null)
            {
                WebMsgBox.Show("Status No is Invalid....!", this.Page);
                txtstatusno.Text = "";
                txtstatusno.Focus();
            }
            else
            {
                DDLstatusname.SelectedValue = SName;
             }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtsanctnno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string SAName = BD.GetSanctn(txtsanctnno.Text);
            if (SAName == null)
            {
                WebMsgBox.Show("Sanction Auth is Invalid....!", this.Page);
                txtsanctnno.Text = "";
                txtsanctnno.Focus();
            }
            else
            {
                DDlSanction.SelectedValue = SAName;
            }
}
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void txtitemno_TextChanged(object sender, EventArgs e)
    {
        try
        {
           
            string IName = BD.GetDitm(txtitemno.Text);
            if (IName == null)
            {
                WebMsgBox.Show("Item No is Invalid....!", this.Page);
                txtitemno.Text = "";
                txtitemno.Focus();
            }
            else
            {
                DDlitemno.SelectedValue = IName;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtitemtypeno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string ITName = BD.GetItemtypeno(txtitemtypeno.Text);
            if (ITName == null)
            {
                WebMsgBox.Show("Item Code is Invalid....!", this.Page);
                txtitemtypeno.Text = "";
                txtitemtypeno.Focus();
            }
            else
            {
                DDltypeofitm.SelectedValue = ITName;
           }
         
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   protected void txtaccno_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (txtprdcode.Text != "" && txtprdname.Text != "")
            {
                if (ViewState["Flag"].ToString() == "AD")
                {
                    //int checkdata = DS.CheckPrevData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                    //if (checkdata > 0)
                    //{
                    //    WebMsgBox.Show("There is already details available  for this data ", this.Page);
                    //    txtprdcode.Text = "";
                    //    txtprdname.Text = "";
                    //    txtaccno.Text = "";
                    //    txtaccname.Text = "";
                    //    txtprdcode.Focus();
                    //    return;
                    //}
                }
                string[] AN;
                AN = customcs.GetAccountNme(txtaccno.Text, txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                if (AN != null)
                {
                    txtaccname.Text = AN[1].ToString();
                    txtdesc.Focus();
               
                }
              
                 else
                {
                    WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                    txtaccno.Text = "";
                    txtaccno.Focus();
                }
                txtclsgbal.Text = DS.GetOpenClose(Session["BRCD"].ToString(), txtprdcode.Text.Trim().ToString(), txtaccno.Text, Session["EntryDate"].ToString(), "ClBal").ToString();
            }
            else
            {
                WebMsgBox.Show("Enter product code!!!",this.Page);
                txtaccno.Text = "";
                txtprdcode.Focus();
            }
             CallEdit();
             BindGrid("PA");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccname_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                //int checkdata = DS.CheckPrevData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                //if (checkdata > 0)
                //{
                //    WebMsgBox.Show("There is already details available  for this data ", this.Page);
                //    txtprdcode.Text = "";
                //    txtprdname.Text = "";
                //    txtaccno.Text = "";
                //    txtaccname.Text = "";
                //    txtprdcode.Focus();
                //    return;
                //}
            }
            string CUNAME = txtaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtaccname.Text = custnob[0].ToString();
                txtaccno.Text = custnob[1].ToString();
                txtdesc.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
            txtclsgbal.Text = DS.GetOpenClose(Session["BRCD"].ToString(), txtprdcode.Text.Trim().ToString(), txtaccno.Text, Session["EntryDate"].ToString(), "ClBal").ToString();
            BindGrid("PA");
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
            double BB,CB;
             BB = Convert.ToDouble(txtbookbal.Text);
                CB = Convert.ToDouble(txtclsgbal.Text);
                 if (txtprdcode.Text=="")
                  {
                      WebMsgBox.Show("Please enter Product code..!!", this.Page);
                      txtprdcode.Focus();
                      return;
                  }

                if (txtaccno.Text == "")
                {
                    WebMsgBox.Show("Please enter Account No..!!", this.Page);
                    txtaccno.Focus();
                    return;
                }

                if (txtdesc.Text == "")
                {
                    WebMsgBox.Show("Please enter Description..!!", this.Page);
                    txtdesc.Focus();
                    return;
                }

                if (txtasstno.Text == "")
                {
                    WebMsgBox.Show("Please enter Type Of Assest..!!", this.Page);
                    txtasstno.Focus();
                    return;
                }

                if (txtstatusno.Text == "")
                {
                    WebMsgBox.Show("Please enter Status No..!!", this.Page);
                    txtstatusno.Focus();
                    return;
                }

                if (txtsanctnno.Text == "")
                {
                    WebMsgBox.Show("Please enter Sanction No..!!", this.Page);
                    txtsanctnno.Focus();
                    return;
                }

                if (txtvendorname.Text == "")
                {
                    WebMsgBox.Show("Please enter Vendor's Name..!!", this.Page);
                    txtvendorname.Focus();
                    return;
                }

                if (txtperiod.Text == "")
                {
                    WebMsgBox.Show("Please enter Period..!!", this.Page);
                    txtperiod.Focus();
                    return;
                }

                if (txtitemno.Text == "")
                {
                    WebMsgBox.Show("Please enter Item No..!!", this.Page);
                    txtitemno.Focus();
                    return;
                }

                if (txtitemtypeno.Text == "")
                {
                    WebMsgBox.Show("Please enter Type Of Item..!!", this.Page);
                    txtitemtypeno.Focus();
                    return;
                }
                if (TxtIdCode.Text == "")
                {
                    WebMsgBox.Show("Please enterId code..!!", this.Page);
                    TxtIdCode.Focus();
                    return;
                }

                if (ddlbranchname.SelectedValue == "0")
                {
                    WebMsgBox.Show("Please Select Branch/Dept..!!", this.Page);
                    ddlbranchname.Focus();
                    return;
                }

                if (txtname.Text == "")
                {
                    WebMsgBox.Show("Please enter Name..!!", this.Page);
                    txtname.Focus();
                    return;
                }

                if (ddlwaranty.SelectedValue == "0")
                {
                    WebMsgBox.Show("Please Select Waranty/Guaranty..!!", this.Page);
                    ddlwaranty.Focus();
                    return;
                }

                if (txtamcdetail.Text == "")
                {
                    WebMsgBox.Show("Please enter AMC Details..!!", this.Page);
                    txtamcdetail.Focus();
                    return;
                }

                if (txtvalue.Text == "")
                {
                    WebMsgBox.Show("Please enter Value..!!", this.Page);
                    txtvalue.Focus();
                    return;
                }

                if (txtbillno.Text == "")
                {
                    WebMsgBox.Show("Please enter Bill No..!!", this.Page);
                    txtbillno.Focus();
                    return;
                }

                if (txtbilldate.Text == "")
                {
                    WebMsgBox.Show("Please enter Bill Date..!!", this.Page);
                    txtbilldate.Focus();
                    return;
                }

                if (ddlAstLoc.SelectedValue == "0")
                {
                    WebMsgBox.Show("Please Select Assest Location Code..!!", this.Page);
                    ddlAstLoc.Focus();
                    return;
                }

                if (txtasublocno.Text == "")
                {
                    WebMsgBox.Show("Please enter  Assest Sub  Location Code..!!", this.Page);
                    txtasublocno.Focus();
                    return;
                }

              

                if (txtassestdesc.Text == "")
                {
                    WebMsgBox.Show("Please enter Assest Description..!!", this.Page);
                    txtassestdesc.Focus();
                    return;
                }

         if (txtpurchasedate.Text == "")
                {
                    WebMsgBox.Show("Please enter Purchase Date..!!", this.Page);
                    txtpurchasedate.Focus();
                    return;
                }

                if (txtentrydate.Text == "")
                {
                    WebMsgBox.Show("Please enter EntryDate..!!", this.Page);
                    txtentrydate.Focus();
                    return;
                }

                if (txtquantity.Text == "")
                {
                    WebMsgBox.Show("Please enter Quality..!!", this.Page);
                    txtquantity.Focus();
                    return;
                }

                if (TxtValueUnit.Text == "")
                {
                    WebMsgBox.Show("Please enter Value unit..!!", this.Page);
                    TxtValueUnit.Focus();
                    return;
                }

                if (Txtpurchase.Text == "")
                {
                    WebMsgBox.Show("Please enter  Purchase Value..!!", this.Page);
                    Txtpurchase.Focus();
                    return;
                }

                if (txtvalason.Text == "")
                {
                    WebMsgBox.Show("Please enter Value As On Date..!!", this.Page);
                    txtvalason.Focus();
                    return;
                }

                if (txtperdep.Text == "")
                {
                    WebMsgBox.Show("Please enter PerDep..!!", this.Page);
                    txtperdep.Focus();
                    return;
                }

                if (txtdepno.Text == "")
                {
                    WebMsgBox.Show("Please enter Dep No..!!", this.Page);
                    txtdepno.Focus();
                    return;
                }

                if (txtbookbal.Text == "")
                {
                    WebMsgBox.Show("Please enter Book Balance..!!", this.Page);
                    txtbookbal.Focus();
                    return;
                }

               
            if (ViewState["Flag"].ToString() == "AD")
            {

                if (BB == CB)
                {
                    if (CB != 0.0)
                    {
                        int Result = DS.Insertdata(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text, txtdesc.Text, txtasstno.Text, txtasstname.Text, txtdate.Text, txtstatusno.Text, DDLstatusname.SelectedItem.Text, txtsanctnno.Text, DDlSanction.SelectedItem.Text, txtvendorname.Text,
                   txtperiod.Text, txtitemno.Text, DDlitemno.SelectedItem.Text, txtitemtypeno.Text, DDltypeofitm.SelectedItem.Text, TxtIdCode.Text, txtbrnchno.Text, txtname.Text, ddlwaranty.SelectedValue,
                   txtamcdetail.Text, txtvalue.Text, txtbillno.Text, txtbilldate.Text, txtchequeno.Text, txtchequedate.Text, txtalocno.Text, DDlASL.SelectedItem.Text, txtasublocno.Text, txtassestdesc.Text,
                   txtpurchasedate.Text, txtentrydate.Text, txtquantity.Text, TxtValueUnit.Text, Txtpurchase.Text, txtvalason.Text, txtperdep.Text, txtdepno.Text, DDlDep.SelectedItem.Text, txtbookbal.Text, txtclsgbal.Text, "1001", "Insert", Session["MID"].ToString());
                        if (Result > 0)
                        {
                            WebMsgBox.Show("Data Added successfully..!!", this.Page);
                            BindGrid("PA");
                            FL = "Insert";//Dhanya Shetty
                            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deadstock_Add _" + txtprdcode.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            ClearData();
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("There is no balance..!!", this.Page);
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Book Balance does not match..!!", this.Page);
                    txtbookbal.Text = "";
                    return;
                }
}
            else if (ViewState["Flag"].ToString() == "MD")
            {
                if (BB == CB)
                {
                   
                    int Result = DS.ModifyData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text, txtdesc.Text, txtasstno.Text, txtasstname.Text, txtdate.Text, txtstatusno.Text, DDLstatusname.SelectedItem.Text, txtsanctnno.Text, DDlSanction.SelectedItem.Text, txtvendorname.Text,
                        txtperiod.Text, txtitemno.Text, DDlitemno.SelectedItem.Text, txtitemtypeno.Text, DDltypeofitm.SelectedItem.Text, TxtIdCode.Text, txtbrnchno.Text, txtname.Text, ddlwaranty.SelectedValue,
                        txtamcdetail.Text, txtvalue.Text, txtbillno.Text, txtbilldate.Text, txtchequeno.Text, txtchequedate.Text, txtalocno.Text, DDlASL.SelectedItem.Text, txtasublocno.Text, txtassestdesc.Text,
                        txtpurchasedate.Text, txtentrydate.Text, txtquantity.Text, TxtValueUnit.Text, Txtpurchase.Text, txtvalason.Text, txtperdep.Text, txtdepno.Text, DDlDep.SelectedItem.Text, txtbookbal.Text, txtclsgbal.Text, "1002", "Modify", Session["MID"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {

                        WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deadstock_Mod _" + txtprdcode.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Book Balance does not match..!!", this.Page);
                    txtbookbal.Text = "";
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {

                if (CB == 0)
                {
                    string AT = DS.GetStage(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                    if (AT == "1003")
                    {
                        if (ViewState["Flag"].ToString() == "DL")
                        {
                            WebMsgBox.Show("Record Already Authorized, You cannot delete !", this.Page);
                            return;
                        }
                    }
                    int Result = DS.DeleteData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text, "1004", "Delete", Session["MID"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deadstock_Delete _" + txtprdcode.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("There is closing balance ,You cannot delete !!", this.Page);
                    ClearData();
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                if (BB == CB)
                {
                    string AT = DS.GetStage(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text);
                    if (AT == "1003")
                    {
                        if (ViewState["Flag"].ToString() == "AT")
                        {
                            WebMsgBox.Show("Record Already Authorized!", this.Page);
                            return;
                        }
                    }
                    int Result = DS.AuthoriseData(Session["BRCD"].ToString(), txtprdcode.Text, txtaccno.Text, "1003", "Autho", Session["MID"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Authorized successfully..!!", this.Page);
                        BindGrid("PA");
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deadstock_Auto _" + txtprdcode.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Book Balance does not match..!!", this.Page);
                    txtbookbal.Text = "";
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

 

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmDeadStockDetails.aspx?Flag=AD", true);
    }
  
    public void ClearData()
    {
        BD.BindStatus(DDLstatusname);
        BD.BindDItem(DDlitemno);
        BD.BindASL(DDlASL);
        BD.BindDep(DDlDep);
        BD.BindSanction(DDlSanction);
        BD.BindTypeofitm(DDltypeofitm);
        txtprdcode.Text = "";
        txtprdname.Text = "";
        txtaccno.Text = "";
        txtaccname.Text = "";
        txtdesc.Text = "";
        txtdate.Text = "";
        txtasstno.Text = "";
        txtasstname.Text = "";
        txtdate.Text = "";
        txtstatusno.Text = "";
        DDLstatusname.SelectedValue = "0";
        txtsanctnno.Text = "";
        DDlSanction.SelectedValue = "0";
        txtvendorname.Text = "";
        txtperiod.Text = "";
        txtitemno.Text = "";
        DDlitemno.SelectedValue = "0";
        txtitemtypeno.Text = "";
        TxtIdCode.Text = "";
        txtbrnchno.Text = "";
        ddlbranchname.SelectedValue = "0";
        txtname.Text = "";
        ddlwaranty.SelectedValue = "0";
        txtamcdetail.Text = "";
        txtvalue.Text = "";
        txtbillno.Text = "";
        txtbilldate.Text = "";
        txtchequeno.Text = "";
        txtchequedate.Text = "";
        txtalocno.Text = "";
        ddlAstLoc.SelectedValue = "0";
        txtasublocno.Text = "";
        DDlASL.SelectedValue = "0";
        txtassestdesc.Text = "";
        txtpurchasedate.Text = "";
       txtquantity.Text = "";
        TxtValueUnit.Text = "";
        Txtpurchase.Text = "";
        txtvalason.Text = "";
        txtperdep.Text = "";
        txtdepno.Text = "";
        DDlDep.SelectedValue = "0";
        txtbookbal.Text="";
        txtclsgbal.Text = "";

    }
    protected void txtdepno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string DName = BD.GetDep(txtdepno.Text);
            if (DName == null)
            {
                WebMsgBox.Show("Dep No is Invalid....!", this.Page);
                txtdepno.Text = "";
                txtdepno.Focus();
            }
            else
            {
                DDlDep.SelectedValue = DName;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallEdit()
    {
        try
        {
            if ((ViewState["Flag"].ToString() == "MD") || (ViewState["Flag"].ToString() == "DL") || (ViewState["Flag"].ToString() == "AT"))
            {
                string res = "";
                DataTable DT = new DataTable();
                DT = DS.GetInfo(Session["BRCD"].ToString(), ViewState["PRDCODE"].ToString(), ViewState["ACCNO"].ToString(), ViewState["Id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtprdcode.Text = DT.Rows[0]["PRDCODE"].ToString();
                    string PDName = customcs.GetProductName(txtprdcode.Text, Session["BRCD"].ToString());
                    if (PDName != null)
                    {
                        txtprdname.Text = PDName;
                    }
                    txtaccno.Text = DT.Rows[0]["ACCNO"].ToString();
                 //res = DS.AccNodisplay(Session["BRCD"].ToString(), txtprdcode.Text);
                 //if (res == "0")
                 //{
                 //    txtaccname.Text = txtprdname.Text;
                 //}
                 //else
                 //{
                         string[] AN;
                        AN = customcs.GetAccountNme(txtaccno.Text, txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                        if (AN != null)
                        {
                            txtaccname.Text = AN[1].ToString();
                 
                        }
                 //}
                 txtdesc.Text = DT.Rows[0]["DESCRIPTION"].ToString();
                    txtasstno.Text = DT.Rows[0]["TYPE_OF_ASSEST_NO"].ToString();
                    txtasstname.Text = DT.Rows[0]["TYPE_OF_ASSEST_DESC"].ToString();
                    txtdate.Text = DT.Rows[0]["DATE"].ToString().Replace("12:00:00 AM", "");
                    txtstatusno.Text = DT.Rows[0]["STATUS_NO"].ToString();
                    DDLstatusname.SelectedItem.Text = DT.Rows[0]["STATUS_DESC"].ToString();
                    txtsanctnno.Text = DT.Rows[0]["SANCTION_NO"].ToString();
                    DDlSanction.SelectedItem.Text = DT.Rows[0]["SANCTION_DESC"].ToString();
                    txtvendorname.Text = DT.Rows[0]["VENDORS_NAME"].ToString();
                    txtperiod.Text = DT.Rows[0]["PERIOD"].ToString();
                    txtitemno.Text = DT.Rows[0]["ITEM_DETAILS_NO"].ToString();
                    DDlitemno.SelectedItem.Text = DT.Rows[0]["ITEM_DETAILS_DESC"].ToString();
                    txtitemtypeno.Text = DT.Rows[0]["TYPE_OF_ITEM_NO"].ToString();
                    DDltypeofitm.SelectedItem.Text = DT.Rows[0]["TYPE_OF_ITEM_DESC"].ToString();
                    TxtIdCode.Text = DT.Rows[0]["ID_CODE"].ToString();
                    txtbrnchno.Text = DT.Rows[0]["BRANCH_DEPT"].ToString();
                    ddlbranchname.SelectedIndex = Convert.ToInt32(DT.Rows[0]["BRANCH_DEPT"].ToString());
                    txtname.Text = DT.Rows[0]["NAME"].ToString();
                    ddlwaranty.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["WARRANTY"].ToString()) ? "0" : DT.Rows[0]["WARRANTY"].ToString();
                    txtamcdetail.Text = DT.Rows[0]["AMC_DETAILS"].ToString();
                    txtvalue.Text = DT.Rows[0]["VALUE"].ToString();
                    txtbillno.Text = DT.Rows[0]["BILL_NO"].ToString();
                    txtbilldate.Text = DT.Rows[0]["BILL_DATE"].ToString().Replace("12:00:00 AM", "");
                    txtchequeno.Text = DT.Rows[0]["CHEQUE_NO"].ToString();
                    txtchequedate.Text = DT.Rows[0]["CHEQUE_DATE"].ToString().Replace("12:00:00 AM", "");
                    txtalocno.Text = DT.Rows[0]["ASSEST_LOC_CODE"].ToString();
                    ddlAstLoc.SelectedIndex = Convert.ToInt32(DT.Rows[0]["ASSEST_LOC_CODE"].ToString());
                    DDlASL.SelectedItem.Text = DT.Rows[0]["ASSEST_SUBLOC_CODE"].ToString();
                    txtasublocno.Text = DT.Rows[0]["ASSLOCNO"].ToString();
                    txtassestdesc.Text = DT.Rows[0]["ASSEST_DESC"].ToString();
                    txtpurchasedate.Text = DT.Rows[0]["PURCHASE_DATE"].ToString().Replace("12:00:00 AM", "");
                    txtentrydate.Text = DT.Rows[0]["ENTRY_DATE"].ToString().Replace("12:00:00 AM", "");
                    txtquantity.Text = DT.Rows[0]["AVBL_QUANTITY"].ToString();
                    TxtValueUnit.Text = DT.Rows[0]["VALUE_PER_UNIT"].ToString();
                    Txtpurchase.Text = DT.Rows[0]["PURCHASE_VALUE"].ToString();
                    txtvalason.Text = DT.Rows[0]["VALUE_ASONDATE"].ToString();
                    txtperdep.Text = DT.Rows[0]["PERDEP"].ToString();
                    txtdepno.Text = DT.Rows[0]["DEPTYPR_NO"].ToString();
                    DDlDep.SelectedItem.Text = DT.Rows[0]["DEPTYPR_DESC"].ToString();
                    txtbookbal.Text = DT.Rows[0]["BOOKBAL"].ToString();
                    txtclsgbal.Text = DT.Rows[0]["CLSGLBAL"].ToString();
                }
                else
                {
                    WebMsgBox.Show("No data found",this.Page);
                    txtprdcode.Text = "";
                    txtprdname.Text = "";
                    txtaccno.Text = "";
                    txtaccname.Text = "";
                    txtprdcode.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlAstLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtalocno.Text = ddlAstLoc.SelectedValue.ToString();
        DDlASL.Focus();
    }
    protected void ddlbranchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbrnchno.Text=ddlbranchname.SelectedValue.ToString();
        txtname.Focus();
    }
  
    public void Enable(bool TF)
    {
        txtprdcode.Enabled = TF;
        txtprdname.Enabled = TF;
            txtaccno.Enabled = TF;
            txtaccname.Enabled = TF;
        txtdesc.Enabled = TF;
        txtasstname.Enabled = TF;
        txtdate.Enabled = TF;
        txtstatusno.Enabled = TF;
        DDLstatusname.Enabled = TF;
        txtsanctnno.Enabled = TF;
        DDlSanction.Enabled = TF;
        txtvendorname.Enabled = TF;
        txtperiod.Enabled = TF;
        txtitemno.Enabled = TF;
        txtentrydate.Enabled = TF;
        DDlitemno.Enabled = TF;
        txtitemtypeno.Enabled = TF;
        DDltypeofitm.Enabled = TF;
        TxtIdCode.Enabled = TF;
        txtbrnchno.Enabled = TF;
       ddlbranchname.Enabled = TF;
        txtname.Enabled = TF;
        ddlwaranty.Enabled = TF;
        txtamcdetail.Enabled = TF;
        txtvalue.Enabled = TF;
        txtbillno.Enabled = TF;
        txtbilldate.Enabled = TF;
        txtchequeno.Enabled = TF;
        txtchequedate.Enabled = TF;
        txtalocno.Enabled = TF;
        ddlAstLoc.Enabled = TF;
        txtasublocno.Enabled = TF;
        DDlASL.Enabled = TF;
        txtassestdesc.Enabled = TF;
        txtpurchasedate.Enabled = TF;
        txtquantity.Enabled = TF;
        TxtValueUnit.Enabled = TF;
        Txtpurchase.Enabled = TF;
        txtvalason.Enabled = TF;
        txtperdep.Enabled = TF;
        txtdepno.Enabled = TF;
        DDlDep.Enabled = TF;
        txtbookbal.Enabled = TF;
        txtclsgbal.Enabled = TF;
  }
    public void subdata()
    {
        try
        {
             DT = DS.GetSubmitData("TOA");
            txtasstno.Text = DT.Rows[0]["SRNO"].ToString();
            txtasstname.Text = DT.Rows[0]["DESCRIPTION"].ToString();
         }
        catch (Exception EX)
        {

            ExceptionLogging.SendErrorToText(EX);
        }

    }
    protected void txtdesc_TextChanged(object sender, EventArgs e)
    {
        DDLstatusname.Focus();
    }
    protected void txtvendorname_TextChanged(object sender, EventArgs e)
    {
        txtperiod.Focus();
    }
    protected void txtperiod_TextChanged(object sender, EventArgs e)
    {
        DDlitemno.Focus();
    }
    protected void TxtIdCode_TextChanged(object sender, EventArgs e)
    {
        ddlbranchname.Focus();
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        ddlwaranty.Focus();
    }
    protected void txtvalue_TextChanged(object sender, EventArgs e)
    {
        txtbillno.Focus();
    }
    protected void txtbilldate_TextChanged(object sender, EventArgs e)
    {
        txtchequeno.Focus();
    }
    protected void txtbillno_TextChanged(object sender, EventArgs e)
    {
        txtbilldate.Focus();
    }
    protected void txtchequeno_TextChanged(object sender, EventArgs e)
    {
        txtchequedate.Focus();
    }

    protected void txtassestdesc_TextChanged(object sender, EventArgs e)
    {
        txtpurchasedate.Focus();
    }
  
    protected void txtasublocno_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            string ASLName = BD.GetASL(txtasublocno.Text);
            if (ASLName == null)
            {
                WebMsgBox.Show("Assest Sub Location is Invalid....!", this.Page);
                txtasublocno.Text = "";
                txtasublocno.Focus();
            }
            else
            {
                DDlASL.SelectedValue = ASLName;
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        
    }
    protected void txtquantity_TextChanged(object sender, EventArgs e)
    {
        TxtValueUnit.Focus();
    }
    protected void TxtValueUnit_TextChanged(object sender, EventArgs e)
    {
        Txtpurchase.Focus();
    }
    protected void Txtpurchase_TextChanged(object sender, EventArgs e)
    {
        txtvalason.Focus();
    }
    protected void txtvalason_TextChanged(object sender, EventArgs e)
    {
        txtperdep.Focus();
    }
    protected void txtperdep_TextChanged(object sender, EventArgs e)
    {
        DDlDep.Focus();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            txtprdcode.Focus();
            ViewState["Flag"] = "AD";
            BtnSubmit.Visible = true;
            Flag = "1";
            TblDiv_MainWindow.Visible = true;
            Div_grid.Visible = true;
}
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdDead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdDead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdDead_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["ACCNO"] = ARR[2].ToString();
            ViewState["Flag"] = "MD";
            BtnSubmit.Text = "Modify";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            EnableMod(false);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void EnableMod(bool TF)
    {
        txtprdcode.Enabled = TF;
        txtprdname.Enabled = TF;
        txtaccno.Enabled = TF;
        txtaccname.Enabled = TF;
    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["ACCNO"] = ARR[2].ToString();
            ViewState["Flag"] = "AT";
            BtnSubmit.Text = "Authorise";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["ACCNO"] = ARR[2].ToString();
            ViewState["Flag"] = "DL";
            BtnSubmit.Text = "Delete";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDLstatusname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtstatusno.Text = BD.GetNOStatus(DDLstatusname.SelectedItem.Text);
            DDlSanction.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDlitemno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtitemno.Text = BD.GetNOItemD(DDlitemno.SelectedItem.Text);
            DDltypeofitm.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDlASL_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtasublocno.Text = BD.GetNOASL(DDlASL.SelectedItem.Text);
            txtassestdesc.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDlDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtdepno.Text = BD.GetNODep(DDlDep.SelectedItem.Text);
            txtbookbal.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDlSanction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtsanctnno.Text = BD.GetNOSanction(DDlSanction.SelectedItem.Text);
            txtvendorname.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDltypeofitm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtitemtypeno.Text = BD.GetNOTypeofitm(DDltypeofitm.SelectedItem.Text);
            TxtIdCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}