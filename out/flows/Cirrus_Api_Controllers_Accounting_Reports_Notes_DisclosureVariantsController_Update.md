[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Update)  [L140–L148] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.WriteQuery [L143]
  └─ calls ReportContentRepository.LoadWriteProperties [L144]
  └─ write DisclosureVariant [L143]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L143]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L144]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]

