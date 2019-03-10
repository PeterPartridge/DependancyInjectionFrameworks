using HelloWorld.Interface;

namespace HelloWorld.Class
{
    public class Speak : IHelloWorld
    {
        public string SayHello()
        {
            return "Hello World";
        }
    }
}
