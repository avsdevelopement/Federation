using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fraagentcal : System.Web.UI.Page
{
    ClsOpenClose OP= new ClsOpenClose();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized ATH= new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsCommon CLScom = new ClsCommon();
    DataTable dt = new DataTable();
    int Result;
    double Coms;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

    }
    protected void agentname_txtchg(object sender, EventArgs e)
    {
        Result = OP.GetAgentexit(agent_no.Text, Session["BRCD"].ToString());
        if (Result == 0)
        {
            WebMsgBox.Show("Sorry Account Number Not Exist......!!", this.Page);
            agent_no.Text = "";
            Agent_name.Text = "";
            agent_no.Focus();
            return;
        }
          Agent_name.Text = OP.GetAgentName(agent_no.Text, Session["BRCD"].ToString());
          Start_date.Focus();
     
    }

    protected void collection_txtchg(object sender, EventArgs e)
    {
        string sdate = Start_date.Text;
        string fdate = last_date.Text;
       // double sd=Convert.ToDouble(sdate.Split('-')[0].ToString());
        //double ld=Convert.ToDouble(fdate.Split('-')[0].ToString());
         string[] arraydt = Session["EntryDAte"].ToString().Split('/');

        if (agent_no.Text == "")
        {
            agent_no.Focus();
            WebMsgBox.Show("Please Enter the Agent no", this.Page);
            return;
        }
        else
            if (Start_date.Text == "" || last_date.Text == "")
            {
                WebMsgBox.Show("Please ENTER the DATE OR TOTAL COLLECTION IS ZERO", this.Page);
               
            }
         else
                if (Convert.ToDouble(sdate.Split('-')[0].ToString()) > Convert.ToDouble(fdate.Split('-')[0].ToString()) || Convert.ToDouble(sdate.Split('-')[1].ToString()) > Convert.ToDouble(fdate.Split('-')[1].ToString()) || Convert.ToDouble(arraydt[2]) < Convert.ToDouble(fdate.Split('-')[0].ToString()) || Convert.ToDouble(arraydt[1]) < Convert.ToDouble(fdate.Split('-')[1].ToString()))
                {
                    WebMsgBox.Show("Please ENTER the DATE OR TOTAL COLLECTION IS ZERO", this.Page);
                    Start_date.Text = "";
                    Start_date.Focus();
                }
            else
            {
                
                // SA_name.Text = fdate.Split('-')[0].ToString();
                //SA_no.Text = fdate.Split('-')[1].ToString();
                //string ldate = Convert.ToDateTime(last_date.Text).ToString();

                double RC = OP.GetOpenClose("OPENING", fdate.Split('-')[0].ToString(), fdate.Split('-')[1].ToString(), "0", agent_no.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "6");
                Total_coll.Text = RC.ToString();
                //public double GetOpenClose(string Flag, string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT)
                 //double RC = OP.GetOpenClose("OPENING", fdate.Split('-')[0].ToString(), fdate.Split('-')[1].ToString(), agent_no.Text, "20031", Session["BRCD"].ToString(), ldate.Split(' ')[0].ToString()Session["EntryDate"].ToString());
                //Total_coll.Text = RC.ToString();

                commission.Text = "";
                com_amt.Text = "";
                td_ded.Text = "";
                TDamt.Text = "";
                net_commission.Text = "";
                commission.Focus();
                   
            }
      
        
        
       
    }
    protected void commission_txtchg(object sender, EventArgs e)
    {
        if (commission.Text == "" || commission.Text == "0")
        {
            WebMsgBox.Show("Please Enter the commission", this.Page);
            commission.Text = "";
            commission.Focus();
        }
        else if (Total_coll.Text == "" || Total_coll.Text == "0")
            {
                WebMsgBox.Show("Please ENTER the DATE OR TOTAL COLLECTION IS ZERO", this.Page);
                commission.Text = "";
                commission.Focus();
            }
        else
        {
            Coms = OP.Comamt(Total_coll.Text, commission.Text, Session["BRCD"].ToString());
            com_amt.Text = Coms.ToString();
            td_ded.Focus();
        }
    }
    protected void TDAmt_txtchg(object sender, EventArgs e)
    {
        if (com_amt.Text == "" || com_amt.Text =="0")
        {
            WebMsgBox.Show("COLLECTION IS ZERO or NUll ", this.Page);
            td_ded.Text = "";
            com_amt.Focus();
        }
        else
        {
            Coms = OP.TDamount(com_amt.Text, td_ded.Text, Session["BRCD"].ToString());
            TDamt.Text = Coms.ToString();
            Coms = OP.NetCommission(com_amt.Text, TDamt.Text, Session["BRCD"].ToString());
            net_commission.Text = Coms.ToString();
            Savingaccno.Focus();
        }
    }
    
    protected void saving_txtchg(object sender, EventArgs e)
    {
        Result = OP.GetSAaccexit(Savingaccno.Text, Session["BRCD"].ToString());
        if (Result == 0)
        {
            WebMsgBox.Show("Sorry Account Number Not Exist......!!", this.Page);
            SA_name.Text = "";
            SA_no.Text = "";
            Savingaccno.Focus();
        }
        int RC = OP.GetSAcustno(Savingaccno.Text, Session["BRCD"].ToString());
        if (RC < 0)
        {
            Savingaccno.Focus();
            WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
               
        }
      
        ViewState["SA_no"] = RC;
        SA_no.Text = RC.ToString();
        SA_name.Text = BD.GetCustName(RC.ToString(),Session["BRCD"].ToString());
    }
    protected void Clear0_Click(object sender, EventArgs e)
    {
        agent_no.Text = ""; 
        Agent_name.Text = "";
        Start_date.Text = "";
        last_date.Text = "";
        Total_coll.Text = "";
        commission.Text = "";
        com_amt.Text = ""; 
        td_ded.Text = "";
        TDamt.Text = "";
        net_commission.Text = "";
        Savingaccno.Text = "";
        SA_name.Text = "";
        SA_no.Text = "";
        agent_no.Focus();
    }
    protected void Pay0_Click(object sender, EventArgs e)
    {
        if (agent_no.Text == ""||com_amt.Text == ""||td_ded.Text == ""|| Savingaccno.Text == "")
        {
            WebMsgBox.Show("Sorry. PLease Enter requer fild", this.Page);
            agent_no.Focus();
            com_amt.Focus();
            Savingaccno.Focus();
            td_ded.Focus();
            return;
        }
        else
        {
            string scroll_no="";
           

            int glcode = CLScom.GetGLcode("1007", Session["BRCD"].ToString());
            int plcode = CLScom.GetPLaccno("1007", Session["BRCD"].ToString());
            int setno = Convert.ToInt32(conn.sExecuteScalar("SELECT MAX(LASTNO)FROM AVS1000 WHERE ACTIVITYNO=7"));

            int auther = ATH.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode.ToString(), plcode.ToString(), agent_no.Text.ToString(), net_commission.Text.ToString(), net_commission.Text.ToString(), net_commission.Text.ToString(), "1", "7", "Transfer", setno.ToString(), "", "", "", "", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "0", SA_no.Text.ToString(), SA_name.Text.ToString(), "0", "0");
            if (auther == 1)
            {
                WebMsgBox.Show("Transfer is complete'", this.Page);
            }
          
           /* 
            agent_no.Text = "";
            Agent_name.Text = "";
            Start_date.Text = "";
            last_date.Text = "";
            Total_coll.Text = "";
            commission.Text = "";
            com_amt.Text = "";
            td_ded.Text = "";
            TDamt.Text = "";
            net_commission.Text = "";
            Savingaccno.Text = "";
            SA_name.Text = "";
            SA_no.Text = "";
            agent_no.Focus();
            */
       /*   WebMsgBox.Show("validation is OK",this.Page);
           ?*/
        }
    }
}