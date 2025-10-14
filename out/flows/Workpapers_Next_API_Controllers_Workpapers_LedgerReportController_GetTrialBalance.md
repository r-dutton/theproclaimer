[web] GET /api/ledger-reports/{binderColumnDataId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetTrialBalance)  [L41–L74] [auth=AuthorizationPolicies.User]
  └─ calls BinderColumnDataRepository.ReadQuery [L50]
  └─ queries BinderColumnData [L50]
    └─ reads_from BinderColumnData
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method ReadQuery [L50]
  └─ sends_request GetTrialBalanceWithAdjustmentsQuery [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetTrialBalanceWithAdjustmentsQueryHandler.Handle [L44–L100]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteWithSourceAccountInfoDto [L97]
  └─ sends_request CanIAccessBinderQuery [L55]
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
  └─ sends_request GetTrialBalanceForDatesQuery [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L85]

