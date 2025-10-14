[web] POST /api/binders/create-excel-document  (Workpapers.Next.API.Controllers.Workpapers.BindersController.CreateExcelDocument)  [L244–L256] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L248]
  └─ writes_to Binder [L248]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L248]
  └─ uses_service UserService
    └─ method GetUser [L251]
  └─ sends_request CreateBinderDocumentCommand [L251]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Binders.CreateBinderDocumentCommandHandler.Handle [L32–L66]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
  └─ sends_request CanIAccessBinderQuery [L249]
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

