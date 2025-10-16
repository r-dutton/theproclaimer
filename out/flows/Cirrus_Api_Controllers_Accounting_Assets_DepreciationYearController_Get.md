[web] GET /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Get)  [L42–L48] status=200 [auth=user]
  └─ maps_to DepreciationYearDto [L45]
    └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
  └─ calls DepreciationYearRepository.ReadQuery [L45]
  └─ query DepreciationYear [L45]
    └─ reads_from DepreciationYears
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DepreciationYear writes=0 reads=1
    └─ mappings 1
      └─ DepreciationYearDto

