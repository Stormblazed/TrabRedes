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
    public partial class Usuario : System.Web.UI.Page
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

                sSql = "SELECT COD_USUARIO,NICK_USUARIO,NOM_USUARIO FROM USUARIO ";
                if (txtPesquisa != "")
                {
                    switch (TipoFiltro)
                    {

                        case "0":
                            sSql += " WHERE NOM_USUARIO like '%" + txtPesquisa + "%'";
                            break;
                        case "1":
                            sSql += " WHERE NICK_USUARIO like '%" + txtPesquisa + "%'";
                            break;
                    }
                }


                DtbReturn = Adados.MySqlReturnData(sSql);

                foreach (DataRow row in DtbReturn.Rows)
                {

                    stringHtml.Append("<tr id='" + row["COD_USUARIO"].ToString() + "' class='even pointer'>");
                    stringHtml.Append("<td>");
                    stringHtml.Append("<div class='btn btn-sm btn-default' onclick='openEdit(" + row["COD_USUARIO"].ToString() + ");'><i class='fa fa-pencil' title='Alterar este Registro'>&nbsp;</i></div>");
                    stringHtml.Append("</td>");
                    stringHtml.Append("<td>" + row["NICK_USUARIO"] + "</td>");
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
                sSql = "DELETE FROM USUARIO WHERE COD_USUARIO IN (" + sid + ")";
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

                string txtNick = queryS["ctl00$Edicao$txtNick"];
                string txtNome = queryS["ctl00$Edicao$txtNome"];
                string txtSenha = queryS["ctl00$Edicao$txtSenha"];
                string txtConfimSenha = queryS["ctl00$Edicao$txtConfirmarSenha"];
                string hideUsuario = queryS["ctl00$Edicao$hideusuarioeditar"];


                if (txtSenha != txtConfimSenha || txtNick == string.Empty || txtNome == string.Empty || txtSenha == string.Empty || txtConfimSenha == string.Empty)
                {
                    retorno.Message = "Insira todos os dados!";
                    retorno.Data = "Insira todos os dados!";
                    retorno.Sucess = true;
                    return retorno;
                }

                Adados.MysqlConstruction();

                DataTable DtbReturn = new DataTable();
                StringBuilder stringQuery = new StringBuilder();

                if (hideUsuario == "") {
                    stringQuery.Append("INSERT INTO usuario(NICK_USUARIO,NOM_USUARIO,DSC_SENHA) VALUES ('"+ txtNick + "','"+ txtNome + "','" + txtSenha + "');");
                }
                else{
                    stringQuery.Append("UPDATE usuario SET NICK_USUARIO = '" + txtNick + "' , NOM_USUARIO = '" + txtNome + "' , DSC_SENHA = '" + txtSenha + "'  WHERE COD_USUARIO = '" + hideUsuario + "'");
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
           ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).InnerText = "Usuário";
           ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).HRef = "../Pages/Usuario.aspx";

        }
        
        public void btnEditar_Click(object sender, EventArgs e)  
        {
            object _pnlListagem = Master.FindControl("__PnlListagem");
            ((System.Web.UI.Control)_pnlListagem).Visible = false;
            object __PnlEdicao = Master.FindControl("__PnlEdicao");
            ((System.Web.UI.Control)__PnlEdicao).Visible = true;

           if (hideUsuario.Value != "")
            {

                AjaxResponse retorno = new AjaxResponse();
                ClsMysql Adados = new ClsMysql();
                DataTable DtbReturn = new DataTable();
                string sSql;

                sSql = "SELECT COD_USUARIO,NICK_USUARIO,NOM_USUARIO,DSC_SENHA FROM USUARIO WHERE COD_USUARIO = " + hideUsuario.Value;
                Adados.MysqlConstruction();

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
                    txtNick.Text = row["NICK_USUARIO"].ToString();
                    txtNome.Text = row["NOM_USUARIO"].ToString();
                    txtSenha.Text = row["DSC_SENHA"].ToString();
                    txtConfirmarSenha.Text = row["DSC_SENHA"].ToString();

                }
                return;

            }

        }



    }
}