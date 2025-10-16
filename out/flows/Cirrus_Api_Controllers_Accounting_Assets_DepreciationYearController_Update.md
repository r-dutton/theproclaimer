[web] PUT /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Update)  [L92–L102] status=200 [auth=user]
  └─ calls DepreciationYearRepository.WriteQuery [L95]
  └─ write DepreciationYear [L95]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method WriteQuery [L95]
      └─ ... (no implementation details available)

