using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsAVS_PCMAC
/// </summary>
public class ClsAVS_PCMAC
{

    public bool StoreMac(string MACAddress, string MID, string ENTRYDATE)
    {
        try
        {
            if (!string.IsNullOrEmpty(MACAddress) && !string.IsNullOrEmpty(MID) && !string.IsNullOrEmpty(ENTRYDATE))
            {
                return (new DbConnection().sExecuteQuery("EXEC USP_AVS_PCMAC @FLAG='RGSTR', @MACADDRESS='" + MACAddress + "', @MID='" + MID + "', @ENTRYDATE='" + ENTRYDATE + "'") > 0 ? true : false);
            }
        }
        catch (Exception Ex)
        {
            
            ExceptionLogging.SendErrorToText(Ex);
        }
        return false;
    }    
    public DataTable ReadMAC(string MACAddress)
    {
        try
        {
            return (new DbConnection().GetDatatable("EXEC USP_AVS_PCMAC @FLAG='READ', @MACADDRESS='" + MACAddress + "'"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return null;
    }
    public string Validate(string MACAddress, bool ValidExist=false)
    {
        string messege = "";
        try
        {
            DataTable mac = ReadMAC(MACAddress);

            if (ValidExist)
            {
                if (mac != null)
                {
                    if (mac.Rows.Count > 0)
                    {
                        messege = "Your PC already is registered.";
                    }
                }
            }
            else
            {
                if (mac != null)
                {
                    if (mac.Rows.Count > 0)
                    {
                        if (Convert.ToString(mac.Rows[0]["STAGE"]) != "1003")
                        {
                            messege = "Your PC is registered, but not authorized.";
                        }
                    }
                    else
                    {
                        messege = "Your PC is not registered.";
                    }
                }
                else
                {
                    messege = "Your PC is not registered.";
                }
            }
        }
        catch (Exception Ex)
        {            
            ExceptionLogging.SendErrorToText(Ex);
        }
        return messege;
    }
}