//TEST COMMIT

/* Business Rules

Everyone spins and the highest spin goes first
    if two people have the same, keep spinning until a winner

    when a player runs out of tokens, they are out
    when one player has all the tokens, they have won

a) Nun means “nisht” or “nothing.” The player does nothing.
b) Gimel  means “gantz” or “everything.” The player gets everything in the pot.
c) Hey means “halb” or “half.” The player gets half of the pot. (If there is an odd number of pieces in the pot, the player takes half of the total plus one).
d) Shin (outside of Israel) means “shtel” or “put in.” */

string readResult = "";
bool numberOfPlayersIsValidInteger = false;


int numberOfPlayers = 0;
string[] playerNames = new string[numberOfPlayers]; 
string[] potentialMaxPlayers = {"Player1, Player2, Player3, Player4"};
int pot = numberOfPlayers * 5;
int playerScore = 0;
Array.Copy(potentialMaxPlayers, playerNames, numberOfPlayers); //QUESTION should this be there or elsewhere?

Random random = new Random();
string[] dreidelSides = {"\u05E0", "\u05D2", "\u05D4", "\u05E9"}; // nun, gimmel, hey, shin
int integerDreidelSpin = random.Next(1, 4); // dreidelSides.Count +1);
string hebrewLetterDreidelSpin = dreidelSides[integerDreidelSpin];


makePlayer();
// setPlayerOrder();
gameTurn();

int howManyPlayers()
{
    Console.WriteLine("enter the number of players (max 4)");
    string ?readResult = Console.ReadLine();

    //Int32.TryParse(readResult, out numberOfPlayers);
    numberOfPlayersIsValidInteger = Int32.TryParse(readResult, out numberOfPlayers);

    return numberOfPlayers;
}

void makePlayer()
{
    howManyPlayers();

    if (numberOfPlayersIsValidInteger) 
        {
            Console.WriteLine($"You are playing dreidel with {numberOfPlayers-1} {(numberOfPlayers > 2 ? "friends" : "friend")}!");
        } 
    else
        {
            Console.WriteLine($"You entered {readResult}, please enter a valid number between 1 and 4");
        }

    if (numberOfPlayers > 4) Console.WriteLine("Sorry, you need between 2 and 4 people to play dreidel! Enter a new player value");
    
    //TODO add try again functionality

    Console.WriteLine("Here are the players: ");
    //foreach (string player in playerNames) Console.WriteLine(player);
}

void setPlayerOrder()
{
    Console.WriteLine("let's do a practice spin to see who goes first!");
    for (int i = 0; i < numberOfPlayers; i++) // each player in playerNames
    {   
        //TODO need to store everyone's first spins
        // TODO need to reorganise playerNames based on this!
        Console.WriteLine($"{playerNames[i]}\t{hebrewLetterDreidelSpin}");
    }
    
    //if dreidelSpinRandom == dreidelSpinRandom 
    {

    }

}

void gameTurn()
{
    foreach (string player in playerNames) {
    // spin the dreidel
        switch (hebrewLetterDreidelSpin)
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
                Console.WriteLine("\u05D4"); // hey - get half the pot, or if pot is odd get half+1
                playerScore = pot % 2 == 0 ? playerScore += pot /2 : playerScore += 1 + pot /2;
                break;
            case "\u05E9":
                Console.WriteLine("\u05E9"); // shin - put one in the pot
                playerScore --;
                pot ++;
                break;
        }
    }
}