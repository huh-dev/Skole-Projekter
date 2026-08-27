global using Types;
using Microsoft.Extensions.DependencyInjection;

namespace Types
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Code setup
            // Code code = new Code();
            
            // var result = code.SearchWord(searchText, "type");
            // Console.WriteLine($"Found {result.WordToSearch} {result.Result} times");

            var searchText = @"C# is a strongly typed language. 
                Every variable and constant has a type, as does every expression that evaluates to a value. 
                Every method declaration specifies a name, the type and kind (value, reference, or output) 
                for each input parameter and for the return value.";

            // Inject setup
            var services = new ServiceCollection();
            services.AddScoped<Code>();
            var serviceProvider = services.BuildServiceProvider();
            var code = serviceProvider.GetRequiredService<Code>();

            var result = code.SearchWord(searchText, "type");
            Console.WriteLine($"Found {result.WordToSearch} {result.Result} times");
            
            // // Console interaction
            // string message = inject.InjectMessage("Hello, World!");
            // Console.WriteLine(message);
        }
    }
}
