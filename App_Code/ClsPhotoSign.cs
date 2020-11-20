using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ClsPhotoSign
/// </summary>
public class ClsPhotoSign
{
    DbConnection conn = new DbConnection();
    string sql = "";
    public ClsPhotoSign()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Save_PhotoSign(string Txtcustno, string TxtcustName, string ImageName, string ContentType, string ImageType, byte Data, string BrCode, string MID, string dateupload)
    {
        DbConnection conn = new DbConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            //string sql = "insert into AVS1011(CUSTNO,DATEOFUPLOAD,PHOTO,PHOTOTYPE,STAGE,BRCD,MID) values ('"+Txtcustno+"')";
            sql = "insert into AVS1011(CUSTNO, CUSTNAME, ACCNO, SUBGLCODE, SRNO, DATEOFUPLOAD, STAGE, PHOTOSTATUS, RTIME, BRCD, MID, CID, VID, PCMAC, PHOTO, PHOTONAME, PHOTOTYPE, CONTENTTYPE) values('" + Txtcustno + "'," + dateupload + ",'1','1001','1','" + BrCode + "','" + MID + "')";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetMID(string custno)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "select MID From ImageMaster where Custno='" + custno + "'";
            dt = conn.GetPhotoTable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    public int InsertSign(string Imagename, string Stage, string BRCD, string AccType, string ProdCode, string CustNo, string PhotType, string PhotoImg, string MID)
    {
        int Result = 0;
        try
        {
            sql = "insert into Imagemaster (Imagename,Stage,BRCD,AccType,ProdCode,CustNo,PhotType,PhotoImg,MID) values ('" + Imagename + "'," + Stage + ",'" + BRCD + "'," + AccType + ",'" + ProdCode + "','" + CustNo + "'," + PhotType + ",'" + PhotoImg + "'," + MID + ")";
            Result = conn.sExecuteQueryMob(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public int DeletePhoto(string DocType, string ID, string BRCD, string custno)
    {
        int Result = 0;
        try
        {
            sql = "update tblKYCPhoto set Stage='1004' where BRCD='" + BRCD + "'and DocType='" + DocType + "' and CustNo='" + custno + "' and ID='" + ID + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public int GetMaxSrNo(string Accno, string SubGlCode, string BRCD, string DocType)
    {
        int Result = 0;
        try
        {
            sql = "select isnull( max(SRNO) from DMS001  where BRCD='" + BRCD + "'and DocType='" + DocType + "' and AccNo='" + Accno + "' and SUBGLCODE='" + SubGlCode + "','1')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }


    public int InsertPhoto(string Imagename, string Stage, string BRCD, string AccType, string ProdCode, string CustNo, string PhotType, string PhotoImg, string MID)
    {
        int Result = 0;
        try
        {
            sql = "insert into Imagemaster (Imagename,Stage,BRCD,AccType,ProdCode,CustNo,PhotType,PhotoImg,MID) values ('" + Imagename + "'," + Stage + ",'" + BRCD + "'," + AccType + ",'" + ProdCode + "','" + CustNo + "'," + PhotType + ",'" + PhotoImg + "'," + MID + ")";
            Result = conn.sExecuteQueryMob(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public void updatedata(string Txtcustno, string BRCD, string MID, string dateupload, byte[] BPHOTO)
    {
        try
        {
            try
            {
                //proceed only when the image has a valid path 
                sql = "insert into AVS1011(CUSTNO,DATEOFUPLOAD,PHOTO,STAGE,PHOTOSTATUS,RTIME,BRCD,MID) values(" + Txtcustno + ",'" + dateupload + "'," + " :BlobParameter,1001,'1','" + dateupload + "','" + BRCD + "','" + MID + "' )";
                SqlCommand cmnd = new SqlCommand(sql, conn.GetDBConnection());
                cmnd.Parameters.Add("BlobParameter", OracleDbType.Blob).Value = BPHOTO;
                cmnd.ExecuteNonQuery();
                cmnd.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }

    public void InsertImage()
    {
        DataTable DT = new DataTable();
        try
        {
            DT = GETDATA();
            if (DT.Rows.Count > 0)
            {
                for (int k = 0; k < DT.Rows.Count - 1; k++)
                {
                    //byte[] BPHOTO =Convert.ToByte(DT.Rows[k]["IMG"]);
                    sql = "insert into AVS1011(CUSTNO,DATEOFUPLOAD,PHOTO,STAGE,PHOTOSTATUS,RTIME,BRCD,MID,IMG_TYPE) values('" + DT.Rows[k]["CNO"].ToString() + "','11/08/2016'," + " :BlobParameter,1001,'1','11/08/2016','1','1111','" + DT.Rows[k]["IMG_TYPE"].ToString() + "' )";
                    SqlCommand cmnd = new SqlCommand(sql, conn.GetDBConnection());
                    //cmnd.Parameters.Add("BlobParameter", OracleDbType.Blob).Value = BPHOTO;
                    cmnd.ExecuteNonQuery();
                    cmnd.Dispose();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public DataTable GETDATA()
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select CNO,IMG,IMG_TYPE,U_ID,OID,ISNULL(AT,0) AT,ISNULL(AC,0) AC,ISNULL(SNO,0) SNO from IMAGE order by CNO";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MavsBanking"].ToString());
            SqlDataAdapter Adapt = new SqlDataAdapter(sql, conn);
            Adapt.Fill(DT);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }


    public void Delete_photosign(string Txtcustno, string BRCD) //BRCD ADDED --Abhishek
    {


        DbConnection conn = new DbConnection();
        try
        {
            sql = "update AVS1011 set STAGE ='1004' WHERE CUSTNO = '" + Txtcustno + "' AND STAGE !='1003' AND STAGE !='1004' and BRCD='" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }

    }


    public void Authorize_photosign(string Txtcustno, string BRCD) //BRCD ADDED --Abhishek
    {

        DbConnection conn = new DbConnection();
        try
        {
            string sql = " update AVS1011 set STAGE ='1003' WHERE  CUSTNO = '" + Txtcustno + "' AND STAGE !='1004' and BRCD='" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void AuthoriseImage(string FileName, int Stage, string BRCD)
    {
        DbConnection conn = new DbConnection();
        try
        {
            string sql = " update ImageMaster set STAGE ='1003' WHERE  IMAGENAME = '" + FileName + "' AND STAGE !='1004' and BRCD='" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void INSERTImage(string FileName, int Stage, string BRCD, string CUSTNO, string AccType, string ProdCode, string Type)
    {
        DbConnection conn = new DbConnection();
        try
        {
            string sql = "Insert into ImageMaster(CustNo,IMAGENAME,STAGE,BRCD,AccType,ProdCode,PhotType) values ('" + CUSTNO + "','" + FileName + "'," + Stage + ",'" + BRCD + "','" + AccType + "','" + ProdCode + "','" + Type + "')";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public DataTable GetAccType(string BRCD, string ACCNo, string GL)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "select * from avs_acc where AccNo='" + ACCNo + "' and BRCD='" + BRCD + "' and SubGLCODE='" + GL + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public DataTable GetData(string BRCD, string Cust)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "exec GetSrNo @BRCD='" + BRCD + "', @CustNo=" + Convert.ToInt32(Cust) + "";
            dt = conn.GetPhotoTable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public void UpdateImage(string Id, int Stage, string Type, string VID)
    {
        DbConnection conn = new DbConnection();
        try
        {
            string sql = "Exec UpdateStatus @Id=" + Id + ",@Stage=" + Stage + ",@Type='" + Type + "',@VID='" + VID + "'";
            conn.sExecuteQueryPH(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }

    public int PhotoAuth(string BRCD, string custno, string ID)
    {
        int res = 0;
        sql = "update imagerelation set Stage='1003' where BRCD='" + BRCD + "' and CustNo='" + custno + "' and ID='" + ID + "'";
        res = conn.sExecuteQueryPH(sql);
        return res;
    }


    public int GetStage(string ID, string BRCD, string custno)
    {
        int res = 0;
        sql = "select stage from imagerelation  where BRCD='" + BRCD + "' and CustNo='" + custno + "' and ID='" + ID + "'";
        res = Convert.ToInt32(conn.sExecuteScalarphoto(sql));
        return res;
    }
    public DataTable GetStage(string BRCD, string custno, string phototype, string cust)
    {
        DataTable DT = new DataTable();

        sql = "select id,Stage from imagemaster  where BRCD='" + BRCD + "' and CustNo='" + custno + "' and phottype='" + phototype + "'";
        DT = conn.GetPhotoTable(sql);
        return DT;
    }
    public void InsertDetails(string Flag, string FileName, string BRCD, string AccNo, string CustNo, string ID, string Type, Byte[] Code)
    {
        DbConnection conn = new DbConnection();
        try
        {
            string sql = "";

            sql = "Exec InsertDetails @Flag='" + Flag + "',@FileName='" + FileName + "',@BRCD='" + BRCD + "',@ACCNO='" + AccNo + "',@CustNo=" + CustNo + ",@Id=" + ID + ",@Type='" + Type + "',@Code='" + Code + "'";
            conn.sExecuteQueryPH(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void INSERTIMG(string Flag, string FileName, string BRCD, string AccNo, string ID, string Type, Byte[] Code)
    {
        DbConnection db = new DbConnection();

        SqlConnection Conn = new SqlConnection(conn.DbName());
        //SqlConnection Conn = new SqlConnection(db.DbName());

        try
        {
            string sql = "";
            if (Type == "1")
                sql = "update ImageRelation set SignIMG=@Code where Id='" + ID + "'";
            else
                sql = "update ImageRelation set PhotoIMG=@Code where Id='" + ID + "'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.Add("@Code", SqlDbType.Binary).Value = Code;
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
}