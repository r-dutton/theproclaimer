[web] POST /api/accounting/reports/notes/policies/variants/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Create)  [L94–L102] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.Add [L100]
  └─ calls PolicyRepository.ReadQuery [L97]
  └─ insert PolicyVariant [L100]
    └─ reads_from PolicyVariants
  └─ query Policy [L97]
  └─ impact_summary
    └─ entities 2 (writes=1, reads=1)
      └─ Policy writes=0 reads=1
      └─ PolicyVariant writes=1 reads=0

