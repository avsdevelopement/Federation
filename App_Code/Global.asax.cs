using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.UI;


public class Global
{


    //////public static string ConStr;
    //////public static string PDateFormat;
    //////public static string ServerName;
    //////public static string DbName;
    //////public static string DbLogin;
    //////public static string DbPwd;
    //////public static string Svr;
    //////public static string CoNameShort;
    //////public static System.Data.SqlClient.SqlConnection Conn;
    //////public static System.Data.SqlClient.SqlConnection Conn1;
    //////public static System.DateTime FromDate;
    //////public static System.DateTime ToDate;
    //////public static int OWAmt1;
    //////public static int OWAmt2;
    //////public static int OWAmt;
    //////public static string[] OValue = new[];
    //////public static string[] TValue = new string[21];
    //////public static string TDrm;
    //////public static string UserStatus;
    //////public static string curr;
    //////public static string DefaultCurr;
    //////public static int InvPFlag;
    //////public static double AllocateAmt;
    //////public static long TheatreKey;
    //////public static string ErrNo;
    //////public static string ErrMsg;
    //////public static string ErrFrm;
    //////public static long HireLocation;
    //////public static long HireOffice;
    //////public static string FilmCoId;
    //////public static string BankDrCr;
    //////public static string UserId;
    //////public static string Gerror;
    //////public static int BankType;
    //////public static DateTime UserTime;
    //////public static int Pyr;
    //////public static long RealKey;
    //////public static long PubKey;
    //////    //'KS 12-12-2013
    //////public static long PrintCostKey;

    //////    //Rahul 15.12.07
    //////public static int OfficeAll;
    //////    //Rahul 15.12.07
    //////string Sql;
    //////    //Rahul 15.12.07
    //////System.Data.SqlClient.SqlCommand CmdRead;
    //////    //Rahul 15.12.07
    //////System.Data.SqlClient.SqlDataReader ObjRead;
    //////string CompanyName;
    ////////Gorakh 02-05-2012 Start
    //////public static string PANNo;
    //////public static string CORCOadd1;
    //////public static string CORCOadd2;
    ////////Gorakh 02-05-2012 Start

    ////////'KS 28-02-2013
    //////public static bool ISCORCOaddTF;
    ////////'KS 28-02-2013

    ////////Gorakh 22-05-2012 Start
    //////public static int BankBSId;
    //////public static int BankMGId;
    //////public static int BankMG2Id;
    //////public static int BankSGId;
    //////public static int BankSG2Id;
    //////public static int BankGId;
    //////public static int InvKeyGId;
    ////////Gorakh 22-05-2012 Start

    ////////'NB-17-08-2011
    //////public static string MailPort;
    //////public static string MailDomain;
    //////public static string MailUId;
    //////public static string MailPwd;
    //////public static string MailFrom;
    //////public static string[] MailTo = new string[11];
    ////////'UR : 08/02/2013 Added PSUMMARY2 'Gorakh 28-05-2012 Start 
    //////public static int PSUMMARY;
    //////public static int PSUMMARY2;
    ////////Gorakh 28-05-2012 Start


    //////    //'UR : 29/08/2012
    //////public static bool ISSAPEXP = false;
    //////public static int GCompBSId;
    //////public static int GCompMGId;
    //////public static int GCompMG2Id;
    //////public static int GCompSGId;
    //////public static int GCompSG2Id;

    //////public static int GCompGId;
    //////    //'UR : 21/01/2013
    //////public static bool IsHeadOfficewiseRpts;

    //////    //'UR : 28/03/2013
    //////public static bool DisplZeroBill;
    //////    //'mak 11-3-15
    //////public static bool ISOFFWISEADD;

    //SqlDataReader  ObjRead=new SqlDataReader();

    public Global()
    {
    }



    //public static object setFocus(System.Web.UI.Control ctrl, System.Web.UI.Page page)
    //{
    //    string s = null;
    //    s = "<Script language='javascript'> document.getElementById('ctl00_cphDetail_" + ctrl.ID + "').focus()</Script>";
    //    page.RegisterStartupScript("focus", s);
    //}


    //public static System.DateTime FnReturnDate(string DateString)
    //{
    //    string S = null;
    //    S = Strings.Mid(DateString, 4, 2) + "/" + Strings.Mid(DateString, 1, 2) + "/" + Strings.Mid(DateString, 7, 4);
    //    return Convert.ToDateTime(S);
    //}


    //public static string FnCompany(string Comp)
    //{
    //    string CompName = null;
    //    if (Comp == "CTL") {
    //        CompName = "Sony Pictures Releasing of India Ltd.";
    //    } else if (Comp == "SPE") {
    //        CompName = "SPE Films India Pvt. Ltd.";
    //    } else if (string.IsNullOrEmpty(Comp) | Comp == "ALL") {
    //        CompName = "Columbia/SPE";
    //        //'Ravindra 11 07 2011
    //    } else if (Comp == "DC") {
    //        CompName = "Demo Company Pvt.Ltd.";
    //    }
    //    //'Ravindra 11 07 2011
    //    return CompName;
    //}
    //public static object FnMessage(string Warn, System.Web.UI.Page Page1)
    //{
    //    if ((!Page1.IsStartupScriptRegistered("clientScript"))) {
    //        Page1.RegisterStartupScript("clientScript", "<Script language=javascript>alert('" + Warn + "');</script>");
    //    }
    //}
    //public static string FnReturnString(System.DateTime Date1)
    //{
    //    string S = null;
    //    S = string.Format(Date1, "dd/MM/yyyy");
    //    return S;
    //}


    ////Popup Message
    //public static object WebMsgBox(string Warn, System.Web.UI.Page Page1)
    //{
    //    if ((!Page1.IsStartupScriptRegistered("clientScript"))) {
    //        Page1.RegisterStartupScript("clientScript", "<Script language=javascript>alert('" + Warn + "');</script>");
    //    }
    //}


    //public static object MsgBox(string Warn, System.Web.UI.Page Page1)
    //{
    //    ScriptManager.RegisterClientScriptBlock(Page1, Page1.GetType(), Guid.NewGuid().ToString(), "alert('" + Warn + "');", true);
    //}
    //// ''Ravindra 28 07 2011
    //public static object BrowsTitle(string Title, System.Web.UI.Page page1)
    //{
    //    //trying different ways to make this work 

    //    string selectScript = "<script language='javascript'>" + "window.document.title.valueOf(this)=" + Title + "').select();</script>";
    //    page1.RegisterStartupScript("SelectScript", selectScript);
    //}
    ////'Ravindra 28 07 2011

    ////'Gorakh 29-09-2011 Start
    //public static string Decrypt(string cipherText)
    //{
    //    byte[] initVectorBytes = null;
    //    initVectorBytes = Encoding.ASCII.GetBytes("@1B2c3D4e5F6g7H8");
    //    byte[] saltValueBytes = null;
    //    saltValueBytes = Encoding.ASCII.GetBytes("s@1tValue");
    //    byte[] cipherTextBytes = null;
    //    cipherTextBytes = Convert.FromBase64String(cipherText);
    //    PasswordDeriveBytes password = default(PasswordDeriveBytes);
    //    password = new PasswordDeriveBytes("Pas5pr@se", saltValueBytes, "SHA1", 2);
    //    byte[] keyBytes = null;
    //    keyBytes = password.GetBytes(256 / 8);
    //    RijndaelManaged symmetricKey = default(RijndaelManaged);
    //    symmetricKey = new RijndaelManaged();
    //    symmetricKey.Mode = CipherMode.CBC;
    //    ICryptoTransform decryptor = default(ICryptoTransform);
    //    decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
    //    MemoryStream memoryStream = default(MemoryStream);
    //    memoryStream = new MemoryStream(cipherTextBytes);
    //    CryptoStream cryptoStream = default(CryptoStream);
    //    cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
    //    byte[] plainTextBytes = null;
    //    plainTextBytes = new byte[cipherTextBytes.Length + 1];
    //    int decryptedByteCount = 0;
    //    decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
    //    memoryStream.Close();
    //    cryptoStream.Close();
    //    string plainText = null;
    //    plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    //    return plainText;
    //}
    ////'Gorakh 29-09-2011 End

    ////Gorakh 22-05-2012 Start
    ////public void FnFindAcc()
    //{
    //    try {
    //        System.Data.Sql = "Select PmBSId,PmMGId,PmMG2Id,PmSGId,PmSG2Id,PmGId From Parameter Where PmField= 'Bank' And PmType='G'";
    //        CmdRead = new System.Data.SqlClient.SqlCommand(System.Data.Sql, GlobalObj.Conn);
    //        ObjRead = CmdRead.ExecuteReader;
    //        if (ObjRead.Read) {
    //            BankBSId = ObjRead("PmBSId");
    //            BankMGId = ObjRead("PmMGId");
    //            BankMG2Id = ObjRead("PmMG2Id");
    //            BankSGId = ObjRead("PmSGId");
    //            BankSG2Id = ObjRead("PmSG2Id");
    //            BankGId = ObjRead("PmGId");
    //        } else {
    //            BankBSId = 0;
    //            BankMGId = 0;
    //            BankMG2Id = 0;
    //            BankSGId = 0;
    //            BankSG2Id = 0;
    //            BankGId = 0;
    //        }
    //    } catch (Exception Ex)
    //    {
    //        Response.Redirect("ErrorPage.Aspx?Error=" + Ex.ToString());
    //    } 
    //    finally
    //    {
    //        if (!ObjRead.IsClosed)
    //            ObjRead.Close();
    //        CmdRead = null;
    //    }

    //    try {
    //        System.Data.Sql = "Select PmBSId,PmMGId,PmMG2Id,PmSGId,PmSG2Id,PmGId From Parameter Where PmField= 'Invoicing Post On Acc' And PmType='G'";
    //        CmdRead = new System.Data.SqlClient.SqlCommand(System.Data.Sql, GlobalObj.Conn);
    //        ObjRead = CmdRead.ExecuteReader;
    //        if (ObjRead.Read) {
    //            RealKey = ObjRead("PmGId");
    //            InvKeyGId = ObjRead("PmGId");
    //            TheatreKey = InvKeyGId;
    //        } else {
    //            InvKeyGId = 0;
    //            RealKey = 0;
    //            Global.TheatreKey = 0;
    //        }
    //    }
    //    catch (Exception Ex) 
    //    {
    //        Response.Redirect("ErrorPage.Aspx?Error=" + Ex.ToString());
    //    } 
    //    finally
    //    {
    //        if (!ObjRead.IsClosed)
    //            ObjRead.Close();
    //        CmdRead = null;
    //    }

    //    //Gorakh 10-06-2012 Start
    //    try {
    //        System.Data.Sql = "Select PmBSId,PmMGId,PmMG2Id,PmSGId,PmSG2Id,PmGId From Parameter Where PmField= 'Film Publicity Group' And PmType='G'";
    //        CmdRead = new System.Data.SqlClient.SqlCommand(System.Data.Sql, GlobalObj.Conn);
    //        ObjRead = CmdRead.ExecuteReader;
    //        if (ObjRead.Read) {
    //            PubKey = ObjRead("PmGId");
    //        } else {
    //            PubKey = 0;
    //        }
    //    } catch (Exception Ex) {
    //        Response.Redirect("ErrorPage.Aspx?Error=" + Ex.ToString());
    //    } finally {
    //        if (!ObjRead.IsClosed)
    //            ObjRead.Close();
    //        CmdRead = null;
    //    }
    //    //Gorakh 10-06-2012 Start
    //    //'KS 12-12-2013
    //    try {
    //        System.Data.Sql = "Select PmBSId,PmMGId,PmMG2Id,PmSGId,PmSG2Id,PmGId From Parameter Where PmField= 'Film PrintCost Group' And PmType='G'";
    //        CmdRead = new System.Data.SqlClient.SqlCommand(System.Data.Sql, GlobalObj.Conn);
    //        ObjRead = CmdRead.ExecuteReader;
    //        if (ObjRead.Read) {
    //            PrintCostKey = ObjRead("PmGId");
    //        } else {
    //            PrintCostKey = 0;
    //        }
    //    } catch (Exception Ex) {
    //        Response.Redirect("ErrorPage.Aspx?Error=" + Ex.ToString());
    //    } finally {
    //        if (!ObjRead.IsClosed)
    //            ObjRead.Close();
    //        CmdRead = null;
    //    }
    //    //'KS 12-12-2013
    //}
    public void Application_Start(object sender, EventArgs e)
    {
    }
    public void Session_Start(object sender, EventArgs e)
    {
    }
    public void Application_BeginRequest(object sender, EventArgs e)
    {
        // Fires at the beginning of each request
    }

    public void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        // Fires upon attempting to authenticate the use
    }

    public void Application_Error(object sender, EventArgs e)
    {
        // Fires when an error occurs
    }

    public void Session_End(object sender, EventArgs e)
    {
        try
        {
            //Exception Ex = Microsoft.SqlServer.Server.GetLastError().GetBaseException();
            //HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            HttpContext.Current.Response.Redirect("FrmLogin.aspx", false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Exception Ex = Microsoft.SqlServer.Server.GetLastError().GetBaseException();           
            HttpContext.Current.Response.Redirect("FrmLogin.aspx", false);
        }
        finally
        {
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

    }

    public void Application_End(object sender, EventArgs e)
    {
        // Fires when the application ends
    }

}