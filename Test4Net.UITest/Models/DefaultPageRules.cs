using Test4Net.UI.Attributes;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;

/// <summary>
/// Define default page validation rules
/// </summary>
public static class DefaultPageRules
{
    /// <summary>
    /// For pages with the attribute derived from IPlatformSupportedAttribute
    /// validates vs the executed test profile.
    /// </summary>
    /// <param name="rules">Page rules</param>
    /// <param name="expectedPlatform">Expected platform to match</param>
    /// <returns>rules with PlatformValidation</returns>
    public static ValidationRules AddPlatformRule(this ValidationRules rules, string expectedPlatform)
    {
        var pagePlatformAttributes = (IPlatformSupportedAttribute[])typeof(IPlatformSupportedAttribute).
            GetCustomAttributes(typeof(IPlatformSupportedAttribute), true);

        rules.Add(new Rule("PlatformValidation", typeof(IPlatformSupportedAttribute),
            validate: () =>
            {
                if (pagePlatformAttributes.Length > 0)
                {
                    return !pagePlatformAttributes.Any(p => p.GetPlatformName().Equals(
                        expectedPlatform, StringComparison.OrdinalIgnoreCase));
                }

                return true;
            }, "Checks page contains platform restrictions and matches with executed test profile."));

        return rules;
    }

    /// <summary>
    /// For pages with the attribute derived from IBrowserSupportedAttribute
    /// validates vs the executed test profile.
    /// </summary>
    /// <param name="rules">Page rules</param>
    /// <param name="expectedBrowser">Expected browser to match</param>
    /// <returns>rules with PlatformValidation</returns>
    public static ValidationRules AddBrowserRule(this ValidationRules rules, string expectedBrowser)
    {
        var browserAttributes = (IBrowserSupportedAttribute[])typeof(IBrowserSupportedAttribute).
            GetCustomAttributes(typeof(IBrowserSupportedAttribute), true);

        rules.Add(new Rule("BrowserValidation", typeof(IPlatformSupportedAttribute),
            validate: () =>
            {
                if (browserAttributes.Length > 0)
                {
                    return !browserAttributes.Any(p => p.GetBrowserDefinitionId().Equals(
                        expectedBrowser, StringComparison.OrdinalIgnoreCase));
                }

                return true;
            }, "Checks page contains browser restrictions and matches with executed test profile."));

        return rules;
    }

    /// <summary>
    /// For pages with the attribute derived from IViewPortSupportedAttribute
    /// validates vs the executed test profile.
    /// </summary>
    /// <param name="rules">Page rules</param>
    /// <param name="expectedViewPort">Expected view port size to match</param>
    /// <returns>rules with PlatformValidation</returns>
    public static ValidationRules AddViewPortRule(this ValidationRules rules, string expectedViewPort)
    {
        var viewPortAttributes = (IViewPortSupportedAttribute[])typeof(IViewPortSupportedAttribute).
            GetCustomAttributes(typeof(IViewPortSupportedAttribute), true);

        rules.Add(new Rule("PlatformValidation", typeof(IPlatformSupportedAttribute),
            validate: () =>
            {
                if (viewPortAttributes.Length > 0)
                {
                    return !viewPortAttributes.Any(p => p.GetViewPortDefinition().Equals(
                        expectedViewPort, StringComparison.OrdinalIgnoreCase));
                }

                return true;
            }, "Checks page contains view port restrictions and matches with executed test profile."));

        return rules;
    }

    /// <summary>
    /// Negotiate faults on page rules validation
    /// </summary>
    /// <param name="faults">Failed rules</param>
    /// <param name="testConfiguration"></param>
    /// <returns>true if negotiation is success</returns>
    /// <exception cref="InvalidOperationException">In case of critical faults</exception>
    public static bool NegotiateDefaultRules(this List<Rule> faults, IUiTestConfiguration testConfiguration)
    {
        // Any page that doesn't supports the testing platform must raise exception
        if (faults.Any(f => f.Owner.GetInterface(nameof(IPlatformSupportedAttribute)) != null))
        {
            var platFormName = testConfiguration.Platform;
            throw new InvalidOperationException($"There are rules that doesn't meets the {platFormName} platform criteria.");
        }

        // Any page that doesn't supports the testing browser must raise exception
        if (faults.Any(f => f.Owner.GetInterface(nameof(IBrowser)) != null))
        {
            var browser = testConfiguration.Browser;
            throw new InvalidOperationException($"There are rules that doesn't meets the {browser} browser criteria.");
        }

        // Any age that doesn't supports the testing browser view port must raise exception
        if (faults.Any(f => f.Owner.GetInterface(nameof(IViewPortSupportedAttribute)) != null))
        {
            var viewPort = testConfiguration.ViewPort;
            throw new InvalidOperationException($"There are rules that doesn't meets the {viewPort} viewport criteria.");
        }

        return true;
    }
}