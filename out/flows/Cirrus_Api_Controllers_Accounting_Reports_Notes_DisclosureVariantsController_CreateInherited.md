[web] POST /api/accounting/reports/notes/disclosures/variants/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.CreateInherited)  [L124–L135] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.ReadQuery [L129]
  └─ calls DisclosureVariantRepository.Add [L133]
  └─ calls DisclosureVariantRepository.ReadQuery [L130]
  └─ query DisclosureTemplate [L129]
    └─ reads_from DisclosureTemplates
  └─ insert DisclosureVariant [L133]
    └─ reads_from DisclosureVariants
  └─ query DisclosureVariant [L130]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L129]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method ReadQuery [L130]
      └─ ... (no implementation details available)

