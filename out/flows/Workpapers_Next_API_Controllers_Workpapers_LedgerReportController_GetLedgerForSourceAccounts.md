[web] GET /api/ledger-reports/{binderColumnDataId:Guid}/source-accounts  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetLedgerForSourceAccounts)  [L80–L114] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderColumnDataRepository.ReadQuery [L97]
  └─ query BinderColumnData [L97]
    └─ reads_from BinderColumnData
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method ReadQuery [L97]
      └─ ... (no implementation details available)
  └─ sends_request GetGeneralLedgerBySourceAccountQuery [L112]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
          └─ ... (no implementation details available)
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L121]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L136]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L184]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L101]
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

