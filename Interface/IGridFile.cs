namespace MarcusJ
{
    public interface IGridFile
    {
        string CleanInputString(string csv);
        void LoadCSVFileToGrid(string filePath);
        void SaveGridToCSVFile(string filePath);
    }
}