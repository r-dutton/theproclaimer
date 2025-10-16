[web] POST /api/accounting/reports/notes/disclosures/variants/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Create)  [L109–L118] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.ReadQuery [L112]
  └─ calls DisclosureVariantRepository.Add [L116]
  └─ query DisclosureTemplate [L112]
    └─ reads_from DisclosureTemplates
  └─ insert DisclosureVariant [L116]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L112]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method Add [L116]
      └─ ... (no implementation details available)

