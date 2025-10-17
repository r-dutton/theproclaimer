[web] GET /api/sources/{type}/ledger  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedger)  [L341–L402] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetGeneralLedgerBySourceAccountQuery -> GetGeneralLedgerBySourceAccountQueryHandler [L379]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
              └─ calls SourceAccountRepository.ReadQuery [L151]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L136]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L121]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQueryHandler.Handle [L38–L87]
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L356]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L71]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
            └─ logs ILogger<IRequestInfoService> [Warning] [L81]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ impact_summary
    └─ requests 4
      └─ CanIAccessBinderQuery
      └─ GetGeneralLedgerBySourceAccountQuery
      └─ GetGeneralLedgerForDatesQuery
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 4
      └─ CanIAccessBinderQueryHandler
      └─ GetGeneralLedgerBySourceAccountQueryHandler
      └─ GetGeneralLedgerForDatesQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ SourceAccountMappingDto
      └─ SourceLightDto

