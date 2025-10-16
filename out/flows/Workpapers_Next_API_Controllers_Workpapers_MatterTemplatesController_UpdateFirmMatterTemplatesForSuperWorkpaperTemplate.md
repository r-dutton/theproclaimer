[web] PUT /api/matter-templates/for-super-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.UpdateFirmMatterTemplatesForSuperWorkpaperTemplate)  [L67–L77] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommand [L74]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Matters.UpdateFirmMatterTemplatesForSuperWorkpaperTemplateCommandHandler.Handle [L32–L64]
      └─ uses_service UserService
        └─ method IsSuperUser [L50]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method WriteQuery [L53]
          └─ ... (no implementation details available)

