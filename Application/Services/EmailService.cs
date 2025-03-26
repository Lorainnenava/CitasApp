using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class EmailService : IEmailService
    {

        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }


        // Enviar un correo electrónico con cuerpo dinámico y soporte para imágenes embebidas
        public bool SendEmail(string recipientAddress, string subject, string templatePath, Dictionary<string, string> placeholders, List<(string cid, string path)> images = null)
        {
            // Configuración del servidor SMTP
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string senderEmail = "lorewow23@gmail.com";
            string senderPassword = "tu_contraseña";


            try
            {
                // Generar el cuerpo del correo a partir de la plantilla y los marcadores
                string bodyEmail = GenerateEmailBody(templatePath, placeholders);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", "your@email.com"));
                message.To.Add(new MailboxAddress("", recipientAddress));
                message.Subject = subject;

                // Crear la parte HTML con imágenes embebidas
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = bodyEmail
                };

                // Agregar las imágenes embebidas
                if (images != null)
                {
                    foreach (var (cid, path) in images)
                    {
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            var image = bodyBuilder.LinkedResources.Add(path);
                            image.ContentId = cid; // Usar el CID proporcionado en el parámetro
                        }
                    }
                }

                message.Body = bodyBuilder.ToMessageBody();

                // Configurar el cliente SMTP y enviar el correo
                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(senderEmail, senderPassword);
                    client.Send(message);

                    client.Disconnect(true);
                }

                _logger.LogInformation("Correo enviado exitosamente a {RecipientAddress}.", recipientAddress);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar correo a {RecipientAddress}.", recipientAddress);
                return false;
            }
        }

        // Generar el cuerpo del correo reemplazando marcadores en una plantilla
        public string GenerateEmailBody(string templatePath, Dictionary<string, string> placeholders)
        {
            // Leer la plantilla desde el archivo
            string template = File.ReadAllText(templatePath);

            // Reemplazar marcadores con valores dinámicos
            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return template;
        }
    }
}
