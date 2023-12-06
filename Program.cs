﻿//TEST COMMIT

/* Business Rules

Everyone spins and the highest spin goes first
    if two people have the same, keep spinning until a winner

    when a player runs out of tokens, they are out
    when one player has all the tokens, they have won

a) Nun means “nisht” or “nothing.” The player does nothing.
b) Gimel  means “gantz” or “everything.” The player gets everything in the pot.
c) Hey means “halb” or “half.” The player gets half of the pot. (If there is an odd number of pieces in the pot, the player takes half of the total plus one).
d) Shin (outside of Israel) means “shtel” or “put in.” */

string? readResult = "";
bool numberOfPlayersIsValidInteger = false;

int playerScore = 0;

string[] potentialMaxPlayers = {"Player1", "Player2", "Player3", "Player4"};
string[] playerNames = new string[potentialMaxPlayers.Length]; 
int[] playerScores = new int[potentialMaxPlayers.Length];

int numberOfPlayers = howManyPlayers();
int pot = numberOfPlayers * 5;

makePlayer();
// setPlayerOrder();
gameTurn(playerNames);


int howManyPlayers()
{
    Console.WriteLine("enter the number of players (max 4)");
    string? readResult = Console.ReadLine();

    numberOfPlayersIsValidInteger = int.TryParse(readResult, out int numberOfPlayers);

    if (numberOfPlayersIsValidInteger && numberOfPlayers > 0 && numberOfPlayers <= 4 ) 
    {
        
        Console.WriteLine($"You are playing dreidel with {numberOfPlayers-1} {(numberOfPlayers > 2 || numberOfPlayers == 0 ? "friends" : "friend")}!");
        //TODO if there's 0 other players   
    } 
    else
    {   
        Console.WriteLine($"You entered {readResult}, please enter a valid number between 1 and 4");
        howManyPlayers();
    }  

    return numberOfPlayers;
}

void makePlayer()
{
    //Array.Copy(potentialMaxPlayers, playerNames, numberOfPlayers-1); 
    Console.WriteLine("Here are the players: ");
    foreach (string player in potentialMaxPlayers.Take(numberOfPlayers)) Console.WriteLine(player);
}

void setPlayerOrder(int numberOfPlayers)
{
    Console.WriteLine("let's do a practice spin to see who goes first!");
    for (int i = 0; i < numberOfPlayers; i++) // each player in playerNames
    {   
        //TODO need to store everyone's first spins
        //TODO need to reorganise playerNames based on this!
        Console.WriteLine($"{playerNames[i]}\t{dreidelSpin()}");
    }
    
    //if dreidelSpinRandom == dreidelSpinRandom 
    {

    }

}

//TODO different players need different scores
void gameTurn(string[] playerNames)
{
    foreach (string player in playerNames) {
    // spin the dreidel
        switch (dreidelSpin())
        {
            case "\u05E0":
                Console.WriteLine($"\u05E0 nun"); //nun - use unicode to show hebrew nun
                Console.WriteLine("Nisht - do nothing");
                break;
            case "\u05D2":
                Console.WriteLine($"\u05D2 gimmel"); //gimmel - get the whole pot
                Console.WriteLine("Gantz ('everything') - get the whole pot.");
                playerScore += pot;
                break;
            case "\u05D4":
                Console.WriteLine($"\u05D4 hey"); // hey - get half the pot, or if pot is odd get half+1
                Console.WriteLine("Halb ('half') - get half the pot.");
                playerScore = pot % 2 == 0 ? playerScore += pot /2 : playerScore += 1 + pot /2;
                
                break;
            case "\u05E9":
                Console.WriteLine($"\u05E9 shin"); // shin - put one in the pot
                Console.WriteLine("Shtel ('put') - put one of your tokens in the pot.");
                playerScore --;
                pot ++;
                break;
        }

        Console.WriteLine(player, playerScore);
    }
}
string dreidelSpin() 
{
    Random random = new Random();
    string[] dreidelSides = {"\u05E0", "\u05D2", "\u05D4", "\u05E9"}; // nun, gimmel, hey, shin
    int integerDreidelSpin = random.Next(1, 4); // dreidelSides.Count +1);
    string hebrewLetterDreidelSpin = dreidelSides[integerDreidelSpin];
    return hebrewLetterDreidelSpin;
}