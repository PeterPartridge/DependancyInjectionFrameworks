using WindsorDependancyInjectionFrameworks.Interface;
using System.Diagnostics;

namespace WindsorDependancyInjectionFrameworks.Class
{
    public class HelloWorld : IHelloWorld
    {
        public string SayHello()
        {
            return "Hello World";
        }
    }
}
