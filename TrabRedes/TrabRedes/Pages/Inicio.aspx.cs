using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Sib.Bessatec.Pages;
using Sib.Bessatec.Classe;
using System.Text;



namespace Sib.Bessatec.Pages
{

    public class Inicio : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod()]
        public static AjaxResponse getDados(string sid, string f)
        {
            AjaxResponse retorno = new AjaxResponse();
            ClsSqlConnection Adados = new ClsSqlConnection();

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

                string id = sid.Split('|')[0];
                string controle = sid.Split('|')[1];
                Adados.SqlConstruction();
                ClsQuerry Querry = new ClsQuerry();
                DataTable DtbReturn = new DataTable();
                System.Text.StringBuilder stringHtml = new StringBuilder();
                string sSql;


                if (id == "null")
                {
                    stringHtml.Append("<div class='card card-info card-outline'>");
                    stringHtml.Append("<div class='card-body'>");
                    stringHtml.Append("<h5>Dados do Cliente</h5>");
                    stringHtml.Append("<div class='row pt-3 pb-3'>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Sigla</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$txtSiglaModal' type='text' id='Pesquisa_txtSiglaModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Nome</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtNomeModal' type='text' id='Pesquisa_TxtNomeModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Cidade</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtCidadeModal' type='text' id='Pesquisa_TxtCidadeModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<h5>Dados de Conexão</h5>");
                    stringHtml.Append("<div class='row pt-3 pb-3'>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Servidor</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtServidorModal' type='text' id='Pesquisa_TxtServidorModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Porta(DB)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtPortaModal' type='text' id='Pesquisa_TxtPortaModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span class='text-danger'>Servidor(Interno)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtServidorInternoModal' type='text' id='Pesquisa_TxtServidorInternoModal' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span class='text-danger'>Porta(Interno)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtPortaInternoModal' type='text' id='Pesquisa_TxtPortaInternoModal' class='form-control-sm form-control mt-1' >");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Usuário(DB)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtUsuarioModal' type='text' id='Pesquisa_TxtUsuarioModal' class='form-control-sm form-control mt-1' >");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Senha(DB)</span>");
                    stringHtml.Append("<div class='input-group mb-3'>");
                    stringHtml.Append("<div class='input-group-prepend c-pointer'  OnClick='MostPassword()' >");
                    stringHtml.Append("<span class='input-group-text form-control-sm form-control mt-1' id='basic-addon1' ><i class='fas fa-eye' id='ShowPassword' ></i></span>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtSenhaModal' type='password' id='Pesquisa_TxtSenhaModal' class='form-control-sm form-control mt-1' aria-describedby='basic-addon1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Banco(DB)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtBancoModal' type='text' id='Pesquisa_TxtBancoModal' class='form-control-sm form-control mt-1' >");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>Mstsc (ip)</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$Txtip' type='text' id='Pesquisa_Txtip' class='form-control-sm form-control mt-1'>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span>WsIntregração</span>");
                    stringHtml.Append("<input name='ctl00$Pesquisa$TxtWsInteModal' type='text' id='Pesquisa_TxtWsInteModal' class='form-control-sm form-control mt-1' >");
                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<h5>Outros</h5>");

                    stringHtml.Append("<div class='row pt-3 pb-3'>");

                    stringHtml.Append("<div class='col-md-5'>");
                    stringHtml.Append("<span>Complemento</span>");
                    stringHtml.Append("<textarea class='form-control' id='TxtAreaCompleModal' name='TxtAreaCompleModal' rows='4'></textarea>");
                    stringHtml.Append("</div>");

                    stringHtml.Append("<div class='col-md-3'>");
                    stringHtml.Append("<span class='checkbox mr-3'>");
                    stringHtml.Append("<input id='chkBloq' type='checkbox' name='chkBloq' value='1' title='Empresa Bloqueada'>");
                    stringHtml.Append("<label for='chkBloq' title='Bloqueado'>&nbsp;Bloqueado</label></span>");
                    stringHtml.Append("<span class='checkbox'>");
                    stringHtml.Append("<input id='chkAtiv' type='checkbox' name='chkAtiv' value='1' title='Suporte Ativo'>");
                    stringHtml.Append("<label for='chkAtiv' title='Suporte Ativo'>&nbsp;Suporte Ativo</label></span>");
                    stringHtml.Append("</div>");

                    stringHtml.Append("<div class='col-md-4'>");
                    stringHtml.Append("<div class='row'>");
                    stringHtml.Append("<span class='col-md-12 mb-2'>Status</span>");
                    stringHtml.Append("<div class='col-2  ml-4 mr-4'>");
                    stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdAtivo' value='1' checked=''>");
                    stringHtml.Append("<label class='form-check-label' for='rdAtivo'>Ativo</label>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("<div class='col-2 mr-4'>");
                    stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdInativo' value='0'>");
                    stringHtml.Append("<label class='form-check-label' for='rdInativo'>Inativo</label>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");

                    stringHtml.Append("</div>");

                    stringHtml.Append("</div>");
                    stringHtml.Append("</div>");
                }
                else
                {


                    sSql = Querry.SqlPesquisaId(id);

                    DtbReturn = Adados.SqlReturnData(sSql);

                    foreach (DataRow row in DtbReturn.Rows)
                    {

                        Boolean isSuporte = Convert.ToBoolean(row["IND_SUPORTE_ATIVO"]);
                        Boolean isSituacao = Convert.ToBoolean(row["IND_SITUACAO"]);
                        Boolean isBloqueio = Convert.ToBoolean(row["IND_BLOQUEIO"]);

                        stringHtml.Append("<div class='card card-info card-outline'>");
                        stringHtml.Append("<div class='card-body'>");
                        stringHtml.Append("<h5>Dados do Cliente</h5>");
                        stringHtml.Append("<div class='row pt-3 pb-3'>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Sigla</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$txtSiglaModal' type='text' id='Pesquisa_txtSiglaModal' class='form-control-sm form-control mt-1' value='" + row["SGL_CLIENTE"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Nome</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtNomeModal' type='text' id='Pesquisa_TxtNomeModal' class='form-control-sm form-control mt-1' value='" + row["NOM_CLIENTE"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Cidade</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtCidadeModal' type='text' id='Pesquisa_TxtCidadeModal' class='form-control-sm form-control mt-1' value='" + row["NOM_LOCALIDADE"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<h5>Dados de Conexão</h5>");
                        stringHtml.Append("<div class='row pt-3 pb-3'>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Servidor</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtServidorModal' type='text' id='Pesquisa_TxtServidorModal' class='form-control-sm form-control mt-1' value='" + Querry.DecryptString(row["NOM_SERVER_SGDB"].ToString().Trim()) + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Porta(DB)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtPortaModal' type='text' id='Pesquisa_TxtPortaModal' class='form-control-sm form-control mt-1' value='" + row["NUM_PORT_SGDB"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span class='text-danger'>Servidor(Interno)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtServidorInternoModal' type='text' id='Pesquisa_TxtServidorInternoModal' class='form-control-sm form-control mt-1' value='" + row["NUM_IP_INTERNO"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span class='text-danger'>Porta(Interno)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtPortaInternoModal' type='text' id='Pesquisa_TxtPortaInternoModal' class='form-control-sm form-control mt-1' value='" + row["NUM_PORT_SGDB_INTERNA"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Usuário(DB)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtUsuarioModal' type='text' id='Pesquisa_TxtUsuarioModal' class='form-control-sm form-control mt-1' value='" + Querry.DecryptString(row["NOM_USER_SGDB"].ToString()) + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Senha(DB)</span>");
                        stringHtml.Append("<div class='input-group mb-3'>");
                        stringHtml.Append("<div class='input-group-prepend c-pointer' OnClick= 'MostPassword()'>");
                        stringHtml.Append("<span class='input-group-text form-control-sm form-control mt-1' id='basic-addon1' ><i class='fas fa-eye' id='ShowPassword' ' ></i></span>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtSenhaModal' type='password' id='Pesquisa_TxtSenhaModal' class='form-control-sm form-control mt-1' value='" + Querry.DecryptString(row["DSC_PWD_SGDB"].ToString()) + "'aria-describedby='basic-addon1'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Banco(DB)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtBancoModal' type='text' id='Pesquisa_TxtBancoModal' class='form-control-sm form-control mt-1' value='" + row["NOM_SGDB"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>Mstsc (ip)</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$Txtip' type='text' id='Pesquisa_Txtip' class='form-control-sm form-control mt-1' value='" + row["NUM_IP_EXTERNO"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span>WsIntregração</span>");
                        stringHtml.Append("<input name='ctl00$Pesquisa$TxtWsInteModal' type='text' id='Pesquisa_TxtWsInteModal' class='form-control-sm form-control mt-1' value='" + row["URL_INTEGRACAO"].ToString() + "'>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<h5>Outros</h5>");
                        stringHtml.Append("<div class='row pt-3 pb-3'>");
                        stringHtml.Append("<div class='col-md-5'>");
                        stringHtml.Append("<span>Complemento</span>");
                        stringHtml.Append("<textarea class='form-control' id='TxtAreaCompleModal' name='TxtAreaCompleModal' rows='4'>" + Querry.DecryptString(row["DSC_COMPLEMENTO"].ToString()) + "</textarea>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("<div class='col-md-3'>");
                        stringHtml.Append("<span class='checkbox mr-3'>");
                        if (isBloqueio)
                        {
                            stringHtml.Append("<input id='chkBloq' type='checkbox' name='chkBloq' value='1' title='Empresa Bloqueada' checked>");
                        }
                        else
                        {
                            stringHtml.Append("<input id='chkBloq' type='checkbox' name='chkBloq' value='1' title='Empresa Bloqueada' >");
                        }
                        stringHtml.Append("<label for='chkBloq' title='Bloqueado'>&nbsp;Bloqueado</label></span>");
                        stringHtml.Append("<span class='checkbox'>");
                        if (isSuporte)
                        {
                            stringHtml.Append("<input id='chkAtiv' type='checkbox' name='chkAtiv' value='1' title='Suporte Ativo' checked>");
                        }
                        else
                        {
                            stringHtml.Append("<input id='chkAtiv' type='checkbox' name='chkAtiv'  value='1' title='Suporte Ativo'>");
                        }
                        stringHtml.Append("<label for='chkAtiv' title='Suporte Ativo'>&nbsp;Suporte Ativo</label></span>");
                        stringHtml.Append("</div>");
                        if (isSituacao)
                        {
                            stringHtml.Append("<div class='col-md-4'>");
                            stringHtml.Append("<div class='row'>");
                            stringHtml.Append("<span class='col-md-12 mb-2'>Status</span>");
                            stringHtml.Append("<div class='col-2  ml-4 mr-4'>");
                            stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdAtivo' value='1' checked=''>");
                            stringHtml.Append("<label class='form-check-label' for='rdAtivo'>Ativo</label>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("<div class='col-2 mr-4'>");
                            stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdInativo' value='0'>");
                            stringHtml.Append("<label class='form-check-label' for='rdInativo'>Inativo</label>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("</div>");
                        }
                        else
                        {
                            stringHtml.Append("<div class='col-md-4'>");
                            stringHtml.Append("<div class='row'>");
                            stringHtml.Append("<span class='col-md-12 mb-2'>Status</span>");
                            stringHtml.Append("<div class='col-2  ml-4 mr-4'>");
                            stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdAtivo' value='1'>");
                            stringHtml.Append("<label class='form-check-label' for='rdAtivo'>Ativo</label>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("<div class='col-2  ml-4 mr-4'>");
                            stringHtml.Append("<input class='form-check-input' type='radio' name='rdStatus' id='rdInativo' value='0' checked=''>");
                            stringHtml.Append("<label class='form-check-label' for='rdInativo'>Inativo</label>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("</div>");
                            stringHtml.Append("</div>");
                        }
                        stringHtml.Append("</div>");
                        stringHtml.Append("</div>");
                        stringHtml.Append("</div>");

                    }
                }


                retorno.Message = "Registros removidos com sucesso";
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
        public static AjaxResponse salvarModalEditar(string sid, string f)
        {
            AjaxResponse retorno = new AjaxResponse();
            ClsSqlConnection Adados = new ClsSqlConnection();
            ClsQuerry Querry = new ClsQuerry();
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

                System.Collections.Specialized.NameValueCollection querySID = System.Web.HttpUtility.ParseQueryString(sid.Split('|')[0]);

                string id = sid.Split('|')[1];
                string controle = sid.Split('|')[2];
                string Strsigla = querySID["ctl00$Pesquisa$txtSiglaModal"].ToString();
                string Strnome = querySID["ctl00$Pesquisa$TxtNomeModal"].ToString();
                string Strcidade = querySID["ctl00$Pesquisa$TxtCidadeModal"].ToString();
                string Strservidor = Querry.EncryptString(querySID["ctl00$Pesquisa$TxtServidorModal"].ToString());
                string Strporta = querySID["ctl00$Pesquisa$TxtPortaModal"].ToString();
                string StrservidorInterno = querySID["ctl00$Pesquisa$TxtServidorInternoModal"].ToString();
                string StrportaInterno = querySID["ctl00$Pesquisa$TxtPortaInternoModal"].ToString();
                string Strusuario = Querry.EncryptString(querySID["ctl00$Pesquisa$TxtUsuarioModal"].ToString());
                string Strsenha = Querry.EncryptString(querySID["ctl00$Pesquisa$TxtSenhaModal"].ToString());
                string Strbanco = querySID["ctl00$Pesquisa$TxtBancoModal"].ToString();
                string StrIp = querySID["ctl00$Pesquisa$Txtip"].ToString();
                string Strwsintegracao = querySID["ctl00$Pesquisa$TxtWsInteModal"].ToString();
                string Strcomplento = Querry.EncryptString(querySID["TxtAreaCompleModal"].ToString());
                Boolean isStatus = (querySID["rdStatus"].ToString() == "1");
                Boolean isBloqueado = (querySID["chkBloqueado"] + "" != "");
                Boolean isSupAtivo = (querySID["chkAtiv"] + "" != "");

                string result = string.Empty;

                if (id == "null")
                {
                    result = Querry.SqlInsert(Strsigla, Strnome, Strcidade, Strservidor, Strporta, StrservidorInterno, StrportaInterno, Strusuario, Strsenha, StrIp, Strbanco, Strwsintegracao, Strcomplento, isBloqueado, isSupAtivo, isStatus);
                }
                else
                {
                    result = Querry.SqlUpdade(id, Strsigla, Strnome, Strcidade, Strservidor, Strporta, StrservidorInterno, StrportaInterno, Strusuario, Strsenha, StrIp, Strbanco, Strwsintegracao, Strcomplento, isBloqueado, isSupAtivo, isStatus);
                }


                retorno.Message = "Registros Gravados com sucesso";
                retorno.Data = result;
                retorno.Sucess = true;

            }
            catch (Exception)
            {
                retorno.Message = "Não foi possível realizar a ação solicitada";
                retorno.Data = String.Empty;
                retorno.Sucess = false;

                throw;
            }
            return retorno;
        }



        [System.Web.Services.WebMethod()]
        public static AjaxResponse getBaseData(string sid, string f)
        {
            AjaxResponse retorno = new AjaxResponse();
            ClsSqlConnection Adados = new ClsSqlConnection();

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
                Boolean isBloqueado = (queryS["chkBloqueado"] + "" != "");
                Boolean isAtivo = (queryS["chkAtivo"] + "" != "");
                string SiglaPesquisa = queryS["ctl00$Pesquisa$txtSigla"].ToString();
                string NomePesquisa = queryS["ctl00$Pesquisa$txtNome"].ToString();
                int StatusPesquisa = Convert.ToInt16(queryS["rdStatus"]);


                Adados.SqlConstruction();
                ClsQuerry Querry = new ClsQuerry();
                DataTable DtbReturn = new DataTable();
                System.Text.StringBuilder stringHtml = new StringBuilder();
                string sSql;

                sSql = Querry.SqlPesquisa(NomePesquisa, SiglaPesquisa, isBloqueado, isAtivo, StatusPesquisa);

                DtbReturn = Adados.SqlReturnData(sSql);

                foreach (DataRow row in DtbReturn.Rows)
                {

                    stringHtml.Append("<tr id='" + row["COD_CLIENTE_CONNECT"].ToString() + "' class='even pointer'>");
                    stringHtml.Append("<td>");
                    stringHtml.Append("<div class='btn btn-sm btn-default' onclick='openEdit(" + row["COD_CLIENTE_CONNECT"].ToString() + ");'><i class='fa fa-pencil' title='Alterar este Registro'>&nbsp;</i></div>");
                    stringHtml.Append("</td>");
                    stringHtml.Append("<td>" + row["SGL_CLIENTE"] + "</td>");
                    stringHtml.Append("<td>" + row["NOM_CLIENTE"] + "</td>");
                    stringHtml.Append("<td>" + row["NUM_TELEFONE"] + "</td>");
                    stringHtml.Append("<td>" + row["SUPORTE_ATIVO"] + "</td>");
                    stringHtml.Append("<td>" + row["SITUACAO"] + "</td>");
                    stringHtml.Append("<td>" + row["BLOQUEIO"] + "</td>");
                    stringHtml.Append("<td></td>");
                    stringHtml.Append("</tr>");
                }


                retorno.Message = "Registros removidos com sucesso";
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



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

    }
}