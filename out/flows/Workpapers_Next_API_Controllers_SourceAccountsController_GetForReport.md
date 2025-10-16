[web] GET /api/source-accounts/for-report  (Workpapers.Next.API.Controllers.SourceAccountsController.GetForReport)  [L109–L114] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceAccountsForReportQuery [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsForReportQueryHandler.Handle [L38–L125]
      └─ maps_to SourceAccountForReportDto [L75]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountForReportDto) [L606]
      └─ maps_to BinderAccountRecordDto [L97]
      └─ maps_to FirmTolerancesDto [L117]
        └─ automapper.registration WorkpapersMappingProfile (Firm->FirmTolerancesDto) [L177]
      └─ uses_service UserService
        └─ method GetFirmId [L116]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L67]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Firm>
        └─ method ReadQuery [L117]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L77]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

