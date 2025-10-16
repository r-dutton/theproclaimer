[web] POST /api/accounting/reports/masters/templates/{templateId}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.CreateFromTemplate)  [L148–L152] status=201 [auth=user,admin]
  └─ sends_request CreateReportMasterFromTemplateCommand -> CreateReportMasterFromTemplateCommandHandler [L151]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportMasterTemplates.CreateReportMasterFromTemplateCommandHandler.Handle [L26–L156]
      └─ maps_to DisclosureTemplateLightDto [L63]
        └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateLightDto) [L829]
      └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
        └─ method ReadQuery [L134]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
      └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
        └─ method ReadQuery [L129]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<ReportMaster> (Scoped (inferred))
        └─ method ReadQuery [L67]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportMasterRepository.ReadQuery
      └─ uses_service IControlledRepository<DisclosureTemplate> (Scoped (inferred))
        └─ method ReadQuery [L63]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<ReportPageType> (Scoped (inferred))
        └─ method WriteQuery [L60]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportPageTypeRepository.WriteQuery
      └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
        └─ method ReadQuery [L59]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateReportMasterFromTemplateCommand
    └─ handlers 1
      └─ CreateReportMasterFromTemplateCommandHandler
    └─ mappings 1
      └─ DisclosureTemplateLightDto

