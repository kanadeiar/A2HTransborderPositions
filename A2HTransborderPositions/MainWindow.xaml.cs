using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Sharp7;

namespace A2HTransborderPositions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //Чтение
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            S7Client client = new S7Client { ConnTimeout = 5_000, RecvTimeout = 5_000 };
            byte[] bufferDb = new byte[4];
            string address = "10.0.57.10";
            var r = client.ConnectTo(address, 0, 2);
            if (r == 0)
            {
                var r0 = client.DBRead(162, 12, 4, bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {client.PLCIpAddress}, ошибка = {r0}");
                TextBoxData.Text = bufferDb.GetDIntAt(0).ToString();
            }
            client.Disconnect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TextBoxData.Text, out int value))
            {
                S7Client client = new S7Client { ConnTimeout = 5_000, RecvTimeout = 5_000 };
                byte[] bufferDb = new byte[4];
                string address = "10.0.57.10";
                var r = client.ConnectTo(address, 0, 2);
                if (r == 0)
                {
                    bufferDb.SetDIntAt(0, value);
                    var r0 = client.DBWrite(162, 12, 4, bufferDb);
                    if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось записать блок данных  в контроллер с адресом {client.PLCIpAddress}, ошибка = {r0}");
                }
                client.Disconnect();
            }
            else
            {
                MessageBox.Show("Не удалось разобрать число из текстового поля");
            }
        }
    }
    

    
}

