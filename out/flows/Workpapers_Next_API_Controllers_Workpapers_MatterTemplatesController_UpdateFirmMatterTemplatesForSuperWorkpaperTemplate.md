[web] PUT /api/matter-templates/for-super-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.UpdateFirmMatterTemplatesForSuperWorkpaperTemplate)  [L67–L77] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommand -> UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommandHandler [L74]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Matters.UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommandHandler.Handle [L32–L64]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method WriteQuery [L53]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.WriteQuery
      └─ uses_service UserService
        └─ method IsSuperUser [L50]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommand
    └─ handlers 1
      └─ UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommandHandler
    └─ caches 1
      └─ IMemoryCache

