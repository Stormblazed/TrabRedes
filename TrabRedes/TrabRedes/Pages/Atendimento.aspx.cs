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
    public partial class Atendimento : System.Web.UI.Page
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

                sSql = "select cod_atendimento, nom_paciente, nom_medico from atendimento a inner join paciente p on p.COD_PACIENTE = a.COD_PACIENTE inner join medico m on m.COD_MEDICO = a.COD_MEDICO";

                if (txtPesquisa != "")
                {
                    switch (TipoFiltro)
                    {

                        case "0":
                            sSql += " WHERE nom_paciente like '%" + txtPesquisa + "%'";
                            break;
                        case "1":
                            sSql += " WHERE nom_medico like '%" + txtPesquisa + "%'";
                            break;
                    }
                }


                DtbReturn = Adados.MySqlReturnData(sSql);

                foreach (DataRow row in DtbReturn.Rows)
                {

                    stringHtml.Append("<tr id='" + row["cod_atendimento"].ToString() + "' class='even pointer'>");
                    stringHtml.Append("<td>");
                    stringHtml.Append("<div class='btn btn-sm btn-default' onclick='openEdit(" + row["cod_atendimento"].ToString() + ");'><i class='fa fa-pencil' title='Alterar este Registro'>&nbsp;</i></div>");
                    stringHtml.Append("</td>");
                    stringHtml.Append("<td>" + row["nom_paciente"] + "</td>");
                    stringHtml.Append("<td>" + row["nom_medico"] + "</td>");                    
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
                sSql = "DELETE FROM atendimento WHERE cod_atendimento IN (" + sid + ")";
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


                string hidemensagem = queryS["ctl00$Edicao$hideMensagem"];
                string ddlpaciente = queryS["ctl00$Edicao$ddlPaciente"];
                string ddlmedico = queryS["ctl00$Edicao$ddlmedico"];
                string hideUsuario = queryS["ctl00$Edicao$hideusuarioeditar"];


                if (hidemensagem == string.Empty || ddlpaciente == string.Empty || ddlmedico == string.Empty)
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
                    stringQuery.Append("insert into atendimento(COD_MEDICO,COD_PACIENTE, OBS) VALUES ('" + ddlmedico + "','" + ddlpaciente  + "','" + hidemensagem +"');");
                }
                else
                {
                    stringQuery.Append("UPDATE atendimento SET COD_MEDICO = '" + ddlmedico + "' , COD_PACIENTE = '" + ddlpaciente + "' , OBS = '" + hidemensagem + "'  WHERE COD_ATENDIMENTO = '" + hideUsuario + "'");
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
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).InnerText = "Atendimento";
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).HRef = "../Pages/Atendimento.aspx";

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

            sSql = "SELECT COD_paciente,NOM_PACIENTE FROM paciente ";
            DtbReturn = Adados.MySqlReturnData(sSql);

            ddlPaciente.DataSource = DtbReturn;
            ddlPaciente.DataTextField = "NOM_PACIENTE";
            ddlPaciente.DataValueField = "COD_paciente";
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("", "0"));
            ddlPaciente.SelectedValue = "0";


            sSql = "SELECT cod_medico,nom_medico FROM medico ";
            DtbReturn = Adados.MySqlReturnData(sSql);

            ddlmedico.DataSource = DtbReturn;
            ddlmedico.DataTextField = "nom_medico";
            ddlmedico.DataValueField = "cod_medico";
            ddlmedico.DataBind();
            ddlmedico.Items.Insert(0, new ListItem("", "0"));
            ddlmedico.SelectedValue = "0";

            if (hideUsuario.Value != "")
            {

                sSql = "SELECT COD_MEDICO,COD_PACIENTE,obs FROM atendimento WHERE COD_ATENDIMENTO = " + hideUsuario.Value;

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
                    ddlmedico.SelectedValue = row["COD_MEDICO"].ToString();
                    ddlPaciente.SelectedValue = row["COD_PACIENTE"].ToString();
                    hideMensagem.Value = row["obs"].ToString();

                }
                return;

            }

        }
    }
}