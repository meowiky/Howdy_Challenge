

using Howdy_Challenge.UserCommunication;
using Newtonsoft.Json;
using System.Globalization;

namespace Howdy_Challenge.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public Dictionary<int, Individual> Individuals { get; set; }
        public Dictionary<DateTime, double> Scores { get; set; }

        private ICommunicateWithUser _communicateWithUser;

        public Group(int groupId, ICommunicateWithUser communicateWithUser)
        {
            GroupId = groupId;
            _communicateWithUser = communicateWithUser;
            Individuals = new Dictionary<int, Individual>();
            Scores = new Dictionary<DateTime, double>();
        }

        public Group(Individual individual, ICommunicateWithUser communicateWithUser)
        {
            GroupId = individual.GroupId;
            _communicateWithUser = communicateWithUser;
            Individuals = new Dictionary<int, Individual>();
            Individuals.Add(individual.EmployeeId, individual);
            Scores = new Dictionary<DateTime, double>();
        }

        public Group() { }

        public void InitializeCommunicateWithUser(ICommunicateWithUser communicateWithUser)
        {
            _communicateWithUser = communicateWithUser;
        }

        public Individual getByEmployeeId(int id)
        {
            Individual individual = null;
            Individuals.TryGetValue(id, out individual);
            return individual;
        }

        public void Add(Individual individual)
        {
            try
            {
                Individuals.Add(individual.EmployeeId, individual);
            }
            catch (ArgumentException ex) 
            {
                _communicateWithUser.ShowMessage($"Individual with ID {individual.EmployeeId} already exists");
            }

            
        }

        public void Remove(Individual individual)
        {
            Individuals.Remove(individual.EmployeeId);
        }

        public void Remove(int id)
        {
            Individuals.Remove(id);
        }

        public double? MonthEvaluation(int year, int month)
        {
            double sum = 0;
            List<int> idOfUsersThatDidntAnswer = new List<int>();
            foreach (Individual individual in Individuals.Values)
            {
                double? evaluation = individual.GetMonthEvaluation(year, month);

                if (evaluation != null)
                {
                    sum += (double)evaluation;
                }
                else
                    idOfUsersThatDidntAnswer.Add(individual.EmployeeId);
            }
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            if (idOfUsersThatDidntAnswer.Count != 0)
            {
                _communicateWithUser.ShowMessage($"Individuals with employee IDs: {string.Join(", ", idOfUsersThatDidntAnswer)} didn't answer questions in month {monthName} in year {year}");
            }
            if (sum != 0)
            {
                double result = sum / (Individuals.Count - idOfUsersThatDidntAnswer.Count);
                int x = 0;
                if(Scores.Count != 0)
                {
                    x = Scores.Count(pair => pair.Key.Year == year && pair.Key.Month == month);
                }
                int count = 1;
                if(x != null)
                {
                    count += x;
                }
                Scores.Add(new DateTime(year, month, count), result);

                return result;
            }
            _communicateWithUser.ShowMessage($"There was no Data for month {monthName} in year {year}");
            return null;
        }

        public double? searchForEvaluation(int year, int month)
        {
            try
            {
                var thismonth = Scores.Where(pair =>
                pair.Key.Year == year && pair.Key.Month == month)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

                var newest = thismonth.OrderByDescending(pair => pair.Key).First();

                return newest.Value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
