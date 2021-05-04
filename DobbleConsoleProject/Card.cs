using System;
using System.Collections.Generic;
using System.Text;

namespace DobbleConsoleProject
{
    class Card
    {
        /// Hold images in an array or List, these are the indexs for that array
        public List<int> imageIndexs; // = new List<int>();
        int x, y;

        public Card(int _x, int _y)
        {
            x = _x;
            y = _y;
            imageIndexs =  new List<int>();
        }

        public Card(List<int> _imageIndexs)
        {
            imageIndexs = _imageIndexs;
        }
    }
}
