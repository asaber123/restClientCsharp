using System;
namespace RestCSharp
{
        //This is a class for values that is required for adding a climbing log requests

    public class ClimbingRoute
    {
        public String grade { get; set; }
        public String name { get; set; }
        public String location { get; set; }
        public String typeOfRoute { get; set; }
        public String user { get; set; }

        public String date { get; set; }
        public String _id{get;set;}

    }
}