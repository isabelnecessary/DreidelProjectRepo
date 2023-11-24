/* Business Rules

Everyone spins and the highest spin goes first
    if two people have the same, keep spinning until a winner

    when a player runs out of tokens, they are out
    when one player has all the tokens, they have won

a) Nun means “nisht” or “nothing.” The player does nothing.
b) Gimel  means “gantz” or “everything.” The player gets everything in the pot.
c) Hey means “halb” or “half.” The player gets half of the pot. (If there is an odd number of pieces in the pot, the player takes half of the total plus one).
d) Shin (outside of Israel) means “shtel” or “put in.” */

int numberOfPlayers = 0;
string[] players = new string[numberOfPlayers]; 
int pot = numberOfPlayers * 5;
Random random = new Random();
string[] dreidelSides = {"\u05E0", "\u05D2", "\u05D4", "\u05E9"}; // nun, gimmel, hey, shin
int dreidelSpinRandom = random.Next(1, 4); // dreidelSides.Count +1); // TODO - 
string dreidelSpinResult = dreidelSides[dreidelSpinRandom];
int playerScore = 0; // TODO - how to get everyone's scores? I guess make a method to CreatePlayer() every time I add a new player?

//TODO - get number and names of players and store in an array

void makePlayer()
{
    Console.WriteLine("enter the number of players (max 4)");
    string ?readResult = Console.ReadLine(); // TODO make this its own method so I can loop it
    Int32.TryParse(readResult, out numberOfPlayers); //todo add null handling
    if (numberOfPlayers > 4) Console.WriteLine("Sorry, you need at least 2 and at most 4 players to play dreidel! Enter a new player value");
    //TODO add try again functionality

    Console.WriteLine("let's do a practice spin to see who goes first!");
    for (int i = 0; i < numberOfPlayers; i++) 
    {   
        //TODO add player1 player2 etc and print them next to scores
        Console.WriteLine(dreidelSpinRandom);
    }
    // TODO if two people have the same, keep spinning until a winner

}

void gameTurn()
{
    foreach (string player in players) {
    // spin the dreidel
        switch (dreidelSpinResult)
        {
            case "\u05E0":
                Console.WriteLine("\u05E0"); //nun - use unicode to show hebrew nun
                Console.WriteLine("Do nothing - nisht");
                break;
            case "\u05D2":
                Console.WriteLine("\u05D2"); //gimmel - get the whole pot
                playerScore += pot;
                break;
            case "\u05D4":
                Console.WriteLine("\u05D4"); // hey - get half the pot
                playerScore = pot % 2 == 0 ? playerScore += pot /2 : playerScore += 1 + pot /2; // if pot is odd, get half+1
                break;
            case "\u05E9":
                Console.WriteLine("\u05E9"); // shin - put one in the pot
                playerScore --;
                pot ++;
                break;
        }
    }
}