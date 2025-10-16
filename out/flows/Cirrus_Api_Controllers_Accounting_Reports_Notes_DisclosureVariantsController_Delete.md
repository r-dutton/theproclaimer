[web] DELETE /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Delete)  [L167–L175] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.Remove [L173]
  └─ calls DisclosureVariantRepository.WriteQuery [L170]
  └─ calls ReportContentRepository.LoadWriteProperties [L171]
  └─ delete DisclosureVariant [L173]
    └─ reads_from DisclosureVariants
  └─ write DisclosureVariant [L170]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L170]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L171]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]

