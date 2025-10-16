[web] POST /api/accounting/reports/notes/policies/variants/tables  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.GetTables)  [L66–L88] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.ReadQuery [L69]
  └─ query PolicyVariant [L69]
    └─ reads_from PolicyVariants
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method ReadQuery [L69]
      └─ ... (no implementation details available)

