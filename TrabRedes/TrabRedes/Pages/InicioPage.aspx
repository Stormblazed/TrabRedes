<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="InicioPage.aspx.cs" Inherits="TrabRedes.Pages.InicioPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Pesquisa" runat="server">
    <div class="row clearfix" id="divShortcut">
        <div class='col-md-4' id='divUsuario'>
            <div class='small-box bg-gradient-light'>
                <div class='inner'>
                    <h5>Cadastro Usuário</h5>
                    <p>Acesso a Area de Cadastro de Usuário</p>
                </div>
                <div class='icon'>
                    <i class='fa fa-users'></i>
                </div>
                <a href='../Pages/Usuario.aspx' class='small-box-footer'>Acessar <i class='fa fa-arrow-circle-right'></i>
                </a>
            </div>
        </div>
        <div class='col-md-4' id='divMedico'>
            <div class='small-box bg-gradient-light'>
                <div class='inner'>
                    <h5>Cadastro Medico</h5>
                    <p>Acesso a Area de Cadastro de Medico</p>
                </div>
                <div class='icon'>
                    <i class='fa fa-user'></i>
                </div>
                <a href='../Pages/Medico.aspx' class='small-box-footer'>Acessar <i class='fa fa-arrow-circle-right'></i>
                </a>
            </div>
        </div>
        <div class='col-md-4' id='divPaciente'>
            <div class='small-box bg-gradient-light'>
                <div class='inner'>
                    <h5>Cadastro Paciente</h5>
                    <p>Acesso a Area de Cadastro de Paciente</p>
                </div>
                <div class='icon'>
                    <i class='fa fa-hospital'></i>
                </div>
                <a href='../Pages/Paciente.aspx' class='small-box-footer'>Acessar <i class='fa fa-arrow-circle-right'></i>
                </a>
            </div>
        </div>
        <div class='col-md-4' id='divAtendimento'>
            <div class='small-box bg-gradient-light'>
                <div class='inner'>
                    <h5>Cadastro Atendimento</h5>
                    <p>Acesso a Area de Atendimentos</p>
                </div>
                <div class='icon'>
                    <i class='fa fa-medkit'></i>
                </div>
                <a href='../Pages/Atendimento.aspx' class='small-box-footer'>Acessar <i class='fa fa-arrow-circle-right'></i>
                </a>
            </div>
        </div>
        

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
