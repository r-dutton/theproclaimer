[web] GET /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Search)  [L59–L78] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetMatterTextTemplatesQuery -> GetMatterTextTemplatesQueryHandler [L73]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.Matters.GetMatterTextTemplatesQueryHandler.Handle [L124–L217]
      └─ maps_to WorkpaperRecordTemplateLightDto [L155]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateLightDto) [L221]
      └─ uses_service IControlledRepository<MatterTextTemplate> (Scoped (inferred))
        └─ method ReadQuery [L174]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterTextTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L155]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetMatterTextTemplatesQuery
    └─ handlers 1
      └─ GetMatterTextTemplatesQueryHandler
    └─ mappings 1
      └─ WorkpaperRecordTemplateLightDto

