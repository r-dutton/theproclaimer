[web] PUT /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Update)  [L92–L102] status=200 [auth=user]
  └─ calls DepreciationYearRepository.WriteQuery [L95]
  └─ write DepreciationYear [L95]
    └─ reads_from DepreciationYears
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DepreciationYear writes=1 reads=0

