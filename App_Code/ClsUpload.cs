using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.UI;

    public class ClsUpload
    {
        DbConnection conn = new DbConnection();
        ClsAVSReconcilation objcomm = new ClsAVSReconcilation();
        //Excel.Application xlapp;
        //Excel.Workbook xlwbk;
        //Excel.Worksheet xlsht;
        DataTable dt;
        DataSet ds;


        public string ImportDataFromExcel(string FilePath, string Extension, string isHDR, string MID,string EntryDate)
        {
            string ConStr = "";
            string ans = "";
            DataTable DT = new DataTable();
            try
            {
                switch (Extension.ToLower())
                {
                    case ".xls": //Excel 97-03
                        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                        break;

                    case ".xlsx": //Excel 07
                        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        break;
                    default:
                        return "Only Excel File can you upload!!!";     
                }
              
                ConStr = String.Format(ConStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(ConStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmdExcel.Connection = connExcel;
                connExcel.Open();

                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cmdExcel.CommandText = "Select * From [" + SheetName + "]"; //[Sheet1$]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(DT);
                connExcel.Close();
                DataRow Drow;
                DataTable dtUploadFiles = new DataTable();
                dtUploadFiles = objcomm.getTable(FLAG: "STRUCTURE",MID:"");
                if (DT.Columns.Count != 7)
                {
                 return "Invalid File Format!!!";                    
                }

                if (conn.ContainColumn(DT,dtUploadFiles) == false)
                {
                    return "Invalid File Format!!!";
                }

                for (int i = 0; i < DT.Rows.Count; i++)
                {

                    for (int j = 0; j < 1; j++)
                    {
                        Drow = dtUploadFiles.NewRow();
                        if (DT.Rows[i]["SRNO"].ToString() != "") Drow["SRNO"] = DT.Rows[i]["SRNO"].ToString();
                        if (DT.Rows[i]["TRANSACTIONDATE"].ToString() != "") Drow["TRANSACTIONDATE"] = DT.Rows[i]["TRANSACTIONDATE"].ToString();
                        if (DT.Rows[i]["INSTRUMENTNO"].ToString() != "") Drow["INSTRUMENTNO"] = DT.Rows[i]["INSTRUMENTNO"].ToString();
                        if (DT.Rows[i]["PARTICULARS"].ToString() != "") Drow["PARTICULARS"] = DT.Rows[i]["PARTICULARS"].ToString();
                        if (DT.Rows[i]["CREDIT"].ToString() != "") Drow["CREDIT"] = DT.Rows[i]["CREDIT"].ToString();
                        if (DT.Rows[i]["DEBIT"].ToString() != "") Drow["DEBIT"] = DT.Rows[i]["DEBIT"].ToString();
                        if (DT.Rows[i]["BALANCE"].ToString() != "") Drow["BALANCE"] = DT.Rows[i]["BALANCE"].ToString();
                        
                        Drow["STAGE"] = "1001";
                        Drow["MID"] = MID;//Session["MID"].ToString();
                        Drow["MIDDTTM"] = EntryDate;//Session["EntryDate"].ToString()
                        Drow["STATUS"] = "1";
                        dtUploadFiles.Rows.Add(Drow);
                    }

                }


                if (dtUploadFiles.Rows.Count > 0)
                {
                    objcomm.getTable(FLAG: "DELETERECORDS", MID: MID);
                    if (conn.fBulkCopy(dtUploadFiles, "AVS_RECONCILATION") == true)
                        ans = "uploaded successfully!!!";
                    else
                        ans = "uploading Fail!!!";
                }
                else
                    ans = "Data not available for upload in file!!!";
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ans;
        }


        public string CreateExcelFile(System.Web.HttpResponse Response, DataTable dt, string FileName)
        {
            string ans = "Download Fail!!!";
            try
            {
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
                Response.ContentType = "application/vnd.ms-excel"; //For xls
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                ans = "Download Successfully!!!";
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ans;
        }


    }