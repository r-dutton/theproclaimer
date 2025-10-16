[web] POST /api/accounting/reports/notes/policies/variants/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Create)  [L94–L102] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.ReadQuery [L97]
  └─ calls PolicyVariantRepository.Add [L100]
  └─ insert PolicyVariant [L100]
    └─ reads_from PolicyVariants
  └─ query Policy [L97]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L97]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method Add [L100]
      └─ ... (no implementation details available)

