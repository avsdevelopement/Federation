using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCashPayAuthDo : System.Web.UI.Page
{
    string setno;
    ClsCashPayAuthDo CurrentCls = new ClsCashPayAuthDo();
    DataTable dt = new DataTable();
    ClsAuthorized Auth = new ClsAuthorized();
    int result;

    protected void Page_Load(object sender, EventArgs e)
    {
        string BRCD = Session["BRCD"].ToString();
        setno = Request.QueryString["setno"];
        string allow = Request.QueryString["allow"];
        if (!IsPostBack)
        {
            try
            {
                dt = CurrentCls.Getformdata(Session["BRCD"].ToString(), Request.QueryString["setno"].ToString());
                if (dt.Rows.Count > 0)
                {
                    if (allow == "NO")
                    {
                        btnSubmit.Enabled = false;
                    }
                    TxtEntrydate.Text = Convert.ToDateTime(dt.Rows[0]["ENTRYDATE"]).ToString("dd-MM-yyyy");
                    txtsetno.Text = setno;
                    TxtProcode.Text = dt.Rows[0]["SUBGLCODE"].ToString();
                    TxtProName.Text = dt.Rows[0]["ACCOUNTNAME"].ToString();
                    TxtAccNo.Text = dt.Rows[0]["ACCNO"].ToString();
                    TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                    txtnaration.Text = dt.Rows[0]["PARTICULARS"].ToString();
                    txtnaration1.Text = dt.Rows[0]["PARTICULARS2"].ToString();
                    txtamountt.Text = dt.Rows[0]["AMOUNT"].ToString();
                    TxtEntrydate.Focus();
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //Response.Redirect("FrmLogin.aspx", true);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // Get Table name for funding from EntryDate
            string getdt = TxtEntrydate.Text.ToString();
            string[] datearray = getdt.Split('-');
            string TBNAME = datearray[2].ToString() + datearray[1].ToString();

            // Authorize Record
            result = CurrentCls.AuthorizeCash(txtsetno.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());

            // Insert record AVSM_******
            int ResultInsert = CurrentCls.fundingCash(Session["BRCD"].ToString(), txtsetno.Text.ToString(), TBNAME);

            // Insert record
            //dt = CurrentCls.fundingCash(Session["BRCD"].ToString(), txtsetno.Text.ToString());

            //if (dt.Rows.Count > 0)
            //{
            //    //Auth.Authorized(TBNAME, Convert.ToDateTime(dt.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy"), Convert.ToDateTime(dt.Rows[0]["POSTINGDATE"]).ToString("dd/MM/yyyy"), Convert.ToDateTime(dt.Rows[0]["FUNDINGDATE"]).ToString("dd/MM/yyyy"), dt.Rows[0]["GLCODE"].ToString(), dt.Rows[0]["SUBGLCODE"].ToString(), dt.Rows[0]["ACCNO"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["PARTICULARS2"].ToString(), dt.Rows[0]["CREDIT"].ToString(), "1", dt.Rows[0]["ACTIVITY"].ToString(), dt.Rows[0]["PMTMODE"].ToString(), dt.Rows[0]["SETNO"].ToString(), dt.Rows[0]["SCROLLNO"].ToString(), dt.Rows[0]["INSTRUMENTNO"].ToString(), "01/01/1900", dt.Rows[0]["INSTBANKCD"].ToString(), dt.Rows[0]["INSTBRCD"].ToString(), dt.Rows[0]["STAGE"].ToString(), "01/01/1900", dt.Rows[0]["BRCD"].ToString(), dt.Rows[0]["MID"].ToString(), dt.Rows[0]["CID"].ToString(), dt.Rows[0]["VID"].ToString(), dt.Rows[0]["PCMAC"].ToString(), dt.Rows[0]["PAYMAST"].ToString(), dt.Rows[0]["CUSTNO"].ToString(), dt.Rows[0]["CUSTNAME"].ToString(), dt.Rows[0]["REFID"].ToString());
            //    //Auth.Authorized(TBNAME, Convert.ToDateTime(dt.Rows[1]["ENTRYDATE"]).ToString("dd/MM/yyyy"), Convert.ToDateTime(dt.Rows[1]["POSTINGDATE"]).ToString("dd/MM/yyyy"), Convert.ToDateTime(dt.Rows[1]["FUNDINGDATE"]).ToString("dd/MM/yyyy"), dt.Rows[1]["GLCODE"].ToString(), dt.Rows[1]["SUBGLCODE"].ToString(), dt.Rows[1]["ACCNO"].ToString(), dt.Rows[1]["PARTICULARS"].ToString(), dt.Rows[1]["PARTICULARS2"].ToString(), dt.Rows[1]["DEBIT"].ToString(), "2", dt.Rows[1]["ACTIVITY"].ToString(), dt.Rows[1]["PMTMODE"].ToString(), dt.Rows[1]["SETNO"].ToString(), dt.Rows[1]["SCROLLNO"].ToString(), dt.Rows[1]["INSTRUMENTNO"].ToString(), "01/01/1900", dt.Rows[1]["INSTBANKCD"].ToString(), dt.Rows[1]["INSTBRCD"].ToString(), dt.Rows[1]["STAGE"].ToString(), "01/01/1900", dt.Rows[1]["BRCD"].ToString(), dt.Rows[1]["MID"].ToString(), dt.Rows[1]["CID"].ToString(), dt.Rows[1]["VID"].ToString(), dt.Rows[1]["PCMAC"].ToString(), dt.Rows[1]["PAYMAST"].ToString(), dt.Rows[1]["CUSTNO"].ToString(), dt.Rows[1]["CUSTNAME"].ToString(), dt.Rows[1]["REFID"].ToString());
            //}

            // dt = CurrentCls.fundingCash(Session["BRCD"].ToString(), setno.ToString());

            if (result > 0)
            {
                WebMsgBox.Show("Record Authorized successfully", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
}