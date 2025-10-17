[web] POST /api/accounting/reports/notes/disclosures/variants/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Create)  [L109–L118] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.Add [L116]
  └─ calls DisclosureTemplateRepository.ReadQuery [L112]
  └─ insert DisclosureVariant [L116]
    └─ reads_from DisclosureVariants
  └─ query DisclosureTemplate [L112]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 2 (writes=1, reads=1)
      └─ DisclosureTemplate writes=0 reads=1
      └─ DisclosureVariant writes=1 reads=0

