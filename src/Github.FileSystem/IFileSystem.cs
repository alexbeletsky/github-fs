namespace Github
{
    public interface IFileSystem
    {
        bool FileExists(string filename);
        bool DirectoryExists(string directory);
        Stat Stat(string filename);
        Contents OpenPath(string path);
    }
}