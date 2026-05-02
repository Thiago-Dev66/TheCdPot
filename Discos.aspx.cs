using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace GestorDiscos
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public List<Disco> ListaDiscos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DiscosServer discosServer = new DiscosServer();
            ListaDiscos = discosServer.listarConSP();

            if (!IsPostBack)
            {
                repDiscos.DataSource = ListaDiscos;
                repDiscos.DataBind();
            }
        }

        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            string val = ((Button)sender).CommandArgument;
        }
    }
}