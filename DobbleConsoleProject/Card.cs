using System;
using System.Collections.Generic;
using System.Text;

namespace DobbleConsoleProject
{
    public class Card
    {
        /// Hold images in an array or List, these are the indexs for that array
        public List<int> imageIndexs; // = new List<int>();
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
                          // may not need this field, need to combine grid and vanishing points list
                          // to create a final list of cards, probably with a 'Deck' class
        }
    }
}
