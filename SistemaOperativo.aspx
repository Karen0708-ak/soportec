<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SistemaOperativo.aspx.cs" Inherits="soportec.SistemaOperativo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <center>
        <div class="container" style="max-width: 500px; text-align: left;">
            <h1 class="text-center">Gestión de Sistemas Operativos</h1>

            <!-- Campo oculto para ID -->
            <asp:HiddenField ID="hf_id_so" runat="server" />

            <!-- Campos del formulario -->
            <asp:Label ID="Label1" runat="server" Text="Nombre del SO" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_nombre_so" CssClass="form-control" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="Label2" runat="server" Text="Versión" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_version_so" CssClass="form-control" runat="server"></asp:TextBox>
            <br />

            <asp:Button ID="btn_guardar" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="btn_guardar_Click" />
            <asp:Button ID="btn_cancelar" CssClass="btn btn-danger" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click" />
            <br />

            <asp:Label ID="lbl_mensaje" runat="server" CssClass="text-info mt-2" />
        </div>
    </center>

    <div class="container mt-4">
        <div class="row">
            <div class="col-md-10 offset-md-1">
                <h3><b>Listado de Sistemas Operativos</b></h3>
                <hr />
                <asp:GridView 
                    ID="gv_so" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="id_so"
                    CssClass="table table-bordered table-striped table-hover text-center"
                    OnRowCommand="gv_so_RowCommand"
                    OnRowDeleting="gv_so_RowDeleting">

                    <Columns>
                        <asp:BoundField DataField="id_so" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="nombre_so" HeaderText="Nombre" />
                        <asp:BoundField DataField="version_so" HeaderText="Versión" />
                        <asp:ButtonField CommandName="EditarSO" Text="Editar" ButtonType="Button" />
                        <asp:ButtonField CommandName="EliminarSO" Text="Eliminar" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
