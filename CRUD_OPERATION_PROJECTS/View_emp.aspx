<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View_emp.aspx.cs" Inherits="CRUD_OPERATION_PROJECTS.View_emp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .gvwidth {
            max-width: 250px;
            word-wrap: break-word;
            white-space: normal;
        }

        .GV_Width_NO {
            max-width: 100px;
            word-wrap: break-word;
            white-space: normal;
        }

        .GV_Width_action {
            min-width: 100px;
            max-width: 100px;
        }

        
        .gridview th, .gridview td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
        }

        .gridview th {
            color: white;
            background-color: #006dc3;
        }

        .gridview tr {
            background-color: #dddddd;
        }

            .gridview tr:hover {
                background-color: #e9ecef;
            }
    </style>

    <div class="container">

        <h2>EMPLOYEE LIST</h2>

        <div class="row">
            <div class="col-2"></div>
            <div class="col-8">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="gv_employees" CssClass="table gridview w-100" DataKeyNames="PKN_EMP_ID" AutoGenerateColumns="false" OnRowEditing="gv_employees_RowEditing" OnRowCancelingEdit="gv_employees_RowCancelingEdit" OnRowUpdating="gv_employees_RowUpdating" OnRowDeleting="gv_employees_RowDeleting">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="GV_Width_NO" HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVslno" runat="server" Text='<%# Bind("PKN_EMP_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGVslno" runat="server" Text='<%# Bind("PKN_EMP_ID") %>' Enabled="false" CssClass="form-control textboxes " onkeydown="return (event.keyCode!=13);" Visible="false"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="gvwidth " ControlStyle-CssClass="gvwidth " ItemStyle-Wrap="true" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVname" runat="server" Text='<%# Bind("C_EMP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGVname" runat="server" Text='<%# Bind("C_EMP_NAME") %>' MaxLength="50" CssClass="form-control textboxes" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="gvwidth " ControlStyle-CssClass="gvwidth" ItemStyle-Wrap="true" HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVdesignation" runat="server" Text='<%# Bind("C_EMP_DESCRIPTION") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGVdesignation" runat="server" Text='<%# Bind("C_EMP_DESCRIPTION") %>' MaxLength="50" CssClass="form-control textboxes" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="GV_Width_action" HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnedit" runat="server" ToolTip="edit" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary"><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtndlt" runat="server" ToolTip="delete" CommandName="delete" CssClass="btn btn-sm btn-outline-danger"><i class="bi bi-trash-fill"></i></asp:LinkButton>

                                        </ItemTemplate>
                                        <EditItemTemplate>

                                            <asp:LinkButton ID="lbtnupdate" runat="server" ToolTip="update" CommandName="Update" CssClass="btn btn-sm btn-outline-success" OnClientClick="return ValidateForm();" ><i class="bi bi-upload"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtncancel" runat="server" ToolTip="cancel" CommandName="Cancel" CssClass="btn btn-sm btn-outline-secondary"><i class="bi bi-x-circle"></i></asp:LinkButton>

                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-2"></div>
        </div>

    </div>

       <%-- popup messages close and ok buttons --%>
   <div class="modal fade" id="exampleModalCenteR" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
       <div class="modal-dialog modal-dialog-centered">
           <div class="modal-content">
               <div class="modal-header">
                   <h1 class="modal-title fs-5" id="exampleModalLabeLs">Alert</h1>
               </div>
               <div class="modal-body">
                   <p id="popupMessagesS">Placeholder for message</p>
               </div>
               <div class="modal-footer" style="place-content: center">
                   <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">close</button>
                   <asp:Button ID="btn_Popup_Msg" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btn_Popup_Msg_Click" />
               </div>
           </div>
       </div>
   </div>

</asp:Content>
