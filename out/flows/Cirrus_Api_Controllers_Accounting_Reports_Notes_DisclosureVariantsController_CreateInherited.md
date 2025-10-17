[web] POST /api/accounting/reports/notes/disclosures/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.CreateInherited)  [L124–L135] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository (methods: Add,ReadQuery) [L133]
  └─ calls DisclosureTemplateRepository.ReadQuery [L129]
  └─ insert DisclosureVariant [L133]
    └─ reads_from DisclosureVariants
  └─ query DisclosureVariant [L130]
    └─ reads_from DisclosureVariants
  └─ query DisclosureTemplate [L129]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 2 (writes=1, reads=2)
      └─ DisclosureVariant writes=1 reads=1
      └─ DisclosureTemplate writes=0 reads=1

