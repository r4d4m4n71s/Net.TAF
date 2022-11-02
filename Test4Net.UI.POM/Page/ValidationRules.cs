﻿namespace Test4Net.UI.POM.Page;

/// <summary>
/// Validation rules model
/// </summary>
public class ValidationRules : Dictionary<string, Rule>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="defaultRules"></param>
    public ValidationRules(List<Rule> defaultRules = null)
    {
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
    /// <param name="rules">list of rules</param>
    /// <returns>this as fluent</returns>
    public ValidationRules Add(List<Rule> rules){
        rules.ForEach(rule => Add(rule));
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
            this[id] = rule;
        }
        else
        {
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
                faults.Add(rule);
            }
        }

        return negotiateFaults(faults);
    }
}

/// <summary>
/// Rule model
/// </summary>
public class Rule
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Unique id</param>
    /// <param name="owner">Object owner of the rule</param>
    /// <param name="validate">Perform a validation action</param>
    /// <param name="definition"></param>
    public Rule(string id, Type owner, Func<bool> validate, string definition = "")
    {
        Id = id;
        Owner = owner;
        IsValid = validate();
        Definition = definition;
    }
    
    /// <summary>
    /// Rule name
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Owner of this rule
    /// </summary>
    public Type Owner { get; init; }
    
    /// <summary>
    /// Validation result
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Developer details the rule definition
    /// </summary>
    public string Definition { get; }
}