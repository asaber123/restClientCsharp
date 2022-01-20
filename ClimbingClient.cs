
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
namespace RestCSharp
{
    //This class is made for the http client requests. The package Microsoft.AspNet.WebApi.Client has been used to make http requests. 
    public class ClimbingClient
    {
        //Making an instance of httpClient which then will run through the whole application. 
        HttpClient httpClient;

        //This is the constructor that runns every time the class will be called. 
        //Here base url is set as well as default headers and acceptance of headers is to be set. 
        public ClimbingClient()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://mysessionlogapi.herokuapp.com/");
            //Clear the response headers that are sent in with every request. 
            httpClient.DefaultRequestHeaders.Accept.Clear();
            //Tells that we want to return in json format. 
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //This funciton is async and returns a list. 
        //First it checks if teh authorization header is set, otherwize error message will be shown. 
        //If header exists then a get request will be sent to the rest-api and store the response in a variable
        //The variable will then be read asyns to then deserialize into a list. 
        public async Task<List<ClimbingRoute>> getClimbingLoggs()
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
                return new List<ClimbingRoute>();
            }
            var routeResponse = await httpClient.GetAsync("api/");
            var routeResponseJson = routeResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ClimbingRoute>>(routeResponseJson.Result);
        }
        //This fucniton takes the arguments, required to add a climbing log.
        //An class with varibales with setters and getters have been crated as a modle of required input. This will then be used to store the data
        //First it checks if the authorization header is sent with the request. If not, error message will be shwon. 
        //Else a new instance of the class "ClimbingRoute" will be made. The values will there be stored. This now class object will be sent as json format to the api. 
        //Depending if the log was scessfully stored, messages will be shown to user. 
        public async void addClimbinglog(String grade, String name, String location, String typeOfRoute)
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
            }
            var climbingLogRequest = new ClimbingRoute();
            climbingLogRequest.grade = grade;
            climbingLogRequest.name = name;
            climbingLogRequest.location = location;
            climbingLogRequest.typeOfRoute = typeOfRoute;
            HttpResponseMessage climbingLogResponse = await httpClient.PostAsJsonAsync("api/", climbingLogRequest);
            if (!climbingLogResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to add logg, try again");

            }

        }
        //This function deletes a climbing log and takes an id as argument. 
        //It first chekcs if authorization header is present, if not an error message will be shown. 
        //If header exists, then a http request with methos delete  will be sent to the api together with id. 
        //An if-sats is made to check if the response status was sucessfull, if not an error message will be shown, if it is then the message "log is deleted" will be shwon. 
        public async void deleteClimbingLog(string id)
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
            }
            var climbingLogRequest = new ClimbingRoute();
            HttpResponseMessage climbingLogResponse = await httpClient.DeleteAsync($"api/{id}");
            if (!climbingLogResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Sorry, failed to delete logg. Try again");

            }
            else
            {
                Console.WriteLine("Log deleted!");
            }


        }
        //This function logg in a user
        //An class with values that is requied for the server is made called "LoginRequest"
        //An instance of the class is made that then stores the data that has been send to the funciton. 
        //The data willl then be sent as a http request with method post and format json to the api. 
        //If the response of the request is sucessfull then an authentification token will be created. 
        //It stores the value from the send back header "auth-token". Then it will be stored as a default header that will be sent in every futher request. 
        public async Task Login(String username, String password)
        {
            var loginRequest = new LoginRequest();
            loginRequest.userName = username;
            loginRequest.password = password;
            HttpResponseMessage loginResponse = await httpClient.PostAsJsonAsync("auth/login", loginRequest);
            var authToken = "";
            if (loginResponse.IsSuccessStatusCode)
            {
                authToken = loginResponse.Headers.GetValues("auth-token").FirstOrDefault();
                //Set auth token to client so that we are authorized from now on.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                Console.WriteLine("\n\nYou logged in sucessfully! Press enter to come to new menu\n\n");
            }
            else
            {
                Console.WriteLine("\n\nSorry, you failed to log in, try again\n\n");
            }
        }
        //This function loggs out the user. It then sets the default authorixation header to null, 
        //wich signalate that the user is logged in and no more requests can be made that requires a token. 
        public async void logoutUser()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            Console.WriteLine("\n\n You logged out, press enter to go to mennu\n\n");
        }
        //This function register a new user. A new class has been made "RegisterRequest" that stores the values that is required for the registration request
        //An instance of the class is made and then the values from the arguments in the functions is set into the class. 
        //The data is then sent as a post request with the format of json to the api. 
        public async void Register(String fullname, String username, String password)
        {
            var registerRequest = new RegisterRequest();
            registerRequest.fullName = fullname;
            registerRequest.userName = username;
            registerRequest.password = password;
            HttpResponseMessage registerResponse = await httpClient.PostAsJsonAsync("auth/signup", registerRequest);
            if (registerResponse.IsSuccessStatusCode == false)
            {
                Console.WriteLine("\n\nSorry The registration could not proceed. All required input must be set,\n username and password must contain at least 6 characters!\n");
            }
            else
            {
                Console.WriteLine("\n\nThe registration was sucessfull\n\n");

            }
        }

        //This fucntion is to check if user is logged in. It returns a boolean (true/false)
        //It checks if authentification header is set, if it is then the funcuiton will return true, if not it will return false. 
        public Boolean CheckIfUserIsLoggedIn()
        {
            if (httpClient.DefaultRequestHeaders.Authorization != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}