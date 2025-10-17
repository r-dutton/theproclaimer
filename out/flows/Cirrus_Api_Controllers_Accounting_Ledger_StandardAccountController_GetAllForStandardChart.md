[web] GET /api/accounting/ledger/standard-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForStandardChart)  [L43–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardAccountLightDto [L54]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightDto) [L445]
  └─ calls StandardAccountRepository.ReadQuery [L54]
  └─ query StandardAccount [L54]
    └─ reads_from StandardAccounts
  └─ sends_request GetDefaultStandardChartIdQuery -> GetDefaultStandardChartIdQueryHandler [L52]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
      └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
        └─ method ReadQuery [L36]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ requests 1
      └─ GetDefaultStandardChartIdQuery
    └─ handlers 1
      └─ GetDefaultStandardChartIdQueryHandler
    └─ mappings 1
      └─ StandardAccountLightDto

