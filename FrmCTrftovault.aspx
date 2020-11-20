<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCTrftovault.aspx.cs" Inherits="FrmCTrftovault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Transfer Of Denomination To Vault
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-6">
                                <label class="control-label col-md-2">User/Santion No: <span class="required">* </span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtUserId" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label col-md-2">Transfer Vault No : <span class="required">* </span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtTVault" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                            <legend>Denomination Table<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label> </legend>
                        </fieldset>
                    <div >
                        
                    </div>
</asp:Content>

