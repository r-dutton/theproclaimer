[web] POST /api/accounting/reports/notes/disclosures/variants/tables  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.GetTables)  [L81–L103] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.ReadQuery [L84]
  └─ query DisclosureVariant [L84]
    └─ reads_from DisclosureVariants
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DisclosureVariant writes=0 reads=1

