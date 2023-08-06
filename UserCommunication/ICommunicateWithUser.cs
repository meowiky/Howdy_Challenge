namespace Howdy_Challenge.UserCommunication
{
    public interface ICommunicateWithUser
    {
        void ShowMessage(string message);
        string getJSONfile();

        string? getMenuAction();

        string? getUserAction();
    }
}