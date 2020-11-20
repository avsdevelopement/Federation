using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Text;
using DatabaseManager;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

public class Common
{

    #region " Variable Declaration "
    //private SqlCommand cmd;
    public SqlConnection con;
    //private SqlDataAdapter objAdapter;
    //private SqlDataReader objReader;
    #endregion

    #region " database connection for db dynamic"
    public void DBConnectionOpenDynamic(int IDAHO)
    {
        DBMasterConnectionOpen();
        //Connection objDBCon = new Connection();
        ////WebServiceDBConnection objDBCon = new WebServiceDBConnection();

        //int BranchID = Convert.ToInt16(System.Web.HttpContext.Current.Application["StateId"]);
        //if (BranchID != 0) {
        //    con = objDBCon.sqlCon(Convert.ToString(BranchID));
        //} else {
        //    if (IDAHO != 0) {
        //        con = objDBCon.sqlCon(Convert.ToString(IDAHO));
        //    } else {
        //        DBMasterConnectionOpen();
        //    }
        //}
    }

    private SqlConnection DBMasterConnectionOpen()
    {
        //DBConnection.MasterConnection objDBMCon = new DBConnection.MasterConnection();
        WebServiceDBConnection objDBMCon = new WebServiceDBConnection();

        con = objDBMCon.sqlCon();
        return con;
    }
    #endregion

    #region " Fill ComboBox "
    //Author     :- Arvind Warade
    //Date       :- 21/02/2012
    //Procedure  :- FillCombo
    //Parameter  :-  lst             -> Name of Control
    //               blnSelectAll    -> Print Default null Value 
    //               intSelectedId   -> Select default value of combo

    public void FillCombo(System.Web.UI.WebControls.DropDownList lst, ref tblAttributes objtblAttr, bool blnSelectAll, int intSelectedId, bool blnAutoPostBack, System.Web.UI.WebControls.DropDownList lst1, System.Web.UI.WebControls.DropDownList lst2, int IDAHO)
    {
        FillDataReader obj = new FillDataReader();
        SqlDataReader objReader = null;

        try
        {

            //If con Is Nothing Then
            DBConnectionOpenDynamic(IDAHO);
            //End If
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();

            objReader = obj.fn_FillDataReader(ref objtblAttr, con);

            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();

            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            if (blnSelectAll == true)
                lst.Items.Insert(0, "");

            if ((lst1 != null))
            {
                if ((objReader != null))
                    if (objReader.IsClosed == false)
                        objReader.Close();
                lst1.AutoPostBack = blnAutoPostBack;
                objReader = obj.fn_FillDataReader(ref objtblAttr, con);
                lst1.DataSource = objReader;
                lst1.DataTextField = objtblAttr.strDisplayField;
                lst1.DataValueField = objtblAttr.strValueField;
                lst1.DataBind();
                if (intSelectedId != 0)
                    lst1.SelectedValue = Convert.ToString(intSelectedId);
                if (blnSelectAll == true)
                    lst1.Items.Insert(0, "");
            }

            if ((lst2 != null))
            {
                if ((objReader != null))
                    if (objReader.IsClosed == false)
                        objReader.Close();
                lst2.AutoPostBack = blnAutoPostBack;
                objReader = obj.fn_FillDataReader(ref objtblAttr, con);
                lst2.DataSource = objReader;
                lst2.DataTextField = objtblAttr.strDisplayField;
                lst2.DataValueField = objtblAttr.strValueField;
                lst2.DataBind();

                if (intSelectedId != 0)
                    lst2.SelectedValue = Convert.ToString(intSelectedId);
                if (blnSelectAll == true)
                    lst2.Items.Insert(0, "");
            }



        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "fillcombo", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }

    public void FillCombo(System.Web.UI.WebControls.DropDownList lst, ref tblAttributes objtblAttr, bool blnSelectAll, int intSelectedId, bool blnAutoPostBack, System.Web.UI.WebControls.DropDownList lst1)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            //If con Is Nothing Then
            DBConnectionOpenDynamic(0);
            //End If
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);

            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();

            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            if (blnSelectAll == true)
                lst.Items.Insert(0, "");

            if ((lst1 != null))
            {
                if ((objReader != null))
                    if (objReader.IsClosed == false)
                        objReader.Close();
                lst1.AutoPostBack = blnAutoPostBack;
                objReader = obj.fn_FillDataReader(ref objtblAttr, con);
                lst1.DataSource = objReader;
                lst1.DataTextField = objtblAttr.strDisplayField;
                lst1.DataValueField = objtblAttr.strValueField;
                lst1.DataBind();
                if (intSelectedId != 0)
                    lst1.SelectedValue = Convert.ToString(intSelectedId);
                if (blnSelectAll == true)
                    lst1.Items.Insert(0, "");
            }




        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "fillcombo", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }

    public void FillCombo(System.Web.UI.WebControls.DropDownList lst, ref tblAttributes objtblAttr, bool blnSelectAll, int intSelectedId, bool blnAutoPostBack, System.Web.UI.WebControls.DropDownList lst1, System.Web.UI.WebControls.DropDownList lst2)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            //If con Is Nothing Then
            DBConnectionOpenDynamic(0);
            //End If
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);

            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();

            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            if (blnSelectAll == true)
                lst.Items.Insert(0, "");

            if ((lst1 != null))
            {
                if ((objReader != null))
                    if (objReader.IsClosed == false)
                        objReader.Close();
                lst1.AutoPostBack = blnAutoPostBack;
                objReader = obj.fn_FillDataReader(ref objtblAttr, con);
                lst1.DataSource = objReader;
                lst1.DataTextField = objtblAttr.strDisplayField;
                lst1.DataValueField = objtblAttr.strValueField;
                lst1.DataBind();
                if (intSelectedId != 0)
                    lst1.SelectedValue = Convert.ToString(intSelectedId);
                if (blnSelectAll == true)
                    lst1.Items.Insert(0, "");
            }

            if ((lst2 != null))
            {
                if ((objReader != null))
                    if (objReader.IsClosed == false)
                        objReader.Close();
                lst2.AutoPostBack = blnAutoPostBack;
                objReader = obj.fn_FillDataReader(ref objtblAttr, con);
                lst2.DataSource = objReader;
                lst2.DataTextField = objtblAttr.strDisplayField;
                lst2.DataValueField = objtblAttr.strValueField;
                lst2.DataBind();

                if (intSelectedId != 0)
                    lst2.SelectedValue = Convert.ToString(intSelectedId);
                if (blnSelectAll == true)
                    lst2.Items.Insert(0, "");
            }



        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "fillcombo", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }

    public void FillCombo(System.Web.UI.WebControls.DropDownList lst, ref tblAttributes objtblAttr, bool blnSelectAll, int intSelectedId, bool blnAutoPostBack)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            //If con Is Nothing Then
            DBConnectionOpenDynamic(0);
            //End If
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);

            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();

            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            if (blnSelectAll == true)
                lst.Items.Insert(0, "");


        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "fillcombo", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }
    public void FillCombo1(System.Web.UI.WebControls.DropDownList lst, ref tblAttributes objtblAttr, bool blnSelectAll, int intSelectedId, bool blnAutoPostBack)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            //If con Is Nothing Then
            DBConnectionOpenDynamic(0);
            //End If
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);

            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();

            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            if (blnSelectAll == true)
                lst.Items.Insert(0, "-- Not Applicable --");




        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "fillcombo", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }
    #endregion

    #region " Fill ListBox Control "

    //Author     :- Arvind Warade
    //Date       :- 21/02/07
    //Procedure  :- FillCombo
    //Parameter  :-  lst             -> Name of Control
    //               intContentId    -> Task Id 
    //               OutPutString    -> Pass a Value to Stored Procedure
    //               blnSelectAll    -> Print Default null Value 
    //               intSelectedId   -> Select default value of combo

    public void FillListBox(System.Web.UI.WebControls.ListBox lst, ref tblAttributes objtblAttr, bool blnAutoPostBack, int IDAHO)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            DBConnectionOpenDynamic(IDAHO);
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();


        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "LB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }

        }
    }

    public void FillListBox(System.Web.UI.WebControls.ListBox lst, ref tblAttributes objtblAttr, bool blnAutoPostBack)
    {
        SqlDataReader objReader = null;
        try
        {
            FillDataReader obj = new FillDataReader();
            DBConnectionOpenDynamic(0);
            if (objReader != null)
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();


        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "LB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }

        }
    }
    #endregion

    #region " Fill RadioListBox Control "

    //Author     :- Arvind Warade
    //Date       :- 21/02/07
    //Procedure  :- FillCombo
    //Parameter  :-  lst             -> Name of Control
    //               intContentId    -> Task Id 
    //               OutPutString    -> Pass a Value to Stored Procedure
    //               blnSelectAll    -> Print Default null Value 
    //               intSelectedId   -> Select default value of combo

    public void FillRadioListBox(System.Web.UI.WebControls.RadioButtonList lst, ref tblAttributes objtblAttr, bool blnAutoPostBack, int intSelectedId, int IDAHO)
    {
        SqlDataReader objReader = null;
        try
        {
            DBConnectionOpenDynamic(IDAHO);

            FillDataReader obj = new FillDataReader();
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.RepeatColumns = objtblAttr.intColumn;
            //lst.RepeatDirection = '1'; // Convert.ToSingle(objtblAttr.strRepeateDirection);
            lst.DataBind();


        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "RLB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }

    public void FillRadioListBox(System.Web.UI.WebControls.RadioButtonList lst, ref tblAttributes objtblAttr, bool blnAutoPostBack, int intSelectedId)
    {
        SqlDataReader objReader = null;
        try
        {
            DBConnectionOpenDynamic(0);

            FillDataReader obj = new FillDataReader();
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            lst.DataSource = objReader;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.RepeatColumns = objtblAttr.intColumn;
            //lst.RepeatDirection = objtblAttr.strRepeateDirection;
            lst.DataBind();


        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "RLB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }
        }
    }

    #endregion

    #region " Fill CheckListBox  Control "

    //Author     :- Arvind Warade
    //Date       :- 21/02/07
    //Procedure  :- FillCombo
    //Parameter  :-  lst             -> Name of Control
    //               intContentId    -> Task Id 
    //               OutPutString    -> Pass a Value to Stored Procedure
    //               blnSelectAll    -> Print Default null Value 
    //               intSelectedId   -> Select default value of combo

    public void FillchkListBox(System.Web.UI.WebControls.CheckBoxList lst, ref tblAttributes objtblAttr, bool blnAutoPostBack, int intSelectedId, int IDAHO)
    {
        SqlDataReader objReader = null;
        try
        {
            DBConnectionOpenDynamic(IDAHO);
            FillDataReader obj = new FillDataReader();
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            lst.DataSource = objReader;
            lst.RepeatColumns = objtblAttr.intColumn;
            //lst.RepeatDirection = objtblAttr.strRepeateDirection;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();
        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "CLB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }

        }
    }

    public void FillchkListBox(System.Web.UI.WebControls.CheckBoxList lst, ref tblAttributes objtblAttr, bool blnAutoPostBack, int intSelectedId)
    {
        SqlDataReader objReader = null;
        try
        {
            DBConnectionOpenDynamic(0);
            FillDataReader obj = new FillDataReader();
            if ((objReader != null))
                if (objReader.IsClosed == false)
                    objReader.Close();
            objReader = obj.fn_FillDataReader(ref objtblAttr, con);
            lst.AutoPostBack = blnAutoPostBack;
            if (intSelectedId != 0)
                lst.SelectedValue = Convert.ToString(intSelectedId);
            lst.DataSource = objReader;
            lst.RepeatColumns = objtblAttr.intColumn;
            //lst.RepeatDirection = objtblAttr.strRepeateDirection;
            lst.DataTextField = objtblAttr.strDisplayField;
            lst.DataValueField = objtblAttr.strValueField;
            lst.DataBind();
        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "CLB", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (objReader != null)
            {
                if (objReader.IsClosed == false) objReader.Close();
            }

        }
    }
    #endregion

    //#region " Data Reader "


    //#endregion

    #region " DataView "

    public DataView getDataView(ref tblAttributes objtblAttr, int IDAHO)
    {
        System.Data.DataView dv = new System.Data.DataView();
        try
        {
            DBConnectionOpenDynamic(IDAHO);
            FillDataView obj = new FillDataView();
            dv = obj.getDataView(ref objtblAttr, con);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "dataview", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        return dv;
    }

    public DataView getDataView(ref tblAttributes objtblAttr)
    {
        System.Data.DataView dv = new System.Data.DataView();
        try
        {
            DBConnectionOpenDynamic(0);
            FillDataView obj = new FillDataView();
            dv = obj.getDataView(ref objtblAttr, con);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "dataview", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }


        }

        return dv;
    }
    #endregion

    #region " DataSet  "
    public DataSet getDataSet(ref tblAttributes objtblAttr, int IDAHO)
    {

        DataSet objDataset = null;
        try
        {
            DBConnectionOpenDynamic(IDAHO);
            FillDataSetUsingCombo obj = new FillDataSetUsingCombo();
            objDataset = obj.getDataSet(ref objtblAttr, con);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "dataset", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

        }

        return objDataset;

    }

    public DataSet getDataSet(ref tblAttributes objtblAttr)
    {
        DataSet objDataset = null;
        try
        {
            DBConnectionOpenDynamic(0);
            FillDataSetUsingCombo obj = new FillDataSetUsingCombo();
            objDataset = obj.getDataSet(ref objtblAttr, con);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "dataset", ex.Message);
        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

        }

        return objDataset;

    }
    #endregion

    #region " Save ,Update ,Delete "
    public string SaveUpdateDelete(ref tblAttributes objtblAttr, int IDAHO)
    {
        string intchk = "0";
        try
        {
            DBConnectionOpenDynamic(IDAHO);
            SaveUpdateDelete obj = new SaveUpdateDelete();
            intchk = obj.fn_SaveUpdateDelete(con, ref  objtblAttr);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
            {

                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "SUD", ex.Message);
            }

        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

        }
        return intchk;
    }
    public string SaveUpdateDelete(ref tblAttributes objtblAttr)
    {
        string intchk = "0";
        try
        {
            DBConnectionOpenDynamic(0);
            SaveUpdateDelete obj = new SaveUpdateDelete();
            intchk = obj.fn_SaveUpdateDelete(con, ref objtblAttr);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
            {

                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "SUD", ex.Message);
            }

        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        return intchk;
    }

    public string SaveUpdateDeleteWithoutTrans(ref tblAttributes objtblAttr)
    {
        string intchk = "0";
        try
        {
            DBConnectionOpenDynamic(0);
            SaveUpdateDelete obj = new SaveUpdateDelete();
            intchk = obj.fn_SaveUpdateDeleteWithoutTrans(con, ref objtblAttr);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
            {

                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "SUD", ex.Message);
            }

        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        return intchk;
    }

    public string SaveUpdateDelete(ref tblAttributes objtblAttr, Byte[] a1, Byte[] a2)
    {
        string intchk = "0";
        try
        {
            DBConnectionOpenDynamic(0);
            SaveUpdateDelete obj = new SaveUpdateDelete();
            intchk = obj.fn_SaveUpdateDelete(con, ref objtblAttr);

        }
        catch (Exception ex)
        {
            if (objtblAttr.strProc != "sp_getIUDErrorDetails")
                saveErrorDetails(objtblAttr.strQuery, objtblAttr.strProc, returnStringFromArr(objtblAttr.strOutputStringArr), HttpContext.Current.Request.RawUrl, "SUD", ex.Message);

        }
        finally
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        return intchk;
    }
    #endregion

    #region " returnJsString "
    public string returnJsString(System.Data.DataView dvJs, bool IsRequiredJsScript, string strJSName)
    {
        string strJs = "";
        if (IsRequiredJsScript == true) strJs = strJs + "<script language='javascript'>";
        strJs = strJs + " var objJs=[";
        if (dvJs != null)
        {
            if (dvJs.Count > 0)
            {
                if (dvJs.Count == 1)
                {
                    strJs = strJs + dvJs[0][0].ToString();
                }
                else
                {
                    string str = "";
                    for (int i = 0; i < dvJs.Count; i++)
                    {
                        if (i == 0)
                        {
                            str = dvJs[i][0].ToString();
                        }
                        else
                        {
                            str = str + "," + dvJs[i][0].ToString();
                        }
                        strJs = str;
                    }
                }
            }
        }
        strJs = strJs + " ];" + strJSName;
        if (IsRequiredJsScript == true) strJs = strJs + "</script>";
        return strJs;
    }
    #endregion

    #region " ErroDetails "
    private void saveErrorDetails(string strQuery, string strProc, string strOP, string strURL, string strType, string strError)
    {
        try
        {
            tblAttributes objAttr = new tblAttributes();
            String[] temp = new String[6];
            if (strQuery != null)
                temp[0] = strQuery;
            else
                temp[0] = "";

            if (strProc != null)
                temp[1] = strProc;
            else
                temp[1] = "";

            if (strOP != null)
                temp[2] = strOP;
            else
                temp[2] = "";

            if (strURL != null)
                temp[3] = strURL;
            else
                temp[3] = "";

            if (strType != null)
                temp[4] = strType;
            else
                temp[4] = "";

            if (strError != "")
                temp[5] = strError;
            else
                temp[5] = "";
            objAttr.resizeArray(temp);
            objAttr.strProc = "sp_getIUDErrorDetails";
            SaveUpdateDelete(ref objAttr);
        }
        catch { }

    }

    #endregion

    private string returnStringFromArr(String[] strArr)
    {
        string strString = "";
        for (int i = 0; i < strArr.Length; i++)
        {
            if (i == 0)
                strString = strArr[i];
            else
                strString = strString + "#" + strArr[i];
        }
        return strString;
    }

}
