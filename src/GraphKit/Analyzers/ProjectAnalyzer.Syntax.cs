using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void CollectStringConstants(ProjectInfo project, SyntaxTree tree, CompilationUnitSyntax root, CancellationToken cancellationToken)
    {
        foreach (var member in root.Members)
        {
            CollectStringConstants(project, tree, member, null, cancellationToken);
        }
    }

    private void CollectStringConstants(ProjectInfo project, SyntaxTree tree, MemberDeclarationSyntax member, string? currentNamespace, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        switch (member)
        {
            case NamespaceDeclarationSyntax namespaceDeclaration:
                foreach (var child in namespaceDeclaration.Members)
                {
                    CollectStringConstants(project, tree, child, namespaceDeclaration.Name.ToString(), cancellationToken);
                }
                break;
            case FileScopedNamespaceDeclarationSyntax fileScoped:
                foreach (var child in fileScoped.Members)
                {
                    CollectStringConstants(project, tree, child, fileScoped.Name.ToString(), cancellationToken);
                }
                break;
            case ClassDeclarationSyntax classDeclaration:
                {
                    var namespaceName = currentNamespace ?? project.RootNamespace;
                    var className = classDeclaration.Identifier.Text;
                    var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
                    CaptureStringConstants(classDeclaration, namespaceName, fqdn);
                    break;
                }
            case StructDeclarationSyntax structDeclaration:
                {
                    var namespaceName = currentNamespace ?? project.RootNamespace;
                    var structName = structDeclaration.Identifier.Text;
                    var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? structName : $"{namespaceName}.{structName}";
                    CaptureStringConstants(structDeclaration, namespaceName, fqdn);
                    break;
                }
        }
    }

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
            case StructDeclarationSyntax structDeclaration:
                AnalyzeStruct(project, tree, structDeclaration, currentNamespace);
                break;
            case InterfaceDeclarationSyntax interfaceDeclaration:
                AnalyzeInterface(project, tree, interfaceDeclaration, currentNamespace);
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
            var isReadOnly = field.Modifiers.Any(m =>
                m.IsKind(SyntaxKind.ReadOnlyKeyword) ||
                m.IsKind(SyntaxKind.ConstKeyword));
            foreach (var variable in declaration.Variables)
            {
                var line = GetLineNumber(tree, variable);
                fieldTypes[variable.Identifier.Text] = new FieldDescriptor(typeName, line, isReadOnly);
            }
        }

        CaptureStringConstants(classDeclaration, namespaceName, fqdn);

    var implementedInterfaces = GetImplementedInterfaceTypes(classDeclaration);
    var inferredResponseType = InferRequestResponseType(implementedInterfaces, project.AssemblyName, project.RelativeDirectory);

        CapturePublisherProxy(project, tree, classDeclaration, namespaceName, fieldTypes);

        if (ImplementsInterface(classDeclaration, "IRequest") || ImplementsInterface(classDeclaration, "IAsyncRequest"))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, implementedInterfaces, inferredResponseType);
            _requests[fqdn] = requestInfo;
            RegisterRequestInterfaces(requestInfo, implementedInterfaces);
        }
        else if (classDeclaration.BaseList is { Types.Count: > 0 })
        {
            var baseTypeName = classDeclaration.BaseList.Types.First().Type.ToString();
            if (IsLikelyRequestName(baseTypeName))
            {
                _derivedRequestCandidates.Add(new DerivedRequestCandidate(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, baseTypeName));
            }
        }

        if (IsMessageContract(classDeclaration))
        {
            _messageContracts[fqdn] = new MessageContractInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, classDeclaration.Identifier.Text);
        }

        if (ImplementsInterface(classDeclaration, "IRequest") || ImplementsInterface(classDeclaration, "IAsyncRequest"))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, implementedInterfaces, inferredResponseType);
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

        if (ImplementsInterface(classDeclaration, "IPipelineBehavior"))
        {
            AnalyzePipelineBehavior(project, tree, classDeclaration, namespaceName, fieldTypes);
        }

        if (ImplementsInterface(classDeclaration, "IRequestPreProcessor") || ImplementsInterface(classDeclaration, "IRequestPostProcessor"))
        {
            AnalyzeRequestProcessor(project, tree, classDeclaration, namespaceName, fieldTypes);
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

        if (IsServiceClass(classDeclaration, tree.FilePath, fieldTypes))
        {
            AnalyzeService(project, tree, classDeclaration, namespaceName, fieldTypes);
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

    private void AnalyzeStruct(ProjectInfo project, SyntaxTree tree, StructDeclarationSyntax structDeclaration, string? currentNamespace)
    {
        var namespaceName = currentNamespace ?? project.RootNamespace;
        var structName = structDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? structName : $"{namespaceName}.{structName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, structDeclaration);

        CaptureStringConstants(structDeclaration, namespaceName, fqdn);

        var implementedInterfaces = GetImplementedInterfaceTypes(structDeclaration);
        var inferredResponseType = InferRequestResponseType(implementedInterfaces, project.AssemblyName, project.RelativeDirectory);

        if (ImplementsInterface(structDeclaration, "IRequest") || ImplementsInterface(structDeclaration, "IAsyncRequest"))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, structName, implementedInterfaces, inferredResponseType);
            _requests[fqdn] = requestInfo;
            RegisterRequestInterfaces(requestInfo, implementedInterfaces);
        }
        else if (structDeclaration.BaseList is { Types.Count: > 0 })
        {
            var baseTypeName = structDeclaration.BaseList.Types.First().Type.ToString();
            if (IsLikelyRequestName(baseTypeName))
            {
                _derivedRequestCandidates.Add(new DerivedRequestCandidate(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, structName, baseTypeName));
            }
        }

        if (IsMessageContract(structDeclaration))
        {
            _messageContracts[fqdn] = new MessageContractInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, structDeclaration.Identifier.Text);
        }

        if (ImplementsInterface(structDeclaration, "INotification"))
        {
            RegisterNotification(project, tree, structDeclaration, namespaceName);
        }

        if (IsOptionsDeclaration(structDeclaration))
        {
            RegisterOptions(project, tree, structDeclaration, namespaceName);
        }

        if (structDeclaration.Modifiers.Any(m => m.Text == "public") &&
            (tree.FilePath.Contains("Dtos", StringComparison.OrdinalIgnoreCase) ||
             structName.EndsWith("Dto", StringComparison.Ordinal) ||
             structName.Contains(".Dtos.", StringComparison.Ordinal)))
        {
            _dtos[fqdn] = new DtoInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, structName);
        }
    }

    private void CaptureStringConstants(TypeDeclarationSyntax typeDeclaration, string? namespaceName, string fqdn)
    {
        foreach (var field in typeDeclaration.Members.OfType<FieldDeclarationSyntax>())
        {
            if (!field.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword)))
            {
                continue;
            }

            var typeName = field.Declaration.Type.ToString();
            if (!string.Equals(typeName, "string", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (var variable in field.Declaration.Variables)
            {
                var initializer = variable.Initializer?.Value;
                if (initializer is null)
                {
                    continue;
                }

                var value = TryEvaluateStringConstant(initializer);
                if (value is null)
                {
                    continue;
                }

                var constFqdn = string.IsNullOrWhiteSpace(fqdn)
                    ? variable.Identifier.Text
                    : $"{fqdn}.{variable.Identifier.Text}";
                var shortKey = constFqdn;
                if (!string.IsNullOrWhiteSpace(namespaceName) && shortKey.StartsWith(namespaceName + ".", StringComparison.Ordinal))
                {
                    shortKey = shortKey[(namespaceName.Length + 1)..];
                }
                _stringConstants[constFqdn] = value;
                _stringConstants[shortKey] = value;
                _stringConstants[variable.Identifier.Text] = value;
            }
        }

        foreach (var nested in typeDeclaration.Members.OfType<ClassDeclarationSyntax>())
        {
            var nestedFqdn = string.IsNullOrWhiteSpace(fqdn)
                ? nested.Identifier.Text
                : $"{fqdn}.{nested.Identifier.Text}";
            CaptureStringConstants(nested, namespaceName, nestedFqdn);
        }

        foreach (var nestedStruct in typeDeclaration.Members.OfType<StructDeclarationSyntax>())
        {
            var nestedFqdn = string.IsNullOrWhiteSpace(fqdn)
                ? nestedStruct.Identifier.Text
                : $"{fqdn}.{nestedStruct.Identifier.Text}";
            CaptureStringConstants(nestedStruct, namespaceName, nestedFqdn);
        }
    }

    private string? TryEvaluateStringConstant(ExpressionSyntax expression)
    {
        switch (expression)
        {
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression):
                return literal.Token.ValueText;
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.NullLiteralExpression):
                return null;
            case InterpolatedStringExpressionSyntax interpolated:
                var builder = new StringBuilder();
                foreach (var content in interpolated.Contents)
                {
                    switch (content)
                    {
                        case InterpolatedStringTextSyntax text:
                            builder.Append(text.TextToken.ValueText);
                            break;
                        case InterpolationSyntax interpolation:
                            var interpolatedValue = TryEvaluateStringConstant(interpolation.Expression);
                            if (interpolatedValue is null)
                            {
                                return null;
                            }
                            builder.Append(interpolatedValue);
                            break;
                        default:
                            return null;
                    }
                }
                return builder.ToString();
            case BinaryExpressionSyntax binary when binary.IsKind(SyntaxKind.AddExpression):
                var left = TryEvaluateStringConstant(binary.Left);
                var right = TryEvaluateStringConstant(binary.Right);
                return left is null || right is null ? null : left + right;
            case IdentifierNameSyntax identifier:
                return LookupStringConstant(identifier.Identifier.Text);
            case MemberAccessExpressionSyntax memberAccess:
                return LookupStringConstant(memberAccess.ToString()) ?? LookupStringConstant(memberAccess.Name.Identifier.Text);
            default:
                return LookupStringConstant(expression.ToString());
        }
    }

    private string? LookupStringConstant(string key)
    {
        if (_stringConstants.TryGetValue(key, out var value))
        {
            return value;
        }

        if (key.Contains('.', StringComparison.Ordinal))
        {
            var simple = GetTopLevelSimpleIdentifier(key);
            if (_stringConstants.TryGetValue(simple, out value))
            {
                return value;
            }
        }

        return null;
    }

    private void AnalyzeInterface(ProjectInfo project, SyntaxTree tree, InterfaceDeclarationSyntax interfaceDeclaration, string? currentNamespace)
    {
        var namespaceName = currentNamespace ?? project.RootNamespace;
        var interfaceName = interfaceDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? interfaceName : $"{namespaceName}.{interfaceName}";

        var methodMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var method in interfaceDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var methodName = method.Identifier.Text;
            if (string.IsNullOrWhiteSpace(methodName))
            {
                continue;
            }

            var returnType = method.ReturnType.ToString();
            if (string.IsNullOrWhiteSpace(returnType) || string.Equals(returnType, "void", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var qualifiedReturnType = QualifyTypeName(returnType);
            if (string.IsNullOrWhiteSpace(qualifiedReturnType))
            {
                continue;
            }

            methodMap[methodName] = qualifiedReturnType;
            if (methodName.EndsWith("Async", StringComparison.Ordinal))
            {
                var alternate = methodName[..^5];
                if (!string.IsNullOrWhiteSpace(alternate))
                {
                    methodMap[alternate] = qualifiedReturnType;
                }
            }
        }

        if (methodMap.Count == 0)
        {
            return;
        }

        RegisterInterfaceMethodMap(fqdn, methodMap);
        RegisterInterfaceMethodMap(interfaceName, methodMap);

        if (interfaceName.StartsWith("I", StringComparison.Ordinal) && interfaceName.Length > 1)
        {
            var trimmed = interfaceName[1..];
            RegisterInterfaceMethodMap(trimmed, methodMap);
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                RegisterInterfaceMethodMap($"{namespaceName}.{trimmed}", methodMap);
            }
        }
    }

    private void RegisterInterfaceMethodMap(string? key, IReadOnlyDictionary<string, string> methods)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        var methodLookup = _interfaceMethodReturnTypes.GetOrAdd(key, _ => new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase));
        foreach (var pair in methods)
        {
            methodLookup[pair.Key] = pair.Value;
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

        var implementedInterfaces = GetImplementedInterfaceTypes(recordDeclaration);
        var inferredResponseType = InferRequestResponseType(implementedInterfaces, project.AssemblyName, project.RelativeDirectory);

        if (ImplementsInterface(recordDeclaration, "IRequest") || IsDerivedRequest(recordDeclaration))
        {
            var requestInfo = new RequestInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, recordName, implementedInterfaces, inferredResponseType);
            _requests[fqdn] = requestInfo;
            RegisterRequestInterfaces(requestInfo, implementedInterfaces);
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

    private void RegisterRequestInterfaces(RequestInfo request, IReadOnlyList<string> interfaceTypes)
    {
        if (interfaceTypes.Count == 0)
        {
            return;
        }

        foreach (var interfaceType in interfaceTypes)
        {
            if (string.IsNullOrWhiteSpace(interfaceType))
            {
                continue;
            }

            var keys = DeriveInterfaceLookupKeys(interfaceType);
            foreach (var key in keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                var lookup = _requestsByInterfaceType.GetOrAdd(key, _ => new ConcurrentDictionary<string, RequestInfo>(StringComparer.OrdinalIgnoreCase));
                lookup[request.Fqdn] = request;
            }
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
            if (string.IsNullOrWhiteSpace(typeText))
            {
                continue;
            }

            var simpleName = GetTopLevelSimpleIdentifier(typeText);
            if (simpleName.Equals(interfaceName, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static IReadOnlyList<string> GetImplementedInterfaceTypes(TypeDeclarationSyntax typeDeclaration)
    {
        if (typeDeclaration.BaseList is not { Types.Count: > 0 })
        {
            return Array.Empty<string>();
        }

        var interfaces = new List<string>();
        foreach (var baseType in typeDeclaration.BaseList.Types)
        {
            var typeText = baseType.Type.ToString();
            if (string.IsNullOrWhiteSpace(typeText) || !IsLikelyInterfaceType(typeText))
            {
                continue;
            }

            interfaces.Add(typeText.Trim());
        }

        return interfaces.Count == 0 ? Array.Empty<string>() : interfaces;
    }

    private string? InferRequestResponseType(IReadOnlyList<string> interfaceTypes, string? preferredAssembly, string? preferredProject)
    {
        if (interfaceTypes.Count == 0)
        {
            return null;
        }

        foreach (var implementedInterface in interfaceTypes)
        {
            if (string.IsNullOrWhiteSpace(implementedInterface))
            {
                continue;
            }

            var simple = GetTopLevelSimpleIdentifier(implementedInterface);
            if (!simple.Equals("IRequest", StringComparison.OrdinalIgnoreCase) &&
                !simple.Equals("IAsyncRequest", StringComparison.OrdinalIgnoreCase) &&
                !simple.Equals("IRequest`1", StringComparison.OrdinalIgnoreCase) &&
                !simple.Equals("IAsyncRequest`1", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var genericArguments = SplitGenericArguments(implementedInterface);
            if (genericArguments.Count == 0)
            {
                continue;
            }

            var responseCandidate = genericArguments[^1].Trim();
            if (string.IsNullOrWhiteSpace(responseCandidate))
            {
                continue;
            }

            var qualified = QualifyTypeName(responseCandidate, preferredAssembly, preferredProject);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                qualified = responseCandidate;
            }

            if (IsGenericPlaceholder(qualified))
            {
                continue;
            }

            return qualified;
        }

        return null;
    }

    private static bool IsLikelyInterfaceType(string typeName)
    {
        var simple = GetTopLevelSimpleIdentifier(typeName);
        return !string.IsNullOrWhiteSpace(simple) &&
               simple.Length > 1 &&
               simple[0] == 'I' &&
               char.IsUpper(simple[1]);
    }

    private static bool ExtendsType(ClassDeclarationSyntax typeDeclaration, string typeName)
        => typeDeclaration.BaseList?.Types.Any(t => t.Type.ToString().EndsWith(typeName, StringComparison.Ordinal)) == true;

    private static bool IsHttpClient(ClassDeclarationSyntax typeDeclaration, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var inheritsHttpClient = typeDeclaration.BaseList?.Types.Any(baseType =>
        {
            var identifier = baseType.Type.ToString();
            var genericIndex = identifier.IndexOf('<');
            if (genericIndex >= 0)
            {
                identifier = identifier[..genericIndex];
            }

            return identifier.EndsWith("HttpClient", StringComparison.Ordinal)
                || identifier.EndsWith("OAuthClient", StringComparison.Ordinal)
                || identifier.EndsWith("RestClient", StringComparison.Ordinal);
        }) == true;

        if (inheritsHttpClient)
        {
            return true;
        }

        var hasHttpClientField = fieldTypes.Values.Any(v => v.Type.Contains("HttpClient", StringComparison.Ordinal));
        if (hasHttpClientField)
        {
            return true;
        }

        var hasWrapperInvocation = HasHttpWrapperInvocation(typeDeclaration);
        var hasAlternateHttpDependency = fieldTypes.Values.Any(v =>
            v.Type.Contains("IOAuthClient", StringComparison.Ordinal) ||
            v.Type.Contains("IDataGetService", StringComparison.Ordinal));

        if (hasAlternateHttpDependency && hasWrapperInvocation)
        {
            return true;
        }

        if (!typeDeclaration.Identifier.Text.EndsWith("Client", StringComparison.Ordinal))
        {
            return false;
        }

        if (!hasWrapperInvocation)
        {
            return false;
        }

        return typeDeclaration.DescendantNodes()
            .OfType<ObjectCreationExpressionSyntax>()
            .Any(creation => creation.Type.ToString().EndsWith("UrlBuilder", StringComparison.Ordinal));
    }

    private static bool HasHttpWrapperInvocation(ClassDeclarationSyntax typeDeclaration)
    {
        var hasUrlBuilder = typeDeclaration
            .DescendantNodes()
            .OfType<ObjectCreationExpressionSyntax>()
            .Any(creation => creation.Type.ToString().EndsWith("UrlBuilder", StringComparison.Ordinal));

        foreach (var invocation in typeDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            var methodName = GetInvocationIdentifier(invocation.Expression);
            if (string.IsNullOrWhiteSpace(methodName))
            {
                continue;
            }

            if (!IsLikelyHttpWrapperName(methodName!))
            {
                continue;
            }

            if (InvocationHasRouteCandidate(invocation, hasUrlBuilder))
            {
                return true;
            }
        }

        return false;
    }

    private static bool InvocationHasRouteCandidate(InvocationExpressionSyntax invocation, bool hasUrlBuilder)
    {
        var arguments = invocation.ArgumentList.Arguments;
        if (arguments.Count == 0)
        {
            return false;
        }

        var limit = Math.Min(arguments.Count, 2);
        for (var i = 0; i < limit; i++)
        {
            if (LooksLikeRouteExpression(arguments[i].Expression))
            {
                return true;
            }
        }

        if (!hasUrlBuilder || arguments.Count <= 1)
        {
            return false;
        }

        var secondArgument = arguments[1].Expression;
        return secondArgument is IdentifierNameSyntax
            or InvocationExpressionSyntax
            or MemberAccessExpressionSyntax
            or ObjectCreationExpressionSyntax;
    }

    private static bool LooksLikeRouteExpression(ExpressionSyntax expression)
    {
        return expression switch
        {
            LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression)
                => literal.Token.ValueText.Contains("/", StringComparison.Ordinal) ||
                   literal.Token.ValueText.StartsWith("http", StringComparison.OrdinalIgnoreCase),
            InterpolatedStringExpressionSyntax interpolated
                => interpolated.Contents
                    .OfType<InterpolatedStringTextSyntax>()
                    .Any(content => content.TextToken.ValueText.Contains("/", StringComparison.Ordinal)),
            _ => false
        };
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
        var nameEndsWithPublisher = classDeclaration.Identifier.Text.EndsWith("Publisher", StringComparison.Ordinal);
        var hasPublisherField = fieldTypes.Values.Any(v => v.Type.Contains("Publisher", StringComparison.OrdinalIgnoreCase) || v.Type.Contains("ServiceBus", StringComparison.OrdinalIgnoreCase));
        var implementsPublisherInterface = classDeclaration.BaseList?.Types.Any(t => t.Type.ToString().Contains("Publisher", StringComparison.OrdinalIgnoreCase)) == true;
        var hasPublishMethod = classDeclaration.Members
            .OfType<MethodDeclarationSyntax>()
            .Any(m => m.Identifier.Text.StartsWith("Publish", StringComparison.OrdinalIgnoreCase));

        if (nameEndsWithPublisher && (hasPublisherField || hasPublishMethod || implementsPublisherInterface))
        {
            return true;
        }

        if (implementsPublisherInterface && hasPublishMethod)
        {
            return true;
        }

        if (hasPublisherField && hasPublishMethod)
        {
            return true;
        }

        return false;
    }

    private static bool IsBackgroundService(ClassDeclarationSyntax classDeclaration)
        => ExtendsType(classDeclaration, "BackgroundService")
            || classDeclaration.BaseList?.Types.Any(t => t.Type.ToString().EndsWith("IHostedService", StringComparison.Ordinal)) == true;

    private static bool IsRepository(ClassDeclarationSyntax classDeclaration)
    {
        if (classDeclaration.Identifier.Text.EndsWith("Repository", StringComparison.Ordinal))
        {
            return true;
        }

        if (!classDeclaration.Identifier.Text.EndsWith("Query", StringComparison.Ordinal))
        {
            return false;
        }

        if (classDeclaration.BaseList is not { Types.Count: > 0 })
        {
            return false;
        }

        foreach (var baseType in classDeclaration.BaseList.Types)
        {
            var baseText = baseType.Type.ToString();
            if (string.IsNullOrWhiteSpace(baseText))
            {
                continue;
            }

            // Dapper queries within Cirrus (and similar solutions) derive from AsyncDapperQuery or helper abstractions such as GetLedgerQuery.
            if (baseText.Contains("AsyncDapperQuery", StringComparison.Ordinal) ||
                baseText.Contains("DapperQuery", StringComparison.Ordinal) ||
                baseText.Contains("GetLedgerQuery", StringComparison.Ordinal) ||
                baseText.Contains("GetTrialBalanceQuery", StringComparison.Ordinal))
            {
                return true;
            }

            var genericIndex = baseText.IndexOf('<');
            if (genericIndex > 0)
            {
                var genericBase = baseText[..genericIndex];
                if (genericBase.EndsWith("Query", StringComparison.Ordinal) &&
                    genericBase.Contains("Dapper", StringComparison.Ordinal))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsDerivedRequest(RecordDeclarationSyntax recordDeclaration)
        => recordDeclaration.BaseList?.Types.Any(t => IsLikelyRequestName(t.Type.ToString())) == true;

    private static bool IsLikelyRequestName(string typeName)
        => typeName.EndsWith("Command", StringComparison.Ordinal)
            || typeName.EndsWith("Query", StringComparison.Ordinal)
            || typeName.EndsWith("Request", StringComparison.Ordinal);
}
