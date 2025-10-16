[web] GET /api/ledger-reports/{binderColumnDataId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetTrialBalance)  [L41–L74] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderColumnDataRepository.ReadQuery [L50]
  └─ query BinderColumnData [L50]
    └─ reads_from BinderColumnData
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method ReadQuery [L50]
      └─ ... (no implementation details available)
  └─ sends_request GetTrialBalanceWithAdjustmentsQuery [L64]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetTrialBalanceWithAdjustmentsQueryHandler.Handle [L44–L100]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteWithSourceAccountInfoDto [L97]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L55]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ sends_request GetTrialBalanceForDatesQuery [L65]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L85]
          └─ ... (no implementation details available)

