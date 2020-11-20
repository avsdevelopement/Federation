using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

/// <summary>
/// Summary description for ClsCo_master
/// </summary>
public class ClsCo_master
{
    DbConnection conn = new DbConnection();
    int Result;
    string sql;
	public ClsCo_master()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int insertcooprative(string CLID,string BRCD,string custno,string societyname,string dirname,string design,string add1,string add2,string city,string pincode,string offaddr,string nataddr,string mobile1,string mobile2,string dob,string pancard,string adharcard,string SRNO)
    {
        sql = "Exec SP_COPRATIVE @FLAG='INSERTDATA', @CUSTNO='" + custno + "',@SOCIETYNAME='" + societyname + "',@DIRECTORNAME='" + dirname + "',@DESIGNATION=design,@ADDRESS1='" + add1 + "',@ADDRESS2='" + add2 + "',@CITY ='" + city + "',@PINCODE='" + pincode + "',@OFFADRR ='" + offaddr + "',@NATADDR ='" + nataddr + "',@MOBILE1='" + mobile1 + "',@MOBILE2='" + mobile2 + "',@DOB ='" +conn.ConvertDate(dob) + "',@PANCARD='" + pancard + "',@ADHARCARD='" + adharcard + "',@CLID='" + CLID + "',@BRCD='" + BRCD + "',@SRNO='"+SRNO+"',@STAGE='1001'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    
    }
    public string  getdetails(string custno)
    {
        sql = "select COUNT(*) from CODIRECTOR where CUSTNO='" + custno + "' AND STAGE!=1004";
        string Result = conn.sExecuteScalar(sql);
         return Result;
        
    }
    public int bindgrid(string CUSTNO,string BRCD,GridView Gview)
    {
        sql = "select * from CODIRECTOR  where CUSTNO='" + CUSTNO + "' and BRCD='" + BRCD + "' and STAGE!=1004 ";
        int Result = conn.sBindGrid(Gview,sql);
        return Result;
    }
    public DataTable showdata(string srno,string custno,string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "select CUSTNO,SOCIETYNAME,SrNo,DIRECTORNAME,DESIGNATION,ADDRESS1,ADDRESS2,CITY,PINCODE,OFFADRR,NATADDR,MOBILE1,MOBILE2,DOB,PANCARD,ADHARCARD from CODIRECTOR where SRNO='" + srno + "' and BRCD='" + BRCD + "' and CUSTNO='" + custno + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int modifycorporate(string CLID, string BRCD, string custno, string societyname, string dirname, string design, string add1, string add2, string city, string pincode, string offaddr, string nataddr, string mobile1, string mobile2, string dob, string pancard, string adharcard, string SRNO)
    {
        sql = "Exec SP_COPRATIVE @FLAG='Modify',@BRCD='"+BRCD+"',@CLID='"+CLID+"', @SOCIETYNAME='" + societyname + "',@DIRECTORNAME='" + dirname + "',@DESIGNATION='" + design + "',@ADDRESS1='" + add1 + "',@ADDRESS2='" + add2 + "',@CITY='" + city + "',@PINCODE='" + pincode + "',@OFFADRR='" + offaddr + "',@NATADDR='" + nataddr + "',@MOBILE1='" + mobile1 + "',@MOBILE2='" + mobile2 + "',@DOB='" + conn.ConvertDate(dob) + "',@PANCARD='" + pancard + "',@ADHARCARD='" + adharcard +  "', @SRNO='" + SRNO + "', @CUSTNO='" + custno + "',@STAGE='1002'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int deletecorporate(string CLID,string BRCD,string SRNO,string CUSTNO)
    {
        sql = "update CODIRECTOR set Stage=1004 where BRCD='" + BRCD + "' and SrNo='" + SRNO + "'and CUSTNO='" + CUSTNO + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string  getsoci(string custno,string brcd)
    {
        sql = "SELECT ORGNAME FROM CO_DETAILS where CUSTNO='" + custno + "' and STAGE!=1004 and BRCD='" + brcd + "'";
       string  Result = conn.sExecuteScalar(sql);
        return Result; 
    }
    public int Insertadd(string CUSTNO,string ADD1,string ADD2,string ROAD,string NEAR,string STATE,string DIST,string TALUKA,string VILLAGE,string PINCODE,string BRCD)
    {
        sql = "INSERT INTO ADDMAST(ADDTYPE,CUSTNO,Flat_Roomno,Society_Name,street_sector,near,state,district,area_taluka,city,pincode,brcd,Effect_Date,stage,pcmac)VALUES('1','" + CUSTNO + "','" + ADD1 + "','" + ADD2 + "','" + ROAD + "','" + NEAR + "','" + STATE + "','" + DIST + "','" + TALUKA + "','" + VILLAGE + "','" + PINCODE + "','" + BRCD + "',GETDATE(),1001,'" + conn.PCNAME() + "')";
         Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string getcustno(string BRCD)
    {
        sql = "select MAX(CUSTNO)+1 from master where brcd='"+ BRCD+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public int ADDCUST(string  BRCD,string CUSTNO,string ORGNAME,string PANCARD,string SPLINST,string CUSTCAT,string REGDATE)
    {
        sql = "INSERT INTO CO_DETAILS (CUSTNO,ORGNAME,PANCARD,SPL_INST,CUSTCATG,REGDATE,BRCD,STAGE)VALUES('"+CUSTNO+"','"+ORGNAME+"','"+PANCARD+"','"+SPLINST+"','"+CUSTCAT+"','"+conn.ConvertDate(REGDATE)+"','"+BRCD+"',1001)";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int BINDCUSTCO(string CUSTNO, string BRCD, GridView Gview)
    {
        sql = "select * from Co_details  where CUSTNO='" + CUSTNO + "' and BRCD='" + BRCD + "' and STAGE!=1004 ";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
    public DataTable showdataCUST(string CUSTNO, string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "SELECT CUSTNO,ORGNAME,PANCARD,SPL_INST,CUSTCATG,REGDATE FROM CO_DETAILS WHERE BRCD='" + BRCD + "' AND CUSTNO='" + CUSTNO + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable showdataCUSTADD(string CUSTNO, string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "SELECT FLAT_ROOMNO,SOCIETY_NAME,STREET_SECTOR,NEAR,STATE,DISTRICT,AREA_TALUKA,CITY,PINCODE FROM ADDMAST WHERE BRCD='"+BRCD+"' AND CUSTNO='"+CUSTNO+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int modifycustadd11(string BRCD, string CUSTNO, string ORGNAME, string PANCARD, string SPLINST, string CUSTCAT, string REGDATE)
    {
        sql = "update CO_DETAILS set ORGNAME='" + ORGNAME + "',PANCARD='" + PANCARD + "',SPL_INST='" + SPLINST + "',CUSTCATG='" + CUSTCAT + "',REGDATE='" + conn.ConvertDate(REGDATE) + "',STAGE=1002 where BRCD='" + BRCD + "' AND CUSTNO='" + CUSTNO + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int modifycustadd12(string CUSTNO, string ADD1, string ADD2, string ROAD, string NEAR, string STATE, string DIST, string TALUKA, string VILLAGE, string PINCODE, string BRCD)
    {
        sql = "update  addmast set CUSTNO='" + CUSTNO + "',Flat_Roomno='" + ADD1 + "',Society_Name='" + ADD2 + "',street_sector='" + ROAD + "',near='" + NEAR + "',state='" + STATE + "',district='" + DIST + "',area_taluka='" + TALUKA + "',city='" + VILLAGE + "',pincode='" + PINCODE + "',Effect_Date=GETDATE(),stage='1002'WHERE  BRCD='" + BRCD + "'AND CUSTNO='" + CUSTNO + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
      public int DELETEADD(string CUSTNO,string BRCD)
    {
        sql = "DELETE FROM ADDMAST WHERE BRCD='"+BRCD+"' AND CUSTNO='"+CUSTNO+"'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
      public int DELETEAINFO(string CUSTNO, string BRCD)
      {
          sql = "DELETE FROM CO_DETAILS WHERE BRCD='" + BRCD + "' AND CUSTNO='" + CUSTNO + "'";
          Result = conn.sExecuteQuery(sql);
          return Result;
      }
      public int UPDATECODETAILS(string TURN,string EMP,string LOC,string CURR,string OTHBANK,string RESCUST,string ORGTYPE,string BLINE,string NOOFBR,string DATESTD,string PLACESTD,string REGNO,string AML,string REGAUTH,string COMDATE,string DATESCUST,string KYC,string RELOFF,string ADHAR,string REGPLACE,string HOADD,string CITY,string PIN,string PHONE,string FAX,string BRCD,string CUSTNO)
      {
          sql = "UPDATE CO_DETAILS SET TURNOVER='" + TURN + "',NOOFEMP='" + EMP + "',LOC='" + LOC + "',CURRENCIES='" + CURR + "',OTHER_BANKERS='" + OTHBANK + "',RESONTOCUST='" + RESCUST + "',ORGTYPE='" + ORGTYPE + "',BUSLINE='" + BLINE + "',NOOFBR='" + NOOFBR + "',DATEESTD='" + conn.ConvertDate(DATESTD) + "',PLACEOSESTD='" + PLACESTD + "',REGNO='" + REGNO + "',AML_RATING='" + AML + "',REGAUTH='" + REGAUTH + "',COMDATE='" + conn.ConvertDate(COMDATE) + "',DATESCUST='" + conn.ConvertDate(DATESCUST) + "',KYC='" + KYC + "',RELOFF='" + RELOFF + "',ADHAR='" + ADHAR + "',REGPALCE='" + REGPLACE + "',HO_ADD='" + HOADD + "',CITY='" + CITY + "',PIN='" + PIN + "',PHONENO='" + PHONE + "',FAXNO='" + FAX + "',STATUS='1',EFFECTDATE=GETDATE(),STAGE='1001',PCMAC='" +conn.PCNAME()+ "'  WHERE CUSTNO='" + CUSTNO + "' AND BRCD='" + BRCD + "'";
          Result = conn.sExecuteQuery(sql);
          return Result;
      }
      public int BINDDATACO(string CUSTNO, string BRCD, GridView Gview)
      {
          sql = "SELECT CUSTNO,ORGNAME,BUSLINE,TURNOVER,NOOFBR,REGNO FROM CO_DETAILS WHERE CUSTNO='"+CUSTNO+"' AND BRCD='"+BRCD+"'";
          int Result = conn.sBindGrid(Gview, sql);
          return Result;
      }
      public DataTable SHOWDATA11(string CUSTNO, string BRCD)
      {
          DataTable DT = new DataTable();
          sql = "SELECT CUSTNO,ORGNAME,TURNOVER,NOOFEMP,LOC,CURRENCIES,OTHER_BANKERS,RESONTOCUST,ORGTYPE,BUSLINE,NOOFBR,DATEESTD,PLACEOSESTD,REGNO,AML_RATING,REGAUTH,COMDATE,DATESCUST,KYC,RELOFF,ADHAR,REGPALCE,HO_ADD,CITY,PIN,PHONENO,FAXNO FROM CO_DETAILS WHERE CUSTNO='"+CUSTNO+"' AND BRCD='"+BRCD+"'";
          DT = conn.GetDatatable(sql);
          return DT;
      }
      public void BINDRISK(DropDownList DDLAML)
      { 
      
        sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1044 order by DESCRIPTION";
        conn.FillDDL(DDLAML, sql);
      }
      public void bindcustcat(DropDownList ddlcat)
      {

          sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1045 order by DESCRIPTION";
          conn.FillDDL(ddlcat, sql);
      }
      public void bindorgtype(DropDownList ddlorg)
      {
          sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1046 order by DESCRIPTION";
          conn.FillDDL(ddlorg, sql);

      }
      public void bindkyc(DropDownList ddltal)
      {
          sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1047 order by DESCRIPTION";
          conn.FillDDL(ddltal, sql);
      }
      public int masterinsert(string custno,string custname,string brcd)
      {
          sql = "insert into master (custno,custname,brcd,STAGE)values('"+custno+"','"+custname+"','"+brcd+"',1001)";
          int result = conn.sExecuteQuery(sql);
          return result;
      }
      public int authorised(string brcd,string custno)
      {
          sql = "update master set stage='1003' where brcd='"+brcd+"' and custno='"+custno+"'";
          int result = conn.sExecuteQuery(sql);
          return result;
      }
      public string  checkautho(string brcd, string custno)
      {
          sql = "select stage from master where brcd='"+brcd+"' and custno='"+custno+"' and stage<>1004";
          string  Result = conn.sExecuteScalar(sql);
          return Result;
      }
      public int updatemast(string brcd, string custno, string custname)
      {
          sql = "update master set custname='"+custname+"' where brcd='"+brcd+"' and custno='"+custno+"'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int UpdateCustno(string CUSTNO)////Added by ankita on 03/07/2017 to update lastno
      {
          sql = "UPDATE AVS1000 SET LASTNO='" + CUSTNO + "' WHERE ACTIVITYNO='40' and BRCD='0'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int ADDmaster(string brcd, string custno, string custname)//-----------------------
      {
          sql = "insert into master(BRCD,CUSTNO,CUSTNAME,STAGE) values('" + brcd + "','" + custno + "','" + custname + "',1001)";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int Addmast(string brcd, string address, string pincode, string custno)
      {
          sql = "insert into addmast(Addtype,BRCD,ADDRESS,PINCODE,CUSTNO,STAGE) values(1,'" + brcd + "','" + address + "','" + pincode + "','" + custno + "',1001)";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int MODIFYmaster(string brcd, string Name, string custno)
      {
          sql = "Update master set custname='" + Name + "' ,stage='1002' where brcd='" + brcd + "' and custno='" + custno + "'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int modifyaddmast(string brcd, string address, string pincode, string custno)
      {
          sql = "Update addmast set address='" + address + "' , pincode='" + pincode + "' , stage='1002' where brcd='" + brcd + "' and custno='" + custno + "'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public DataTable addmastview(string custno, string brcd, string address, string pincode)
      {
          DataTable DT = new DataTable();
          sql = "select address,pincode from addmast where custno='" + custno + "' and brcd='" + brcd + "'";
          DT = conn.GetDatatable(sql);
          return DT;
      }


      public string masterView(string custno)
      {
          sql = "select CUSTNAME from master where CUSTNO='" + custno + "'";
          string Result = conn.sExecuteScalar(sql);
          return Result;
      }
      public string mastercustno(string custno)
      {
          sql = "select custno from master where CUSTNO='" + custno + "' ";
          string Result = conn.sExecuteScalar(sql);
          return Result;
      }
      public int deletemaster(string brcd, string Name, string custno)
      {
          sql = "Update master set stage='1004' where brcd='" + brcd + "' and custno='" + custno + "'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
      public int Deleteaddmast(string brcd, string address, string pincode, string custno)
      {
          sql = "Update addmast set stage='1004' where brcd='" + brcd + "' and custno='" + custno + "'";
          int RSULT = conn.sExecuteQuery(sql);
          return RSULT;
      }
}
