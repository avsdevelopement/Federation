using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for QueryStringModule
/// </summary>
public class QueryStringModule : IHttpModule
{
    static ASCIIEncoding encoding;

    static QueryStringModule()
    {
        encoding = new ASCIIEncoding();
    }
    public void Dispose()
    {

    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += context_BeginRequest;
    }

    void context_BeginRequest(object sender, EventArgs e)
    {

        HttpContext context = HttpContext.Current;
        if (context.Request.Url.OriginalString.Contains("aspx") &&
                   context.Request.RawUrl.Contains("?"))
        {
            string query = context.Request.RawUrl;
            string path = GetVirtualPath();

            if (query.Contains(PARAMETER_NAME))
            {
                // Decrypts the query string and rewrites the path.
                //string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                string decryptedQuery = Decrypt(query);
                context.RewritePath(path, string.Empty, decryptedQuery);
            }
            else if (context.Request.HttpMethod == "GET")
            {
                // Encrypt the query string and redirects to the encrypted URL.
                // Remove if you don't want all query strings to be encrypted automatically.
                if (query != "")
                {
                    string encryptedQuery = Encrypt(query);
                    context.Response.Redirect(encryptedQuery);
                }
            }
        }
    }

    private const string PARAMETER_NAME = "encrypt=1&";
    private const string ENCRYPTION_KEY = "key";


    private static string GetVirtualPath()
    {
        string path = HttpContext.Current.Request.RawUrl;
        path = path.Substring(0, path.IndexOf("?"));
        path = path.Substring(path.LastIndexOf("/") + 1);
        return path;
    }

    /// <summary>
    /// Function encrypts the url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>

    public static string Encrypt(string url)
    {
        string cookedUrl = url;

        if (url != null && url.Contains(".aspx?"))
        {
            cookedUrl = url.Substring(0, url.IndexOf('?') + 1);

            var queryStrings = url.Substring(url.IndexOf('?') + 1).Split('&');

            foreach (var queryString in queryStrings)
            {
                if (!string.IsNullOrEmpty(queryString))
                    cookedUrl += queryString.Split('=')[0] + "=" + Encrypting(queryString.Split('=')[1]) + "&";
            }
            cookedUrl = cookedUrl.Substring(0, cookedUrl.Length - 1);
            cookedUrl = cookedUrl.Replace("?", "?" + PARAMETER_NAME);
        }
        return cookedUrl;

    }

    /// <summary>
    /// Functin decrypts the url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>

    public static string Decrypt(string url)
    {
        if (url.Contains(".aspx?"))
        {
            var path = HttpContext.Current.Request.RawUrl;
            path = path.Substring(0, path.IndexOf("?", StringComparison.Ordinal) + 1);
            path = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);

            var queryStrings = url.Substring(url.IndexOf('?') + 1).Split('&');

            foreach (var queryString in queryStrings)
            {
                path += queryString.Split('=')[0] + "=" + Decrypting(queryString.Substring(queryString.IndexOf('=') + 1)) + "&";
            }
            path = path.Substring(0, path.Length - 1);
            url = path;
        }
        return url;
    }
    /// <summary>
    /// Function encodes the input parameter
    /// </summary>
    /// <param name="target">the target which is to be encoded</param>
    /// <returns>encoded string</returns>

    static string Encrypting(string encryptString)
    {
        try
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
        catch { }
        return null;
    }
    /// <summary>
    /// Function decodes the input parameter
    /// </summary>
    /// <param name="target">the target which is to be decoded</param>
    /// <returns>decode string</returns>

    static string Decrypting(string cipherText)
    {
        try
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        catch { }
        return null;
    }


}