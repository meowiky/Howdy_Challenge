
using Howdy_Challenge.UserCommunication;
using Newtonsoft.Json;

namespace Howdy_Challenge.Models
{
    public class Individual
    {
        public int EmployeeId { get; set; }
        public int GroupId {  get; set; }

        private Dictionary<DateTime, int[]> _answers;

        private ICommunicateWithUser _communicateWithUser;

        public Individual(int employeeId, int groupId, int[] answers, DateTime dateTime, ICommunicateWithUser communicateWithUser)
        {
            EmployeeId = employeeId;
            GroupId = groupId;
            _communicateWithUser = communicateWithUser;
            _answers = new Dictionary<DateTime, int[]>();
            _answers.Add(dateTime, answers);
        }

        public Individual() { }

        public void InitializeCommunicateWithUser(ICommunicateWithUser communicateWithUser)
        {
            _communicateWithUser = communicateWithUser;
        }


        public void AddAnswer(int[] answers, DateTime dateTime)
        {
            try
            {
                _answers.Add(dateTime, answers);
            }
            catch (ArgumentException ex)
            {
                _communicateWithUser.ShowMessage($"Answers in this specific DateTime are already registered");
            }
            
        }

        public double? GetMonthEvaluation(int year, int month)
        {
            var thismonth = _answers.Where(pair =>
            pair.Key.Year == year && pair.Key.Month == month)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

            if (thismonth.Count > 0)
            {
                var newest = thismonth.OrderByDescending(pair => pair.Key).First();
                int sum = 0;
                foreach (var answer in newest.Value)
                {
                    sum += answer;
                }
                return sum / newest.Value.Length;
            }
            else
            {
                return null;
            }
        }
    }
}
