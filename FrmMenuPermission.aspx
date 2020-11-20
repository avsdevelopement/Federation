<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMenuPermission.aspx.cs" Inherits="FrmMenuPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="global/Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="global/Scripts/jquery.contextMenu.js" type="text/javascript"></script>
    <link href="global/vendor/jquery/jquery.contextMenu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            $("#MyTreeDiv A").contextMenu({
                menu: 'myMenu'
            });
        });

        function getGUID(mystr) {
            var reGUID = /\w{8}[-]\w{4}[-]\w{4}[-]\w{4}[-]\w{12}/g
            var retArr = [];
            var retval = '';
            retArr = mystr.match(reGUID);
            if (retArr != null) {
                retval = retArr[retArr.length - 1];
            }
            return retval;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Menu Permission 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">
                                        <div id="MyTreeDiv">
                                            <asp:TreeView ID="TreeView1" ShowCheckBoxes="All" ExpandDepth="0" runat="server">
                                            </asp:TreeView>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate()" TabIndex="40" />
                                        <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="42" />
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
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

