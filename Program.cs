using System;
using System.Text;

namespace Blackjack
{
    class Programm
    {
        static void Main(string[] args)
        {
            double CurrentBalance = GetMoney("start", 0, 0, "");
            int[] PlayDeck = GetPlayDeck();
            int[] DeckShuffled = ShuffleDeck(PlayDeck);

            double CurrentBet = GetBet(CurrentBalance);
            CurrentBalance = GetMoney("Other", CurrentBalance, CurrentBet, "-");
            
            int[] PlayerHand = new int[20];
            for (int i = 0; i < 2; i++)
            {
                PlayerHand[i] = DrawCard(DeckShuffled);
                DeckShuffled = EmptyTop(DeckShuffled);
            }
            int[] DealerHand = new int[20];
            for (int i = 0; i < 2; i++)
            {
                DealerHand[i] = DrawCard(DeckShuffled);
                DeckShuffled = EmptyTop(DeckShuffled);
            }
            int PlayerChoice = PlayerTurn(PlayerHand, DealerHand, DeckShuffled);
        }

        
        //Get Array of 6 or 8 Playdecks
        static int[] GetPlayDeck()
        {
            // Register Small Playdeck
            int[] PlayDeckSmall = {
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11
                };
            // register Big Playdeck
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
            while (repeat)      //Check if amount if valid
            {
                Console.WriteLine("Möchtest du mit 6 oder mit 8 Decks spielen?");
                bool IsNumberOfDecksNumeric = int.TryParse(Console.ReadLine(), out NumberOfDecks);      //Readline
                if (IsNumberOfDecksNumeric && NumberOfDecks == 6 || NumberOfDecks == 8)         //Is it valid?
                {
                    repeat = false;
                }
                else        //Error message
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingange");
                    repeat = true;
                }

            }
            if (NumberOfDecks == 6)         //Return Small Playdeck
            {
                Console.Clear();
                return PlayDeckSmall;
            }
            else        //Return Big Playdeck
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

        static int DrawCard(int[] Deck)
        {
            int i = 0;
            while (Deck[i] == 0)
            {
                i++;
            }
            int Card = Deck[i];
            return Card;
        }

        static int[] EmptyTop(int[] Deck)
        {
            int i = 0;
            while (Deck[i] == 0)
            {
                i++;
            }
            Deck[i] = 0;
            return Deck;
        }

        

        //Get starting Money, adds or substracts a specific amount of money
        static double GetMoney(string MoneyType, double Money, double Amount, string MathType)
        {
            if (MoneyType == "start")       //Get starting Money
            {
                double StartingMoney = 0;
                bool repeat = true;
                Console.Clear();
                while (repeat)      //Loop for checking if amount is valid
                {
                    Console.WriteLine("Mit wie viel Startgeld willst du starten? ");
                    bool IsStartingMoneyNumeric = double.TryParse(Console.ReadLine(), out StartingMoney);
                    if (IsStartingMoneyNumeric)         //Success message
                    {
                        repeat = false;
                    }
                    else        //Error message for an invalid amount
                    {
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe");
                        repeat = true;
                    }
                }


                return StartingMoney;
            }
            else        //Adding or Substracting amount of currentBalance
            {
                if (MathType == "-")        //Subtracting
                {
                    Money = Money - Amount;
                }
                else        //Adding
                {
                    Money = Money - Amount;
                }
                return Money;
            }
        }

        

        //Get how much money the players betts
        static double GetBet(double CurrentBalance)
        {

            bool repeat = true;
            double Bet = 0;
            Console.Clear();
            while (repeat)          //Loop to ensure a valid amount of money
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Kontostand: {CurrentBalance}€");
                Console.WriteLine("Wie viel willst du setzen?");

                //Check if amount is valid
                bool IsBetNumeric = double.TryParse(Console.ReadLine(), out Bet);
                if (IsBetNumeric)
                {
                    if (CurrentBalance >= Bet)          //Success message
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Du hast erfolgreich {Bet}€ gesetzt");
                        repeat = false;
                    }
                    else        //Error message if player doesnt have enough money
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine("Du darfst nur so viel setzen, wie du auch hast!");
                        Console.WriteLine($"Aktuell hasst du {CurrentBalance}€");
                    }
                }
                else        // Error message if its not a valid amount
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                    repeat = true;
                }
            }
            return Bet;
        }

        
         static int PlayerTurn(int[] PlayerHand, int[] DealerHand, int[] DeckShuffled)
         {
             Console.WriteLine($"Du hast {PlayerHand[0]} und {PlayerHand[1]}. Eine Karte des Dealers ist {DealerHand[0]}. Was möchtest du machen?" +
                $"\n1: Hit \n2: Double \n3: Stand");
             bool Repeat = true;
             int PlayerAction = 0;
             while (Repeat == true)
             {
                 PlayerAction = Convert.ToInt32(Console.ReadLine());
                 bool IsActionNumeric = int.TryParse(Console.ReadLine(), out PlayerAction);
                 if(IsActionNumeric && PlayerAction == 1 || PlayerAction == 2 || PlayerAction == 3)
                 {
                     Repeat = false;
                 }
                 else { Console.Clear(); Console.WriteLine("Falsche Eingabe"); }
             }
             return PlayerAction;
         }



    }
}
