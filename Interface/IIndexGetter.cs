namespace MarcusJ
{
    public interface IIndexGetter
    {
        int GetColumnIndex(int gridIndex);
        int GetRowIndex(int gridIndex);
        int GetSquareIndex(int gridIndex);
        int GetIndexByRowColumn(int rowIndex, int columnIndex);
        int GetIndexBySquarePosition(int squareIndex, int position);
    }
}