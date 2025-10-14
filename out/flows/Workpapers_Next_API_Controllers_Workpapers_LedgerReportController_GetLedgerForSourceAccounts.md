[web] GET /api/ledger-reports/{binderColumnDataId:Guid}/source-accounts  (Workpapers.Next.API.Controllers.Workpapers.LedgerReportController.GetLedgerForSourceAccounts)  [L80–L114] [auth=AuthorizationPolicies.User]
  └─ calls BinderColumnDataRepository.ReadQuery [L97]
  └─ queries BinderColumnData [L97]
    └─ reads_from BinderColumnData
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method ReadQuery [L97]
  └─ sends_request GetGeneralLedgerBySourceAccountQuery [L112]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L121]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L136]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L184]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]
  └─ sends_request CanIAccessBinderQuery [L101]
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

