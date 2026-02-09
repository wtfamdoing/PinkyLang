class FileReader 
{
    public string Read(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"The file {fileName} doesn't exist", fileName);
        }
        return File.ReadAllText(fileName);
    }
}