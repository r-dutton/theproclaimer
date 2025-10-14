[web] DELETE /api/accounting/reports/page-layouts/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.Delete)  [L104–L112] [auth=Authentication.UserPolicy]
  └─ calls ReportPageLayoutRepository.Remove [L110]
  └─ calls ReportPageLayoutRepository.WriteQuery [L107]
  └─ writes_to ReportPageLayout [L110]
    └─ reads_from ReportPageLayouts
  └─ writes_to ReportPageLayout [L107]
    └─ reads_from ReportPageLayouts
  └─ uses_service IControlledRepository<ReportPageLayout>
    └─ method WriteQuery [L107]
  └─ sends_request CanIAccessFileQuery [L109]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]

