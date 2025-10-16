[web] GET /api/accounting/ledger/standard-accounts/recommendations  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetAllForFile)  [L70–L91] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardAccountRecommendationDto [L80]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountRecommendationDto) [L444]
  └─ calls StandardAccountRepository.ReadQuery [L80]
  └─ query StandardAccount [L80]
    └─ reads_from StandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ mappings 1
      └─ StandardAccountRecommendationDto

