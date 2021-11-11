<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TrabRedes.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Pesquisa" runat="server">
    <div style="width: 100%; height: 100%;">
        <div class="row ">
            <div class="col-md-2"></div>
            <div class="col-md-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row m-2">                          
                            <input name='txtNick' type='text' id='txtNick' class='form-control-sm form-control mt-1' aria-describedby='basic-addon1'  placeholder="Email/username">
                        </div>
                        <div class="row m-2">
                            <input name='txtSenha' type='password' id='txtSenha' class='form-control-sm form-control mt-1' aria-describedby='basic-addon1'  placeholder="*********">                            
                        </div>
                        <div class="row m-2">
                            <div class="col-md-4"></div>
                            <div class="btn btn-sm btn-success mr-2" onclick="openLogin('null');">Login&nbsp;<i class="fa fa-user" title="Login no Sistema"></i></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        function openLogin(el, call) {
            doSecureAction({
                sid: { sid: el + "|" + call, f: $("form:eq(0)").serialize() },
                method: 'getLogin'
            }, function (_d) {
                if (_d.Message == "Logado") {
                    window.location.href = 'InicioPage.aspx';
                } else {
                    openModalMsg(_d.Message, false);
                }

                
            });

        }
    </script>
</asp:Content>
