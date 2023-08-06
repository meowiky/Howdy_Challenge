

using Howdy_Challenge.DataManagment;
using Howdy_Challenge.UserCommunication;
using System.Text.RegularExpressions;

namespace Howdy_Challenge.App
{
    public class HowdyChallengeApp
    {
        private ICommunicateWithUser _communicateWithUser;
        private DataHandler _dataHandler;
        public HowdyChallengeApp() 
        {
             _communicateWithUser = new ConsoleCommunication();
            _dataHandler = new DataHandler(_communicateWithUser);
        }
        public void Run() 
        {
            bool shallExit = false;
            while (!shallExit)
            {
                var userAction = _communicateWithUser.getMenuAction();

                switch (userAction)
                {
                    case "X":
                    case "x":
                        shallExit = true;
                        break;
                    case "A":
                    case "a":
                        var x = _communicateWithUser.getJSONfile();
                        if(x != null)
                        {
                            _dataHandler.AddingNewSessionFromjson(x);
                        }
                        break;
                    case "S":
                    case "s":
                        Search();
                        break;
                    case "E":
                    case "e":
                        Evaluation();
                        break;
                    default:
                        _communicateWithUser.ShowMessage("Invalid choice");
                        break;
                }
            }
            _dataHandler.SerializeAllGroup();
        }

        private void Evaluation()
        {
            _communicateWithUser.ShowMessage("Input group ID, Month in number and year divided by enter");
            var a = _communicateWithUser.getUserAction();
            var b = _communicateWithUser.getUserAction();
            var c = _communicateWithUser.getUserAction();
            if ((int.TryParse(a, out int groupid)) && (int.TryParse(b, out int month)) && (int.TryParse(c, out int year)))
            {
                Howdy_Challenge.Models.Group group = _dataHandler.searchGroupId(groupid);
                if (group != null)
                {
                    double? evaluation = group.MonthEvaluation(year, month);
                    if (evaluation != null)
                    {
                        _communicateWithUser.ShowMessage($"Evaluation was recorded with value {evaluation}");
                    }
                }
                else
                {
                    _communicateWithUser.ShowMessage($"Group with ID {groupid} doesnt exist");
                }
            }
            else
            {
                _communicateWithUser.ShowMessage("The input/'s is/are not a valid number/'s.");
            }
        }

        private void Search()
        {
            _communicateWithUser.ShowMessage("Input group ID, Month in number and year divided by enter");
            var m = _communicateWithUser.getUserAction();
            var n = _communicateWithUser.getUserAction();
            var o = _communicateWithUser.getUserAction();
            if ((int.TryParse(m, out int groupid)) && (int.TryParse(n, out int month)) && (int.TryParse(o, out int year)))
            {
                Howdy_Challenge.Models.Group group = _dataHandler.searchGroupId(groupid);
                if (group != null)
                {
                    double? result = group.searchForEvaluation(year, month);
                    if(result != null)
                    {
                        _communicateWithUser.ShowMessage($"Group with ID {groupid} have Evaluation value for {result}");
                    }
                    else
                    {
                        _communicateWithUser.ShowMessage($"Group with ID {groupid} have no Evaluation for this month and year");
                    }
                }
                else
                {
                    _communicateWithUser.ShowMessage($"Group with ID {groupid} doesnt exist");
                }
            }
            else
            {
                _communicateWithUser.ShowMessage("The input/'s is/are not a valid number/'s.");
            }
        }

    }
}
