using System.Net;
using System.Net.Mail;

namespace BolsaTrabajoV1.App_Code
{
    public class CorreoHelper
    {
        private string correo, nombre, asunto, mensaje;

        public string Asunto
        {
            get
            {
                return asunto;
            }

            set
            {
                asunto = value;
            }
        }

        public string Correo
        {
            get
            {
                return correo;
            }

            set
            {
                correo = value;
            }
        }

        public string Mensaje
        {
            get
            {
                return mensaje;
            }

            set
            {
                mensaje = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public void enviarCorreo() {
            var fromAddress = new MailAddress("sistemabolsadetrabajo@gmail.com", "SIBTRA S.A de C.V.");
            var toAddress = new MailAddress(correo, nombre);
            string fromPassword = "sibtra2017";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            MailMessage msg = new MailMessage(fromAddress.ToString(), toAddress.ToString(), asunto, mensaje);

            msg.IsBodyHtml = true;

            smtp.Send(msg);

        }




    }
}