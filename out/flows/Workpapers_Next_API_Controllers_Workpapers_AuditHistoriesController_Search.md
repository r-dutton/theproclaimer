[web] GET /api/audit-trail/search  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.Search)  [L27–L44] [auth=AuthorizationPolicies.User]
  └─ sends_request GetAuditHistoriesQuery [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.AuditHistories.GetAuditHistoriesQueryHandler.Handle [L28–L184]
      └─ uses_service AuditHistoryStorageService
        └─ method GetLogs [L182]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L56]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L142]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L101]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
  └─ sends_request CanIAccessBinderQuery [L36]
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

