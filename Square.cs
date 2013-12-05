namespace LabirynthConsole
{
    public class Square
    {
        public int walls { get; set; }
        private bool isFinish;
        public Square()
        {
            this.walls = 0;
        }
        public Square(int walls)
        {
            this.walls = walls;
        }

        public void addWall(int wall)
        {
            this.walls = (this.walls + wall < 16) ? this.walls + wall : this.walls;
        }

        public void deleteWall(int wall)
        {
            this.walls = (this.walls - wall >= 0) ? this.walls - wall : this.walls;
        }       
    }
}
