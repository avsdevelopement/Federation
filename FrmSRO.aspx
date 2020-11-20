<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSRO.aspx.cs" Inherits="FrmSRO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        #tblGoldLoan td {
            border: solid thin black;
        }

        #tblBranch td {
            border: solid thin black;
        }
        #tblSRO td {
            border: solid thin black;
        }
    </style>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        SRO Details
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Notice Type:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlNotic" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlNotic_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">SRO Master</asp:ListItem>
                                    <asp:ListItem Value="2">Demand Notice Details</asp:ListItem>
                                    <asp:ListItem Value="3">File Assign to SRO</asp:ListItem>
                                    <asp:ListItem Value="4">Follow Up Master</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

