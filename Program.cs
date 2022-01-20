using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RestCSharp
{
    public class Program
    {
        //Making all functions that happens in main when user choose an menu option. 

        //This function sends the request to the class ClimbingClient and returns a boolean (true/false)
        static Boolean isLoggedIn(ClimbingClient client)
        {
            return client.CheckIfUserIsLoggedIn();
        }

        //This is an async function that sends users input to the function "login" in ClimbingClient. 
        static async Task login(ClimbingClient client)
        {
            Console.Write("Log in \n\n");
            Console.CursorVisible = true;
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            await client.Login(username, password);
        }
        //This function takes users input and send it into the function Register in class ClimbingClient. 
        static void register(ClimbingClient client)
        {
            Console.Write("Register\n\n");
            Console.CursorVisible = true;
            Console.Write("Full name: ");
            string fullname = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            client.Register(fullname, username, password);
        }
        //This fucnctipn activate the logoutUser fnction in class ClimbingClient
        static void logout(ClimbingClient client)
        {
            //return client.logoutUser();
            client.logoutUser();
        }

        //This is an async fucniton that calls the function getClimbingLoggs and store it into the variable routes. 
        //Then it checks if there is any routes/loggs to get. If no routes an error message is shown
        //If  there are routes/loggs then a foreach loop runns through and prints out a message together with the values from the logg. 
        static async Task getLoggs(ClimbingClient client)
        {
            var routes = await client.getClimbingLoggs();
            if (routes.Count == 0)
            {
                Console.WriteLine("There is no loggs to get");
            }
            else
            {
                foreach (ClimbingRoute route in routes)
                {
                    Console.WriteLine(route.typeOfRoute);
                    Console.WriteLine($"Route name: {route.name}\n");
                    Console.WriteLine($"Route grade: {route.grade}\n");
                    Console.WriteLine($"Route location: {route.location}\n");
                    Console.WriteLine($"Type of route: {route.typeOfRoute}\n");
                    Console.WriteLine($"Id: {route._id}\n");


                }
            }

        }
        //This function ask user for input and sends the input  into the function "addClimbinglog" i klassen ClimbingClient. 
        //If user is sucessfully stored, then a message is shown to the user, saying that logg is added. 
        static void addlog(ClimbingClient client)
        {

            Console.Write("Add a new logg\n\n");
            Console.CursorVisible = true;
            Console.Write("Name of the route: ");
            string name = Console.ReadLine();
            Console.Write("Grade: ");
            string grade = Console.ReadLine();
            Console.Write("Indoor or outdoor?: ");
            string location = Console.ReadLine();
            Console.Write("Sport, trad or bouldering?: ");
            string typeOfRoute = Console.ReadLine();

            client.addClimbinglog(grade, name, location, typeOfRoute);
            Console.WriteLine("\n\nLogg is added!\n\n");
        }
        //This fucntion ask user to enter the id of the log that the user want to delete. 
        //Then it takes the users input and send it into the function "deleteClimbingLog" i klassen ClimbingClient
        //If climbing log was sucessfully deleted, then the message "logg is deleted" will be shown on the sceen. 
        static void deleteLog(ClimbingClient client)
        {
            Console.Write("Delete log\n\n");
            Console.CursorVisible = true;
            Console.Write("Choose id of route to delete: ");
            string id = Console.ReadLine();

            client.deleteClimbingLog(id);
            Console.WriteLine("Logg is deleted!");
        }

        //This is the main program. 
        static void Main(string[] args)
        {
            //Making an instance of the class ClimbingClient. 
            var client = new ClimbingClient();
            //This always runns when nothing else runns. 
           do 
            {
                //Checking if user is logged in through the function "isLoggedIn" which returns true if user is logged in. 
                //The first menu is shown when user is not logged in. 
                if (isLoggedIn(client) == false)
                {
                    Console.WriteLine("MyLog\n\n");
                    Console.WriteLine("1. Log in\n");
                    Console.WriteLine("2. Register\n");
                    Console.WriteLine("X. Exit\n");
                    string inp = Console.ReadLine().ToLower();

                    //This switch is checking users input, and depending on the users input different cases will run. 
                    switch (inp)
                    {
                        case "1":
                            Console.CursorVisible = true;
                            login(client).Wait();
                            break;
                        case "2":
                            Console.CursorVisible = true;
                            register(client);
                            break;
                        case "x":
                            Console.CursorVisible = true;
                            Environment.Exit(0);
                            break;
                    }
                }
                //If user is logged in then this menu will be displayed and run instead. 
                else
                {

                    Console.WriteLine("MyLog\n\n");
                    Console.WriteLine("1. Log out\n");
                    Console.WriteLine("2. Get climbing loggs\n");
                    Console.WriteLine("3. Add a new climbing log\n");
                    Console.WriteLine("4. Delete climbing log\n");
                    Console.WriteLine("X. Exit\n");

                    string inp = Console.ReadLine().ToLower();
                    
                    //This switch is checking users input, and depending on the users input different cases will run. 
                    switch (inp)
                    {
                        case "1":
                            Console.CursorVisible = true;

                            logout(client);
                            break;
                        case "2":
                            Console.CursorVisible = true;

                            getLoggs(client).Wait();
                            break;
                        case "3":
                            Console.CursorVisible = true;

                            addlog(client);
                            break;
                        case "4":
                            Console.CursorVisible = true;

                            deleteLog(client);
                            break;
                        case "x":
                            Console.CursorVisible = true;

                            Environment.Exit(0);
                            break;
                    }
                }
            }while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}




