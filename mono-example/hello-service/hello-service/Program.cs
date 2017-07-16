using System;

using Nancy.Hosting.Self;
using Nancy;

using NUnit.Framework;


namespace helloservice
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var endpointAddress = args[0];
            using (var host = new NancyHost(new Uri(endpointAddress))) {
                host.Start();

                Console.WriteLine("Running on {0}...", endpointAddress);
                Console.WriteLine("ENTER to stop service");
                Console.ReadLine();
            }
        }
    }


    public class HelloService : NancyModule
    {
        public HelloService()
        {
            Get["/hello"] = _ => {
                Console.WriteLine("General greeting requested");
                return "Hello, World!";
            };

            Post["/hello"] = _ =>
            {
                var name = Request.Query["Name"];
                Console.WriteLine("Personalized greeting requested for {0}", name);
                return HelloLogic.Greet_person(name);
            };
        }
    }


    public class HelloLogic {
        public static string Greet_person(string name) {
            return $"Hello, {name}!";
        }
    }


    [TestFixture]
    public class test_HelloLogic {
        [Test]
        public void Greet_person() {
            var result = HelloLogic.Greet_person("Bruce");
            Assert.AreEqual("Hello, Bruce!", result);
        }
    }
}
