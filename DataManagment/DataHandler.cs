
using Howdy_Challenge.DataManagment.DTO;
using Howdy_Challenge.Models;
using Howdy_Challenge.UserCommunication;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml;

namespace Howdy_Challenge.DataManagment
{
    public class DataHandler
    {
        private Dictionary<int, Group> _allGroups;
        private ICommunicateWithUser _communicateWithUser;

        public DataHandler(ICommunicateWithUser communicateWithUser)
        {
            _communicateWithUser = communicateWithUser;
            if (File.Exists("groups.json"))
            {
                string json = File.ReadAllText("groups.json");
                
                    _allGroups = JsonConvert.DeserializeObject<Dictionary<int, Group>>(json);

                    foreach (var group in _allGroups.Values)
                    {
                        group.InitializeCommunicateWithUser(communicateWithUser);
                        foreach (var individual in group.Individuals.Values)
                        {
                            individual.InitializeCommunicateWithUser(_communicateWithUser);
                        }
                    }
                
            }
            else
            {
                _allGroups = new Dictionary<int, Group>();
            }
        }
        public void AddingNewSessionFromjson(string data)
        {
            if (data[0] == '[')
            {
                var sessions = System.Text.Json.JsonSerializer.Deserialize<List<Session>>(data);
                foreach (Session session in sessions)
                {
                    SessionAdd(session);
                }
            }
            else
            {
                var singlesession = System.Text.Json.JsonSerializer.Deserialize<Session>(data);
                SessionAdd(singlesession);
            }
            
        }

        private void SessionAdd(Session session)
        {
            Group group = searchGroupId(session.groupId);
            if (group != null)
            {
                Individual individual = searchEmployeeId(group, session.employeeId);
                if (individual == null)
                {
                    individual = new Individual(session.employeeId, session.groupId, session.answers, session.AnsweredOn, _communicateWithUser);
                    group.Add(individual);
                }
                else
                {
                    individual.AddAnswer(session.answers, session.AnsweredOn);
                }
            }
            else
            {
                Individual individual = new Individual(session.employeeId, session.groupId, session.answers, session.AnsweredOn, _communicateWithUser);
                group = new Group(individual, _communicateWithUser);
                try
                {
                    _allGroups.Add(session.groupId, group);
                }
                catch (ArgumentException ex)
                {
                    _communicateWithUser.ShowMessage($"Group with ID {session.groupId} already exists");
                }
            }
        }

        public Group searchGroupId(int groupId)
        {
            Group group = null;
            _allGroups.TryGetValue(groupId, out group);
            return group;
        }

        public Individual searchEmployeeId(int groupId, int employeeId)
        {
            Group group = searchGroupId(groupId);
            if (group != null)
            {
                return group.getByEmployeeId(employeeId);
            }
            return null;
        }

        public Individual searchEmployeeId(Group group, int employeeId)
        {
            if (group != null)
            {
                return group.getByEmployeeId(employeeId);
            }
            return null;
        }

        public void SerializeAllGroup()
        {
            string json = JsonConvert.SerializeObject(_allGroups, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("groups.json", json);
        }
    }
}
