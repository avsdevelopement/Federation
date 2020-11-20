<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDocDetails.aspx.cs" Inherits="FrmDocDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Validate() {
            var TxtAccT = document.getElementById('<%=txtRef.ClientID%>').value;
            if (TxtAccT == "") {
                alert("Please enter Ref No......!!");
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Document Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Ref No<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtRef" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();"/>
                                                    
                                                </div>
                                            </div>
                                             <div class="row" style="margin: 7px 0 7px 0">
                                                 <div class="col-md-5">
                                                     <img id="img1" runat="server" />
                                                 </div>
                                                   <div class="col-md-5">
                                                     <img id="img2" runat="server" />
                                                 </div>
                                             </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                 <div class="col-md-5">
                                                     <img id="img3" runat="server" />
                                                 </div>
                                                   <div class="col-md-5">
                                                     <img id="img4" runat="server" />
                                                 </div>
                                             </div>
                                              <div class="row" style="margin: 7px 0 7px 0">
                                                 <div class="col-md-5">
                                                     <img id="img5" runat="server" />
                                                 </div>
                                                   <div class="col-md-5">
                                                     <img id="img6" runat="server" />
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
</asp:Content>

