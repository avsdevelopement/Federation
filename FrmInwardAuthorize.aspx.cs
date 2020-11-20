using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInwardAuthorize : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsInwardUpload IU = new ClsInwardUpload();
    ClsAuthorized AA = new ClsAuthorized();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void txtbankcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtbankcd.Text == "")
            {
            
                goto ext;
            }
            txtbnkdname.Text = customcs.GetBankName(txtbankcd.Text);
           // AutoBranch.ContextKey = txtbankcd.Text;
            if (txtbnkdname.Text == "" & txtbankcd.Text != "")
            {
                WebMsgBox.Show("Please enter valid bank code", this.Page);
                txtbankcd.Text = "";
                txtbankcd.Focus();
            }
            else
            {
               // txtbrnchcd.Focus();
            }
        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
  
    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
    }

    protected void Authorize_Click(object sender, EventArgs e)
    {
        try
        {
           
                if (RdbAutho.Checked == true)
                {

                    BindToClear();
                }
                else if (RdbNonAutho.Checked == true)
                {
                    BindToReturn();
                }

           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }
    protected void BindToClear()
    {
        try
        {
            IU.GetInwardProcess(GrdAN, Session["EntryDate"].ToString(), TxtINWDate.Text, Session["BRCD"].ToString(), "AUTHO");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BindToReturn()
    {
        try
        {
            IU.GetInwardProcess(GrdAN, Session["EntryDate"].ToString(), TxtINWDate.Text, Session["BRCD"].ToString(), "NONAUTHO");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtbnkdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string BKNAME = txtbnkdname.Text;
            string[] bknob = BKNAME.Split('_');
            if (bknob.Length > 1)
            {
                txtbnkdname.Text = bknob[0].ToString();
                txtbankcd.Text = (string.IsNullOrEmpty(bknob[1].ToString()) ? "" : bknob[1].ToString());
               // ViewState["CUSTNO"] = custnob[2].ToString();
                string[] TD = Session["EntryDate"].ToString().Split('/');
               // txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
               // txtnaration1.Focus();
            }
            else
            {
                //lblMessage.Text = "Invalid Account Number.........!!";
               // ModalPopup.Show(this.Page);
                WebMsgBox.Show("Enter Valid Bank Name", this.Page);
                txtbnkdname.Text = "";
                txtbankcd.Text = "";
                txtbankcd.Focus();
                return;
            }
        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void LnkAutho_Click(object sender, EventArgs e)
    //{
    //    //Result = AT.Authorized(DT.Rows[K]["ENTRYDATE"].ToString(), DT.Rows[K]["ENTRYDATE"].ToString(), Edate.ToString(), DT.Rows[K]["GLCODE"].ToString(), DT.Rows[K]["PRDUCT_CODE"].ToString(), DT.Rows[K]["ACC_NO"].ToString(), DT.Rows[K]["PARTICULARS"].ToString(), DT.Rows[K]["PARTICULARS"].ToString(), DT.Rows[K]["INSTRU_AMOUNT"].ToString(), DT.Rows[K]["CD"].ToString() == "D" ? 2.ToString() : 1.ToString(), "51", PMTMODE, DT.Rows[K]["SET_NO"].ToString(), DT.Rows[K]["INSTRU_NO"].ToString(), DT.Rows[K]["INSTRUDATE"].ToString(), DT.Rows[K]["BANK_CODE"].ToString(), DT.Rows[K]["BRANCH_CODE"].ToString(), DT.Rows[K]["STAGE"].ToString(), DateTime.Now.Date.ToString("dd/MM/yyyy"), DT.Rows[K]["BRCD"].ToString(), DT.Rows[K]["MID"].ToString(), DT.Rows[K]["CID"].ToString(), DT.Rows[K]["VID"].ToString(), "CASH", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["CUSTNAME"].ToString(), "0000");
    //    try
    //    {
                     
    //        int Result=0;
               
    //        string SetNo;
            
    //        SetNo= BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo").ToString();

    //        Result=AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, subglcode, accno, "", "", amount, "2", "1", "By CLG", SetNo, instno, instd, "0", "0", "1003", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "0",custno, cname, "0");
    //        if (Result > 0)
    //        {
    //            // int resultout = CurrentCls.UpdateSet(setno, Session["BRCD"].ToString());
    //            // SG 06\10\16
    //            int resultout = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", SetNo);

    //            if (Result > 0)
    //            {
    //                //lblMessage.Text = "Record Submitted Successfully Woth Recipt No :" + SetNo;
    //                WebMsgBox.Show(" Record Submitted with No : " + SetNo, this.Page);
    //                //ModalPopup.Show(this.Page);
    //            }
                
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //    }
    //}
    protected void GrdAN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void GrdAN_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string accno = (GrdAN.SelectedRow.FindControl("ACCNO") as Label).Text;
            string subglcode = (GrdAN.SelectedRow.FindControl("SUBGLCODE") as Label).Text;
            //string glcode = (GrdAN.SelectedRow.FindControl("GLCODE") as Label).Text;
            string amount = (GrdAN.SelectedRow.FindControl("IBAL") as Label).Text;
            string instno = (GrdAN.SelectedRow.FindControl("INSTNO") as Label).Text;
            string instd = (GrdAN.SelectedRow.FindControl("INSTD") as Label).Text;
            string cname = (GrdAN.SelectedRow.FindControl("ANAME") as Label).Text;
            string custno = (GrdAN.SelectedRow.FindControl("CUSTNO") as Label).Text;
            string gl="";
            int Result = 0;

            string SetNo;
            if (ViewState["FL"].ToString() == "A")
            {
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                //int resultout = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", SetNo);

                gl = BD.GetAccTypeGL(subglcode, Session["BRCD"].ToString());
                string[] glc = gl.Split('_');
                string glcode = glc[1].ToString();
                Result = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, subglcode, accno, "", "", amount, "2", "1", "By CLG", SetNo, instno, instd, "0", "0", "1003", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "0", custno, cname, "0", "0");
                if (Result > 0)
                {
                    // int resultout = CurrentCls.UpdateSet(setno, Session["BRCD"].ToString());
                    // SG 06\10\16

                    if (Result > 0)
                    {
                        //lblMessage.Text = "Record Submitted Successfully Woth Recipt No :" + SetNo;
                        WebMsgBox.Show(" Record Submitted with No : " + SetNo, this.Page);
                        //ModalPopup.Show(this.Page);
                    }

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkVerify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string custno = objlink.CommandArgument;
            ViewState["FL"] = "V";
            ViewState["CUSTNO"] = custno;
            string url = "FrmVerifySign.aspx?CUSTNO=" + custno + "";
            NewWindows(url);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void LnkAutho_Click(object sender, EventArgs e)
    {
        ViewState["FL"] = "A";
    }
}