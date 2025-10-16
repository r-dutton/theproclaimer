[web] POST /api/accounting/reports/notes/policies/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.CreateInherited)  [L108–L117] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.ReadQuery [L111]
  └─ calls PolicyVariantRepository.Add [L115]
  └─ calls PolicyVariantRepository.ReadQuery [L112]
  └─ insert PolicyVariant [L115]
    └─ reads_from PolicyVariants
  └─ query PolicyVariant [L112]
    └─ reads_from PolicyVariants
  └─ query Policy [L111]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L111]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method ReadQuery [L112]
      └─ ... (no implementation details available)

