using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabRedes.Pages;

namespace TrabRedes.Pages
{
    public partial class Usuario : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            object _pnlListagem = Master.FindControl("__PnlListagem");
            ((System.Web.UI.Control)_pnlListagem).Visible = true;
            //object _pnlEditar = Master.FindControl("__PnlEdicao");
            //((System.Web.UI.Control)_pnlEditar).Visible = true;


        }
        private void Setpage()
        {
            


        }

    }
}