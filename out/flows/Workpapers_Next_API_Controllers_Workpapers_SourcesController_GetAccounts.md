[web] PUT /api/sources/{type}/reimport-accounts  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAccounts)  [L225–L234] [auth=AuthorizationPolicies.User]
  └─ sends_request ImportAccountsFromApi [L231]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.ImportAccountsFromApiHandler.Handle [L33–L64]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L46]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L59]
  └─ sends_request CanIAccessBinderQuery [L228]
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

