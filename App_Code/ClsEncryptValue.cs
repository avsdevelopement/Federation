using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public class ClsEncryptValue
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "", Date = "";

	public ClsEncryptValue()
	{
		
	}
    
    public string Getdate()
    {
        string dt = "";
        try
        {
            sql = "select getdate()";
            dt = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    protected string GetIPAddress()
    {
        string ipaddress="";
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipaddress = Convert.ToString(IP);
            }
        }
        return ipaddress;
    }

    public string GetMK(string MID)//Insert Entry
    {
        string Value = "";
        Date = Getdate();
        try
        {
            string PCMAC = GetIPAddress();
            Value = encrypt(Date + "_" + PCMAC + "_" + MID);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Value;
    }

    public string GetCK(string CID)//Entry Autho
    {
        string Value = "";
        Date = Getdate();
        try
        {
            string PCMAC = GetIPAddress();
            Value = encrypt(Date + "_" + PCMAC + "_" + CID);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Value;
    }

    public string GetVK(string VID)//EntryCancel
    {
        string Value = "";
        Date = Getdate();
        try
        {
            string PCMAC = GetIPAddress();
            Value = encrypt(Date + "_" + PCMAC + "_" + VID);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Value;
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    //Added By Amol ON 20/11/2017 for insert data into M table from allvcr after check voucher tally or not
    public string InsertTallyVoucher(string BrCode, string EDate, string VoucherNo)
    {
        try
        {
            sql = "Exec SP_InsertTallyVoucher @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @SetNo = '" + VoucherNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

}