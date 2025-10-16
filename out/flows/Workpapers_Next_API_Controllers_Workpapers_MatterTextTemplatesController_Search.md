[web] GET /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Search)  [L59–L78] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetMatterTextTemplatesQuery [L73]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.Matters.GetMatterTextTemplatesQueryHandler.Handle [L124–L217]
      └─ maps_to WorkpaperRecordTemplateLightDto [L155]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateLightDto) [L221]
      └─ uses_service IControlledRepository<MatterTextTemplate>
        └─ method ReadQuery [L174]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L155]
          └─ ... (no implementation details available)

