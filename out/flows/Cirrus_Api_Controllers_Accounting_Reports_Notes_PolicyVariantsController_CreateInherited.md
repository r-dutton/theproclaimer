[web] POST /api/accounting/reports/notes/policies/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.CreateInherited)  [L108–L117] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository (methods: Add,ReadQuery) [L115]
  └─ calls PolicyRepository.ReadQuery [L111]
  └─ insert PolicyVariant [L115]
    └─ reads_from PolicyVariants
  └─ query PolicyVariant [L112]
    └─ reads_from PolicyVariants
  └─ query Policy [L111]
  └─ impact_summary
    └─ entities 2 (writes=1, reads=2)
      └─ PolicyVariant writes=1 reads=1
      └─ Policy writes=0 reads=1

