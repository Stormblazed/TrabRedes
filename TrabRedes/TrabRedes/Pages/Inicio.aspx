<%@ Page Language="C#" MasterPageFile="~/Master/MasterSib.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Sib.Bessatec.Pages.Inicio" Title="Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/datatables/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Pesquisa" runat="Server">
    <div class="card card-info card-outline">
        <div class="card-body">
            <h5>Pesquisa</h5>
            <div class="row pt-3">
                <div class="col-md-3">
                    <span>Sigla</span>
                    <asp:TextBox ID="txtSigla" CssClass="form-control-sm form-control mt-1" runat="server" onkeypress=""></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <span>Nome</span>
                    <asp:TextBox ID="txtNome" CssClass="form-control-sm form-control mt-1" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-5">
                    <span>Status</span>
                    <div class="row mt-2 ">
                        <div class="col-1  ml-4 mr-4">
                            <input class="form-check-input" type="radio" name="rdStatus" id="rdAtivo" value="1" checked>
                            <label class="form-check-label" for="rdAtivo">Ativo</label>
                        </div>
                        <div class="col-1 mr-4">
                            <input class="form-check-input" type="radio" name="rdStatus" id="rdInativo" value="0">
                            <label class="form-check-label" for="rdInativo">Inativo</label>
                        </div>
                        <div class="col-7">
                            <span class="checkbox mr-3">
                                <input id="chkBloqueado" type="checkbox" name="chkBloqueado" title="Empresa Bloqueada">
                                <label for="chkBloqueado" title="Bloqueado">&nbsp;Bloqueado</label></span>
                            <span class="checkbox">
                                <input id="chkAtivo" type="checkbox" name="chkAtivo" title="Suporte Ativo" checked>
                                <label for="chkAtivo" title="Suporte Ativo">&nbsp;Suporte Ativo</label></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3  d-flex align-items-end">
                    <div class="btn btn-sm btn-success mr-2" onclick="openEdit('null');">Adicionar&nbsp;<i class="fa fa-check-circle" title="Adicionar registro"></i></div>
                    <div class="btn btn-sm btn-info" id="btnpesquisar" onclick="Pesquisar(this);">Pesquisar&nbsp;<i class="fa fa-search" title="Pesquisar os registros"></i></div>
                </div>
            </div>
        </div>
    </div>

    <div class="card card-secondary card-outline">
        <div class="card-body">
            <h5>Resultado da pesquisa</h5>
            <table id="tableLista" class="SMA-table table table-striped cell-border display w-100">
                <thead>
                    <tr class="headings">
                        <th data-priority="1" style="display: table-cell; width: 1%"></th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">SIGLA</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">CLIENTE</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">TELEFONE</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">SUPORTE</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">SITAÇÃO</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">BLOQUEIO</th>
                        <th data-priority="1" style="display: table-cell; width: 1%"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="odd">
                        <td colspan="100%" class="dataTables_empty text-center">Nenhum registro encontrado</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript" src="../Scripts/datatables/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Pesquisar();
            $("#<%=txtSigla.ClientID%>,#<%=txtNome.ClientID%> ").keypress(function (_event) {
                if (_event.which == '13') {
                    Pesquisar();
                }
            });
        });

        function Pesquisar(el) {
            disableButtonAndWait(el);
            constructDefaultDataTable({ buttons: createDefaultButtonRemover() }, function () {
                enableButtonAndRemoveWait(el);
            });
        }

        function MostPassword() {

            var passwordField = $('#Pesquisa_TxtSenhaModal');
            var passwordFieldType = passwordField.attr('type');
            if (passwordFieldType == 'password') {
                passwordField.attr('type', 'text');
                $(this).val('Hide');
            }
            else {
                passwordField.attr('type', 'password');
                $(this).val('Show');
            }
        };

        function openEdit(el, call) {
            doSecureAction({
                sid: { sid: el + "|" + call, f: $("form:eq(0)").serialize() },
                method: 'getDados'
            }, function (_d) {
                    
                    var _modal = openModalConfirm({ message: _d.Data, oklabel: "Gravar", cancellabel: "Cancelar", size: "modal-xl", function: "MostPassword()" }, function () {
                    doSecureAction({
                        sid: { sid: _modal.find(":input").serialize() + "|" + el + "|" + call, f: $("form:eq(0)").serialize() },
                        method: 'salvarModalEditar'

                    }, function (_d) {
                        $(".modal-backdrop").removeClass("show");
                        $(".modal-backdrop").addClass("hide");
                        $(".modal").removeClass("show");
                        $(".modal").addClass("hide");
                        openModalMsg(_d.Message, false, function () {
                            Pesquisar();
                           
                        });
                       
                           

                    })
                });
            })
        }



    </script>
</asp:Content>

