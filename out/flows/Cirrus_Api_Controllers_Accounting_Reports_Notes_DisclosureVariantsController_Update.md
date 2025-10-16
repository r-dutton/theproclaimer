[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Update)  [L140–L148] status=200 [auth=Authentication.UserPolicy]
  └─ calls ReportContentRepository.LoadWriteProperties [L144]
  └─ calls DisclosureVariantRepository.WriteQuery [L143]
  └─ write DisclosureVariant [L143]
    └─ reads_from DisclosureVariants
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L144]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DisclosureVariant writes=1 reads=0
    └─ services 1
      └─ ReportContentRepository

