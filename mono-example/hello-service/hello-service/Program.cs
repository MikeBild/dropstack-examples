using System;
using System.IO;

using Mono.Unix;
using Mono.Unix.Native;

using Nancy.Hosting.Self;
using Nancy;

using NUnit.Framework;
using System.Linq;
using System.Diagnostics;

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
                if (Is_running_on_Mono) {
                    Console.WriteLine("Ctrl-C to stop service host");
                    UnixSignal.WaitAny(UnixTerminationSignals);
                }
                else {
                    Console.WriteLine("ENTER to stop service host");
                    Console.ReadLine();
                }
            }
        }


        private static bool Is_running_on_Mono => Type.GetType("Mono.Runtime") != null;

        private static UnixSignal[] UnixTerminationSignals => new[] {
            new UnixSignal(Signum.SIGINT),
            new UnixSignal(Signum.SIGTERM),
            new UnixSignal(Signum.SIGQUIT),
            new UnixSignal(Signum.SIGHUP)
        };
    }


    public class HelloService : NancyModule
    {
        public HelloService()
        {
            Get["/helloworld"] = _ => {
                Console.WriteLine("General greeting requested");
                return "Hello, World!";
            };

            Get["/hellome"] = _ => {
                var name = Request.Query["Name"];
                Console.WriteLine("Personalized greeting requested for {0}", name);
                return HelloLogic.Greet_person(name);
            };

            Post["/hellome"] = _ => {
			    var name = Request.Query["Name"];
			    Console.WriteLine("Advanced personalized greeting requested for {0}", name);
			    return HelloLogic.Greet_visitor(name);
            };

            Get["/provokeexception"] = _ => {
                throw new ApplicationException("Bye, bye from the hello service...");
            };

            Get["/provokeexit"] = _ => {
                Console.WriteLine("Exiting service upon request, but with a non-zero exit code...");
                Environment.Exit(-99);
                return ""; // will never be reached
            };

            Get["/version"] = _ =>
            {
                Console.WriteLine("Version inquired");

                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return $"Version: {fvi.FileVersion}";
            };
        }
    }


    public class HelloLogic {
        public static string Greet_person(string name) {
            return $"Hello, {name}!";
        }

        public static string Greet_visitor(string name) {
            const string VISIT_LOG = "visitors.txt";

            File.AppendAllLines(VISIT_LOG, new[]{name});

            var visits = File.ReadLines(VISIT_LOG);
            var numberOfVisitsBy = visits.Where(visitor => name == visitor).Count();

            var greeting = "";
            switch(numberOfVisitsBy) {
                case 1:
                    greeting = "Hello, {0}!";
                    break;
                case 2:
                    greeting = "Welcome back, {0}!";
                    break;
                default:
                    greeting = "Hello, my good friend {0}!";
                    break;
            }

            return string.Format(greeting, name);
        }
    }


    [TestFixture]
    public class test_HelloLogic {
        [SetUp]
        public void Setup() {
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
        }


        [Test]
        public void Greet_person() {
            var result = HelloLogic.Greet_person("Bruce");
            Assert.AreEqual("Hello, Bruce!", result);
        }


        [Test]
        public void Greet_visitor() {
            File.Delete("visitors.txt");

            var result = HelloLogic.Greet_visitor("Peter");
            Assert.AreEqual("Hello, Peter!", result);

            result = HelloLogic.Greet_visitor("Peter");
            Assert.AreEqual("Welcome back, Peter!", result);

            result = HelloLogic.Greet_visitor("Peter");
            Assert.AreEqual("Hello, my good friend Peter!", result);

            result = HelloLogic.Greet_visitor("Mary");
            Assert.AreEqual("Hello, Mary!", result);

            result = HelloLogic.Greet_visitor("Peter");
            Assert.AreEqual("Hello, my good friend Peter!", result);

            result = HelloLogic.Greet_visitor("Mary");
            Assert.AreEqual("Welcome back, Mary!", result);
        }
    }
}