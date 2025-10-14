[web] POST /api/accounting/reports/notes/policies/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.CreateInherited)  [L108–L117] [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.ReadQuery [L111]
  └─ calls PolicyVariantRepository.Add [L115]
  └─ calls PolicyVariantRepository.ReadQuery [L112]
  └─ queries PolicyVariant [L112]
    └─ reads_from PolicyVariants
  └─ writes_to PolicyVariant [L115]
    └─ reads_from PolicyVariants
  └─ queries Policy [L111]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L111]
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method ReadQuery [L112]

