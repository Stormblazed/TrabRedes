<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Atendimento.aspx.cs" Inherits="TrabRedes.Pages.Atendimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../scripts/datatables/datatables.min.css" rel="stylesheet" />
    <link href="../scripts/datepicker/datepicker.min.css" rel="stylesheet" />
      <link href="../scripts/summernote/summernote.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Pesquisa" ContentPlaceHolderID="Pesquisa" runat="server">
    <asp:HiddenField runat="server" ID="hideUsuario" />
    <asp:Button ID="btnEditar" class="d-none" runat="server" OnClick="btnEditar_Click" />
    <div class="card bg-light-gradient mb-2">
        <div class="card-header">
             <div class="btn btn-sm btn-danger mr-2" onclick="openEdit('VOLTARINICIO');">Voltar&nbsp;<i class="fa fa-arrow-left" title="Voltar"></i></div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="justify-content-start d-flex">
                        <asp:DropDownList ID="OptFiltroPesquisa" runat="server" CssClass="form-control form-control-sm w-50 mr-1">
                            <asp:ListItem Value="0">PACIENTE</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">MEDICO</asp:ListItem>
                        </asp:DropDownList>
                        <input id="txtPesquisa" runat="server" class="form-control form-control-sm" type="text">
                    </div>
                </div>
                <div class="col-md-8  d-flex align-items-end float-xl-left">
                    <div class="btn btn-sm btn-primary mr-2" onclick="openEdit('NOVO');">Novo Registro&nbsp;<i class="fa fa-plus" title="Adicionar registros"></i></div>
                    <div class="btn btn-sm btn-info" onclick="Pesquisar(this);">Pesquisar&nbsp;<i class="fa fa-search" title="Pesquisar os registros"></i></div>
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
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">PACIENTE</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">MEDICO</th>                        
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

<asp:Content ID="Edicao" ContentPlaceHolderID="Edicao" runat="server">
      <asp:HiddenField runat="server" ID="hideusuarioeditar" />
    <div  class="row">
          <div class="col-md-8  d-flex align-items-end float-xl-left">
                    <div class="btn btn-sm btn-danger mr-2" onclick="openEdit('VOLTAR');">Voltar&nbsp;<i class="fa fa-arrow-left" title="Voltar"></i></div>
                    <div class="btn btn-sm btn-success" onclick="openSalvar(this);">Gravar&nbsp;<i class="fa fa-save" title="Gravar os registros"></i></div>
                </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card mt-3 card-info">
                <div class="card-header">
                    <span>DADOS PESSOAIS</span>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <span>* PACIENTE:</span>
                            <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>                                                   
                        </div>
                        <div class="col-md-2">
                            <span>* MEDICO:</span>
                            <asp:DropDownList ID="ddlmedico" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>                                                   
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <span>* Exame:</span>
                            <div class="summernote"></div>
                            <asp:HiddenField ID="hideMensagem" runat="server"></asp:HiddenField>
                        </div>
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
      <script type="text/javascript" src="../scripts/summernote/summernote.min.js"></script>
    <script type="text/javascript" src="../scripts/summernote/lang/summernote-pt-BR.min.js"></script>
    <script src="../scripts/inputmask/jquery.inputmask.bundle.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Pesquisar();
            createSummernote()
        });

        function Pesquisar(el) {
            disableButtonAndWait(el);
            constructDefaultDataTable({ buttons: createDefaultButtonRemover() }, function () {
                enableButtonAndRemoveWait(el);
            });
        }

        function openEdit(t) {
           
            if (t == "NOVO") {
                $('#<%= btnEditar.ClientID %>').click();                
            } else if (t == "VOLTAR") {                
                location.href = '../Pages/Atendimento.aspx';
            } else if (t == "VOLTARINICIO") {
                location.href = '../Pages/InicioPage.aspx';
            } else if (t != "") {
                $('#<%= hideUsuario.ClientID %>').val(t);
                $('#<%= btnEditar.ClientID %>').click();
            }

        }

        function openSalvar(el) {
            doSecureAction({
                sid: { sid: "" , f: $("form:eq(0)").serialize() },
                method: 'getSalvarUsuario'
            }, function (_d) {
                openModalMsg(_d.Message, false, function () {
                if (_d.Message == "Alterado com Sucesso") {
                    location.href = '../Pages/Atendimento.aspx';
                }
                });
            });

        }

        function openDelete(dt) {
            var sid = dt.rows({ selected: true }).nodes().to$().map(function (e) { return $(this).attr("id"); }).get().join();
            doSecureAction({
                sid: { sid: sid, f: $("form:eq(0)").serialize() },
                method: 'getDelete'
            }, function (_d) {
                openModalMsg(_d.Message, false, function () {
                    if (_d.Message == "Deletado com Sucesso") {
                        location.href = '../Pages/Atendimento.aspx';
                    }
                });
            });

        }


        function createSummernote() {
            var _codeRefresh = atob($("#<%=hideMensagem.ClientID%>").val());

             $('.summernote').summernote({
                 height: 200,
                 lang: 'pt-BR',
                 code: _codeRefresh,
                 toolbar: [
                     ['style', ['style']],
                     ['font', ['bold', 'italic', 'underline', 'clear']],
                     ['fontname', ['fontname']],
                     ['color', ['color']],
                     ['para', ['ul', 'ol', 'paragraph']],
                     ['height', ['height']],
                     ['table', ['table']],
                     ['insert', ['link', 'picture', 'hr']],
                     ['visualizar', ['visualizar']]
                 ],
                 buttons: {
                     visualizar: function (context) {
                         var ui = $.summernote.ui;
                         // create button
                         var button = ui.button({
                             contents: '<i class="fa fa-eye"/> Visualizar',
                             tooltip: 'Visualizar mensagem',
                             click: function () {
                                 var _code = "";
                                 _code = "<div style='overflow-x:auto;' class='w-100'><h5>EXAME</h5>";
                                _code = _code + atob($("#<%=hideMensagem.ClientID%>").val());
                                _code = _code + "</div>";

                                //context.invoke('editor.insertText', 'hello');
                                openModalMsg(_code, { size: "modal-full" })
                            }
                        });

                        return button.render();   // return button as jquery object
                    }
                }
            });

            if (_codeRefresh !== undefined) {
                $('.summernote').summernote('code', _codeRefresh);
            }

            $(".summernote").on('summernote.blur', function () {
                var el = this;
                $("#<%=hideMensagem.ClientID%>").val(btoa($(el).summernote('code')));
            });
         }
        
    </script>
</asp:Content>
