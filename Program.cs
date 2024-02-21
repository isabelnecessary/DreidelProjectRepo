using System.Linq;

bool numberOfPlayersIsValidInteger = false;

string[] potentialMaxPlayers = {"Player1", "Player2", "Player3", "Player4", "Player5"};
string[] playerNames = new string[potentialMaxPlayers.Length]; 
int[] playerScores = new int[potentialMaxPlayers.Length];

int numberOfPlayers = HowManyPlayers();
int pot = numberOfPlayers * 5;

MakePlayer();
SetPlayerOrder(numberOfPlayers);
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
    for (int i = 0; i < numberOfPlayers; i++) {
        playerScores[i] = 5;
        Console.WriteLine($"{playerNames[i]} \t {playerScores[i]} tokens");
    }
}


void SetPlayerOrder(int numberOfPlayers)
{   
    string[] initialSpinResults = new string[playerNames.Length];
    
    Console.WriteLine("\nLet's do a practice spin to see who goes first!");

    for (int i = 0; i < numberOfPlayers; i++)
    {   
        initialSpinResults[i] = DreidelSpin();
       
        Console.WriteLine($"{playerNames[i]}\t\t{initialSpinResults[i]}");
    }
    
    if (initialSpinResults.Count(x => x == "\u05E9") == playerNames.Length) // if every player rolled a shin, roll again
    {
        Console.WriteLine("Everyone rolled the lowest, a \u05E9 shin! Let's try again.");
        Thread.Sleep(500);
        SetPlayerOrder(numberOfPlayers);
    }

    else if (initialSpinResults.Count(x => x == "\u05D2") == 1) // if one player rolled a gimmel
    {
        int indexOfGimmel = Array.IndexOf(initialSpinResults, "\u05D2");

        // https://stackoverflow.com/a/25794168
        //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-8.0 
        // playerNames = playerNames.Where(
        //    (playerNames, index) => index != indexOfGimmel).ToArray();
        // Array.Copy(newPlayerNames, playerNames, playerNames.Length);       

        //An Alternative Approach
        Reorderer(playerNames, indexOfGimmel, 0);
    }

    else if (initialSpinResults.Count(x => x == "\u05D4") == 1) // if one player rolled a hey
    {
        int indexOfHey = Array.IndexOf(initialSpinResults, "\u05D4");
        //TODO then player who rolls gimmel should be moved to the start of the array 
            //An Alternative Approach
        Reorderer(playerNames, indexOfHey, 0);
        
    }

    else // by default, this means playerNames.Length - 1 people rolled a shin and one person rolled a nun
    {
        int indexOfNun = Array.IndexOf(initialSpinResults, "\u05E0");
        //TODO then player who rolls nun  should be moved to the start of the array 
            //An Alternative Approach
        Reorderer(playerNames, indexOfNun, 0);
    }
}

void GameTurn(string[] playerNames, int[] playerScores)
{
    Console.WriteLine($"\nNew round! \nThe pot currently contains {pot} tokens.");
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

                    //TODO when I have 2 players and remove Player2, she's momentarily replaced by Player3 in this "Here are the current players" but then not anywhere else
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
    
    int integerDreidelSpin = random.Next(0, dreidelSides.Length); // dreidelSides.Count +1);
    string hebrewLetterDreidelSpin = dreidelSides[integerDreidelSpin];
    
    return hebrewLetterDreidelSpin;
}

void Reorderer(string[] playerNames, int oldIndex, int newIndex)
{
    //https://stackoverflow.com/questions/7242909/moving-elements-in-array-c-sharp
    
    if (oldIndex == newIndex)
    {
        return;
    }

    string tmp = playerNames[oldIndex];

    if (newIndex < oldIndex) 
    {
        // Need to move part of the array "up" to make room
        Array.Copy(playerNames, newIndex, playerNames, newIndex + 1, oldIndex - newIndex);
    }
    else
    {
        // Need to move part of the array "down" to fill the gap
        Array.Copy(playerNames, oldIndex + 1, playerNames, oldIndex, newIndex - oldIndex);
    }

    playerNames[newIndex] = tmp;

    Console.WriteLine($"{playerNames[oldIndex]} is at position {newIndex} ");
}



