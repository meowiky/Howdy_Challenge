using Howdy_Challenge.DataManagment;

namespace Howdy_Challenge.UserCommunication
{
    public class ConsoleCommunication : ICommunicateWithUser
    {

        public ConsoleCommunication()
        {
            
        }
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string getJSONfile()
        {
            Console.Write("Enter the path to the JSON file: ");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllText(filePath);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading or parsing JSON: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            return null;
        }

        public string? getMenuAction()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[A] add new session by json file");
            Console.WriteLine("[E] do new evaluation for specific month");
            Console.WriteLine("[S] search for evaluation by Group ID and specific month and year");
            Console.WriteLine("[X] exit");

            var userChoice = Console.ReadLine();
            return userChoice;
        }

        public string? getUserAction()
        {
            var userChoice = Console.ReadLine();
            return userChoice;
        }
    }
}
