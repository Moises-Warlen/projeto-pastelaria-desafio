using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
public class EmailService
{
    // Método responsável por enviar um e-mail
    public void EnviarEmail(string para, string assunto, string corpo)
    {
        // Cria uma instância do cliente SMTP
        SmtpClient cliente = new SmtpClient();

        // Configura o cliente SMTP com as informações obtidas do arquivo Web.config
        cliente.Host = ConfigurationManager.AppSettings["smtp:Host"]; // Endereço do servidor SMTP
        cliente.Port = int.Parse(ConfigurationManager.AppSettings["smtp:Port"]); // Porta do servidor SMTP
        cliente.Credentials = new NetworkCredential(
            ConfigurationManager.AppSettings["smtp:UserName"], // Nome de usuário para autenticação
            ConfigurationManager.AppSettings["smtp:Password"]  // Senha para autenticação
        );
        cliente.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtp:EnableSsl"]); // Configura se o SSL está ativado

        // Cria e configura a mensagem de e-mail
        MailMessage mensagem = new MailMessage();
        mensagem.From = new MailAddress(ConfigurationManager.AppSettings["smtp:From"]); // Endereço de e-mail do remetente
        mensagem.To.Add(para); // Endereço de e-mail do destinatário
        mensagem.Subject = assunto; // Assunto do e-mail
        mensagem.Body = corpo; // Corpo do e-mail
        mensagem.IsBodyHtml = true; // Define se o corpo do e-mail é em HTML

        try
        {
            // Tenta enviar o e-mail
            cliente.Send(mensagem);
        }
        catch (Exception)
        {
            // Captura a exceção, mas não faz nada. O programa continua sem interrupção.
        }
    }
}
