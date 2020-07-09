using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        string path;
        public UserControl1()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            System.Net.Mail.MailAddress from = new MailAddress(LoginBox.Text, "gg");
            // кому отправляем
            System.Net.Mail.MailAddress to = new MailAddress(To.Text);
            // создаем объект сообщения
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(from, to);
            // тема письма
            m.Subject = Theme.Text;
            // текст письма
            m.Body = Body.Text;
            // письмо представляет код html
            m.IsBodyHtml = false;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(Service.Text, 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(LoginBox.Text, PassBox.Password);

            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
