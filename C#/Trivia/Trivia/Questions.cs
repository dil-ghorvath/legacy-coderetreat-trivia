using System.Collections.Generic;

namespace Trivia;

public class Questions
{
    private readonly Dictionary<string, LinkedList<string>> _questions;

    public Questions()
    {
        _questions = new Dictionary<string, LinkedList<string>>
        {
            { "Pop", new LinkedList<string>() },
            { "Science", new LinkedList<string>() },
            { "Sports", new LinkedList<string>() },
            { "Rock", new LinkedList<string>() }
        };

        for (var i = 0; i < 50; i++)
        {
            _questions["Pop"].AddLast("Pop question " + i);
            _questions["Science"].AddLast("Science question " + i);
            _questions["Sports"].AddLast("Sports question " + i);
            _questions["Rock"].AddLast("Rock question " + i);
        }
    }

    public string GetNextQuestion(string category)
    {
        var question = _questions[category].First;
        _questions[category].RemoveFirst();
        return question.Value;
    }
}
