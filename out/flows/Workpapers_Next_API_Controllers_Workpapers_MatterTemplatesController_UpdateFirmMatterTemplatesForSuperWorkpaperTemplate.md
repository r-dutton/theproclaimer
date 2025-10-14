[web] PUT /api/matter-templates/for-super-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.UpdateFirmMatterTemplatesForSuperWorkpaperTemplate)  [L67–L77] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommand [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Matters.UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommandHandler.Handle [L32–L64]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method WriteQuery [L53]
      └─ uses_service UserService
        └─ method IsSuperUser [L50]

