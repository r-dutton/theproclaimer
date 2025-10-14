[web] GET /api/journals/{binderId:guid}/report/profit-reconciliation  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetProfitReconciliationReport)  [L174–L180] [auth=AuthorizationPolicies.User]
  └─ sends_request GetProfitReconciliationReportQuery [L179]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetProfitReconciliationReportQueryHandler.Handle [L31–L155]
      └─ uses_service IControlledRepository<Journal>
        └─ method ReadQuery [L46]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L44]
  └─ sends_request CanIAccessBinderQuery [L177]
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

