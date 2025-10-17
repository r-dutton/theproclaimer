[web] GET /api/source-accounts/for-report  (Workpapers.Next.API.Controllers.SourceAccountsController.GetForReport)  [L109–L114] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceAccountsForReportQuery -> GetSourceAccountsForReportQueryHandler [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsForReportQueryHandler.Handle [L38–L125]
      └─ calls SourceAccountRepository.ReadQuery [L97]
      └─ maps_to FirmTolerancesDto [L117]
        └─ automapper.registration WorkpapersMappingProfile (Firm->FirmTolerancesDto) [L177]
      └─ maps_to BinderAccountRecordDto [L97]
      └─ maps_to SourceAccountForReportDto [L75]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountForReportDto) [L606]
      └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
        └─ method ReadQuery [L117]
          └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.ReadQuery
      └─ uses_service UserService
        └─ method GetFirmId [L116]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
            └─ uses_service Firm>
              └─ method GetFirmId [L139]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L67]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetSourceAccountsForReportQuery
    └─ handlers 1
      └─ GetSourceAccountsForReportQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ BinderAccountRecordDto
      └─ FirmTolerancesDto
      └─ SourceAccountForReportDto

