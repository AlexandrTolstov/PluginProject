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

            //обновляем список плагинов
            RefreshPlugins();
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
        private void RefreshPlugins()
        {
            plugins.Clear();

            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);

            if (!pluginDirectory.Exists)
                pluginDirectory.Create(); //Создать  директорию по заданному пути

            string[] pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                //Загружаем сборку
                Assembly asm = Assembly.LoadFrom(file);
                //ищем типы, имплементирующие наш интерфейс IPlugin,
                //чтобы не захватить лишнего
                var types = asm.GetTypes().Where(t => t.GetInterfaces().
                                            Where(i => i.FullName == typeof(IPlugin).FullName).Any());
                //заполняем экземплярами полученных типов коллекцию плагинов
                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IPlugin;
                    plugins.Add(plugin);
                }
            }
        }
    }
}
