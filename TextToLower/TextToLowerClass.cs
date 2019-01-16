using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace TextToLower
{
    public class TextToLowerClass
    {
        public class TextToLowerPlugin : IPlugin
        {
            public string Activate(string input)
            {
                //переводим прописные буквы в строчные    
                return input.ToLower();
            }
        }
    }
}
