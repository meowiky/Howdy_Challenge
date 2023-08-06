using Howdy_Challenge.Models;
using System.Text.Json.Serialization;

namespace Howdy_Challenge.DataManagment.DTO
{
    public record Session(
    [property: JsonPropertyName("employeeId")] int employeeId,
    [property: JsonPropertyName("groupId")] int groupId,
    [property: JsonPropertyName("answeredOn")] string answeredOnString,
    [property: JsonPropertyName("answer1")] int answer1,
    [property: JsonPropertyName("answer2")] int answer2,
    [property: JsonPropertyName("answer3")] int answer3,
    [property: JsonPropertyName("answer4")] int answer4,
    [property: JsonPropertyName("answer5")] int answer5
)
    {
        public DateTime AnsweredOn => DateTime.Parse(answeredOnString, System.Globalization.CultureInfo.InvariantCulture);
        public int[] answers => new int[] { answer1, answer2, answer3, answer4, answer5 };
    }

}
