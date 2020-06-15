using System;
using System.Reflection;

namespace Asp.net_Web_API_torutial_for_begginers.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}