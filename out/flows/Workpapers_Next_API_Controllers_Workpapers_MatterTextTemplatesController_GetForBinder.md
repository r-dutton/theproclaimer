[web] GET /api/matter-text-templates/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetForBinder)  [L80–L109] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service IMatterTextParsingService (AddScoped)
    └─ method ParseMatterText [L103]
      └─ implementation Workpapers.Next.ApplicationService.Features.MatterTextParsing.MatterTextParsingService.ParseMatterText [L18-L194]
        └─ uses_client ClientRepository [L124]
        └─ uses_client Client [L122]
        └─ uses_service Binder
          └─ method ParseMatterText [L57]
            └─ implementation Workpapers.Next.DomainModel.Model.Workpapers.Binder.ParseMatterText [L52-L929]
  └─ sends_request GetMatterTextTemplatesWithWorkpaperRecordTemplateQuery [L97]
  └─ impact_summary
    └─ clients 2
      └─ Client
      └─ ClientRepository
    └─ services 1
      └─ IMatterTextParsingService
    └─ requests 1
      └─ GetMatterTextTemplatesWithWorkpaperRecordTemplateQuery

