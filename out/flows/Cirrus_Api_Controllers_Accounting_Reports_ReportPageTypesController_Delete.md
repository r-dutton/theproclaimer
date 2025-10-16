[web] DELETE /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Delete)  [L203–L210] status=200 [auth=user,admin]
  └─ calls ReportContentRepository.LoadWriteProperties [L207]
  └─ calls ReportPageTypeRepository.Remove [L208]
  └─ calls ReportPageTypeRepository.WriteQuery [L206]
  └─ delete ReportPageType [L208]
    └─ reads_from ReportPageTypes
  └─ write ReportPageType [L206]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L206]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L207]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]

