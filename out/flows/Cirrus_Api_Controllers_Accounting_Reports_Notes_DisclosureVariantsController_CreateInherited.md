[web] POST /api/accounting/reports/notes/disclosures/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.CreateInherited)  [L124–L135] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.ReadQuery [L129]
  └─ calls DisclosureVariantRepository.Add [L133]
  └─ calls DisclosureVariantRepository.ReadQuery [L130]
  └─ queries DisclosureTemplate [L129]
    └─ reads_from DisclosureTemplates
  └─ queries DisclosureVariant [L130]
    └─ reads_from DisclosureVariants
  └─ writes_to DisclosureVariant [L133]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L129]
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method ReadQuery [L130]

