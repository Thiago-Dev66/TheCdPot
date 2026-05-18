<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Discos.aspx.cs" Inherits="GestorDiscos.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row row-cols-1 row-cols-md-3 d-4">
        <asp:Repeater runat="server" ID="repDiscos">
            <ItemTemplate>
                <div class="col">
                    <div class="card" style="width: 18rem;">
                        <img src="<%#Eval("UrlImagenCover") %>" class="card-img-top" alt="<%#Eval("Titulo") %>">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Titulo") %></h5>
                            <p class="card-text"><%#Eval("FechaLanzamiento") %> </p>
                            <asp:Button Text="Ver Detalles" CssClass="btn btn-primary" ID="btnDetalles" 
                                CommandArgument='<%#Eval("Id") %>' CommandName="Detalles" OnClick="btnDetalles_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
