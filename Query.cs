﻿namespace GatewayGraph
{
    public class Query
    {
        public Greetings GetGreetings() => new Greetings();
    }

    public class Greetings
    {
        public string Hello() => "World";
    }
}
