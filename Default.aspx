<%@ Page Title="Listado de Equipos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="soportec._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Listado de Equipos
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 style="font-family;color:white">LISTADO DE EQUIPOS</h1>
        <br /><br />
        <asp:GridView ID="gridEquipos" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
            CssClass="table table-bordered table-striped" OnRowCommand="gridEquipos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="marca" HeaderText="Marca" />
                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                <asp:BoundField DataField="procesador" HeaderText="Procesador" />
                <asp:TemplateField HeaderText="Foto">
                    <ItemTemplate>
                        <img src='<%# Eval("fotoe") %>' alt="Foto" width="80" height="60" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ficha Técnica">
                    <ItemTemplate>
                        <a href='<%# Eval("fichatec") %>' target="_blank">Ver ficha</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sistema_operativo_nombre" HeaderText="Sistema Operativo" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="EditarRegistro" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-primary btn-sm" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="EliminarRegistro" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de eliminar este equipo?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar nuevo equipo" OnClick="btnAgregar_Click" />
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red"></asp:Label>
    </main>
</asp:Content>