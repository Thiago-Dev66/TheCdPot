using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace GestorDiscos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var negocio = new DiscosServer();
            dgvDiscos.DataSource = negocio.listarConSP();
            dgvDiscos.DataBind();
        }
    }
}