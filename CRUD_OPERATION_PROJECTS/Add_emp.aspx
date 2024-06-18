<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_emp.aspx.cs" Inherits="CRUD_OPERATION_PROJECTS.Add_emp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script>
        function showAlert(message) {
            var popupMessage = document.getElementById('popupMessageS');
            var modal = new bootstrap.Modal(document.getElementById('exampleModalCenterR'));

            popupMessage.innerHTML = message;
            modal.show();
        }
    </script>

    <div class="container">
        <h2>ADD EMPLOYEE</h2>

        <div class="row">
            <div class="col-2"></div>
            <div class="col-8">

                <div class="row border p-3 rounded shadow">
                    <div class="col-md-12">
                        <div class="row">
                            <label for="lbl_emp_name" class="form-label">Employee Name <span style='color:red'> * </span> </label>
                            <asp:TextBox ID="txt_emp_name" runat="server" MaxLength="99" TextMode="SingleLine" CssClass="form-control textboxes"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="row">
                            <label for="lbl_emp_desc" class="form-label">Employee Description <span style='color:red'> * </span> </label>
                            <asp:TextBox ID="txt_emp_desc" runat="server" TextMode="MultiLine" MaxLength="250" CssClass="form-control textboxes"></asp:TextBox>
                        </div>
                    </div>

                    <div class="text-end mt-2 p-2">
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btn_save_Click" OnClientClick="return ValidateForm();"  />
                        <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-secondary" Text="Clear" OnClick="btn_clear_Click" />
                    </div>

                </div>
            </div>
            <div class="col-2"></div>
        </div>



    </div>

    <div class="modal fade" id="exampleModalCenterR" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">Alert</h1>
            </div>
            <div class="modal-body">
                <p id="popupMessageS">Placeholder for message</p>
            </div>
            <div class="modal-footer" style="place-content: center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" autofocus>close</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>
