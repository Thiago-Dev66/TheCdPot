using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestorDiscos
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EstilosServer estilosServer = new EstilosServer();
                TiposEdicionServer tiposEdicionServer = new TiposEdicionServer();

                //Cargar desplegables desde db:
                ddlEstilo.DataSource = estilosServer.ListarEstilos();

                //Que quiero que muestre:
                ddlEstilo.DataTextField = "Descripcion";
                //Que valor tiene por detras:
                ddlEstilo.DataValueField = "Id";
                ddlEstilo.DataBind();

                ddlEdicion.DataSource = tiposEdicionServer.ListEdicion();
                ddlEdicion.DataTextField = "Description";
                ddlEdicion.DataValueField = "Id";
                ddlEdicion.DataBind();

                int id = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                if (id != 0)
                {
                    var negocio = new DiscosServer();
                    Disco disco = negocio.GetDiscoById(id);

                    txtTitulo.Text = disco.Titulo;
                    txtFechaLanzamiento.Text = disco.FechaLanzamiento.ToString("yyyy-MM-dd");
                    txtCantidadCanciones.Text = disco.CantidadDeCanciones.ToString();
                    txtImagen.Text = disco.UrlImagenCover;
                    ddlEstilo.SelectedValue = disco.Estilo.Id.ToString();
                    ddlEdicion.SelectedValue = disco.Edicion.Id.ToString();

                    btnAgregar.Text = "Modificar";
                }
            }
        }

        protected void txtImagen_TextChanged(object sender, EventArgs e)
        {
            string placeholder = "https://community.softr.io/uploads/db9110/original/2X/7/74e6e7e382d0ff5d7773ca9a87e6f6f8817a68a6.jpeg";

            try
            {
                imgCover.ImageUrl = txtImagen.Text;
                imgCover.AlternateText = "Your image here";

                if (imgCover.ImageUrl == "")
                {
                    imgCover.ImageUrl = placeholder;
                    imgCover.AlternateText = "placeholder";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var negocio = new DiscosServer();
                var disco = new Disco()
                {
                    Estilo = new Estilos(),
                    Edicion = new TipoEdicion()
                };

                disco.Titulo = txtTitulo.Text;
                disco.FechaLanzamiento = DateTime.Parse(txtFechaLanzamiento.Text);
                disco.CantidadDeCanciones = int.Parse(txtCantidadCanciones.Text);
                disco.Estilo.Id = int.Parse(ddlEstilo.SelectedValue);
                disco.Edicion.Id = int.Parse(ddlEdicion.SelectedValue);
                disco.UrlImagenCover = txtImagen.Text;

                if (Request.QueryString["id"] != null) 
                {
                    disco.Id = Convert.ToInt32(Request.QueryString["id"]);
                    negocio.Modificar(disco);
                }
                else
                    negocio.Agregar(disco);

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}