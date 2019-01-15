using System;
using System.Collections.Generic;
using System.IO;
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
using PluginInterface;

namespace PluginProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private List<IPlugin> plugins = new List<IPlugin>();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void BtnRefineText_Click(object sender, RoutedEventArgs e)
        {
            string result = txtPhrase.Text; //берем содержимое текстбокса

            //TODO: пройти по списку плагинов и дать каждому
            //обработать текст, сохрагяя результат в result

            txtPhrase.Text = result;
        }
    }
}
