using System;
namespace Blackjack
{ 
    class Programm
    {
        static void Main(string[] args)
        {
            int[] PlayDeck = GetPlayDeck();            
        }

        static int[] GetPlayDeck()
        {
            int[] PlayDeckSmall = {
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11
                };
            int[] PlayDeckBig = {
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                        2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11
                };
            bool repeat = true;
            int NumberOfDecks = 6;
            Console.Clear();
            while (repeat)
            {
                Console.WriteLine("Möchtest du mit 6 oder mit 8 Decks spielen?");
                bool IsNumberOfDecksNumeric = int.TryParse(Console.ReadLine(), out NumberOfDecks);
                if (IsNumberOfDecksNumeric && NumberOfDecks == 6 || NumberOfDecks == 8)
                {
                    repeat = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingange");
                    repeat = true;
                }

            }
            if (NumberOfDecks == 6)
            {
                Console.Clear();
                return PlayDeckSmall;
            }
            else
            {
                Console.Clear();
                return PlayDeckBig;
            }
            
            
            
        }

        
        static int[] ShuffleDeck(int[] deckToShuffle)
        {
            Random ranNum = new Random();
            int randomNum, temp;
            
            //needs the deck to shuffle (can be unlimited number of cards)
            for (int i = deckToShuffle.Length - 1; i > 0; --i)
            {
                //new random for non shuffeled item
                randomNum = ranNum.Next(i + 1);
        
                //swap the random item with last item in the array
                temp = deckToShuffle[i];
                deckToShuffle[i] = deckToShuffle[randomNum];
                deckToShuffle[randomNum] = temp;
            }
            return deckToShuffle;  
        }


        static double GetStartingMoney()
        {
            double StartingMoney = 0;
            bool repeat = true;
            Console.Clear();
            while (repeat)
            {
                Console.WriteLine("Mit wie viel Startgeld willst du starten? ");
                bool IsStartingMoneyNumeric = double.TryParse(Console.ReadLine(), out StartingMoney);
                if (IsStartingMoneyNumeric)
                {
                    repeat = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                    repeat = true;
                }
            }


            return StartingMoney;
        }


        static double GetBet(double CurrentBalance)
        {
            bool repeat = true;
            double Bet = 0;
            Console.Clear();
            while (repeat)
            { 
                Console.WriteLine("Wie viel willst du setzen?");

                bool IsBetNumeric = double.TryParse(Console.ReadLine(), out Bet);
                if (IsBetNumeric)
                {
                    if (CurrentBalance >= Bet)
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Du hasst erfolgreich {Bet}€ gesetzt");
                        repeat = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine("Du darfst nur so viel setzen, wie du auch hasst!");
                        Console.WriteLine($"Aktuell hasst du {CurrentBalance}€");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                    repeat = true;
                }
            }
            return Bet;
        }


        
    }
}


