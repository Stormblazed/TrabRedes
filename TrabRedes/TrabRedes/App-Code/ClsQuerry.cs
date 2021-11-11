using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.Controles;
using System.Data;


namespace Sib.Bessatec.Classe
{
    public class ClsQuerry
    {

        public string SqlPesquisa(string PesquisaNome = "", string PesquisaSigla = "", Boolean ChkBlock = false, Boolean ChkSuportePesq = true, int OptStatus = 1)
        {
            string sSql = string.Empty;
            sSql = string.Empty;
            sSql = "SELECT DISTINCT " +
                   "COD_CLIENTE_CONNECT,ISNULL(NOM_CLIENTE,'-') [NOM_CLIENTE], NUM_TELEFONE, CASE WHEN ISNULL(IND_SITUACAO , 0) = 1 THEN 'ATIVO' ELSE 'INATIVO' END AS [SITUACAO] , CASE WHEN ISNULL(IND_SUPORTE_ATIVO , 0) = 1 THEN 'SUPORTE ATIVO' ELSE 'SUPORTE INATIVO' END AS [SUPORTE_ATIVO] ,  NOM_LOCALIDADE, ISNULL(SGL_CLIENTE,'-') [SGL_CLIENTE], COD_CLIENTE_CONNECT , " +
                   "CASE WHEN ISNULL(IND_BLOQUEIO , 0) = 1 THEN 'SIM' ELSE 'NÃO' END AS [BLOQUEIO]  " +
                   "FROM RECOLL_WB..VW_CLIENTE_SIB WHERE 1=1 AND COD_CLIENTE_CONNECT IS NOT NULL ";
            if (PesquisaNome != "")
            {
                sSql += " AND NOM_CLIENTE LIKE '%" + PesquisaNome + "%'";
            }
            if (PesquisaSigla != "")
            {
                sSql += " AND SGL_CLIENTE LIKE '%" + PesquisaSigla + "%'";
            }
            if (ChkBlock == true)
            {
                sSql += " AND ISNULL(IND_BLOQUEIO , 0) = " + 1;
            }
            else
            {
                sSql += " AND ISNULL(IND_BLOQUEIO , 0) = " + 0;
            }
            if (ChkSuportePesq == true)
            {
                sSql += " AND ISNULL(IND_SUPORTE_ATIVO , 0) = " + 1;
            }
            else
            {
                sSql += " AND ISNULL(IND_SUPORTE_ATIVO , 0) = " + 0;
            }

            sSql += " AND ISNULL(IND_SITUACAO , 0) = " + OptStatus +
                " ORDER BY SGL_CLIENTE ";

            return sSql;

        }

        public string SqlPesquisaId(string id = "")
        {
            string sSql = string.Empty;
            sSql = string.Empty;
            sSql = "SELECT * FROM RECOLL_WB..VW_CLIENTE_SIB VW LEFT JOIN CLIENTE C ON C.COD_CLIENTE = VW.COD_CLIENTE WHERE 1=1 AND COD_CLIENTE_CONNECT IS NOT NULL ";
            if (id != "")
            {
                sSql += " AND COD_CLIENTE_CONNECT = '" + id + "'";
            }
            sSql += " ORDER BY SGL_CLIENTE ";
            return sSql;

        }

        public string SqlInsert(string Strsigla = "", string Strnome = "", string Strcidade = "", string Strservidor = "", string Strporta = "", string StrservidorInterno = "", string StrportaInterno = "", string Strusuario = "", string Strsenha = "", string Strbanco = "", string Strip = "", string Strwsintegracao = "", string Strcomplento = "", Boolean isBloqueado = false, Boolean isSupAtivo = false, Boolean isStatus = false)
        {
            try
            {

                ClsSqlConnection Adados = new ClsSqlConnection();
                Adados.SqlConstruction();
                string sSql = string.Empty;
                DataTable DtbReturn = new DataTable();
                string strcodcidpesq = string.Empty;
                string strcodnomepesq = string.Empty;
                if (Strcidade != "")
                {
                    sSql = "SELECT COD_LOCALIDADE FROM LOCALIDADE WHERE NOM_LOCALIDADE LIKE '" + Strcidade + "'";
                    DtbReturn = Adados.SqlReturnData(sSql);

                    if (DtbReturn.Rows.Count != 0)
                    {
                        strcodcidpesq = DtbReturn.Rows[0][0].ToString();
                    }
                    else
                    {
                        strcodcidpesq = "0";
                    }
                    sSql = string.Empty;
                    DtbReturn.Clear();
                }


                if (Strnome != "")
                {
                    sSql = "SELECT COD_CLIENTE FROM CLIENTE WHERE 1=1" +
                        "AND NOM_CLIENTE LIKE '" + Strnome + "'";
                    DtbReturn = Adados.SqlReturnData(sSql);

                    if (DtbReturn.Rows.Count != 0)
                    {
                        strcodnomepesq = DtbReturn.Rows[0][0].ToString();
                        sSql = "UPDATE CLIENTE SET IND_BLOQUEIO = '"+ isBloqueado +"' WHERE COD_CLIENTE = '" + strcodnomepesq + "'";

                    }
                    else { strcodnomepesq = "0"; }

                    sSql = string.Empty;
                    DtbReturn.Clear();
                }




                sSql = "INSERT INTO dbo.CLIENTE_CONNECT ( COD_CLIENTE,SGL_CLIENTE,NOM_EMPRESA," +
                    "NOM_SERVER_SGDB,NOM_USER_SGDB,DSC_PWD_SGDB,NOM_SGDB," +
                    "NUM_PORT_SGDB,NUM_PORT_SGDB_INTERNA,NUM_IP_EXTERNO," +
                    "NUM_IP_INTERNO,END_EMAIL_NOTIFICACAO,DSC_COMPLEMENTO," +
                    "IND_SITUACAO,URL_INTEGRACAO,IND_CONNECT_OK," +
                    "NUM_VERSAO_DESK,NUM_VERSAO_DBG,NUM_VERSAO_DBM," +
                    "NUM_VERSAO_APP,NUM_SERIE,QTD_ACESSO,DSC_TIPO_INSTALACAO," +
                    "DAT_INSTALACAO,DAT_ENCERRAMENTO,QTD_ACESSO_LIBERADO," +
                    "IND_SUPORTE_ATIVO,NOM_HOSTNAME,DAT_VALIDACAO_WS," +
                    "IND_BANCO_BESSA,NOM_SERVIDOR_BESSA,DAT_ULTIMA_ATUALIZACAO," +
                    "DSC_MOTIVO_BLOQUEIO,DAT_CTR_INCLUSAO,DAT_CTR_ALTERACAO,NOM_CTR_ACESSO," +
                    "NOM_CTR_PROCESSO ) VALUES (" + strcodnomepesq + ",'" + Strsigla + "','" + Strnome + "','" + Strservidor + "','" + Strusuario + "','" + Strsenha + "','" + Strbanco + "','" + Strporta + "','" + StrportaInterno + "', +'" + Strip + "', '" + StrservidorInterno + "' , NULL , '" + Strcomplento + "', '" + isStatus +
                    "', NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,GETDATE(),NULL , 0 ,'" + isSupAtivo + "','Bessatec',GETDATE(),NULL,NULL,GETDATE(),NULL,NULL,NULL,'ADMINISTRADOR','SIB')";

                Adados.SqlExecutaData(sSql);
                return "Exito";
            }
            catch (Exception)
            {
                return "Erro";
                throw;
            }



        }
        public string SqlUpdade(string ID, string Strsigla = "", string Strnome = "", string Strcidade = "", string Strservidor = "", string Strporta = "", string StrservidorInterno = "", string StrportaInterno = "", string Strusuario = "", string Strsenha = "", string Strip = "", string Strbanco = "", string Strwsintegracao = "", string Strcomplento = "", Boolean isBloqueado = false, Boolean isSupAtivo = false, Boolean isStatus = true)
        {

            try
            {
                ClsSqlConnection Adados = new ClsSqlConnection();
                Adados.SqlConstruction();
                string sSql = string.Empty;
                DataTable DtbReturn = new DataTable();
                string strcodcidpesq = string.Empty;
                string strcodnomepesq = string.Empty;
                if (Strcidade != "")
                {
                    sSql = "SELECT COD_LOCALIDADE FROM LOCALIDADE WHERE NOM_LOCALIDADE LIKE '" + Strcidade + "'";
                    DtbReturn = Adados.SqlReturnData(sSql);

                    if (DtbReturn.Rows.Count != 0)
                    {
                        strcodcidpesq = DtbReturn.Rows[0][0].ToString();
                    }
                    else
                    {
                        strcodcidpesq = "0";
                    }
                    sSql = string.Empty;
                    DtbReturn.Clear();
                }


                if (Strnome != "")
                {
                    sSql = "SELECT COD_CLIENTE FROM CLIENTE WHERE 1=1" +
                        "AND NOM_CLIENTE LIKE '" + Strnome + "'";
                    DtbReturn = Adados.SqlReturnData(sSql);

                    if (DtbReturn.Rows.Count != 0)
                    {
                        strcodnomepesq = DtbReturn.Rows[0][0].ToString();
                        sSql = "UPDATE CLIENTE SET IND_BLOQUEIO = '" + isBloqueado + "' WHERE COD_CLIENTE = '" + strcodnomepesq + "'";
                    }
                    else { strcodnomepesq = "0"; }

                    sSql = string.Empty;
                    DtbReturn.Clear();
                }


                sSql = "UPDATE CLIENTE_CONNECT SET COD_CLIENTE = '" + strcodnomepesq + "',  SGL_CLIENTE ='" + Strsigla + "' , NOM_EMPRESA ='" + Strnome + "' , NOM_SERVER_SGDB = '" + Strservidor + "' , NUM_PORT_SGDB_INTERNA = '" + StrportaInterno + "' " +
                    " , NUM_IP_INTERNO = '" + StrservidorInterno + "' , NOM_USER_SGDB = '" + Strusuario + "' , DSC_PWD_SGDB ='" + Strsenha + "' " +
                    ", NOM_SGDB ='" + Strbanco + "' , NUM_PORT_SGDB = '" + Strporta + "' , NUM_IP_EXTERNO = '" + Strip + "' ,  DSC_COMPLEMENTO = '" + Strcomplento + "' , IND_SITUACAO='" + isStatus + "'  ,  IND_SUPORTE_ATIVO = '" + isSupAtivo + "' WHERE COD_CLIENTE_CONNECT = '"+ ID + "' ";

                Adados.SqlExecutaData(sSql);


                return "Exito";
            }
            catch (Exception ex)
            {
                return "Erro :" + ex.Message ;
                throw;
            }
        }


        public string DecryptString(string sid)
        {
            return Security.DecryptStringPublic(sid);
        }

        public string EncryptString(string sid)
        {
            return Security.EncryptStringPublic(sid);
        }

    }
}
