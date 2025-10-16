[web] GET /api/accounting/ledger/standard-accounts/recommendations  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForFile)  [L70–L91] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardAccountRecommendationDto [L80]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountRecommendationDto) [L444]
  └─ calls StandardAccountRepository.ReadQuery [L80]
  └─ query StandardAccount [L80]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)

