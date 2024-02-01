using System;
using System.Text;

namespace Blackjack
{
    class Programm
    {
        static void Main(string[] args)
        {
            double CurrentBalance = GetMoney("start", 0, 0);
            int[] PlayDeck = GetPlayDeck();
            int[] DeckShuffled = ShuffleDeck(PlayDeck);

            double CurrentBet = GetBet(CurrentBalance);
            CurrentBalance = GetMoney("-", CurrentBalance, CurrentBet);

            int[] PlayerHand = new int[22];
            for (int i = 0; i < 2; i++)
            {
                PlayerHand[i] = DrawCard(DeckShuffled);
                DeckShuffled = EmptyTop(DeckShuffled);
            }
            int[] DealerHand = new int[22];
            for (int i = 0; i < 2; i++)
            {
                DealerHand[i] = DrawCard(DeckShuffled);
                DeckShuffled = EmptyTop(DeckShuffled);
            }
            bool MakeChoice = true;
            string surrender = Surrender(DealerHand, PlayerHand, CurrentBet);
            if (surrender == "Surrender")
            {
                CurrentBalance = GetMoney(surrender, CurrentBalance, CurrentBet);
                MakeChoice = false;
            }

            bool Bust = false;
            int PlayerCard = 2;
            while (MakeChoice == true && Bust == false)
            {
                int PlayerChoice = PlayerTurn(PlayerHand, DealerHand, DeckShuffled, CurrentBalance, CurrentBet);
                Console.Clear();
                switch (PlayerChoice)
                {
                    case 1:
                        {
                            PlayerHand[PlayerCard] = DrawCard(DeckShuffled);
                            DeckShuffled = EmptyTop(DeckShuffled);
                            Bust = CheckBust(PlayerHand);
                            MakeChoice = true;
                            PlayerCard++;
                            break;
                        }
                    case 2:
                        {
                            bool RepeatDouble = true;
                            while (RepeatDouble)
                            {
                                double CanDouble = GetMoney("check", CurrentBalance, CurrentBet * 2);
                                if (CanDouble == 2)
                                {
                                    CurrentBalance = GetMoney("-", CurrentBalance, CurrentBet);
                                    CurrentBet = CurrentBet * 2;
                                    RepeatDouble = false;
                                    PlayerHand[PlayerCard] = DrawCard(DeckShuffled);
                                    DeckShuffled = EmptyTop(DeckShuffled);
                                    Bust = CheckBust(PlayerHand);
                                    MakeChoice = false;
                                }
                                else
                                {
                                    Console.WriteLine("Nicht genügend Geld, Betrüger!");
                                }
                            }
                            MakeChoice = false;
                            break;
                        }
                    case 3:
                        {
                            MakeChoice = false;
                            break;
                        }
                }

                if (Bust == true)
                {
                    Console.WriteLine("Bust!");
                }
                else
                {
                    int PlayerFinal = FinalScore(PlayerHand);
                    int DealerFinal = FinalScore(DealerHand);
                    int DealerCard = 2;
                    while (DealerFinal < 17)
                    {
                        DealerHand[DealerCard] = DrawCard(DeckShuffled);
                        DeckShuffled = EmptyTop(DeckShuffled);
                        DealerFinal = FinalScore(DealerHand);
                    }
                    if (DealerFinal > 21)
                        Console.WriteLine("Der Dealer geht Bust! Du gewinnst!");
                    else if (PlayerFinal > DealerFinal)
                    {
                        Console.WriteLine($"Du hast {PlayerFinal}, der Dealer hat {DealerFinal}. Du gewinnst!");
                    }
                    else
                    {
                        Console.WriteLine($"Du hast {PlayerFinal}, der Dealer hat {DealerFinal}. Du verlierst!");
                    }
                }
            }
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
        static double GetMoney(string MoneyType, double Money, double Amount)
        {
            switch (MoneyType)
            {
                case "start":
                    double StartingMoney = 0;
                    bool repeat = true;
                    Console.Clear();
                    while (repeat)      //Loop for checking if amount is valid
                    {
                        Console.WriteLine("Mit wie viel Startgeld willst du starten? ");
                        bool IsStartingMoneyNumeric = double.TryParse(Console.ReadLine(), out StartingMoney);
                        if (IsStartingMoneyNumeric && StartingMoney > 0)         //Success message
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
                case "check":
                    double CheckType = 0;
                    if (Amount > Money)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                case "Surrender":
                    Money = Money - Amount / 2;
                    return Money;
                case "-":
                    Money = Money - Amount;
                    return Money;
                case "+":
                    Money = Money + Amount;
                    return Money;
                default:
                    return 0;
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


        static int PlayerTurn(int[] PlayerHand, int[] DealerHand, int[] DeckShuffled, double CurrentBalance, double CurrentBet)
        {
            int HandSize = 0;
            for (int i = 0; i < 20; i++)
            {
                if (PlayerHand[i] != 0)
                {
                    HandSize++;
                }
            }
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Kontostand: {CurrentBalance}€, Gesetzt: {CurrentBet}€");

                        int pHC = 0;
            Console.Write("Du hast ");
            while (PlayerHand[pHC] != 0)
            {
                if (PlayerHand[pHC] != 0)
                {
                    pHC++;
                    Console.Write(PlayerHand[pHC - 1]);
                    if (PlayerHand[pHC] != 0 && PlayerHand[pHC + 1] != 0)
                    {
                        Console.Write(", ");
                    }
                    else if (PlayerHand[pHC + 1] == 0 && PlayerHand[pHC] != 0)
                    {
                        Console.Write(" und ");
                    }
                }
            
            }
            Console.WriteLine("");
            
            Console.WriteLine($"Eine Karte des Dealers ist {DealerHand[0]}. Was möchtest du machen?" +
               $"\n1: Hit" +
               $" \n2: Double" +
               $" \n3: Stand");
            bool Repeat = true;
            int PlayerAction = 0;
            while (Repeat)
            {
                bool IsStartingMoneyNumeric = int.TryParse(Console.ReadLine(), out PlayerAction);
                if (IsStartingMoneyNumeric && PlayerAction == 1 || PlayerAction == 2 || PlayerAction == 3)
                {
                    Repeat = false;
                }
            }
            return PlayerAction;
        }


        static bool CheckBust(int[] Hand)
        {
            int HandTotal = 0;
            bool Bust = false;
            for (int i = 0; i < 22; i++)
            {
                if (Hand[i] == 11)
                {
                    Hand[i] = 1;
                }
                HandTotal = Hand[i] + HandTotal;
            }
            if (HandTotal > 21)
            {
                Bust = true;
            }
            else
            {
                Bust = false;
            }
            return Bust;
        }

        static int[] Discard(int[] dealerDisc, int[] playerDisc, int[] discardPile)
        {
            //requires dealer Hand and Player Hand to discard (requires initialized discard Pile according to length of Deck of size 8) : int[] discardPile = new int[416]; for 8 deck
            dealerDisc[0] = 5; dealerDisc[1] = 7; dealerDisc[2] = 0;

            int dealerLength = 0, playerLength = 0, dhcount = 0, phcount = 0;
            int discardLength = discardPile.Length;
            while (dealerDisc[dealerLength] != 0 && dealerLength <= dealerDisc.Length)
            {
                dealerLength++;
            }

            while (playerDisc[playerLength] != 0 && playerLength <= playerDisc.Length)
            {
                playerLength++;
            }
            for (int i = 0; i < discardPile.Length && phcount < playerLength && dhcount < dealerLength;)
            {

                if (discardPile[i] == 0)
                {

                    if (dealerDisc[dhcount] != 0)
                    {
                        discardPile[i] = dealerDisc[dhcount];

                        dhcount++;
                        i++;
                        //adds dealer card to discard
                    }
                    if (playerDisc[phcount] != 0)
                    {
                        discardPile[i] = playerDisc[phcount];
                        phcount++;
                        i++;
                        //adds player card to discard
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return discardPile;
        }

        static string Surrender(int[] dealerHand, int[] playerHand, double currentBet)
        {
            int surrYesOrNo = 0; string returnval = "null";
            bool Repeat = false;
            while (!Repeat && surrYesOrNo != 1 || !Repeat && surrYesOrNo != 2)
            {
                Console.WriteLine($"Du hast {playerHand[0]} und {playerHand[1]}. Eine Karte des Dealers ist {dealerHand[0]}. Dein Einsatz ist {currentBet}€. Möchtest du Aufgeben? (Du verlierst nur die Hälfte deines Einsatzes)" +
                               $"\n1: Ja \n2: Nein");
                Repeat = int.TryParse(Console.ReadLine(), out surrYesOrNo);
                Console.Clear();
            }
            if (surrYesOrNo == 1)
            {
                return "Surrender";
            }
            else
            {
                return "JulianDerHS";
            }
        }


        static int FinalScore(int[] Hand)
        {
            int HandTotal = 0;
            for (int i = 0; i < 22; i++)
            {
                HandTotal = Hand[i] + HandTotal;
            }
            if (HandTotal > 21)
            {
                for (int i = 0; i < 22; i++)
                {
                    if (Hand[i] == 11)
                    {
                        Hand[i] = 1;
                    }
                    HandTotal = Hand[i] + HandTotal;
                }
            }
            return HandTotal;
        }






    }
}
