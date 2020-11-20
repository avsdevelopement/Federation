using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.UI.WebControls;

public partial class FrmCashBook : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsCashBook CB = new ClsCashBook();
    ClsDayBook DBook = new ClsDayBook();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //batch file download

        /* string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
         string pathDownload = Path.Combine(pathUser, "Downloads");

         location.Value = pathDownload;
  
        */

        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDT.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDT.Text = Session["EntryDate"].ToString();
        }
        //Another_Click();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashBk_Grd" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void GrdCashBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void BindGrid()
    {
        try
        {
            CB.GetCashBook(GrdCashBook, Session["BRCD"].ToString(), TxtFDT.Text, TxtTDT.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void GrdCashBook_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "RECIPT";
                HeaderCell2.ColumnSpan = 4;
                HeaderRow.Cells.Add(HeaderCell2);
                HeaderCell2.VerticalAlign = VerticalAlign.Middle;

                HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "PAYMENT";
                HeaderCell2.ColumnSpan = 4;
                //HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                GrdCashBook.Controls[0].Controls.AddAt(0, HeaderRow);

                GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderRow1.Style["font-weight"] = "bold";

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Sub Gl";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Account No";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Account Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Amount";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Sub Gl";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Account No";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Account Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Amount";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderRow.Attributes.Add("class", "header");
                HeaderRow1.Attributes.Add("class", "header");
                //FooterRow.Attributes.Add("class", "footer");
                GrdCashBook.Controls[0].Controls.AddAt(1, HeaderRow1);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        //HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);

        //Process proc = null;
        //string filepath = Server.MapPath("~/Reports/VoucherSubBook_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".bat");

        //System.Diagnostics.Process.Start(filepath); //Provided the name and path is correctloseMainWindow())
        ////    p.Kill();
        /* string batDir = "C:/AVS";
         proc = new Process();
         proc.StartInfo.WorkingDirectory = batDir;
         proc.StartInfo.FileName = "C:/AVS/CashBookPrint.bat";
         proc.StartInfo.CreateNoWindow = false;
         proc.Start();
         proc.WaitForExit();
         * 
         * 
         */
        DataTable DT = new DataTable();

        try
        {
            if (Rdeatils.SelectedValue == "1")
            {
                DT = CB.CreateCB(TxtFDT.Text, TxtTDT.Text, Session["BRCD"].ToString());
                TextFile(DT);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                DT = CB.CreateCBSumry(TxtFDT.Text, TxtTDT.Text, Session["BRCD"].ToString());
                TextFile2(DT);
            }

            /* string Path = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
            if (File.Exists(Path))
                File.Delete(Path);
            */
            if (TxtRptName.Text == "")
            {
                TxtRptName.Text = "p";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + TxtRptName.Text + ".txt");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
            string txt = "";

            /*string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            string location="";
            location += "start notepad.exe /p " + pathDownload + "/" + TxtRptName.Text + ".txt";
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(location);
                writer.Close();
                Response.Output.Write(writer);
            }
          
            */

            if (TxtRptName.Text == "")
            {
                TxtRptName.Text = "p";
            }
            if (DT.Rows.Count > 0)
            {
                double OB = DBook.GetOpening(Session["BRCD"].ToString(), "OP", TxtFDT.Text);
                double CB = DBook.GetOpening(Session["BRCD"].ToString(), "CL", TxtFDT.Text);
                txt = "\r\n " + SBPageHeader1(Session["BankName"].ToString(), Session["BName"].ToString(), TxtFDT.Text, TxtTDT.Text, OB.ToString());
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                string CBAL = "0";
                string DBAL = "0";
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    txt = "|" + (i + 1).ToString().PadRight(6).Substring(0, 6) + "|" + DT.Rows[i]["Csubgl"].ToString().PadRight(9).Substring(0, 9);
                    txt += "|" + DT.Rows[i]["CACCNAME"].ToString().PadRight(33).Substring(0, 33);
                    txt += "|" + DT.Rows[i]["CAMOUNT"].ToString().PadLeft(15).Substring(0, 15);
                    txt += "|" + DT.Rows[i]["Dsubgl"].ToString().PadRight(9).Substring(0, 9);
                    txt += "|" + DT.Rows[i]["DACCNAME"].ToString().PadRight(33).Substring(0, 33);
                    txt += "|" + DT.Rows[i]["DAMOUNT"].ToString().PadLeft(15).Substring(0, 15);
                    txt += "|".PadRight(15).Substring(0, 15) + "|";
                    // txt += "|" + DT.Rows[i]["DTOKENO"].ToString().PadRight(14).Substring(0, 14) + "|";
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(txt);
                        writer.Close();
                        Response.Output.Write(writer);
                    }
                    CBAL = (Convert.ToDouble(CBAL) + Convert.ToDouble(DT.Rows[i]["CAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["CAMOUNT"].ToString())).ToString();
                    DBAL = (Convert.ToDouble(DBAL) + Convert.ToDouble(DT.Rows[i]["DAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["DAMOUNT"].ToString())).ToString();
                }
                string CTotal = "0";
                string DTotal = "0";
                CTotal = (Convert.ToDouble(OB) + Convert.ToDouble(CBAL)).ToString();
                DTotal = (Convert.ToDouble(CB) + Convert.ToDouble(DBAL)).ToString();
                txt = "|------|---------|---------------------------------|---------------|---------|---------------------------------|---------------|--------------|\r\n";
                txt += "|      |         |                                 |               |         | Closing Cash                    |" + CB.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|--------------------------------------------------|---------------|-------------------------------------------|---------------|--------------|\r\n";
                txt += "| Total Receipt                                    |" + CBAL.ToString().PadLeft(15).Substring(0, 15) + "|  Total Payment:                           |" + DBAL.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|--------------------------------------------------|---------------|-------------------------------------------|---------------|--------------|\r\n";
                txt += "|  Grand Total                                     |" + CTotal.ToString().PadLeft(15).Substring(0, 15) + "|  Grand Total                              |" + DTotal.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|---------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                /*string Path1 = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".bat");
                if (File.Exists(Path1))
                    File.Delete(Path1);

                txt = "Print " + Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
                using (StreamWriter writer = new StreamWriter(Path1, true))
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    //Response.Output.Write(writer);
                }*/
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.SuppressContent = true;
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                // String template = File.ReadAllText(Server.MapPath("~/Reports/Daybook_Report_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt"));
                /* Response.Write("<script type='text/javascript'>");
                 Response.Write("window.open('Reports/" + TxtRptName.Text +"_"+ Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt','_blank');");
                 Response.Write("</script>");*/
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Print()", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }


    }
    protected void Print_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashBk_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (Rdeatils.SelectedValue == "1")
            {
                string RedirectUrl = "FrmRView.aspx?FDate=" + TxtFDT.Text + "&TDate=" + TxtTDT.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCashBook.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + RedirectUrl + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string RedirectUrl = "FrmRView.aspx?FDate=" + TxtFDT.Text + "&TDate=" + TxtTDT.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptcashBookSummary.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + RedirectUrl + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "3")
            {
                string RedirectUrl = "FrmRView.aspx?FDate=" + TxtFDT.Text + "&TDate=" + TxtTDT.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCashBook_ALLDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + RedirectUrl + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnView_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable DT = new DataTable();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashBk_Text" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (Rdeatils.SelectedValue == "1")
            {
                DT = CB.CreateCB(TxtFDT.Text, TxtTDT.Text, Session["BRCD"].ToString());
                TextFile(DT);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                DT = CB.CreateCBSumry(TxtFDT.Text, TxtTDT.Text, Session["BRCD"].ToString());
                TextFile2(DT);
            }


        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void TextFile(DataTable DT)
    {
        try
        {

            /*string Path = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
            if (File.Exists(Path))
                File.Delete(Path);
            */
            if (TxtRptName.Text == "")
            {
                TxtRptName.Text = "p";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + TxtRptName.Text + ".txt");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
            string txt = "";




            if (DT.Rows.Count > 0)
            {
                double OB = DBook.GetOpening(Session["BRCD"].ToString(), "OP", TxtFDT.Text);
                double CB = DBook.GetOpening(Session["BRCD"].ToString(), "CL", TxtFDT.Text);
                txt = "\r\n " + SBPageHeader(Session["BankName"].ToString(), Session["BName"].ToString(), TxtFDT.Text, TxtTDT.Text, OB.ToString());
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                string CBAL = "0";
                string DBAL = "0";
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    txt = "|" + (i + 1).ToString().PadRight(6).Substring(0, 6) + "|" + DT.Rows[i]["Csubgl"].ToString().PadRight(12).Substring(0, 12);
                    txt += "|" + DT.Rows[i]["CAACNO"].ToString().PadRight(14).Substring(0, 14) + "|" + DT.Rows[i]["CSETNO"].ToString().PadRight(8).Substring(0, 8);
                    txt += "|" + (DT.Rows[i]["CAACNO"].ToString() == "0" ? DT.Rows[i]["CGlName"].ToString() : DT.Rows[i]["CACCNAME"].ToString()).ToString().PadRight(32).Substring(0, 32) + "|" + DT.Rows[i]["CAMOUNT"].ToString().PadLeft(14).Substring(0, 14);
                    txt += "|" + DT.Rows[i]["Dsubgl"].ToString().PadRight(12).Substring(0, 12);
                    txt += "|" + DT.Rows[i]["DAACNO"].ToString().PadRight(14).Substring(0, 14) + "|" + DT.Rows[i]["DSETNO"].ToString().PadRight(8).Substring(0, 8);
                    txt += "|" + (DT.Rows[i]["DAACNO"].ToString() == "0" ? DT.Rows[i]["DGlName"].ToString() : DT.Rows[i]["DACCNAME"].ToString()).ToString().PadRight(32).Substring(0, 32) + "|" + DT.Rows[i]["DAMOUNT"].ToString().PadLeft(14).Substring(0, 14);
                    //+ DT.Rows[i]["DTOKENO"].ToString().PadLeft(14).Substring(0, 14) + "|"; In test mamco column is present. -- marina 05/07/2018
                    txt += "|".PadRight(15).Substring(0, 15) + "|";

                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(txt);
                        writer.Close();
                        Response.Output.Write(writer);
                    } CBAL = (Convert.ToDouble(CBAL) + Convert.ToDouble(DT.Rows[i]["CAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["CAMOUNT"].ToString())).ToString();
                    DBAL = (Convert.ToDouble(DBAL) + Convert.ToDouble(DT.Rows[i]["DAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["DAMOUNT"].ToString())).ToString();
                }
                string CTotal = "0";
                string DTotal = "0";
                CTotal = (Convert.ToDouble(OB) + Convert.ToDouble(CBAL)).ToString();
                DTotal = (Convert.ToDouble(CB) + Convert.ToDouble(DBAL)).ToString();
                txt = "|------|------------|--------------|--------|--------------------------------|--------------|------------|--------------|--------|--------------------------------|--------------|--------------|\r\n";
                txt += "|      |            |              |        |                                |              |            |              |        | Closing Cash                   |" + CB.ToString().PadLeft(14).Substring(0, 14) + "|              |\r\n";
                txt += "|----------------------------------------------------------------------------|--------------|---------------------------------------------------------------------|--------------|--------------|\r\n";
                txt += "| Total Receipt                                                              |" + CBAL.ToString().PadLeft(14).Substring(0, 14) + "|  Total Payment:                                                     |" + DBAL.ToString().PadLeft(14).Substring(0, 14) + "|              |\r\n";
                txt += "|----------------------------------------------------------------------------|--------------|---------------------------------------------------------------------|--------------|--------------|\r\n";
                txt += "|  Grand Total                                                               |" + CTotal.ToString().PadLeft(14).Substring(0, 14) + "|  Grand Total                                                        |" + DTotal.ToString().PadLeft(14).Substring(0, 14) + "|              |\r\n";
                txt += "|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                /*  string Path1 = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".bat");
                  if (File.Exists(Path1))
                      File.Delete(Path1);

                  txt = "Print " + Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
                  using (StreamWriter writer = new StreamWriter(Path1, true))
                  {
                      writer.WriteLine(txt);
                      writer.Close();
                      //Response.Output.Write(writer);
                  }*/
                /* //HttpContext.Current.Response.Flush();
                 //HttpContext.Current.Response.SuppressContent = true;
                 //HttpContext.Current.ApplicationInstance.CompleteRequest();
                 // String template = File.ReadAllText(Server.MapPath("~/Reports/Daybook_Report_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt"));
                 Response.Write("<script type='text/javascript'>");
                 Response.Write("window.open('Reports/" + TxtRptName.Text +"_"+ Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt','_blank');");
                 Response.Write("</script>");
                */


                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {

    }
    protected string SBPageHeader(string BankName, string BrName, string FrDate, string TDate, string OB)
    {
        string sQuery = "", str1 = "-";

        try
        {
            string RptName = "Cash Book Report From " + FrDate + " To " + TDate;
            sQuery =
            " " + str1.ToString().PadLeft(191, '-') + " \r\n" +
            "| Bank Name    : " + (BankName.ToString().PadRight(101).Substring(0, 101)).ToString() + " Print Date   : " + DateTime.Now.Date.ToString("dd/MM/yyyy").ToString().PadRight(57).Substring(0, 57) + " |\r\n" +
            "| Branch Name  : " + (BrName.ToString().PadRight(101).Substring(0, 101)).ToString() + " Print UserID  : " + Session["UserName"].ToString().PadRight(57).Substring(0, 57) + "|\r\n" +
            "| Report Name  : " + RptName.ToString().PadRight(175).Substring(0, 175) + "|\r\n" +
            "|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|                                         Receipt                                           |                                                Payment                                            |\r\n" +
            "|-------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------|\r\n" +
            "|SR.NO.|  A/C Type  |    A/C No    | Set No | A/C Name                       |    Amount    |  A/C Type  |    A/C No    | Set No | A/C Name                       |    Amount    |   Token No   |\r\n" +
            "|------|------------|--------------|--------|--------------------------------|--------------|------------|--------------|--------|--------------------------------|--------------|--------------|\r\n" +
            "|      |            |              |        | Opening Cash                   |" + OB.ToString().PadLeft(14).Substring(0, 14) + "|            |              |        |                                |              |              |\r\n" +
            "|------|------------|--------------|--------|--------------------------------|--------------|------------|--------------|--------|--------------------------------|--------------|--------------|";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sQuery;
    }
    public void TextFile2(DataTable DT)
    {
        try
        {



            /* string Path = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
            if (File.Exists(Path))
                File.Delete(Path);
            */
            if (TxtRptName.Text == "")
            {
                TxtRptName.Text = "p";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + TxtRptName.Text + ".txt");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
            string txt = "";


            /*string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            string location="";
            location += "start notepad.exe /p " + pathDownload + "/" + TxtRptName.Text + ".txt";
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(location);
                writer.Close();
                Response.Output.Write(writer);
            }
          
            */

            if (TxtRptName.Text == "")
            {
                TxtRptName.Text = "p";
            }
            if (DT.Rows.Count > 0)
            {
                double OB = DBook.GetOpening(Session["BRCD"].ToString(), "OP", TxtFDT.Text);
                double CB = DBook.GetOpening(Session["BRCD"].ToString(), "CL", TxtFDT.Text);
                txt = "\r\n " + SBPageHeader1(Session["BankName"].ToString(), Session["BName"].ToString(), TxtFDT.Text, TxtTDT.Text, OB.ToString());
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                string CBAL = "0";
                string DBAL = "0";
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    txt = "|" + (i + 1).ToString().PadRight(6).Substring(0, 6) + "|" + DT.Rows[i]["Csubgl"].ToString().PadRight(9).Substring(0, 9);
                    txt += "|" + DT.Rows[i]["CACCNAME"].ToString().PadRight(33).Substring(0, 33);
                    txt += "|" + DT.Rows[i]["CAMOUNT"].ToString().PadLeft(15).Substring(0, 15);
                    txt += "|" + DT.Rows[i]["Dsubgl"].ToString().PadRight(9).Substring(0, 9);
                    txt += "|" + DT.Rows[i]["DACCNAME"].ToString().PadRight(33).Substring(0, 33);
                    txt += "|" + DT.Rows[i]["DAMOUNT"].ToString().PadLeft(15).Substring(0, 15);
                    txt += "|".PadRight(15).Substring(0, 15) + "|";
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(txt);
                        writer.Close();
                        Response.Output.Write(writer);
                    }
                    CBAL = (Convert.ToDouble(CBAL) + Convert.ToDouble(DT.Rows[i]["CAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["CAMOUNT"].ToString())).ToString();
                    DBAL = (Convert.ToDouble(DBAL) + Convert.ToDouble(DT.Rows[i]["DAMOUNT"].ToString() == "" ? "0" : DT.Rows[i]["DAMOUNT"].ToString())).ToString();
                }
                string CTotal = "0";
                string DTotal = "0";
                CTotal = (Convert.ToDouble(OB) + Convert.ToDouble(CBAL)).ToString();
                DTotal = (Convert.ToDouble(CB) + Convert.ToDouble(DBAL)).ToString();
                txt = "|------|---------|---------------------------------|---------------|---------|---------------------------------|---------------|--------------|\r\n";
                txt += "|      |         |                                 |               |         | Closing Cash                    |" + CB.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|--------------------------------------------------|---------------|-------------------------------------------|---------------|--------------|\r\n";
                txt += "| Total Receipt                                    |" + CBAL.ToString().PadLeft(15).Substring(0, 15) + "|  Total Payment:                           |" + DBAL.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|--------------------------------------------------|---------------|-------------------------------------------|---------------|--------------|\r\n";
                txt += "|  Grand Total                                     |" + CTotal.ToString().PadLeft(15).Substring(0, 15) + "|  Grand Total                              |" + DTotal.ToString().PadLeft(15).Substring(0, 15) + "|              |\r\n";
                txt += "|---------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    Response.Output.Write(writer);
                }
                /*string Path1 = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".bat");
                if (File.Exists(Path1))
                    File.Delete(Path1);

                txt = "Print " + Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt");
                using (StreamWriter writer = new StreamWriter(Path1, true))
                {
                    writer.WriteLine(txt);
                    writer.Close();
                    //Response.Output.Write(writer);
                }*/
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.SuppressContent = true;
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                // String template = File.ReadAllText(Server.MapPath("~/Reports/Daybook_Report_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt"));
                /* Response.Write("<script type='text/javascript'>");
                 Response.Write("window.open('Reports/" + TxtRptName.Text +"_"+ Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".txt','_blank');");
                 Response.Write("</script>");*/
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected string SBPageHeader1(string BankName, string BrName, string FrDate, string TDate, string OB)
    {
        string sQuery = "", str1 = "-";

        try
        {
            string RptName = "Cash Book Summary Report From " + FrDate + " To " + TDate;
            sQuery =
            " " + str1.ToString().PadLeft(141, '-') + " \r\n" +
            "| Bank Name    : " + (BankName.ToString().PadRight(70).Substring(0, 70)).ToString() + " Print Date   : " + DateTime.Now.Date.ToString("dd/MM/yyyy").ToString().PadRight(38).Substring(0, 38) + " |\r\n" +
            "| Branch Name  : " + (BrName.ToString().PadRight(70).Substring(0, 70)).ToString() + " Print UserID  : " + Session["UserName"].ToString().PadRight(38).Substring(0, 38) + "|\r\n" +
            "| Report Name  : " + RptName.ToString().PadRight(125).Substring(0, 125) + "|\r\n" +
            "|---------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|                                 Receipt                          |                         Payment                                          |\r\n" +
            "|------------------------------------------------------------------|--------------------------------------------------------------------------|\r\n" +
            "|SR.NO.| Product |         Product Name            |    Amount     | Product |         Product Name            |    Amount     |   Token No   |\r\n" +
            "|------|---------|---------------------------------|---------------|---------|---------------------------------|---------------|--------------|\r\n" +
            "|      |         |Opening Cash                     |" + OB.ToString().PadLeft(15).Substring(0, 15) + "|         |                                 |               |              |\r\n" +
            "|------|---------|---------------------------------|---------------|---------|---------------------------------|---------------|--------------|";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sQuery;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string filepath = Server.MapPath("~/Reports/" + TxtRptName.Text + "_" + Session["BRCD"].ToString() + "_" + Session["MID"].ToString() + ".bat");

            System.Diagnostics.Process.Start(filepath); //Provided the name and path is correctloseMainWindow())
            ////    p.Kill();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Another_Click()
    {
        try
        {
            string txt = "";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=p.bat");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");

            txt += "start notepad.exe /p " + pathDownload + "/" + TxtRptName.Text + ".txt";
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(txt);
                writer.Close();
                Response.Output.Write(writer);
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();


            ////    p.Kill();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}