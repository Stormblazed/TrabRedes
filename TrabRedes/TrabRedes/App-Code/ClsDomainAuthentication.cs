using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.DirectoryServices.AccountManagement;

namespace Sib.Bessatec.Classe
{
    public struct Credentials
    {
        public string Username;
        public string Password;
       
    }

   
    public class ClsDomainAuthentication
    {
        public Credentials Credentials;
        public string Domain;
        public Boolean Login = false;

        public ClsDomainAuthentication(string Username, string Password, string SDomain)
        {
            Credentials.Username = Username;
            Credentials.Password = Password;
            Domain = SDomain;
        }

        public bool IsValid()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain))
            {
                Login = pc.ValidateCredentials(Credentials.Username, Credentials.Password);
                return Login;
            }
        }

       
    }
       
}



