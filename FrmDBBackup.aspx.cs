using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SqlClient;

public partial class FrmDBBackup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common objcommon = new Common();
            DatabaseManager.tblAttributes objAttr = new DatabaseManager.tblAttributes();
          
            ProcessDirectory();
        }
    }

    
    protected void btnDatabaseBackup_Click(object sender, EventArgs e)
    {
        try
        {
            string strPath = Server.MapPath("Uploads/DBBackup/");
            string strDBDetails = System.Configuration.ConfigurationManager.AppSettings["db"];
            String[] strArr = strDBDetails.Split('|');
            string strname = strPath + "YSPM" + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Month + "_" + DateTime.Now.Second + ".bak";

            DatabaseManager.tblAttributes objAttr = new DatabaseManager.tblAttributes();
            Common objCommon = new Common();

            String[] temp = { strname, strArr[3] };

            objAttr.resizeArray(temp);
            objAttr.strProc = "BackupDatabase";
            string intMaxId = objCommon.SaveUpdateDeleteWithoutTrans(ref objAttr);

            if (intMaxId == "1")
            {
                lblStatus.Text = " Backup Success";
                ProcessDirectory();

            }
            else
            {
                lblStatus.Text = "Server Error ";
            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = "Server Error " + ex.Message;
        }
        //DBBackup
    }

    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        try
        {
            string remoteUri = System.Configuration.ConfigurationManager.AppSettings["uplFilePath"] + "/Uploads/DBBackup/";

            string strPath = Server.MapPath("Uploads/DBBackup/");

            string fileName = ddlDownload.SelectedItem.Text, myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            myStringWebResource = remoteUri + fileName;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.

            string strSavePath = getDownloadFolderPath() + "\\" + fileName;
            myWebClient.DownloadFile(myStringWebResource, getDownloadFolderPath() + "/" + fileName);
            lblStatus.Text = "Download Success ";


            OpenFolder(strSavePath);
            //System.IO.File.Open(getDownloadFolderPath() + "/" + fileName, FileMode.Open);
        }
        catch (Exception ex)
        {

            lblStatus.Text = "Download Fail " + ex.Message;
        }
    }

    public void OpenFolder(string filePath)
    {
        Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
    }

    string getDownloadFolderPath()
    {
        return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
    }

    void ProcessDirectory()
    {
        ddlDownload.Items.Clear();

        string targetDirectory = Server.MapPath("Uploads/DBBackup");
        // Process the list of files found in the directory. 
        string[] fileEntries = Directory.GetFiles(targetDirectory);


        foreach (string fileName in fileEntries)
        {
            if (fileName != "vssver2.scc" && !fileName.Contains("vssver"))
            {
                String[] strArr = fileName.Split('\\');
                ddlDownload.Items.Add(strArr[strArr.Length - 1]);
            }
        }

    }

 

  
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (ddlDownload.SelectedIndex != -1)
        {
            string strPath = Server.MapPath("Uploads/DBBackup/") + ddlDownload.SelectedItem.Text;
            if (System.IO.File.Exists(strPath))
            {
                System.IO.File.Delete(strPath);
                ProcessDirectory();
                lblStatus.Text = "Remove Success ";
            }

        }
    }
    
}