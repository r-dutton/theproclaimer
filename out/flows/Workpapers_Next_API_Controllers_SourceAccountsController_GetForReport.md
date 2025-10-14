[web] GET /api/source-accounts/for-report  (Workpapers.Next.API.Controllers.SourceAccountsController.GetForReport)  [L109–L114] [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceAccountsForReportQuery [L113]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsForReportQueryHandler.Handle [L38–L125]
      └─ maps_to SourceAccountForReportDto [L75]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountForReportDto) [L606]
      └─ maps_to BinderAccountRecordDto [L97]
      └─ maps_to FirmTolerancesDto [L117]
        └─ automapper.registration WorkpapersMappingProfile (Firm->FirmTolerancesDto) [L177]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L67]
      └─ uses_service IControlledRepository<Firm>
        └─ method ReadQuery [L117]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L75]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L77]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
      └─ uses_service UserService
        └─ method GetFirmId [L116]

