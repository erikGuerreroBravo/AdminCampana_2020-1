using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//cargamos la libreria que necesitamos para los correos
using System.Net;
using System.Net.Mail;
using AdminCampana_2020.Domain;

namespace AdminCampana_2020.Message.Recovery
{
    public class RecoveryPassword
    {
        public bool RecuperarPasswordAccount(EmailDomainModel emailDM)
        {
            bool respuesta = false;
            try
            {
                //creamos el objeto del tipo mailmessage
                MailMessage correo = new MailMessage();
                ///establecemos el objeto mail adrees con el correo de salida es decir el intermediario
                correo.From = new MailAddress("bravo.guerrero.erik@gmail.com");
                ///la direccion de correo a donde enviaremos nuestro de email
                correo.To.Add(emailDM.FromEmail);
                //enviamos el asunto del email
                correo.Subject = emailDM.Asunto;
                //el mensaje del correo email
                correo.Body = emailDM.Mensaje;
                //establecemos que el mensaje es html puro
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587; //465 o 587 25
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                string sCuentaMail = "bravo.guerrero.erik@gmail.com";
                string sPasswordMail = "Sistemas410";
                //enviamos el correo electronico con las credenciales adecuadas
                smtpClient.Credentials = new NetworkCredential(sCuentaMail,sPasswordMail);
                smtpClient.Send(correo);
                respuesta = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                
            }
            return respuesta;
        }
    }
}
