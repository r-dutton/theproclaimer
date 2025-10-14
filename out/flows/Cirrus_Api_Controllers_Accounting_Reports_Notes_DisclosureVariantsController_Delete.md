[web] DELETE /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Delete)  [L167–L175] [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.Remove [L173]
  └─ calls DisclosureVariantRepository.WriteQuery [L170]
  └─ calls ReportContentRepository.LoadWriteProperties [L171]
  └─ writes_to DisclosureVariant [L173]
    └─ reads_from DisclosureVariants
  └─ writes_to DisclosureVariant [L170]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L170]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L171]

