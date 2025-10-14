[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Update)  [L140–L148] [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.WriteQuery [L143]
  └─ calls ReportContentRepository.LoadWriteProperties [L144]
  └─ writes_to DisclosureVariant [L143]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L143]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L144]

