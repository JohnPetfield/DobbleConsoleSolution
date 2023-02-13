using System.Collections.Generic;

namespace DobbleConsoleProject
{
    public class Card
    {
        /// Hold images in an array or List, these are the indexs for that array
        public List<int> imageIndexs;
        int x, y;
        bool grid;

        public Card(int _x, int _y)
        {
            x = _x;
            y = _y;
            imageIndexs =  new List<int>();
            grid = true;
        }

        public Card(List<int> _imageIndexs)
        {
            imageIndexs = _imageIndexs;
            grid = false; // this constructor is called only by vanishing points (therefore not in n x n grid)
        }
    }
}
