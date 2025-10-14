[web] POST /api/accounting/reports/notes/disclosures/variants/tables  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.GetTables)  [L81–L103] [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.ReadQuery [L84]
  └─ queries DisclosureVariant [L84]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method ReadQuery [L84]

