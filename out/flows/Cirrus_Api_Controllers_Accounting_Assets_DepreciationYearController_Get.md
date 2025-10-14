[web] GET /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Get)  [L42–L48] [auth=user]
  └─ maps_to DepreciationYearDto [L45]
    └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
  └─ calls DepreciationYearRepository.ReadQuery [L45]
  └─ queries DepreciationYear [L45]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L45]

