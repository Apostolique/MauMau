// https://en.wikipedia.org/wiki/Mau-Mau_(card_game)

namespace MauMau {
    public class Program {
        public static void Main(string[] args) {
            string[] suits = {"Hearts", "Spades", "Diamonds", "Clubs"};
            string[] ranks = {"Ace of", "2 of", "3 of", "4 of", "5 of", "6 of", "7 of", "8 of", "9 of", "10 of", "Jack of", "Queen of", "King of"};
            string[] an = {"Ace", "8"};

            int a = 0;
            int b = 0;
            int c = 0;

            string?[] deck = new string[52];
            string?[] discard = new string[52];
            string?[] remake = new string[52];
            string?[] hand1 = new string[52];
            string?[] hand2 = new string[52];

            string? input1;
            int input2 = 0;
            int state = 0; // The various cards of the game. (0) = setup the cards. (1) = setup the hands. (2) = Player 1 turn. (3) = Player 2 turn. (4) = Reshuffle the discard and put them back in the game. (5) = Final reference of the game.
            int remake1 = 0; // Cards must be added to the player 1's hand.
            int remake2 = 0; // Cards must be added to the player 2's hand.
            int add1 = 0; // Amount of cards to add to the player 1's hand.
            int add2 = 0; // Amount of cards to add to the player 2's hand.
            int flip = 0; // Switch showing that a new card must be added on top of the deck. (1) = deck card (Happens at the start of the game.). (2) Card in player 1's hand. (3) Card in player 2's hand.
            int counter = 0; // To count the amount of cards that were taken from the deck.

            string[] card;
            string[] cardDiscarted;
            string delimiter = " ";

            Random random = new Random();

            while (state != 5) {
                while (state == 0) { // Setup the game, make the cards and shuffle the deck.
                    for (int i = 0; i <= 12; i++) {
                        for (int j = 0; j <= 3; j++) {
                            deck[a] = $"{ranks[i]} {suits[j]}";
                            a++;
                        }
                    }
                    a = 0;

                    deck = deck.OrderBy(x => random.Next()).ToArray();

                    state = 1;
                }

                while (state == 1) {
                    add1 = 5; // Amount of cards to give player 1.
                    add2 = 5; // Amount of cards to give player 2.
                    remake1 = 1; // A switch: 1 means on, 0 means off.
                    remake2 = 1; // A switch: 1 means on, 0 means off.
                    flip = 1; // A switch: 1 means on, 0 means off.
                    state = 2; // (2) = Player 1's turn.
                }

                while (remake1 == 1 && add1 != 0) { // When cards are removed from the deck by player 1.
                    for (int i = 0; i <= add1 - 1; i++) {
                        if (hand1[i + c] == null) {
                            hand1[i + c] = deck[i];
                            counter++;

                            for (int j = 0; j <= add1 - 1 && i == add1 - 1; j++) { // Shift the cards to replace the ones that got removed.
                                for (int k = 0; k <= 50; k++) {
                                    deck[k] = deck[k + 1];
                                }
                            }
                            b = 0;

                            while (i == add1 - 1 && 52 - add1 != 52 && b + 51 - add1 <= 50) {
                                deck[b + 52 - add1] = null;
                                b++;
                            }
                        } else {
                            c++;
                            i--;
                        }
                    }

                    add1 = 0;
                    remake1 = 0;
                    b = 0;
                    c = 0;
                }
                while (remake2 == 1 && add2 != 0) { // When cards are removed from the deck by player 2.
                    for (int i = 0; i <= add2 - 1; i++) {
                        if (hand2[i + c] == null) {
                            hand2[i + c] = deck[i];
                            counter++;

                            for (int j = 0; j <= add2 - 1 && i == add2 - 1; j++) { // Shift the cards to replace the ones that got removed.
                                for (int k = 0; k <= 50; k++) {
                                    deck[k] = deck[k + 1];
                                }
                            }
                            b = 0;

                            while (i == add2 - 1 && 52 - add2 != 52 && b + 51 - add2 <= 50) {
                                deck[b + 52 - add2] = null;
                                b++;
                            }
                        } else {
                            c++;
                            i--;
                        }
                    }

                    add2 = 0;
                    remake2 = 0;
                    b = 0;
                    c = 0;
                }

                while (flip == 1) { // To flip the card on top of the deck and put it in the discard.
                    for (int i = 51; i >= 2; i--) { // Move the cards towards the bottom before putting the card on top of the discard.
                        discard[i] = discard[i - 1];
                    }
                    discard[0] = deck[0];
                    for (int j = 0; j <= 0; j++) { // Move the cards up to replace the removed cards.
                        for (int k = 0; k <= 50; k++) {
                            deck[k] = deck[k + 1];
                        }
                    }
                    b = 0;
                    while (b + 50 <= 50) {
                        deck[b + 51] = null;
                        b++;
                    }
                    flip = 0;
                    b = 0;
                    counter++;
                }
                while (flip == 2) { // To flip the card on top of player 1's hand and put it in the discard.
                    for (int i = 51; i >= 1; i--) {
                        discard[i] = discard[i - 1];
                    }
                    discard[0] = hand1[input2 - 1];
                    hand1[input2 - 1] = null;
                    for (int j = 0; j <= 0; j++) { // Move the cards up to replace the removed cards.
                        for (int k = input2 - 1; k <= 50 - input2; k++) {
                            hand1[k] = hand1[k + 1];
                        }
                    }
                    b = 0;
                    while (b + 50 <= 50) {
                        hand1[b + 51] = null;
                        b++;
                    }
                    flip = 0;
                    b = 0;
                    input2 = 0;
                }
                while (flip == 3) { // To flip the card on top of player 2's hand and put it in the discard.
                    for (int i = 51; i >= 1; i--) {
                        discard[i] = discard[i - 1];
                    }
                    discard[0] = hand2[input2 - 1];
                    hand2[input2 - 1] = null;
                    for (int j = 0; j <= 0; j++) { // Move the cards up to replace the removed cards.
                        for (int k = input2 - 1; k <= 50 - input2; k++) {
                            hand2[k] = hand2[k + 1];
                        }
                    }
                    b = 0;
                    while (b + 50 <= 50) {
                        hand2[b + 51] = null;
                        b++;
                    }
                    flip = 0;
                    b = 0;
                    input2 = 0;
                }

                if (deck[0] == null && remake1 != 1 && remake2 != 1) { // To reshuffle the carts when the deck is empty. There is a bug here somewhere.
                    while (a != 50) { // Copy all the cards under the one on top of the discard towards the temp deck that will be shuffled.
                        if (discard[a + 1] != null && a != 50) {
                            remake[a] = discard[a + 1];
                            discard[a + 1] = null;
                            a++;
                        } else {
                            a++;
                        }
                    }
                    a = 0;
                    b = 0;
                    c = 0;

                    remake = remake.OrderBy(x => random.Next()).ToArray();

                    for (int i = 0; i < 51; i++) {
                        if (remake[i] == null) {
                            for (int j = i; j < 52; j++) {
                                if (remake[j] != null) {
                                    remake[i] = remake[j];
                                    remake[j] = null;
                                    i++;
                                    j = i;
                                }
                            }
                        }
                    }

                    Console.WriteLine();
                    while (c != 51 && remake[c] != null) {
                        deck[c] = remake[c];
                        remake[c] = null;
                        c++;
                    }
                    Console.WriteLine("Deck Reshuffled");
                    if (deck[0] == null) {
                        Console.WriteLine("There are no more cards. Drawing cards is no longer possible.");
                    }

                    a = 0;
                    b = 0;
                    c = 0;
                }

                if (hand1[0] == null) {
                    Console.WriteLine("\nYou have won!\n");
                    state = 5;
                } else if (hand2[0] == null) {
                    Console.WriteLine("\nYou have lost!\n");
                    state = 5;
                }

                while (state == 2 && remake1 != 1 && remake2 != 1 && flip == 0) { // This is where player 1 can control their cards.
                    for (int i = 0; i == 0; i++) {
                        Console.WriteLine($"\n///\n///\t{discard[0]}\n///\n"); // To show the top card on the discard.
                        b = 0;
                        Console.WriteLine("\nPlayer 1 hand\n");

                        while (hand1[b] != null) {
                            Console.WriteLine($"{b + 1}. \t{hand1[b]}");
                            b++;
                        }

                        Console.WriteLine($"\nPick a card by entering the number next to it.\nAvailable number(s): 1 to {b} and {b + 1} to draw a card.\n");
                        input1 = Console.ReadLine();

                        if (Int32.TryParse(input1, out input2)) {
                            if (input2 > 0 && input2 <= b || input2 - 1 == b) {
                                if (input2 > 0 && input2 <= b && input2 - 1 != b) {
                                    card = hand1[input2 - 1]!.Split(delimiter);
                                    cardDiscarted = discard[0]!.Split(delimiter);

                                    if (card[0] == cardDiscarted[0]) {
                                        state = 3;
                                        flip = 2;
                                    } else if (card[2] == cardDiscarted[2]) {
                                        state = 3;
                                        flip = 2;
                                    } else {
                                        Console.WriteLine("\nYour card cannot be played right now.");
                                        input2 = 0;
                                        i = -1;
                                        b = 0;
                                    }
                                } else if (input2 - 1 == b) {
                                    Console.WriteLine("\nA card was added to your hand.\n");
                                    input2 = 0;
                                    remake1 = 1;
                                    add1 = 1;
                                    b = 0;
                                    i = 1;
                                    state = 3;
                                } else {
                                    Console.WriteLine("Not a valid card.");
                                    input2 = 0;
                                    i = -1;
                                    b = 0;
                                }
                            }
                        }

                        if (input2 <= 0 && i != -1 && i != 1 || input2 > b + 1 && i != -1 && i != 1) {
                            Console.WriteLine("\nYour input cannot be understood.");
                            i = -1;
                            b = 0;
                        }
                    }
                }
                while (state == 3 && remake1 != 1 && remake2 != 1 && flip == 0) { // This is where player 2 can control their cards.
                    for (int i = 0; i == 0; i++) {
                        b = 0;

                        while (hand2[b] != null) {
                            b++;
                        }

                        input2 = 0;
                        card = hand2[0]!.Split(delimiter);
                        cardDiscarted = discard[0]!.Split(delimiter);
                        for (int j = 0; j <= b - 1 && card[0] != cardDiscarted[0]; j++) {
                            input2 = j;
                            card = hand2[input2]!.Split(delimiter);
                            cardDiscarted = discard[0]!.Split(delimiter);

                            if (card[0] == cardDiscarted[0]) {
                                input1 = $"{j}";
                                j = b + 1;
                            }
                        }
                        for (int j = 0; j <= b - 1 && card[2] != cardDiscarted[2] && card[0] != cardDiscarted[0]; j++) {
                            input2 = j;
                            card = hand2[j]!.Split(delimiter);
                            cardDiscarted = discard[0]!.Split(delimiter);

                            if (card[2] == cardDiscarted[2]) {
                                j = b + 1;
                            }
                        }
                        if (card[0] != cardDiscarted[0] && card[2] != cardDiscarted[2]) {
                            input2 = b;
                        }
                        input2++;
                        input1 = $"{input2}";
                        if (input2 != b + 1) {
                            if (an.Contains(card[0])) {
                                Console.WriteLine($"\nThe AI played an {hand2[input2 - 1]}. They have {b - 1} cards remaining.");
                            } else {
                                Console.WriteLine($"\nThe AI played a {hand2[input2 - 1]}. They have {b - 1} cards remaining.");
                            }
                        }

                        if (Int32.TryParse(input1, out input2)) {
                            if (input2 > 0 && input2 <= b || input2 - 1 == b) {
                                if (input2 > 0 && input2 <= b && input2 - 1 != b) {
                                    card = hand2[input2 - 1]!.Split(delimiter);
                                    cardDiscarted = discard[0]!.Split(delimiter);

                                    if (card[0] == cardDiscarted[0]) {
                                        state = 2;
                                        flip = 3;
                                    } else if (card[2] == cardDiscarted[2]) {
                                        state = 2;
                                        flip = 3;
                                    } else {
                                        Console.WriteLine("\nYour card cannot be played right now.");
                                        input2 = 0;
                                        i = -1;
                                        b = 0;
                                    }
                                } else if (input2 - 1 == b) {
                                    Console.WriteLine($"\nThe AI drew a card. They now have {b + 1} cards remaining.");
                                    input2 = 0;
                                    remake2 = 1;
                                    add2 = 1;
                                    b = 0;
                                    i = 1;
                                    state = 2;
                                } else {
                                    Console.WriteLine("Not a valid card.");
                                    input2 = 0;
                                    i = -1;
                                    b = 0;
                                }
                            }
                        }

                        if (input2 <= 0 && i != -1 && i != 1 || input2 > b + 1 && i != -1 && i != 1) {
                            Console.WriteLine("\nYour input cannot be understood.");
                            i = -1;
                            b = 0;
                        }
                    }
                }
            }

            Thread.Sleep(3000);
        }
    }
}
