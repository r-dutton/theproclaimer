[web] GET /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Get)  [L42–L48] status=200 [auth=user]
  └─ maps_to DepreciationYearDto [L45]
    └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
  └─ calls DepreciationYearRepository.ReadQuery [L45]
  └─ query DepreciationYear [L45]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

