//TEST COMMIT

/* Business Rules

Everyone spins and the highest spin goes first
    if two people have the same, keep spinning until a winner

a) Nun means “nisht” or “nothing.” The player does nothing.
b) Gimel  means “gantz” or “everything.” The player gets everything in the pot.
c) Hey means “halb” or “half.” The player gets half of the pot. (If there is an odd number of pieces in the pot, the player takes half of the total plus one).
d) Shin (outside of Israel) means “shtel” or “put in.” 

when a player runs out of tokens, they are out
when one player has all the tokens, they have won
*/

bool numberOfPlayersIsValidInteger = false;

string[] potentialMaxPlayers = {"Player1", "Player2", "Player3", "Player4", "Player5"};
string[] playerNames = new string[potentialMaxPlayers.Length]; 
int[] playerScores = new int[potentialMaxPlayers.Length];

int numberOfPlayers = HowManyPlayers();
int pot = numberOfPlayers * 5;

MakePlayer();
// setPlayerOrder();
do {
    GameTurn(playerNames, playerScores);
} while (numberOfPlayers > 1);
Console.WriteLine($"Only one player is left - game over, and congratulations to {playerNames[0]} for winning with {playerScores[0]} tokens!");
//TODO - when players are removed, the corresponding playerScores aren't removed, so I can't print their score as well...


int HowManyPlayers()
{
    Console.WriteLine("enter the number of players (max 5)");
    string? readResult = Console.ReadLine();

    numberOfPlayersIsValidInteger = int.TryParse(readResult, out int numberOfPlayers);

    if (numberOfPlayersIsValidInteger && numberOfPlayers > 1 && numberOfPlayers <= 5 ) 
    {       
        Console.WriteLine($"You are playing dreidel with {numberOfPlayers-1} {(numberOfPlayers > 2 ? "friends" : "friend")}!");
    } 
    else
    {   
        Console.WriteLine($"You entered {readResult}, please enter a valid number between 2 and 5");
        return HowManyPlayers();
    }  

    return numberOfPlayers;
}

void MakePlayer()
{
    Array.Copy(potentialMaxPlayers, playerNames, numberOfPlayers); 
    Console.WriteLine("Here are the players: ");
    foreach (string player in playerNames) Console.WriteLine(player);
}

void SetPlayerOrder(int numberOfPlayers)
{   
    string[] initialSpinResults = new string[playerNames.Length];
    
    Console.WriteLine("let's do a practice spin to see who goes first!");
    for (int i = 0; i < numberOfPlayers; i++)
    {   
        initialSpinResults[i] = DreidelSpin();

        //TODO need to reorganise playerNames based on this!
        
        Console.WriteLine($"{playerNames[i]}\t{playerScores[i]}");
    }
    
    //if dreidelSpinRandom == dreidelSpinRandom 
    {

    }

}

//TODO different players need different scores
void GameTurn(string[] playerNames, int[] playerScores)
{
    Console.WriteLine("\nNew round!");
    for (int i = 0; i < numberOfPlayers; i++) {
    // spin the dreidel
        switch (DreidelSpin())
        {
            case "\u05E0":
                Console.WriteLine($"{playerNames[i]} has rolled a \u05E0 nun"); //nun - use unicode to show hebrew nun
                Console.WriteLine("Nisht - do nothing");
                break;
            case "\u05D2":
                Console.WriteLine($"{playerNames[i]} has rolled a \u05D2 gimmel"); //gimmel - get the whole pot
                Console.WriteLine("Gantz ('everything') - get the whole pot.");
                playerScores[i] += pot;
                pot = 0;
                break;
            case "\u05D4":
                Console.WriteLine($"{playerNames[i]} has rolled a \u05D4 hey"); // hey - get half the pot, or if pot is odd get half+1
                Console.WriteLine("Halb ('half') - get half the pot.");
                if (pot % 2 == 0)
                {
                    playerScores[i] += pot /2;
                    pot /= 2;
                }
                else
                {
                    playerScores[i] += 1 + pot /2;
                    pot /= 2;
                }                
                break;
            case "\u05E9":
                Console.WriteLine($"{playerNames[i]} has rolled a \u05E9 shin"); // shin - put one in the pot
                Console.WriteLine("Shtel ('put') - put one of your tokens in the pot.");
                if (playerScores[i] > 0)
                {
                    try {playerScores[i] --;}                              
                    catch
                    {
                        //TODO problem, is handling "0 players left" (???) but in wrong place
                        //This handles the case where 0 players are left
                        ArgumentNullException ex;
                        Console.WriteLine("You have no tokens to put back! You are out of the game.");
                        numberOfPlayers--;                   
                        int indexToRemove = i; //Array.IndexOf(playerNames, playerNames[i]);
                        playerNames = playerNames.Where((source, index) => index != indexToRemove).ToArray();
                    }   
                    pot ++;
                }
                else 
                {
                    Console.WriteLine("You have no tokens to put back! You are out of the game.");
                    numberOfPlayers--;                   
                    int indexToRemove = i; //Array.IndexOf(playerNames, playerNames[i]);
                    playerNames = playerNames.Where((source, index) => index != indexToRemove).ToArray();
                    playerScores = playerScores.Where((source, index) => index != indexToRemove).ToArray();
                    Console.WriteLine("Here are the current players:");
                    foreach (string player in playerNames) Console.WriteLine(player);
                }
                break;
        }
    }
    Console.WriteLine("Player scores:");
    for (int i = 0; i < numberOfPlayers; i++)
    {
        Console.WriteLine($"{playerNames[i]} has {playerScores[i]} tokens");
    }
}

string DreidelSpin() 
{
    Random random = new Random();
    string[] dreidelSides = {"\u05E0", "\u05D2", "\u05D4", "\u05E9"}; // nun, gimmel, hey, shin
    
    int integerDreidelSpin = random.Next(1, 4); // dreidelSides.Count +1);
    string hebrewLetterDreidelSpin = dreidelSides[integerDreidelSpin];
    
    return hebrewLetterDreidelSpin;
}