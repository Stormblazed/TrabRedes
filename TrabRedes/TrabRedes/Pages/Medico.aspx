<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Medico.aspx.cs" Inherits="TrabRedes.Pages.Medico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../scripts/datatables/datatables.min.css" rel="stylesheet" />
    <link href="../scripts/datepicker/datepicker.min.css" rel="stylesheet" />
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
                            <asp:ListItem Value="0">EMAIL</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">NOME</asp:ListItem>
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
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">NOME</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">EMAIL</th>
                        <th class="column-title sorting_asc_disabled sorting_desc_disabled" data-priority="1" style="display: table-cell;">USUARIO VINCULADO</th>
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
                            <span>* NOME:</span>
                            <asp:TextBox ID="txtnome" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <span>* EMAIL:</span>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <span>* USUARIO:</span>
                            <asp:DropDownList ID="ddlusuario" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>                                                   
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

        function openEdit(t) {
           
            if (t == "NOVO") {
                $('#<%= btnEditar.ClientID %>').click();                
            } else if (t == "VOLTAR") {                
                location.href = '../Pages/Medico.aspx';
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
                    location.href = '../Pages/Medico.aspx';
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
                        location.href = '../Pages/Medico.aspx';
                    }
                });
            });

        }

        
    </script>
</asp:Content>
