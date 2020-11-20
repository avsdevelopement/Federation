using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IAdminAcces
/// </summary>
public interface IAdminAcces
{
    int CheckAdminAccess(string BRCD,string UserGroup);
}