using Howdy_Challenge.App;
using Howdy_Challenge.DataManagment;
using Howdy_Challenge.UserCommunication;

namespace Howdy_Challenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            HowdyChallengeApp app = new HowdyChallengeApp();
            /*
            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred. " +
                   "Exception message: " + ex.Message);
            }
            */
            app.Run();
        }
        //C:\Strasne super priecinok\c# .net\session.json
        //C:\Strasne super priecinok\bukovska - howdychallenge\answers.json
    }
}