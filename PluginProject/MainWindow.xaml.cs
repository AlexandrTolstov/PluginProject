using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins"); //Создаем строку из текущей директории и имяни папки (Plugins)
        private List<IPlugin> plugins = new List<IPlugin>();
        public MainWindow()
        {
            InitializeComponent();

            CreateMenuPlugins(); //Создаем меню со списком Плагинов

            CoWorker db = new CoWorker();

            //получаем объекты из бд и выводим textBox
            var CoWork = db.Co_Workers.ToList();

            foreach (var Co in CoWork)
            {
                txtPhrase.Text += Co.C_Name + "\n";
            }
        }

        private void BtnRefineText_Click(object sender, RoutedEventArgs e)
        {
            string result = txtPhrase.Text; //берем содержимое текстбокса

            //TODO: пройти по списку плагинов и дать каждому
            //обработать текст, сохрагяя результат в result
            foreach (var plugin in plugins)
                result = plugin.Activate(result);

            txtPhrase.Text = result;
        }

        private void CreateMenuPlugins()
        {
            /*Создание директории и считывание вайла плагина*/
            plugins.Clear();

            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);

            if (!pluginDirectory.Exists)
                pluginDirectory.Create(); //Создать  директорию по заданному пути

            string[] pluginFiles = Directory.GetFiles(pluginPath, "*.dll");

            //Запоминаем имена файлов
            string[] pluginNames = new string[pluginFiles.Length];
            int i = 0;
            foreach (var item in pluginFiles)
            {
                pluginNames[i] = System.IO.Path.GetFileName(item);
                i++;
            }
            i = 0;

            foreach (var file in pluginFiles)
            {
                //Загружаем сборку
                Assembly asm = Assembly.LoadFrom(file);
                //ищем типы, имплементирующие наш интерфейс IPlugin,
                //чтобы не захватить лишнего
                var types = asm.GetTypes().Where(t => t.GetInterfaces().
                                            Where(z => z.FullName == typeof(IPlugin).FullName).Any());
                //заполняем экземплярами полученных типов коллекцию плагинов
                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IPlugin;
                    plugins.Add(plugin);
                }
            }
            /*Создание директории и считывание вайла плагина*/

            MenuItem[] menuItem = new MenuItem[plugins.Count];

            i = 0;
            foreach (var plugin in plugins)
            {
                menuItem[i] = new MenuItem { Header = pluginNames[i], IsCheckable = true, IsChecked = true };
                MenuPlugins.Items.Add(menuItem[i]);
                i++;
            }
            i = 0;
        }
    }
}
