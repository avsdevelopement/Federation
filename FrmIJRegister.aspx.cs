using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.IO.Compression;
using Ionic.Zip;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web.Util;

public partial class FrmIJRegister : System.Web.UI.Page
{
    ClsAccopen accop = new ClsAccopen();
    ClsIJRegister IJ = new ClsIJRegister();
    string[] cname, cname1, cname2;
    string ACC1, ACC2, PWD = "";
    DataTable dt = new DataTable();
    string redirectURL = "";
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

                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + "4" + "_" + "4";
                AutoCompleteExtender1.ContextKey = Session["BRCD"].ToString() + "_" + "4" + "_" + "4";
                AutoCompleteExtender2.ContextKey = Session["BRCD"].ToString() + "_" + "4" + "_" + "4";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void RblIJReg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RblIJReg.SelectedValue == "0")
            {
                if (RblType.SelectedValue == "0")
                {
                    if (TxtMemNo.Text == "")
                    {
                        WebMsgBox.Show("Please enter account number..!!", this.Page);
                        return;
                    }
                    ACC1 = TxtMemNo.Text;
                    ACC2 = "";
                }
                else if (RblType.SelectedValue == "1")
                {
                    if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
                    {
                        WebMsgBox.Show("Please enter account number..!!", this.Page);
                        return;
                    }
                    ACC1 = TxtFMemNo.Text;
                    ACC2 = TxtTMemNo.Text;
                }
                redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptIRegister.rdlc";
                // redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptIJReg.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (RblIJReg.SelectedValue == "1")
            {
                if (RblType.SelectedValue == "0")
                {
                    if (TxtMemNo.Text == "")
                    {
                        WebMsgBox.Show("Please enter account number..!!", this.Page);
                        return;
                    }
                    ACC1 = TxtMemNo.Text;
                    ACC2 = "";
                }
                else if (RblType.SelectedValue == "1")
                {
                    if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
                    {
                        WebMsgBox.Show("Please enter account number..!!", this.Page);
                        return;
                    }
                    ACC1 = TxtFMemNo.Text;
                    ACC2 = TxtTMemNo.Text;
                }
                redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptJRegister.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cname = accop.Getcustname_SHR(TxtMemNo.Text.ToString()).Split('_');

            TxtMemName.Text = cname[0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        try
        {
            TxtMemNo.Text = "";
            TxtMemName.Text = "";
            TxtFMemNo.Text = "";
            TxtFMemName.Text = "";
            TxtTMemNo.Text = "";
            TxtTMemName.Text = "";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void RblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RblType.SelectedValue == "0")
            {
                Div_FT.Visible = false;
                TxtMemNo.Visible = true;
                DIV_MEMNAME.Visible = true;
            }
            if (RblType.SelectedValue == "1")
            {
                Div_FT.Visible = true;
                TxtMemNo.Visible = false;
                DIV_MEMNAME.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtTMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cname1 = accop.Getcustname_SHR (TxtTMemNo.Text.ToString()).Split('_');

            TxtTMemName.Text = cname1[0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtFMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cname2 = accop.Getcustname_SHR(TxtFMemNo.Text.ToString()).Split('_');

            TxtFMemName.Text = cname2[0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public string GetReportData()
    {
        int FileCount = 0;
        try
        {

            string contentType = string.Empty;
            contentType = "application/pdf";
            DataSet thisDataSet = GetIR(Session["BRCD"].ToString(),TxtMemNo.Text);

            if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
            {
                WebMsgBox.Show("Sorry No Record found......!!", this.Page);

            }
            else
            {
                DataTable DTAC = new DataTable();
                int ArrIndex = 0;
                DTAC = thisDataSet.Tables[0];
                DataTable DataT = DTAC.Clone();
                string[] AccArray = new string[1000000];

                for (int i = 0; i < DTAC.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        ArrIndex = GetArrayElementIndex(AccArray);

                        if (AccArray[ArrIndex - 1].ToString() != DTAC.Rows[i]["ACCNO"].ToString())
                        {
                            AccArray[ArrIndex] = DTAC.Rows[i]["ACCNO"].ToString();
                        }
                        else
                        {
                            ArrIndex = GetArrayElementIndex(AccArray);
                        }
                    }
                    else
                    {
                        AccArray[i] = DTAC.Rows[i]["ACCNO"].ToString();

                    }

                }

                int Count = 0;
                Count = GetArrayElementIndex(AccArray);
                int CountAcc = 0;
                int DTACCount = 0;
                int OrgCountRows = (DTAC.Rows.Count) - 1;
                int FOrgCountRows = OrgCountRows;
                string folderName = @"c:\I Register\";

                foreach (DataRow dr in DTAC.Rows)
                {
                    if (DTAC.Rows[FOrgCountRows - OrgCountRows]["ACCNO"].ToString() == AccArray[CountAcc].ToString())
                    {
                        DataT.ImportRow(dr);
                        if ((FOrgCountRows - OrgCountRows) != (DTAC.Rows.Count) - 1)
                            OrgCountRows--;
                        DTACCount++;
                    }

                    if (DTAC.Rows[FOrgCountRows - OrgCountRows]["ACCNO"].ToString() != AccArray[CountAcc].ToString())
                    {
                        string fileName = "_" + AccArray[CountAcc] + ".pdf";
                        //PWD = DataT.Rows[0]["CalcAccID_PW"].ToString() != null ? DataT.Rows[0]["CalcAccID_PW"].ToString() : "101010";
                        CountAcc++;
                        string extension;
                        string encoding;
                        string mimeType;
                        string[] streams;
                        Warning[] warnings;

                        LocalReport report = new LocalReport();
                        report.ReportPath = Server.MapPath("~/RptIRegister.rdlc");
                        ReportDataSource rds = new ReportDataSource();
                        rds.Name = "ReportDS";
                        rds.Value = DataT;
                        report.DataSources.Add(rds);

                        byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  



                        if (!System.IO.File.Exists(folderName))
                        {
                            using (System.IO.MemoryStream input = new System.IO.MemoryStream(mybytes))
                            {
                                using (System.IO.MemoryStream output = new System.IO.MemoryStream())
                                {
                                    PdfReader reader = new PdfReader(input);
                                    PdfEncryptor.Encrypt(reader, output, true, PWD, PWD, PdfWriter.ALLOW_SCREENREADERS);
                                    mybytes = output.ToArray();
                                    
                                    using (FileStream fs = File.Create(folderName + fileName)) //ANKITA c:\\targetfolder\
                                    {
                                        fs.Write(mybytes, 0, mybytes.Length);
                                    }
                                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                    Response.BinaryWrite(mybytes);

                                }
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("File  already exists.", this.Page);

                        }

                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = contentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.WriteFile(folderName + fileName);

                       DataT.Clear();


                    }
                }
                string fileName1 = "_" + AccArray[CountAcc] + ".pdf";
                CountAcc++;
                string extension1;
                string encoding1;
                string mimeType1;
                string[] streams1;
                Warning[] warnings1;
                //PWD = DataT.Rows[0]["CalcAccID_PW"].ToString() != null ? DataT.Rows[0]["CalcAccID_PW"].ToString() : "101010";

                LocalReport report1 = new LocalReport();
                report1.ReportPath = Server.MapPath("~/RptIRegister.rdlc");
                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = "ReportDS";//This refers to the dataset name in the RDLC file  
                rds1.Value = DataT;
                report1.DataSources.Add(rds1);

                byte[] mybytes1 = report1.Render("PDF", null, out extension1, out encoding1, out mimeType1, out streams1, out warnings1); //for exporting to PDF  


                if (!System.IO.File.Exists(folderName))
                {
                    using (System.IO.MemoryStream input = new System.IO.MemoryStream(mybytes1))
                    {
                        using (System.IO.MemoryStream output = new System.IO.MemoryStream())
                        {
                            PdfReader reader = new PdfReader(input);
                            PdfEncryptor.Encrypt(reader, output, true, PWD, PWD, PdfWriter.ALLOW_SCREENREADERS);
                            mybytes1 = output.ToArray();
                            using (FileStream fs = File.Create(folderName + fileName1)) //ANKITA c:\\targetfolder\
                            {
                                fs.Write(mybytes1, 0, mybytes1.Length);
                            }
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(mybytes1);

                        }
                    }
                }
                else
                {
                    WebMsgBox.Show("File  already exists.", this.Page);

                }

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = contentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName1);
                Response.WriteFile(folderName + fileName1);

                 DataT.Clear();
                FileCount = Directory.EnumerateFiles(@"c:\I Register\", "*.pdf").Count();
              

            }
        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        return Convert.ToString(FileCount);
    }
    public DataSet GetIR(string brcd, string accno)////Added by ankita on 06/07/2017 to display IJRegister
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IJ.BindIR(brcd, TxtFMemNo.Text,TxtTMemNo.Text);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }
    public int GetArrayElementIndex(string[] Arr)
    {
        int cc = 0;
        try
        {
            for (int c = 0; c < Arr.Length; c++)
            {
                if (Arr[c] != null)
                {
                    cc++;
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return cc;

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (RblIJReg.SelectedValue == "0")
        {
            if (RblType.SelectedValue == "0")
            {
                if (TxtMemNo.Text == "")
                {
                    WebMsgBox.Show("Please enter account number..!!", this.Page);
                    return;
                }
                ACC1 = TxtMemNo.Text;
                ACC2 = "";
            }
            else if (RblType.SelectedValue == "1")
            {
                if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
                {
                    WebMsgBox.Show("Please enter account number..!!", this.Page);
                    return;
                }
                ACC1 = TxtFMemNo.Text;
                ACC2 = TxtTMemNo.Text;
            }
            redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptIRegister.rdlc";
            // redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptIJReg.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        if (RblIJReg.SelectedValue == "1")
        {
            if (RblType.SelectedValue == "0")
            {
                if (TxtMemNo.Text == "")
                {
                    WebMsgBox.Show("Please enter account number..!!", this.Page);
                    return;
                }
                ACC1 = TxtMemNo.Text;
                ACC2 = "";
            }
            else if (RblType.SelectedValue == "1")
            {
                if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
                {
                    WebMsgBox.Show("Please enter account number..!!", this.Page);
                    return;
                }
                ACC1 = TxtFMemNo.Text;
                ACC2 = TxtTMemNo.Text;
            }
            redirectURL = "FrmRView.aspx?&brcd=" + Session["BRCD"].ToString() + "&accno=" + ACC1 + "&taccno=" + ACC2 + "&rptname=RptJRegister.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
    }
}