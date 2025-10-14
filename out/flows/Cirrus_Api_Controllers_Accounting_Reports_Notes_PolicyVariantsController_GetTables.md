[web] POST /api/accounting/reports/notes/policies/variants/tables  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.GetTables)  [L66–L88] [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.ReadQuery [L69]
  └─ queries PolicyVariant [L69]
    └─ reads_from PolicyVariants
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method ReadQuery [L69]

