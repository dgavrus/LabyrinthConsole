namespace LabirynthConsole
{
    public class Coordinate
    {
        public int j { get; set; }
        public int i { get; set; }
        public Coordinate(int x, int y)
        {
            i = y;
            j = x;
        }
        public void setCoordinate(int x, int y)
        {
            j = x;
            i = y;
        }
    }
}
