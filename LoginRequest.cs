using System;
namespace RestCSharp
{
    //This is a class for values that is required for login requests
    public class LoginRequest
    {
        public String userName { get; set; }
        public String password { get; set; }
    }
}
