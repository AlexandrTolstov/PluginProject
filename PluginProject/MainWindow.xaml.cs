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
using SearchPluginInterface;
using DBConnect;

namespace PluginProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins"); //Создаем строку из текущей директории и имяни папки (Plugins)
        private List<SearchInterface> searchPlugins = new List<SearchInterface>();
        public MainWindow()
        {
            InitializeComponent();

            CreateMenuPlugins(); //Создаем меню со списком Плагинов

            try
            {
                WorkersSet db = new WorkersSet();
                CoWorkerGrid.ItemsSource = db.Co_Workers; //Связываем таблицу с БД
            }
            catch (Exception ex)
            {
                txtPhrase.Text += "\n" + ex.Message;
            }
        }

        private void BtnRefineText_Click(object sender, RoutedEventArgs e)
        {

            /*-----------------------------------------------*/

            string result = txtPhrase.Text; //берем содержимое текстбокса

            //TODO: пройти по списку плагинов и дать каждому
            //обработать текст, сохраняя результат в worker
            Worker worker = new Worker();
            foreach (var plugin in searchPlugins)
                worker = plugin.Search(result);

            ID_TextBox.Text = worker.ID.ToString();
            Name_TextBox.Text = worker.C_Name;
            Surname_TextBox.Text = worker.C_Surname;
            Date_of_Birth_TextBox.Text = worker.C_Date_of_Birth.ToString();
            email_TextBox.Text = worker.C_email;
            adress_TextBox.Text = worker.C_adress;
        }

        private void CreateMenuPlugins()
        {
            /*------------------------------------------------------*/

            /*Создание директории и считывание вайла плагина*/
            searchPlugins.Clear();

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
                                            Where(z => z.FullName == typeof(SearchInterface).FullName).Any());
                //заполняем экземплярами полученных типов коллекцию плагинов
                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as SearchInterface;
                    searchPlugins.Add(plugin);
                }
            }
            /*Создание директории и считывание вайла плагина*/

            //Создаем подпункты меню с названиями файлов плагинов
            MenuItem[] menuItem = new MenuItem[searchPlugins.Count];
            i = 0;
            foreach (var plugin in searchPlugins)
            {
                menuItem[i] = new MenuItem { Header = pluginNames[i], IsCheckable = true, IsChecked = true };
                MenuPlugins.Items.Add(menuItem[i]);
                i++;
            }
            i = 0;
        }
    }
}
