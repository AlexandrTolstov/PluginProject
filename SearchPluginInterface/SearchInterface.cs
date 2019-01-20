using DBConnect;

namespace SearchPluginInterface
{
    public interface SearchInterface
    {
        string Name { get; }
        Worker Search(string input);
    }
}
