<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SistemaOperativo.aspx.cs" Inherits="soportec.SistemaOperativo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <center>
        <div class="container" style="max-width: 500px; text-align: left;">
            <h1 class="text-center">Gestión de Sistemas Operativos</h1>

            <!-- Campo oculto para ID -->
            <asp:HiddenField ID="hf_id_sistema_operativo" runat="server" />

            <!-- Campos del formulario -->
            <asp:Label ID="Label1" runat="server" Text="Nombre" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_nombre" CssClass="form-control" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="Label2" runat="server" Text="Versión" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_version" CssClass="form-control" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="Label3" runat="server" Text="Documentación (Ruta del archivo)" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_documentacion" CssClass="form-control" runat="server"></asp:TextBox>
            <br />

            <asp:Button ID="btn_guardar" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="btn_guardar_Click" />
            <asp:Button ID="btn_cancelar" CssClass="btn btn-danger" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click" />
            <br />

            <asp:Label ID="labl_mensajes" runat="server" Text="" CssClass="text-info mt-2"></asp:Label>
        </div>
    </center>

    <div class="container mt-4">
        <div class="row">
            <div class="col-md-10 offset-md-1">
                <h3><b>Listado de Sistemas Operativos Registrados</b></h3>
                <hr />
                <asp:GridView 
                    ID="gv_sistemas_operativos" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="id"
                    CssClass="table table-bordered table-striped table-hover text-center"
                    OnRowCommand="gv_sistemas_operativos_RowCommand"
                    OnRowDeleting="gv_sistemas_operativos_RowDeleting">

                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="version" HeaderText="Versión" />
                        <asp:BoundField DataField="documentacion" HeaderText="Documentación" />
                        <asp:ButtonField CommandName="EditarSO" Text="Editar" ButtonType="Button" />
                        <asp:ButtonField CommandName="EliminarSO" Text="Eliminar" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>