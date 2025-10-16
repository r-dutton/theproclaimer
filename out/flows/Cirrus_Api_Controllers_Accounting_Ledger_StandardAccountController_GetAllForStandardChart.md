[web] GET /api/accounting/ledger/standard-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForStandardChart)  [L43–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardAccountLightDto [L54]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightDto) [L445]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountLightDto) [L690]
  └─ calls StandardAccountRepository.ReadQuery [L54]
  └─ query StandardAccount [L54]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)
  └─ sends_request GetDefaultStandardChartIdQuery [L52]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L36]
          └─ ... (no implementation details available)

