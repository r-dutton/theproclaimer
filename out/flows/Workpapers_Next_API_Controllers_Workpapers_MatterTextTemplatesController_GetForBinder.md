[web] GET /api/matter-text-templates/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetForBinder)  [L80–L109] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service IMatterTextParsingService (AddScoped)
    └─ method ParseMatterText [L103]
      └─ implementation Workpapers.Next.ApplicationService.Features.MatterTextParsing.MatterTextParsingService.ParseMatterText [L18-L194]
  └─ sends_request GetMatterTextTemplatesWithWorkpaperRecordTemplateQuery [L97]

