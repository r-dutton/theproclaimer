[web] POST /api/journals/{id}/excel-import  (Workpapers.Next.API.Controllers.Workpapers.JournalController.ImportFromExcelFile)  [L256–L264] [auth=AuthorizationPolicies.User]
  └─ sends_request ParseUploadedExcelFileToImportModelCommand [L261]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ParseUploadedExcelFileToImportModelCommandHandler.Handle [L25–L60]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetFileBytes [L54]
  └─ sends_request CanIAccessBinderQuery [L259]
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
  └─ returns TrialBalanceParseResult [L261]

