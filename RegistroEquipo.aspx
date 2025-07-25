﻿<%@ Page Title="Registrar Equipo" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="RegistroEquipo.aspx.cs" Inherits="soportec.RegistroEquipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registrar Equipo
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Registrar Nuevo Equipo</h2>
    <table class="table">
        <tr><td>Nombre:</td><td><asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" /></td></tr>
        <tr><td>Marca:</td><td><asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" /></td></tr>
        <tr><td>Modelo:</td><td><asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" /></td></tr>
        <tr><td>Procesador:</td><td><asp:TextBox ID="txtProcesador" runat="server" CssClass="form-control" /></td></tr>
        <tr><td>Foto (URL):</td><td><asp:TextBox ID="txtFoto" runat="server" CssClass="form-control" /></td></tr>
        <tr><td>Ficha Técnica (URL):</td><td><asp:TextBox ID="txtFicha" runat="server" CssClass="form-control" /></td></tr>
        <tr>
            <td>Sistema Operativo:</td>
            <td>
                <asp:DropDownList ID="ddlSO" runat="server" CssClass="form-control"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
</asp:Content>
