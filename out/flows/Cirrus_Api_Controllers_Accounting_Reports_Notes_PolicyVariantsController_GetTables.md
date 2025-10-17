[web] POST /api/accounting/reports/notes/policies/variants/tables  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.GetTables)  [L66–L88] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.ReadQuery [L69]
  └─ query PolicyVariant [L69]
    └─ reads_from PolicyVariants
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PolicyVariant writes=0 reads=1

