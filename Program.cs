using System;
using System.Text;
namespace Blackjack
{
    class Programm
    {
        static void Main(string[] args)
        {
            // Adds a retro look
            Console.ForegroundColor = ConsoleColor.Green;
            //Sets up initial game by getting starting balance and shuffling the decks
            int[] cheatCodes = CheatCodeAsk();
            int[] discardPile = new int[416];
            int[] playDeck = GetPlayDeck();
            int[] deckShuffled = ShuffleDeck(playDeck);
            double currentBalance = GetMoney("start", 0, 0);
            bool isCardInDeck = true; bool isMoneyLeft = true; bool playRound = true; bool betruger = false;
            //The main loop including the games steps
            while (isCardInDeck && isMoneyLeft && playRound)
            {
                //Setting Bet
                double currentBet = GetBet(currentBalance);
                currentBalance = GetMoney("-", currentBalance, currentBet);
                //Players and Dealer draw and Surrender is an option
                int[] playerHand = new int[22];
                for (int i = 0; i < 2; i++)
                {
                    playerHand[i] = DrawCard(deckShuffled);
                    deckShuffled = EmptyTop(deckShuffled);
                }
                int[] DealerHand = new int[22];
                for (int i = 0; i < 2; i++)
                {
                    DealerHand[i] = DrawCard(deckShuffled);
                    deckShuffled = EmptyTop(deckShuffled);
                }
                bool makeChoice = true;
                string surrender = Surrender(DealerHand, playerHand, currentBet);
                if (surrender == "Aufgeben")
                {
                    currentBalance = GetMoney("Surrender", currentBalance, currentBet);
                    makeChoice = false;
                }
                //The Loop of Player's Choices
                bool Bust = false;
                int PlayerCard = 2;
                while (makeChoice && !Bust)
                {
                    Console.Clear();
                    int PlayerChoice = PlayerTurn(playerHand, DealerHand, deckShuffled, currentBalance, currentBet, discardPile, cheatCodes);
                    switch (PlayerChoice)
                    {
                        //Choose to hit
                        case 1:
                            {
                                playerHand[PlayerCard] = DrawCard(deckShuffled);
                                deckShuffled = EmptyTop(deckShuffled);
                                Bust = CheckBust(playerHand);
                                makeChoice = true;
                                PlayerCard++;
                                break;
                            }
                        //Choose to double and check if possible
                        case 2:
                            {
                                bool RepeatDouble = true;
                                while (RepeatDouble)
                                {
                                    double CanDouble = GetMoney("check", currentBalance, currentBet * 2);
                                    if (CanDouble == 2)
                                    {
                                        currentBalance = GetMoney("-", currentBalance, currentBet);
                                        currentBet = currentBet * 2;
                                        RepeatDouble = false;
                                        playerHand[PlayerCard] = DrawCard(deckShuffled);
                                        deckShuffled = EmptyTop(deckShuffled);
                                        Bust = CheckBust(playerHand);
                                        makeChoice = false;
                                    }
                                    else
                                    {
                                        RepeatDouble = false;
                                        playRound = false;
                                        betruger = true;
                                        for (int i = 0; i < 100; i++)
                                        {
                                            Console.WriteLine("Nicht genügend Geld, Betrüger!");
                                            Thread.Sleep(10);
                                        }
                                        Thread.Sleep(100);
                                        Console.WriteLine("\nDu wurdest aus dem Casino geworfen. \nKomm wieder, wenn du Geld hast!");
                                    }
                                }
                                MakeChoice = false;
                                break;
                            }
                        //Choose to Stand
                        case 3:
                            {
                                MakeChoice = false;
                                break;
                            }
                    }
                }
                if (!betruger)
                {
                    //Dealer Drawing and outputting the final results
                    if (Bust == true)
                    {
                        int PlayerFinal = FinalScore(playerHand);
                        int DealerFinal = FinalScore(DealerHand);
                        Console.WriteLine($"Bust! Du hattest {PlayerFinal}, der Dealer hatte {DealerFinal}.");
                    }
                    else
                    {
                        int PlayerFinal = FinalScore(playerHand);
                        int DealerFinal = FinalScore(DealerHand);
                        int DealerCard = 2;
                        while (DealerFinal < 17 && DealerFinal < PlayerFinal)
                        {
                            DealerHand[DealerCard] = DrawCard(deckShuffled);
                            deckShuffled = EmptyTop(deckShuffled);
                            DealerFinal = FinalScore(DealerHand);
                        }
                        if (DealerFinal > 21)
                        {
                            Console.WriteLine("Der Dealer geht Bust! Du gewinnst!");
                            currentBalance = Payout(currentBet, currentBalance);
                        }
                        else if (PlayerFinal > DealerFinal)
                        {
                            Console.WriteLine($"Du hast {PlayerFinal}, der Dealer hat {DealerFinal}. Du gewinnst!");
                            currentBalance = Payout(currentBet, currentBalance);
                        }
                        else
                        {
                            Console.WriteLine($"Du hast {PlayerFinal}, der Dealer hat {DealerFinal}. Du verlierst!");
                        }
                    }
                    //Discarding Hands and Checking if another round is possible
                    discardPile = Discard(DealerHand, playerHand, discardPile);
                    isMoneyLeft = CheckForMoney(currentBalance);
                    isCardInDeck = CheckForCard(deckShuffled);
                    if (isMoneyLeft && isCardInDeck)
                    {
                        Console.WriteLine("\nDrücke Enter um noch eine Runde zu spielen. \nDrücke etwas anderes um aufzuhören.");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                            playRound = true;
                        else
                            playRound = false;
                    }
                    else if (!isCardInDeck)
                    {
                        Console.WriteLine("\nDas Casino hat für heute Geschlossen, da es keine spielbaren Karten mehr hat.");
                        playRound = false;
                    }
                    else if (!isMoneyLeft)
                    {
                        Console.WriteLine("\nDu wurdest aus dem Casino geworfen. \nKomm wieder, wenn du Geld hast!");
                        playRound = false;
                    }
                }
            }
        }
        //Get Array of 6 or 8 Playdecks
        static int[] GetPlayDeck()
        {
            // Register Small Playdeck
            int[] playDeckSmall = {
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11
                };
            // register Big Playdeck
            int[] playDeckBig = {
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11,
                    2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11
                };
            bool IsNumberOfDecksNumeric = false;
            int NumberOfDecks = 6;
            Console.Clear();
            while (!IsNumberOfDecksNumeric)      //Check if amount if valid
            {
                Console.WriteLine("Möchtest du mit 6 oder mit 8 Decks spielen?");
                IsNumberOfDecksNumeric = int.TryParse(Console.ReadLine(), out NumberOfDecks);      //Readline
                if (IsNumberOfDecksNumeric && NumberOfDecks == 6 || NumberOfDecks == 8)         //Is it valid?
                { }
                else        //Error message
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingange");
                    IsNumberOfDecksNumeric = false;
                }
            }
            if (NumberOfDecks == 6)         //Return Small Playdeck
            {
                Console.Clear();
                return playDeckSmall;
            }
            else        //Return Big Playdeck
            {
                Console.Clear();
                return playDeckBig;
            }
        }
        static int[] ShuffleDeck(int[] deckToShuffle)
        {
            Random ranNum = new Random();
            int randomNum, temp;
            for (int j = 0; j < 10; j++) //number of shuffels
            {
                for (int i = deckToShuffle.Length - 1; i > 0; --i)
                {
                    //new random for non shuffeled item
                    randomNum = ranNum.Next(i + 1);
                    //swap the random item with last item in the array
                    temp = deckToShuffle[i];
                    deckToShuffle[i] = deckToShuffle[randomNum];
                    deckToShuffle[randomNum] = temp;
                }
            }
            return deckToShuffle;
        }
        // Function for getting one card that wasnt drawn before
        static int DrawCard(int[] deck)
        {
            int i = 0;
            while (deck[i] == 0)     //Loop for checking that the card hasnt been drawn before
            {
                i++;
            }
            int Card = deck[i];
            return Card;      //Returns the value of the drawn card
        }
        // Deletes the top card of the deck
        static int[] EmptyTop(int[] deck)
        {
            int i = 0;
            while (deck[i] == 0)
            {
                i++;
            }
            deck[i] = 0;
            return deck;
        }
        //Get starting Money, adds or substracts a specific amount of money
        static double GetMoney(string moneyType, double money, double amount)
        {
            double returnValue = 0;
            switch (moneyType)
            {
                case "start":       // Case for getting startingmoney
                    bool IsStartingMoneyNumeric = false;
                    Console.Clear();
                    while (!IsStartingMoneyNumeric)      //Loop for checking if amount is valid
                    {
                        Console.WriteLine("Mit wie viel Startgeld willst du starten? ");
                        IsStartingMoneyNumeric = double.TryParse(Console.ReadLine(), out returnValue);
                        if (IsStartingMoneyNumeric && returnValue > 0)         //Success message
                        { }
                        else        //Error message for an invalid amount
                        {
                            Console.Clear();
                            Console.WriteLine("Ungültige Eingabe");
                        }
                    }
                    break;
                case "check":       // Case for checking if player has enough money
                    if (amount > money)
                    {
                        returnValue = 1;
                        break;
                    }
                    else
                    {
                        returnValue = 2;
                        break;
                    }
                case "Surrender":       //Case for surrender function
                    returnValue = money + (amount / 2);
                    break;
                case "-":       //Case for substracting money
                    returnValue = money - amount;
                    break;
                case "+":       //Case for adding money
                    returnValue = money + amount;
                    break;
                default:        //If someone was to stupid to use the correct case
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }
        //Get how much money the players betts
        static double GetBet(double currentBalance)
        {
            Console.Clear();
            bool IsBetNumeric = false;
            double Bet = 0;
            while (!IsBetNumeric)          //Loop to ensure a valid amount of money
            {
                Console.OutputEncoding = Encoding.UTF8;         //Formating console for allowing €
                Console.WriteLine($"Kontostand: {currentBalance}€");
                Console.WriteLine("Wie viel willst du setzen?");
                //Check if amount is valid
                IsBetNumeric = double.TryParse(Console.ReadLine(), out Bet);
                if (IsBetNumeric)
                {
                    if (currentBalance >= Bet && Bet > 0)          //Success message
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Du hast erfolgreich {Bet}€ gesetzt");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    else        //Error message if player doesnt have enough money
                    {
                        Console.Clear();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine("Du darfst nur so viel setzen, wie du auch hast!");
                        Console.WriteLine($"Aktuell hast du {currentBalance}€");
                        IsBetNumeric = false;
                    }
                }
                else        // Error message if its not a valid amount
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                }
            }
            return Bet;
        }
        //Asking the player for what to do
        static int PlayerTurn(int[] playerHand, int[] DealerHand, int[] deckShuffled, double currentBalance, double currentBet, int[] discardPile, int[] cheatCodes)
        {
            //Checking size of hand
            int HandSize = 0;
            for (int i = 0; i < 20; i++)
            {
                if (playerHand[i] != 0)
                {
                    HandSize++;
                }
            }
            //Outputting Balance, Bet, Cards and Choices
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Kontostand: {currentBalance}€, Gesetzt: {currentBet}€");
            int pHC = 0;
            Console.Write("Du hast ");
            while (playerHand[pHC] != 0)
            {
                if (playerHand[pHC] != 0)
                {
                    pHC++;
                    Console.Write(playerHand[pHC - 1]);
                    if (playerHand[pHC] != 0 && playerHand[pHC + 1] != 0)
                    {
                        Console.Write(", ");
                    }
                    else if (playerHand[pHC + 1] == 0 && playerHand[pHC] != 0)
                    {
                        Console.Write(" und ");
                    }
                }
            }
            Console.Write(".");
            Console.WriteLine("");
            Console.WriteLine($"Eine Karte des Dealers ist {DealerHand[0]}. Was möchtest du machen?" +
               $"\n1: Hit" +
               $" \n2: Double" +
               $" \n3: Stand");
            CheatCodeCheck(cheatCodes, DealerHand, playerHand, discardPile, deckShuffled);
            //Checking if player acton is valid
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
        //Checking if either Player or Dealer went Bust
        static bool CheckBust(int[] Hand)
        {
            //Calculating total score including reducing 11 to 1
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
            //Returning if Bust
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
                i++;
            }
            return discardPile;
        }
        //Surrender function
        static string Surrender(int[] dealerHand, int[] playerHand, double currentBet)
        {
            Console.Clear();
            int surrYesOrNo = 0;
            bool IsSurrenderNumeric = false;
            while (!IsSurrenderNumeric && surrYesOrNo != 1 || !IsSurrenderNumeric && surrYesOrNo != 2)
            {
                Console.WriteLine($"Du hast {playerHand[0]} und {playerHand[1]}. \nEine Karte des Dealers ist {dealerHand[0]}. Dein Einsatz ist {currentBet}€. Möchtest du Aufgeben? (Du verlierst nur die Hälfte deines Einsatzes)" +
                               $"\n1: Nein \n2: Ja");
                IsSurrenderNumeric = int.TryParse(Console.ReadLine(), out surrYesOrNo);
                Console.Clear();
                if (surrYesOrNo != 1 && surrYesOrNo != 2)
                {
                    IsSurrenderNumeric = false;
                }
                if (surrYesOrNo == 2)
                {
                    return "Aufgeben";
                }
                else if (surrYesOrNo == 1)
                {
                    return "Julian";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                }
            }
            return "CodeBroken";
        }
        //Calculates Final Score of Player and Dealer
        static int FinalScore(int[] Hand)
        {
            int HandTotal = 0;
            for (int i = 0; i < 22; i++)
            {
                HandTotal = Hand[i] + HandTotal;
            }
            if (HandTotal > 21)
            {
                HandTotal = 0;
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
        //Checks if at least 8 Cards remain
        static bool CheckForCard(int[] Deck)
        {
            if (Deck[Deck.Length - 8] == 0)
                return false;
            else
                return true;
        }
        //Checks if any money is left
        static bool CheckForMoney(double Balance)
        {
            if (Balance > 0)
                return true;
            else
                return false;
        }
        static double Payout(double currentBet, double currentBalance)
        {
            return currentBet * 1.5 + currentBalance;
        }
        static int CardCountTrue(int[] dealerHand, int[] playerHand, int[] discardPile, int[] deck)
        {
            int runningCount, trueCount, playerLength, discLength, deckLength, deckLength2;
            deckLength = 0;
            for (deckLength = deck.Length - 1; deckLength < deck.Length && deck[deckLength] != 0; deckLength--) { }
            deckLength = deck.Length - deckLength;
            playerLength = 0; discLength = 0; runningCount = 0;
            for (; playerLength < playerHand.Length && playerHand[playerLength] != 0; playerLength++)
            {
                if (playerHand[playerLength] > 1 && playerHand[playerLength] < 7)
                {
                    runningCount++;
                }
                if (playerHand[playerLength] > 9)
                {
                    runningCount--;
                }
            }
            if (dealerHand[0] > 1 && dealerHand[0] < 7)
            {
                runningCount++;
            }
            if (dealerHand[0] > 9)
            {
                runningCount--;
            }
            for (; discLength < discardPile.Length && discardPile[discLength] != 0; discLength++)
            {
                if (discardPile[discLength] > 1 && discardPile[discLength] < 7)
                {
                    runningCount++;
                }
                if (discardPile[discLength] > 9)
                {
                    runningCount--;
                }
            }
            deckLength2 = deckLength / 52;
            trueCount = runningCount / deckLength2;
            Console.WriteLine($"Running Count: {runningCount}.");
            return trueCount;
        }
        static int[] CheatCodeAsk()
        {
            //needs int[] cheatCodes = CheatCodeAsk(); at the start of main
            int[] cheatCodes = { 0, 0, 0, 0 };
            string tempCode;
            Console.WriteLine("Starten? \n1. Ja");
            string jaNein = Console.ReadLine();
            Console.Clear();
            while (jaNein == "CheatMenu = true")
            {
                Console.WriteLine("Gib exit ein um zu beenden\nCode: ");
                tempCode = Console.ReadLine();
                if (tempCode == "CardCount")
                {
                    cheatCodes[0] = 1;
                }
                else if (tempCode == "RevealDealerCard")
                {
                    cheatCodes[1] = 1;
                }
                else if (tempCode == "exit")
                {
                    jaNein = "";
                }
            }
            //
            //for (int i = 0; i < cheatCodes.Length; i++)
            //{
            //    Console.Write(cheatCodes[i]);
            //    if (i != cheatCodes.Length - 1)
            //    {
            //        Console.Write(", ");
            //    }
            //}
            //Thread.Sleep(500);
            return cheatCodes;
        }
        static void CheatCodeCheck(int[] cheatCodes, int[] dealerHand, int[] playerHand, int[] discardPile, int[] deck)
        {
            //cmd is CheatCodeCheck(cheatCodes, DealerHand, playerHand, discardPile, deckShuffled);
            int tempint;
            if (cheatCodes[0] == 1)
            {
                tempint = CardCountTrue(dealerHand, playerHand, discardPile, deck);
                Console.WriteLine($"True Count ist {tempint}.");
            }
            if (cheatCodes[1] == 1)
            {
                Console.WriteLine($"Second Dealer Card: {dealerHand[1]}.");
            }
        }
    }
}
