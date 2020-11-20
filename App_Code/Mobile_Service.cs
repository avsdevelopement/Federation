using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Mobile_Service : System.Web.Services.WebService {
    string MessageResponce,sql,sql1;
    DbConnection con = new DbConnection();
    SqlCommand CMD;
    SqlDataAdapter Adapt;
    DataTable DT;

    public Mobile_Service () {
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
   

    //  Added By Amol on 31/07/2017
    public string Send_SMS(string CustNo, string EntryDate)
    {

        System.Net.WebRequest WebReq;
        System.Net.WebResponse WebRes;
        string mno = "";
        string Server = "";
        string UserName = "";
        string Password = "";
        string source = "";
        string MobileNo = "";
        string URL = "";
        string responce = "";
        string responcestatus = "";
        string SMSClient = "";
        string WebResponseString = "";
        int Type = 0;
        string Message = "";
        string BKCD = "";
        int K = 0;

        sql = "Select Mobile, SMS_Description From AVS1092 Where SMS_STATUS = 1 and Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And CustNo = '" + CustNo + "'";
        try
        {
            Adapt = new SqlDataAdapter(sql, con.GetDBConnection());

            DT = new DataTable();
            Adapt.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                sql = "Select BANKCD From BankName Where BRCD = '" + Session["BRCD"].ToString() + "'";
                BKCD = con.sExecuteScalar(sql);

                if (BKCD != "1002")
                {
                    Type = 0;
                }

                DataTable dtPara = new DataTable();
                sql = "Select ListField, ListValue From PARAMETER Where ListField = 'SMSUID' Union Select ListField, ListValue From PARAMETER Where ListField='SMSPWD' Union Select ListField, ListValue From PARAMETER Where ListField='SMSSENDER' Union Select ListField, ListValue From PARAMETER Where ListField='SMSLINK'  Union Select ListField, ListValue From PARAMETER Where ListField='SMSClient'";
                dtPara = con.GetDatatable(sql);
                WebReq = null;
                WebRes = null;
                for (int a = 0; a < dtPara.Rows.Count; a++)
                {
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSUID")
                        UserName = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSPWD")
                        Password = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSSENDER")
                        source = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSLINK")
                        Server = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSClient")
                        SMSClient = dtPara.Rows[a]["ListValue"].ToString();
                }
                for (K = 0; K <= DT.Rows.Count - 1; K++)
                {
                    Message = DT.Rows[K]["SMS_Description"].ToString();

                    //For convert message into unicode here
                    //Message = System.Net.WebUtility.UrlEncode(Message);
                    //if (Type == 2)
                    //{
                    //    Message = ConvertToUnicode(Message);
                    //}

                    mno = DT.Rows[K]["Mobile"].ToString().Trim();
                    if (mno.Length == 10)
                    {
                    }
                    else
                    {
                        goto a;
                    }

                    WebResponseString = "";

                    if (SMSClient == "Palghar")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + MobileNo + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1";
                    }

                    if (SMSClient == "Business")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "ID=" + UserName + "&Pwd=" + Password + "&PhNo=" + MobileNo + "&Text=" + Message.ToString()+"";
                    }
                    else if (SMSClient == "RouteSms")
                    {
                        MobileNo = "%2b" + DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "destination=" + MobileNo + "&source=" + source + "&message=" + Message.ToString() + "&username=" + UserName + "&password=" + Password + "&type=" + Type;
                    }
                    else if (SMSClient == "AdMark")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "clientid=" + UserName + "&apikey=" + Password + "&msisdn=" + MobileNo + "&sid=YSHPAT&msg=" + Message.ToString() + "&fl=0&gwid=2";
                    }
                    else if (SMSClient == "TubeLight")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "username=" + UserName + "&password=" + Password + "&type=Text&sender=" + source + "&mobile=" + MobileNo + "&message=" + Message.ToString();
                        
                    }

                    WebReq = System.Net.HttpWebRequest.Create(URL);
                    WebReq.Timeout = 25000;

                    //Code for getting response from web
                    WebRequest myreq = (WebRequest)WebRequest.Create(URL);
                    WebResponse myres = (WebResponse)myreq.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(myres.GetResponseStream());
                    WebResponseString = reader.ReadToEnd();
                    myres.Close();
                    //Display Response.                

                    responce = WebResponseString.ToString();
                   // responce = "0";
                    if (BKCD != "1002") //For AVS Link
                    {
                        responcestatus = responce.Substring(0, 4);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And CUSTNO='" + CustNo + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And CUSTNO='" + CustNo + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                    else    
                    {
                        //For YASHOMANDIR Link
                        responcestatus = responce.Substring(14, 3);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And CUSTNO='" + CustNo + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And CUSTNO='" + CustNo + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                a:
                    continue;
                }
            }
            return responcestatus;
        }
        catch (Exception ex)
        {
            return null;
        }
        return responcestatus;
    }


    public string Send_SMSALL(string CustNo, string Message, string MobileNo, string messageType = "0")
    {
        SmsModule SMS = new SmsModule();

        System.Net.WebRequest WebReq;
        System.Net.WebResponse WebRes;
        string mno = "";
        string Server = "";
        string UserName = "";
        string Password = "";
        string source = "";

        string URL = "";
        string responce = "";
        string responcestatus = "";
        string SMSClient = "";
        string WebResponseString = "";

        

        string BKCD = "";
        int K = 0;


        try
        {


            sql = "Select BANKCD From BankName Where BRCD = '" + Session["BRCD"].ToString() + "'";
            BKCD = con.sExecuteScalar(sql);

            DataTable dtPara = new DataTable();
            sql = "Select ListField, ListValue From PARAMETER Where ListField = 'SMSUID' Union Select ListField, ListValue From PARAMETER Where ListField='SMSPWD' Union Select ListField, ListValue From PARAMETER Where ListField='SMSSENDER' Union Select ListField, ListValue From PARAMETER Where ListField='SMSLINK'  Union Select ListField, ListValue From PARAMETER Where ListField='SMSClient'";
            dtPara = con.GetDatatable(sql);
            WebReq = null;
            WebRes = null;
            for (int a = 0; a < dtPara.Rows.Count; a++)
            {
                if (dtPara.Rows[a]["ListField"].ToString() == "SMSUID")
                    UserName = dtPara.Rows[a]["ListValue"].ToString();
                if (dtPara.Rows[a]["ListField"].ToString() == "SMSPWD")
                    Password = dtPara.Rows[a]["ListValue"].ToString();
                if (dtPara.Rows[a]["ListField"].ToString() == "SMSSENDER")
                    source = dtPara.Rows[a]["ListValue"].ToString();
                if (dtPara.Rows[a]["ListField"].ToString() == "SMSLINK")
                    Server = dtPara.Rows[a]["ListValue"].ToString();
                if (dtPara.Rows[a]["ListField"].ToString() == "SMSClient")
                    SMSClient = dtPara.Rows[a]["ListValue"].ToString();
            }

        


            WebResponseString = "";

            if (SMSClient == "Palghar")
            {
                  MobileNo = DT.Rows[K]["Mobile"].ToString();
                //  MobileNo="8485058488";
                URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + MobileNo + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1";
            }
            else if (SMSClient == "RouteSms")
            {
             
                if (messageType.Equals("2"))
                {
                    URL = Server + "destination=" + MobileNo + "&source=" + source + "&message=" + SMS.ConvertToUnicode(Message) + "&username=" + UserName + "&password=" + Password + "&type=" + messageType;

                }
                else
                {
                    URL = Server + "destination=" + MobileNo + "&source=" + source + "&message=" + Message.ToString() + "&username=" + UserName + "&password=" + Password + "&type=" + messageType;

                }
            }

            if (SMSClient == "Business")
            {
              //  MobileNo = DT.Rows[K]["Mobile"].ToString();
                URL = Server + "ID=" + UserName + "&Pwd=" + Password + "&PhNo=" + MobileNo + "&Text=" + Message.ToString() + "";
            }
            else if (SMSClient == "AdMark")
            {
                 MobileNo = DT.Rows[K]["Mobile"].ToString();
             //   MobileNo = "8485058488";
                URL = Server + "clientid=" + UserName + "&apikey=" + Password + "&msisdn=" + MobileNo + "&sid=YSHPAT&msg=" + Message.ToString() + "&fl=0&gwid=2";
            }
            else if (SMSClient == "TubeLight")
            {
                MobileNo = DT.Rows[K]["Mobile"].ToString();
               // MobileNo = "8485058488";
                URL = Server + "username=" + UserName + "&password=" + Password + "&type=Text&sender=" + source + "&mobile=" + MobileNo + "&message=" + Message.ToString();

            }

            WebReq = System.Net.HttpWebRequest.Create(URL);
            WebReq.Timeout = 25000;

            //Code for getting response from web
            WebRequest myreq = (WebRequest)WebRequest.Create(URL);
            WebResponse myres = (WebResponse)myreq.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(myres.GetResponseStream());
            WebResponseString = reader.ReadToEnd();
            myres.Close();
            //Display Response.                

            responce = WebResponseString.ToString();



            // responce = "0";
            if (BKCD != "1002") //For AVS Link
            {
                responcestatus = responce.Substring(0, 4);

            }
            else
            {
                //For YASHOMANDIR Link
                responcestatus = responce.Substring(14, 3);

            }





        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return responcestatus;
    }

   

    public string ConvertToUnicode(string str)
    {
        byte[] ArrayOFBytes = System.Text.Encoding.Unicode.GetBytes(str);
        string UnicodeString = "";
        int v = 0;
        for (v = 0; v <= ArrayOFBytes.Length - 1; v++)
        {
            if (v % 2 == 0)
            {
                int t = ArrayOFBytes[v];
                ArrayOFBytes[v] = ArrayOFBytes[v + 1];
                ArrayOFBytes[v + 1] = Convert.ToByte(t);
            }
        }
        string c = BitConverter.ToString(ArrayOFBytes);
        c = c.Replace("-", "");
        UnicodeString = UnicodeString + c;
        return UnicodeString;
    }

    public string  Send_TrailSMS(string Mobileno, string EntryDate)
    {

        System.Net.WebRequest WebReq;
        System.Net.WebResponse WebRes;
        string mno = "";
        string Server = "";
        string UserName = "";
        string Password = "";
        string source = "";
        string MobileNo = "";
        string URL = "";
        string responce = "";
        string responcestatus = "";
        string SMSClient = "";
        string WebResponseString = "";
        int Type = 0;
        string Message = "";
        string BKCD = "";
        int K = 0;

        sql = "Select Mobile, SMS_Description From AVS1092 Where SMS_STATUS = 1 and Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And Mobile = '" + Mobileno + "'";
        try
        {
            Adapt = new SqlDataAdapter(sql, con.GetDBConnection());

            DT = new DataTable();
            Adapt.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                sql = "Select BANKCD From BankName Where BRCD = '" + Session["BRCD"].ToString() + "'";
                BKCD = con.sExecuteScalar(sql);

                if (BKCD != "1002")
                {
                    Type = 0;
                }

                DataTable dtPara = new DataTable();
                sql = "Select ListField, ListValue From PARAMETER Where ListField = 'SMSUID' Union Select ListField, ListValue From PARAMETER Where ListField='SMSPWD' Union Select ListField, ListValue From PARAMETER Where ListField='SMSSENDER' Union Select ListField, ListValue From PARAMETER Where ListField='SMSLINK' Union Select ListField, ListValue From PARAMETER Where ListField='SMSClient'";
                dtPara = con.GetDatatable(sql);
                WebReq = null;
                WebRes = null;
                for (int a = 0; a < dtPara.Rows.Count; a++)
                {
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSUID")
                        UserName = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSPWD")
                        Password = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSSENDER")
                        source = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSLINK")
                        Server = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSClient")
                        SMSClient = dtPara.Rows[a]["ListValue"].ToString();
                }
                for (K = 0; K <= DT.Rows.Count - 1; K++)
                {
                    Message = DT.Rows[K]["SMS_Description"].ToString();

                    //For convert message into unicode here
                    //Message = System.Net.WebUtility.UrlEncode(Message);
                    //if (Type == 2)
                    //{
                    //    Message = ConvertToUnicode(Message);
                    //}

                    mno = DT.Rows[K]["Mobile"].ToString().Trim();
                    if (mno.Length == 10)
                    {
                    }
                    else
                    {
                        goto a;
                    }

                    WebResponseString = "";

         
                    if (SMSClient == "Palghar")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + MobileNo + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1";
                    }
                    else if (SMSClient == "RouteSms")
                    {
                        MobileNo = "%2b" + DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "destination=" + MobileNo + "&source=" + source + "&message=" + Message.ToString() + "&username=" + UserName + "&password=" + Password + "&type=" + Type;
                    }
                    else if (SMSClient == "AdMark")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "clientid=" + UserName + "&apikey=" + Password + "&msisdn=" + MobileNo + "&sid=YSHPAT&msg=" + Message.ToString() + "&fl=0&gwid=2";
                    }
                    else if (SMSClient == "TubeLight")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "username=" + UserName + "&password=" + Password + "&type=Text&sender=" + source + "&mobile=" + MobileNo + "&message=" + Message.ToString();

                    }

                    WebReq = System.Net.HttpWebRequest.Create(URL);
                    WebReq.Timeout = 25000;

                    //Code for getting response from web
                    WebRequest myreq = (WebRequest)WebRequest.Create(URL);
                    WebResponse myres = (WebResponse)myreq.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(myres.GetResponseStream());
                    WebResponseString = reader.ReadToEnd();
                    myres.Close();
                    //Display Response.                

                    responce = WebResponseString.ToString();
                    //responce = "0";
                    if (BKCD != "1002") //For AVS Link
                    {
                        responcestatus = responce.Substring(0, 4);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And Mobile='" + Mobileno+ "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And Mobile='" + Mobileno + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                    else
                    {
                        //For YASHOMANDIR Link
                        responcestatus = responce.Substring(14, 3);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And Mobile='" + Mobileno + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And Mobile='" + Mobileno + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                a:
                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return responcestatus;
    }

    //Added by Amruta 06/09/2017
    public string Send_DCSMS(string BRCD,string EntryDate)
    {

        System.Net.WebRequest WebReq;
        System.Net.WebResponse WebRes;
        string mno = "";
        string Server = "";
        string UserName = "";
        string Password = "";
        string source = "";
        string MobileNo = "";
        string URL = "";
        string responce = "";
        string responcestatus = "";
        string SMSClient = "";
        string WebResponseString = "";
        int Type = 0;
        string Message = "";
        string BKCD = "";
        int K = 0;

        sql = "Select Mobile, SMS_Description,CustNo From AVS1092 Where SMS_STATUS = 1 and Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And BRCD = '" + BRCD + "' and SMS_TYPE='C'";
        try
        {
            Adapt = new SqlDataAdapter(sql, con.GetDBConnection());

            DT = new DataTable();
            Adapt.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                sql = "Select BANKCD From BankName Where BRCD = '" + Session["BRCD"].ToString() + "'";
                BKCD = con.sExecuteScalar(sql);

                if (BKCD != "1002")
                {
                    Type = 0;
                }
              

                DataTable dtPara = new DataTable();
                sql = "Select ListField, ListValue From PARAMETER Where ListField = 'SMSUID' Union Select ListField, ListValue From PARAMETER Where ListField='SMSPWD' Union Select ListField, ListValue From PARAMETER Where ListField='SMSSENDER' Union Select ListField, ListValue From PARAMETER Where ListField='SMSLINK' Union Select ListField, ListValue From PARAMETER Where ListField='SMSClient'";
                dtPara = con.GetDatatable(sql);
                WebReq = null;
                WebRes = null;
                for (int a = 0; a < dtPara.Rows.Count; a++)
                {
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSUID")
                        UserName = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSPWD")
                        Password = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSSENDER")
                        source = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSLINK")
                        Server = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "SMSClient")
                        SMSClient = dtPara.Rows[a]["ListValue"].ToString();
                }
                for (K = 0; K <= DT.Rows.Count - 1; K++)
                {
                    Message = DT.Rows[K]["SMS_Description"].ToString();

                    //For convert message into unicode here
                    //Message = System.Net.WebUtility.UrlEncode(Message);
                    //if (Type == 2)
                    //{
                    //    Message = ConvertToUnicode(Message);
                    //}

                    mno = DT.Rows[K]["Mobile"].ToString().Trim();
                    if (mno.Length == 10)
                    {
                    }
                    else
                    {
                        goto a;
                    }

                    WebResponseString = "";

            
                    if (SMSClient == "Palghar")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + MobileNo + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1";
                    }
                    else if (SMSClient == "RouteSms")
                    {
                        MobileNo = "%2b" + DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "destination=" + MobileNo + "&source=" + source + "&message=" + Message.ToString() + "&username=" + UserName + "&password=" + Password + "&type=" + Type;
                    }
                    else if (SMSClient == "AdMark")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "clientid=" + UserName + "&apikey=" + Password + "&msisdn=" + MobileNo + "&sid=YSHPAT&msg=" + Message.ToString() + "&fl=0&gwid=2";
                    }
                    else if (SMSClient == "TubeLight")
                    {
                        MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "username=" + UserName + "&password=" + Password + "&type=Text&sender=" + source + "&mobile=" + MobileNo + "&message=" + Message.ToString();

                    }

                    WebReq = System.Net.HttpWebRequest.Create(URL);
                    WebReq.Timeout = 25000;

                    //Code for getting response from web
                    WebRequest myreq = (WebRequest)WebRequest.Create(URL);
                    WebResponse myres = (WebResponse)myreq.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(myres.GetResponseStream());
                    WebResponseString = reader.ReadToEnd();
                    myres.Close();
                    //Display Response.                

                    responce = WebResponseString.ToString();
                    //responce = "0";
                    if (BKCD != "1002") //For AVS Link
                    {
                        responcestatus = responce.Substring(0, 4);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And BRCD='" + BRCD + "' and CustNo='"+DT.Rows[K]["CustNo"].ToString()+"'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And BRCD='" + BRCD + "' and CustNo='" + DT.Rows[K]["CustNo"].ToString() + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                    else
                    {
                        //For YASHOMANDIR Link
                        responcestatus = responce.Substring(14, 3);
                        sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And BRCD='" + BRCD + "' and CustNo='" + DT.Rows[K]["CustNo"].ToString() + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                        sql1 = "";
                        sql1 = "Delete From AVS1092 Where sms_date='" + con.ConvertDate(EntryDate).ToString() + "' And BRCD='" + BRCD + "' and CustNo='" + DT.Rows[K]["CustNo"].ToString() + "'";
                        CMD = new SqlCommand(sql1, con.GetDBConnection());
                        CMD.ExecuteNonQuery();
                    }
                a:
                    continue;
                }
            }
            return responcestatus;
        }
        catch (Exception ex)
        {
            return null;
        }
        return responcestatus;
    }

     public string SenBulkSMS(string Flag, string brcd, string SMS, string EntryDate,string Mobile,string SMSType)
    {

        System.Net.WebRequest WebReq;
        System.Net.WebResponse WebRes;
        string mno = "";
        string Server = "";
        string UserName = "";
        string Password = "";
        string source = "";
        string MobileNo = "";
        string URL = "";
        string responce = "";
        string responcestatus = "";

        string WebResponseString = "";
        int Type = 0;
        string Message = "";
        string BKCD = "";
        int K = 0;
        string SMSClient = "";

        //sql = "EXEC Isp_AVS0089 '" + Flag + "','" + brcd + "'";
        sql = "Select ID,Mobile, SMS_Description,CustNo,SMS_TYPE From AVS1092_BULK Where SMS_STATUS = 1 and Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And BRCD = '" + brcd + "'";
        try
        {
            Adapt = new SqlDataAdapter(sql, con.GetDBConnection());

            DT = new DataTable();
            Adapt.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                sql = "Select BANKCD From BankName Where BRCD = '" + Session["BRCD"].ToString() + "'";
                BKCD = con.sExecuteScalar(sql);

                if (BKCD != "1002")
                {
                    Type = 0;
                }

                DataTable dtPara = new DataTable();
                sql = "Select ListField, ListValue From PARAMETER Where ListField = 'BulkSMSUID' Union Select ListField, ListValue From PARAMETER Where ListField='BulkSMSPWD' Union Select ListField, ListValue From PARAMETER Where ListField='BulkSMSSENDER' Union Select ListField, ListValue From PARAMETER Where ListField='BulkSMSLINK' Union Select ListField, ListValue From PARAMETER Where ListField='BulkSMSClient'";
                dtPara = con.GetDatatable(sql);
                WebReq = null;
                WebRes = null;
                for (int a = 0; a < dtPara.Rows.Count; a++)
                {
                    if (dtPara.Rows[a]["ListField"].ToString() == "BULKSMSUID")
                        UserName = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "BULKSMSPWD")
                        Password = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "BULKSMSSENDER")
                        source = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "BULKSMSLINK")
                        Server = dtPara.Rows[a]["ListValue"].ToString();
                    if (dtPara.Rows[a]["ListField"].ToString() == "BULKSMSClient")
                        SMSClient = dtPara.Rows[a]["ListValue"].ToString();
                }
                Message =DT.Rows[K]["SMS_Description"].ToString();
                //For convert message into unicode here
               // Message = System.Net.WebUtility.UrlEncode(Message);
               // if (Type == 2)
               //{
               //    Message = ConvertToUnicode(SMS);
               //}

                string PHONE = "";
                for (K = 0; K <= DT.Rows.Count - 1; K++)
                {

                    //mno =Mobile;
                    //if (mno.Length == 10)
                    //{
                    //    if (K == 0)
                    //        PHONE = DT.Rows[K]["MOBILE"].ToString().Trim();
                    //    else
                    //        PHONE += "," + DT.Rows[K]["MOBILE"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    continue;
                    //}

                    //}
                    WebResponseString = "";

                    if (SMSClient == "Palghar")
                    {
                        //MobileNo = DT.Rows[K]["Mobile"].ToString();
                        if (DT.Rows[K]["SMS_TYPE"].ToString() == "0")
                            URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + DT.Rows[K]["Mobile"].ToString() + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1";
                        else
                        URL = Server + "user=" + UserName + "&key=" + Password + "&mobile=" + DT.Rows[K]["Mobile"].ToString() + "&message=" + Message.ToString() + "&senderid=" + source + "&accusage=1&unicode=1";
                    }
                    else if (SMSClient == "RouteSms")
                    {
                        //MobileNo = "%2b" + DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "destination=" + DT.Rows[K]["Mobile"].ToString() + "&source=" + source + "&message=" + Message.ToString() + "&username=" + UserName + "&password=" + Password + "&type=" + Type;
                    }
                    else if (SMSClient == "AdMark")
                    {
                        //MobileNo = DT.Rows[K]["Mobile"].ToString();
                        URL = Server + "clientid=" + UserName + "&apikey=" + Password + "&msisdn=" + DT.Rows[K]["Mobile"].ToString() + "&sid=" + source + "&msg=" + Message.ToString() + "&fl=0&gwid=2";
                    }
                    else if (SMSClient == "TubeLight")
                    {
                        // MobileNo = DT.Rows[K]["Mobile"].ToString();
                        //URL=http://103.209.99.7/sendsms/bulksms.php?username=XXXXXX&password=XXXXXX&type=TEXT&mobile=99999xxxxx,99999xxxxx&sender=XXXXXX&message=Testing
                        if (DT.Rows[K]["SMS_TYPE"].ToString() == "0")
                            URL = Server + "username=" + UserName + "&password=" + Password + "&type=TEXT&mobile=" + DT.Rows[K]["Mobile"].ToString() + "&sender=" + source + "&message=" + Message.ToString();
                        else
                            URL = Server + "username=" + UserName + "&password=" + Password + "&type=Unicode&mobile=" + DT.Rows[K]["Mobile"].ToString() + "&sender=" + source + "&message=" + Message.ToString();

                    }
                    WebReq = System.Net.HttpWebRequest.Create(URL);
                    WebReq.Timeout = 25000;

                    //Code for getting response from web
                    WebRequest myreq = (WebRequest)WebRequest.Create(URL);
                    WebResponse myres = (WebResponse)myreq.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(myres.GetResponseStream());
                    WebResponseString = reader.ReadToEnd();
                    myres.Close();
                    //Display Response.                

                    responce = WebResponseString.ToString();
                    //if (BKCD != "1002") //For AVS Link
                    //{
                    //    for (int L = 0; L <= DT.Rows.Count - 1; L++)
                    //    {
                    //string[] RESCODE=responce.Split(',');
                    //responcestatus = responce.Substring(0, 4);
                    //  sql1 = "Insert Into AVS1092_BULK (CUSTNO,MOBILE,SMS_DATE,SMS_DESCRIPTION,RESPONSE) VALUES('" + DT.Rows[L]["CUSTNO"].ToString().Trim() + "','" + DT.Rows[L]["MOBILE"].ToString().Trim() + "',CONVERT(VARCHAR(11),'"+EntryDate+"',121),N'" + SMS + "','" + RESCODE[L].Substring(0, 4) + "')";
                    sql1 = "update AVS1092_BULK set SMS_STATUS=3 where ID=" + DT.Rows[K]["ID"].ToString() + "";
                    CMD = new SqlCommand(sql1, con.GetDBConnection());
                    CMD.ExecuteNonQuery();
                    //     }
                    // }
                    // else
                    // {
                    // //    //For YASHOMANDIR Link
                    //     responcestatus = responce.Substring(14, 3);
                    // //    sql1 = "Insert Into AVS1092_B Select *,'" + responcestatus + "' From AVS1092 Where Sms_Date = '" + con.ConvertDate(EntryDate).ToString() + "' And CUSTNO='" + CustNo + "'";
                    // //    CMD = new SqlCommand(sql1, con.GetDBConnection());
                    // //    CMD.ExecuteNonQuery();
                    //}

                }

            }
            return responce;
        }
        catch (Exception ex)
        {
            return null;
        }
        return responce;
    }


}
