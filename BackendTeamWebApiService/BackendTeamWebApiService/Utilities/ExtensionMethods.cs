namespace BackendTeamWebApiService.Utilities;

using Amazon.DynamoDBv2.Model;
using Models;
using System.Text;

/// <summary>
/// Dictionary Extension Methods
/// </summary>
public static class DictionaryExtensionMethods
{
    
    /// <summary>
    /// Sets the attributes for a person in the referenced dictionary.
    /// </summary>
    /// <param name="personAttributes">The dictionary to add the person attributes to.</param>
    /// <param name="person">The person whose attributes we need to read from.</param>
    public static void SetPerson(this Dictionary<string, AttributeValue> personAttributes, Person person)
    {
        personAttributes["Id"] = new AttributeValue(person.Id.ToString());
        personAttributes["FirstName"] = new AttributeValue(person.FirstName);
        personAttributes["LastName"] = new AttributeValue(person.LastName);
    }

    /// <summary>
    /// Generic method that can be used to generate a 0 padded number.
    /// </summary>
    /// <param name="id">The integer value to we want to pad with 0s.</param>
    /// <param name="stringLength">The length of the string.</param>
    /// <returns>A zero left-padded string.</returns>
    public static string GetZeroLeftPaddedNumber(this int id, int stringLength)
    {
        StringBuilder stringBuilder = new StringBuilder(id);

        while (stringBuilder.Length < stringLength)
        {
            stringBuilder.Append('0', 0);
        }

        return stringBuilder.ToString();
    }
}