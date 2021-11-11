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
    public partial class Paciente : System.Web.UI.Page
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

                sSql = "SELECT COD_PACIENTE,NOM_PACIENTE,EMAIL, CASE WHEN IND_TIPO_SANGUE = 1 THEN  concat(dsc_tipo_sangue, '+')  ELSE concat(dsc_tipo_sangue, '-') END  dsc_tipo_sangue , DSC_TIPO_CONVENIO  FROM paciente m  inner join TIPO_SANGUE u on m.COD_TIPO_SANGUE = u.COD_TIPO_SANGUE INNER JOIN TIPO_CONVENIO C on c.COD_TIPO_CONVENIO = m.COD_TIPO_CONVENIO ";

                if (txtPesquisa != "")
                {
                    switch (TipoFiltro)
                    {

                        case "0":
                            sSql += " WHERE EMAIL like '%" + txtPesquisa + "%'";
                            break;
                        case "1":
                            sSql += " WHERE NOM_PACIENTE like '%" + txtPesquisa + "%'";
                            break;
                    }
                }


                DtbReturn = Adados.MySqlReturnData(sSql);

                foreach (DataRow row in DtbReturn.Rows)
                {

                    stringHtml.Append("<tr id='" + row["COD_PACIENTE"].ToString() + "' class='even pointer'>");
                    stringHtml.Append("<td>");
                    stringHtml.Append("<div class='btn btn-sm btn-default' onclick='openEdit(" + row["COD_PACIENTE"].ToString() + ");'><i class='fa fa-pencil' title='Alterar este Registro'>&nbsp;</i></div>");
                    stringHtml.Append("</td>");
                    stringHtml.Append("<td>" + row["NOM_PACIENTE"] + "</td>");
                    stringHtml.Append("<td>" + row["EMAIL"] + "</td>");
                    stringHtml.Append("<td>" + row["dsc_tipo_sangue"] + "</td>");
                    stringHtml.Append("<td>" + row["DSC_TIPO_CONVENIO"] + "</td>");
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
                sSql = "DELETE FROM paciente WHERE cod_paciente IN (" + sid + ")";
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
                string ddlconvenio = queryS["ctl00$Edicao$ddlconvenio"];
                string ddlsangue = queryS["ctl00$Edicao$ddlsangue"];
                string hideUsuario = queryS["ctl00$Edicao$hideusuarioeditar"];


                if (txtNome == string.Empty || txtemail == string.Empty || ddlconvenio == string.Empty || ddlsangue == string.Empty)
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
                    stringQuery.Append("insert into paciente(NOM_PACIENTE,EMAIL,COD_TIPO_CONVENIO,COD_TIPO_SANGUE) VALUES ('"+txtNome+"','"+ txtemail + "', '"+ ddlconvenio + " ','"+ ddlsangue + "');");
                }
                else
                {
                    stringQuery.Append("UPDATE paciente SET NOM_PACIENTE = '" + txtNome + "' , EMAIL = '" + txtemail + "' , COD_TIPO_CONVENIO = '" + ddlconvenio + "' , COD_TIPO_SANGUE = '" + ddlsangue + "' WHERE COD_paciente = '" + hideUsuario + "'");
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
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).InnerText = "Paciente";
            ((System.Web.UI.HtmlControls.HtmlAnchor)__LblTitulo).HRef = "../Pages/Paciente.aspx";

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

            sSql = "SELECT COD_TIPO_CONVENIO,DSC_TIPO_CONVENIO FROM tipo_convenio ";
            DtbReturn = Adados.MySqlReturnData(sSql);

            ddlconvenio.DataSource = DtbReturn;
            ddlconvenio.DataTextField = "DSC_TIPO_CONVENIO";
            ddlconvenio.DataValueField = "COD_TIPO_CONVENIO";
            ddlconvenio.DataBind();
            ddlconvenio.Items.Insert(0, new ListItem("", "0"));
            ddlconvenio.SelectedValue = "0";


            sSql = "SELECT COD_TIPO_SANGUE, CASE WHEN IND_TIPO_SANGUE = 1 THEN  concat(dsc_tipo_sangue, '+')  ELSE concat(dsc_tipo_sangue, '-') END  dsc_tipo_sangue FROM tipo_sangue ";
            DtbReturn = Adados.MySqlReturnData(sSql);

            ddlsangue.DataSource = DtbReturn;
            ddlsangue.DataTextField = "dsc_tipo_sangue";
            ddlsangue.DataValueField = "COD_TIPO_SANGUE";
            ddlsangue.DataBind();
            ddlsangue.Items.Insert(0, new ListItem("", "0"));
            ddlsangue.SelectedValue = "0";



            if (hideUsuario.Value != "")
            {

                sSql = "SELECT COD_PACIENTE,NOM_PACIENTE,EMAIL,COD_TIPO_SANGUE,COD_TIPO_CONVENIO FROM PACIENTE WHERE COD_PACIENTE = " + hideUsuario.Value;

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
                    txtnome.Text = row["NOM_PACIENTE"].ToString();
                    txtemail.Text = row["EMAIL"].ToString();
                    ddlsangue.SelectedValue = row["COD_TIPO_SANGUE"].ToString();
                    ddlconvenio.SelectedValue = row["COD_TIPO_CONVENIO"].ToString();

                }
                return;

            }

        }

    }
}