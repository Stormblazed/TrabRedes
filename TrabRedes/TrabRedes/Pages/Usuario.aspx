<%@ Page Title="Cadastro de Usuario" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="TrabRedes.Pages.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../scripts/datatables/datatables.min.css" rel="stylesheet" />
    <link href="../scripts/datepicker/datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Pesquisa" ContentPlaceHolderID="Pesquisa" runat="server">
    <div class="card bg-light-gradient mb-2">
        <div class="card-body">
            <div class="row">                
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Nick</label>
                        <input id="txtNick" runat="server" class="form-control form-control-sm" type="text">
                    </div>
                </div>
                <div class="col-md-10  d-flex align-items-end float-xl-left">
                    <div class="btn btn-sm btn-info" onclick="Pesquisar(this);">Pesquisar&nbsp;<i class="fa fa-search" title="Pesquisar os registros"></i></div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="card bg-light-gradient mb-2">
        <div class="card-body">
            <div class="row">                
        <table id="tableLista" class="SMA-table table table-striped cell-border display w-100">
        <thead>
            <tr class="headings">
                <th data-priority="1" style="display: table-cell; width: 1%"></th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">NOME</th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">CPF</th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">CIDADE</th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">TELEFONE</th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">CADASTRO</th>
                <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">USUÁRIO</th>
                <th data-priority="1" style="display: table-cell; width: 1%"></th>
            </tr>
        </thead>
        <tbody>
            <tr class="odd">
                <td colspan="100%" class="dataTables_empty">Nenhum registro encontrado</td>
            </tr>
        </tbody>
    </table>
                </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Edicao" ContentPlaceHolderID="Edicao" runat="server">
    <div class="col-md-12">

        <div class="card mt-3 card-info">
            <div class="card-header">
                <span>DADOS PESSOAIS</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <span>* Nome:</span>
                        <asp:TextBox ID="txtNome" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* CPF:</span>
                        <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control form-control-sm form-control-pfj"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* RG:</span>
                        <asp:TextBox ID="txtRg" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <span>* Orgão Exp:</span>
                        <asp:DropDownList ID="ddlOrgExp" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-1 pl-4">
                        <span>* Nascimento:</span>
                        <asp:TextBox ID="txtNasc" runat="server" MaxLength="10" CssClass="form-control form-control-sm form-control-date"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* Mãe:</span>
                        <asp:TextBox ID="txtMae" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Pai:</span>
                        <asp:TextBox ID="txtPai" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* Escolaridade:</span>
                        <asp:DropDownList ID="ddlEscolaridade" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 mt-1">
                        <span>Naturalidade:</span>
                        <asp:TextBox ID="txtNaturalidade" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mt-1">
                        <span>Carteira de Reservista:</span>
                        <asp:TextBox ID="txtReservista" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mt-1">
                        <span>CNH:</span>
                        <asp:TextBox ID="txtCnh" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mt-1">
                        <span>Telefone/Celular:</span>
                        <asp:TextBox ID="txtTelCel" runat="server" CssClass="form-control form-control-sm form-control-phone"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mt-1">
                        <span>Grupo Sanguineo</span>
                        <asp:TextBox ID="txtGrupoSang" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>


        <div class="card mt-3 card-info">
            <div class="card-header">
                <span>ENDEREÇO</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-1">
                        <span>Tipo:</span>
                        <asp:DropDownList ID="ddlTipoLogradouro" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>Logradouro:</span>
                        <asp:TextBox ID="txtLogradouro" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <span>Nº:</span>
                        <asp:TextBox ID="txtNumEnd" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Complemento:</span>
                        <asp:TextBox ID="txtComplemento" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Bairro:</span>
                        <asp:TextBox ID="txtBairro" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Cidade:</span>
                        <asp:TextBox ID="txtLocalidade" runat="server" Style="text-transform: uppercase" CssClass="form-control form-control-sm autocomplete" data-value="localidade"></asp:TextBox>
                        <asp:HiddenField ID="hideCidadePesquisa" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <span>CEP:</span>
                        <div class="justify-content-start d-flex">
                            <asp:TextBox ID="txtCep" runat="server" CssClass="form-control form-control-sm form-control-cep" />
                            <div class="btn btn-sm btn-info" onclick="consultarCEP(this);">Consultar</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="card mt-3 card-info">
            <div class="card-header">
                <span>REGISTRO PROFISSIONAL</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <span>Função:</span>
                        <asp:DropDownList ID="ddlFuncao" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>Departamento:</span>
                        <asp:DropDownList ID="ddlDepart" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>Situação:</span>
                        <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>Unidade:</span>
                        <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>* PIS:</span>
                        <asp:TextBox ID="txtPis" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* CTPS:</span>
                        <asp:TextBox ID="txtCtps" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* Salário:</span>
                        <asp:TextBox ID="txtSal" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>* Carga Horária:</span>
                        <asp:TextBox ID="txtCargaHora" runat="server" MaxLength="2" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <span>* Admissão:</span>
                        <asp:TextBox ID="txtAdmissao" runat="server" MaxLength="10" CssClass="form-control form-control-sm form-control-date"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Demissão:</span>
                        <asp:TextBox ID="txtDemissao" runat="server" MaxLength="10" CssClass="form-control form-control-sm form-control-date"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>


        <div class="card mt-3 card-info">
            <div class="card-header">
                <span>DADOS BANCÁRIOS</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-1">
                        <span>Tipo Conta:</span>
                        <asp:DropDownList ID="ddlTipoConta" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <span>* Banco:</span>
                        <asp:TextBox ID="txtBanco" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <span>* Agência:</span>
                        <asp:TextBox ID="txtAg" runat="server" MaxLength="5" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <span>* Conta:</span>
                        <asp:TextBox ID="txtConta" runat="server" MaxLength="12" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <span>Observação:</span>
                        <asp:TextBox ID="txtObs" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="footer" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript" src="../scripts/datatables/datatables.min.js"></script>
    <script type="text/javascript" src="../scripts/autocomplete/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="../scripts/datepicker/datepicker.min.js"></script>
    <script src="../scripts/inputmask/jquery.inputmask.bundle.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Pesquisar();
        });

        function Pesquisar(el) {
            disableButtonAndWait(el);
            constructDefaultDataTable({ buttons: createDefaultButtonRemover() }, function () {
                enableButtonAndRemoveWait(el);
            });
        }

        function setModal(id, type) {
            enableButtonAndRemoveWait(id); ''
            doSecureAction({
                sid: {
                    sid: JSON.stringify({
                        "type": type
                        , "codigo": id
                    }),
                    f: $("form:eq(0)").serialize()
                },
                method: "modalEmpregado"
            }, function (_d) {

                //modal
                var _modal = openModalConfirm({
                    message: _d.Data,
                    oklabel: "Gravar",
                    cancellabel: "Cancelar",
                    size: "modal-xl"
                }, function () {

                    doSecureAction({
                        sid: {
                            sid: JSON.stringify({
                                "serialize": _modal.find(":input").serialize(),
                                "type": type,
                                "codigo": id
                            }),
                            f: $("form:eq(0)").serialize()
                        },
                        method: "gravaEmpregado"
                    }, function (_d) {
                        d.Data;
                    }, function (_msg) {
                        openModalMsg(_msg, false);
                    });

                }); //final modal

            }, function (_msg) {
                openModalMsg(_msg, false);
            });
        }
        //id = COD_EMPREGADO
        //type = tipo de modal que irá abrir
        function openView(_id, _type) {
            if (_type == "FotoEmpregado") {
                var _url = "../Processo/ModalTrocaImagem.aspx?sId=" + _id + "&sTipo=" + _type;
                var _size = "small";
                createIframeModal(_url, _size);
            } else {
                setModal(_id, _type);
            }
        }

      <%--  function consultarCEP(el) {
            if (!isButtonEnabled($(el))) { return false; }
            disableButtonAndWait($(el));

            doSecureAction({
                sid: {
                    sid: "getCEP", f: $("form:eq(0)").serialize()
                },
                method: "getCEP"
            },

            function (_d) {
                var _ret = JSON.parse(_d.Data);
                $("#<%=txtLogradouro.ClientID%>").val(_ret.logradouro);
                $("#<%=txtBairro.ClientID%>").val(_ret.bairro);
                $("#<%=hideCidadePesquisa.ClientID%>").val(_ret.sid);
                $("#<%=txtLocalidade.ClientID%>").val(_ret.cidade + " - " + _ret.uf);
                $("#<%=ddlTipoLogradouro.ClientID%>").val("");
                $("#<%=txtNumEnd.ClientID%>").val("");
                enableButtonAndRemoveWait(el);
            }, function (msg) {
                openModalMsg(msg, true);
                enableButtonAndRemoveWait(el);
            });
        }--%>
    </script>
</asp:Content>
