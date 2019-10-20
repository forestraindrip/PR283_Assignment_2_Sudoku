namespace MarcusJ
{
    public interface ICompleteChecker
    {
        bool IsColumnCompleted(int columnIndex);
        bool IsRowCompleted(int rowIndex);
        bool IsSquareCompleted(int squareIndex);
    }
}