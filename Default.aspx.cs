using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace GestorDiscos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
                 
        }
        protected void CargarGrid()
        {
            var negocio = new DiscosServer();
            dgvDiscos.DataSource = negocio.Listar();
            dgvDiscos.DataBind();
        }

        protected void dgvDiscos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDiscos.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void dgvDiscos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvDiscos.SelectedDataKey["Id"]);
            Response.Redirect($"AltaDisco.aspx?id={id}");
        }
    }
}