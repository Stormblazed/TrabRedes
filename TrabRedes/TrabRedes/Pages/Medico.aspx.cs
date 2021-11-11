using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrabRedes.Pages;
using TrabRedes.App_Code;
using System.Data;
using System.Text;

namespace TrabRedes.Pages
{
    public partial class Medico : System.Web.UI.Page
    {

        [System.Web.Services.WebMethod()]
        public static AjaxResponse getBaseData(string sid, string f)
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

                System.Collections.Specialized.NameValueCollection queryS = System.Web.HttpUtility.ParseQueryString(f);
                string TipoFiltro = queryS["ctl00$Pesquisa$OptFiltroPesquisa"].ToString();
                string txtPesquisa = queryS["ctl00$Pesquisa$txtPesquisa"].ToString();

                Adados.MysqlConstruction();
                DataTable DtbReturn = new DataTable();
                System.Text.StringBuilder stringHtml = new StringBuilder();
                string sSql = string.Empty;

                sSql = "SELECT COD_MEDICO,NOM_MEDICO,EMAIL, NOM_USUARIO FROM medico m  inner join usuario u on m.cod_usuario = u.cod_usuario ";
               
                if (txtPesquisa != "")
                {
                    switch (TipoFiltro)
                    {

                        case "0":
                            sSql += " WHERE EMAIL like '%" + txtPesquisa + "%'";
                            break;
                        case "1":
                            sSql += " WHERE NOM_MEDICO like '%" + txtPesquisa + "%'";
                            break;
                    }
                }


                DtbReturn = Adados.MySqlReturnData(sSql);

                foreach (DataRow row in DtbReturn.Rows)
                {

                    stringHtml.Append("<tr id='" + row["COD_MEDICO"].ToString() + "' class='even pointer'>");
                    stringHtml.Append("<td>");
                    stringHtml.Append("<div class='btn btn-sm btn-default' onclick='openEdit(" + row["COD_MEDICO"].ToString() + ");'><i class='fa fa-pencil' title='Alterar este Registro'>&nbsp;</i></div>");
                    stringHtml.Append("</td>");
                    stringHtml.Append("<td>" + row["NOM_MEDICO"] + "</td>");
                    stringHtml.Append("<td>" + row["EMAIL"] + "</td>");
                    stringHtml.Append("<td>" + row["NOM_USUARIO"] + "</td>");
                    stringHtml.Append("<td></td>");
                    stringHtml.Append("</tr>");
                }


                retorno.Message = "Pesquisa realizada com sucesso";
                retorno.Data = stringHtml.ToString();
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

        [System.Web.Services.WebMethod()]
        public static AjaxResponse getDelete(string sid, string f)
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

                System.Collections.Specialized.NameValueCollection queryS = System.Web.HttpUtility.ParseQueryString(f);


                Adados.MysqlConstruction();

                System.Text.StringBuilder stringHtml = new StringBuilder();
                string sSql = string.Empty;
                sSql = "DELETE FROM MEDICO WHERE COD_MEDICO IN (" + sid + ")";
                Adados.MySqlExecutaData(sSql);

                retorno.Message = "Deletado com Sucesso";
                retorno.Data = "Deletado com Sucesso";
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

        [System.Web.Services.WebMethod()]
        public static AjaxResponse getSalvarUsuario(string sid, string f)
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

                
                string txtNome = queryS["ctl00$Edicao$txtnome"];
                string txtemail = queryS["ctl00$Edicao$txtemail"];
                string dllusuario = queryS["ctl00$Edicao$ddlusuario"];
                string hideUsuario = queryS["ctl00$Edicao$hideusuarioeditar"];


                if (txtNome == string.Empty || txtemail == string.Empty || dllusuario == string.Empty )
                {
                    retorno.Message = "Insira todos os dados!";
                    retorno.Data = "Insira todos os dados!";
                    retorno.Sucess = true;
                    return retorno;
                }

                Adados.MysqlConstruction();

                DataTable DtbReturn = new DataTable();
                StringBuilder stringQuery = new StringBuilder();

                if (hideUsuario == "")
                {
                    stringQuery.Append("insert into medico(NOM_MEDICO,EMAIL,COD_USUARIO) VALUES ('"+ txtNome + "','" + txtemail + "', '" + dllusuario + "');");
                }
                else
                {
                    stringQuery.Append("UPDATE medico SET NOM_MEDICO = '" + txtNome + "' , EMAIL = '" + txtemail + "' , COD_USUARIO = '" + dllusuario + "'  WHERE COD_MEDICO = '" + hideUsuario + "'");
                }

                Adados.MySqlExecutaData(stringQuery.ToString());

                retorno.Message = "Alterado com Sucesso";
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
            object _pnlListagem = Master.FindControl("__PnlListagem");
            ((System.Web.UI.Control)_pnlListagem).Visible = true;


            object __LblTitulo = Master.FindControl("__LblTitulo");
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).InnerText = "Medico";
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).HRef = "../Pages/Medico.aspx";

        }

        public void btnEditar_Click(object sender, EventArgs e)
        {
            object _pnlListagem = Master.FindControl("__PnlListagem");
            ((System.Web.UI.Control)_pnlListagem).Visible = false;
            object __PnlEdicao = Master.FindControl("__PnlEdicao");
            ((System.Web.UI.Control)__PnlEdicao).Visible = true;

            AjaxResponse retorno = new AjaxResponse();
            ClsMysql Adados = new ClsMysql();
            DataTable DtbReturn = new DataTable();
            string sSql;
            Adados.MysqlConstruction();

            sSql = "SELECT COD_USUARIO,NOM_USUARIO FROM USUARIO ";
            DtbReturn = Adados.MySqlReturnData(sSql);

            ddlusuario.DataSource = DtbReturn;
            ddlusuario.DataTextField = "NOM_USUARIO";
            ddlusuario.DataValueField = "COD_USUARIO";
            ddlusuario.DataBind();            
            ddlusuario.Items.Insert(0, new ListItem("", "0"));
            ddlusuario.SelectedValue = "0";
            if (hideUsuario.Value != "")
            {             

                sSql = "SELECT COD_MEDICO,NOM_MEDICO,EMAIL,COD_USUARIO FROM MEDICO WHERE COD_MEDICO = " + hideUsuario.Value;
            
                DtbReturn = Adados.MySqlReturnData(sSql);
             
                if (DtbReturn.Rows.Count != 1)
                {
                    ((System.Web.UI.Control)_pnlListagem).Visible = true;
                    ((System.Web.UI.Control)__PnlEdicao).Visible = false;
                    hideUsuario.Value = "";
                    return;

                }
                foreach (DataRow row in DtbReturn.Rows)
                {
                    hideusuarioeditar.Value = hideUsuario.Value;
                    txtnome.Text = row["NOM_MEDICO"].ToString();
                    txtemail.Text = row["EMAIL"].ToString();
                    ddlusuario.SelectedValue = row["COD_USUARIO"].ToString();                    

                }
                return;

            }

        }

    }
}