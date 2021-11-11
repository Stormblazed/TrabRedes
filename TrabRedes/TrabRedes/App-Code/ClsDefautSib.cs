using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabRedes.App_Code
{
    public class ClsDefautSib
    {

        public Boolean validateAjaxRequest(string f)
        {
            Boolean validRequest = true;

            return validRequest;
            //incompleto ainda

            //        Public Function validateAjaxRequest(f As String) As Boolean
            //    Dim validRequest As Boolean = True

            //    'DEBUG
            //    Return validRequest

            //    Try
            //        Dim queryS As System.Collections.Specialized.NameValueCollection = System.Web.HttpUtility.ParseQueryString(f)
            //        Dim param As String = convertFromSID(queryS("ctl00$__token") & "")
            //        If Trim(param) <> Date.Now.ToString("dd/MM/yyyy") Then
            //            Throw New Exception("Token não é válido")
            //        End If
            //        param = convertFromSID(queryS(__SMAEditControls.__SID) & "")
            //        If Trim(param) = String.Empty Then
            //            Throw New Exception("SID não é válido")
            //        End If

            //        'Vamos monitorar as atividades do usuário
            //        'Call watchUser(f)

            //        validRequest = True
            //    Catch ex As Exception
            //        validRequest = False
            //    End Try
            //    Return validRequest
            //End Function


        }

        public struct structJsonInputData
        {
            public string id1;
            public string id2;
        }

        public static T getJsonData<T>(string jSon)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;

            return jsSerializer.Deserialize<T>(jSon);
        }

        public static string convertToBase64(string param)
        {
            if (param.ToString() == string.Empty)
                return param;
            return System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(param));
        }


    }
}