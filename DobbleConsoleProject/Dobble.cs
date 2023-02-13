using System;
using System.Collections.Generic;

namespace DobbleConsoleProject
{
    // This project is now a class library (program.cs no longer required)
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
        }

        public List<Card> CreateShuffledDeck()
        {
            Random random = new Random();
            List<Card> deck = CreateDeck();

            /// https://forum.unity.com/threads/randomize-array-in-c.86871/
            // Knuth shuffle algorithm
            for (int t = 0; t < deck.Count; t++)
            {
                Card tmp = deck[t];
                int r = random.Next(t, deck.Count - 1);
                deck[t] = deck[r];
                deck[r] = tmp;
            }

            return deck;
        }

        public List<Card> CreateDeck()
        {
            CreateCards(n, Cards, ref overallImageIndex, vanishingPoints);

            List<Card> returnDeck = new List<Card>();

            // convert n x n grid into list of cards
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    returnDeck.Add(Cards[x,y]);
                }
            }

            CreateUniqueSymbolForVanishingPoints(vanishingPoints, overallImageIndex);

            // add vanishing point to final list of cards
            returnDeck.AddRange(vanishingPoints);
            DispCards(n, Cards,vanishingPoints);

            return returnDeck;
        }

        void CreateUniqueSymbolForVanishingPoints(List<Card> vanishingPoints, int overallImageIndex)
        {
            // overallImageIndex has been incremented and not been used
            foreach(Card c in vanishingPoints)
            {
                c.imageIndexs.Add(overallImageIndex);
            }
        }

        void InitialiseCardList(int _n, Card[,] _cards)
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

        void CreateCards(int _n, Card[,] _cards, ref int overallImageIndex, List<Card> vanishingPoints)
        {
            InitialiseCardList(_n, _cards);

            /// Horizontal line
            ApplyVector(_n, _cards, new Vector(1, 0), ref overallImageIndex, false, vanishingPoints);
            /// Vertical line
            ApplyVector(_n, _cards, new Vector(0, 1), ref overallImageIndex, true, vanishingPoints);

            /// All Diagonal lines
            for (int noDiag = 1; noDiag < _n; noDiag++)
            {
                ApplyVector(_n, _cards, new Vector(noDiag, 1), ref overallImageIndex, false, vanishingPoints);
            }
        }

        int Modn(int i, int n)
        {
            int returnVal = i % (n);
            return returnVal;
        }

        void ApplyVector(int n, Card[,] cards, Vector v, ref int overallImageIndex, bool vertical, List<Card> vanishingPoints)
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

                    cards[Modn(startXCoord + i * v.x + (vertical ? curImgIndex : 0), n),
                          Modn(startYCoord + i * v.y + (vertical ? 0 : curImgIndex), n)].imageIndexs.Add(overallImageIndex);
                }
                imageIndexsForVanishingPoint.Add(overallImageIndex);
                overallImageIndex++;
            }

            /// Creating vanishing point
            Card vanishingPoint = new Card(imageIndexsForVanishingPoint);
            vanishingPoints.Add(vanishingPoint);

        }

        void DispCards(int _n, Card[,] _cards, List<Card> _vanishingPoints)
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
                        int i = 0;
                        foreach (int cardIndex in inpCards[column, row].imageIndexs)
                        {
                            Console.Write(((i == 0) ? "" : ",") +
                                            (cardIndex < 10 ? "0" : "") +
                                           cardIndex.ToString());
                            i++;
                        }
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
