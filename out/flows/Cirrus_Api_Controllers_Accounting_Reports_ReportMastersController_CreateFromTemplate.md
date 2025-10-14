[web] POST /api/accounting/reports/masters/templates/{templateId}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.CreateFromTemplate)  [L148–L152] [auth=user,admin]
  └─ sends_request CreateReportMasterFromTemplateCommand [L151]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportMasterTemplates.CreateReportMasterFromTemplateCommandHandler.Handle [L26–L156]
      └─ maps_to DisclosureTemplateLightDto [L63]
        └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateLightDto) [L829]
      └─ uses_service IControlledRepository<DisclosureTemplate>
        └─ method ReadQuery [L63]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L59]
      └─ uses_service IControlledRepository<ReportMaster>
        └─ method ReadQuery [L67]
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L129]
      └─ uses_service IControlledRepository<ReportPageType>
        └─ method WriteQuery [L60]
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L134]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L64]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]

