[web] POST /api/accounting/reports/notes/disclosures/variants/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Create)  [L109–L118] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.ReadQuery [L112]
  └─ calls DisclosureVariantRepository.Add [L116]
  └─ queries DisclosureTemplate [L112]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureVariant [L116]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L112]
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method Add [L116]

