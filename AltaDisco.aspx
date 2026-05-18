<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaDisco.aspx.cs" Inherits="GestorDiscos.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row mt-5">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtTitulo" class="form-label">Título</label>
                <asp:TextBox type="text" ID="txtTitulo" class="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtFechaLanzamiento" class="form-label">Fecha de Lanzamiento</label>
                <asp:TextBox type="date" ID="txtFechaLanzamiento" class="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtCantidadCanciones" class="form-label">Cantidad de Canciones</label>
                <asp:TextBox type="number" ID="txtCantidadCanciones" class="form-control" runat="server" />
            </div>
            <asp:Button Text="Agregar" id="btnAgregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" runat="server" />
            <asp:Button Text="Cancelar" id="btnCancelar" PostBackUrl="~/Default.aspx" CssClass="btn btn-primary" runat="server" />
        </div>
        
        <div class="col-6">
            <div class="mb-3">
                <label for="ddlEstilo" class="form-label">Estilo</label>
                <asp:DropDownList ID="ddlEstilo" class="form-select" runat="server">
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlEdicion" class="form-label">Edición</label>
                <asp:DropDownList ID="ddlEdicion" class="form-select" runat="server">
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <!--El Update Panel evita la recarga de toda la pantalla. Los elementos que esten dentro
                    de dicha etiqueta se volveran a cargar si uno de ellos tiene un autopostback=true -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <label for="txtImagen" class="form-label">Cover</label>
                        <asp:TextBox type="text" ID="txtImagen" class="form-control" OnTextChanged="txtImagen_TextChanged" AutoPostBack="true" runat="server" />
                        <asp:Image ID="imgCover"
                            CssClass="rounded float-start img-fluid" alt="" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
