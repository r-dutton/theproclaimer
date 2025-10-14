[web] POST /api/accounting/reports/page-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.Create)  [L84–L92] [auth=Authentication.UserPolicy]
  └─ calls ReportPageLayoutRepository.Add [L90]
  └─ writes_to ReportPageLayout [L90]
    └─ reads_from ReportPageLayouts
  └─ uses_service IControlledRepository<ReportPageLayout>
    └─ method Add [L90]
  └─ sends_request CanIAccessFileQuery [L88]
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

