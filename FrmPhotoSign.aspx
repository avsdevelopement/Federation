<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPhotoSign.aspx.cs" Inherits="FrmPhotoSign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link rel="stylesheet" href="https://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css">
    <script src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="https://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#FileUpload1").change(function () {
                $("#dvPreview").html("");
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                if (regex.test($(this).val().toLowerCase())) {
                    if ($.browser.msie && parseFloat(jQuery.browser.version) <= 9.0) {
                        $("#dvPreview").show();
                        $("#dvPreview")[0].filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = $(this).val();
                    }
                    else {
                        if (typeof (FileReader) != "undefined") {
                            $("#dvPreview").show();
                            $("#dvPreview").append("<img />");
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                $("#dvPreview img").attr("src", e.target.result);
                            }
                            reader.readAsDataURL($(this)[0].files[0]);
                        } else {
                            alert("This browser does not support FileReader.");
                        }
                    }
                } else {
                    alert("Please upload a valid image file.");
                }
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 208px;
        }
    </style>

    <script type="text/javascript">
        function Validate() {
            var custno = document.getElementById('<%=Txtcustno.ClientID%>').value;
            var Txtcustname = document.getElementById('<%=Txtcustname.ClientID%>').value;
            var message = '';

            if (custno == "") {
                //alert("please enter Cust No.......!!!")
                message = 'Please Enter Customer No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show');
                document.getElementById('<%=Txtcustno.ClientID%>').focus();
                return false;
            }
            if (Txtcustname == "") {
                //alert("please enter Cust No.......!!!")
                message = 'Please Enter Customer Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show');
                document.getElementById('<%=Txtcustname.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Save" />
        </Triggers>
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
                                                <ul class="nav nav-pills">
                                                    <li>
                                                        <asp:LinkButton ID="Save" runat="server" Text="Save" class="btn btn-default" OnClick="BtnSave_Click" OnClientClick="Javascript:return Validate();" Style="border: 1px solid #3561dc;"><i class="fa fa-plus-circle"></i> Save </asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <asp:LinkButton ID="Delete" runat="server" Text="Delete" class="btn btn-default" OnClick="BtnDelete_Click" OnClientClick="javascript:return Validate();" Style="border: 1px solid #3561dc;"><i class="fa fa-pencil-square-o"></i> Delete </asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <asp:LinkButton ID="Authorize" runat="server" Text="Authorize" OnClick="BtnAuthorize_Click" class="btn btn-default" OnClientClick="javascript:return Validate();" Style="border: 1px solid #3561dc;"><i class="fa fa-times"></i> Authorize </asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <asp:LinkButton ID="PreviewPhoto" runat="server" Text="PreviewPhoto" OnClick="PreviewPhoto_Click" class="btn btn-default" OnClientClick="javascript:return Validate();" Style="border: 1px solid #3561dc;"><i class="fa fa-times"></i> PreviewPhoto </asp:LinkButton>
                                                    </li>

                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
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
                                                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>


                                                            </div>

                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">File Type</label>
                                                            </div>
                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:DropDownList ID="ddlDocType" CssClass="form-control" Width="150px" runat="server"></asp:DropDownList>
                                                            </div>

                                                            <br />

                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Upload Image</label>
                                                            </div>


                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:FileUpload ID="FileUpload1" CssClass="fileinput-filename" runat="server" Width="200" />
                                                            </div>
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
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row">
                                                    <div class="col-md-7">
                                                        <div class="col-md-10">
                                                            <div class="col-md-6">
                                                           
                                                                <label class="control-label ">File Type</label>
                                                            </div>
                                                            <div class="col-md-6" style="margin-top:10PX">
                                                                <asp:DropDownList ID="ddlDocType1" CssClass="form-control" Width="150px" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                     
                                                    </div>
                                                </div>
                                                  <div class="form-actions">
                                <div class="row">
                                  <div class="col-md-7">
                                                        <div class="col-md-10">
                                                            <div class="col-md-6">
                                       
                                    </div>
                                 <div class="col-md-6">
                                      <asp:Button ID="BTNKYC" runat="server"  Text="KYC" CssClass="btn btn-primary" OnClick= "BTNKYC_Click" OnClientClick="Javascript:return isvalidate();" />  
                                      <asp:Button ID="Exist" runat="server" Text="Exit" CssClass="btn btn-success" />
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
                <div class="row">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdMaster" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                            OnPageIndexChanging= "grdMaster_PageIndexChanging"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="CUSTNO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                <asp:TemplateField HeaderText="PHOTO_TYPE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PHOTO_TYPE" runat="server" Text='<%# Eval("PHOTO_TYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="DATEOFUPLOAD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DATEOFUPLOAD" runat="server" Text='<%# Eval("DATEOFUPLOAD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
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
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            <%--<asp:Image ID="imgPopup" runat="server" Width="450px" Height="450px" />--%>
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
