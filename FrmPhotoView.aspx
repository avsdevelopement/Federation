<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPhotoView.aspx.cs" Inherits="FrmPhotoView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .zoom_img img
        {
            margin: 50px;
            height: 100px;
            width: 100px;
            -moz-transition: -moz-transform 0.5s ease-in;
            -webkit-transition: -webkit-transform 0.5s ease-in;
            -o-transition: -o-transform 0.5s ease-in;
        }

            .zoom_img img:hover
            {
                -moz-transform: scale(2);
                -webkit-transform: scale(2);
                -o-transform: scale(2);
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Photo & Signature
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>

                                            <div style="border: 1px solid #3598dc">
                                                <div class="row">
                                                    <div class="col-md-7">


                                                        <div class="col-md-10">

                                                            <div class="col-md-12" style="height: 40px; margin-top: 20px;">
                                                                <asp:Button ID="Scan" Text="Scan" runat="server" CssClass="fileinput-filename" />
                                                                <asp:ListBox ID="ListBox2" runat="server" Height="23px" Visible="false"></asp:ListBox>
                                                            </div>



                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Customer NO.</label>
                                                            </div>
                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:TextBox ID="Txtcustno" OnTextChanged="Txtcustno_TextChanged" Width="100px" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>

                                                            <br />

                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Customer Name</label>
                                                            </div>
                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:TextBox ID="Txtcustname" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">File Type</label>
                                                            </div>
                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:DropDownList ID="ddlDocType" CssClass="form-control" Width="150px" runat="server"></asp:DropDownList>
                                                            </div>

                                                            <br />

                                                            <%--<div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Upload Image</label>
                                                            </div>--%>


                                                           <%-- <div class="col-md-6" style="height: 51px;">
                                                                <asp:FileUpload ID="FileUpload1" CssClass="fileinput-filename" runat="server" Width="200" />
                                                            </div>--%>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-3 pull-right" style="background: rgba(53, 152, 220, 0.38); border: 1px solid rgb(152, 150, 150); height: 200px; margin-right: 30px; margin-top: 20px;">
                                                        Take Image
                                                        <asp:ImageButton ID="ImageB" runat="server" OnClick="ImageB_Click" />
                                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="150px" Width="200px" />
                                                        <div id="dvPreview">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div>
                                                    <div class="row" style="margin: 10px;">
                                                        <div class="col-md-6">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 10px;">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:Button ID="Preview" runat="server" Text="Priview" CssClass="btn btn-success" OnClick="Preview_Click" />
                                                        </div>
                                                        <div class="col-md-4">
                                                        
                                                                <%--<asp:Button ID="BtnUploadBulk" runat="server" Text="Upload Photos" CssClass="btn btn-primary" OnClick="BtnUploadBulk_Click" />--%>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <asp:Button ID="Report" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Report_Click" Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="alertModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <%--<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>--%>
                            <div class="zoom_img">
                                <asp:Image ID="imgPopup" runat="server" Width="450px" Height="450px" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

