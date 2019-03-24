using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RougeLikeCSharp
{
    class Program
    {
        //Consol Colors:
        //-BackgroundColor = rgb(39, 33, 77)
        //-ForegroundColor = rgb(212, 36, 80)

        //Booleans
        bool firstBoot = true;
        bool ClassChoosen = false;

        //Strings
        string playerName;
        string playerClass;

        string mapFile;                                                                                         //Change to support multiple levels.
        string itemFile;
        //string itemFileLine;
        string filePathMap = "C:\\Users\\Tonnes\\source\\repos\\RougeLikeCSharp\\RougeLikeCSharp\\Maps\\map1.txt";
        string filePathItems = "C:\\Users\\Tonnes\\source\\repos\\RougeLikeCSharp\\RougeLikeCSharp\\Items\\items1.txt";       

        //Integers
        readonly int width = 100;
        readonly int height = 30;

        int playerClassSelection = 0;

        int playerX, playerY;

        //TODO: Array instead of hundreds of variables
        int playerHealth = 100;
        int playerHealthMax = 100;
        int playerMana = 10;
        int playerManaMax = 10;
        int playerExp = 0;
        int playerExpTotal = 100;
        int playerExpBonus = 0;
        int playerAttack = 30;
        int playerLevel = 1;

        int playerHealed = 0;

        //END TODO

        //TODO: Array instead of hundreds of variables
        int itemHealthLargeHealthAdd = 10;


        //END TODO

        //ai
        int viewDistance = 3;
        int enemyAttack = 15;
        int randomAiAttack;

        //other
        int gameState = 0; //Implement so that we can have other screens or something like that.
        int mapLevel = 0;

        //int animationAttackPause = 75;

        //Arrays, Lists
        List<Wall> walls;
        List<Tile> tiles;
        List<Item> items;
        List<Item> inventory;
        List<Classes> classes;

        //Doubles
        double playerExpMax;

        //Others
        ConsoleKeyInfo PressedKey;
        StreamReader streamReader;

        Random random;

        static Program program = new Program();

        Program()
        {
            //Getting console ready.
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
           
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            //Map
            walls = new List<Wall>();
            tiles = new List<Tile>();

            //Items
            items = new List<Item>();
            inventory = new List<Item>(); //Should probobly refer to item list instead of creating a new object of the exact same values.

            //Classes
            classes = new List<Classes>();

            //Player
            //createPlayer() //Somehow
            playerClassSelection = 0;

            //TODO: Make more dynamic, so that it can change by level.
            tiles.Add(new Tile {
                tileSpawn = 'S',
                tileWall = '#',
                tileHealth = 'H',
                tileEnemy = 'X',
                tileNewLine = '\r',
                tilePlayer = '@',
                /*tileAttack_Up = '^',
                tileAttack_Down = 'v',
                tileAttack_Left = '<',
                tileAttack_Right = '>',*/
                tileErase = " ",
                tileNothing = ' '
            });
            //END TODO

            //TODO: Create a more dynamic class system
            classes.Add(new Classes { className = "Knight", classHealthBonus = 30, classManaBonus = 0, classExpBonus = 0, classAttackBonus = 0 });
            classes.Add(new Classes { className = "Rouge ", classHealthBonus = 0, classManaBonus = 0, classExpBonus = 10, classAttackBonus = 20 });
            classes.Add(new Classes { className = "Wizard", classHealthBonus = -10, classManaBonus = 30, classExpBonus = 0, classAttackBonus = 10 });
            //END TODO

            //Reads map from file.
            streamReader = new StreamReader(filePathMap);
            mapFile = streamReader.ReadToEnd();            

            //Reads items from file.
            streamReader = new StreamReader(filePathItems);
            itemFile = streamReader.ReadToEnd();

            getItems(itemFile);

            //Random generator
            random = new Random();
        }

        static void Main(string[] args)
        {
            program.GameLoop();
        }

        //Assigns player-values to player
        void createPlayer(string pName, string pClass)
        {
            //Strings
            playerName = pName; //Needs to be changed to an input.

            //Integers
            playerHealth += classes[playerClassSelection].classHealthBonus;
            playerHealthMax += classes[playerClassSelection].classHealthBonus;
            playerMana += classes[playerClassSelection].classManaBonus;
            playerManaMax += classes[playerClassSelection].classManaBonus;
            playerExpBonus += classes[playerClassSelection].classExpBonus;
            playerAttack += classes[playerClassSelection].classAttackBonus;

            //Default spawing location if none other spesefied.
            playerX = width / 2;
            playerY = height / 2;

            //Doubles
            playerExpMax = 100;

            //DELETE
            //classes[0].className = "Knight";
            //classes[0].classHealthBonus = 0;
            //classes[0].classManaBonus = 0;
            //classes[0].classExpBonus = 0;
            //classes[0].classAttackBonus = 0;
            //classes[1].className = "Rouge";
            //classes[1].classHealthBonus = 0;
            //classes[1].classManaBonus = 0;
            //classes[1].classExpBonus = 0;
            //classes[1].classAttackBonus = 0;
            //classes[2].className = "Wizard";
            //classes[2].classHealthBonus = 0;
            //classes[2].classManaBonus = 0;
            //classes[2].classExpBonus = 0;
            //classes[2].classAttackBonus = 0;
            //classes[3].className = "Witch";
            //classes[3].classHealthBonus = 0;
            //classes[3].classManaBonus = 0;
            //classes[3].classExpBonus = 0;
            //classes[3].classAttackBonus = 0;
            //DELETE

            //for (int i = 0; i < classes.Length; i++)
            //{
            //    if (pClass == classes[i].className)
            //    {
            //        playerClass = pClass;
            //    }
            //}
        }

        void getItems(string item)
        {
            //Implement
        }

        void drawInventory()
        {
            //Implement
        }

        //Get map from string and translate it to a list.
        void createMap(string map)
        {
            int i = 0;
            int k = 0;
            for (int j = 0; j < map.Length; j++)
            {
                if (k <= width - 1 && i <= height - 4) {
                    k++;
                    if (map[j] == tiles[mapLevel].tileNewLine)
                    {
                        i++;
                        k = 0;
                    }
                    if (map[j] == tiles[mapLevel].tileWall)
                    {
                        walls.Add(new Wall { wallChar = map[j], cordX = k, cordY = i + 3, color = ConsoleColor.Gray }); // +3 so that it wont be under the menu.
                    }
                    else if (map[j] == tiles[mapLevel].tileHealth)
                    {
                        walls.Add(new Wall { wallChar = map[j], cordX = k, cordY = i + 3, color = ConsoleColor.Blue });
                    }
                    else if (map[j] == tiles[mapLevel].tileEnemy)
                    {
                        walls.Add(new Wall { wallChar = map[j], cordX = k, cordY = i + 3, color = ConsoleColor.Red });
                    }
                    else if (map[j] == tiles[mapLevel].tileSpawn)
                    {
                        walls.Add(new Wall { wallChar = map[j], cordX = k, cordY = i + 3, color = ConsoleColor.Yellow });
                    }
                    else if (map[j] == tiles[mapLevel].tileNothing)
                    {
                        walls.Add(new Wall { wallChar = map[j], cordX = k, cordY = i + 3, color = ConsoleColor.White });
                    }
                }
            }
        }

        //Writes the map from list to console.
        void writeMap()
        {
            for (int i = 0; i < walls.Count; i++)
            {          
                if (walls[i].wallChar != tiles[mapLevel].tileSpawn)
                {
                    Console.ForegroundColor = walls[i].color;
                    Console.SetCursorPosition(walls[i].cordX, walls[i].cordY);
                    Console.Write(walls[i].wallChar);
                }
                else
                {
                    playerX = walls[i].cordX;
                    playerY = walls[i].cordY;
                }
            }
            Console.ResetColor();
        }

        /**
         * Clearing of cordinates, used for clearing and updating the status bar
         * x = X cord
         * y = Y cord
         * lineheight = number of lines
         * lineWidth = Line length
         */
        void clearCords(int x, int y, int lineHeight, int lineWidth)
        {
            if (lineHeight == 0 && lineWidth == 0)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(tiles[mapLevel].tileNothing);
            }
            else if (lineHeight >= 1 || lineWidth >= 1)
            {
                for (int i = 0; i < lineHeight; i++)
                {
                    for (int j = 0; j < lineWidth; j++)
                    {                      
                        Console.SetCursorPosition(x + j, y + i);
                        Console.Write(tiles[mapLevel].tileNothing);
                    }
                }
            }
        }

        //Test if there is a wall where you are going, or not.
        bool canWalk(int cord_X, int cord_Y, int direction, bool ai)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (!ai)
                {
                    if (walls[i].wallChar == tiles[mapLevel].tileWall)
                    {
                        if (direction == 0) //W, Up
                        {
                            if (walls[i].cordY == cord_Y - 1 && walls[i].cordX == cord_X)
                            {
                                return false;
                            }
                        }
                        if (direction == 1) //S, Down
                        {
                            if (walls[i].cordY == cord_Y + 1 && walls[i].cordX == cord_X)
                            {
                                return false;
                            }
                        }
                        if (direction == 2) //A, Left
                        {
                            if (walls[i].cordX == cord_X - 1 && walls[i].cordY == cord_Y)
                            {
                                return false;
                            }
                        }
                        if (direction == 3) //D, Right
                        {
                            if (walls[i].cordX == cord_X + 1 && walls[i].cordY == cord_Y)
                            {
                                return false;
                            }
                        }
                    }

                    // Pickups and other tiles
                    // +10 More health
                    if (walls[i].wallChar == tiles[mapLevel].tileHealth)
                    {
                        if (walls[i].cordX == cord_X && walls[i].cordY == cord_Y)
                        {
                            //Healing, extra health, and checking for how much you healed.
                            playerHealthMax += itemHealthLargeHealthAdd;
                            playerHealed = playerHealthMax - playerHealth;
                            playerHealth = playerHealthMax;

                            //Delete item from ground at pickup
                            walls[i].wallChar = tiles[mapLevel].tileNothing;

                            //Using 'direction' for an extra integer.
                            writeBottomText(2, "You healed", itemHealthLargeHealthAdd, playerHealed);
                        }
                    }
                    //Implement better enemies
                    if (walls[i].wallChar == tiles[mapLevel].tileEnemy)
                    {
                        if (walls[i].cordX == cord_X && walls[i].cordY == cord_Y)
                        {
                            randomAiAttack = random.Next(Convert.ToInt32(Math.Round(enemyAttack / 1.5)), enemyAttack);
                            playerHealth -= randomAiAttack;                               
                            playerExp += 10 + playerExpBonus;
                            walls[i].wallChar = tiles[mapLevel].tileNothing;

                            writeBottomText(1, "You where attacked by an enemy for", 0, randomAiAttack);
                        }
                    }
                    //BUGGED
                    ////Text
                    //if (walls[i].wallChar == tiles[mapLevel].tileNothing)
                    //{
                    //    if (walls[i].cordX == cord_X && walls[i].cordY == cord_Y)
                    //    {
                    //        writeBottomText(0, "You walked", direction, 0);
                    //    }
                    //}
                    //END BUGGED
                }
                else
                {
                    if ((walls[i].wallChar == tiles[mapLevel].tileWall) || (walls[i].wallChar == tiles[mapLevel].tileEnemy))
                    {
                        if (direction == 0) //W, Up
                        {
                            if (walls[i].cordY == cord_Y + 1 && walls[i].cordX == cord_X)
                            {
                                return false;
                            }
                        }
                        if (direction == 1) //S, Down
                        {
                            if (walls[i].cordY == cord_Y - 1 && walls[i].cordX == cord_X)
                            {
                                return false;
                            }
                        }
                        if (direction == 2) //A, Left
                        {
                            if (walls[i].cordX == cord_X + 1 && walls[i].cordY == cord_Y)
                            {
                                return false;
                            }
                        }
                        if (direction == 3) //D, Right
                        {
                            if (walls[i].cordX == cord_X - 1 && walls[i].cordY == cord_Y)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        //We need to add another parameter for every type of tile. Not a good way of doing things i say.
        void writeBottomText(int type, string text, int direction, int extra)
        {
            //Types
            //0 = nothing
            //1 = enemy
            //2 = health
            //add the rest...

            clearCords(0, height - 1, 1, width - 1);
            Console.SetCursorPosition(0, height - 1);

            //type 0 is not currently in use, because of a bug.
            if (type == 0)
            {
                switch (direction)
                {
                    case 0:
                        Console.Write(text + " north.");
                        break;
                    case 1:
                        Console.Write(text + " south.");
                        break;
                    case 2:
                        Console.Write(text + " west.");
                        break;
                    case 3:
                        Console.Write(text + " east.");
                        break;
                    default:
                        Console.Write("You don't quite know what direction you went in...");
                        break;
                }
            }
            else if (type == 1)
            {
                Console.Write(text + " " + extra + " health.");
            }
            else if (type == 2)
            {
                Console.Write(text + " " + extra + " health, and gained " + direction + " additional health.");
            }
            else if (type == 3)
            {
                Console.Write(text + " " + extra + ". You gained 5(" + direction + ") attack.");
            }
            else
            {
                Console.Write("Eh? What went wrong?");
            }
        }

        void ai(List<Wall> aiMap, int playerX_ai, int playerY_ai)
        {
            int direction_ai_y = 0;
            int direction_ai_x = 0;

            for (int i = 0; i < aiMap.Count; i++)
            {
                //number = ai view distance
                if (proximityDetector(playerX_ai, playerY_ai, aiMap[i].cordX, aiMap[i].cordY, viewDistance)) {
                    //Add test for if there are any other AIs in the path
                    if (aiMap[i].wallChar == 'X')
                    {
                        //Choosing direction for ai to walk
                        // 0 up, 1 down, 2 left, 3 right
                        if (aiMap[i].cordY < playerY_ai)
                        {
                            direction_ai_y = 0; //up
                        }
                        else if (aiMap[i].cordY > playerY_ai)
                        {
                            direction_ai_y = 1; //down
                        }
                        if (aiMap[i].cordX < playerX_ai)
                        {
                            direction_ai_x = 2; //left
                        }
                        else if (aiMap[i].cordX > playerX_ai)
                        {
                            direction_ai_x = 3; //right
                        }

                        //Testing if the ai can walk the direction + walking
                        //The bot can walk twice, once y, and once x.
                        //up down
                        if (canWalk(aiMap[i].cordX, aiMap[i].cordY, direction_ai_y, true))
                        {
                            if (aiMap[i].cordY != playerY_ai)
                            {
                                if (direction_ai_y == 0)
                                {
                                    aiMap[i].cordY++;

                                    Console.SetCursorPosition(aiMap[i].cordX, aiMap[i].cordY - 1);
                                    Console.Write(tiles[mapLevel].tileNothing);
                                }
                                else if (direction_ai_y == 1)
                                {
                                    aiMap[i].cordY--;

                                    Console.SetCursorPosition(aiMap[i].cordX, aiMap[i].cordY + 1);
                                    Console.Write(tiles[mapLevel].tileNothing);
                                }
                            }
                        }
                        //left right
                        if (canWalk(aiMap[i].cordX, aiMap[i].cordY, direction_ai_x, true))
                        {
                            if (aiMap[i].cordX != playerX_ai)
                            {
                                if (direction_ai_x == 2)
                                {
                                    aiMap[i].cordX++;

                                    Console.SetCursorPosition(aiMap[i].cordX - 1, aiMap[i].cordY);
                                    Console.Write(tiles[mapLevel].tileNothing);
                                }
                                else if (direction_ai_x == 3)
                                {
                                    aiMap[i].cordX--;

                                    Console.SetCursorPosition(aiMap[i].cordX + 1, aiMap[i].cordY);
                                    Console.Write(tiles[mapLevel].tileNothing);
                                }
                            }
                        }

                        //Updates enemies
                        Console.ForegroundColor = aiMap[i].color;
                        Console.SetCursorPosition(aiMap[i].cordX, aiMap[i].cordY);
                        Console.Write(aiMap[i].wallChar);
                        Console.ResetColor();
                    }
                }
            }
        }

        //Used for proximity detection (if for example an enemy is 5 tiles away from the player)
        bool proximityDetector(int playerX_ai, int playerY_ai, int aiX, int aiY, int aiViewDistance)
        {
            if (((playerX_ai - (aiX + 2)) <= aiViewDistance) && ((playerY_ai - aiY) <= aiViewDistance) && ((playerX_ai - (aiX - 2)) >= -aiViewDistance) && ((playerY_ai - aiY) >= -aiViewDistance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        void classMenuButtons(playerClassSelection)
        {
            if (playerClassSelection == 0)
            {
                Console.SetCursorPosition(0, 2);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(classes[1].className);
                Console.SetCursorPosition(0, 3);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(classes[2].className);

                Console.SetCursorPosition(0, 1);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Write(classes[0].className);
            }
        }

        //NEEDS REWORK!!!!!!
        //GameOver
        void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition((width / 2) - 1, (height / 2) - 1);
            Console.Write("GAMEOVER");
            Console.ReadKey();
            Quit();
            //createPlayer(playerName, playerClass);
            //gameState = 0;
            //GameLoop();
        }
        
        //Quits the console. REWORK?
        void Quit()
        {
            Environment.Exit(1);         
        }

        //The main game loop.
        void GameLoop()
        {
            Console.Clear();

            //Getting player information
            if (firstBoot) {
                Console.CursorVisible = true;


                //Naming the character
                Console.Write("What is your name?\n");
                playerName = Console.ReadLine();

                //Clearing screen and hiding cursor
                Console.Clear();
                Console.CursorVisible = false;

                Console.SetCursorPosition(1, height - 1);
                Console.Write("Use up and down arrows, then press enter to choose class.");
                Console.SetCursorPosition(0, 0);

                //Selecting character class
                Console.Write("Choose a class:\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Write(classes[0].className);

                Console.BackgroundColor = ConsoleColor.Gray;               
                Console.Write("\n{0}\n{1}", classes[1].className, classes[2].className);


                Console.SetCursorPosition(0, 1);

                int curPos = 1;
                do {
                    //Choosing class menu controls
                    PressedKey = Console.ReadKey(true);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    switch (PressedKey.Key)
                    {
                        //Min 1, Max 3
                        case ConsoleKey.UpArrow:
                            if (curPos == 2 || curPos == 3)
                            {
                                curPos--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (curPos == 1 || curPos == 2)
                            {
                                curPos++;
                            }
                            break;
                        case ConsoleKey.Enter:
                            ClassChoosen = true;
                            break;
                        default:
                            break;
                    }
                    playerClassSelection = curPos - 1;

                    //Changing color buttons
                    if (playerClassSelection == 0)
                    {
                        Console.SetCursorPosition(0, 2);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[1].className);
                        Console.SetCursorPosition(0, 3);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[2].className);

                        Console.SetCursorPosition(0, 1);
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(classes[0].className);
                    }
                    else if (playerClassSelection == 1)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[0].className);
                        Console.SetCursorPosition(0, 3);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[2].className);

                        Console.SetCursorPosition(0, 2);
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(classes[1].className);
                    }
                    else if (playerClassSelection == 2)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[0].className);
                        Console.SetCursorPosition(0, 2);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(classes[1].className);

                        Console.SetCursorPosition(0, 3);
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(classes[2].className);
                    }

                    Console.SetCursorPosition(0, curPos);
                } while (!ClassChoosen);
                

                playerClass = classes[playerClassSelection].className;
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.SetCursorPosition(10, 10);

                Console.Write("You have choosen: {0}", playerClass);                                             
                Console.ReadKey();
                //playerClass = Console.ReadLine().ToUpper();


                createPlayer(playerName, playerClass);

                //Reseting coloring and terminal text
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                //NEEDS REWORK!!!
                firstBoot = false; //Does not need to re-enter information after gameover.
            }

            Console.SetCursorPosition(0,2);
            //Writes a line.
            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("-");
            }

            //Creates map
            createMap(mapFile);
            //Writes the map once.
            writeMap();

            do
            {
                clearCords(0, 0, 2, width);

                //Leveling up
                if (playerExp >= playerExpMax)
                {
                    playerLevel++;
                    playerExp = 0;
                    playerAttack += 5;
                    playerExpMax = Math.Round(playerExpMax * 1.2);
                    playerExpTotal += playerExpTotal;

                    writeBottomText(3, "You level up to level", playerAttack, playerLevel);
                }

                if (playerHealth <= 0)
                {
                    gameState = 1;
                    GameOver();
                }

                //Print player information
                Console.SetCursorPosition(0, 0);
                Console.Write("Name: {0}  Class: {1}\nHealth: {2}/{3} Level: {4}  Attack: {5}  Exp: {6}/{7}(bonus: {8})", playerName, playerClass, playerHealth, playerHealthMax, playerLevel, playerAttack, playerExp, playerExpMax, playerExpBonus);

                //Stops CPU from melting, maybe.
                Thread.Sleep(10);

                //Writes the player.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(tiles[mapLevel].tilePlayer);
                Console.ResetColor();

                //Handles the keypresses from the player.
                PressedKey = Console.ReadKey(true);
                switch (PressedKey.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (canWalk(playerX, playerY, 0, false))
                        {
                            //if (PressedKey.Modifiers == ConsoleModifiers.Shift)
                            //{
                            //    //Attack animation UP
                            //    if (playerY >= 4)
                            //    {
                            //        Console.SetCursorPosition(playerX, playerY - 1);
                            //        Console.Write(tiles[mapLevel].tileAttack_Up);
                            //        Thread.Sleep(animationAttackPause);
                            //        Console.SetCursorPosition(playerX, playerY - 1);
                            //        Console.Write(tiles[mapLevel].tileErase);
                            //    }
                            //}
                            //else
                            //{
                            playerY -= 1;
                            Console.SetCursorPosition(playerX, playerY + 1);
                            Console.Write(tiles[mapLevel].tileNothing);
                            //}
                        }
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (canWalk(playerX, playerY, 2, false))
                        {
                            //if (PressedKey.Modifiers == ConsoleModifiers.Shift)
                            //{
                            //    //Attack animation LEFT
                            //    if (true) //ADD LEFT EDGE BLOCK
                            //    {
                            //        Console.SetCursorPosition(playerX - 1, playerY);
                            //        Console.Write(tiles[mapLevel].tileAttack_Left);
                            //        Thread.Sleep(animationAttackPause);
                            //        Console.SetCursorPosition(playerX - 1, playerY);
                            //        Console.Write(tiles[mapLevel].tileErase);
                            //    }
                            //}
                            //else
                            //{
                            playerX -= 1;
                            Console.SetCursorPosition(playerX + 1, playerY);
                            Console.Write(tiles[mapLevel].tileNothing);
                            //}
                        }
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (canWalk(playerX, playerY, 3, false))
                        {
                            //if (PressedKey.Modifiers == ConsoleModifiers.Shift)
                            //{
                            //    //Attack animation RIGHT
                            //    if (true) //ADD RIGHT EDGE BLOCK
                            //    {
                            //        Console.SetCursorPosition(playerX + 1, playerY);
                            //        Console.Write(tiles[mapLevel].tileAttack_Right);
                            //        Thread.Sleep(animationAttackPause);
                            //        Console.SetCursorPosition(playerX + 1, playerY);
                            //        Console.Write(tiles[mapLevel].tileErase);
                            //    }
                            //}
                            //else
                            //{
                            playerX += 1;
                            Console.SetCursorPosition(playerX - 1, playerY);
                            Console.Write(tiles[mapLevel].tileNothing);
                            //}
                        }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (canWalk(playerX, playerY, 1, false))
                        {
                            //if (PressedKey.Modifiers == ConsoleModifiers.Shift)
                            //{
                            //    //Attack animation DOWN
                            //    if (true) //ADD BOTTOM EDGE BLOCK
                            //    {
                            //        Console.SetCursorPosition(playerX, playerY + 1);
                            //        Console.Write(tiles[mapLevel].tileAttack_Down);
                            //        Thread.Sleep(animationAttackPause);
                            //        Console.SetCursorPosition(playerX, playerY + 1);
                            //        Console.Write(tiles[mapLevel].tileErase);
                            //    }
                            //}
                            //else
                            //{
                            playerY += 1;
                            Console.SetCursorPosition(playerX, playerY - 1);
                            Console.Write(tiles[mapLevel].tileNothing);
                            //}
                        }
                        break;
                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        Quit();
                        break;
                }

                //Stops player from moving out of bounds(map).
                if (playerY == 2)
                {
                    playerY += 1;
                }
                if (playerY == height)
                {
                    playerY = height - 1;
                }

                if (playerX == -1)
                {
                    playerX += 1;
                }
                if (playerX == width)
                {
                    playerX = width - 1;
                }

                //ai
                ai(walls, playerX, playerY);

            } while (gameState == 0);
        }                  
    }
}