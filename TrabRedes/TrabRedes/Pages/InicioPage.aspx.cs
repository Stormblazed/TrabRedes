using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrabRedes.Pages
{
    public partial class InicioPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object _pnlListagem = Master.FindControl("__PnlListagem");
            ((System.Web.UI.Control)_pnlListagem).Visible = true;

        }
    }
}