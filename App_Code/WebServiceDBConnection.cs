using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
 
public class WebServiceDBConnection
{

    public SqlConnection sqlCon()
    {
        string strCustId = System.Configuration.ConfigurationManager.AppSettings["CustId"];
        string strURL = "customer.staffpanel.in";// System.Configuration.ConfigurationManager.AppSettings["URL"];

        ConnectionParam obj = null;

        string strMasterCon = "";
        string strUserId="";
        string strPassword="";
        string strServerName="";
        string strDBName="";

        try
        {
            string strDBDetails = HttpContext.Current.Application["DBDetails"].ToString();
            strDBDetails = System.Configuration.ConfigurationManager.AppSettings["db"];
            
            //  strDBDetails = @"AMRUTA-PC\SQLEXPRESS|sa|mnbv@1234|EmpPayroll";
            //  strDBDetails = @"103.27.86.197|sa|admin^it1008|StaffPan_AadityaAnagha";
            //  strDBDetails = @"103.27.86.197|sa|admin^it1008|StaffPan_NirmalUjjwal";
            //  strDBDetails = @"103.27.86.197|sa|admin^it1008|StaffPan_YavatmalUrban";

            if (strDBDetails == "")
            {

                string jsonString = GET("http://" + strURL + "/RestJSONService.svc/GetJsonData/" + strCustId);
                jsonString = jsonString.Replace('[', ' ').Replace(']', ' ');

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ConnectionParam));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                obj = (ConnectionParam)ser.ReadObject(stream);

                HttpContext.Current.Application["DBDetails"] =  obj.ServerIP + "|" + obj.UserID + "|" + obj.Password + "|" + obj.DatabaseName ;

                strUserId = obj.UserID;
                strPassword = obj.Password;
                strServerName = obj.ServerIP;
                strDBName = obj.DatabaseName;
            }
            else
            {
                String[] strArr = strDBDetails.Split('|');          
                strServerName = strArr[0];
                strUserId = strArr[1];
                strPassword = strArr[2];
                strDBName = strArr[3];

                HttpContext.Current.Application["DBDetails"] = strServerName + "|" + strUserId + "|" + strPassword + "|" + strDBName;
            }
        }
        catch
        {

            string strDBDetails = HttpContext.Current.Application["DBDetails"].ToString();

            if (strDBDetails == "")
            {


                String[] strPathArr = System.Web.HttpContext.Current.Request.RawUrl.Split('/');
                string strFilePath = "";
                for (int i = 0; i < strPathArr.Length - 3; i++)
                {
                    strFilePath = strFilePath + "../";
                }

                System.Data.DataTable dt = null;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(strFilePath + "XML/connection.xml");

                XmlReader xmlFile = null;
                xmlFile = XmlReader.Create(strPath, new XmlReaderSettings());
                DataSet ds = new DataSet();
                ds.ReadXml(xmlFile, XmlReadMode.InferTypedSchema);
                //dv = new DataView(ds.Tables[0]);
                dt = ds.Tables[0];

                String[] strArr = strCustId.Split('_');

                DataRow[] dataRows = dt.Select(" sdcid = " + strArr[0]);

                if (dataRows.Length > 0)
                {
                    foreach (DataRow rows in dataRows)
                    {
                        strUserId = rows["SqlUserID"].ToString();
                        strPassword = rows["SqlPassword"].ToString();
                        strServerName = rows["ServerName"].ToString();
                        strDBName = rows["SqlDBName"].ToString();

                        HttpContext.Current.Application["DBDetails"] = strServerName + "|" + strUserId + "|" + strPassword + "|" + strDBName;
                    }
                }

            }
            else
            {
                String[] strArr = strDBDetails.Split('|');
                strServerName = strArr[0];
                strUserId = strArr[1];
                strPassword = strArr[2];
                strDBName = strArr[3];
                HttpContext.Current.Application["DBDetails"] = strServerName + "|" + strUserId + "|" + strPassword + "|" + strDBName;
            }
        }



        strMasterCon = "User ID=" + strUserId;
        strMasterCon = strMasterCon + ";password=" + strPassword;
        strMasterCon = strMasterCon + ";data source=" + strServerName;
        strMasterCon = strMasterCon + ";persist security info=False";
        strMasterCon = strMasterCon + ";initial catalog=" + strDBName;
        strMasterCon = strMasterCon + ";Connect Timeout=1200";
        return new SqlConnection(strMasterCon);
    }

    public class ConnectionParam
    {
        public string CustId { get; set; }
        public string CustName { get; set; }
        public string DatabaseName { get; set; }
        public string DbId { get; set; }
        public string Password { get; set; }
        public string ServerIP { get; set; }
        public string ServerName { get; set; }
        public string UserID { get; set; }
    }

    string GET(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = errorResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                String errorText = reader.ReadToEnd();
                // log errorText
            }
            throw;
        }
    }

}