using Test4Net.Logging.Interfaces;

namespace Test4Net.UI.Page;

/// <summary>
/// Validation rules model
/// </summary>
public class ValidationRules : Dictionary<string, Rule>
{
    /// <summary>
    /// Logger
    /// </summary>
    protected readonly ILog _log;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="log">logger</param>
    /// <param name="defaultRules"></param>
    public ValidationRules(ILog log, List<Rule> defaultRules = null)
    {
        _log = log;
        if(defaultRules != null)
            foreach (var rule in defaultRules)
                Add(rule);
    }

    /// <summary>
    /// Add page validation rule
    /// <see cref="Rule"/>
    /// </summary>
    /// <param name="rule">Rule</param>
    /// <returns>this as fluent</returns>
    public ValidationRules Add(Rule rule)
    {
        Add(rule.Id, rule);
        return this;
    }

    /// <summary>
    /// Add page validation rule
    /// <see cref="Rule"/>
    /// </summary>
    /// <param name="id">Unique id</param>
    /// <param name="rule">Rule</param>
    public new void Add(string id, Rule rule)
    {
        if (ContainsKey(rule.Id))
        {
            _log.Info($"Overwriting [{id}] validation rule.");
            this[id] = rule;
        }
        else
        {
            _log.Info($"Adding [{id}] validation rule.");
            base.Add(id, rule);
        }
    }

    /// <summary>
    /// Perform rules validation
    /// </summary>
    /// <returns></returns>
    public virtual bool ValidateRules(Func<List<Rule>, bool> negotiateFaults)
    {
        var faults = new List<Rule>();
        foreach (var rule in Values)
        {
            if (!rule.IsValid)
            {
                _log.Warn($"Rule [{rule.Id}] is not valid.");

                if (string.IsNullOrEmpty(rule.Details))
                    _log.Info($"...{rule.Details}");

                faults.Add(rule);
            }
        }

        return negotiateFaults(faults);
    }
}

/// <summary>
/// Rule model
/// </summary>
public struct Rule
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Unique id</param>
    /// <param name="validate">Perform a validation action</param>
    /// <param name="details"></param>
    public Rule(string id, Func<bool> validate, string details = "")
    {
        Id = id;
        IsValid = validate();
        Details = details;
    }

    /// <summary>
    /// Rule name
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Validation result
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Developer details or description about the rule
    /// </summary>
    public string Details { get; }
}