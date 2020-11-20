        <%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51173.aspx.cs" Inherits="FrmAVS51173" %>

        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
            <style>
                .form-inline .form-group {
                    margin-right: 10px;
                }

                .well-primary {
                    color: rgb(255, 255, 255);
                    background-color: rgb(66, 139, 202);
                    border-color: rgb(53, 126, 189);
                }

                .glyphicon {
                    margin-right: 5px;
                }


                inset {
                    border-style: inset;
                }

                /*Media Query*/
                @media (min-width: 11px) {

                    .input-group {
                        margin-top: -8px;
                    }
                }

                .example-modal .modal {
                    position: relative;
                    top: auto;
                    bottom: auto;
                    right: auto;
                    left: auto;
                    display: block;
                    z-index: 1;
                }


                .example-modal .modal {
                    background: transparent !important;
                }
            </style>
            <script type="text/javascript">
                function ShowPopup(title, body) {

                    alert(title);

                    window.location = "FrmAVS51173.aspx";
                }
    </script>
            <script type="text/javascript">
                function FormatIt(obj) {

                    if (obj.value.length == 2) // Day
                        obj.value = obj.value + "/";
                    if (obj.value.length == 5) // month 
                        obj.value = obj.value + "/";
                    if (obj.value.length == 11) // year 
                    {
                        alert("Please Enter valid Date");
                        obj.value = "";
                    }
                }
            </script>
            <script>
                function isNumber(evt) {
                    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                        return false;
                    return true;
                }


                function OnltAlphabets(evt) {
                    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                        return true;

                    return false;
                }

                function CheckFirstChar(key, txt) {
                    if (key == 32 && txt.value.length <= 0) {
                        return false;
                    }
                    else if (txt.value.length > 0) {
                        if (txt.value.charCodeAt(0) == 32) {
                            txt.value = txt.value.substring(1, txt.value.length);
                            return true;
                        }
                    }
                    return true;
                }

            </script>
           

        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Printing & Stationary

                            </div>
                        </div>

                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="portlet-body">
                                                <div class="tab-pane active" id="tab__blue">
                                                    <ul class="nav nav-pills">
                                                        <li class="pull-right">
                                                            <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                            <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <div class="form-actions">
                                                            <div class="row">
                                                               <br />
                                                                <br />
                                                                <div class="col-md-offset-1 col-md-12">
                                                                    <asp:Button ID="btnVender" runat="server" Text="Vender Master" CssClass="btn btn-primary" OnClick="Vender_Click" TabIndex="49" />
                                                                    <asp:Button ID="btnProduct" runat="server" Text="Product Master" CssClass="btn btn-primary" OnClick="btnProduct_Click"  TabIndex="50" />
                                                                     <asp:Button ID="btnStock" runat="server" Text="Opening Stock" CssClass="btn btn-primary" TabIndex="53" onclick="btnStock_Click"/>
                                                                   
                                                                    <asp:Button ID="btnPurchase" runat="server" Text="Purchase Master" CssClass="btn btn-primary" OnClick="btnPurchase_Click" TabIndex="51" />
                                                                    <asp:Button ID="btnIssue" runat="server" Text="Sales" CssClass="btn btn-primary" TabIndex="52" OnClick="btnIssue_Click" />
                                                                       <asp:Button ID="btnUse" runat="server" Text="Use Stock" CssClass="btn btn-primary" TabIndex="54" OnClick="btnUse_Click" />
                                                                       <asp:Button ID="btnClosing" runat="server" Text="Closing Stock" CssClass="btn btn-primary" OnClick="btnClosing_Click" TabIndex="55" />

                                                                </div>
                                                                <br />
                                                                <br />
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                                <%--  <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-offset-3 col-md-9">
                                                            <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" data-toggle="modal" data-target="#modal-default" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                            <asp:Button ID="btnClear" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="50" />
                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="51" />
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            

            <%-- Product Master --%>

          

            

        </asp:Content>

