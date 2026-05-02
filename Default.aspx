<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestorDiscos.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col"></div>
        <div class="col-8 mt-5"> 
            <asp:GridView ID="dgvDiscos" AutoGenerateColumns="false" CssClass="table" runat="server">
                <Columns>
                    <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaLanzamiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Cantidad de Canciones" DataField="CantidadDeCanciones" />
                    <asp:BoundField HeaderText="Estilo" DataField="EstiloDescripcion" />
                    <asp:BoundField HeaderText="Edicion" DataField="EdicionDescripcion" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col"></div>
    </div>

</asp:Content>
