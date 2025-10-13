using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void ProcessMember(ProjectInfo project, SyntaxTree tree, MemberDeclarationSyntax member, string? currentNamespace, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        switch (member)
        {
            case NamespaceDeclarationSyntax namespaceDeclaration:
                foreach (var child in namespaceDeclaration.Members)
                {
                    ProcessMember(project, tree, child, namespaceDeclaration.Name.ToString(), cancellationToken);
                }
                break;
            case FileScopedNamespaceDeclarationSyntax fileScoped:
                foreach (var child in fileScoped.Members)
                {
                    ProcessMember(project, tree, child, fileScoped.Name.ToString(), cancellationToken);
                }
                break;
            case ClassDeclarationSyntax classDeclaration:
                AnalyzeClass(project, tree, classDeclaration, currentNamespace);
                break;
            case RecordDeclarationSyntax recordDeclaration:
                AnalyzeRecord(project, tree, recordDeclaration, currentNamespace);
                break;
        }
    }

    private void AnalyzeClass(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string? currentNamespace)
    {
        var namespaceName = currentNamespace ?? project.RootNamespace;
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var fieldTypes = new Dictionary<string, FieldDescriptor>(StringComparer.OrdinalIgnoreCase);
        foreach (var field in classDeclaration.Members.OfType<FieldDeclarationSyntax>())
        {
            var declaration = field.Declaration;
            var typeName = declaration.Type.ToString();
            foreach (var variable in declaration.Variables)
            {
                var line = GetLineNumber(tree, variable);
                fieldTypes[variable.Identifier.Text] = new FieldDescriptor(typeName, line);
            }
        }

        if (IsMessageContract(classDeclaration))
        {
            _messageContracts[fqdn] = new MessageContractInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, classDeclaration.Identifier.Text);
        }

        if (ImplementsInterface(classDeclaration, "IRequest") || ImplementsInterface(classDeclaration, "IAsyncRequest"))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);
            _requests[fqdn] = requestInfo;
        }
        else if (classDeclaration.BaseList is { Types.Count: > 0 })
        {
            var baseTypeName = classDeclaration.BaseList.Types.First().Type.ToString();
            if (IsLikelyRequestName(baseTypeName))
            {
                _derivedRequestCandidates.Add(new DerivedRequestCandidate(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, baseTypeName));
            }
        }

        if (IsOptionsDeclaration(classDeclaration))
        {
            RegisterOptions(project, tree, classDeclaration, namespaceName);
        }

        if (ImplementsInterface(classDeclaration, "INotification"))
        {
            RegisterNotification(project, tree, classDeclaration, namespaceName);
        }

        if (IsPublisher(classDeclaration, fieldTypes))
        {
            AnalyzePublisher(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (IsController(classDeclaration))
        {
            AnalyzeController(project, tree, classDeclaration, namespaceName, fieldTypes);
            return;
        }

        if (IsValidator(classDeclaration))
        {
            AnalyzeValidator(project, tree, classDeclaration, namespaceName);
        }

        if (ImplementsInterface(classDeclaration, "IRequestHandler") || ImplementsInterface(classDeclaration, "IAsyncRequestHandler"))
        {
            AnalyzeHandler(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (ImplementsInterface(classDeclaration, "INotificationHandler"))
        {
            AnalyzeNotificationHandler(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (IsRepository(classDeclaration))
        {
            AnalyzeRepository(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (ExtendsType(classDeclaration, "Profile"))
        {
            AnalyzeMappingProfile(project, tree, classDeclaration, namespaceName);
        }

        if (ExtendsType(classDeclaration, "DbContext"))
        {
            AnalyzeDbContext(project, tree, classDeclaration, namespaceName);
        }

        if (IsHttpClient(classDeclaration, fieldTypes))
        {
            AnalyzeHttpClient(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (IsEntity(classDeclaration))
        {
            AnalyzeEntity(project, tree, classDeclaration, namespaceName);
        }

        if (IsBackgroundService(classDeclaration))
        {
            AnalyzeBackgroundService(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (IsMinimalApiContainer(classDeclaration, tree.FilePath))
        {
            AnalyzeMinimalApiFromClass(project, tree, classDeclaration, namespaceName);
        }

        if (classDeclaration.Modifiers.Any(m => m.Text == "public") &&
            (tree.FilePath.Contains("Dtos", StringComparison.OrdinalIgnoreCase) ||
             className.EndsWith("Dto", StringComparison.Ordinal) ||
             className.Contains(".Dtos.", StringComparison.Ordinal)))
        {
            _dtos[fqdn] = new DtoInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);
        }
    }

    private void AnalyzeRecord(ProjectInfo project, SyntaxTree tree, RecordDeclarationSyntax recordDeclaration, string? currentNamespace)
    {
        var namespaceName = currentNamespace ?? project.RootNamespace;
        var recordName = recordDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? recordName : $"{namespaceName}.{recordName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, recordDeclaration);

        if (ImplementsInterface(recordDeclaration, "IRequest") || IsDerivedRequest(recordDeclaration))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, recordName);
            _requests[fqdn] = requestInfo;
        }
        else if (recordDeclaration.BaseList is { Types.Count: > 0 })
        {
            var baseTypeName = recordDeclaration.BaseList.Types.First().Type.ToString();
            if (IsLikelyRequestName(baseTypeName))
            {
                _derivedRequestCandidates.Add(new DerivedRequestCandidate(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, recordName, baseTypeName));
            }
        }

        if (ImplementsInterface(recordDeclaration, "INotification"))
        {
            RegisterNotification(project, tree, recordDeclaration, namespaceName);
        }

        if (IsOptionsDeclaration(recordDeclaration))
        {
            RegisterOptions(project, tree, recordDeclaration, namespaceName);
        }

        if (recordDeclaration.Modifiers.Any(m => m.Text == "public") && tree.FilePath.Contains("Dtos", StringComparison.OrdinalIgnoreCase))
        {
            _dtos[fqdn] = new DtoInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, recordName);
        }

        if (IsMessageContract(recordDeclaration))
        {
            _messageContracts[fqdn] = new MessageContractInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, recordName);
        }
    }

    private void AnalyzeValidator(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        var dtoType = classDeclaration.BaseList?.Types
            .FirstOrDefault(t => t.Type is GenericNameSyntax generic && generic.Identifier.Text == "AbstractValidator")?
            .Type as GenericNameSyntax;

        var targetType = dtoType?.TypeArgumentList.Arguments.FirstOrDefault()?.ToString();
        if (targetType is null)
        {
            return;
        }

        var validatorName = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? validatorName : $"{namespaceName}.{validatorName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        _validators.Add(new ValidatorInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, validatorName, targetType));
    }

    private static bool IsController(ClassDeclarationSyntax classDeclaration)
    {
        if (!classDeclaration.Identifier.Text.EndsWith("Controller", StringComparison.Ordinal))
        {
            return false;
        }

        if (classDeclaration.AttributeLists.SelectMany(list => list.Attributes).Any(attr => attr.Name.ToString().Contains("ApiController", StringComparison.Ordinal)))
        {
            return true;
        }

        return classDeclaration.BaseList?.Types.Any(t => t.Type.ToString().Contains("ControllerBase", StringComparison.Ordinal)) == true;
    }

    private static bool IsValidator(ClassDeclarationSyntax classDeclaration)
        => classDeclaration.BaseList?.Types.Any(t => t.Type is GenericNameSyntax generic && generic.Identifier.Text == "AbstractValidator") == true;

    private static bool ImplementsInterface(TypeDeclarationSyntax typeDeclaration, string interfaceName)
    {
        if (typeDeclaration.BaseList is not { Types.Count: > 0 })
        {
            return false;
        }

        foreach (var baseType in typeDeclaration.BaseList.Types)
        {
            var typeText = baseType.Type.ToString();
            if (typeText.StartsWith(interfaceName, StringComparison.Ordinal))
            {
                return true;
            }

            var genericIndex = typeText.IndexOf('<');
            if (genericIndex >= 0)
            {
                typeText = typeText[..genericIndex];
            }

            var simpleName = typeText.Split('.').Last();
            if (simpleName.Equals(interfaceName, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static bool ExtendsType(ClassDeclarationSyntax typeDeclaration, string typeName)
        => typeDeclaration.BaseList?.Types.Any(t => t.Type.ToString().EndsWith(typeName, StringComparison.Ordinal)) == true;

    private static bool IsHttpClient(ClassDeclarationSyntax typeDeclaration, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        if (!typeDeclaration.Identifier.Text.EndsWith("Client", StringComparison.Ordinal))
        {
            return false;
        }

        return fieldTypes.Values.Any(v => v.Type.Contains("HttpClient", StringComparison.Ordinal));
    }

    private static bool IsEntity(ClassDeclarationSyntax classDeclaration)
    {
        if (!classDeclaration.Modifiers.Any(m => m.Text == "public"))
        {
            return false;
        }

        if (classDeclaration.AttributeLists.SelectMany(list => list.Attributes)
            .Any(attr => attr.Name.ToString().Contains("Table", StringComparison.Ordinal)))
        {
            return true;
        }

        if (classDeclaration.BaseList is not { Types.Count: > 0 })
        {
            return false;
        }

        foreach (var baseType in classDeclaration.BaseList.Types)
        {
            var typeName = baseType.Type.ToString();
            var genericIndex = typeName.IndexOf('<');
            if (genericIndex >= 0)
            {
                typeName = typeName[..genericIndex];
            }

            if (typeName.EndsWith("Entity", StringComparison.Ordinal) ||
                typeName.EndsWith("Aggregate", StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsMinimalApiContainer(ClassDeclarationSyntax classDeclaration, string filePath)
        => filePath.EndsWith("Program.cs", StringComparison.OrdinalIgnoreCase) || classDeclaration.Identifier.Text.EndsWith("Endpoints", StringComparison.Ordinal);

    private static bool IsMessageContract(TypeDeclarationSyntax typeDeclaration)
        => typeDeclaration.AttributeLists.SelectMany(list => list.Attributes)
            .Any(attr => attr.Name.ToString().Contains("MessageContract", StringComparison.Ordinal))
            || typeDeclaration.Identifier.Text.Contains("Published", StringComparison.Ordinal);

    private static bool IsPublisher(ClassDeclarationSyntax classDeclaration, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        if (!classDeclaration.Identifier.Text.EndsWith("Publisher", StringComparison.Ordinal))
        {
            return false;
        }

        return fieldTypes.Values.Any(v => v.Type.Contains("ServiceBus", StringComparison.Ordinal));
    }

    private static bool IsBackgroundService(ClassDeclarationSyntax classDeclaration)
        => ExtendsType(classDeclaration, "BackgroundService")
            || classDeclaration.BaseList?.Types.Any(t => t.Type.ToString().EndsWith("IHostedService", StringComparison.Ordinal)) == true;

    private static bool IsRepository(ClassDeclarationSyntax classDeclaration)
        => classDeclaration.Identifier.Text.EndsWith("Repository", StringComparison.Ordinal);

    private static bool IsDerivedRequest(RecordDeclarationSyntax recordDeclaration)
        => recordDeclaration.BaseList?.Types.Any(t => IsLikelyRequestName(t.Type.ToString())) == true;

    private static bool IsLikelyRequestName(string typeName)
        => typeName.EndsWith("Command", StringComparison.Ordinal)
            || typeName.EndsWith("Query", StringComparison.Ordinal)
            || typeName.EndsWith("Request", StringComparison.Ordinal);
}
