using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsOutClear
{
    string sql, sqlc, sqld;
    DbConnection conn = new DbConnection();

    public ClsOutClear()
	{
		
	}

    public int Get_CLG_GL_NO( string brcd)
    {
        string sql_clg_gl = "select CLG_GL_NO from OWG_PARAMETER WHERE BRCD='" + brcd + "' AND STAGE <>  1004";
        int CLG_GL_NO = Convert.ToInt32(conn.sExecuteScalar(sql_clg_gl));
        return CLG_GL_NO;
    }       

    public int GetNewSetNo(string brcd)
    {
        int SetNo = 1;
        sql = "(select (ISNULL(MAX(SET_NO),0) +TO_NUMBER('1')) FROM OWG_201607 WHERE BRCD = '" + brcd + "')";
        SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        return SetNo;
    }

    public int GetNewScrollNo(int setno, string brcd)
    {
        
        sql = "select (ISNULL(MAX(SCROLL_NO),0) +TO_NUMBER('1')) FROM OWG_201607 WHERE BRCD = '" + brcd + "' AND SET_NO='"+setno+"'";
        int ScrollNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        return ScrollNo;
    }


    public int GetcurrentSetNo(string brcd)
    {
        int SetNo = 1;
        sql = "(select MAX(SET_NO) FROM OWG_201607 WHERE BRCD = '" + brcd + "')";
        SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        return SetNo;
    }

    public int InsertNewSetNo(string Entrydate, string BRCD, string Procode, string AccNo, string AccTypeid, string OpTypeId, string partic, string bankcd, string brnchcd, string insttype, string instdate, string instno, string instamt, string MID, string PACMAC, int SetNo, int ScrollNo, int CLG_GL_NO)
    {
        sqlc = "INSERT INTO OWG_201607 (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '31/01/2016','DD-MM-YYYY'),'1','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + instdate + "','DD-MM-YYYY'),'" + instno + "','" + instamt + "','1001','" + MID + "','0','0','" + PACMAC + "','3', SYSDATE), 'C','" + SetNo + "', '" + ScrollNo + "')";
        sqld = "INSERT INTO OWG_201607 (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '31/01/2016','DD-MM-YYYY'),'1','" + Procode + "','" + CLG_GL_NO + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + instdate + "','DD-MM-YYYY'),'" + instno + "','" + instamt + "','1001','" + MID + "','0','0','" + PACMAC + "','3', SYSDATE), 'D','" + SetNo + "', '" + ScrollNo + "')";
        int result=conn.sExecuteQuery(sqlc);
        conn.sExecuteQuery(sqld);
        return result;
    }

    // Delete Entries
    public int DeleteOwgClearingEntry(int setno, string brcd)
    {
        int result = 0;
        sql = "UPDATE OWG_201607 SET STAGE='1004' WHERE SET_NO='" + setno + "' AND BRCD = '"+brcd+"' AND STAGE <> '1003' ";
        result = conn.sExecuteQuery(sql);
        return result;       
    }


    public void DeleteData()
    {
        string sqltruncate = "delete from OWG_201607_TEMP";
        string sqltruncatecommit = "commit";
        conn.sExecuteQuery(sqltruncate);
        conn.sExecuteQuery(sqltruncatecommit);
    }

    public void ReportData(string sbrcd)
    {
        
        DataTable dt = new DataTable();   
        sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from OWG_201607 OW " +
                " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO AND M.BRCD = ACC.BRCD " +
                " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1004' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, SCRL ";
        dt = conn.GetDatatable(sql);
        
        int j = 0;
        if (dt.Rows.Count > 1)
        {
            
        }
        int i = 0;
        if (dt.Rows.Count > 1)
        {
            i = dt.Rows.Count;
            while (i > 0)
                    {
                        string sqlinsert = "Insert into OWG_201607_TEMP VALUES ('" + dt.Rows[j][0] + "','" + dt.Rows[j][1] + "','" + dt.Rows[j][2] + "','" + dt.Rows[j][3] + "','" + dt.Rows[j][4] + "','" + dt.Rows[j][5] + "','" + dt.Rows[j][6] + "','" + dt.Rows[j][7] + "','" + Convert.ToDateTime(dt.Rows[j][8]).ToString("dd/MM/yyyy") + "','DD-MM-YYYY'),'" + dt.Rows[j][9] + "')";
                        conn.sExecuteQuery(sqlinsert);                        
                        i = i - 1;
                        j = j + 1;
                    }
        }
        

        int k = 0;
    }
}