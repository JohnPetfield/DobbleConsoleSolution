using System;
using System.Collections.Generic;

namespace DobbleConsoleProject
{

    /// <summary>
    /// 
    /// Dobble has n = 7 grid
    /// 8 symbols on a card
    /// 7 * 7 = 49 cards
    /// there are n + 1 vanishing points (8),
    /// so total Cards possible = 49 + 8 = 57
    /// Dobble comes with 55 cards, 
    /// 
    /// meaning here are 2 possible cards to find
    /// and create to add to Dobble
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int overallImageIndex = 0;
            int n = 7;
            //Console.WriteLine( 4 % ( n - 1));

            Card[,] Cards          = new Card[n, n];

            List<Card> vanishingPoints = new List<Card>();

            initialiseCardList(n, Cards);
            createCards(n, Cards, ref overallImageIndex , vanishingPoints);
            dispCards(n, Cards , vanishingPoints);
        }

        static void initialiseCardList(int _n, Card[,] _cards)
        {
            int n = _n;
            Card[,] inpCards = _cards;
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    Card c = new Card(x, y);
                    inpCards[x, y] = c;
                }
            }
        }

        static void createCards(int _n, Card[,] _cards,  ref int overallImageIndex , List<Card> vanishingPoints)
        {
            /*
            /// Main Diagonal line (1 down, 1 across)
            //applyVector(_n, _cards, new Vector(1, 1), ref overallImageIndex);

            ///      Diagonal line (1 down, 2 across)
            //applyVector(_n, _cards, new Vector(2, 1), ref overallImageIndex);

            ///      Diagonal line (1 down, 3 across)
            //applyVector(_n, _cards, new Vector(3, 1), ref overallImageIndex);

            ///      Diagonal line (1 down, 4 across)
            //applyVector(_n, _cards, new Vector(4, 1), ref overallImageIndex);
            */


            /// Horizontal line
            applyVector( _n, _cards, new Vector(1,0), ref overallImageIndex, false , vanishingPoints);
            /// Vertical line
            applyVector(_n, _cards, new Vector(0, 1), ref overallImageIndex, true , vanishingPoints);
            
            /// All Diagonal lines
            for(int noDiag = 1; noDiag < _n; noDiag++)
            {
                applyVector(_n, _cards, new Vector(noDiag, 1), ref overallImageIndex, false , vanishingPoints);
            }
        }

        static int mymodn(int i, int n)
        {
            int returnVal = i % (n);
            return returnVal;
        }

        static void applyVector(int n, Card[,] cards, Vector v, ref int overallImageIndex, bool vertical , List<Card> vanishingPoints)
        {
            int startXCoord = 0;
            int startYCoord = 0;

            List<int> imageIndexsForVanishingPoint = new List<int>();

            /// change images for each row, i.e. draw the line move the line along and redraw
            for(int curImgIndex = 0; curImgIndex < n; curImgIndex ++)
            { 
                /// Apply this single image to for each card in the row
                for (int i = 0; i < n; i++)
                {
                    //Console.WriteLine("(" + (startXCoord + i * v.x) + "," + (startYCoord + i * v.y + curImgIndex) + ")");

                    cards[mymodn(startXCoord + i * v.x + (vertical ? curImgIndex: 0)          , n),
                          mymodn(startYCoord + i * v.y + (vertical ? 0          : curImgIndex), n)].imageIndexs.Add(overallImageIndex);
                }
                imageIndexsForVanishingPoint.Add(overallImageIndex);
                overallImageIndex++;
            }
            
            /// Creating vanishing point
            Card vanishingPoint = new Card(imageIndexsForVanishingPoint);
            vanishingPoints.Add(vanishingPoint);
            
        }
        static void dispCards(int _n, Card[,] _cards , List<Card> _vanishingPoints)
        {
            int n = _n;
            Card[,] inpCards = _cards;
            List<Card> inpVanishingPoints = _vanishingPoints;

            Console.WriteLine("==========");
            Console.WriteLine("n x n grid");
            Console.WriteLine("==========");

            /// Display n x n grid
            for (int row = 0; row < n; row++)
            {
                Console.Write(" | ");
                for (int column= 0; column < n; column++)
                {
                    if (inpCards[column, row].imageIndexs.Count > 0)
                    {
                        //Console.Write(inpCards[row, column].imageIndexs);
                        int i = 0;
                        foreach (int cardIndex in inpCards[column, row].imageIndexs)
                        {
                            Console.Write(((i == 0) ? "" : ",") +
                                            (cardIndex < 10 ? "0" : "") +
                                           cardIndex.ToString());
                            i++;
                        }
                    }
                    //Console.Write("B");

                    // Anything not coded for yet, print a 'B'
                    for(int j = 0; j < n - inpCards[column, row].imageIndexs.Count; j++)
                    {
                        Console.Write("B");
                    }
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("==========");
            Console.WriteLine("vanishing points");
            Console.WriteLine("==========");
            
            /// Display the vanishing points (not in n x n grid)
            foreach(Card c in _vanishingPoints)
            {
                int i = 0;
                foreach(int imgIdx in c.imageIndexs)
                {
                    Console.Write(((i == 0) ? "" : ",") + 
                                  (imgIdx < 10 ? "0" : "") + 
                                   imgIdx.ToString()
                                   );
                    i++;
                }
                Console.WriteLine();
            }
        }
    }
}
