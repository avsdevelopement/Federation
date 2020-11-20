using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class FrmCheckbook : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    Clspayorder pay = new Clspayorder();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit_Click(object sender, EventArgs e)
    {

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {

    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {

    }
    protected void txtset_TextChanged(object sender, EventArgs e)
    {
        try
        {
            dt = pay.GetInfo(txtpay.Text, Session["BRCD"].ToString(), txtset.Text);
            txtpayamt.Text = dt.Rows[0]["CREDIT"].ToString();
            txtcstno.Text = dt.Rows[0]["CUSTNO"].ToString();
            txtnam.Text = dt.Rows[0]["CUSTNAME"].ToString();
            txtaccno.Text = dt.Rows[0]["ACCNO"].ToString();
            txtissuenam.Text = dt.Rows[0]["CUSTNAME"].ToString();
            txtpaynam.Text = ConvertNumbertoWords(Convert.ToInt32(txtpayamt.Text));
            //txtrefno.Text = pay.Getrefno(txtrefno.Text, Session["BRCD"].ToString());
            ENDN(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtpayamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string word = ConvertNumbertoWords(Convert.ToInt32(txtpayamt.Text));
            txtpaynam.Text = word;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public static string ConvertNumbertoWords(int number)
    {
        string words = "";
        try
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));           
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
        }
        catch (Exception Ex)        
        {
            ExceptionLogging.SendErrorToText(Ex);
            ////Response.Redirect("FrmLogin.aspx", true);
        }
        return words;
    }

    protected void btnissue_Click(object sender, EventArgs e)
    {
        try
        {
            string ponumber = txtpay.Text.ToString();
            string brcd = Session["BRCD"].ToString();
            string setno = txtset.Text.ToString();
            string payamt = txtpayamt.Text.ToString();
            string payamtnam = txtpaynam.Text.ToString();
            //string acctype
            //string accname
            string custno = txtcstno.Text.ToString();
            string accno = txtaccno.Text.ToString();
            string name = txtnam.Text.ToString();
            string issuename = txtissuenam.Text.ToString();



            int RC = pay.insert(ponumber, brcd, setno, payamt, payamtnam, custno, accno, name, issuename);
            if (RC > 0)
            {
                WebMsgBox.Show(" Record Successfully Added", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }


    public void ENDN(bool TF)
    {


        txtpayamt.Enabled = TF;
        txtpaynam.Enabled = TF;
        txtcstno.Enabled = TF;
        txtaccno.Enabled = TF;
        txtnam.Enabled = TF;
        txtissuenam.Enabled = TF;


    }
}