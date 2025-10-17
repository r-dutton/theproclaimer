[web] PUT /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Update)  [L122–L130] status=200 [auth=Authentication.UserPolicy]
  └─ calls ReportContentRepository.LoadWriteProperties [L127]
  └─ calls PolicyVariantRepository.WriteQuery [L125]
  └─ write PolicyVariant [L125]
    └─ reads_from PolicyVariants
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L127]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ PolicyVariant writes=1 reads=0
    └─ services 1
      └─ ReportContentRepository

