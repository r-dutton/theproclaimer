[web] POST /api/export/binders/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderExportController.StartBinderExport)  [L29–L37] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L33]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]
  └─ sends_request ExportPdfCommand [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Export.Commands.ExportPdfCommandHandler.Handle [L59–L492]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L237]
      └─ uses_service DocRaptorConfiguration
        └─ method ApiKey [L410]
      └─ uses_service ExportConfiguration
        └─ method MaxDocumentPackageSizeMb [L268]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L92]
      └─ uses_service UserService
        └─ method GetUser [L238]

