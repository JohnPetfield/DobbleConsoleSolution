using System;
using System.Collections.Generic;
using System.Text;

namespace DobbleConsoleProject
{
    public class Dobble
    {
        public int n;
        int overallImageIndex = 0;

        public Card[,] Cards;
        public List<Card> vanishingPoints = new List<Card>();

        public Dobble(int _n)
        {
            n = _n;
            Cards =  new Card[n, n];
            //createDeck(n);
        }
        public List<Card> createDeck()
        {
            createCards(n, Cards, ref overallImageIndex, vanishingPoints);
            //dispCards(n, Cards, vanishingPoints);

            List<Card> returnDeck = new List<Card>();

            // convert n x n grid into list of cards
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    returnDeck.Add(Cards[x,y]);
                }
            }

            // add vanishing point to final list of cards
            returnDeck.AddRange(vanishingPoints);
            return returnDeck;
        }

        void initialiseCardList(int _n, Card[,] _cards)
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

         void createCards(int _n, Card[,] _cards, ref int overallImageIndex, List<Card> vanishingPoints)
        {
            initialiseCardList(_n, _cards);

            /// Horizontal line
            applyVector(_n, _cards, new Vector(1, 0), ref overallImageIndex, false, vanishingPoints);
            /// Vertical line
            applyVector(_n, _cards, new Vector(0, 1), ref overallImageIndex, true, vanishingPoints);

            /// All Diagonal lines
            for (int noDiag = 1; noDiag < _n; noDiag++)
            {
                applyVector(_n, _cards, new Vector(noDiag, 1), ref overallImageIndex, false, vanishingPoints);
            }
        }

         int mymodn(int i, int n)
        {
            int returnVal = i % (n);
            return returnVal;
        }

         void applyVector(int n, Card[,] cards, Vector v, ref int overallImageIndex, bool vertical, List<Card> vanishingPoints)
        {
            int startXCoord = 0;
            int startYCoord = 0;

            List<int> imageIndexsForVanishingPoint = new List<int>();

            /// change images for each row, i.e. draw the line move the line along and redraw
            for (int curImgIndex = 0; curImgIndex < n; curImgIndex++)
            {
                /// Apply this single image to for each card in the row
                for (int i = 0; i < n; i++)
                {
                    //Console.WriteLine("(" + (startXCoord + i * v.x) + "," + (startYCoord + i * v.y + curImgIndex) + ")");

                    cards[mymodn(startXCoord + i * v.x + (vertical ? curImgIndex : 0), n),
                          mymodn(startYCoord + i * v.y + (vertical ? 0 : curImgIndex), n)].imageIndexs.Add(overallImageIndex);
                }
                imageIndexsForVanishingPoint.Add(overallImageIndex);
                overallImageIndex++;
            }

            /// Creating vanishing point
            Card vanishingPoint = new Card(imageIndexsForVanishingPoint);
            vanishingPoints.Add(vanishingPoint);

        }

         void dispCards(int _n, Card[,] _cards, List<Card> _vanishingPoints)
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
                for (int column = 0; column < n; column++)
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
                    for (int j = 0; j < n - inpCards[column, row].imageIndexs.Count; j++)
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
            foreach (Card c in _vanishingPoints)
            {
                int i = 0;
                foreach (int imgIdx in c.imageIndexs)
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
