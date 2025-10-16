[web] DELETE /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Delete)  [L149–L157] status=200 [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.Remove [L155]
  └─ calls ReportContentRepository.LoadWriteProperties [L154]
  └─ calls PolicyVariantRepository.WriteQuery [L152]
  └─ delete PolicyVariant [L155]
    └─ reads_from PolicyVariants
  └─ write PolicyVariant [L152]
    └─ reads_from PolicyVariants
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L154]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ PolicyVariant writes=2 reads=0
    └─ services 1
      └─ ReportContentRepository

