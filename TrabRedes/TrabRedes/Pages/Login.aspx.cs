using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabRedes.App_Code;


namespace TrabRedes.Pages
{
    public partial class Login : System.Web.UI.Page
    {

   
        [System.Web.Services.WebMethod()]
        public static AjaxResponse getLogin(string sid, string f)
        {
            AjaxResponse retorno = new AjaxResponse();
            ClsMysql Adados = new ClsMysql();

            try
            {
                ClsDefautSib valid = new ClsDefautSib();
                if (valid.validateAjaxRequest(f) == false)
                {
                    retorno.Message = "Ajax request validation failed";
                    retorno.Data = String.Empty;
                    retorno.Sucess = false;
                    return retorno;
                }
                //Dim queryS As System.Collections.Specialized.NameValueCollection = System.Web.HttpUtility.ParseQueryString(f)
                System.Collections.Specialized.NameValueCollection queryS = System.Web.HttpUtility.ParseQueryString(f);

                string txtNick = queryS["txtNick"];
                string txtSenha = queryS["txtNick"];


                if(txtNick == string.Empty && txtSenha == string.Empty)
                {
                    retorno.Message = "Insira todos os dados!";
                    retorno.Data = "Insira todos os dados!";
                    retorno.Sucess = true;
                    return retorno;
                }

                Adados.MysqlConstruction();
               
                DataTable DtbReturn = new DataTable();
                StringBuilder stringQuery = new StringBuilder();

                stringQuery.Append("SELECT 1 FROM usuario where NICK_USUARIO = '" + txtNick + "' AND DSC_SENHA = '" + txtSenha + "';");

                DtbReturn = Adados.MySqlReturnData(stringQuery.ToString());

                if (DtbReturn.Rows.Count == 0)
                {
                    retorno.Message = "Dados Incorretos.";
                    retorno.Data = "Dados Incorretos.";
                    retorno.Sucess = true;
                    return retorno;
                }                    


                retorno.Message = "Logado";
                retorno.Data = "Logado";
                retorno.Sucess = true;

            }
            catch (Exception ex)
            {

                retorno.Message = "Não foi possível realizar a ação solicitada";
                retorno.Data = ex.Message;
                retorno.Sucess = false;
            }
            return retorno;
        }
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        
    }
}