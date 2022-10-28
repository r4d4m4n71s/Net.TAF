using System.Reflection;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Test4Net.Util.Json;

namespace Test4Net.UI.Util;

/// <summary>
/// Driver options setup utilities
/// </summary>
public static class DriverOptionsExtensions
{
    /// <summary>
    /// Built a T type of options from a dictionary
    /// </summary>
    /// <typeparam name="TOptions"><see cref="DriverOptions"/></typeparam>
    /// <param name="options">Target</param>
    /// <param name="setupAsDic">Options as dictionary</param>
    /// <returns>A build options</returns>
    /// <exception cref="ArgumentException">In case of conversion errors</exception>
    public static TOptions BuildFromDictionary<TOptions>(this TOptions options, IDictionary<string, object> setupAsDic) where TOptions : DriverOptions
    {
        var _options = options.DeepClone();

        var rejected = _options.MapOptionsFromDic(setupAsDic);

        // Validate all 
        var rejectedForDriverOp = rejected.Where(s =>
            !_options.GetTypeSafeOptionName(s.Key).Equals(string.Empty)).ToList();

        if (rejectedForDriverOp.Any())
            throw new ArgumentException("There are errors setting up driver options from dictionary: " +
                string.Join("\n", rejectedForDriverOp.Select(o =>
                    $"[{o.Key}]:[{o.Value.Key} -- {o.Value.Value}]\n\nCheck in json the object structures meets {_options.GetType()} formats.")));

        // for rejected options try to add as additional options
        var userOptions = rejected.Where(s =>
            _options.GetTypeSafeOptionName(s.Key).Equals(string.Empty)).ToList();

        foreach (var additionalOpKvp in userOptions)
            _options.AddAdditionalOption(additionalOpKvp.Key, additionalOpKvp.Value.Key);

        return _options;
    }

    /// <summary>
    /// Categorize and assign driver options
    /// Force to add options mapping keys-values to DriverOptions properties
    /// </summary>
    /// <returns>Options that were not able to set</returns>
    public static IDictionary<string, KeyValuePair<string, string>> MapOptionsFromDic(this DriverOptions options, IDictionary<string, object> optionsAsDic)
    {
        var rejectedOptions = new Dictionary<string, KeyValuePair<string, string>>();
        foreach (var optionKvp in optionsAsDic)
        {
            try
            {
                options.AddOption(optionKvp.Key, optionKvp.Value);
            }
            catch (Exception e)
            {
                rejectedOptions.Add(optionKvp.Key, new KeyValuePair<string, string>(optionKvp.Value.ToString(), e.Message));
            }
        }
        return rejectedOptions;
    }

    /// <summary>
    /// Try to add a option using the own driver options ways
    /// </summary>
    /// <param name="options">Target options model</param>
    /// <param name="name">Option name</param>
    /// <param name="value">Value to add</param>
    public static void AddOption(this DriverOptions options, string name, object value)
    {
        var optionInfo = GetTypeSafeOptionName(options, name)!.Split(" ");
        
        // Get type of element
        switch (optionInfo.Length > 1 ? optionInfo[1] : string.Empty)
        {
            case "property":
                AddAsPropertyValue(options, optionInfo[0], value);
                break;

            case "method":
                AddAsMethodCalling(options, optionInfo[0], value);
                break;

            default:
                if (!TryAddOptionAsPropertyValue(options, name, value))
                    if (!TryAddOptionAsMethodCalling(options, name, value))
                        throw new ArgumentException(
                            $"There is no property o method related to [{name}] option as part of {options.GetType()}");
                break;
        }
    }

    /// <summary>
    /// Get typed method or property from driver options
    /// </summary>
    /// <param name="options">Target</param>
    /// <param name="optionName">option to look up</param>
    /// <returns></returns>
    public static string GetTypeSafeOptionName(this DriverOptions options, string optionName) 
    {
        // Retrieve option type and safe name from driver options
        var method = options.GetType().GetMethod("GetTypeSafeOptionName",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        return method!.Invoke(options, new object[] { optionName })!.ToString();
    }

    /// <summary>
    /// Serialize options to get internal properties
    /// </summary>
    /// <returns></returns>
    public static IDictionary<string, object> AsDic(this DriverOptions options) =>
        options.ToString().ToDictionary();

    /// <summary>
    /// Force to add a driver option as property value
    /// </summary>
    /// <param name="options">Target driver options</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Value to set</param>
    /// <returns>true if success</returns>
    private static bool TryAddOptionAsPropertyValue(DriverOptions options, string name, object value)
    {
        // To upper case first character to meet camel case property naming convention
        //var key = string.Concat(name.First().ToString().ToUpper(), name.AsSpan(1));

        // Get option as property
        var propertyInfo = options.GetType().GetProperties().FirstOrDefault(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        try
        {
            AddAsPropertyValue(options, name, value, propertyInfo);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Try to add a driver option as property value
    /// throws exception if the operation is not success
    /// </summary>
    /// <param name="options">Target driver options</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Value to set</param>
    /// <param name="propertyInfo">Property info to look up object</param>
    private static void AddAsPropertyValue(DriverOptions options, string name, object value, PropertyInfo propertyInfo = null)
    {
        if (propertyInfo == null)
            propertyInfo = options.GetType().GetProperty(name);

        // There is no related info to the  
        if (propertyInfo == null)
            throw new ArgumentException($"For option: {name} there is not a related property to set.");

        // Setting as enum
        if (Convert.ToBoolean(propertyInfo.GetValue(options)?.GetType().IsEnum))
        {
            propertyInfo.SetValue(options, GetEnumByReflection(propertyInfo, options, value.ToString()));
            return;
        }

        // Setting as object
        if (!Convert.ToBoolean(propertyInfo.PropertyType?.IsPrimitive))
        {
            try
            {
                propertyInfo.SetValue(options, JToken.Parse(value.ToString()!).ToObject(propertyInfo.PropertyType));
                return;
            }
            catch (Exception)
            { // ignored
            }
        }

        propertyInfo.SetValue(options, value);
    }

    /// <summary>
    /// Force to add a driver option as function calling
    /// throws exception if the operation is not success
    /// </summary>
    /// <param name="options">Target driver options</param>
    /// <param name="name">Method name</param>
    /// <param name="value">Value to set</param>
    /// <returns>True if success</returns>
    private static bool TryAddOptionAsMethodCalling(DriverOptions options, string name, object value)
    {
        // Get all methods match the option
        var methodsInfo = options.GetType().GetMethods().Where(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.IsPublic && !p.IsSpecialName).ToList();
        try
        {
            AddAsMethodCalling(options, name, value, methodsInfo);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Try to add a driver option as function calling
    /// throws exception 
    /// </summary>
    /// <param name="options">Target driver options</param>
    /// <param name="name">Method name</param>
    /// <param name="value">Value to set</param>
    /// <param name="methodsInfo">Methods info to look up object</param>
    private static void AddAsMethodCalling(DriverOptions options, string name, object value, List<MethodInfo> methodsInfo = null)
    {
        methodsInfo ??= options.GetType().GetMethods().Where(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.IsPublic && !p.IsSpecialName).ToList();

        // There is no related method
        if (!methodsInfo.Any())
            throw new ArgumentException($"For option: {name} there is not a related method to call.");

        JToken jToken = null;
        try
        {
            jToken = JToken.Parse(value.ToString()!);
        }
        catch (Exception)
        { // ignored
        }

        var errorsMessages = new List<string>();
        // Force to try execute each method candidate
        foreach (var methodInfo in methodsInfo)
        {
            try
            {
                var parameters = new List<object>();
                var parameterTypes = methodInfo.GetParameters().ToList().ConvertAll(p => p.ParameterType);
                var i = 0;

                // for each method parameters try to map input values                
                foreach (var type in parameterTypes)
                {
                    // case primitive value
                    if (jToken == null)
                    {
                        parameters.Add(value.ToString());
                        continue;
                    }

                    // case array type object map complete 
                    if (type.IsArray || type.IsEnum)
                        parameters.Add(jToken.ToObject(type));
                    else
                        // map one by one
                        parameters.Add(
                            jToken is JArray array ? array[i].ToObject(type) : jToken.ToObject(type));
                    i++;
                }
                methodInfo.Invoke(options, parameterTypes.Count > 0 ? parameters.ToArray() : null);
                return;
            }
            catch (Exception e)
            {
                errorsMessages.Add($"{methodInfo.Name} -> {e.Message}");
            }
        }

        throw new Exception("Unable to execute any candidate methods: \n" + string.Join("\n", errorsMessages));
    }

    /// <summary>
    /// Get enum from object using reflection
    /// </summary>
    /// <param name="propertyInfo">Target property info</param>
    /// <param name="target">Object to get enum</param>
    /// <param name="referenceValue">ReferenceValue</param>
    /// <returns>Enum or null</returns>
    private static object GetEnumByReflection(PropertyInfo propertyInfo, object target, string referenceValue)
    {
        var enumType = propertyInfo.GetValue(target)?.GetType();
        var enumName = enumType?.GetEnumNames().FirstOrDefault(e => e.Equals(referenceValue, StringComparison.OrdinalIgnoreCase));

        Enum.TryParse(enumType!, enumName, out object enumValue);
        return enumValue;
    }
}