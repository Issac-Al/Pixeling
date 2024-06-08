using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using UnityEditor;
using System;
using TMPro;
public class Emailer : MonoBehaviour
{
    public TMP_InputField recipientEmail;
    [SerializeField]
    private string mailBody, subject;
    public TMP_InputField username, accountNumber, group;
    public DataManager dataManager;
    // Start is called before the first frame update
    public void SendEmail()
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("smtp-mail.outlook.com");
        //smtpServer.EnableSsl = true;
        smtpServer.Timeout = 10000;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.UseDefaultCredentials = false;
        smtpServer.Port = 587;

        mail.From = new MailAddress("pixeling_ad@outlook.com");
        mail.To.Add(new MailAddress(recipientEmail.text));

        mail.Subject = subject;
        mail.Body = mailBody;

        smtpServer.Credentials = new System.Net.NetworkCredential("pixeling_ad@outlook.com","4m4t!st4987") as ICredentialsByHost; smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        smtpServer.Send(mail);
    }

    public void SendEmail(string recipientEmail)
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("smtp-mail.outlook.com");
        //smtpServer.EnableSsl = true;
        smtpServer.Timeout = 10000;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.UseDefaultCredentials = false;
        smtpServer.Port = 587;

        mail.From = new MailAddress("pixeling_ad@outlook.com");
        mail.To.Add(new MailAddress(recipientEmail));

        mail.Subject = subject;
        mail.Body = mailBody;

        smtpServer.Credentials = new System.Net.NetworkCredential("pixeling_ad@outlook.com", "4m4t!st4987") as ICredentialsByHost; smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        smtpServer.Send(mail);
    }


    public void SetMailBody(string body)
    {
        mailBody = body;
    }

    public void MailBodyStartGame()
    {
        subject = "Inicio de partida de: " + username.text + " " + accountNumber.text + " del grupo " + group.text;
        mailBody = "Se ha iniciado una nueva partida el: " + System.DateTime.Now.ToString();
        dataManager.playerDataSO.professorEmail = recipientEmail.text;
        dataManager.playerDataSO.name = username.text;
        dataManager.playerDataSO.accountNumber = accountNumber.text;
        dataManager.playerDataSO.group = group.text;
        dataManager.SaveData();
    }

    public void PlayerProgress(string zone, string name, string accountNumber, string score)
    {
        subject = "El alumno: " + name + " " + accountNumber + " termino el desafío " + zone;
        mailBody = "Se termino el desafío el: " + System.DateTime.Now.ToString() + "\n" + " con una calificación de: " + score;
    }

    public void PrintInputFieldValues()
    {
        Debug.Log(recipientEmail.text);
        Debug.Log(username.text);
        Debug.Log(accountNumber.text);
        Debug.Log(group.text);
    }

    public void SendText(string phoneNumber)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;

        mail.From = new MailAddress("myEmail@gmail.com");

        mail.To.Add(new MailAddress(phoneNumber + "@txt.att.net"));//See carrier destinations below
                                                                   //message.To.Add(new MailAddress("5551234568@txt.att.net"));
        mail.To.Add(new MailAddress(phoneNumber + "@vtext.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@messaging.sprintpcs.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@tmomail.net"));
        mail.To.Add(new MailAddress(phoneNumber + "@vmobl.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@messaging.nextel.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@myboostmobile.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@message.alltel.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@mms.ee.co.uk"));



        mail.Subject = "Subject";
        mail.Body = "";

        SmtpServer.Port = 587;

        SmtpServer.Credentials = new System.Net.NetworkCredential("myEmail@gmail.com", "MyPasswordGoesHere") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }

}

