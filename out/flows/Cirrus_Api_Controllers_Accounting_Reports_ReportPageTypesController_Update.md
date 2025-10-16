[web] PUT /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Update)  [L170–L177] status=200 [auth=user,admin]
  └─ calls ReportContentRepository.LoadWriteProperties [L174]
  └─ calls ReportPageTypeRepository.WriteQuery [L173]
  └─ write ReportPageType [L173]
    └─ reads_from ReportPageTypes
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L174]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportPageType writes=1 reads=0
    └─ services 1
      └─ ReportContentRepository

