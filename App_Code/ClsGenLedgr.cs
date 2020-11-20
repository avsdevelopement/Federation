using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsGenLedgr
{
    DataTable dt = new DataTable();
    ClsOpenClose OC = new ClsOpenClose();
    decimal amount = new decimal();
    string sql="";
    DbConnection conn = new DbConnection();
    decimal ClBalance = 0;
    ClsCommon CMN = new ClsCommon();

	public ClsGenLedgr()
	{	
	}

    // General Ledger Daily
    public DataTable GetGenLedgrm(string FD, string TD, string GL, string SGL, string BRCD)
    {
        try 
        {
        // Add columns to datatable
        dt.Columns.Add("date");
        dt.Columns.Add("openingbal");
        dt.Columns.Add("payment");
        dt.Columns.Add("receipt");
        dt.Columns.Add("closingbal");
        dt.Columns.Add("type");

        // Get all days dates in array
        DateTime Fromdate=Convert.ToDateTime(FD);
        DateTime Todate=Convert.ToDateTime(TD);

        double count   =(Todate - Fromdate).TotalDays;
        DateTime [] AllDates= new DateTime[(Convert.ToInt32(count)+2)];
        AllDates[0] = Fromdate;        
        int i=0;
        while (Fromdate <= Todate)
        {
            dt.Rows.Add();
            dt.Rows[i]["date"] = Fromdate;
            string[] curentdate = Fromdate.ToString().Split(' ');
            string[] dtsplit = curentdate[0].ToString().Split('/');

            // Add opening balance
            decimal OpBalance = OC.GetOpenCloseGL("OPENING", dtsplit[2].ToString(), dtsplit[1].ToString(), SGL, BRCD, Fromdate.ToString(), GL);
            // Add Closing balance to table
            if (i == 0)
            {
                ClBalance = OC.GetOpenCloseGL("CLOSING", dtsplit[2].ToString(), dtsplit[1].ToString(), SGL, BRCD, Fromdate.ToString(), GL);
            }

            if (i > 0)
            {
                OpBalance = Convert.ToDecimal(dt.Rows[i - 1]["closingbal"]);
            }

            // Get Receipt
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit[2].ToString() + "" + dtsplit[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND ENTRYDATE='" + curentdate[0].ToString() + "' AND TRXTYPE='1' AND STAGE <>'1004'";
            string result = conn.sExecuteScalar(sql);
            decimal RECEIPT = Convert.ToDecimal(result);

            // get Payment
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit[2].ToString() + "" + dtsplit[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND ENTRYDATE='" + curentdate[0].ToString() + "' AND TRXTYPE='2' AND STAGE <>'1004'";
            result = conn.sExecuteScalar(sql);
            decimal PAYMENT = Convert.ToDecimal(result);

            // Claculate closing balance
            ClBalance = OpBalance - PAYMENT + RECEIPT;
            // Get TYPE (DEBIT / CREDIT)
            string type = "";
            if (ClBalance < 0)
            {
                type = "DR";
            }
            else
            {
                type = "CR";
            }

            // Adding values to datatable
            if (i == 0)
            {
                dt.Rows[i]["openingbal"] = OpBalance;
            }
            else
            {
                dt.Rows[i]["openingbal"] = dt.Rows[i-1]["closingbal"];
            }
            dt.Rows[i]["closingbal"] = ClBalance;
            dt.Rows[i]["receipt"] = RECEIPT;
            dt.Rows[i]["payment"] = PAYMENT;
            dt.Rows[i]["TYPE"]=type;

            i = i + 1;
            Fromdate = Fromdate.AddDays(1);
        }
        // End  

        // Remove rows which have 0 payment and receipt 
        //int rcount = dt.Rows.Count;
        //for (i = 0; i < rcount; i++)
        //{
        //    if (Convert.ToDecimal(dt.Rows[i]["receipt"]) == 0 && Convert.ToDecimal(dt.Rows[i]["payment"]) == 0)
        //    {
        //        dt.Rows.RemoveAt(i);
        //        rcount=rcount - 1;
        //        i = i - 1;
        //    }
        //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return dt;
    }

    // general Ledger Monthly
    public DataTable GetGenLedgrmonthly(string FD, string TD, string GL, string SGL, string BRCD)
    {
        int monthcount=0;
        try 
        {
        // Add columns to datatable
        dt.Columns.Add("date");
        dt.Columns.Add("openingbal");
        dt.Columns.Add("payment");
        dt.Columns.Add("receipt");
        dt.Columns.Add("closingbal");
        dt.Columns.Add("type");
        dt.Columns.Add("listMY");
        dt.Columns.Add("startdate");
        dt.Columns.Add("enddate");
       
        // Now add first date and last date for all dates
        DataTable DtDate= CMN.GetMonthYearList(FD,TD);

        // get row count from dataset
        int DatedtRowcount=DtDate.Rows.Count;
        string[] dtsplit1L;
        string[] dtsplit1F;
        int lastdayL;
        string lastdateL;
        string firstdateF;
        for (int l=0; l<DatedtRowcount ; l++)
        {            
            dt.Rows.Add();
            if(l==0)
            {
                dt.Rows[0]["listMY"]=DtDate.Rows[0][0].ToString();
                
                // Assign first date
                dt.Rows[0]["startdate"]=FD;
                
                // Assign Last date
                dtsplit1L = FD.ToString().Split('/');
                // Get last date of this FIRST months
                lastdayL = DateTime.DaysInMonth(Convert.ToInt32(dtsplit1L[2]), Convert.ToInt32(dtsplit1L[1]));
                lastdateL = lastdayL.ToString() + "/" + dtsplit1L[1].ToString() + "/" + dtsplit1L[2].ToString();
                dt.Rows[0]["enddate"]=lastdateL;
                dt.Rows[0]["date"] = lastdateL;
                // NOW get OPENING BALANCE for first record
                string NewDate = Convert.ToDateTime(FD).AddDays(-1).ToString();
                string[] SplitStartdateSpace = NewDate.Split(' ');
                string[] SplitStartdate = SplitStartdateSpace[0].Split('/');
                decimal OpBalance = OC.GetOpenCloseGL("CLOSING", SplitStartdate[2].ToString(), SplitStartdate[1].ToString(), SGL, BRCD, NewDate, GL);
                dt.Rows[0]["openingbal"]=OpBalance;

                // Get OPENING, PAYMENT, RECEIPT, CLOSING Values To datatable
                DatatblMonthlyLedgr(dt, GL, SGL, BRCD, 0);
            }
            else if(l==(DatedtRowcount-1))
            {
                dt.Rows[(l)]["listMY"] = DtDate.Rows[l][0].ToString();

                
                dtsplit1F = TD.ToString().Split('/');
                firstdateF = "01/" + dtsplit1F[1].ToString() + "/" + dtsplit1F[2].ToString();

                // Assign First date
                dt.Rows[l]["startdate"] = firstdateF;
                // Assign Last date
                dt.Rows[l]["enddate"] = TD;
                dt.Rows[l]["date"] = TD;

                // Get OPENING, PAYMENT, RECEIPT, CLOSING Values To datatable
                DatatblMonthlyLedgr(dt, GL, SGL, BRCD, l);
            }
            else
            {
                dt.Rows[(l)]["listMY"] = DtDate.Rows[l][0].ToString();

                // Now Split Month and year
                dtsplit1L = dt.Rows[(l)]["listMY"].ToString().Split(' ');

                // Get last date of this FIRST months
                lastdayL = DateTime.DaysInMonth(Convert.ToInt32(dtsplit1L[1]), Convert.ToInt32(dtsplit1L[0]));

                // Assign First date
                dt.Rows[l]["startdate"] = "01/" + dtsplit1L[0] + "/" + dtsplit1L[1];

                // Assign Last date
                dt.Rows[l]["enddate"] = lastdayL + "/" + dtsplit1L[0] + "/" + dtsplit1L[1];
                dt.Rows[l]["date"] = dt.Rows[l]["enddate"].ToString();

                // Get OPENING, PAYMENT, RECEIPT, CLOSING Values To datatable
                DatatblMonthlyLedgr(dt, GL, SGL, BRCD,l);
            }
        }

        return dt;

        // Get all days dates in array
        DateTime Fromdate = Convert.ToDateTime(FD);
        DateTime Todate = Convert.ToDateTime(TD);
        // Split date
        string[] dtsplit1 = FD.ToString().Split('/');
        // Get last date of this FIRST months
        int lastday = DateTime.DaysInMonth(Convert.ToInt32(dtsplit1[2]), Convert.ToInt32(dtsplit1[1]));
        string lastdate = lastday.ToString() + "/" + dtsplit1[1].ToString() + "/" + dtsplit1[2].ToString();

        // Get Values for first month
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + FD.ToString() + "' AND '" + lastdate.ToString() + "') AND TRXTYPE='1' AND STAGE <>'1004'";
        string result1 = conn.sExecuteScalar(sql);


        // Get Receipt
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + FD.ToString() + "' AND '" + lastdate.ToString() + "') AND TRXTYPE='1' AND STAGE <>'1004'";
        string result = conn.sExecuteScalar(sql);
        decimal RECEIPT = Convert.ToDecimal(result);

        // get Payment
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + FD.ToString() + "' AND '" + lastdate.ToString() + "') AND TRXTYPE='2' AND STAGE <>'1004'";
        result = conn.sExecuteScalar(sql);
        decimal PAYMENT = Convert.ToDecimal(result);
                
        dt.Rows.Add();
        dt.Rows[0]["date"] = lastdate;
        dt.Rows[0]["openingbal"] = result1;
        dt.Rows[0]["receipt"] = RECEIPT;
        dt.Rows[0]["payment"] = PAYMENT;
        dt.Rows[0]["closingbal"] = Convert.ToDecimal(result1) - RECEIPT + PAYMENT;
        if(Convert.ToDecimal(dt.Rows[0]["closingbal"]) >0)
        {
            dt.Rows[0]["type"] ="CR";
        }
        else
        {
            dt.Rows[0]["type"] ="DR";
        }

        // Split date
        string[] dtsplit2 = lastdate.ToString().Split('/');

        // Now get next month
        string NextMonth="";

        // Get last date of this FIRST months
        int lastday1 = DateTime.DaysInMonth(Convert.ToInt32(dtsplit2[2]), Convert.ToInt32(dtsplit2[1]));
        string lastdate1 = lastday.ToString() + "/" + dtsplit2[1].ToString() + "/" + dtsplit2[2].ToString();

        // Now get Next Month value
        lastdate = Convert.ToDateTime(lastdate).AddMonths(1).ToString();

        // pass new date for next iteration        
        while (Convert.ToDateTime(lastdate) > Convert.ToDateTime(TD) )
        {
            
            // Now get Next Month value
            string CurrentMonthLastdate = Convert.ToDateTime(lastdate).AddMonths(1).ToString();
            string splitcdate = CurrentMonthLastdate.Split(' ').ToString();
            string splitdatespace = splitcdate[0].ToString().Split('/').ToString();
            string CurrentMonthFirstdate = splitcdate[0] + "/" + splitcdate[1] + "/" + splitcdate[2];
        }

        // get starting date of LAST MONTH
        string[] dtsplit3 = TD.ToString().Split('/');
        string datefirst = "01/" + dtsplit3[1].ToString() + "/" + dtsplit3[2].ToString();

        double count = (Todate - Fromdate).TotalDays;
        DateTime[] AllDates = new DateTime[(Convert.ToInt32(count) + 2)];
        AllDates[0] = Fromdate;
        int i = 0;
        

        // get date parameters for query
        string FinFromDate = "";
        string FinToDate = "";
        int currentMonth=0;
        int currentYear=0;
        string[] Findtsplit = FinFromDate.ToString().Split('/');
        
        int k=0;

        while(currentMonth > Convert.ToInt32(dtsplit2[1].ToString()) && currentYear > Convert.ToInt32(dtsplit2[2].ToString()))
        {
            
        }

        // Finally Get OPENING balance
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + Findtsplit[2].ToString() + "" + Findtsplit[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + FinFromDate.ToString() + "' AND '" + FinToDate.ToString() + "') AND TRXTYPE='1' AND STAGE <>'1004'";
        string Finresult = conn.sExecuteScalar(sql);


        // Get data for first Month
        if (i == 0)
        {
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + FD.ToString() + "' AND '" + lastdate.ToString() + "') AND TRXTYPE='1' AND STAGE <>'1004'";
            string result0 = conn.sExecuteScalar(sql);
        }

        // get data for last month
        else if(i==monthcount)
        {
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + datefirst + "' AND '" + TD.ToString() + "') AND TRXTYPE='1' AND STAGE <>'1004'";
            string result2 = conn.sExecuteScalar(sql);
        }

        // get data for inbetween months
        else if(i<monthcount)
        {
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit1[2].ToString() + "" + dtsplit1[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND TRXTYPE='1' AND STAGE <>'1004'";
            string result3 = conn.sExecuteScalar(sql);
        }

        while (Fromdate <= Todate)
        {
            dt.Rows.Add();
            dt.Rows[i]["date"] = Fromdate;
            string[] curentdate = Fromdate.ToString().Split(' ');
            string[] dtsplit = curentdate[0].ToString().Split('/');

            // Add opening balance
            decimal OpBalance = OC.GetOpenCloseGL("OPENING", dtsplit[2].ToString(), dtsplit[1].ToString(), SGL, BRCD, Fromdate.ToString(), GL);
            // Add Closing balance to table
            if (i == 0)
            {
                ClBalance = OC.GetOpenCloseGL("CLOSING", dtsplit[2].ToString(), dtsplit[1].ToString(), SGL, BRCD, Fromdate.ToString(), GL);
            }

            if (i > 0)
            {
                OpBalance = Convert.ToDecimal(dt.Rows[i - 1]["closingbal"]);
            }

            // Get Receipt
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit[2].ToString() + "" + dtsplit[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND ENTRYDATE='" + curentdate[0].ToString() + "' AND TRXTYPE='1' AND STAGE <>'1004'";
           // string result = conn.sExecuteScalar(sql);
            //decimal RECEIPT = Convert.ToDecimal(result);

            // get Receipt
            sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + dtsplit[2].ToString() + "" + dtsplit[1].ToString() + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND ENTRYDATE='" + curentdate[0].ToString() + "' AND TRXTYPE='2' AND STAGE <>'1004'";
            result = conn.sExecuteScalar(sql);
            //decimal PAYMENT = Convert.ToDecimal(result);

            // Claculate closing balance
            ClBalance = OpBalance - PAYMENT + RECEIPT;
            // Get TYPE (DEBIT / CREDIT)
            string type = "";
            if (ClBalance < 0)
            {
                type = "DR";
            }
            else
            {
                type = "CR";
            }

            // Adding values to datatable
            if (i == 0)
            {
                dt.Rows[i]["openingbal"] = OpBalance;
            }
            else
            {
                dt.Rows[i]["openingbal"] = dt.Rows[i - 1]["closingbal"];
            }
            dt.Rows[i]["closingbal"] = ClBalance;
            dt.Rows[i]["receipt"] = RECEIPT;
            dt.Rows[i]["payment"] = PAYMENT;
            dt.Rows[i]["TYPE"] = type;

            i = i + 1;
            Fromdate = Fromdate.AddDays(1);
        }
        // End  

        // Remove rows which have 0 payment and receipt 
        //int rcount = dt.Rows.Count;
        //for (i = 0; i < rcount; i++)
        //{
        //    if (Convert.ToDecimal(dt.Rows[i]["receipt"]) == 0 && Convert.ToDecimal(dt.Rows[i]["payment"]) == 0)
        //    {
        //        dt.Rows.RemoveAt(i);
        //        rcount=rcount - 1;
        //        i = i - 1;
        //    }
        //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return dt;
    }

    public void AddToDataTable(DataTable dt, string startdate, string lastdate, string GL, string SGL, string BRCD)
    {
        dt.Rows.Add();

    }

    // Now get OPENING BALANCE, PAYMENT, RECEIPT and print
    public void DatatblMonthlyLedgr(DataTable dt, string GL, string SGL, string BRCD, int l)
    {
        try 
        {
        //Get Month and year
        string[] splitMY = dt.Rows[l]["listMY"].ToString().Split( );

        // Get Receipt
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + splitMY[1] + "" + splitMY[0] + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + dt.Rows[l]["startdate"] + "' AND '" + dt.Rows[l]["enddate"] + "') AND TRXTYPE='1' AND STAGE <>'1004'";
        string result = conn.sExecuteScalar(sql);
        decimal RECEIPT = Convert.ToDecimal(result);
        dt.Rows[l]["receipt"]=RECEIPT;

        // get Payment
        sql = "SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT from AVSM_" + splitMY[1] + "" + splitMY[0] + " where BRCD='" + BRCD + "' AND GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND (ENTRYDATE BETWEEN '" + dt.Rows[l]["startdate"] + "' AND '" + dt.Rows[l]["enddate"] + "') AND TRXTYPE='2' AND STAGE <>'1004'";
        result = conn.sExecuteScalar(sql);
        decimal PAYMENT = Convert.ToDecimal(result);
        dt.Rows[l]["payment"] = PAYMENT;

        if (l == 0)
        {
            decimal closingbalance = Convert.ToDecimal(dt.Rows[l]["openingbal"]) - PAYMENT + RECEIPT;
            dt.Rows[l]["closingbal"] = closingbalance;
            if(closingbalance >0)
            {
                dt.Rows[l]["type"] = "CR";
            }
            else if (closingbalance > 0)
            {
                dt.Rows[l]["type"] = "DR";
            }
            
        }
        else if (l != 0)
        {
            dt.Rows[l]["openingbal"] = dt.Rows[l - 1]["closingbal"];
            decimal closingbalance = Convert.ToDecimal(dt.Rows[l]["openingbal"]) - PAYMENT + RECEIPT;
            dt.Rows[l]["closingbal"] = closingbalance;
            if (closingbalance > 0)
            {
                dt.Rows[l]["type"] = "CR";
            }
            else if (closingbalance > 0)
            {
                dt.Rows[l]["type"] = "DR";
            }
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
    }

    // Get Payment
    public decimal GetPayment(string GL, string SGL, string BRCD, string EDT)
    {
        try 
        {
        string[] curentdate = EDT.ToString().Split(' ');
        string[] dtsplit = curentdate[0].ToString().Split('/');
        string TBLNM = "AVSM_" + dtsplit[2].ToString() + "" + dtsplit[1].ToString();
        sql = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return amount;
    }

}