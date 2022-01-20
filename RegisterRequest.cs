using System;
namespace RestCSharp
{
        //This is a class for values that is required for registtration requests

    public class RegisterRequest
    {
        public String fullName { get; set; }
        public String userName { get; set; }
        public String password { get; set; }
    }
}