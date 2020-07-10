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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>

    [Serializable]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Service { get; set; }


        public User(string login, string password, string service)
        {
            Login = login;
            Password = password;
            Service = service;

        }
    }
    public partial class UserControl1 : UserControl
    {

        User user;
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "users.dat");
        public UserControl1()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    user = (User)formatter.Deserialize(fs);
                }
            }
            
            InitializeComponent();
            if (user != null)
            {
                LoginBox.Text = user.Login;
                PassBox.Password = user.Password;
                Service.Text = user.Service;
            }

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
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
                MessageBox.Show("сообщение отправлено!");
            }
            catch
            {
                MessageBox.Show("всё сломався!");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
                user = (new User(LoginBox.Text,
                    PassBox.Password, Service.Text));


                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, user);
                }
            
        }
    }
}
